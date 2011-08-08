using System.IO;
using System.Threading;

namespace BetTeamsBattle.ScreenShotsMaker.ScreenShotMaker.Interfaces
{
    internal interface IScreenShotMaker
    {
        Stream GetScreenshotPngStream(string url, SynchronizationContext synchronizationContext);
    }
}