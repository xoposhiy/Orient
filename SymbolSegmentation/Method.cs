using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace SymbolSegmentation
{
    public class Method : MethodDeclaration.IMethod
    {
        public double DetectOrient(Bitmap img)
        {
            return 0;
        }

        public double DetectOrient(string file)
        {
            var image = new Image<Bgr, byte>(file).Resize(400, 400, INTER.CV_INTER_LINEAR, true);
            var gray = image.Convert<Gray, Byte>();

            var cannyThreshold = new Gray(180);
            var cannyThresholdLinking = new Gray(120);

            var cannyEdges = gray.Canny(cannyThreshold, cannyThresholdLinking);
            var contours = cannyEdges.FindContours();

            return 0;
        }
    }
}
