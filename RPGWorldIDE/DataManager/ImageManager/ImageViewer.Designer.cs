namespace NebulaGames.RPGWorld.DataManager.ImageManager
{
    partial class ImageViewer
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
            this.PicturePreviewBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PicturePreviewBox)).BeginInit();
            this.SuspendLayout();
            // 
            // PicturePreviewBox
            // 
            this.PicturePreviewBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PicturePreviewBox.Location = new System.Drawing.Point(0, 0);
            this.PicturePreviewBox.Name = "PicturePreviewBox";
            this.PicturePreviewBox.Size = new System.Drawing.Size(524, 435);
            this.PicturePreviewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicturePreviewBox.TabIndex = 0;
            this.PicturePreviewBox.TabStop = false;
            // 
            // ImageViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 435);
            this.Controls.Add(this.PicturePreviewBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImageViewer";
            this.ShowIcon = false;
            this.Text = "ImageViewer";
            ((System.ComponentModel.ISupportInitialize)(this.PicturePreviewBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PicturePreviewBox;
    }
}