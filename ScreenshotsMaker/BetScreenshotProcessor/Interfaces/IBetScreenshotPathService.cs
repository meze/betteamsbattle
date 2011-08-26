namespace BetTeamsBattle.ScreenshotsMaker.BetScreenshotProcessor.Interfaces
{
    public interface IBetScreenshotPathService
    {
        string GetPath(long betScreenshotId);
        string GetUrl(long betScreenshotId);
    }
}