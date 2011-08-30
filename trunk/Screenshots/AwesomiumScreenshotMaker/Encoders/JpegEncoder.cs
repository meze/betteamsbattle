using System.Windows.Media.Imaging;
using BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker.Encoders.Interfaces;

namespace BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker.Encoders
{
    internal class JpegEncoder : IEncoder
    {
        public BitmapEncoder GetEncoder(BitmapSource bitmap)
        {
            var jpegEncoder = new JpegBitmapEncoder() {QualityLevel = 50};
            jpegEncoder.Frames.Add(BitmapFrame.Create(bitmap));
            return jpegEncoder;
        }
    }
}