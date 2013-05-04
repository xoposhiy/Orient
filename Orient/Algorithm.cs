using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Orient
{
    public class Algorithm
    {
        private MainFormState state;
        private Dictionary<Rectangle, List<Rectangle>> pointGroup;
        private Dictionary<Rectangle, List<Rectangle>> lineGroup;
        private const int BetweenWordAndPoint = 2;

        public Algorithm(string file) {
            state = new MainFormState(file);
        }

        public bool Run() {
            if (!state.Criteria90())
                state.Rotate();
            var skew = state.FilteredLines.Average(line => line.LinearRegression().Skew());
            state.Rotate(skew);
            
            lineGroup = state.FilteredLines.Select(line => line.MBR).Group();
            pointGroup = state.Points.Group();
            var score = state.FilteredLines.Sum(line => FindBraces(line) + FindPunctuation(line));
            state.Rotate(180);

            lineGroup = state.FilteredLines.Select(line => line.MBR).Group();
            pointGroup = state.Points.Group();
            var rotateScore = state.FilteredLines.Sum(line => FindBraces(line) + FindPunctuation(line));
            
            return score >= rotateScore;
        }

        private int FindPunctuation(TextLine line) {
            var result = 0;
            if (!pointGroup.ContainsKey(line.MBR.Sector())) return 0;
            var pointList = pointGroup[line.MBR.Sector()];
            var lineList = lineGroup[line.MBR.Sector()];
            var remainingLines = lineList.OrderBy(box => box.X).ToList();
            while (remainingLines.Any()) {
                var remainingLine = remainingLines.First();
                remainingLines.Remove(remainingLine);
                IEnumerable<Rectangle> yCompatible = pointList.Where(point => point.IntersectsY(remainingLine, -49, 5));
                IEnumerable<Rectangle> punctuation =
                        yCompatible.Where(
                            r => r.Left.InRange(remainingLine.Right, remainingLine.Right + BetweenWordAndPoint));
                if (punctuation.Count() != 0)
                    result++;
            }
            return result;
        }

        private int FindBraces(TextLine line) {
            var result = 0;
            if (!pointGroup.ContainsKey(line.MBR.Sector())) return 0;
            var pointList = pointGroup[line.MBR.Sector()];
            var lineList = lineGroup[line.MBR.Sector()];
            var remainingLines = lineList.OrderBy(box => box.X).ToList();
            while (remainingLines.Any())
            {
                var remainingLine = remainingLines.First();
                remainingLines.Remove(remainingLine);
                IEnumerable<Rectangle> yCompatible = pointList.Where(point => point.IntersectsY(remainingLine, 5, -45));
                IEnumerable<Rectangle> punctuation =
                        yCompatible.Where(
                            r => r.Left.InRange(remainingLine.Left - BetweenWordAndPoint, remainingLine.Right + BetweenWordAndPoint));
                if (punctuation.Count() != 0)
                    result++;
            }
            return result;
        }
    }
}
