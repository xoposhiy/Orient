using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Contour
{
    class SymbolSegmentation
    {
        public static Rectangle[] GetBoundingBoxes(string file)
        {
            if (!File.Exists(file)) throw new FileNotFoundException();
            var img = new Image<Bgr, byte>(file);
            
            var gray = img.Convert<Gray, Byte>().Canny(new Gray(180), new Gray(120));
            var result = new List<Rectangle>();
            for (var contour = gray.FindContours(); contour != null; contour = contour.HNext)
                result.Add(contour.BoundingRectangle);

            return result.ToArray();
        }
    }
}
