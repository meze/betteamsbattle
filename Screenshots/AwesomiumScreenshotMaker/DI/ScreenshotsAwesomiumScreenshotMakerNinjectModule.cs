using System.Windows.Media.Imaging;
using BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker.Interfaces;
using Ninject.Modules;

namespace BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker.DI
{
    public class ScreenshotsAwesomiumScreenshotMakerNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRenderBufferToEncodedImageStreamConverter>().To<RenderBufferToEncodedImageStreamConverter>();
            Bind<IScreenshotMaker>().To<ScreenshotMaker>();

            Bind<IScreenshotMakerFactory>().To<ScreenshotMakerFactory>();
            Bind<IScreenshotRenderService>().To<ScreenshotRenderService>();
        }
    }
}