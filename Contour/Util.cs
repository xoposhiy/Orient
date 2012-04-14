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
            return img.Convert<Gray, Byte>().SmoothMedian(3).ThresholdBinary(new Gray(180), new Gray(255));
        }

        public static Image<Gray, byte> Process(string file)
        {
            return new Image<Bgr, byte>(file).Process();
        }

        public static bool InRange(this int value, int min, int max)
        {
            return min <= value && value <= max;
        }

        public static int Area(this Rectangle rect)
        {
            return rect.Width*rect.Height;
        }

        public static int Diameter(this Rectangle rect)
        {
            return Math.Max(rect.Width, rect.Height);
        }

        public static bool IntersectsWith(this IEnumerable<Rectangle> boxes, Rectangle rect)
        {
            return boxes.Any(box => box.IntersectsWith(rect));
        }

        public static Rectangle Join(this Rectangle r1, Rectangle r2)
        {
            var x = Math.Min(r1.X, r2.X);
            var y = Math.Min(r1.Y, r2.Y);
            var w = Math.Max(r1.Right, r2.Right) - x;
            var h = Math.Max(r1.Bottom, r2.Bottom) - y;
            return new Rectangle(x, y, w, h);
        }

        public static Point LeftTop(this Rectangle rect)
        {
            return new Point(rect.X, rect.Y);
        }

        public static Point CenterBottom(this Rectangle rect)
        {
            return new Point((rect.Left + rect.Right)/2, rect.Bottom);
        }

        public static Point CenterTop(this Rectangle rect)
        {
            return new Point((rect.Left + rect.Right)/2, rect.Top);
        }
    }
}