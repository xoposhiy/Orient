using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Orient
{
    public class MainFormState
    {
        public Rectangle[] Boxes;
        public Rectangle[] Chars;
        public Image<Gray, byte> GrayImage;
        public TextLine[] Lines;
        public Image<Bgr, byte> OriginalImg;
        public Rectangle[] Points;

        public MainFormState(string filename, int maxWordDistance = 10, int maxCharSize = 50, int minPunctuationSize = 3, int minCharSize = 10, int binarizationThreshold = 230, bool smoothMedianCheckbox = false)
        {
            var image = new Image<Bgr, byte>(filename);
            var analyser = new DocumentAnalyser(maxWordDistance);
            var segmentation = new SymbolSegmentation(maxCharSize, minPunctuationSize, minCharSize);
            var binarizaton = new Binarizaton(binarizationThreshold, smoothMedianCheckbox);
            new MainFormState(image, analyser, segmentation, binarizaton);
        }
        
        public MainFormState(Image<Bgr, byte> image, DocumentAnalyser analyser, SymbolSegmentation segmentation, Binarizaton binarizaton)
        {
            OriginalImg = image;
            GrayImage = binarizaton.Process(OriginalImg);
            Boxes = segmentation.GetBoundingBoxes(GrayImage);
            Chars = segmentation.FindChars(Boxes);
            Points = segmentation.FindPunctuation(Boxes);
            Lines = analyser.Extract(Chars);
        }

        public bool Criteria90()
        {
            /*var skew = new double[4];
            for (int i = 0; i < 4; i++)
            {
                double angle = GetAvgAngleOfLongLines();
                skew[i] = angle;
                RotateButtonClick(null, null);
            }
            return skew[1] < skew[0] || skew[1] < skew[2] ||
                   skew[3] < skew[0] || skew[3] < skew[2];*/
            //            var current = state.Lines.Count(IsLine);
            var current = GetLines().Count();
            Rotate();
            //            var rotate = state.Lines.Count(IsLine);
            var rotate = GetLines().Count();
            return current > rotate;
        }

        private double GetAvgAngleOfLongLines()
        {
            int longetLineLength = Lines.Max(lin => lin.Chars.Count()) / 2;
            return Lines.Where(line => line.Chars.Count() > longetLineLength).
                Average(line => Math.Abs(line.LinearRegression().Skew()));
        }

        public void Rotate(double angle = 90) {
            OriginalImg.Rotate(angle, new Bgr(Color.White), false);
        }

        /// <summary>
        /// Get filtered (approved) lines
        /// </summary>
        public IEnumerable<TextLine> GetLines()
        {
            //            return lines;
            var lines = Lines.Where(IsLine).ToList();
            return GetNonIntersectedLines(lines);
            //            return GetJoinedLines(lines);
        }

        /// <summary>
        /// Except any line with intersection 
        /// </summary>
        private static IEnumerable<TextLine> GetNonIntersectedLines(List<TextLine> lines)
        {
            var nonFiltered = lines.Select(line => line.MBR).ToList();
            var dictionary = nonFiltered.ToArray().Group();
            return lines.Where(line => !nonFiltered.Where(rect => line.MBR != rect).IntersectsWith(line.MBR));
        }

        /// <summary>
        /// Join intersected lines 
        /// </summary>
        private IEnumerable<TextLine> GetJoinedLines(List<TextLine> lines)
        {
            var rects = lines.Select(line => line.MBR).ToList();
            var unionLines = rects.Select(rect => new TextLine(rects.Where(rect.IntersectsWith).ToArray())).ToList();
            return new HashSet<TextLine>(unionLines.Where(IsLine), new TextEqualityComparer());
        }

        private bool IsLine(TextLine line)
        {
//            return model.Predict(Util.GetVector(line, state.OriginalImg.Size)) == 1;
//            skew, count, rWidth, meanHeight, stdDev (Use training set 88% correctly)
//            skew (85%)
            /*skew < -2.29
            |   skew < -8 : 0 (13/0) [4/0]
            |   skew >= -8
            |   |   stdDev < 3.26 : 0 (7/0) [2/0]
            |   |   stdDev >= 3.26 : 1 (7/3) [1/0]
            skew >= -2.29
            |   stdDev < 4.33 : 1 (55/4) [31/6]
            |   stdDev >= 4.33
            |   |   skew < 1.91
            |   |   |   skew < -1.19 : 0 (2/0) [1/0]
            |   |   |   skew >= -1.19 : 1 (7/2) [4/1]
            |   |   skew >= 1.91 : 0 (8/0) [7/2]*/
            var skew = line.LinearRegression(true).Skew();
            var standartDeviationHeight = line.StandartDeviationHeight();
            if (skew < -2.29)
            {
                if (skew < -8)
                    return false;
                return standartDeviationHeight >= 3.26;
            }
            if (standartDeviationHeight < 4.33)
                return true;
            if (skew < 1.91)
                return skew >= -1.19;
            return false;
        }
    }
}
