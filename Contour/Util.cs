using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Contour
{
    public class Binarizaton
    {
        public Binarizaton(int threshold, bool smoothMedian)
        {
            this.threshold = threshold;
            this.smoothMedian = smoothMedian;
        }

        private readonly int threshold;
        private readonly bool smoothMedian;

        public Image<Gray, byte> Process(Image<Bgr, byte> img)
        {
            var processed = img.Convert<Gray, Byte>();
            if (smoothMedian) processed = processed.SmoothMedian(3);
            return processed.ThresholdBinary(new Gray(threshold), new Gray(255));
        }
    }

    public static class Util
    {
        public static bool InRange(this int value, int min, int max)
        {
            return min <= value && value <= max;
        }

        public static Point DistanceTo(this Rectangle r1, Rectangle r2)
        {
            var c1 = r1.Center();
            var c2 = r2.Center();
            return new Point(Math.Abs(c1.X - c2.X), Math.Abs(c1.Y - c2.Y));
        }

        public static int MaxCoordDistanceTo(this Rectangle r1, Rectangle r2)
        {
            var c1 = r1.Center();
            var c2 = r2.Center();
            return Math.Max(Math.Abs(c1.X - c2.X), Math.Abs(c1.Y - c2.Y));
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
            return new Point((rect.Left + rect.Right) / 2, rect.Bottom);
        }
        
        public static Point Center(this Rectangle rect)
        {
            return new Point((rect.Left + rect.Right) / 2, (rect.Top + rect.Bottom) / 2);
        }

        public static Point CenterTop(this Rectangle rect)
        {
            return new Point((rect.Left + rect.Right)/2, rect.Top);
        }
    }
}