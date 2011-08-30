using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using AwesomiumSharp;
using BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker.Encoders.Interfaces;
using BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker.Interfaces;

namespace BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker
{
    internal class RenderBufferToEncodedImageStreamConverter : IRenderBufferToEncodedImageStreamConverter
    {
        private readonly IEncoder _encoder;

        public RenderBufferToEncodedImageStreamConverter(IEncoder encoder)
        {
            _encoder = encoder;
        }

        public Stream ConvertToEncodedImageStream(RenderBuffer buffer)
        {
            var bitmap = new WriteableBitmap(buffer.Width, buffer.Height, 96, 96,
                                             PixelFormats.Bgra32, BitmapPalettes.WebPaletteTransparent);

            buffer.CopyToBitmap(bitmap);

            var encoder = _encoder.GetEncoder(bitmap);

            var imageStream = new MemoryStream();
            encoder.Save(imageStream);

            return imageStream;
        }
    }
}