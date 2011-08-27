using BetTeamsBattle.Screenshots.BettScreenshotsManager.Interfaces;
using Ninject.Modules;

namespace BetTeamsBattle.Screenshots.BettScreenshotsManager.DI
{
    public class ScreenshotsBetScreenshotsManagerNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBetScreenshotProcessor>().To<BetScreenshotProcessor>();
            
            Bind<IScreenshotsMakingManager>().To<ScreenshotsMakingManager>();
        }
    }
}