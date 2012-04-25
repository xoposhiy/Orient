using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Emgu.CV;
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

        private MainFormState state;

        public MainForm()
        {
            InitializeComponent();
            imageFileDialog.InitialDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\..\..\base\certificates\";
        }

        private void ImageFileClick(object sender, EventArgs e)
        {
            if (imageFileDialog.ShowDialog() != DialogResult.OK) return;
            OpenFile(imageFileDialog.FileName);
        }

        private void OpenFile(string filenameToOpen)
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
        }

        private void UpdateImage()
        {
            markedImg = state.GrayImage.Convert<Bgr, byte>();
            if (ShowMarks)
            {
                foreach (Rectangle box in state.Boxes.Except(state.Chars).Except(state.Points))
                    markedImg.Draw(box, new Bgr(Color.DarkGray), 1);

                foreach (TextLine line in state.Lines)
                    markedImg.Draw(line.MBR, new Bgr(Color.Cyan), 1);

                foreach (Rectangle rect in state.Chars)
                    markedImg.Draw(rect, new Bgr(Color.Green), 1);

                foreach (Rectangle rect in state.Points)
                    markedImg.Draw(rect, new Bgr(Color.Red), 1);

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

        protected bool ShowMarks
        {
            get { return showMarksToolStripMenuItem.Checked; }
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

        private void ToolStripButton1Click(object sender, EventArgs e)
        {
            if (state == null) return;
            Image<Bgr, byte> rotate = state.OriginalImg.Rotate(90, new Bgr(Color.Red), false);
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