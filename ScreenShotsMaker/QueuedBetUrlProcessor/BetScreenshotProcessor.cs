using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading;
using System.Transactions;
using Amazon.S3;
using Amazon.S3.Model;
using BetTeamsBattle.Configuration;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Repositories.Specifications;
using BetTeamsBattle.Data.Repositories.UnitOfWork;
using BetTeamsBattle.Data.Repositories.UnitOfWork.Interfaces;
using BetTeamsBattle.ScreenShotsMaker.QueuedBetUrlProcessor.Interfaces;
using BetTeamsBattle.ScreenShotsMaker.ScreenShotMaker.Interfaces;
using NLog;
using BetTeamsBattle.Data.Repositories.Infrastructure.TransactionScope.Interfaces;

namespace BetTeamsBattle.ScreenShotsMaker.QueuedBetUrlProcessor
{
    internal class BetScreenshotProcessor : IBetScreenshotProcessor
    {
        private readonly ITransactionScopeFactory _transactionScopeFactory;
        private readonly IUnitOfWorkScopeFactory _unitOfWorkScopeFactory;
        private readonly IRepository<BetScreenshot> _repositoryOfBetScreenshot;
        private readonly IScreenShotMakerFactory _screenShotMakerFactory;
        private readonly IScreenshotAmazonS3Putter _screenshotAmazonS3Putter;

        public BetScreenshotProcessor(ITransactionScopeFactory transactionScopeFactory, IUnitOfWorkScopeFactory unitOfWorkScopeFactory, IRepository<BetScreenshot> repositoryOfBetScreenshot, IScreenShotMakerFactory screenShotMakerFactory, IScreenshotAmazonS3Putter screenshotAmazonS3Putter)
        {
            _transactionScopeFactory = transactionScopeFactory;
            _unitOfWorkScopeFactory = unitOfWorkScopeFactory;
            _repositoryOfBetScreenshot = repositoryOfBetScreenshot;
            _screenShotMakerFactory = screenShotMakerFactory;
            _screenshotAmazonS3Putter = screenshotAmazonS3Putter;
        }

        public void Process(long queuedBetUrlId, AmazonS3 amazonS3Client, SynchronizationContext synchronizationContext)
        {
            using (var transactionScope = _transactionScopeFactory.Create())
            {
                using (var unitOfWorkScope = _unitOfWorkScopeFactory.Create())
                {
                    var betScreenshot = _repositoryOfBetScreenshot.Get(EntitySpecifications.IdIsEqualTo<BetScreenshot>(queuedBetUrlId)).Include(bs => bs.BattleBet).Single();

                    betScreenshot.ProcessingStartDateTime = DateTime.UtcNow;

                    var screenShotMaker = _screenShotMakerFactory.Create();

                    var screenshotPngStream = screenShotMaker.GetScreenshotPngStream(betScreenshot.BattleBet.Url, synchronizationContext);

                    _screenshotAmazonS3Putter.PutScreenshot(amazonS3Client, betScreenshot, screenshotPngStream);

                    betScreenshot.ProcessingFinishDateTime = DateTime.UtcNow;
                    unitOfWorkScope.SaveChanges();
                }

                transactionScope.Complete();
            }
        }
    }
}