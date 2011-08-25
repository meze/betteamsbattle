using System.IO;
using AwesomiumSharp;

namespace BetTeamsBattle.ScreenShotsMaker.ScreenShotMaker.Interfaces
{
    internal interface IRenderBufferToPngStreamConverter
    {
        Stream ConvertToPngStream(RenderBuffer buffer);
    }
}