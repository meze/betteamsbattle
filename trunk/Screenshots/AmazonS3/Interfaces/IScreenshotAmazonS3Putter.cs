using System.IO;
using Amazon.S3;

namespace BetTeamsBattle.BettScreenshotsManager.BetScreenshotProcessor.Interfaces
{
    public interface IScreenshotAmazonS3Putter
    {
        void PutScreenshot(AmazonS3 amazonS3Client, string bucketName, string path, Stream stream);
    }
}