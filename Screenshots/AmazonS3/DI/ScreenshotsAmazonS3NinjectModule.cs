using BetTeamsBattle.Screenshots.AmazonS3.Interfaces;
using Ninject.Modules;

namespace BetTeamsBattle.Screenshots.AmazonS3.DI
{
    public class ScreenshotsAmazonS3NinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBetScreenshotPathService>().To<BetScreenshotPathService>();
            Bind<IScreenshotAmazonS3Putter>().To<ScreenshotAmazonS3Putter>();
        }
    }
}