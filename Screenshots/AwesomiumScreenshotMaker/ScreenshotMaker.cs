﻿using System.IO;
using System.Threading;
using AwesomiumSharp;
using BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker.Interfaces;
using BetTeamsBattle.Screenshots.Common;

namespace BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker
{
    internal class ScreenshotMaker : IScreenshotMaker
    {
        private readonly IRenderBufferToEncodedImageStreamConverter _renderBufferToEncodedImageStreamConverter;
        private readonly IScreenshotRenderService _screenshotRenderService;

        public ScreenshotMaker(IScreenshotRenderService screenshotRenderService,
                               IRenderBufferToEncodedImageStreamConverter renderBufferToEncodedImageStreamConverter)
        {
            _screenshotRenderService = screenshotRenderService;
            _renderBufferToEncodedImageStreamConverter = renderBufferToEncodedImageStreamConverter;
        }

        public Stream GetScreenshotEncodedStream(string url, SynchronizationContext synchronizationContext, out ImageFormat imageFormat)
        {
            RenderBuffer renderBuffer = _screenshotRenderService.GetRender(url, synchronizationContext);

            return _renderBufferToEncodedImageStreamConverter.ConvertToEncodedImageStream(renderBuffer, out imageFormat);
        }
    }
}