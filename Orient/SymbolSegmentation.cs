﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Orient
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

        public Rectangle[] GetBoundingBoxes(Image<Gray, byte> gray) {
            return FindRectangles(gray);
//            return FindContours(gray).ToArray();
        }

        private static Rectangle[] FindRectangles(Image<Gray, byte> gray) {
            var result = new List<Rectangle>();
            for (var contour = gray.FindContours(); contour != null; contour = contour.HNext) {
                var rect = contour.BoundingRectangle;
                result.Add(new Rectangle(rect.X, rect.Y, rect.Width - 1, rect.Height - 1));
//                result.Add(rect);
            }
            return result.ToArray();
        }


        private bool IsBlack(Color color) {
//            return Color.FromName("ff000000").Equals(color) || Color.Black.Equals(color);
            return "ff000000".Equals(color.Name) || Color.Black.Equals(color);
        }

        private IEnumerable<Rectangle> FindContours(Image<Gray, byte> gray) {
            var data = new bool[gray.Height][];
            var map =  (Bitmap) gray.Bitmap.Clone();
            for (var i = 0; i < gray.Height; ++i)
            {
                data[i] = new bool[gray.Width];
                for (var j = 0; j < gray.Width; ++j) {
//                    data[i][j] = gray[i, j].IsBlack();
                    data[i][j] = IsBlack(map.GetPixel(j, i));
                }
            }
//            var count = data.SelectMany(black => black).Count(black => black);
            var component = new List<List<Point>>();
            //Find start point
            var used = new HashSet<Point>();
            var queue = new Queue<Point>();
            var start = GetStart(data, used);
//            if (start != null) queue.Enqueue((Point) start);
            while (start != null) {
                var comp = new List<Point>();
                component.Add(comp);
                queue.Enqueue((Point) start);
                while (queue.Count != 0) {
                    var points = GetPoints(queue.Dequeue(), data, used);
                    foreach (var to in points) {
                        if (used.Contains(to)) continue;
                        used.Add(to);
                        queue.Enqueue(to);
                        comp.Add(to);
                    }
                }
                start = GetStart(data, used);
            }
            return
                component.Select(
                    p =>
                    new Rectangle(p.Min(poi => poi.X), p.Min(poi => poi.Y), p.Max(poi => poi.X) - p.Min(poi => poi.X),
                                  p.Max(poi => poi.Y) - p.Min(poi => poi.Y)));
        }

        /*private static Point? GetStart(Image<Gray, byte> gray, HashSet<Point> used) {
            var start = new Point(0, 0);
            while (!gray[start].IsBlack() || used.Contains(start)) {
                if (start.X == gray.Width - 1) {
                    if (start.Y == gray.Height - 1)
                        break;
                    start.Y += 1;
                    start.X = 0;
                }
                else
                    start.X += 1;
            }
            if (gray[start].IsBlack() && !used.Contains(start))
                return start;
            return null;
        }*/
        private static Point? GetStart(bool[][] gray, HashSet<Point> used)
        {
            var start = new Point(0, 0);
            var width = gray[0].Length;
            var height = gray.Length;
            while (!gray[start.Y][start.X] || used.Contains(start))
            {
                if (start.X == width - 1)
                {
                    if (start.Y == height - 1)
                        break;
                    start.Y += 1;
                    start.X = 0;
                }
                else
                    start.X += 1;
            }
            if (gray[start.Y][start.X] && !used.Contains(start))
                return start;
            return null;
        }

        private IEnumerable<Point> GetPoints(Point point, bool[][] gray, HashSet<Point> used) {
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
            return res.Where(p => p.X >= 0 && p.X < gray[0].Length && p.Y >= 0 && p.Y < gray.Length && gray[p.Y][p.X] && !used.Contains(p));
        } 

        public Rectangle[] FindPunctuation(Rectangle[] boxes)
        {
            Rectangle[] points = boxes.Where(box => box.Diameter().InRange(minPunctuationSize, maxCharSize-1)).ToArray();
            Rectangle[] chars = FindChars(boxes);
            var groupBy = chars.Group();
            return 
                points
//                .Where(p => !chars.IntersectsWith(p))
//                .Where(p => chars.Any(ch => ch.MaxCoordDistanceTo(p) < ch.Diameter())) // significantly slow down. need optimization (for example with KD-Tree)
                .Where(p => groupBy.ContainsKey(p.Sector()) && !groupBy.IntersectsWith(p))
                .ToArray();
        }

        public Rectangle[] FindChars(Rectangle[] boxes) {
            return boxes.Where(box => box.Diameter().InRange(minCharSize, maxCharSize)).ToArray();
        }
    }
}
