using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Orient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //http://www.emgu.com/wiki/index.php/Shape_(Triangle,_Rectangle,_Circle,_Line)_Detection_in_CSharp
        private void Button1Click(object sender, EventArgs e)
        {
            if (openImageDialog.ShowDialog() == DialogResult.OK)
            {
                //Load the image from file
                img = new Image<Bgr, byte>(openImageDialog.FileName).Resize(400, 400, INTER.CV_INTER_LINEAR, true);
                originalImageBox.Image = img;

                //lineImageBox.Image = HoughTransform();
                lineImageBox.Image = img.Convert<Gray, Byte>().Canny(new Gray(180), new Gray(180));
            }
        }

        private Image<Bgr, byte> img;
        public Image<Bgr, byte> HoughTransform()
        {
            
            //Convert the image to grayscale and filter out the noise
            var gray = img.Convert<Gray, Byte>();//.PyrDown().PyrUp();
            
            var cannyThreshold = new Gray(180);
            var cannyThresholdLinking = new Gray(120);

            var cannyEdges = gray.Canny(cannyThreshold, cannyThresholdLinking);
            var lines = cannyEdges.HoughLinesBinary(
                (double) distanceResolution.Value, //Distance resolution in pixel-related units
                Math.PI / 45.0, //Angle resolution measured in radians.
                (int) threshold.Value, //threshold
                (double) minWidth.Value, //min Line width
                (double) gap.Value //gap between lines
                )[0]; //Get the lines from the first channel


            #region draw lines

            Image<Bgr, Byte> lineImage = img.CopyBlank();
            foreach (LineSegment2D line in lines)
                lineImage.Draw(line, new Bgr(Color.Green), 2);
            
            #endregion

            return lineImage;
        }
    }
}
