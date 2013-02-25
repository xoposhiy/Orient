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
        }

        private Contour<Point> FindContours(Image<Gray, byte> gray) {
            var storage = new MemStorage();
//            storage.
            //Find start point
            var used = new HashSet<Point>();
            var queue = new Queue<Point>();
            var start = GetStart(gray, used);
            if (start != null) queue.Enqueue((Point) start);
            while (queue.Count != 0) {
                var v = queue.Dequeue();
                foreach (var to in GetPoints(v, gray)) {
                    if (!used.Contains(to)) {
                        used.Add(to);
                        queue.Enqueue(to);

                    }
                }
            }
            return new Contour<Point>(storage);
        }

        private static Point? GetStart(Image<Gray, byte> gray, HashSet<Point> used) {
            var start = new Point(0, 0);
            while (!gray[start].Equals(new Gray(255)) || used.Contains(start)) {
                if (start.X == gray.Width) {
                    if (start.Y == gray.Height)
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

        private List<Point> GetPoints(Point point, Image<Gray, byte> gray) {
            var res = new List<Point> {
                new Point(point.X + 1, point.Y - 1),
                new Point(point.X + 1, point.Y),
                new Point(point.X + 1, point.Y + 1),
                new Point(point.X, point.Y + 1),
                new Point(point.X - 1, point.Y + 1),
                new Point(point.X - 1, point.Y),
                new Point(point.X - 1, point.Y - 1),
                new Point(point.X, point.Y - 1),
            };
            return new List<Point>(res.Where(p => gray[p].Equals(new Gray(255))));
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
