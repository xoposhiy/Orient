using System;
using System.Drawing;
using System.Linq;
using Emgu.CV.Structure;

namespace Contour
{
    public class TextLine
    {
        public TextLine(Rectangle[] chars)
        {
            Chars = chars;
            MBR = Chars.Aggregate(Chars.First(), Util.Join);
        }

        public Rectangle[] Chars { get; private set; }
        public Rectangle MBR { get; private set; }

        public LineSegment2D LinearRegression()
        {
            var xAvg = Chars.Average(rect => rect.CenterBottom().X);
            var yAvg = Chars.Average(rect => rect.Bottom);
            var b = (Chars.Average(rect => rect.CenterBottom().X * rect.Bottom) - xAvg * yAvg) /
                   (Chars.Average(rect => Math.Pow(rect.CenterBottom().X, 2)) - Math.Pow(xAvg, 2));
            var a = yAvg - b*xAvg;
            return new LineSegment2D(new Point(MBR.Left, (int) (a + b*MBR.Left)),
                                     new Point(MBR.Right, (int) (a + b*MBR.Right)));
        }
    }
}