namespace Contour
{
    partial class TrainForm
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
            this.SaveButton = new System.Windows.Forms.Button();
            this.TrueButton = new System.Windows.Forms.Button();
            this.FalseButton = new System.Windows.Forms.Button();
            this.imageFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBox
            // 
            this.imageBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imageBox.Location = new System.Drawing.Point(12, 12);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(406, 150);
            this.imageBox.TabIndex = 21;
            this.imageBox.TabStop = false;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(12, 168);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(132, 23);
            this.SaveButton.TabIndex = 22;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButtonClick);
            // 
            // TrueButton
            // 
            this.TrueButton.Location = new System.Drawing.Point(150, 168);
            this.TrueButton.Name = "TrueButton";
            this.TrueButton.Size = new System.Drawing.Size(132, 23);
            this.TrueButton.TabIndex = 24;
            this.TrueButton.Text = "0°";
            this.TrueButton.UseVisualStyleBackColor = true;
            this.TrueButton.Click += new System.EventHandler(this.TrueButtonClick);
            // 
            // FalseButton
            // 
            this.FalseButton.Location = new System.Drawing.Point(288, 168);
            this.FalseButton.Name = "FalseButton";
            this.FalseButton.Size = new System.Drawing.Size(132, 23);
            this.FalseButton.TabIndex = 25;
            this.FalseButton.Text = "90°";
            this.FalseButton.UseVisualStyleBackColor = true;
            this.FalseButton.Click += new System.EventHandler(this.FalseButtonClick);
            // 
            // imageFileDialog
            // 
            this.imageFileDialog.FileName = "imageFileDialog";
            // 
            // TrainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 201);
            this.Controls.Add(this.FalseButton);
            this.Controls.Add(this.TrueButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.imageBox);
            this.Name = "TrainForm";
            this.Text = "Train";
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Emgu.CV.UI.ImageBox imageBox;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button TrueButton;
        private System.Windows.Forms.Button FalseButton;
        protected System.Windows.Forms.OpenFileDialog imageFileDialog;

    }
}