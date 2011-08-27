using System.Threading;

namespace BetTeamsBattle.Screenshots.BettScreenshotsManager.Interfaces
{
    internal interface IBetScreenshotProcessor
    {
        void Process(long betScreenshotId, Amazon.S3.AmazonS3 amazonS3Client, SynchronizationContext synchronizationContext);
    }
}