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

        public static Rectangle[] FilterPoints(this Rectangle[] boxes)
        {
            var points = boxes.Where(box => (box.Area() > 10) && (box.Area() <= 100)).ToArray();
            var chars = boxes.FilterChars();
            return points.Where(box => !chars.IntersectsWith(box)).ToArray();
        }

        public static Rectangle[] FilterChars(this Rectangle[] boxes)
        {
            return boxes.Where(box => (box.Area() > 100) && (box.Area() < 700)).ToArray();
        }

        public static int Area(this Rectangle rect)
        {
            return rect.Width * rect.Height;
        }

        public static bool IntersectsWith(this IEnumerable<Rectangle> boxes, Rectangle rect)
        {
            return boxes.Any(box => box.IntersectsWith(rect));
        }

        public static Rectangle[][] GetLines(this Rectangle[] boxes, int separator)
        {
            try
            {
                var result = new List<Rectangle[]>();
                var rectangles = boxes.ToList();
                while (rectangles.Count != 0)
                {
                    var leftBoxes = rectangles.Where(box => box.X == rectangles.Min(rect => rect.X));
                    if (!leftBoxes.Any()) break;
                    var start = leftBoxes.First();//. && box.Y == rectangles.Min(rect => rect.Y));
                    var line = new List<Rectangle> {start};
                    rectangles.Remove(start);

                    foreach (
                        var rectangle in
                            rectangles.Where(
                                rect => line.Y().Intersect(rect) && (rect.X - line.Max(box => box.Right)) <= separator).
                                OrderBy(rect => rect.X))
                    {
                        rectangles.Remove(rectangle);
                        line.Add(rectangle);
                    }
                    result.Add(line.ToArray());
                }
                return result.ToArray();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static KeyValuePair<int, int> Y(this List<Rectangle> line)
        {
            var lastElems = line.Count > 3 ? line.OrderByDescending(rect => rect.X).Take(3) : line;
            return new KeyValuePair<int, int>(lastElems.Min(rect => rect.Y), lastElems.Max(rect => rect.Y));
        }

        public static bool Intersect(this KeyValuePair<int, int> source, Rectangle sect)
        {
            if (sect.Y >= source.Key && sect.Y <= source.Value && sect.Middle() <= source.Value)
                return true;
            if (sect.Bottom >= source.Key && sect.Bottom <= source.Value && sect.Middle() <= source.Value)
                return true;
            return sect.Y >= source.Key && sect.Bottom <= source.Value;
        }

        public static double Middle(this Rectangle sect)
        {
            return ((double) (sect.Y + sect.Bottom))/2;
        }
    }
}
