using System.Collections.Generic;
using System.Drawing;

namespace GrayToRGB
{
    class ImageProcessor
    {
        public Bitmap image { get; set; }

        public ImageProcessor(Bitmap image)
        {
            this.image = image;
        }

        private bool inRange(Color c1, Color c2, int range)
        {
            return c1.R < c2.R + range && c1.R > c2.R - range;
        }

        private Color Mix(Color c1, Color c2)
        {
            int r = (int)(c1.R * 0.75 + c2.R * 0.25);
            int g = (int)(c1.G * 0.75 + c2.G * 0.25);
            int b = (int)(c1.B * 0.75 + c2.B * 0.25);
            return Color.FromArgb(r, g, b);
        }

        private bool IsValid(int x, int y, Color color)
        {
            if (x < 0 || x >= image.Width) return false;
            if (y < 0 || y >= image.Height) return false;
            if (!inRange(color, image.GetPixel(x, y), 50)) return false;
            return true;
        }

        public void FillIn(int startX, int startY, Color color, Color backColor)
        {
            bool[,] visited = new bool[image.Width, image.Height];
            visited.Initialize();

            Queue<Pixel> toColor = new Queue<Pixel>();
            toColor.Enqueue(new Pixel(startX, startY));

            while (toColor.Count > 0)
            {
                Pixel current = toColor.Dequeue();

                if (visited[current.x, current.y]) continue;

                visited[current.x, current.y] = true;

                Color newColor = Mix(color, image.GetPixel(current.x, current.y));

                image.SetPixel(current.x, current.y, newColor);

                if (IsValid(current.x + 1, current.y, backColor))
                {
                    toColor.Enqueue(new Pixel(current.x + 1, current.y));
                }
                if (IsValid(current.x - 1, current.y, backColor))
                {
                    toColor.Enqueue(new Pixel(current.x - 1, current.y));
                }
                if (IsValid(current.x, current.y + 1, backColor))
                {
                    toColor.Enqueue(new Pixel(current.x, current.y + 1));
                }
                if (IsValid(current.x, current.y - 1, backColor))
                {
                    toColor.Enqueue(new Pixel(current.x, current.y - 1));
                }
            }
        }

    }
}
