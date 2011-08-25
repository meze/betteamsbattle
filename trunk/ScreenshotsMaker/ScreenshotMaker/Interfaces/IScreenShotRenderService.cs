using System.Threading;
using AwesomiumSharp;

namespace BetTeamsBattle.ScreenShotsMaker.ScreenShotMaker.Interfaces
{
    internal interface IScreenshotRenderService
    {
        RenderBuffer GetRender(string url, SynchronizationContext synchronizationContext);
    }
}