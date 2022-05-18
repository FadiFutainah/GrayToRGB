using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrayToRGB
{
    public partial class Form1 : Form
    {
        private Bitmap buffer;
        private Bitmap picture;
        private bool draw;
        private Graphics graphics;
        private Color color;
        private List<BrushPoint> startingPoints = new List<BrushPoint>();

        public Form1()
        {
            InitializeComponent();
            graphics = pictureBox1.CreateGraphics();
            picture = new Bitmap(pictureBox1.Image);
            buffer = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(buffer, 0, 0);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (draw)
            {
                using (var context = Graphics.FromImage(buffer))
                {
                    SolidBrush brush = new SolidBrush(color);
                    graphics.FillEllipse(brush, e.X, e.Y, 10, 10);
                }
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!draw) 
                startingPoints.Add(new BrushPoint(e.X, e.Y, color, picture.GetPixel(e.X, e.Y)));
            
            draw = true;    
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            draw = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            color = colorDialog1.Color;
            button1.BackColor = colorDialog1.Color;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ImageProcessor processor = new ImageProcessor(picture);
            
            startingPoints.ForEach((point) => processor.FillIn(point.x, point.y, point.color, point.backColor));
            startingPoints.Clear();
            graphics.DrawImageUnscaled(processor.image, 0, 0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog opnfd = new OpenFileDialog();
            opnfd.Filter = "Image Files (*.jpg;*.jpeg;.*.gif;)|*.jpg;*.jpeg;.*.gif";
            if (opnfd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(opnfd.FileName);
                picture = new Bitmap(pictureBox1.Image);
                buffer = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            }

        }
    }
}
