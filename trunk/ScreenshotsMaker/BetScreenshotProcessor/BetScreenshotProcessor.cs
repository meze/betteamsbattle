using System;
using System.IO;
using System.Linq;
using System.Threading;
using Amazon.S3;
using BetTeamsBattle.Configuration;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Repositories.Specifications;
using BetTeamsBattle.Data.Repositories.UnitOfWork.Interfaces;
using BetTeamsBattle.ScreenShotsMaker.ScreenShotMaker.Interfaces;
using BetTeamsBattle.ScreenshotsMaker.BetScreenshotProcessor.Interfaces;
using BetTeamsBattle.Data.Repositories.Infrastructure.TransactionScope.Interfaces;

namespace BetTeamsBattle.ScreenshotsMaker.BetScreenshotProcessor
{
    internal class BetScreenshotProcessor : IBetScreenshotProcessor
    {
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

        public void Process(long betScreenshotId, AmazonS3 amazonS3Client, SynchronizationContext synchronizationContext)
        {
            using (var transactionScope = _transactionScopeFactory.Create())
            {
                try
                {
                    ProcessCore(betScreenshotId, amazonS3Client, synchronizationContext);

                    transactionScope.Complete();
                }
                catch
                {
                    //ToDo: add NLog error here
                }
            }
        }

        private void ProcessCore(long betScreenshotId, AmazonS3 amazonS3Client, SynchronizationContext synchronizationContext)
        {
            using (var unitOfWorkScope = _unitOfWorkScopeFactory.Create())
            {
                BetScreenshot betScreenshot = null;
                try
                {
                    betScreenshot = _repositoryOfBetScreenshot.Get(EntitySpecifications.IdIsEqualTo<BetScreenshot>(betScreenshotId)).Single();
                    var battleBet = _repositoryOfBattleBet.Get(BattleBetSpecifications.BetScreenshotOwner(betScreenshot.Id)).Single();

                    betScreenshot.StartedProcessingDateTime = DateTime.UtcNow;

                    var screenshotPngStream = GetScreenshot(battleBet.Url, betScreenshot, synchronizationContext);

                    PutScreenshot(amazonS3Client, screenshotPngStream, battleBet, betScreenshot);

                    betScreenshot.FinishedProcessingDateTime = DateTime.UtcNow;

                    betScreenshot.StatusEnum = BetScreenshotStatus.Processed;
                }
                catch (Exception ex)
                {
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
            var screenshotPngStream = screenShotMaker.GetScreenshotPngStream(battleBetUrl, synchronizationContext);

            betScreenshot.FinishedScreenshotRetrievalDateTime = DateTime.UtcNow;

            return screenshotPngStream;
        }

        private void PutScreenshot(AmazonS3 amazonS3Client, Stream screenshotPngStream, BattleBet battleBet, BetScreenshot betScreenshot)
        {
            betScreenshot.StartedScreenshotSavingDateTime = DateTime.UtcNow;

            var path = _betScreenshotPathService.GetPath(betScreenshot.Id);
            _screenshotAmazonS3Putter.PutScreenshot(amazonS3Client, AppSettings.AmazonBucketName, path, screenshotPngStream);

            betScreenshot.FinishedScreenshotSavingDateTime = DateTime.UtcNow;
        }
    }
}