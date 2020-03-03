using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GifToPng
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.AllowDrop = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Image gifImg = Image.FromFile(textBox1.Text);
            FrameDimension dimension = new FrameDimension(gifImg.FrameDimensionsList[0]);
            int frameCount = gifImg.GetFrameCount(dimension);
            gifImg.SelectActiveFrame(dimension, 0);
            int w = gifImg.Width;
            int h = gifImg.Height;
            int iw = h * frameCount;
            Console.WriteLine(frameCount);
            int pointy = 0;
            Graphics g;
            Bitmap bmp = new Bitmap(w, iw);
            g = Graphics.FromImage(bmp);

            for(int i = 0;i < frameCount; i++)
            {
                gifImg.SelectActiveFrame(dimension, i);
                g.DrawImage(gifImg, new Point(0, pointy));
                pointy += h;
            }
            g.Dispose();
            pictureBox1.Image = bmp;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            if(sf.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(sf.FileName, ImageFormat.Png);
            }
        }
    }
}
