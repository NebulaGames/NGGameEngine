namespace NebulaGames.RPGWorld
{
    partial class LoadOrCreateProject
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.NewBlankProject = new DevExpress.XtraEditors.SimpleButton();
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.PreviousProjects = new DevExpress.XtraEditors.ListBoxControl();
            this.LoadProjectButton = new DevExpress.XtraEditors.SimpleButton();
            this.BrowseForProjectButton = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PreviousProjects)).BeginInit();
            this.SuspendLayout();
            // 
            // NewBlankProject
            // 
            this.NewBlankProject.Location = new System.Drawing.Point(14, 13);
            this.NewBlankProject.Name = "NewBlankProject";
            this.NewBlankProject.Size = new System.Drawing.Size(119, 74);
            this.NewBlankProject.StyleController = this.styleController1;
            this.NewBlankProject.TabIndex = 0;
            this.NewBlankProject.Text = "New Blank Project";
            this.NewBlankProject.Click += new System.EventHandler(this.NewBlankProject_Click);
            // 
            // styleController1
            // 
            this.styleController1.LookAndFeel.SkinName = "DevExpress Dark Style";
            this.styleController1.LookAndFeel.UseDefaultLookAndFeel = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.NewBlankProject);
            this.panel1.Location = new System.Drawing.Point(351, 18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(463, 417);
            this.panel1.TabIndex = 1;
            // 
            // PreviousProjects
            // 
            this.PreviousProjects.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PreviousProjects.Location = new System.Drawing.Point(24, 53);
            this.PreviousProjects.Name = "PreviousProjects";
            this.PreviousProjects.Size = new System.Drawing.Size(321, 343);
            this.PreviousProjects.StyleController = this.styleController1;
            this.PreviousProjects.TabIndex = 2;
            // 
            // LoadProjectButton
            // 
            this.LoadProjectButton.Location = new System.Drawing.Point(226, 402);
            this.LoadProjectButton.Name = "LoadProjectButton";
            this.LoadProjectButton.Size = new System.Drawing.Size(119, 33);
            this.LoadProjectButton.StyleController = this.styleController1;
            this.LoadProjectButton.TabIndex = 2;
            this.LoadProjectButton.Text = "Load Project";
            // 
            // BrowseForProjectButton
            // 
            this.BrowseForProjectButton.Location = new System.Drawing.Point(24, 18);
            this.BrowseForProjectButton.Name = "BrowseForProjectButton";
            this.BrowseForProjectButton.Size = new System.Drawing.Size(119, 33);
            this.BrowseForProjectButton.StyleController = this.styleController1;
            this.BrowseForProjectButton.TabIndex = 3;
            this.BrowseForProjectButton.Text = "Browse For Project";
            // 
            // LoadOrCreateProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::NebulaGames.RPGWorld.IDE.Properties.Resource_En.ControlBackground1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.BrowseForProjectButton);
            this.Controls.Add(this.LoadProjectButton);
            this.Controls.Add(this.PreviousProjects);
            this.Controls.Add(this.panel1);
            this.Name = "LoadOrCreateProject";
            this.Size = new System.Drawing.Size(831, 454);
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PreviousProjects)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton NewBlankProject;
        private DevExpress.XtraEditors.StyleController styleController1;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.ListBoxControl PreviousProjects;
        private DevExpress.XtraEditors.SimpleButton LoadProjectButton;
        private DevExpress.XtraEditors.SimpleButton BrowseForProjectButton;
    }
}
