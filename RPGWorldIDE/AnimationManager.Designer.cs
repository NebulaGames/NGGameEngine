namespace NebulaGames.RPGWorld.IDE
{
    partial class AnimationManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnimationManager));
            this.AnimationFormSplitContainer = new DevExpress.XtraEditors.SplitContainerControl();
            this.AnimationTopPanelControl = new DevExpress.XtraEditors.PanelControl();
            this.MainDrawingPanelControl = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.AnimationFormSplitContainer)).BeginInit();
            this.AnimationFormSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AnimationTopPanelControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainDrawingPanelControl)).BeginInit();
            this.SuspendLayout();
            // 
            // AnimationFormSplitContainer
            // 
            this.AnimationFormSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AnimationFormSplitContainer.Location = new System.Drawing.Point(0, 120);
            this.AnimationFormSplitContainer.Name = "AnimationFormSplitContainer";
            this.AnimationFormSplitContainer.Panel1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.AnimationFormSplitContainer.Panel1.Appearance.Options.UseBackColor = true;
            this.AnimationFormSplitContainer.Panel1.Text = "Panel1";
            this.AnimationFormSplitContainer.Panel2.Appearance.BackColor = System.Drawing.Color.White;
            this.AnimationFormSplitContainer.Panel2.Appearance.Options.UseBackColor = true;
            this.AnimationFormSplitContainer.Panel2.Controls.Add(this.MainDrawingPanelControl);
            this.AnimationFormSplitContainer.Size = new System.Drawing.Size(895, 504);
            this.AnimationFormSplitContainer.SplitterPosition = 244;
            this.AnimationFormSplitContainer.TabIndex = 0;
            // 
            // AnimationTopPanelControl
            // 
            this.AnimationTopPanelControl.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.AnimationTopPanelControl.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.AnimationTopPanelControl.Appearance.Options.UseBackColor = true;
            this.AnimationTopPanelControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.AnimationTopPanelControl.Location = new System.Drawing.Point(0, 0);
            this.AnimationTopPanelControl.LookAndFeel.UseDefaultLookAndFeel = false;
            this.AnimationTopPanelControl.Name = "AnimationTopPanelControl";
            this.AnimationTopPanelControl.Size = new System.Drawing.Size(895, 120);
            this.AnimationTopPanelControl.TabIndex = 1;
            // 
            // MainDrawingPanelControl
            // 
            this.MainDrawingPanelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainDrawingPanelControl.Location = new System.Drawing.Point(0, 0);
            this.MainDrawingPanelControl.Name = "MainDrawingPanelControl";
            this.MainDrawingPanelControl.Size = new System.Drawing.Size(639, 504);
            this.MainDrawingPanelControl.TabIndex = 0;
            this.MainDrawingPanelControl.Resize += new System.EventHandler(this.MainDrawingPanelControl_Resize);
            // 
            // AnimationManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 624);
            this.Controls.Add(this.AnimationFormSplitContainer);
            this.Controls.Add(this.AnimationTopPanelControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AnimationManager";
            this.Text = "Animation Manager";
            ((System.ComponentModel.ISupportInitialize)(this.AnimationFormSplitContainer)).EndInit();
            this.AnimationFormSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AnimationTopPanelControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainDrawingPanelControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl AnimationFormSplitContainer;
        private DevExpress.XtraEditors.PanelControl AnimationTopPanelControl;
        private DevExpress.XtraEditors.PanelControl MainDrawingPanelControl;
    }
}