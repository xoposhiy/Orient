using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Contour;

namespace MethodTest
{
    public partial class Form1 : Form
    {
        private Dictionary<string, bool> orientation; 
        private static readonly string TestBase = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\..\..\base";

        public Form1()
        {
            InitializeComponent();
            var form = new MainForm();
            orientation = new Dictionary<string, bool>();
            foreach (var folder in Directory.GetDirectories(TestBase))
                foreach (var file in Directory.GetFiles(folder))
                    orientation.Add(file, form.Criteria90(file));

            dataGridView1.DataSource = new BindingSource(orientation, null);
        }
    }
}
