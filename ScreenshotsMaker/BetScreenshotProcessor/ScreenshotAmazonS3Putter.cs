using System.IO;
using Amazon.S3;
using Amazon.S3.Model;
using BetTeamsBattle.ScreenshotsMaker.BetScreenshotProcessor.Interfaces;

namespace BetTeamsBattle.ScreenshotsMaker.BetScreenshotProcessor
{
    internal class ScreenshotAmazonS3Putter : IScreenshotAmazonS3Putter
    {
        public void PutScreenshot(AmazonS3 amazonS3Client, string bucketName, string path, Stream stream)
        {
            var putObjectRequest = new PutObjectRequest();

            putObjectRequest.WithInputStream(stream);
            putObjectRequest.WithBucketName(bucketName);
            putObjectRequest.WithKey(path);

            amazonS3Client.PutObject(putObjectRequest);
        }
    }
}