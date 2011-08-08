using System.IO;
using System.Threading;
using AwesomiumSharp;
using BetTeamsBattle.ScreenShotsMaker.ScreenShotMaker.Interfaces;

namespace BetTeamsBattle.ScreenShotsMaker.ScreenShotMaker
{
    internal class ScreenShotMaker : IScreenShotMaker
    {
        private readonly IRenderBufferToPngStreamConverter _renderBufferToPngStreamConverter;
        private readonly IScreenShotRenderService _screenShotRenderService;

        public ScreenShotMaker(IScreenShotRenderService screenShotRenderService,
                               IRenderBufferToPngStreamConverter renderBufferToPngStreamConverter)
        {
            _screenShotRenderService = screenShotRenderService;
            _renderBufferToPngStreamConverter = renderBufferToPngStreamConverter;
        }

        #region IScreenShotMaker Members

        public Stream GetScreenshotPngStream(string url, SynchronizationContext synchronizationContext)
        {
            RenderBuffer renderBuffer = _screenShotRenderService.GetRender(url, synchronizationContext);

            return _renderBufferToPngStreamConverter.ConvertToPngStream(renderBuffer);
        }

        #endregion
    }
}