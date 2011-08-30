using System.IO;
using System.Threading;
using AwesomiumSharp;
using BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker.Interfaces;

namespace BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker
{
    internal class ScreenshotMaker : IScreenshotMaker
    {
        private readonly IRenderBufferToJpegStreamConverter _renderBufferToJpegStreamConverter;
        private readonly IScreenshotRenderService _screenshotRenderService;

        public ScreenshotMaker(IScreenshotRenderService screenshotRenderService,
                               IRenderBufferToJpegStreamConverter renderBufferToJpegStreamConverter)
        {
            _screenshotRenderService = screenshotRenderService;
            _renderBufferToJpegStreamConverter = renderBufferToJpegStreamConverter;
        }

        public Stream GetScreenshotJpegStream(string url, SynchronizationContext synchronizationContext)
        {
            RenderBuffer renderBuffer = _screenshotRenderService.GetRender(url, synchronizationContext);

            return _renderBufferToJpegStreamConverter.ConvertToJpegStream(renderBuffer);
        }
    }
}