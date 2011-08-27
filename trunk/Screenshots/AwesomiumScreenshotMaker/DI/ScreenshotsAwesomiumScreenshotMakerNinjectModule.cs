using BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker.Interfaces;
using Ninject.Modules;

namespace BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker.DI
{
    public class ScreenshotsAwesomiumScreenshotMakerNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRenderBufferToPngStreamConverter>().To<RenderBufferToPngStreamConverter>();
            Bind<IScreenshotMaker>().To<ScreenshotMaker>();
            Bind<IScreenshotMakerFactory>().To<ScreenshotMakerFactory>();
            Bind<IScreenshotRenderService>().To<ScreenshotRenderService>();
        }
    }
}