using System.IO;
using Amazon.S3.Model;
using BetTeamsBattle.Screenshots.AmazonS3.Interfaces;

namespace BetTeamsBattle.Screenshots.AmazonS3
{
    internal class ScreenshotAmazonS3Putter : IScreenshotAmazonS3Putter
    {
        public void PutScreenshot(Amazon.S3.AmazonS3 amazonS3Client, string bucketName, string path, Stream stream)
        {
            var putObjectRequest = new PutObjectRequest();

            putObjectRequest.WithInputStream(stream);
            putObjectRequest.WithBucketName(bucketName);
            putObjectRequest.WithKey(path);

            amazonS3Client.PutObject(putObjectRequest);
        }
    }
}