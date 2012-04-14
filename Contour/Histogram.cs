using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Contour
{
    public partial class Histogram : Form
    {
        public Histogram(IEnumerable<Rectangle> boxes)
        {
            InitializeComponent();
            hist.Titles.Add("Колличество — Диаметр");

            IEnumerable<KeyValuePair<int, int>> data = boxes
                .GroupBy(Util.Diameter)
                .ToDictionary(grp => grp.Key, grp => grp.Count())
                .OrderByDescending(entry => entry.Key)
                .Where(entry => entry.Key < 100);

            var ser = new Series();
            foreach (var dataEntry in data)
                ser.Points.AddXY(dataEntry.Key, dataEntry.Value);
            hist.Series.Clear();
            hist.Series.Add(ser);
        }
    }
}