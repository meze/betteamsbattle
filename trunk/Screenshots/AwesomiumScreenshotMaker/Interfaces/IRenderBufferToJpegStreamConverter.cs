using System.IO;
using AwesomiumSharp;

namespace BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker.Interfaces
{
    internal interface IRenderBufferToJpegStreamConverter
    {
        Stream ConvertToJpegStream(RenderBuffer buffer);
    }
}