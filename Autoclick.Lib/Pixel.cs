using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Autoclick.Lib
{
    public struct Pixel
    {
        public Color Color { get; }
        public Vector2D Position { get; }

        public Pixel(Color color, int x, int y)
        {
            Color = color;
            Position = new Vector2D(x, y);
        }
    }
}
