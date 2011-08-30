using System;
using System.IO;
using System.Linq;
using System.Threading;
using BetTeamsBattle.Configuration;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Repositories.Specifications;
using BetTeamsBattle.Data.Repositories.UnitOfWork.Interfaces;
using BetTeamsBattle.Data.Repositories.Infrastructure.TransactionScope.Interfaces;
using BetTeamsBattle.Screenshots.AmazonS3.Interfaces;
using BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker.Interfaces;
using BetTeamsBattle.Screenshots.BettScreenshotsManager.Interfaces;
using NLog;

namespace BetTeamsBattle.Screenshots.BettScreenshotsManager
{
    internal class BetScreenshotProcessor : IBetScreenshotProcessor
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ITransactionScopeFactory _transactionScopeFactory;
        private readonly IUnitOfWorkScopeFactory _unitOfWorkScopeFactory;
        private readonly IRepository<BetScreenshot> _repositoryOfBetScreenshot;
        private readonly IRepository<BattleBet> _repositoryOfBattleBet;
        private readonly IScreenshotMakerFactory _screenshotMakerFactory;
        private readonly IScreenshotAmazonS3Putter _screenshotAmazonS3Putter;
        private readonly IBetScreenshotPathService _betScreenshotPathService;

        public BetScreenshotProcessor(ITransactionScopeFactory transactionScopeFactory, IUnitOfWorkScopeFactory unitOfWorkScopeFactory, IRepository<BetScreenshot> repositoryOfBetScreenshot, IRepository<BattleBet> repositoryOfBattleBet, IScreenshotMakerFactory screenshotMakerFactory, IScreenshotAmazonS3Putter screenshotAmazonS3Putter, IBetScreenshotPathService betScreenshotPathService)
        {
            _transactionScopeFactory = transactionScopeFactory;
            _unitOfWorkScopeFactory = unitOfWorkScopeFactory;
            _repositoryOfBetScreenshot = repositoryOfBetScreenshot;
            _repositoryOfBattleBet = repositoryOfBattleBet;
            _screenshotMakerFactory = screenshotMakerFactory;
            _screenshotAmazonS3Putter = screenshotAmazonS3Putter;
            _betScreenshotPathService = betScreenshotPathService;
        }

        public void Process(long betScreenshotId, Amazon.S3.AmazonS3 amazonS3Client, SynchronizationContext synchronizationContext)
        {
            using (var transactionScope = _transactionScopeFactory.Create())
            {
                try
                {
                    ProcessCore(betScreenshotId, amazonS3Client, synchronizationContext);

                    transactionScope.Complete();
                }
                catch (Exception ex)
                {
                    Logger.ErrorException(String.Format("Failed to process betScreenshotId = {0}. Was not saved", betScreenshotId), ex);
                }
            }
        }

        private void ProcessCore(long betScreenshotId, Amazon.S3.AmazonS3 amazonS3Client, SynchronizationContext synchronizationContext)
        {
            using (var unitOfWorkScope = _unitOfWorkScopeFactory.Create())
            {
                BetScreenshot betScreenshot = null;
                try
                {
                    betScreenshot = _repositoryOfBetScreenshot.Get(EntitySpecifications.IdIsEqualTo<BetScreenshot>(betScreenshotId)).Single();
                    var battleBet = _repositoryOfBattleBet.Get(BattleBetSpecifications.BetScreenshotOwner(betScreenshot.Id)).Single();

                    betScreenshot.StartedProcessingDateTime = DateTime.UtcNow;

                    var screenshotEncodedStream = GetScreenshot(battleBet.Url, betScreenshot, synchronizationContext);

                    PutScreenshot(amazonS3Client, screenshotEncodedStream, betScreenshot);

                    betScreenshot.FinishedProcessingDateTime = DateTime.UtcNow;

                    betScreenshot.StatusEnum = BetScreenshotStatus.Processed;
                }
                catch (Exception ex)
                {
                    Logger.TraceException(String.Format("Failed to process betScreenshotId = {0}. Trying to save as failed", betScreenshotId), ex);
                    betScreenshot.StatusEnum = BetScreenshotStatus.Failed;
                }
                finally
                {
                    unitOfWorkScope.SaveChanges();
                }
            }
        }

        private Stream GetScreenshot(string battleBetUrl, BetScreenshot betScreenshot, SynchronizationContext synchronizationContext)
        {
            betScreenshot.StartedScreenshotRetrievalDateTime = DateTime.UtcNow;

            var screenShotMaker = _screenshotMakerFactory.Create();
            var screenshotEncodedStream = screenShotMaker.GetScreenshotEncodedStream(battleBetUrl, synchronizationContext);

            betScreenshot.FinishedScreenshotRetrievalDateTime = DateTime.UtcNow;

            return screenshotEncodedStream;
        }

        private void PutScreenshot(Amazon.S3.AmazonS3 amazonS3Client, Stream screenshotJpegStream, BetScreenshot betScreenshot)
        {
            betScreenshot.StartedScreenshotSavingDateTime = DateTime.UtcNow;

            var path = _betScreenshotPathService.GetPath(betScreenshot.Id);
            _screenshotAmazonS3Putter.PutScreenshot(amazonS3Client, AppSettings.AmazonBucketName, path, screenshotJpegStream);

            betScreenshot.FinishedScreenshotSavingDateTime = DateTime.UtcNow;
        }
    }
}