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
            return GetBoundingBoxes(new Image<Bgr, byte>(file));
        }

        public static Rectangle[] GetBoundingBoxes(Bitmap bmp)
        {
            return GetBoundingBoxes(new Image<Bgr, byte>(bmp));
        }

        public static Rectangle[] GetBoundingBoxes(Image<Bgr, byte> img)
        {
            var gray = img.Convert<Gray, Byte>();//.Canny(new Gray(180), new Gray(120));
            var result = new List<Rectangle>();
            for (var contour = gray.FindContours(); contour != null; contour = contour.HNext)
                result.Add(contour.BoundingRectangle);

            return result.ToArray();
        }
    }
}
