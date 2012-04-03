namespace MethodTest
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
            this.FolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.DllDialog = new System.Windows.Forms.OpenFileDialog();
            this.Out = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DllDialog
            // 
            this.DllDialog.FileName = "openFileDialog1";
            // 
            // Out
            // 
            this.Out.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Out.AutoSize = true;
            this.Out.Location = new System.Drawing.Point(13, 13);
            this.Out.Name = "Out";
            this.Out.Size = new System.Drawing.Size(0, 13);
            this.Out.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.Out);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog FolderDialog;
        private System.Windows.Forms.OpenFileDialog DllDialog;
        private System.Windows.Forms.Label Out;
    }
}