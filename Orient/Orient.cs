using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Orient
{
    public class Orient
    {
        private MainFormState state;
        private Dictionary<Rectangle, List<Rectangle>> pointGroup;
//        private Dictionary<Rectangle, List<Rectangle>> lineGroup;
        private const int BetweenWordAndPoint = 2;

        public Orient(MainFormState state) {
            this.state = state;
        }

        public Orient(string file) : this(new MainFormState(file)) {}

        public bool HasCorrectOrientation() {
            if (!state.Criteria90())
                state.Rotate();
            var skew = state.FilteredLines.Average(line => line.LinearRegression().Skew());
            state.Rotate(skew);
            
//            lineGroup = state.FilteredLines.Select(line => line.MBR).Group();
            pointGroup = state.Points.Group();
            var score = state.FilteredLines.Sum(line => CountPattern(line));
            state.Rotate(180);

//            lineGroup = state.FilteredLines.Select(line => line.MBR).Group();
            pointGroup = state.Points.Group();
            var rotateScore = state.FilteredLines.Sum(line => CountPattern(line));
            
            return score >= rotateScore;
        }

        public int CountPattern(TextLine line) {
            return FindBraces(line) + FindPunctuation(line) + FindUpperCase(line);
        }

        private int FindUpperCase(TextLine line) {
            var firstChar = line.Chars.First();
            var regression = line.LinearRegression(true);
            return regression.P1.Y > firstChar.Y ? 1 : 0;
        }

        private int FindPunctuation(TextLine line) {
            if (!pointGroup.ContainsKey(line.MBR.Sector())) return 0;
            var pointList = pointGroup[line.MBR.Sector()];
            var remainingLine = line.MBR;
            var yCompatible = pointList.Where(point => point.IntersectsY(remainingLine, -49, 5));
            var punctuation =
                    yCompatible.Where(
                        r => r.Left.InRange(remainingLine.Right, remainingLine.Right + BetweenWordAndPoint));
            return punctuation.Count() != 0 ? 1 : 0;
        }

        private int FindBraces(TextLine line) {
            if (!pointGroup.ContainsKey(line.MBR.Sector())) return 0;
            var pointList = pointGroup[line.MBR.Sector()];
            var remainingLine = line.MBR;
            var yCompatible = pointList.Where(point => point.IntersectsY(remainingLine, 5, -45));
            var punctuation =
                    yCompatible.Where(
                        r => r.Left.InRange(remainingLine.Left - BetweenWordAndPoint, remainingLine.Right + BetweenWordAndPoint));
            return punctuation.Count() != 0 ? 1 : 0;
        }
    }
}
