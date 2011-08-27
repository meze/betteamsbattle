﻿using System.IO;
using Amazon.S3;

namespace BetTeamsBattle.ScreenshotsMaker.BetScreenshotProcessor.Interfaces
{
    internal interface IScreenshotAmazonS3Putter
    {
        void PutScreenshot(AmazonS3 amazonS3Client, string bucketName, string path, Stream stream);
    }
}