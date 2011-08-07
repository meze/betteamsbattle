using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Amazon.S3;
using Amazon.S3.Model;
using AwesomiumSharp;
using BetTeamsBattle.Configuration;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.ContextScope;
using BetTeamsBattle.Data.Repositories.Specifications;
using NLog;
using ScreenShotsMaker.Interfaces;
using Silverlight.Samples;

namespace ScreenShotsMaker
{
    internal class ScreenShotsMakingManager : IScreenShotsMakingManager
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();

        private SemaphoreSlim _semaphore = new SemaphoreSlim(10);

        private readonly IScreenShotMakerFactory _screenShotMakerFactory;
        private readonly IRepository<QueuedBetUrl> _repositoryOfQueuedBetUrl;

        private AmazonS3 _client;
        private IList<long> _queuedBetUrlsIdsInProcessing = new List<long>();

        public ScreenShotsMakingManager(IScreenShotMakerFactory screenShotMakerFactory, IRepository<QueuedBetUrl> repositoryOfQueuedBetUrl)
        {
            _screenShotMakerFactory = screenShotMakerFactory;
            _repositoryOfQueuedBetUrl = repositoryOfQueuedBetUrl;
        }

        public void Run()
        {
            WebCore.Initialize(new WebCoreConfig() { CustomCSS = "::-webkit-scrollbar { visibility: hidden; }" });

            var accessKeyId = AppSettings.AmazonAccessKeyId;
            var secretAccessKeyId = AppSettings.AmazonSecretAccessKeyId;
            _client = Amazon.AWSClientFactory.CreateAmazonS3Client(accessKeyId, secretAccessKeyId);

            while (true)
            {
                var queuedBetUrlsIds = _repositoryOfQueuedBetUrl.Get(QueuedBetUrlSpecifications.NotProcessed()).OrderBy(qbu => qbu.Id).Select(qbu => qbu.Id).ToList();
                if (queuedBetUrlsIds.Count == 0)
                {
                    Thread.Sleep(1000);
                    continue;
                }

                foreach (var queuedBetUrlId in queuedBetUrlsIds)
                {
                    _semaphore.Wait();

                    var thread = new Thread(ProcessInNewThread);
                    thread.Start(queuedBetUrlId);
                }
            }

            WebCore.Shutdown();
        }

        private void ProcessInNewThread(object obj)
        {
            var queuedBetUrlId = (long) obj;

            try
            {
                using (var transactionScope = new TransactionScope())
                {
                    using (var contextScope = new ContextScope())
                    {
                        var queuedBetUrl = _repositoryOfQueuedBetUrl.Get(EntitySpecifications.EntityIdIsEqualTo<QueuedBetUrl>(queuedBetUrlId)).Single();
                        queuedBetUrl.StartDateTime = DateTime.UtcNow;
                        contextScope.SaveChanges();

                        var screenShotMaker = _screenShotMakerFactory.Create();

                        int width;
                        int height;
                        var screenShotFileName = screenShotMaker.MakeScreenshot(queuedBetUrl.Url, out width, out height);

                        var putObjectRequest = new PutObjectRequest();

                        var path = queuedBetUrl.TypeEnum == QueuedBetUrlType.Open
                                       ? AppSettings.AmazonOpenBetScreenshotsPath
                                       : AppSettings.AmazonCloseBetScreenshotsPath;
                        putObjectRequest.
                            WithFilePath(screenShotFileName).
                            WithBucketName(AppSettings.AmazonBucketName).
                            WithKey(String.Format("{0}{1}.png", path, queuedBetUrl.Id));

                        _client.PutObject(putObjectRequest);

                        queuedBetUrl.FinishDateTime = DateTime.UtcNow;
                        contextScope.SaveChanges();

                        transactionScope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorException("Can't make screenshot", ex);
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}