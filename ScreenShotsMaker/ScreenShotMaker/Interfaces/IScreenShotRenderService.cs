using System.Threading;
using AwesomiumSharp;

namespace BetTeamsBattle.ScreenShotsMaker.ScreenShotMaker.Interfaces
{
    internal interface IScreenShotRenderService
    {
        RenderBuffer GetRender(string url, SynchronizationContext synchronizationContext);
    }
}