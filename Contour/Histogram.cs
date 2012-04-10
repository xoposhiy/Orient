using System;
using System.Collections;
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
            hist.Series.Clear();
            var ser = new Series();
            
            hist.Titles.Add("Колличество/Площадь");

            var boxList = new List<Rectangle>(boxes);
            var data = boxList
                .GroupBy(Util.Area).Select(grp => grp.First())
                .ToDictionary(Util.Area, box => boxList.FindAll(rectangle => rectangle.Area().Equals(box.Area())).Count)
                .OrderByDescending(entry => entry.Key)
                .Where(entry => entry.Value > 7);

            foreach (var dataEntry in data)
                ser.Points.AddXY(dataEntry.Key, dataEntry.Value);

            hist.Series.Add(ser);
        }
    }
}
