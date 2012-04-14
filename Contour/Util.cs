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
            return img.Convert<Gray, Byte>().SmoothMedian(3).ThresholdBinary(new Gray(200), new Gray(255));
        }

        public static Image<Gray, byte> Process(string file)
        {
            return new Image<Bgr, byte>(file).Process();
        }

        public static bool InRange(this int value, int min, int max)
        {
            return min <= value && value <= max;
        }

        public static Rectangle[] FilterPoints(this Rectangle[] boxes)
        {
            Rectangle[] points = boxes.Where(box => box.Diameter().InRange(2, 9)).ToArray();
            Rectangle[] chars = boxes.FilterChars();
            return points.Where(box => !chars.IntersectsWith(box)).ToArray();
        }

        public static Rectangle[] FilterChars(this Rectangle[] boxes)
        {
            return boxes.Where(box => box.Area().InRange(100, 1000) && box.Diameter().InRange(10, 70)).ToArray();
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

        public static TextLine[] GetLines(this Rectangle[] boxes, int separator)
        {
            var result = new List<TextLine>();
            List<Rectangle> remainingBoxes = boxes.OrderBy(box => box.X).ToList();
            while (remainingBoxes.Any())
            {
                var leftmost = remainingBoxes.First();
                var line = new List<Rectangle> {leftmost};
                remainingBoxes.Remove(leftmost);

                do
                {
                    var lineEnding = GetLineEndingMBR(line, 3);
                    var yCompatible = remainingBoxes.Where(r => IntersectsY(r, lineEnding));
                    var yxCompatible = yCompatible.Where(r => r.Left.InRange(lineEnding.Right, lineEnding.Right + separator));
                    Rectangle? next = yxCompatible.OrderBy(b => b.X).Cast<Rectangle?>().FirstOrDefault();
                    if (next == null) break;
                    line.Add(next.Value);
                    remainingBoxes.Remove(next.Value);
                } while (true);

                if (line.Count > 1)
                    result.Add(new TextLine(line.ToArray()));
            }
            return result.ToArray();
        }

        private static bool IntersectsY(Rectangle r1, Rectangle r2)
        {
            int lowestCommon = Math.Min(r1.Bottom, r2.Bottom);
            int highestCommon = Math.Max(r1.Top, r2.Top);
            return lowestCommon >= highestCommon;
        }

        private static Rectangle GetLineEndingMBR(ICollection<Rectangle> line, int count)
        {
            var ending = line.Skip(Math.Max(0, line.Count - count)).ToList();
            return ending.Aggregate(ending.First(), Util.Join);
        }
    }

    public class TextLine
    {
        public TextLine(Rectangle[] chars)
        {
            Chars = chars;
            MBR = Chars.Aggregate(Chars.First(), Util.Join);
        }

        public Rectangle[] Chars { get; private set; }
        public Rectangle MBR { get; private set; }
    }
}