using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace MethodTest
{
    public partial class Form1 : Form
    {
        private Dictionary<string, double> orientation; 
        
        public Form1()
        {
            InitializeComponent();
            
            orientation = new Dictionary<string, double>();
            if (FolderDialog.ShowDialog() == DialogResult.OK)
                foreach (var file in Directory.GetFiles(FolderDialog.SelectedPath))
                    orientation.Add(file, 0);

            if (DllDialog.ShowDialog() == DialogResult.OK)
            {
                File.Copy(DllDialog.FileName, "method.dll", true);
                var keyCollection = new string[orientation.Keys.Count];
                orientation.Keys.CopyTo(keyCollection, 0);
                foreach (var file in keyCollection)
                {
                    orientation[file] = 1;// DetectOrient(file);
//                    orientation[file] = Method.DetectOrient(file);
                    Out.Text += string.Format("{0} {1}\n", file, orientation[file]);
                }
            }
        }
    }
}
