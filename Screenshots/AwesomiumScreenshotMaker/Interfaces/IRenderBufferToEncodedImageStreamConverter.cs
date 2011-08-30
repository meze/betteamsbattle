using System.IO;
using AwesomiumSharp;

namespace BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker.Interfaces
{
    internal interface IRenderBufferToEncodedImageStreamConverter
    {
        Stream ConvertToEncodedImageStream(RenderBuffer buffer);
    }
}