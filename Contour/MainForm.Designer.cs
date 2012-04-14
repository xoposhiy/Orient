namespace Contour
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
			this.imageBox = new Emgu.CV.UI.ImageBox();
			this.imageFile = new System.Windows.Forms.Button();
			this.imageFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.hist = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
			this.SuspendLayout();
			// 
			// imageBox
			// 
			this.imageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.imageBox.Location = new System.Drawing.Point(12, 41);
			this.imageBox.Name = "imageBox";
			this.imageBox.Size = new System.Drawing.Size(805, 400);
			this.imageBox.TabIndex = 13;
			this.imageBox.TabStop = false;
			// 
			// imageFile
			// 
			this.imageFile.Location = new System.Drawing.Point(12, 12);
			this.imageFile.Name = "imageFile";
			this.imageFile.Size = new System.Drawing.Size(75, 23);
			this.imageFile.TabIndex = 14;
			this.imageFile.Text = "Image";
			this.imageFile.UseVisualStyleBackColor = true;
			this.imageFile.Click += new System.EventHandler(this.ImageFileClick);
			// 
			// imageFileDialog
			// 
			this.imageFileDialog.FileName = "imageFileDialog";
			// 
			// hist
			// 
			this.hist.Location = new System.Drawing.Point(93, 12);
			this.hist.Name = "hist";
			this.hist.Size = new System.Drawing.Size(75, 23);
			this.hist.TabIndex = 15;
			this.hist.Text = "Histogram";
			this.hist.UseVisualStyleBackColor = true;
			this.hist.Click += new System.EventHandler(this.HistClick);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(212, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(391, 26);
			this.label1.TabIndex = 16;
			this.label1.Text = "hold SPACE to show original image";
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// Orient
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(832, 454);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.hist);
			this.Controls.Add(this.imageFile);
			this.Controls.Add(this.imageBox);
			this.KeyPreview = true;
			this.Name = "MainForm";
			this.Text = "Orient";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
			((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private Emgu.CV.UI.ImageBox imageBox;
        private System.Windows.Forms.Button imageFile;
        private System.Windows.Forms.OpenFileDialog imageFileDialog;
        private System.Windows.Forms.Button hist;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ImageList imageList1;

    }
}

