using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Transactions;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using AwesomiumSharp;
using BetTeamsBattle.Configuration;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Repositories.Specific.Entity;
using BetTeamsBattle.Data.Repositories.Specific.QueuedBetUrl;
using BetTeamsBattle.ScreenShotsMaker.QueuedBetUrlProcessor.Interfaces;
using BetTeamsBattle.ScreenShotsMaker.ScreenShotMakingManager.Interfaces;
using NLog;

namespace BetTeamsBattle.ScreenShotsMaker.ScreenShotMakingManager
{
    internal class ScreenShotsMakingManager : IScreenShotsMakingManager
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly IRepository<QueuedBetUrl> _repositoryOfQueuedBetUrl;
        private readonly IQueuedBetUrlProcessor _queuedBetUrlProcessor;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(10);

        private AmazonS3 _amazonS3Client;
        private IList<long> _queuedBetUrlsIdsInProcessing = new List<long>();

        public ScreenShotsMakingManager(IRepository<QueuedBetUrl> repositoryOfQueuedBetUrl,
            IQueuedBetUrlProcessor queuedBetUrlProcessor)
        {
            _repositoryOfQueuedBetUrl = repositoryOfQueuedBetUrl;
            _queuedBetUrlProcessor = queuedBetUrlProcessor;
        }

        #region IScreenShotsMakingManager Members

        public void Run()
        {
            WebCore.Initialize(new WebCoreConfig { CustomCSS = "::-webkit-scrollbar { visibility: hidden; }" });

            string accessKeyId = AppSettings.AmazonAccessKeyId;
            string secretAccessKeyId = AppSettings.AmazonSecretAccessKeyId;
            _amazonS3Client = AWSClientFactory.CreateAmazonS3Client(accessKeyId, secretAccessKeyId);

            var synchronizationContext = SynchronizationContext.Current;

            while (true)
            {
                List<long> queuedBetUrlsIds =
                    _repositoryOfQueuedBetUrl.Get(
                        QueuedBetUrlSpecifications.NotProcessed() &&
                        !EntitySpecifications.EntityIdIsContainedIn<QueuedBetUrl>(_queuedBetUrlsIdsInProcessing)).
                        OrderBy(qbu => qbu.Id).
                        Select(qbu => qbu.Id).ToList();
                if (queuedBetUrlsIds.Count == 0)
                {
                    Thread.Sleep(1000);
                    continue;
                }

                foreach (long queuedBetUrlId in queuedBetUrlsIds)
                {
                    _semaphore.Wait();

                    lock (_queuedBetUrlsIdsInProcessing)
                    {
                        queuedBetUrlsIds.Add(queuedBetUrlId);
                    }

                    var thread = new Thread(() =>
                        {
                            try
                            {
                                _queuedBetUrlProcessor.Process(queuedBetUrlId, _amazonS3Client, synchronizationContext);
                            }
                            catch (Exception ex)
                            {
                                _logger.ErrorException("Can't make screenshot", ex);
                            }
                            finally
                            {
                                lock (_queuedBetUrlsIdsInProcessing)
                                {
                                    _queuedBetUrlsIdsInProcessing.Remove(queuedBetUrlId);
                                }

                                _semaphore.Release();
                            }
                        });
                    thread.Start();
                }
            }

            WebCore.Shutdown();
        }

        #endregion

        private void ProcessInNewThread(object obj)
        {
            var queuedBetUrlId = (long)obj;
            
        }
    }
}