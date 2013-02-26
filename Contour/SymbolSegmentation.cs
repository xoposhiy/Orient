using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Contour
{
    public class SymbolSegmentation
    {
        private readonly int maxCharSize;
        private readonly int minPunctuationSize;
        private readonly int minCharSize;

        public SymbolSegmentation(int maxCharSize, int minPunctuationSize, int minCharSize)
        {
            this.maxCharSize = maxCharSize;
            this.minPunctuationSize = minPunctuationSize;
            this.minCharSize = minCharSize;
        }

        public Rectangle[] GetBoundingBoxes(Image<Gray, byte> gray)
        {
            var result = new List<Rectangle>();
            for (var contour = gray.FindContours(); contour != null; contour = contour.HNext)
            {
                var rect = contour.BoundingRectangle;
//                result.Add(new Rectangle(rect.X, rect.Y, rect.Width - 1, rect.Height - 1));
                result.Add(rect);
            }
            return result.ToArray();
//            return FindContours(gray).ToArray();
        }

        private IEnumerable<Rectangle> FindContours(Image<Gray, byte> gray) {
            //            var storage = new MemStorage();
            //            storage.
            var component = new List<List<Point>>();
            //Find start point
            var used = new HashSet<Point>();
            var queue = new Queue<Point>();
            var start = GetStart(gray, used);
//            if (start != null) queue.Enqueue((Point) start);
            while (start != null) {
                var comp = new List<Point>();
                component.Add(comp);
                queue.Enqueue((Point) start);
                while (queue.Count != 0) {
                    foreach (var to in GetPoints(queue.Dequeue(), gray)) {
                        if (used.Contains(to)) continue;
                        used.Add(to);
                        queue.Enqueue(to);
                        comp.Add(to);
                    }
                }
                start = GetStart(gray, used);
            }
            return
                component.Select(
                    p =>
                    new Rectangle(p.Min(poi => poi.X), p.Min(poi => poi.Y), p.Max(poi => poi.X) - p.Min(poi => poi.X),
                                  p.Max(poi => poi.Y) - p.Min(poi => poi.Y)));
        }

        private static Point? GetStart(Image<Gray, byte> gray, HashSet<Point> used) {
            var start = new Point(0, 0);
            while (!gray[start].Equals(new Gray(255)) || used.Contains(start)) {
                if (start.X == gray.Width - 1) {
                    if (start.Y == gray.Height - 1)
                        break;
                    start.Y += 1;
                    start.X = 0;
                }
                else
                    start.X += 1;
            }
            if (gray[start].Equals(new Gray(255)) && !used.Contains(start))
                return start;
            return null;
        }

        private IEnumerable<Point> GetPoints(Point point, Image<Gray, byte> gray) {
            var res = new List<Point> {
                point,
                new Point(point.X + 1, point.Y - 1),
                new Point(point.X + 1, point.Y),
                new Point(point.X + 1, point.Y + 1),
                new Point(point.X, point.Y + 1),
                new Point(point.X - 1, point.Y + 1),
                new Point(point.X - 1, point.Y),
                new Point(point.X - 1, point.Y - 1),
                new Point(point.X, point.Y - 1),
            };
            return res.Where(p => p.X >= 0 && p.X < gray.Width && p.Y >= 0 && p.Y < gray.Height && gray[p].Equals(new Gray(255)));
        } 

        public Rectangle[] FindPunctuation(Rectangle[] boxes)
        {
            Rectangle[] points = boxes.Where(box => box.Diameter().InRange(minPunctuationSize, maxCharSize-1)).ToArray();
            Rectangle[] chars = FindChars(boxes);
            return 
                points
                .Where(p => !chars.IntersectsWith(p))
                //.Where(p => chars.Any(ch => ch.MaxCoordDistanceTo(p) < ch.Diameter())) // significantly slow down. need optimization (for example with KD-Tree)
                .ToArray();
        }

        public Rectangle[] FindChars(Rectangle[] boxes)
        {
            return boxes.Where(box => box.Diameter().InRange(minCharSize, maxCharSize)).ToArray();
        }
    }
}
