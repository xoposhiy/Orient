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
            var img = new Image<Bgr, byte>(@"C:\Emgu\emgucv-windows-x86 2.3.0.1416\Solution\VS2010\Orient\bin\Debug\IMG052.jpg");
            img = img.Resize(400, 400, INTER.CV_INTER_LINEAR, true);
            originalImageBox.Image = img.Copy();

            //lineImageBox.Image = HoughTransform();
            var gray = img.Convert<Gray, Byte>().Canny(new Gray(180), new Gray(120));
            var contour = gray.FindContours();
            while ( contour != null)
            {
                img.Draw(contour.BoundingRectangle, new Bgr(Color.Green), 1);
                contour = contour.HNext;
            }

            lineImageBox.Image = img;
        }
    }
}