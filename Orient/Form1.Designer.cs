namespace Orient
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.openImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.lineImageBox = new Emgu.CV.UI.ImageBox();
            this.originalImageBox = new Emgu.CV.UI.ImageBox();
            this.minWidth = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.threshold = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.gap = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.distanceResolution = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.lineImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.originalImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.threshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.distanceResolution)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Открыть";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1Click);
            // 
            // openImageDialog
            // 
            this.openImageDialog.FileName = "openImageDialog";
            // 
            // lineImageBox
            // 
            this.lineImageBox.Location = new System.Drawing.Point(417, 38);
            this.lineImageBox.Name = "lineImageBox";
            this.lineImageBox.Size = new System.Drawing.Size(400, 400);
            this.lineImageBox.TabIndex = 3;
            this.lineImageBox.TabStop = false;
            // 
            // originalImageBox
            // 
            this.originalImageBox.Location = new System.Drawing.Point(12, 38);
            this.originalImageBox.Name = "originalImageBox";
            this.originalImageBox.Size = new System.Drawing.Size(400, 400);
            this.originalImageBox.TabIndex = 2;
            this.originalImageBox.TabStop = false;
            // 
            // minWidth
            // 
            this.minWidth.Location = new System.Drawing.Point(171, 13);
            this.minWidth.Name = "minWidth";
            this.minWidth.Size = new System.Drawing.Size(61, 20);
            this.minWidth.TabIndex = 4;
            this.minWidth.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(94, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "minLineWidth";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(238, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "threshold";
            // 
            // threshold
            // 
            this.threshold.Location = new System.Drawing.Point(292, 12);
            this.threshold.Name = "threshold";
            this.threshold.Size = new System.Drawing.Size(49, 20);
            this.threshold.TabIndex = 7;
            this.threshold.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(347, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "gapBetweenLines";
            // 
            // gap
            // 
            this.gap.Location = new System.Drawing.Point(445, 13);
            this.gap.Name = "gap";
            this.gap.Size = new System.Drawing.Size(45, 20);
            this.gap.TabIndex = 9;
            this.gap.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(496, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(192, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "distanceResolutionInPixel-RelatedUnits";
            // 
            // distanceResolution
            // 
            this.distanceResolution.Location = new System.Drawing.Point(694, 12);
            this.distanceResolution.Name = "distanceResolution";
            this.distanceResolution.Size = new System.Drawing.Size(55, 20);
            this.distanceResolution.TabIndex = 11;
            this.distanceResolution.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 450);
            this.Controls.Add(this.distanceResolution);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.gap);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.threshold);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.minWidth);
            this.Controls.Add(this.lineImageBox);
            this.Controls.Add(this.originalImageBox);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.lineImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.originalImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.threshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.distanceResolution)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private Emgu.CV.UI.ImageBox originalImageBox;
        private Emgu.CV.UI.ImageBox lineImageBox;
        private System.Windows.Forms.OpenFileDialog openImageDialog;
        private System.Windows.Forms.NumericUpDown minWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown threshold;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown gap;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown distanceResolution;
    }
}

