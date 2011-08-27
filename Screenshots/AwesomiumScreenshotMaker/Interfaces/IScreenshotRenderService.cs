using System.Threading;
using AwesomiumSharp;

namespace BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker.Interfaces
{
    internal interface IScreenshotRenderService
    {
        RenderBuffer GetRender(string url, SynchronizationContext synchronizationContext);
    }
}