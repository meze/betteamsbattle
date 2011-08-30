using System.Windows.Media.Imaging;

namespace BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker.Encoders.Interfaces
{
    internal interface IEncoder
    {
        BitmapEncoder GetEncoder(BitmapSource bitmap);
    }
}