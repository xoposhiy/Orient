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

        public LineSegment2D LinearRegression(bool upsideDown)
        {
			if (upsideDown)
        		return Chars.LinearRegression(rect => rect.CenterBottom().X, rect => rect.Top);
			else
        		return Chars.LinearRegression(rect => rect.CenterBottom().X, rect => rect.Bottom);
        }
    }
}