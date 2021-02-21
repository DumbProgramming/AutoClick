using SimWinInput;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoClick
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start autoclicker...");
            CaptureSettings captureSettings = new CaptureSettings(1920, 1080);
            ImageProcessor imageProcessor = new ImageProcessor(captureSettings);

            imageProcessor.Capture();
            Console.WriteLine($"Total pixels: {imageProcessor.Pixels.Count()}");

            //imageProcessor.DebugPrint("./temp_image.png", ImageFormat.Png, "#000000", "#ffffff");
            //imageProcessor.DebugPrint("./temp_image.png", ImageFormat.Png, "#000000", "#ffffff", new Vector2d(150, 100), new Vector2d(1800, 800));

            var searchedColor = ColorTranslator.FromHtml("#000000");
            Console.WriteLine($"Total {searchedColor} pixels: {imageProcessor.GetSpecificColoredPixels(searchedColor).Count()}");


            foreach (var pixel in imageProcessor.GetSpecificColoredPixels(searchedColor))
            {
                var position = pixel.Position;

                SimMouse.Act(SimMouse.Action.MoveOnly, position.X, position.Y);
            }

            Console.WriteLine("End autoclicker...");
            Console.ReadKey();
        }
    }
}
