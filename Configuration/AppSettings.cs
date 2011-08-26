using System;
using System.Configuration;

namespace BetTeamsBattle.Configuration
{
    public class AppSettings
    {
        public static string AmazonAccessKeyId
        {
            get
            {
                var amazonAccessKeyId = ConfigurationManager.AppSettings["amazonAccessKeyId"];
                if (string.IsNullOrEmpty(amazonAccessKeyId))
                    throw new ArgumentException("amazonAccessKeyId");
                return amazonAccessKeyId;
            }
        }

        public static string AmazonSecretAccessKeyId
        {
            get
            {
                var amazonSecretAccessKeyId = ConfigurationManager.AppSettings["amazonSecretAccessKeyId"];
                if (string.IsNullOrEmpty(amazonSecretAccessKeyId))
                    throw new ArgumentException("amazonSecretAccessKeyId");
                return amazonSecretAccessKeyId;
            }
        }

        public static string AmazonBucketName
        {
            get 
            {
                var amazonBucketName = ConfigurationManager.AppSettings["amazonBucketName"];
                if (string.IsNullOrEmpty(amazonBucketName))
                    throw new ArgumentException("amazonBucketName");
                return amazonBucketName;
            }
        }

        public static string AmazonBetsScreenshotsDirectory
        {
            get 
            { 
                var amazonBetsScreenshotsDirectory = ConfigurationManager.AppSettings["amazonBetsScreenshotsDirectory"];
                if (string.IsNullOrEmpty(amazonBetsScreenshotsDirectory))
                    throw new ArgumentException("amazonBetsScreenshotsDirectory");
                return amazonBetsScreenshotsDirectory;
            }
        }
    }
}