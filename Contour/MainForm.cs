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

namespace Contour
{
    public partial class MainForm : Form
    {
        private DocumentAnalyser analyser;
        private string filename;
        private Image<Bgr, byte> markedImg;
        private SymbolSegmentation segmentation;

        private MainFormState state;

        public MainForm()
        {
            InitializeComponent();
            imageFileDialog.InitialDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) +
                                               @"\..\..\base\certificates\";
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
            state = new MainFormState(image ?? state.OriginalImg, analyser, segmentation);
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
                Rectangle prev = line.Chars.First();
                foreach (Rectangle next in line.Chars.Skip(1))
                {
                    markedImg.Draw(new LineSegment2D(prev.CenterBottom(), next.CenterBottom()), new Bgr(Color.RoyalBlue),
                                   2);
                    markedImg.Draw(new CircleF(next.CenterBottom(), 2), new Bgr(Color.Aqua), 2);
                    prev = next;
                }
            }
            imageBox.Image = markedImg;
        }

        private void HistClick(object sender, EventArgs e)
        {
            hist.Visible = histButton.Checked;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (state != null)
                    imageBox.Image = state.OriginalImg;
            }
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
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

        private void Image_Resize(object sender, EventArgs e)
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

        private void MainForm_Shown(object sender, EventArgs e)
        {
            UpdateHistPosition();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (state == null) return;
            Image<Bgr, byte> rotate = state.OriginalImg.Rotate(90, new Bgr(Color.Red), false);
            UpdateSettings(rotate);
        }

        private void nextFileButton_Click(object sender, EventArgs e)
        {
            MoveInDirectoryBy(1);
        }

        private void prevFileButton_Click(object sender, EventArgs e)
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

        private void prevToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prevFileButton.PerformClick();
        }

        private void nextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nextFileButton.PerformClick();
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            histButton.PerformClick();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileButton.PerformClick();
        }

        private void options_ValueChanged(object sender, EventArgs e)
        {
            UpdateSettings();
        }

        private void rotateImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rotateButton.PerformClick();
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

        public MainFormState(Image<Bgr, byte> image, DocumentAnalyser analyser, SymbolSegmentation segmentation)
        {
            OriginalImg = image;
            GrayImage = OriginalImg.Process();
            Boxes = segmentation.GetBoundingBoxes(GrayImage);
            Chars = segmentation.FindChars(Boxes);
            Points = segmentation.FindPunctuation(Boxes);
            Lines = analyser.Extract(Chars);
        }
    }
}