using BetTeamsBattle.ScreenshotsMaker.BetScreenshotProcessor;
using BetTeamsBattle.ScreenshotsMaker.BetScreenshotProcessor.Interfaces;
using Ninject.Modules;

namespace BetTeamsBattle.ScreenShotsMaker.DI
{
    public class ScreenshotsMakerForFrontendNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBetScreenshotPathService>().To<BetScreenshotPathService>();
        }
    }
}