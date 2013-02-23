using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Contour
{
    public class DocumentAnalyser
    {
        private readonly int maxBetweenWordsDistance;

        public DocumentAnalyser(int maxBetweenWordsDistance)
        {
            this.maxBetweenWordsDistance = maxBetweenWordsDistance;
        }

        private static bool IntersectsY(Rectangle r1, Rectangle r2)
        {
            int lowestCommon = Math.Min(r1.Bottom, r2.Bottom);
            int highestCommon = Math.Max(r1.Top, r2.Top);
            return lowestCommon >= highestCommon;
        }

        private static Rectangle GetLineEndingMBR(ICollection<Rectangle> line, int count)
        {
            List<Rectangle> ending = line.Skip(Math.Max(0, line.Count - count)).ToList();
            return ending.Aggregate(ending.First(), Util.Join);
        }

        public TextLine[] Extract(Rectangle[] boxes)
        {
            var result = new List<TextLine>();
            List<Rectangle> remainingBoxes = boxes.OrderBy(box => box.X).ToList();
            while (remainingBoxes.Any())
            {
                Rectangle leftmost = remainingBoxes.First();
                var line = new List<Rectangle> {leftmost};
                remainingBoxes.Remove(leftmost);

                do
                {
                    Rectangle lineEnding = GetLineEndingMBR(line, 3);
                    IEnumerable<Rectangle> yCompatible = remainingBoxes.Where(r => IntersectsY(r, lineEnding));
                    IEnumerable<Rectangle> yxCompatible =
                        yCompatible.Where(
                            r => r.Left.InRange(lineEnding.Right, lineEnding.Right + maxBetweenWordsDistance));
                    Rectangle? next = yxCompatible.OrderBy(b => b.X).Cast<Rectangle?>().FirstOrDefault();
                    if (next == null) break;
                    line.Add(next.Value);
                    remainingBoxes.Remove(next.Value);
                } while (true);

                if (line.Count > 2)
                    result.Add(new TextLine(line.ToArray()));
            }
            return result.ToArray();
        }
    }
}