namespace NebulaGames.RPGWorld.Dialogs
{
    partial class ProgressBarWindow
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
            this.MainProgressBar = new System.Windows.Forms.ProgressBar();
            this.ProgressBarMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // MainProgressBar
            // 
            this.MainProgressBar.Location = new System.Drawing.Point(5, 38);
            this.MainProgressBar.Name = "MainProgressBar";
            this.MainProgressBar.Size = new System.Drawing.Size(380, 23);
            this.MainProgressBar.TabIndex = 0;
            // 
            // ProgressBarMessage
            // 
            this.ProgressBarMessage.AutoSize = true;
            this.ProgressBarMessage.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProgressBarMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.ProgressBarMessage.Location = new System.Drawing.Point(9, 9);
            this.ProgressBarMessage.Name = "ProgressBarMessage";
            this.ProgressBarMessage.Size = new System.Drawing.Size(373, 25);
            this.ProgressBarMessage.TabIndex = 1;
            this.ProgressBarMessage.Text = "Updating Package Information";
            // 
            // ProgressBarWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 64);
            this.Controls.Add(this.ProgressBarMessage);
            this.Controls.Add(this.MainProgressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressBarWindow";
            this.ShowIcon = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ProgressBar MainProgressBar;
        public System.Windows.Forms.Label ProgressBarMessage;
    }
}