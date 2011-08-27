using System.IO;
using AwesomiumSharp;

namespace BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker.Interfaces
{
    internal interface IRenderBufferToPngStreamConverter
    {
        Stream ConvertToPngStream(RenderBuffer buffer);
    }
}