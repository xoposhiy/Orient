using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Contour
{
    public static class Util
    {
        public static Image<Gray, byte> Process(this Image<Bgr, byte> img)
        {
            return img.Convert<Gray, Byte>().SmoothMedian(3);
        }

        public static Image<Gray, byte> Process(string file)
        {
            return new Image<Bgr, byte>(file).Process();
        }

        public static Rectangle[] FilterPoints(this IEnumerable<Rectangle> boxes)
        {
            return boxes.Where(box => (box.Area() > 10) && (box.Area() < 100)).ToArray();
        }

        public static Rectangle[] FilterChars(this IEnumerable<Rectangle> boxes)
        {
            return boxes.Where(box => (box.Area() > 100) && (box.Area() < 700)).ToArray();
        }

        public static int Area(this Rectangle rect)
        {
            return rect.Width * rect.Height;
        }
    }
}
