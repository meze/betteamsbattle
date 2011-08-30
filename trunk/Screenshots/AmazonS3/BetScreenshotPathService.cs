using System;
using BetTeamsBattle.Configuration;
using BetTeamsBattle.Screenshots.AmazonS3.Interfaces;
using BetTeamsBattle.Screenshots.Common;

namespace BetTeamsBattle.Screenshots.AmazonS3
{
    internal class BetScreenshotPathService : IBetScreenshotPathService
    {
        public string GetFileName(long betScreenshotId, ImageFormat imageFormat)
        {
            return String.Format("{0}.{1}", betScreenshotId, GetExtension(imageFormat));
        }

        public string GetPath(long betScreenshotId, ImageFormat imageFormat)
        {
            return String.Format("{0}/{1}", AppSettings.AmazonBetsScreenshotsDirectory, GetFileName(betScreenshotId, imageFormat));
        }

        private string GetPath(string fileName)
        {
            return String.Format("{0}/{1}", AppSettings.AmazonBetsScreenshotsDirectory, fileName);
        }

        public string GetUrl(string fileName)
        {
            return String.Format("https://{0}.s3.amazonaws.com/{1}", AppSettings.AmazonBucketName, GetPath(fileName));
        }

        private string GetExtension(ImageFormat imageFormat)
        {
            if (imageFormat == ImageFormat.Png)
                return "png";
            else if (imageFormat == ImageFormat.Jpeg)
                return "jpg";
            else
                throw new ArgumentOutOfRangeException("imageFormat");
        }
    }
}