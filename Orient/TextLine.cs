using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Emgu.CV.Structure;

namespace Orient
{
    [Serializable]
    public class TextLine
    {
        public TextLine(Rectangle[] chars)
        {
            Chars = chars;
            MBR = Chars.Aggregate(Chars.First(), Util.Join);
        }

        public Rectangle[] Chars { get; private set; }
        public Rectangle MBR { get; private set; }

        public LineSegment2D LinearRegression(bool upsideDown = false) {
            Func<Rectangle, double> func = rect => rect.Bottom;
            if (upsideDown)
        		func = rect => rect.Top;
            return Chars.LinearRegression(rect => rect.CenterBottom().X, func);
        }
    }

    public class TextEqualityComparer : IEqualityComparer<TextLine> {
        public bool Equals(TextLine x, TextLine y) {
            return x.MBR.Equals(y.MBR);
        }

        public int GetHashCode(TextLine obj) {
            return obj.MBR.GetHashCode();
        }
    }
}