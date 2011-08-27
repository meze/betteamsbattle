using System.IO;
using System.Threading;
using AwesomiumSharp;
using BetTeamsBattle.AwesomiumScreenshotMaker.Interfaces;

namespace BetTeamsBattle.AwesomiumScreenshotMaker
{
    internal class ScreenshotMaker : IScreenshotMaker
    {
        private readonly IRenderBufferToPngStreamConverter _renderBufferToPngStreamConverter;
        private readonly IScreenshotRenderService _screenshotRenderService;

        public ScreenshotMaker(IScreenshotRenderService screenshotRenderService,
                               IRenderBufferToPngStreamConverter renderBufferToPngStreamConverter)
        {
            _screenshotRenderService = screenshotRenderService;
            _renderBufferToPngStreamConverter = renderBufferToPngStreamConverter;
        }

        #region IScreenshotMaker Members

        public Stream GetScreenshotPngStream(string url, SynchronizationContext synchronizationContext)
        {
            RenderBuffer renderBuffer = _screenshotRenderService.GetRender(url, synchronizationContext);

            return _renderBufferToPngStreamConverter.ConvertToPngStream(renderBuffer);
        }

        #endregion
    }
}