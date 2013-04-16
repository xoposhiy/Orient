using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.ML;
using Emgu.CV.Structure;
using Orient;

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
        private const string ModelFileName = @"..\..\Contour\Resources\model";
        private const string InfoFileName = @"..\..\Contour\Resources\info.arff";
        private MainForm form = new MainForm();
        private MainFormState state;
        private Dictionary<string, List<TextLineInfo>> data;
        private static readonly string TestBase = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\..\..\base";
        private string file;
        private TextLine line;
        private Queue<string> files;

        public TrainForm()
        {
            InitializeComponent();
            model = CreateSvm();
            data = Directory.GetDirectories(TestBase).SelectMany(Directory.GetFiles).ToDictionary(file => file, file => new List<TextLineInfo>());
            files = new Queue<string>(data.Keys);
            OpenNext();
//            LoadDetectedLine();
        }

        public static SVM CreateSvm()
        {
            var model = new SVM();
            model.Load(ModelFileName);
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
                var vector = info.GetVector();//info.Line.GetVector(info.Size);
                for (var j = 0; j < 7; ++j)
                    trainData.Data[i, j] = vector.Data[0, j];
                trainClass.Data[i, 0] = info.Orientation ? 1 : 0;
	            i++;
            }
            return model.TrainAuto(trainData, trainClass, null, null, param.MCvSVMParams, 5);
        }

        private void SaveButtonClick(object sender, EventArgs e)
        {
            try {
                SaveDetectedLine();
                TrainData();
                model.Save(ModelFileName);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            finally {
                MessageBox.Show(string.Format("Text lines marking saved to {0}\nSVM Model trained", InfoFileName));
            }
        }

        private void LoadDetectedLine()
        {
            var serializer = new BinaryFormatter();
            if (!File.Exists(InfoFileName)) return;
            var reader = new FileStream(InfoFileName, FileMode.Open);
            try
            {
                TextLineInfo[] infos = (TextLineInfo[]) serializer.Deserialize(reader);
                foreach (var info in infos)
                {
                    data[info.FileName].Add(info);
                }
            }
            finally
            {
                reader.Close();
            }
        }

        private void SaveDetectedLine()
        {
//            var serializer = new XmlSerializer(typeof (TextLineInfo));
//            var writer = XmlWriter.Create(InfoFileName); 
//            var reader = XmlReader.Create(InfoFileName);
//            var serializer = new BinaryFormatter();
            var writer = new FileStream(InfoFileName, FileMode.Append);
            var stream = new StreamWriter(writer);
//            foreach (var info in data.Values.SelectMany(list => list))
//                serializer.Serialize(writer, info);
//            serializer.Serialize(writer, data.Values.SelectMany(list => list).ToArray());
            foreach (var info in data.Values.SelectMany(list => list)) {
                var vector = info.GetVector();
                var r = new List<float>();
                for (var i = 0; i < vector.Cols; i++)
                    r.Add(vector.Data[0, i]);
                r.Add(info.Orientation ? 1 : 0);
                stream.WriteLine(string.Join(",", r));
            }
            stream.Flush();
            writer.Close();
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
                file = GetFile();
                form.OpenFile(file);
                Text = file;
                var rotate = new Random().Next(4);
                for (var i = 0; i < rotate; i++)
                    form.RotateButtonClick(null, null);
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

        private string GetFile() {
//            return data.Keys.ElementAt(new Random().Next(data.Count));
            var res = files.Dequeue();
            files.Enqueue(res);
            return res;
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
