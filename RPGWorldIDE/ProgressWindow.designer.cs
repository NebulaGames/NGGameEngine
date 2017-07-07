namespace NebulaGames.RPGWorld
{
    partial class ProgressWindow
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
            this.MainProgressBar = new DevExpress.XtraEditors.ProgressBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.MainProgressBar.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // MainProgressBar
            // 
            this.MainProgressBar.Location = new System.Drawing.Point(12, 12);
            this.MainProgressBar.Name = "MainProgressBar";
            this.MainProgressBar.Properties.Step = 1;
            this.MainProgressBar.Size = new System.Drawing.Size(526, 43);
            this.MainProgressBar.TabIndex = 0;
            // 
            // ProgressWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 66);
            this.Controls.Add(this.MainProgressBar);
            this.Name = "ProgressWindow";
            this.Text = "ProgressWindow";
            ((System.ComponentModel.ISupportInitialize)(this.MainProgressBar.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ProgressBarControl MainProgressBar;
    }
}