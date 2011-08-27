using System.IO;

namespace BetTeamsBattle.Screenshots.AmazonS3.Interfaces
{
    public interface IScreenshotAmazonS3Putter
    {
        void PutScreenshot(Amazon.S3.AmazonS3 amazonS3Client, string bucketName, string path, Stream stream);
    }
}