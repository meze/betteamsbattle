using System.IO;
using System.Threading;

namespace BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker.Interfaces
{
    public interface IScreenshotMaker
    {
        Stream GetScreenshotPngStream(string url, SynchronizationContext synchronizationContext);
    }
}