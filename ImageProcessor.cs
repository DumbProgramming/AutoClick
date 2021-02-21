using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoClick
{
    public class ImageProcessor
    {
        private readonly List<Pixel> _pixels;

        public IEnumerable<Pixel> Pixels { get => _pixels; }
        public CaptureSettings CaptureSettings { get; set; }

        public ImageProcessor(CaptureSettings captureSettings)
        {
            _pixels = new List<Pixel>();
            CaptureSettings = captureSettings;
        }

        public ImageProcessor Capture()
        {
            using (var bitmap = new Bitmap(CaptureSettings.XDimension, CaptureSettings.YDimension))
            {
                using (var g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(
                        CaptureSettings.SourcePosition.X,
                        CaptureSettings.SourcePosition.Y,
                        CaptureSettings.DestinationPosition.X,
                        CaptureSettings.DestinationPosition.Y,
                        bitmap.Size,
                        CopyPixelOperation.SourceCopy
                    );
                }

                for (int x = 0; x < CaptureSettings.XDimension; x++)
                {
                    for (int y = 0; y < CaptureSettings.YDimension; y++)
                    {
                        _pixels.Add(new Pixel(bitmap.GetPixel(x, y), x, y));
                    }
                }
            }
            return this;
        }

        public void DebugPrint(string filename, ImageFormat imageFormat, string foreColorHex, string backColorHex)
        {
            var foreColor = ColorTranslator.FromHtml(foreColorHex);
            var backColor = ColorTranslator.FromHtml(backColorHex);

            DebugPrint(filename, imageFormat, foreColor, backColor);
        }

        public void DebugPrint(string filename, ImageFormat imageFormat, Color foreColor, Color backColor)
        {
            using (var bitmap = new Bitmap(CaptureSettings.XDimension, CaptureSettings.YDimension))
            {
                foreach (var pixel in Pixels)
                {
                    bitmap.SetPixel(pixel.Position.X, pixel.Position.Y, pixel.Color);
                }

                foreach (var pixel in GetExceptSpecificColoredPixels(foreColor))
                {
                    bitmap.SetPixel(pixel.Position.X, pixel.Position.Y, backColor);
                }

                bitmap.Save(filename, imageFormat);
            }
        }

        public void DebugPrint(string filename, ImageFormat imageFormat, Color foreColor, Color backColor, Vector2d startPoint, Vector2d endpoint)
        {
            using (var bitmap = new Bitmap(CaptureSettings.XDimension, CaptureSettings.YDimension))
            {
                foreach (var pixel in Pixels)
                {
                    bitmap.SetPixel(pixel.Position.X, pixel.Position.Y, pixel.Color);
                }

                foreach (var pixel in GetExceptSpecificColoredPixels(foreColor, startPoint, endpoint))
                {
                    bitmap.SetPixel(pixel.Position.X, pixel.Position.Y, backColor);
                }

                bitmap.Save(filename, imageFormat);
            }
        }

        public void DebugPrint(string filename, ImageFormat imageFormat, string foreColorHex, string backColorHex, Vector2d startPoint, Vector2d endpoint)
        {
            var foreColor = ColorTranslator.FromHtml(foreColorHex);
            var backColor = ColorTranslator.FromHtml(backColorHex);

            DebugPrint(filename, imageFormat, foreColor, backColor, startPoint, endpoint);
        }

        public IEnumerable<Pixel> GetSpecificColoredPixels(Color color)
        {
            return _pixels.Where(x => x.Color.Equals(color));
        }

        public IEnumerable<Pixel> GetSpecificColoredPixels(string hexValue)
        {
            var filterColor = ColorTranslator.FromHtml(hexValue);

            return GetSpecificColoredPixels(filterColor);
        }

        public IEnumerable<Pixel> GetSpecificColoredPixels(Color color, Vector2d startPoint, Vector2d endpoint)
        {
            return _pixels.Where(p => p.Position.X > startPoint.X &&
                    p.Position.X < endpoint.X &&
                    p.Position.Y > startPoint.Y &&
                    p.Position.Y < endpoint.Y)
                .Where(p => p.Color.Equals(color));
        }

        public IEnumerable<Pixel> GetSpecificColoredPixels(string hexValue, Vector2d startPoint, Vector2d endpoint)
        {
            var filterColor = ColorTranslator.FromHtml(hexValue);

            return GetSpecificColoredPixels(filterColor, startPoint, endpoint);
        }

        public IEnumerable<Pixel> GetExceptSpecificColoredPixels(Color color)
        {
            return _pixels.Where(x => !x.Color.Equals(color));
        }

        public IEnumerable<Pixel> GetExceptSpecificColoredPixels(string hexValue)
        {
            var filterColor = ColorTranslator.FromHtml(hexValue);

            return GetExceptSpecificColoredPixels(filterColor);
        }

        public IEnumerable<Pixel> GetExceptSpecificColoredPixels(Color color, Vector2d startPoint, Vector2d endpoint)
        {
            return _pixels.Where(p => p.Position.X > startPoint.X &&
                    p.Position.X < endpoint.X &&
                    p.Position.Y > startPoint.Y &&
                    p.Position.Y < endpoint.Y)
                .Where(p => !p.Color.Equals(color));
        }

        public IEnumerable<Pixel> GetExceptSpecificColoredPixels(string hexValue, Vector2d startPoint, Vector2d endpoint)
        {
            var filterColor = ColorTranslator.FromHtml(hexValue);

            return GetExceptSpecificColoredPixels(filterColor, startPoint, endpoint);
        }
    }
}
