using System.IO;
using AwesomiumSharp;
using BetTeamsBattle.Screenshots.Common;

namespace BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker.Interfaces
{
    internal interface IRenderBufferToEncodedImageStreamConverter
    {
        Stream ConvertToEncodedImageStream(RenderBuffer buffer, out ImageFormat imageFormat);
    }
}