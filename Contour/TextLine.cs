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

        public double Mean()
        {
            return Chars.Sum(rect => rect.Bottom*rect.CenterBottom().YProbability(rect.Top, rect.Bottom))/Chars.Count();
        }

        public LineSegment2D LinearRegression()
        {
            var meanValue = (int)Mean();
            return new LineSegment2D(new Point(MBR.Left, meanValue), new Point(MBR.Right, meanValue));
        }
    }
}