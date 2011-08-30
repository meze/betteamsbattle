using System.IO;
using System.Threading;
using BetTeamsBattle.Screenshots.Common;

namespace BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker.Interfaces
{
    public interface IScreenshotMaker
    {
        Stream GetScreenshotEncodedStream(string url, SynchronizationContext synchronizationContext, out ImageFormat imageFormat);
    }
}