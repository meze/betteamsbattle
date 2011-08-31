using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Amazon;
using BetTeamsBattle.Configuration;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Repositories.Specifications;
using BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker;
using BetTeamsBattle.Screenshots.BettScreenshotsManager.Interfaces;
using NLog;

namespace BetTeamsBattle.Screenshots.BettScreenshotsManager
{
    internal class ScreenshotsMakingManager : IScreenshotsMakingManager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IRepository<BetScreenshot> _repositoryOfBetScreenshot;
        private readonly IBetScreenshotProcessor _betScreenshotProcessor;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(10);

        private Amazon.S3.AmazonS3 _amazonS3Client;
        private IList<long> _beingProcessedBetScreenshotsIds = new List<long>();
        private SynchronizationContext _synchronizationContext;

        private bool _closed = false;
        private Thread _pollingThread;

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

            _pollingThread = new Thread(StartPolling);
            _pollingThread.Start();
        }

        #endregion

        private void StartPolling()
        {
            while (!_closed)
            {
                IEnumerable<long> notProcessedBetScreenshotsIds;
                try
                {

                    notProcessedBetScreenshotsIds =
                        _repositoryOfBetScreenshot.Get(
                            BetScreenshotSpecifications.NotProcessed() &&
                            EntitySpecifications.IdIsNotContainedIn<BetScreenshot>(_beingProcessedBetScreenshotsIds)).
                            OrderBy(qbu => qbu.Id).
                            Select(qbu => qbu.Id).ToList();
                }
                catch (Exception ex)
                {
                    Logger.ErrorException("Can't retrieve notProcessedScreenshotsIds", ex);
                    continue;
                }
                if (notProcessedBetScreenshotsIds.Count() == 0)
                {
                    Thread.Sleep(1000);
                    continue;
                }

                foreach (long notProcessedBetScreenshotId in notProcessedBetScreenshotsIds)
                {
                    _semaphore.Wait();

                    lock (_beingProcessedBetScreenshotsIds)
                    {
                        _beingProcessedBetScreenshotsIds.Add(notProcessedBetScreenshotId);
                    }

                    var thread = new Thread(ProcessInNewThread);
                    thread.Start(notProcessedBetScreenshotId);
                }
            }
        }

        private void ProcessInNewThread(object obj)
        {
            var betScreenshotId = (long)obj;

            try
            {
                _betScreenshotProcessor.Process(betScreenshotId, _amazonS3Client, _synchronizationContext);
            }
            catch (Exception ex)
            {
                Logger.ErrorException("Can't make screenshot", ex);
            }
            finally
            {
                lock (_beingProcessedBetScreenshotsIds)
                {
                    _beingProcessedBetScreenshotsIds.Remove(betScreenshotId);
                }

                _semaphore.Release();
            }
        }

        public void Stop()
        {
            _closed = true;

            _pollingThread.Join();

            AwesomiumCore.Shutdown();
        }
    }
}