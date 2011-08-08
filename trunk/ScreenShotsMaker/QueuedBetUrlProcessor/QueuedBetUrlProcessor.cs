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
using BetTeamsBattle.Data.Repositories.ContextScope;
using BetTeamsBattle.Data.Repositories.Specifications;
using BetTeamsBattle.ScreenShotsMaker.QueuedBetUrlProcessor.Interfaces;
using BetTeamsBattle.ScreenShotsMaker.ScreenShotMaker.Interfaces;
using NLog;

namespace BetTeamsBattle.ScreenShotsMaker.QueuedBetUrlProcessor
{
    internal class QueuedBetUrlProcessor : IQueuedBetUrlProcessor
    {
        private readonly IRepository<QueuedBetUrl> _repositoryOfQueuedBetUrl;
        private readonly IScreenShotMakerFactory _screenShotMakerFactory;
        private readonly IScreenshotAmazonS3Putter _screenshotAmazonS3Putter;

        public QueuedBetUrlProcessor(IRepository<QueuedBetUrl> repositoryOfQueuedBetUrl, IScreenShotMakerFactory screenShotMakerFactory, IScreenshotAmazonS3Putter screenshotAmazonS3Putter)
        {
            _repositoryOfQueuedBetUrl = repositoryOfQueuedBetUrl;
            _screenShotMakerFactory = screenShotMakerFactory;
            _screenshotAmazonS3Putter = screenshotAmazonS3Putter;
        }

        public void Process(long queuedBetUrlId, AmazonS3 amazonS3Client, SynchronizationContext synchronizationContext)
        {
            using (var transactionScope = new TransactionScope())
            {
                using (var contextScope = new ContextScope())
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

                    transactionScope.Complete();
                }
            }

        }
    }
}