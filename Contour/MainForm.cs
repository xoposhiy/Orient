using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Contour
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
			imageFileDialog.InitialDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\..\..\base\certificates\";
        }

        private MainFormState state;

        private void ImageFileClick(object sender, EventArgs e)
        {
			if (imageFileDialog.ShowDialog() != DialogResult.OK) return;
            OpenFile(imageFileDialog.FileName);
        }

        private void OpenFile(string filenameToOpen)
        {
            filename = filenameToOpen;
            Text = filenameToOpen;
            state = new MainFormState(new Image<Bgr, byte>(filename), 50);
            UpdateImage();
            UpdateHistogram();
        }

        private void UpdateHistogram()
        {
            hist.Titles.Clear();
            hist.Titles.Add("Колличество — Диаметр");

            IEnumerable<KeyValuePair<int, int>> data = state.Boxes
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

        private void UpdateImage()
        {
            markedImg = state.GrayImage.Convert<Bgr, byte>();

            foreach (var box in state.Boxes.Except(state.Chars).Except(state.Points))
                markedImg.Draw(box, new Bgr(Color.DarkGray), 1);

            foreach (var line in state.Lines)
                markedImg.Draw(line.MBR, new Bgr(Color.Cyan), 1);

            foreach (var rect in state.Chars)
                markedImg.Draw(rect, new Bgr(Color.Green), 1);

            foreach (var rect in state.Points)
                markedImg.Draw(rect, new Bgr(Color.Red), 1);

            foreach (var line in state.Lines)
            {
                var prev = line.Chars.First();
                foreach (var next in line.Chars.Skip(1))
                {
                    markedImg.Draw(new LineSegment2D(prev.CenterBottom(), next.CenterBottom()), new Bgr(Color.RoyalBlue), 2);
                    markedImg.Draw(new CircleF(next.CenterBottom(), 2), new Bgr(Color.Aqua), 2);
                    prev = next;
                }
            }
            imageBox.Image = markedImg;
        }

    	private Image<Bgr, byte> markedImg;
        private string filename;

        private void HistClick(object sender, EventArgs e)
    	{
            hist.Visible = histButton.Checked;
        }

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Space)
			{
				if (state != null)
					imageBox.Image = state.OriginalImg;
			}
		}

		private void Form1_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Space)
			{
				if (markedImg != null)
					imageBox.Image = markedImg;
			}
		}

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            UpdateHistPosition();
        }

        private void UpdateHistPosition()
        {
            hist.Width = Width/3;
            hist.Height = Height/3;
            hist.Left = ClientSize.Width - hist.Width - SystemInformation.VerticalScrollBarWidth;
            hist.Top = ClientSize.Height - hist.Height - toolStrip1.Height - SystemInformation.HorizontalScrollBarHeight;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            UpdateHistPosition();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            state = new MainFormState(state.OriginalImg.Rotate(90, new Bgr(Color.Red), false), 50);
            UpdateImage();
            UpdateHistogram();
        }

        private void nextFileButton_Click(object sender, EventArgs e)
        {
            MoveInDirectoryBy(1);
        }

        private void MoveInDirectoryBy(int d)
        {
            string dir = Path.GetDirectoryName(filename);
            var files = Directory.GetFiles(dir).OrderBy(f => f).ToList();
            var ind = files.IndexOf(filename);
            ind = ind + d;
            if (ind.InRange(0, files.Count - 1))
                OpenFile(files[ind]);
        }

        private void prevFileButton_Click(object sender, EventArgs e)
        {
            MoveInDirectoryBy(-1);
        }

        private void prevToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveInDirectoryBy(-1);
        }

        private void nextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveInDirectoryBy(1);
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            histButton.PerformClick();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileButton.PerformClick();
        }

    }

    public class MainFormState
    {
        public Image<Bgr, byte> OriginalImg;
        public Image<Gray, byte> GrayImage;
        public Rectangle[] Boxes;
        public Rectangle[] Chars;
        public Rectangle[] Points;
        public TextLine[] Lines;

        public MainFormState(Image<Bgr, byte> image, int lineSeparationThreshold)
        {
            OriginalImg = image;
            GrayImage = OriginalImg.Process();
            Boxes = SymbolSegmentation.GetBoundingBoxes(GrayImage);
            Chars = Boxes.FilterChars();
            Points = Boxes.FilterPoints();
            Lines = Chars.GetLines(lineSeparationThreshold);
        }
    }
}