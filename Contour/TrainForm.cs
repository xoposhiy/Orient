using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.ML;
using Emgu.CV.Structure;

namespace Contour
{
    public partial class TrainForm : Form
    {
        private SVM model;
        private SVMParams param = new SVMParams
            {
                KernelType = Emgu.CV.ML.MlEnum.SVM_KERNEL_TYPE.LINEAR,
                SVMType = Emgu.CV.ML.MlEnum.SVM_TYPE.C_SVC,
                C = 1,
                TermCrit = new MCvTermCriteria(100, 0.00001)
            };
        private const string FileName = @"..\..\Contour\Resources\model";
        private MainForm form = new MainForm();
        private MainFormState state;
        private Dictionary<string, List<TextLineInfo>> data;
        private static readonly string TestBase = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\..\..\base";
        private string file;
        private TextLine line;

        public TrainForm()
        {
            InitializeComponent();
            model = CreateSvm();
            data = Directory.GetDirectories(TestBase).SelectMany(Directory.GetFiles).ToDictionary(file => file, file => new List<TextLineInfo>());
            OpenNext();
        }

        public static SVM CreateSvm()
        {
            var model = new SVM();
            model.Load(FileName);
            return model;
        }

        private bool TrainData()
        {
            var dataInfo = data.Values.SelectMany(list => list).ToList();
            var trainData = new Matrix<float>(dataInfo.Count, 7);
            var trainClass = new Matrix<float>(dataInfo.Count, 1);
            int i = 0;
            foreach (var info in dataInfo)
            {
                /*trainData.Data[i, 0] = (float) info.Line.LinearRegression(true).Skew();
                trainData.Data[i, 1] = info.Line.Chars.Length;
                trainData.Data[i, 2] = (float) info.Line.RelativeWidth(info.Size.Width);
                trainData.Data[i, 3] = (float) info.Line.RelativeHeight(info.Size.Height);
                trainData.Data[i, 4] = (float) info.Line.RegressionVariance();
                trainData.Data[i, 5] = (float) info.Line.MeanHeight(info.Size.Height);
                trainData.Data[i, 6] = (float) info.Line.StandartDeviationHeight();*/
                trainData.Add(Util.GetVector(info.Line, info.Size));
                trainClass.Data[i++, 0] = info.Orientation ? 1 : 0;
            }
            return model.TrainAuto(trainData, trainClass, null, null, param.MCvSVMParams, 5);
        }

        private void SaveButtonClick(object sender, EventArgs e)
        {
            TrainData();
            model.Save(FileName);
        }

        private void TrueButtonClick(object sender, EventArgs e)
        {
            DetectLine(true);
            OpenNext();
        }

        private void OpenNext()
        {
            try
            {
                file = data.Keys.ElementAt(new Random().Next(data.Count));
                form.OpenFile(file);
                Text = file;
                state = form.state;
                if (data[file].Count == state.Lines.Length) OpenNext();
                line = GetLine();
                if (line == null) OpenNext();
                else imageBox.Image = state.OriginalImg.Copy(line.MBR);
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show("Примеры закончились");
            }
        }

        private TextLine GetLine()
        {
            var res = state.Lines[new Random().Next(state.Lines.Length)];
            while (data[file].Count != state.Lines.Length)
                if (data[file].Exists(info => info.Line == res))
                    res = state.Lines[new Random().Next(state.Lines.Length)];
                else return res;
            return null;
        }

        private void DetectLine(bool orient)
        {
            data[file].Add(new TextLineInfo(file, state.GrayImage, line, orient));
        }

        private void FalseButtonClick(object sender, EventArgs e)
        {
            DetectLine(false);
            OpenNext();
        }
    }
}
