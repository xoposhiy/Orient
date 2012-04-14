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
            {
            	var rect = contour.BoundingRectangle;
            	result.Add(new Rectangle(rect.X, rect.Y, rect.Width-1, rect.Height-1));
            }

        	return result.ToArray();
        }
    }
}
