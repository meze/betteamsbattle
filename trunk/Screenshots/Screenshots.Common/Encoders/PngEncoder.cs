using System.Windows.Media.Imaging;
using BetTeamsBattle.Screenshots.Common.Encoders.Interfaces;

namespace BetTeamsBattle.Screenshots.Common.Encoders
{
    internal class PngEncoder : IEncoder
    {
        public BitmapEncoder GetEncoder(BitmapSource bitmap)
        {
            var encoder = new PngBitmapEncoder { Interlace = PngInterlaceOption.Off };
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            return encoder;
        }

        public ImageFormat ImageFormat
        {
            get { return ImageFormat.Png; }
        }
    }
}