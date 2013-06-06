using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Orient
{
    public class AlgorithmExecutor
    {
        private Dictionary<Rectangle, List<Rectangle>> pointGroup;
//        private Dictionary<Rectangle, List<Rectangle>> lineGroup;
        private const int BetweenWordAndPoint = 2;

        public AlgorithmExecutor(MainFormState state) {
            State = state;
        }

        public AlgorithmExecutor(string file) : this(new MainFormState(file)) {}

        public MainFormState State { get; set; }

        public bool HasCorrectOrientation() {
            if (!State.Criteria90())
                State.Rotate();
            var skew = State.FilteredLines.Average(line => line.LinearRegression().Skew());
            State.Rotate(skew);
            
            var score = CountPatterns();
            State.Rotate(180);
            var rotateScore = CountPatterns();
            
            return score >= rotateScore;
        }

        public int CountPatterns() {
            return 2*(CountPattern(HasPunctuation) + CountPattern(HasQuotations)) + CountPattern(HasUpperCase);
        }

        public int CountPattern(Func<TextLine, bool> countPatterns) {
            pointGroup = State.Points.Group();
            return State.FilteredLines.Count(countPatterns);
        }

	    public bool HasUpperCase(TextLine line) {
            var firstChar = line.Chars.First();
            var regression = line.LinearRegression(true);
	        return regression.P1.Y > firstChar.Y;
	    }

	    public bool HasPunctuation(TextLine word) {
            var punctuation = GetPunctuation(word);
	        return punctuation.Count() != 0;
        }

        public IEnumerable<Rectangle> GetPunctuation(TextLine word) {
            return GetRectangles(word, new Rectangle(word.MBR.Right, word.MBR.Bottom - 10, 5, 15));
        }

        public bool HasQuotations(TextLine word) {
            var punctuation = GetQuatation(word);
	        return punctuation.Count() != 0;
        }

        public IEnumerable<Rectangle> GetQuatation(TextLine word) {
            return GetRectangles(word, new Rectangle(word.MBR.Right, word.MBR.Top - 5, 5, 15));
        }

        private IEnumerable<Rectangle> GetRectangles(TextLine word, Rectangle box) {
            pointGroup = State.Points.Group();
            var pointList = GetPoints(word.MBR.Sector());
            return pointList.Where(box.IntersectsWith);
        }

        private IEnumerable<Rectangle> GetPoints(Rectangle box) {
            return pointGroup.ContainsKey(box) ? pointGroup[box] : new List<Rectangle>();
        }
    }
}
