using System;
using System.Collections.Generic;
using System.Text;

namespace Autoclick.Lib
{
    public class CaptureSettings
    {
        public Vector2D SourcePosition { get; set; } = new Vector2D(0, 0);
        public Vector2D DestinationPosition { get; set; } = new Vector2D(0, 0);
        public int XDimension { get; set; }
        public int YDimension { get; set; }

        public CaptureSettings(int xDimension, int yDimension)
        {
            XDimension = xDimension;
            YDimension = yDimension;
        }
    }
}
