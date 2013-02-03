namespace TrainSVM
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.imageFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.openFileButton = new System.Windows.Forms.ToolStripButton();
            this.histButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.rotateButton = new System.Windows.Forms.ToolStripButton();
            this.prevFileButton = new System.Windows.Forms.ToolStripButton();
            this.nextFileButton = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.optionsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.maxCharSize = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.minCharSize = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.minPunctuationSize = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.maxWordDistance = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.binarizationThreshold = new System.Windows.Forms.NumericUpDown();
            this.smoothMedianCheckbox = new System.Windows.Forms.CheckBox();
            this.hist = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.imageBox = new Emgu.CV.UI.ImageBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prevToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.histogramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorHistogramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showMarksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.run90ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolbarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.operationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.optionsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxCharSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minCharSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minPunctuationSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxWordDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.binarizationThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageFileDialog
            // 
            this.imageFileDialog.FileName = "imageFileDialog";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // openFileButton
            // 
            this.openFileButton.Image = global::TrainSVM.Properties.Resources.folder_open;
            this.openFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(91, 36);
            this.openFileButton.Text = "Open file";
            this.openFileButton.Click += new System.EventHandler(this.ImageFileClick);
            // 
            // histButton
            // 
            this.histButton.CheckOnClick = true;
            this.histButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.histButton.Image = ((System.Drawing.Image)(resources.GetObject("histButton.Image")));
            this.histButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.histButton.Name = "histButton";
            this.histButton.Size = new System.Drawing.Size(36, 36);
            this.histButton.Text = "Show histogram";
            this.histButton.Click += new System.EventHandler(this.HistClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(323, 36);
            this.toolStripLabel1.Text = "hold SPACE to show original image";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileButton,
            this.histButton,
            this.rotateButton,
            this.prevFileButton,
            this.nextFileButton,
            this.toolStripSeparator1,
            this.toolStripLabel1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(908, 39);
            this.toolStrip1.TabIndex = 17;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Visible = false;
            // 
            // rotateButton
            // 
            this.rotateButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.rotateButton.Image = ((System.Drawing.Image)(resources.GetObject("rotateButton.Image")));
            this.rotateButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rotateButton.Name = "rotateButton";
            this.rotateButton.Size = new System.Drawing.Size(36, 36);
            this.rotateButton.Text = "Rotate image";
            this.rotateButton.Click += new System.EventHandler(this.RotateButtonClick);
            // 
            // prevFileButton
            // 
            this.prevFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.prevFileButton.Image = ((System.Drawing.Image)(resources.GetObject("prevFileButton.Image")));
            this.prevFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.prevFileButton.Name = "prevFileButton";
            this.prevFileButton.Size = new System.Drawing.Size(36, 36);
            this.prevFileButton.Text = "Previous file";
            this.prevFileButton.ToolTipText = "Previous file";
            this.prevFileButton.Click += new System.EventHandler(this.PrevFileButtonClick);
            // 
            // nextFileButton
            // 
            this.nextFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.nextFileButton.Image = ((System.Drawing.Image)(resources.GetObject("nextFileButton.Image")));
            this.nextFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.nextFileButton.Name = "nextFileButton";
            this.nextFileButton.Size = new System.Drawing.Size(36, 36);
            this.nextFileButton.Text = "Next file";
            this.nextFileButton.ToolTipText = "Next file";
            this.nextFileButton.Click += new System.EventHandler(this.NextFileButtonClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.optionsPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.hist);
            this.splitContainer1.Panel2.Controls.Add(this.imageBox);
            this.splitContainer1.Panel2.Resize += new System.EventHandler(this.ImageResize);
            this.splitContainer1.Size = new System.Drawing.Size(908, 482);
            this.splitContainer1.SplitterDistance = 222;
            this.splitContainer1.SplitterWidth = 10;
            this.splitContainer1.TabIndex = 20;
            // 
            // optionsPanel
            // 
            this.optionsPanel.AutoScroll = true;
            this.optionsPanel.Controls.Add(this.label2);
            this.optionsPanel.Controls.Add(this.maxCharSize);
            this.optionsPanel.Controls.Add(this.label3);
            this.optionsPanel.Controls.Add(this.minCharSize);
            this.optionsPanel.Controls.Add(this.label4);
            this.optionsPanel.Controls.Add(this.minPunctuationSize);
            this.optionsPanel.Controls.Add(this.label1);
            this.optionsPanel.Controls.Add(this.maxWordDistance);
            this.optionsPanel.Controls.Add(this.label5);
            this.optionsPanel.Controls.Add(this.binarizationThreshold);
            this.optionsPanel.Controls.Add(this.smoothMedianCheckbox);
            this.optionsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optionsPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.optionsPanel.Location = new System.Drawing.Point(0, 0);
            this.optionsPanel.Name = "optionsPanel";
            this.optionsPanel.Padding = new System.Windows.Forms.Padding(8);
            this.optionsPanel.Size = new System.Drawing.Size(222, 482);
            this.optionsPanel.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 16);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "max size of char MBR:";
            // 
            // maxCharSize
            // 
            this.maxCharSize.Location = new System.Drawing.Point(11, 32);
            this.maxCharSize.Name = "maxCharSize";
            this.maxCharSize.Size = new System.Drawing.Size(120, 20);
            this.maxCharSize.TabIndex = 3;
            this.maxCharSize.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.maxCharSize.ValueChanged += new System.EventHandler(this.OptionsValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 63);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "min size of char MBR:";
            // 
            // minCharSize
            // 
            this.minCharSize.Location = new System.Drawing.Point(11, 79);
            this.minCharSize.Name = "minCharSize";
            this.minCharSize.Size = new System.Drawing.Size(120, 20);
            this.minCharSize.TabIndex = 5;
            this.minCharSize.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.minCharSize.ValueChanged += new System.EventHandler(this.OptionsValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 110);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "min size of punctuation MBR:";
            // 
            // minPunctuationSize
            // 
            this.minPunctuationSize.Location = new System.Drawing.Point(11, 126);
            this.minPunctuationSize.Name = "minPunctuationSize";
            this.minPunctuationSize.Size = new System.Drawing.Size(120, 20);
            this.minPunctuationSize.TabIndex = 7;
            this.minPunctuationSize.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.minPunctuationSize.ValueChanged += new System.EventHandler(this.OptionsValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 157);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "max distance between words in one line:";
            // 
            // maxWordDistance
            // 
            this.maxWordDistance.Location = new System.Drawing.Point(11, 173);
            this.maxWordDistance.Name = "maxWordDistance";
            this.maxWordDistance.Size = new System.Drawing.Size(120, 20);
            this.maxWordDistance.TabIndex = 9;
            this.maxWordDistance.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.maxWordDistance.ValueChanged += new System.EventHandler(this.OptionsValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 204);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "binarization threshold";
            // 
            // binarizationThreshold
            // 
            this.binarizationThreshold.Location = new System.Drawing.Point(11, 220);
            this.binarizationThreshold.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.binarizationThreshold.Name = "binarizationThreshold";
            this.binarizationThreshold.Size = new System.Drawing.Size(120, 20);
            this.binarizationThreshold.TabIndex = 11;
            this.binarizationThreshold.Value = new decimal(new int[] {
            230,
            0,
            0,
            0});
            this.binarizationThreshold.ValueChanged += new System.EventHandler(this.OptionsValueChanged);
            // 
            // smoothMedianCheckbox
            // 
            this.smoothMedianCheckbox.AutoSize = true;
            this.smoothMedianCheckbox.Checked = true;
            this.smoothMedianCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.smoothMedianCheckbox.Location = new System.Drawing.Point(11, 246);
            this.smoothMedianCheckbox.Name = "smoothMedianCheckbox";
            this.smoothMedianCheckbox.Size = new System.Drawing.Size(97, 17);
            this.smoothMedianCheckbox.TabIndex = 13;
            this.smoothMedianCheckbox.Text = "smooth median";
            this.smoothMedianCheckbox.UseVisualStyleBackColor = true;
            this.smoothMedianCheckbox.CheckedChanged += new System.EventHandler(this.OptionsValueChanged);
            // 
            // hist
            // 
            chartArea1.Name = "ChartArea1";
            this.hist.ChartAreas.Add(chartArea1);
            this.hist.Location = new System.Drawing.Point(333, 236);
            this.hist.Name = "hist";
            series1.ChartArea = "ChartArea1";
            series1.Name = "Series1";
            this.hist.Series.Add(series1);
            this.hist.Size = new System.Drawing.Size(316, 178);
            this.hist.TabIndex = 22;
            this.hist.Text = "chart1";
            this.hist.Visible = false;
            // 
            // imageBox
            // 
            this.imageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox.Location = new System.Drawing.Point(0, 0);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(676, 482);
            this.imageBox.TabIndex = 20;
            this.imageBox.TabStop = false;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.operationsToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(908, 24);
            this.menuStrip.TabIndex = 16;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.prevToolStripMenuItem,
            this.nextToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.openFileToolStripMenuItem.Text = "Open file";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.OpenFileToolStripMenuItemClick);
            // 
            // prevToolStripMenuItem
            // 
            this.prevToolStripMenuItem.Name = "prevToolStripMenuItem";
            this.prevToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)));
            this.prevToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.prevToolStripMenuItem.Text = "Previous file";
            this.prevToolStripMenuItem.Click += new System.EventHandler(this.PrevToolStripMenuItemClick);
            // 
            // nextToolStripMenuItem
            // 
            this.nextToolStripMenuItem.Name = "nextToolStripMenuItem";
            this.nextToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)));
            this.nextToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.nextToolStripMenuItem.Text = "Next file";
            this.nextToolStripMenuItem.Click += new System.EventHandler(this.NextToolStripMenuItemClick);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.histogramToolStripMenuItem,
            this.colorHistogramToolStripMenuItem,
            this.showMarksToolStripMenuItem,
            this.run90ToolStripMenuItem,
            this.showToolbarToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // histogramToolStripMenuItem
            // 
            this.histogramToolStripMenuItem.CheckOnClick = true;
            this.histogramToolStripMenuItem.Name = "histogramToolStripMenuItem";
            this.histogramToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.histogramToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.histogramToolStripMenuItem.Text = "Show MBR size histogram";
            this.histogramToolStripMenuItem.Click += new System.EventHandler(this.HistogramToolStripMenuItemClick);
            // 
            // colorHistogramToolStripMenuItem
            // 
            this.colorHistogramToolStripMenuItem.Name = "colorHistogramToolStripMenuItem";
            this.colorHistogramToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.colorHistogramToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.colorHistogramToolStripMenuItem.Text = "Intensity histogram";
            this.colorHistogramToolStripMenuItem.Click += new System.EventHandler(this.ColorHistogramToolStripMenuItemClick);
            // 
            // showMarksToolStripMenuItem
            // 
            this.showMarksToolStripMenuItem.CheckOnClick = true;
            this.showMarksToolStripMenuItem.Name = "showMarksToolStripMenuItem";
            this.showMarksToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.showMarksToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.showMarksToolStripMenuItem.Text = "Show MBR marks";
            this.showMarksToolStripMenuItem.Click += new System.EventHandler(this.ShowMarksToolStripMenuItemClick);
            // 
            // run90ToolStripMenuItem
            // 
            this.run90ToolStripMenuItem.Name = "run90ToolStripMenuItem";
            this.run90ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D9)));
            this.run90ToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.run90ToolStripMenuItem.Text = "Run 90° criteria";
            this.run90ToolStripMenuItem.Click += new System.EventHandler(this.Run90ToolStripMenuItemClick);
            // 
            // showToolbarToolStripMenuItem
            // 
            this.showToolbarToolStripMenuItem.Name = "showToolbarToolStripMenuItem";
            this.showToolbarToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.showToolbarToolStripMenuItem.Size = new System.Drawing.Size(253, 22);
            this.showToolbarToolStripMenuItem.Text = "Show toolbar";
            this.showToolbarToolStripMenuItem.Click += new System.EventHandler(this.ShowToolbarToolStripMenuItemClick);
            // 
            // operationsToolStripMenuItem
            // 
            this.operationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rotateImageToolStripMenuItem});
            this.operationsToolStripMenuItem.Name = "operationsToolStripMenuItem";
            this.operationsToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.operationsToolStripMenuItem.Text = "Operations";
            // 
            // rotateImageToolStripMenuItem
            // 
            this.rotateImageToolStripMenuItem.Name = "rotateImageToolStripMenuItem";
            this.rotateImageToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.rotateImageToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.rotateImageToolStripMenuItem.Text = "Rotate image";
            this.rotateImageToolStripMenuItem.Click += new System.EventHandler(this.RotateImageToolStripMenuItemClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 506);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "Orient";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.Shown += new System.EventHandler(this.MainFormShown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainFormKeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainFormKeyUp);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.optionsPanel.ResumeLayout(false);
            this.optionsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxCharSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minCharSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minPunctuationSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxWordDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.binarizationThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog imageFileDialog;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton openFileButton;
        private System.Windows.Forms.ToolStripButton histButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton rotateButton;
        private System.Windows.Forms.ToolStripButton prevFileButton;
        private System.Windows.Forms.ToolStripButton nextFileButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Emgu.CV.UI.ImageBox imageBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart hist;
        private System.Windows.Forms.FlowLayoutPanel optionsPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown maxCharSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown minCharSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown minPunctuationSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown maxWordDistance;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prevToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem histogramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem operationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotateImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorHistogramToolStripMenuItem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown binarizationThreshold;
        private System.Windows.Forms.ToolStripMenuItem showMarksToolStripMenuItem;
        private System.Windows.Forms.CheckBox smoothMedianCheckbox;
        private System.Windows.Forms.ToolStripMenuItem run90ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolbarToolStripMenuItem;
    }
}

