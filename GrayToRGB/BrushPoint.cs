using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrayToRGB
{
    class BrushPoint
    {
        public int x { get; set; }
        public int y { get; set; }
        public Color color { get; set; }
        public Color backColor { get; set; }

        public BrushPoint(int x, int y, Color color, Color backColor)
        {
            this.x = x;
            this.y = y;
            this.color = color;
            this.backColor = backColor;
        }
    }
}
