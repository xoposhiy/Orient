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
                result.Add(new Rectangle(rect.X, rect.Y, rect.Width - 1, rect.Height - 1));
            }
            return result.ToArray();
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
