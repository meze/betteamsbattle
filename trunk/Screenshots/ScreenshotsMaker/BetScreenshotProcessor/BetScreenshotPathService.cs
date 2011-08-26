using System;
using BetTeamsBattle.Configuration;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.ScreenshotsMaker.BetScreenshotProcessor.Interfaces;

namespace BetTeamsBattle.ScreenshotsMaker.BetScreenshotProcessor
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