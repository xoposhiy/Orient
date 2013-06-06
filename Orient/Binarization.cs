using System;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Orient
{
    public class Binarizaton
    {
        public Binarizaton(int threshold, bool smoothMedian)
        {
            this.threshold = threshold;
            SmoothMedian = smoothMedian;
        }

        private int threshold;
        public bool SmoothMedian { get; set; }

        public Image<Gray, byte> Process(Image<Bgr, byte> img)
        {
            var processed = img.Convert<Gray, Byte>();
            if (SmoothMedian) processed = processed.SmoothMedian(3);
//            var binary = processed.ThresholdBinary(new Gray(threshold), new Gray(255));
//            var binary = processed.ThresholdAdaptive(new Gray(255), ADAPTIVE_THRESHOLD_TYPE.CV_ADAPTIVE_THRESH_MEAN_C, THRESH.CV_THRESH_OTSU, 3, new Gray(0));
            var binary = new Image<Gray, Byte>(processed.Size);
            threshold = (int)CvInvoke.cvThreshold(processed, binary, threshold, 255, THRESH.CV_THRESH_OTSU);
//            if (thresh != threshold)
//            {
//                threshold = thresh;
//                return Process(img);
//            }
//            binary.Erode(1).Dilate(1);
//            binary.Dilate(1).Erode(1);
            return binary;
        }
    }
}
