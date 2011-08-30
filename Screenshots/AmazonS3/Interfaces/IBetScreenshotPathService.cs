using BetTeamsBattle.Screenshots.Common;

namespace BetTeamsBattle.Screenshots.AmazonS3.Interfaces
{
    public interface IBetScreenshotPathService
    {
        string GetPath(long betScreenshotId, ImageFormat imageFormat);
        string GetUrl(string fileName);
        string GetFileName(long betScreenshotId, ImageFormat imageFormat);
    }
}