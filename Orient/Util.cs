using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Orient
{
    public static class Util
    {
        public static bool InRange(this int value, int min, int max) {
            return min <= value && value <= max;
        }

        public static Point DistanceTo(this Rectangle r1, Rectangle r2)
        {
            var c1 = r1.Center();
            var c2 = r2.Center();
            return new Point(Math.Abs(c1.X - c2.X), Math.Abs(c1.Y - c2.Y));
        }

        public static int MaxCoordDistanceTo(this Rectangle r1, Rectangle r2)
        {
            var c1 = r1.Center();
            var c2 = r2.Center();
            return Math.Max(Math.Abs(c1.X - c2.X), Math.Abs(c1.Y - c2.Y));
        }

        public static int Area(this Rectangle rect)
        {
            return rect.Width*rect.Height;
        }

        public static int Diameter(this Rectangle rect)
        {
            return Math.Max(rect.Width, rect.Height);
        }

        public static bool IntersectsWith(this IEnumerable<Rectangle> boxes, Rectangle rect) {
            return boxes.Any(box => box.IntersectsWith(rect));
        }

        public static bool IntersectsWith(this Dictionary<Rectangle, List<Rectangle>> dict, Rectangle rect) {
            return dict[rect.Sector()].Any(box => box.IntersectsWith(rect));
        }

        public static Dictionary<Rectangle, List<Rectangle>> Group(this IEnumerable<Rectangle> rectangles) {
            return rectangles.GroupBy(rect => rect.Sector(), rect => rect).ToDictionary(group => group.Key, group => group.ToList());
        } 

        public static Rectangle Sector(this Rectangle rect, int size = 100) {
            return new Rectangle((rect.X / size) * size, (rect.Y / size) * size, size, size);
        }

        public static Rectangle[] Sectors(this Rectangle rect, int size = 100) {
            return new[] {
                             new Rectangle((rect.Left/size)*size, (rect.Top/size)*size, size, size),
                             new Rectangle((rect.Right/size)*size, (rect.Top/size)*size, size, size),
                             new Rectangle((rect.Left/size)*size, (rect.Bottom/size)*size, size, size),
                             new Rectangle((rect.Right/size)*size, (rect.Bottom/size)*size, size, size)
                         }.Distinct().ToArray();
        }

        public static Rectangle Join(this Rectangle r1, Rectangle r2)
        {
            var x = Math.Min(r1.X, r2.X);
            var y = Math.Min(r1.Y, r2.Y);
            var w = Math.Max(r1.Right, r2.Right) - x;
            var h = Math.Max(r1.Bottom, r2.Bottom) - y;
            return new Rectangle(x, y, w, h);
        }

        public static Point LeftTop(this Rectangle rect)
        {
            return new Point(rect.X, rect.Y);
        }

        public static Point CenterBottom(this Rectangle rect)
        {
            return new Point((rect.Left + rect.Right) / 2, rect.Bottom);
        }
        
        public static Point Center(this Rectangle rect)
        {
            return new Point((rect.Left + rect.Right) / 2, (rect.Top + rect.Bottom) / 2);
        }

        public static Point CenterTop(this Rectangle rect)
        {
            return new Point((rect.Left + rect.Right)/2, rect.Top);
        }

        public static Rectangle Copy(this Rectangle rect)
        {
            return new Rectangle(rect.Location, rect.Size);
        }
		
		public static LineSegment2D LinearRegression<T>(this IList<T> items, Func<T, double> getX, Func<T, double> getY)
		{
			var xAvg = items.Average(getX);
			var yAvg = items.Average(getY);
			var b = (items.Average(item => getX(item) * getY(item)) - xAvg * yAvg) /
				   (items.Average(item => Math.Pow(getX(item), 2)) - Math.Pow(xAvg, 2));
			var a = yAvg - b * xAvg;
			var left = items.Min(getX);
			var right = items.Max(getX);
			return new LineSegment2D(new Point((int) left, (int)(a + b * left)),
									 new Point((int) right, (int)(a + b * right)));
		}

        /// <summary>
        /// Угол наклона
        /// </summary>
        public static double Skew(this LineSegment2D line)
        {
            return line.GetExteriorAngleDegree(HorizontalLine);
        }

        public static LineSegment2D HorizontalLine = new LineSegment2D(new Point(0, 0), new Point(1, 0));

        /// <summary>
        /// Относительная ширина линии
        /// </summary>
        public static double RelativeWidth(this TextLine line, int imgWidth)
        {
            return ((double) line.Chars.Sum(rect => rect.Width)) / imgWidth;
        }

        /// <summary>
        /// Относительная высота линии
        /// </summary>
        public static double RelativeHeight(this TextLine line, int imgHeight)
        {
            return ((double) line.Chars.Max(rect => rect.Height)) / imgHeight;
            //todo use max(rect.bottom) - min(rect.top)
        }

        /// <summary>
        /// Дисперсия расстояний от блоков до линии регрессии
        /// 
        /// TODO  Проверить нереальные числа
        /// </summary>
        public static double RegressionVariance(this TextLine line)
        {
            var regression = line.LinearRegression(true);
            return line.Chars.Sum(rec => Distance(regression, rec.CenterTop()).Sqr()) - line.Chars.Sum(rec => Distance(regression, rec.CenterTop())).Sqr();
        }

        /// <summary>
        /// Относительная средняя высота блоков
        /// </summary>
        public static double MeanHeight(this TextLine line, int imgHeight)
        {
            return line.Chars.Average(rect => rect.Height) / imgHeight;
        }

        /// <summary>
        /// Среднеквадратичное отклонение высоты блоков
        /// </summary>
        public static double StandartDeviationHeight(this TextLine line)
        {
            var mean = line.Chars.Average(rec => rec.Height);
            return Math.Sqrt(line.Chars.Sum(rec => (rec.Height - mean).Sqr()) / line.Chars.Count());
        }

        public static double Sqr(this double num)
        {
            return num*num;
        }

        public static double Distance(this LineSegment2D line, Point p)
        {
            var v = line.P1;
            var w = line.P2;
            if (line.Length == 0) return Distance(p, v);
            var t = ((p.X - v.X) * (w.X - v.X) + (p.Y - v.Y) * (w.Y - v.Y)) / line.Length.Sqr();
            return Distance(p, v.X + t * (w.X - v.X), v.Y + t * (w.Y - v.Y));
        }

        private static double Distance(Point p, double x, double y)
        {
            return Math.Sqrt(Math.Pow(p.X - x, 2) + Math.Pow(p.Y - y, 2));
        }

        private static double Distance(Point p, Point v)
        {
            return Distance(p, v.X, v.Y);
        }

        public static Matrix<float> GetVector(this TextLine line, Size size)
        {
            var res = new Matrix<float>(1, 7);
            res.Data[0, 0] = (float)line.LinearRegression(true).Skew();
            res.Data[0, 1] = line.Chars.Length;
            res.Data[0, 2] = (float)line.RelativeWidth(size.Width);
            res.Data[0, 3] = (float)line.RelativeHeight(size.Height);
            res.Data[0, 4] = (float)line.RegressionVariance();
            res.Data[0, 5] = (float)line.MeanHeight(size.Height);
            res.Data[0, 6] = (float)line.StandartDeviationHeight();
            return res;
        }

        public static Matrix<float> GetVector(this TextLineInfo info) {
            return info.Line.GetVector(info.Size);
        }

        public static bool IsBlack(this Gray color)
        {
            return color.Equals(new Gray(0));
        }
    }
}