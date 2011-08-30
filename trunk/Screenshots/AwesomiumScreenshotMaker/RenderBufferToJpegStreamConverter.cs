using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using AwesomiumSharp;
using BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker.Interfaces;

namespace BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker
{
    internal class RenderBufferToJpegStreamConverter : IRenderBufferToJpegStreamConverter
    {
        public Stream ConvertToJpegStream(RenderBuffer buffer)
        {
            var bitmap = new WriteableBitmap(buffer.Width, buffer.Height, 96, 96,
                                             PixelFormats.Bgra32, BitmapPalettes.WebPaletteTransparent);

            buffer.CopyToBitmap(bitmap);

            var jpegEncoder = new JpegBitmapEncoder() {QualityLevel = 1 };
            jpegEncoder.Frames.Add(BitmapFrame.Create(bitmap));

            var jpegImageStream = new MemoryStream();
            jpegEncoder.Save(jpegImageStream);

            return jpegImageStream;
        }
    }
}