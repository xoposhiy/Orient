using System.Drawing;
using System.Linq;

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
    }
}