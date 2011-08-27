using BetTeamsBattle.BettScreenshotsManager.BetScreenshotProcessor;
using BetTeamsBattle.BettScreenshotsManager.BetScreenshotProcessor.Interfaces;
using Ninject.Modules;

namespace BetTeamsBattle.BettScreenshotsManager.DI
{
    public class AmazonS3NinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBetScreenshotPathService>().To<BetScreenshotPathService>();
            Bind<IScreenshotAmazonS3Putter>().To<ScreenshotAmazonS3Putter>();
        }
    }
}