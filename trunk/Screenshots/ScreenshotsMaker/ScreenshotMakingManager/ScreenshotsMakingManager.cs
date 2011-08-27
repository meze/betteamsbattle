using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Amazon;
using Amazon.S3;
using BetTeamsBattle.AwesomiumScreenshotMaker;
using BetTeamsBattle.BettScreenshotsManager.BetScreenshotProcessor.Interfaces;
using BetTeamsBattle.BettScreenshotsManager.ScreenshotMakingManager.Interfaces;
using BetTeamsBattle.Configuration;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Repositories.Specifications;
using NLog;

namespace BetTeamsBattle.BettScreenshotsManager.ScreenshotMakingManager
{
    internal class ScreenshotsMakingManager : IScreenshotsMakingManager
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly IRepository<BetScreenshot> _repositoryOfBetScreenshot;
        private readonly IBetScreenshotProcessor _betScreenshotProcessor;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(10);

        private AmazonS3 _amazonS3Client;
        private IList<long> _queuedBetUrlsIdsInProcessing = new List<long>();
        private SynchronizationContext _synchronizationContext;

        public ScreenshotsMakingManager(IRepository<BetScreenshot> repositoryOfBetScreenshot,
            IBetScreenshotProcessor betScreenshotProcessor)
        {
            _repositoryOfBetScreenshot = repositoryOfBetScreenshot;
            _betScreenshotProcessor = betScreenshotProcessor;
        }

        #region IScreenshotsMakingManager Members

        public void Run()
        {
            AwesomiumCore.Initialze();

            string accessKeyId = AppSettings.AmazonAccessKeyId;
            string secretAccessKeyId = AppSettings.AmazonSecretAccessKeyId;
            _amazonS3Client = AWSClientFactory.CreateAmazonS3Client(accessKeyId, secretAccessKeyId);

            _synchronizationContext = SynchronizationContext.Current;

            while (true)
            {
                var queuedBetUrlsIds =
                    _repositoryOfBetScreenshot.Get(
                        BetScreenshotSpecifications.NotProcessed() &&
                        EntitySpecifications.IdIsNotContainedIn<BetScreenshot>(_queuedBetUrlsIdsInProcessing)).
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

                    var thread = new Thread(ProcessInNewThread);
                    thread.Start(queuedBetUrlId);
                }
            }

            AwesomiumCore.Shutdown();
        }

        #endregion

        private void ProcessInNewThread(object obj)
        {
            var queuedBetUrlId = (long)obj;

            try
            {
                _betScreenshotProcessor.Process(queuedBetUrlId, _amazonS3Client, _synchronizationContext);
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
        }
    }
}