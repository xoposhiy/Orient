using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Orient
{
    public class AlgorithmExecutor
    {
        private MainFormState state;
        private Dictionary<Rectangle, List<Rectangle>> pointGroup;
//        private Dictionary<Rectangle, List<Rectangle>> lineGroup;
        private const int BetweenWordAndPoint = 2;

        public AlgorithmExecutor(MainFormState state) {
            this.state = state;
        }

        public AlgorithmExecutor(string file) : this(new MainFormState(file)) {}

        public bool HasCorrectOrientation() {
            if (!state.Criteria90())
                state.Rotate();
            var skew = state.FilteredLines.Average(line => line.LinearRegression().Skew());
            state.Rotate(skew);
            
            var score = CountPattern();
            state.Rotate(180);
            var rotateScore = CountPattern();
            
            return score >= rotateScore;
        }

        public int CountPattern() {
            pointGroup = state.Points.Group();
            return state.FilteredLines.Sum(line => CountPattern(line));
        }

		public int CountPattern(Func<TextLine, int> countPatterns) {
            pointGroup = state.Points.Group();
            return state.FilteredLines.Sum(countPatterns);
        }

        private int CountPattern(TextLine line) {
            return CountQuotations(line) + CountPunctuation(line) + CountUpperCase(line);
        }

	    public int CountUpperCase(TextLine line) {
            var firstChar = line.Chars.First();
            var regression = line.LinearRegression(true);
            return regression.P1.Y > firstChar.Y ? 1 : 0;
        }

	    public int CountPunctuation(TextLine word) {
            if (!pointGroup.ContainsKey(word.MBR.Sector())) return 0;
            var pointList = pointGroup[word.MBR.Sector()];
            var remainingLine = word.MBR;
            var yCompatible = pointList.Where(point => point.IntersectsY(remainingLine, -49, 5));
            var punctuation =
                    yCompatible.Where(
                        r => r.Left.InRange(remainingLine.Right, remainingLine.Right + BetweenWordAndPoint));
            return punctuation.Count() != 0 ? 1 : 0;
        }

	    public int CountQuotations(TextLine line) {
            if (!pointGroup.ContainsKey(line.MBR.Sector())) return 0;
            var pointList = pointGroup[line.MBR.Sector()];
            var remainingLine = line.MBR;
            var yCompatible = pointList.Where(point => point.IntersectsY(remainingLine, 5, -45));
            var punctuation =
                    yCompatible.Where(
                        r => r.Left.InRange(remainingLine.Right, remainingLine.Right + BetweenWordAndPoint));
            return punctuation.Count() != 0 ? 1 : 0;
        }
    }
}
