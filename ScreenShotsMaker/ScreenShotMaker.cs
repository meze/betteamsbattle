using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using AwesomiumSharp;
using ScreenShotsMaker.Interfaces;

namespace ScreenShotsMaker
{
    internal class ScreenShotMaker : IScreenShotMaker
    {
        private bool _finishedLoading;
        private bool _finishedScrollDataReceived;
        private ScrollData _scrollData;

        public string MakeScreenshot(string url, out int width, out int height)
        {
            var webView1 = WebCore.CreateWebView(1024, 768);
            webView1.LoadURL(url);
            webView1.LoadCompleted += OnLoadCompleted;

            while (!_finishedLoading)
            {
                Thread.Sleep(100);
                WebCore.Update();
            }

            webView1.RequestScrollData();
            webView1.ScrollDataReceived += OnScrollDataReceived;

            while (!_finishedScrollDataReceived)
            {
                Thread.Sleep(100);
                WebCore.Update();
            }

            width = _scrollData.ContentWidth;
            height = _scrollData.ContentHeight;
            var webView2 = WebCore.CreateWebView(width, height);

            _finishedLoading = false;
            webView2.LoadURL(url);
            webView2.LoadCompleted += OnLoadCompleted;

            while (!_finishedLoading)
            {
                Thread.Sleep(100);
                WebCore.Update();
            }

            var tempFileName = Path.GetTempFileName();
            webView2.Render().SaveToPNG(tempFileName);

            return tempFileName;
        }

        private void OnScrollDataReceived(object sender, ScrollDataEventArgs e)
        {
            _finishedScrollDataReceived = true;
            _scrollData = e.ScrollData;
        }

        private void OnLoadCompleted(object sender, EventArgs e)
        {
            _finishedLoading = true;
        }
    }
}