using System.Windows.Media.Imaging;
using BetTeamsBattle.Screenshots.Common.Encoders.Interfaces;

namespace BetTeamsBattle.Screenshots.Common.Encoders
{
    internal class JpegEncoder : IEncoder
    {
        public BitmapEncoder GetEncoder(BitmapSource bitmap)
        {
            var jpegEncoder = new JpegBitmapEncoder() {QualityLevel = 50};
            jpegEncoder.Frames.Add(BitmapFrame.Create(bitmap));
            return jpegEncoder;
        }

        public ImageFormat ImageFormat
        {
            get { return ImageFormat.Jpeg; }
        }
    }
}