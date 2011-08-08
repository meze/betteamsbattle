using System.Threading;
using AwesomiumSharp;
using BetTeamsBattle.ScreenShotsMaker.ScreenShotMaker.Interfaces;

namespace BetTeamsBattle.ScreenShotsMaker.ScreenShotMaker
{
    internal class ScreenShotRenderService : IScreenShotRenderService
    {
        private bool _finishedScrollDataReceived;
        private ScrollData _scrollData;

        #region IScreenShotRenderService Members

        public RenderBuffer GetRender(string url, SynchronizationContext synchronizationContext)
        {
            WebView webView = null;
            synchronizationContext.Send((object state) => webView = WebCore.CreateWebView(1024, 768), null);
             
            webView.LoadURL(url);

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