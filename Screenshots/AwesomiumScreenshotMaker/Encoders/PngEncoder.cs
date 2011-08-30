using System.Text;
using System.Windows.Media.Imaging;
using BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker.Encoders.Interfaces;

namespace BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker.Encoders
{
    internal class PngEncoder : IEncoder
    {
        public BitmapEncoder GetEncoder(BitmapSource bitmap)
        {
            var encoder = new PngBitmapEncoder { Interlace = PngInterlaceOption.On };
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            return encoder;
        }
    }
}