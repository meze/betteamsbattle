using System.Windows.Media.Imaging;
using BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker.Encoders;
using BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker.Encoders.Interfaces;
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
            Bind<IEncoder>().To<PngEncoder>();
            Bind<IScreenshotMakerFactory>().To<ScreenshotMakerFactory>();
            Bind<IScreenshotRenderService>().To<ScreenshotRenderService>();
        }
    }
}