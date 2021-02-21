using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoClick
{
    public class CaptureSettings
    {
        public Vector2d SourcePosition { get; set; } = new Vector2d(0, 0);
        public Vector2d DestinationPosition { get; set; } = new Vector2d(0, 0);
        public int XDimension { get; set; }
        public int YDimension { get; set; }

        public CaptureSettings(int xDimension, int yDimension)
        {
            XDimension = xDimension;
            YDimension = yDimension;
        }
    }
}
