using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using AwesomiumSharp;
using BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker.Interfaces;
using BetTeamsBattle.Screenshots.Common;
using BetTeamsBattle.Screenshots.Common.Encoders.Interfaces;

namespace BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker
{
    internal class RenderBufferToEncodedImageStreamConverter : IRenderBufferToEncodedImageStreamConverter
    {
        private readonly IEncoder _encoder;

        public RenderBufferToEncodedImageStreamConverter(IEncoder encoder)
        {
            _encoder = encoder;
        }

        public Stream ConvertToEncodedImageStream(RenderBuffer buffer, out ImageFormat imageFormat)
        {
            var bitmap = new WriteableBitmap(buffer.Width, buffer.Height, 96, 96,
                                             PixelFormats.Bgra32, BitmapPalettes.WebPaletteTransparent);

            buffer.CopyToBitmap(bitmap);

            imageFormat = _encoder.ImageFormat;
            var encoder = _encoder.GetEncoder(bitmap);

            var imageStream = new MemoryStream();
            encoder.Save(imageStream);

            return imageStream;
        }
    }
}