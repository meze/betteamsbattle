using System.Threading;
using Amazon.S3;

namespace BetTeamsBattle.ScreenShotsMaker.QueuedBetUrlProcessor.Interfaces
{
    internal interface IBetScreenshotProcessor
    {
        void Process(long betScreenshotId, AmazonS3 amazonS3Client, SynchronizationContext synchronizationContext);
    }
}