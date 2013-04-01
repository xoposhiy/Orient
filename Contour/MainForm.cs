using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Emgu.CV;
using Emgu.CV.ML;
using Emgu.CV.Structure;
using Emgu.CV.UI;

namespace Contour
{
    public partial class MainForm : Form
    {
        private DocumentAnalyser analyser;
        private string filename;
        private Image<Bgr, byte> markedImg;
        private SymbolSegmentation segmentation;
        private Binarizaton binarizaton;

        public MainFormState state;
        private SVM model;

        public MainForm()
        {
            InitializeComponent();
            imageFileDialog.InitialDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\..\..\base\certificates\";
            model = TrainForm.CreateSvm();
        }

        private void ImageFileClick(object sender, EventArgs e)
        {
            if (imageFileDialog.ShowDialog() != DialogResult.OK) return;
            OpenFile(imageFileDialog.FileName);
        }

        public void OpenFile(string filenameToOpen)
        {
            filename = filenameToOpen;
            Text = filenameToOpen;
            UpdateSettings(new Image<Bgr, byte>(filename));
        }

        private void UpdateSettings(Image<Bgr, byte> image = null)
        {
            if (state == null && image == null) return;
            analyser = new DocumentAnalyser((int) maxWordDistance.Value);
            segmentation = new SymbolSegmentation((int) maxCharSize.Value, (int) minPunctuationSize.Value, (int)minCharSize.Value);
            binarizaton = new Binarizaton((int)binarizationThreshold.Value, smoothMedianCheckbox.Checked);
            state = new MainFormState(image ?? state.OriginalImg, analyser, segmentation, binarizaton);
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
            hist.ChartAreas[0].AxisX.Minimum = 0;
            hist.Series.Clear();
            hist.Series.Add(ser);

            //Find peak of small boxes (pepper noise)
            var sum = data.Where(keyValuePair => keyValuePair.Key < 5).Sum(keyValuePair => keyValuePair.Value);
            if (!smoothMedianCheckbox.Checked && sum > 5000)
                smoothMedianCheckbox.Checked = true;
        }

        private void UpdateImage()
        {
            markedImg = state.GrayImage.Convert<Bgr, byte>();
            if (ShowMarks)
            {
                if (ShowBoxes)
                    foreach (Rectangle box in state.Boxes.Except(state.Chars).Except(state.Points))
                        markedImg.Draw(box, new Bgr(Color.DarkGray), 1);
                if (ShowLines)
                    foreach (TextLine line in state.Lines)
                        markedImg.Draw(line.MBR, new Bgr(Color.Cyan), 1);

                if (ShowChars)
                    foreach (Rectangle rect in state.Chars)
                        markedImg.Draw(rect, new Bgr(Color.Green), 1);

                if (ShowFilteredLines)
                    foreach (TextLine line in GetLines())
                        markedImg.Draw(line.MBR, new Bgr(Color.Orange), 1);

                if (ShowPunctuation)
                    foreach (Rectangle rect in state.Points)
                        markedImg.Draw(rect, new Bgr(Color.Red), 1);

                if (ShowLinearRegression)
                    foreach (TextLine line in state.Lines)
                    {
                        /*Rectangle prev = line.Chars.First();
                        foreach (Rectangle next in line.Chars.Skip(1))
                        {
                            markedImg.Draw(new LineSegment2D(prev.CenterBottom(), next.CenterBottom()),
                                           new Bgr(Color.RoyalBlue),
                                           2);
                            markedImg.Draw(new CircleF(next.CenterBottom(), 2), new Bgr(Color.Aqua), 2);
                            prev = next;
                        }*/
					    markedImg.Draw(line.LinearRegression(false), new Bgr(Color.RoyalBlue), 2);
					    markedImg.Draw(line.LinearRegression(true), new Bgr(Color.BlueViolet), 2);
				    }
            }
            imageBox.Image = markedImg;
        }

        private IEnumerable<TextLine> GetLines() {
            var lines = state.Lines.Where(IsLine);
//            return lines;
            var nonFiltered = lines.Select(line => line.MBR).ToList();
            return lines.Where(line => !nonFiltered.Where(rect => line.MBR != rect).IntersectsWith(line.MBR));
        }

        private bool IsLine(TextLine line) {
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
            if (skew < -2.29) {
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

        protected bool ShowMarks
        {
            get { return showMarksToolStripMenuItem.Checked; }
        }

        protected bool ShowLines
        {
            get { return showTextLineToolStripMenuItem.Checked; }
        }

        protected bool ShowFilteredLines
        {
            get { return showFilteredTextLineToolStripMenuItem.Checked; }
        }
        
        protected bool ShowBoxes
        {
            get { return showBoxToolStripMenuItem.Checked; }
        }

        protected bool ShowChars
        {
            get { return showCharToolStripMenuItem.Checked; }
        }

        protected bool ShowLinearRegression
        {
            get { return showLinearRegressionToolStripMenuItem.Checked; }
        }
        
        protected bool ShowPunctuation
        {
            get { return showPointPunctuationToolStripMenuItem.Checked; }
        }

        private void HistClick(object sender, EventArgs e)
        {
            hist.Visible = histButton.Checked;
        }

        private void MainFormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (state != null)
                    imageBox.Image = state.OriginalImg;
            }
        }

