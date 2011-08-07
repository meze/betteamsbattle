namespace ScreenShotsMaker.Interfaces
{
    internal interface IScreenShotMaker
    {
        string MakeScreenshot(string url, out int width, out int height);
    }
}