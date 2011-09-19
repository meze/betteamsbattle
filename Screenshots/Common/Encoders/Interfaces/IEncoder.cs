using System.Windows.Media.Imaging;

namespace BetTeamsBattle.Screenshots.Common.Encoders.Interfaces
{
    public interface IEncoder
    {
        BitmapEncoder GetEncoder(BitmapSource bitmap);
        ImageFormat ImageFormat { get; }
    }
}