using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Contour;
using Emgu.CV;
using Emgu.CV.ML;
using Emgu.CV.Structure;

namespace TrainSVM
{
    public partial class TrainForm : Form
    {
        private SVM model;
        private SVMParams param;
        private const string FileName = "model";
        private MainForm form = new MainForm();
        private MainFormState state;
        private Dictionary<int, List<TextLine>> data = new Dictionary<int, List<TextLine>>(); 

        public TrainForm()
        {
            InitializeComponent();
            CreateSvm();
        }

        private void CreateSvm()
        {
            model = new SVM();
            param = new SVMParams
            {
                KernelType = Emgu.CV.ML.MlEnum.SVM_KERNEL_TYPE.LINEAR,
                SVMType = Emgu.CV.ML.MlEnum.SVM_TYPE.C_SVC,
                C = 1,
                TermCrit = new MCvTermCriteria(100, 0.00001)
            };
//            TrainData(model, param, b);
        }

        private bool TrainData(MainFormState state, int skew)
        {
            TextLine[] textLines = state.Lines;
            /*var trainData = new Matrix<float>(textLines.Length, 2);
            var trainClass = new Matrix<float>(textLines.Length, 1);
            for (int i = 0; i < textLines.Length; ++i)
            {
                trainData.Data[i, 0] = textLines[i].Chars.Length;
                trainData.Data[i, 1] = (float)textLines[i].LinearRegression(false).Skew();
                trainClass.Data[i, 0] = skew;
            }
            bool trainAuto = model.TrainAuto(trainData, trainClass, null, null, param.MCvSVMParams, 5);
            MessageBox.Show(trainAuto.ToString());
            return trainAuto;*/
            if (data.ContainsKey(skew))
                data[skew].AddRange(textLines);
            else
                data[skew] = new List<TextLine>(textLines);
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            model.Save(FileName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            model.Load(FileName);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TrainData(state, 0);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (imageFileDialog.ShowDialog() != DialogResult.OK) return;
            form.OpenFile(imageFileDialog.FileName);
            state = form.state;
            imageBox.Image = state.GrayImage;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TrainData(state, 90);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TrainData(state, 180);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TrainData(state, 270);
        }

        private void button8_Click(object sender, EventArgs e)
        {
//            TextLine[] textLines = state.Lines;
            int size = data.Values.Sum(value => value.Count);
            var trainData = new Matrix<float>(size, 2);
            var trainClass = new Matrix<float>(size, 1);
//            for (int i = 0; i < size; ++i)
            int i = 0;
            foreach (var entry in data)
            {
                foreach (var textLine in entry.Value)
                {
                    trainData.Data[i, 0] = textLine.Chars.Length;
                    trainData.Data[i, 1] = (float) textLine.LinearRegression(false).Skew();
                    trainClass.Data[i++, 0] = entry.Key;
                }
            }
            try
            {
                bool trainAuto = model.TrainAuto(trainData, trainClass, null, null, param.MCvSVMParams, 5);
                MessageBox.Show(trainAuto.ToString());
            }
            catch (Exception exception)
            {
                MessageBox.Show("Необходимо как минимум 2 разных класса");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (state == null) return;
            var sample = new Matrix<float>(1, 2);
            var predictValue = new Dictionary<float, int>();
            foreach (var textLine in state.Lines)
            {
                sample.Data[0, 0] = textLine.Chars.Length;
                sample.Data[0, 1] = (float) textLine.LinearRegression(false).Skew();
                float predict = model.Predict(sample);
                if (predictValue.ContainsKey(predict))
                    predictValue[predict]++;
                else
                    predictValue.Add(predict, 1);
            }
            MessageBox.Show(predictValue.Aggregate("", (current, stat) => current + (stat.Key + ": " + stat.Value + "\n")));
        }
    }
}
