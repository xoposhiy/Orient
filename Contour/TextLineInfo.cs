using System;
using System.Drawing;
using System.Linq;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Contour
{
    class TextLineInfo
    {
        public TextLine Line { get; private set; }
        public Point Coordinate { get; private set; }
        public String FileName { get; private set; }
        public Size Size { get; private set; }
        public bool Orientation { get; private set; }

        public TextLineInfo(string file, Image<Gray, byte> img, TextLine line, bool orient)
        {
            Line = line;
            var firstRect = line.Chars.First();
            Coordinate = new Point(firstRect.Left, firstRect.Top);
            FileName = file;
            Size = img.Size;
            Orientation = orient;
        }
    }
}
