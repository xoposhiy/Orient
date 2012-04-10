using System;
using System.Drawing;
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
            imageFileDialog.FileName = @"20.1.png";
            imageFileDialog.InitialDirectory = @"..\..\base\certificates\";
            ImageFileClick(this, null);
        }

        private void ImageFileClick(object sender, EventArgs e)
        {
            if (imageFileDialog.ShowDialog() != DialogResult.OK) return;
            var img = new Image<Bgr, byte>(imageFileDialog.FileName);
            originalImageBox.Image = img.Copy();

            var gray = img.Process();
            
            boxes = SymbolSegmentation.GetBoundingBoxes(gray);
            foreach (var rect in boxes.FilterChars())
                img.Draw(rect, new Bgr(Color.Green), 1);

            foreach (var rect in boxes.FilterPoints())
                img.Draw(rect, new Bgr(Color.Red), 1);

            lineImageBox.Image = img;
        }

        private Rectangle[] boxes;

        private void HistClick(object sender, EventArgs e)
        {
            new Histogram(boxes) {Visible = true};
        }
    }
}