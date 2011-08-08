using System;
using System.IO;
using Amazon.S3;
using Amazon.S3.Model;
using BetTeamsBattle.Configuration;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.ScreenShotsMaker.QueuedBetUrlProcessor.Interfaces;

namespace BetTeamsBattle.ScreenShotsMaker.QueuedBetUrlProcessor
{
    internal class ScreenshotAmazonS3Putter : IScreenshotAmazonS3Putter
    {
        public void PutScreenshot(AmazonS3 amazonS3Client, QueuedBetUrl queuedBetUrl, Stream screenshotPngStream)
        {
            var putObjectRequest = new PutObjectRequest();

            putObjectRequest.WithInputStream(screenshotPngStream);

            putObjectRequest.WithBucketName(AppSettings.AmazonBucketName);

            string path = queuedBetUrl.TypeEnum == QueuedBetUrlType.Open
                              ? AppSettings.AmazonOpenBetScreenshotsPath
                              : AppSettings.AmazonCloseBetScreenshotsPath;
            putObjectRequest.WithKey(String.Format("{0}{1}.png", path, queuedBetUrl.Id));

            amazonS3Client.PutObject(putObjectRequest);
        }
    }
}