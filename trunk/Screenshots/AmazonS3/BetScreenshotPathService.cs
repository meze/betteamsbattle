using System;
using BetTeamsBattle.Configuration;
using BetTeamsBattle.Screenshots.AmazonS3.Interfaces;

namespace BetTeamsBattle.Screenshots.AmazonS3
{
    internal class BetScreenshotPathService : IBetScreenshotPathService
    {
        public string GetPath(long betScreenshotId)
        {
            return String.Format("{0}/{1}.png", AppSettings.AmazonBetsScreenshotsDirectory, betScreenshotId);
        }

        public string GetUrl(long betScreenshotId)
        {
            return String.Format("https://{0}.s3.amazonaws.com/{1}", AppSettings.AmazonBucketName, GetPath(betScreenshotId));
        }
    }
}