using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Contour
{
    class SymbolSegmentation
    {
        public static Rectangle[] GetBoundingBoxes(Image<Gray, byte> gray)
        {
            var result = new List<Rectangle>();
            for (var contour = gray.FindContours(); contour != null; contour = contour.HNext)
                result.Add(contour.BoundingRectangle);

            return result.ToArray();
        }
    }
}
