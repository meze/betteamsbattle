using BetTeamsBattle.BettScreenshotsManager.BetScreenshotProcessor;
using BetTeamsBattle.BettScreenshotsManager.BetScreenshotProcessor.Interfaces;
using BetTeamsBattle.BettScreenshotsManager.ScreenshotMakingManager;
using BetTeamsBattle.BettScreenshotsManager.ScreenshotMakingManager.Interfaces;
using Ninject.Modules;

namespace BetTeamsBattle.BettScreenshotsManager.DI
{
    public class ScreenshotsMakerNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBetScreenshotProcessor>().To<BetScreenshotProcessor.BetScreenshotProcessor>();
            
            Bind<IScreenshotsMakingManager>().To<ScreenshotsMakingManager>();
        }
    }
}