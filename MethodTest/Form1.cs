using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using HoughTransform;

namespace MethodTest
{
    public partial class Form1 : Form
    {
        [DllImport("method.dll")]
        public static extern double DetectOrient(Bitmap img);
        [DllImport("method.dll")]
        public static extern double DetectOrient(string file);

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
                    orientation[file] = DetectOrient(file);
//                    orientation[file] = Method.DetectOrient(file);
                    Out.Text += string.Format("{0} {1}\n", file, orientation[file]);
                }
            }
        }
    }
}
