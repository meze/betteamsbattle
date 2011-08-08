using BetTeamsBattle.ScreenShotsMaker.ScreenShotMaker;
using BetTeamsBattle.ScreenShotsMaker.ScreenShotMaker.Interfaces;
using BetTeamsBattle.ScreenShotsMaker.ScreenShotMakingManager;
using BetTeamsBattle.ScreenShotsMaker.ScreenShotMakingManager.Interfaces;
using Ninject.Modules;

namespace BetTeamsBattle.ScreenShotsMaker.DI
{
    public class ScreenShotsMakerNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IScreenShotMaker>().To<ScreenShotMaker.ScreenShotMaker>();
            Bind<IScreenShotMakerFactory>().To<ScreenShotMakerFactory>();

            Bind<IScreenShotsMakingManager>().To<ScreenShotsMakingManager>();
        }
    }
}