        private void MainFormKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (markedImg != null)
                    imageBox.Image = markedImg;
            }
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
        }

        private void ImageResize(object sender, EventArgs e)
        {
            UpdateHistPosition();
        }

        private void UpdateHistPosition()
        {
            hist.Width = Width/3;
            hist.Height = Height/3;
            hist.Left = hist.Parent.ClientSize.Width - hist.Width - SystemInformation.VerticalScrollBarWidth;
            hist.Top = hist.Parent.ClientSize.Height - hist.Height - SystemInformation.HorizontalScrollBarHeight;
        }

        private void MainFormShown(object sender, EventArgs e)
        {
            UpdateHistPosition();
        }

        public void RotateButtonClick(object sender, EventArgs e)
        {
            if (state == null) return;
            Image<Bgr, byte> rotate = state.OriginalImg.Rotate(90, new Bgr(Color.White), false);
            UpdateSettings(rotate);
        }

        private void NextFileButtonClick(object sender, EventArgs e)
        {
            MoveInDirectoryBy(1);
        }

        private void PrevFileButtonClick(object sender, EventArgs e)
        {
            MoveInDirectoryBy(-1);
        }

        private void MoveInDirectoryBy(int d)
        {
            if (filename == null) return;
            string dir = Path.GetDirectoryName(filename);
            if (dir == null) return;
            List<string> files = Directory.GetFiles(dir).OrderBy(f => f).ToList();
            int ind = files.IndexOf(filename);
            ind = ind + d;
            if (ind.InRange(0, files.Count - 1))
                OpenFile(files[ind]);
        }

        private void PrevToolStripMenuItemClick(object sender, EventArgs e)
        {
            prevFileButton.PerformClick();
        }

        private void NextToolStripMenuItemClick(object sender, EventArgs e)
        {
            nextFileButton.PerformClick();
        }

        private void HistogramToolStripMenuItemClick(object sender, EventArgs e)
        {
            histButton.PerformClick();
        }

        private void OpenFileToolStripMenuItemClick(object sender, EventArgs e)
        {
            openFileButton.PerformClick();
        }

        private void OptionsValueChanged(object sender, EventArgs e)
        {
            UpdateSettings();
        }

        private void RotateImageToolStripMenuItemClick(object sender, EventArgs e)
        {
            rotateButton.PerformClick();
        }

        private void ColorHistogramToolStripMenuItemClick(object sender, EventArgs e)
        {
            HistogramViewer.Show(state.OriginalImg.Convert<Gray, byte>(), 128);
        }

        private void ShowMarksToolStripMenuItemClick(object sender, EventArgs e)
        {
            UpdateImage();
        }

        private void Run90ToolStripMenuItemClick(object sender, EventArgs e)
        {
            MessageBox.Show(Criteria90() ? "All right" : "Image rotated to the right or left by 90° degrees");
        }

        private bool Criteria90() {
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
            RotateButtonClick(null, null);
//            var rotate = state.Lines.Count(IsLine);
            var rotate = GetLines().Count();
            return current > rotate;
        }

        private double GetAvgAngleOfLongLines() {
            int longetLineLength = state.Lines.Max(lin => lin.Chars.Count())/2;
            return state.Lines.Where(line => line.Chars.Count() > longetLineLength).
                Average(line => Math.Abs(line.LinearRegression(false).Skew()));
        }

        public bool Criteria90(string fileName)
        {
            OpenFile(fileName);
            return Criteria90();
        }

        private void ShowToolbarToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (toolStrip1.Visible) toolStrip1.Hide();
            else toolStrip1.Show();
        }

        private void TrainSvmToolStripMenuItemClick(object sender, EventArgs e)
        {
            new TrainForm().Show();
        }

        private void ShowFilteredTextLineToolStripMenuItemClick(object sender, EventArgs e)
        {
            UpdateImage();
        }

        private void ShowTextLineToolStripMenuItemClick(object sender, EventArgs e)
        {
            UpdateImage();
        }

        private void ShowCharToolStripMenuItemClick(object sender, EventArgs e)
        {
            UpdateImage();
        }

        private void ShowBoxToolStripMenuItemClick(object sender, EventArgs e)
        {
            UpdateImage();
        }

        private void ShowPointPunctuationToolStripMenuItemClick(object sender, EventArgs e)
        {
            UpdateImage();
        }

        private void ShowLinearRegressionToolStripMenuItemClick(object sender, EventArgs e)
        {
            UpdateImage();
        }

        private void RunSvmFilterCriteriaToolStripMenuItemClick(object sender, EventArgs e)
        {
            var skew = new int[4];
            for (int i = 0; i < 4; i++)
            {
                skew[i] = state.Lines.Count(line => model.Predict(Util.GetVector(line, state.OriginalImg.Size)) == 1);
                RotateButtonClick(null, null);
            }
            MessageBox.Show(skew[0] >= skew[1] || skew[0] >= skew[3] || skew[2] >= skew[1] || skew[2] >= skew[3] ? "Write orientation" : "Rotated by 90 degrees");
        }
    }

    public class MainFormState
    {
        public Rectangle[] Boxes;
        public Rectangle[] Chars;
        public Image<Gray, byte> GrayImage;
        public TextLine[] Lines;
        public Image<Bgr, byte> OriginalImg;
        public Rectangle[] Points;

        public MainFormState(Image<Bgr, byte> image, DocumentAnalyser analyser, SymbolSegmentation segmentation, Binarizaton binarizaton)
        {
            OriginalImg = image;
            GrayImage = binarizaton.Process(OriginalImg);
            Boxes = segmentation.GetBoundingBoxes(GrayImage);
            Chars = segmentation.FindChars(Boxes);
            Points = segmentation.FindPunctuation(Boxes);
            Lines = analyser.Extract(Chars);
        }
    }
}