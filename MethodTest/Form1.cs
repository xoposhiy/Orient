using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Orient;

namespace MethodTest {
    public partial class Form1 : Form {
        private static readonly string TestBase = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) +
                                                  @"\..\..\base";

        private IEnumerable<string> files;

        public Form1() {
            InitializeComponent();
            files = Directory.GetDirectories(TestBase).SelectMany(Directory.GetFiles);
        }

        private void Run90Criteria(object sender, EventArgs e) {
            var orientation = new Dictionary<string, bool>();
            try {
                foreach (var file in files) {
                    orientation[file] = new MainFormState(file).Criteria90();
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

            dataGridView1.Visible = true;
            dataGridView1.DataSource = new BindingSource(orientation, null);
            Text = orientation.Count.ToString() + '/' + files.Count();
        }

        private void Run180Criteria(object sender, EventArgs e) {
            var orientation = new Dictionary<string, bool>();
            try {
                foreach (var file in files) {
                    var state = new MainFormState(file);
//                    state.Rotate(180);
                    orientation[file] = state.Criteria180();
//                    orientation[file] = new Orient.Orient(file).CountPattern();
                }
            }
            catch (Exception ex) {
//                MessageBox.Show(ex.Message);
            }

            dataGridView1.Visible = true;
            dataGridView1.DataSource = new BindingSource(orientation, null);
            foreach (var b in orientation) {
                
            }
            Text = orientation.Values.Count(value => value).ToString() + '/' + orientation.Count + '/' + files.Count();
        }
    }
}
