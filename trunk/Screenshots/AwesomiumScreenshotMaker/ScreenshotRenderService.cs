using System.Threading;
using AwesomiumSharp;
using BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker.Interfaces;

namespace BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker
{
    internal class ScreenshotRenderService : IScreenshotRenderService
    {
        private bool _finishedScrollDataReceived;
        private ScrollData _scrollData;

        #region IScreenshotRenderService Members

        public RenderBuffer GetRender(string url, SynchronizationContext synchronizationContext)
        {
            WebView webView = null;
            synchronizationContext.Send((object state) => webView = WebCore.CreateWebView(1024, 768), null);

            synchronizationContext.Send(((object state) => webView.LoadURL(url)), null);

            while (webView.IsLoadingPage)
                SleepAndUpdateCore(synchronizationContext);

            webView.RequestScrollData();
            webView.ScrollDataReceived += OnScrollDataReceived;

            while (!_finishedScrollDataReceived)
                SleepAndUpdateCore(synchronizationContext);

            webView.Resize(_scrollData.ContentWidth, _scrollData.ContentHeight);

            while (webView.IsResizing)
                SleepAndUpdateCore(synchronizationContext);

            return webView.Render();
        }

        #endregion

        private void OnScrollDataReceived(object sender, ScrollDataEventArgs e)
        {
            _finishedScrollDataReceived = true;
            _scrollData = e.ScrollData;
        }

        private void SleepAndUpdateCore(SynchronizationContext synchronizationContext)
        {
            Thread.Sleep(100);
            synchronizationContext.Send((object state) => WebCore.Update(), null);
        }
    }
}