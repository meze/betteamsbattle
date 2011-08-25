using System.IO;
using Amazon.S3;
using BetTeamsBattle.Data.Model.Entities;

namespace BetTeamsBattle.ScreenShotsMaker.QueuedBetUrlProcessor.Interfaces
{
    internal interface IScreenshotAmazonS3Putter
    {
        void PutScreenshot(AmazonS3 amazonS3Client, string bucketName, string path, Stream stream);
    }
}