using BetTeamsBattle.ScreenShotsMaker.QueuedBetUrlProcessor;
using BetTeamsBattle.ScreenShotsMaker.QueuedBetUrlProcessor.Interfaces;
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
            Bind<IRenderBufferToPngStreamConverter>().To<RenderBufferToPngStreamConverter>();
            Bind<IScreenShotMaker>().To<ScreenShotMaker.ScreenShotMaker>();
            Bind<IScreenShotMakerFactory>().To<ScreenShotMakerFactory>();
            Bind<IScreenShotRenderService>().To<ScreenShotRenderService>();

            Bind<IQueuedBetUrlProcessor>().To<QueuedBetUrlProcessor.QueuedBetUrlProcessor>();
            Bind<IScreenshotAmazonS3Putter>().To<ScreenshotAmazonS3Putter>();

            Bind<IScreenShotsMakingManager>().To<ScreenShotsMakingManager>();
        }
    }
}