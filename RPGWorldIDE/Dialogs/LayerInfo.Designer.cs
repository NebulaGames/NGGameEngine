namespace NebulaGames.RPGWorld.Dialogs
{
    partial class LayerInfo
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.LayerName = new DevExpress.XtraEditors.TextEdit();
            this.LayerType = new DevExpress.XtraEditors.RadioGroup();
            this.IsVisible = new DevExpress.XtraEditors.CheckEdit();
            this.TypeOfLayerLbl = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.MainIDELookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.SaveLayerButton = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.LayerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LayerType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsVisible.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(27, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Name";
            // 
            // LayerName
            // 
            this.LayerName.Location = new System.Drawing.Point(46, 10);
            this.LayerName.Name = "LayerName";
            this.LayerName.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LayerName.Size = new System.Drawing.Size(161, 20);
            this.LayerName.TabIndex = 1;
            // 
            // LayerType
            // 
            this.LayerType.Location = new System.Drawing.Point(46, 36);
            this.LayerType.Name = "LayerType";
            this.LayerType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Background", "Background"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Passable", "Passable"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Objects", "Objects"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("BoundingBox", "BoundingBox"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Triggers", "Triggers")});
            this.LayerType.Size = new System.Drawing.Size(161, 96);
            this.LayerType.TabIndex = 2;
            // 
            // IsVisible
            // 
            this.IsVisible.Location = new System.Drawing.Point(46, 140);
            this.IsVisible.Name = "IsVisible";
            this.IsVisible.Properties.Caption = "";
            this.IsVisible.Size = new System.Drawing.Size(75, 19);
            this.IsVisible.TabIndex = 3;
            // 
            // TypeOfLayerLbl
            // 
            this.TypeOfLayerLbl.Location = new System.Drawing.Point(16, 36);
            this.TypeOfLayerLbl.Name = "TypeOfLayerLbl";
            this.TypeOfLayerLbl.Size = new System.Drawing.Size(24, 13);
            this.TypeOfLayerLbl.TabIndex = 4;
            this.TypeOfLayerLbl.Text = "Type";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(11, 143);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(29, 13);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Visible";
            // 
            // MainIDELookAndFeel
            // 
            this.MainIDELookAndFeel.LookAndFeel.SkinName = "Visual Studio 2013 Dark";
            //this.MainIDELookAndFeel.LookAndFeel..TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.False;
            // 
            // SaveLayerButton
            // 
            this.SaveLayerButton.Location = new System.Drawing.Point(82, 165);
            this.SaveLayerButton.Name = "SaveLayerButton";
            this.SaveLayerButton.Size = new System.Drawing.Size(75, 23);
            this.SaveLayerButton.TabIndex = 6;
            this.SaveLayerButton.Text = "Save Layer";
            this.SaveLayerButton.Click += new System.EventHandler(this.SaveLayerButton_Click);
            // 
            // LayerInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(216, 196);
            this.Controls.Add(this.SaveLayerButton);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.TypeOfLayerLbl);
            this.Controls.Add(this.IsVisible);
            this.Controls.Add(this.LayerType);
            this.Controls.Add(this.LayerName);
            this.Controls.Add(this.labelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LayerInfo";
            this.Text = "Layer Info";
            this.Load += new System.EventHandler(this.LayerInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LayerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LayerType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsVisible.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit LayerName;
        private DevExpress.XtraEditors.RadioGroup LayerType;
        private DevExpress.XtraEditors.CheckEdit IsVisible;
        private DevExpress.XtraEditors.LabelControl TypeOfLayerLbl;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.LookAndFeel.DefaultLookAndFeel MainIDELookAndFeel;
        private DevExpress.XtraEditors.SimpleButton SaveLayerButton;
    }
}