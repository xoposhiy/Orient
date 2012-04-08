using System;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Contour
{
    public class Util
    {
        public static Image<Gray, byte> Procecc(Image<Bgr, byte> img)
        {
            return img.Convert<Gray, Byte>().SmoothMedian(3);
        }

        public static Image<Gray, byte> Procecc(string file)
        {
            return Procecc(new Image<Bgr, byte>(file));
        }
    }
}
