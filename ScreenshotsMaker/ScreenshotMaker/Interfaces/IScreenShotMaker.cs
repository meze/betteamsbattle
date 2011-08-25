using System.IO;
using System.Threading;

namespace BetTeamsBattle.ScreenShotsMaker.ScreenShotMaker.Interfaces
{
    internal interface IScreenshotMaker
    {
        Stream GetScreenshotPngStream(string url, SynchronizationContext synchronizationContext);
    }
}