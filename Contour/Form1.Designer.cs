namespace Contour
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
            this.lineImageBox = new Emgu.CV.UI.ImageBox();
            this.originalImageBox = new Emgu.CV.UI.ImageBox();
            ((System.ComponentModel.ISupportInitialize)(this.lineImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.originalImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // lineImageBox
            // 
            this.lineImageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lineImageBox.Location = new System.Drawing.Point(417, 12);
            this.lineImageBox.Name = "lineImageBox";
            this.lineImageBox.Size = new System.Drawing.Size(400, 400);
            this.lineImageBox.TabIndex = 13;
            this.lineImageBox.TabStop = false;
            // 
            // originalImageBox
            // 
            this.originalImageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.originalImageBox.Location = new System.Drawing.Point(12, 12);
            this.originalImageBox.Name = "originalImageBox";
            this.originalImageBox.Size = new System.Drawing.Size(400, 400);
            this.originalImageBox.TabIndex = 12;
            this.originalImageBox.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 425);
            this.Controls.Add(this.lineImageBox);
            this.Controls.Add(this.originalImageBox);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.lineImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.originalImageBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Emgu.CV.UI.ImageBox lineImageBox;
        private Emgu.CV.UI.ImageBox originalImageBox;

    }
}

