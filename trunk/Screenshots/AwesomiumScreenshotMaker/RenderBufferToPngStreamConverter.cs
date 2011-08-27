using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using AwesomiumSharp;
using BetTeamsBattle.AwesomiumScreenshotMaker.Interfaces;

namespace BetTeamsBattle.AwesomiumScreenshotMaker
{
    internal class RenderBufferToPngStreamConverter : IRenderBufferToPngStreamConverter
    {
        #region IRenderBufferToPngStreamConverter Members

        public Stream ConvertToPngStream(RenderBuffer buffer)
        {
            var bitmap = new WriteableBitmap(buffer.Width, buffer.Height, 96, 96,
                                             PixelFormats.Bgra32, BitmapPalettes.WebPaletteTransparent);

            buffer.CopyToBitmap(bitmap);

            var encoder = new PngBitmapEncoder {Interlace = PngInterlaceOption.On};
            encoder.Frames.Add(BitmapFrame.Create(bitmap));

            var pngImageStream = new MemoryStream();
            encoder.Save(pngImageStream);

            return pngImageStream;
        }

        #endregion
    }
}