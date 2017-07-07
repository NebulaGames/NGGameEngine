namespace NebulaGames.RPGWorld.Dialogs.LoadProjectPage
{
    partial class LoadOrCreateProjectDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadOrCreateProjectDialog));
            this.loadOrCreateProject1 = new NebulaGames.RPGWorld.LoadOrCreateProject();
            this.SuspendLayout();
            // 
            // loadOrCreateProject1
            // 
            this.loadOrCreateProject1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("loadOrCreateProject1.BackgroundImage")));
            this.loadOrCreateProject1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.loadOrCreateProject1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadOrCreateProject1.Location = new System.Drawing.Point(0, 0);
            this.loadOrCreateProject1.Name = "loadOrCreateProject1";
            this.loadOrCreateProject1.Size = new System.Drawing.Size(822, 443);
            this.loadOrCreateProject1.TabIndex = 0;
            this.loadOrCreateProject1.Load += new System.EventHandler(this.loadOrCreateProject1_Load);
            // 
            // LoadOrCreateProjectDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 443);
            this.Controls.Add(this.loadOrCreateProject1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoadOrCreateProjectDialog";
            this.Text = "LoadOrCreateProjectDialog";
            this.ResumeLayout(false);

        }

        #endregion

        private LoadOrCreateProject loadOrCreateProject1;
    }
}