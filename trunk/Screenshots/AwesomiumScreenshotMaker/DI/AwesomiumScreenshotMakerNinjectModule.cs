using BetTeamsBattle.AwesomiumScreenshotMaker.Interfaces;
using Ninject.Modules;

namespace BetTeamsBattle.AwesomiumScreenshotMaker.DI
{
    public class AwesomiumScreenshotMakerNinjectModule : NinjectModule
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