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
            
            var score = CountPatterns();
            state.Rotate(180);
            var rotateScore = CountPatterns();
            
            return score >= rotateScore;
        }

        public int CountPatterns() {
            return 2*(CountPattern(HasPunctuation) + CountPattern(HasQuotations)) + CountPattern(HasUpperCase);
        }

        public int CountPattern(Func<TextLine, bool> countPatterns) {
            pointGroup = state.Points.Group();
            return state.FilteredLines.Count(countPatterns);
        }

	    public bool HasUpperCase(TextLine line) {
            var firstChar = line.Chars.First();
            var regression = line.LinearRegression(true);
	        return regression.P1.Y >= firstChar.Y;
	    }

	    public bool HasPunctuation(TextLine word) {
            var punctuation = GetPunctuation(word);
	        return punctuation.Count() != 0;
        }

        public IEnumerable<Rectangle> GetPunctuation(TextLine word) {
            return GetRectangles(word, -49, 5);
        }

        public bool HasQuotations(TextLine word) {
            var punctuation = GetQuatation(word);
	        return punctuation.Count() != 0;
        }

        public IEnumerable<Rectangle> GetQuatation(TextLine word) {
            return GetRectangles(word, 5, -45);
        }

        private IEnumerable<Rectangle> GetRectangles(TextLine word, int topIndent, int bottomIndent) {
            if (!pointGroup.ContainsKey(word.MBR.Sector())) return new List<Rectangle>();
            var pointList = pointGroup[word.MBR.Sector()];
            var remainingLine = word.MBR;
            var yCompatible = pointList.Where(point => point.IntersectsY(remainingLine, topIndent, bottomIndent));
            var punctuation =
                yCompatible.Where(
                    r => r.Left.InRange(remainingLine.Right, remainingLine.Right + BetweenWordAndPoint));
            return punctuation;
        }
    }
}
