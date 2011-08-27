namespace BetTeamsBattle.Screenshots.AmazonS3.Interfaces
{
    public interface IBetScreenshotPathService
    {
        string GetPath(long betScreenshotId);
        string GetUrl(long betScreenshotId);
    }
}