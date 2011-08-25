using BetTeamsBattle.ScreenShotsMaker.ScreenShotMaker;
using BetTeamsBattle.ScreenShotsMaker.ScreenShotMaker.Interfaces;
using BetTeamsBattle.ScreenShotsMaker.ScreenShotMakingManager;
using BetTeamsBattle.ScreenShotsMaker.ScreenShotMakingManager.Interfaces;
using BetTeamsBattle.ScreenshotsMaker.BetScreenshotProcessor;
using BetTeamsBattle.ScreenshotsMaker.BetScreenshotProcessor.Interfaces;
using Ninject.Modules;

namespace BetTeamsBattle.ScreenShotsMaker.DI
{
    public class ScreenShotsMakerNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRenderBufferToPngStreamConverter>().To<RenderBufferToPngStreamConverter>();
            Bind<IScreenshotMaker>().To<ScreenShotMaker.ScreenshotMaker>();
            Bind<IScreenshotMakerFactory>().To<ScreenshotMakerFactory>();
            Bind<IScreenshotRenderService>().To<ScreenshotRenderService>();

            Bind<IBetScreenshotProcessor>().To<BetScreenshotProcessor>();
            Bind<IScreenshotAmazonS3Putter>().To<ScreenshotAmazonS3Putter>();

            Bind<IScreenshotsMakingManager>().To<ScreenshotsMakingManager>();
        }
    }
}