using Autoclick.Lib;
using System;
using System.Linq;
using System.Threading;
using WindowsInput;

namespace Autoclick.Core
{
    public static class Program
    {
        private static readonly InputSimulator _input = new InputSimulator();

        static void Main(string[] args)
        {
            Console.WriteLine("Start autoclicker...");

            _input.Mouse.SetScreenSettings(65535, 1919, 1079);

            CaptureSettings captureSettings = new CaptureSettings(1920, 1080);
            ImageProcessor imageProcessor = new ImageProcessor(captureSettings);

            imageProcessor.Capture();

            var colors = imageProcessor.GetContainedColors();

            foreach (var pixel in imageProcessor.GetSpecificColoredPixels(colors.Max(x => string.Format("#{0:X2}{1:X2}{2:X2}", x.R, x.G, x.B))))
            {
                var position = pixel.Position;

                _input.Mouse.MoveMouseTo(position.X, position.Y);
                _input.Mouse.LeftButtonClick();
            }

            Console.WriteLine("End autoclicker...");
            Console.ReadKey();
        }
    }
}
