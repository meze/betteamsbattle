using System;
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
    internal class QueuedBetUrlProcessor : IQueuedBetUrlProcessor
    {
        private readonly ITransactionScopeFactory _transactionScopeFactory;
        private readonly IUnitOfWorkScopeFactory _unitOfWorkScopeFactory;
        private readonly IRepository<QueuedBetUrl> _repositoryOfQueuedBetUrl;
        private readonly IScreenShotMakerFactory _screenShotMakerFactory;
        private readonly IScreenshotAmazonS3Putter _screenshotAmazonS3Putter;

        public QueuedBetUrlProcessor(ITransactionScopeFactory transactionScopeFactory, IUnitOfWorkScopeFactory unitOfWorkScopeFactory, IRepository<QueuedBetUrl> repositoryOfQueuedBetUrl, IScreenShotMakerFactory screenShotMakerFactory, IScreenshotAmazonS3Putter screenshotAmazonS3Putter)
        {
            _transactionScopeFactory = transactionScopeFactory;
            _unitOfWorkScopeFactory = unitOfWorkScopeFactory;
            _repositoryOfQueuedBetUrl = repositoryOfQueuedBetUrl;
            _screenShotMakerFactory = screenShotMakerFactory;
            _screenshotAmazonS3Putter = screenshotAmazonS3Putter;
        }

        public void Process(long queuedBetUrlId, AmazonS3 amazonS3Client, SynchronizationContext synchronizationContext)
        {
            using (var transactionScope = _transactionScopeFactory.Create())
            {
                using (var contextScope = _unitOfWorkScopeFactory.Create())
                {
                    QueuedBetUrl queuedBetUrl =
                        _repositoryOfQueuedBetUrl.Get(
                            EntitySpecifications.EntityIdIsEqualTo<QueuedBetUrl>(queuedBetUrlId)).Single();

                    queuedBetUrl.StartDateTime = DateTime.UtcNow;
                    contextScope.SaveChanges();

                    var screenShotMaker = _screenShotMakerFactory.Create();

                    Stream screenshotPngStream = screenShotMaker.GetScreenshotPngStream(queuedBetUrl.Url, synchronizationContext);

                    _screenshotAmazonS3Putter.PutScreenshot(amazonS3Client, queuedBetUrl, screenshotPngStream);

                    queuedBetUrl.FinishDateTime = DateTime.UtcNow;
                    contextScope.SaveChanges();
                }

                transactionScope.Complete();
            }
        }
    }
}