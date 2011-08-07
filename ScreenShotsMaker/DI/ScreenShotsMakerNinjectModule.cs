using BetTeamsBattle.Data.Model;
using Ninject.Modules;
using ScreenShotsMaker.Interfaces;

namespace ScreenShotsMaker.DI
{
    public class ScreenShotsMakerNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IScreenShotMaker>().To<ScreenShotMaker>();
            Bind<IScreenShotMakerFactory>().To<ScreenShotMakerFactory>();

            Bind<IScreenShotsMakingManager>().To<ScreenShotsMakingManager>();
        }
    }
}