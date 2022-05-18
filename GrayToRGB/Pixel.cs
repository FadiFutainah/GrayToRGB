using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrayToRGB
{
    class Pixel
    {
        public int x { get; set; }
        public int y { get; set; }

        public Pixel(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

}
