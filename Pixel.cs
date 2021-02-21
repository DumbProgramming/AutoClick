using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoClick
{
    public struct Pixel
    {
        public Color Color { get; }
        public Vector2d Position { get; }

        public Pixel(Color color, int x, int y)
        {
            Color = color;
            Position = new Vector2d(x, y);
        }
    }
}
