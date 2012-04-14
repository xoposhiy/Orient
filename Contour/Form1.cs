using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Contour
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
			imageFileDialog.InitialDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\..\..\base\certificates\";
			imageFileDialog.FileName = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\..\..\base\certificates\20.1.png";
            ImageFileClick(this, null);
        }

        private void ImageFileClick(object sender, EventArgs e)
        {
			if (imageFileDialog.ShowDialog() != DialogResult.OK) return;
            originalImg = new Image<Bgr, byte>(imageFileDialog.FileName);

			var gray = originalImg.Process();
        	markedImg = gray.Convert<Bgr, byte>();
            boxes = SymbolSegmentation.GetBoundingBoxes(gray);
            var filterChars = boxes.FilterChars();
            foreach (var rect in filterChars)
				markedImg.Draw(rect, new Bgr(Color.Green), 1);

            foreach (var rect in boxes.FilterPoints())
				markedImg.Draw(rect, new Bgr(Color.Red), 1);

			foreach (var line in filterChars.GetLines(50))
			{
				var prev = line.First();
				foreach (var next in line.Skip(1))
				{
					markedImg.Draw(new LineSegment2D(prev.CenterBottom(), next.CenterBottom()), new Bgr(Color.RoyalBlue), 2);
					markedImg.Draw(new CircleF(next.CenterBottom(), 2), new Bgr(Color.Aqua), 2);
					prev = next;
				}
			}
			imageBox.Image = markedImg;
        }

        private Rectangle[] boxes;
    	private Image<Bgr, byte> originalImg;
    	private Image<Bgr, byte> markedImg;

    	private void HistClick(object sender, EventArgs e)
        {
            new Histogram(boxes) {Visible = true};
        }

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Space)
			{
				if (originalImg != null)
					imageBox.Image = originalImg;
			}
		}

		private void Form1_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Space)
			{
				if (originalImg != null)
					imageBox.Image = markedImg;
			}
		}
    }
}