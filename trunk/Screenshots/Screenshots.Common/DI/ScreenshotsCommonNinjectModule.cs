using BetTeamsBattle.Screenshots.Common.Encoders;
using BetTeamsBattle.Screenshots.Common.Encoders.Interfaces;
using Ninject.Modules;

namespace BetTeamsBattle.Screenshots.Common.DI
{
    public class ScreenshotsCommonNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IEncoder>().To<PngEncoder>();
        }
    }
}