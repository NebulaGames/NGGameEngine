namespace NebulaGames.RPGWorld
{
    partial class WorldProperties
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
            this.WorldName = new DevExpress.XtraEditors.TextEdit();
            this.Save = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.WorldName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // WorldName
            // 
            this.WorldName.Location = new System.Drawing.Point(13, 26);
            this.WorldName.Name = "WorldName";
            this.WorldName.Size = new System.Drawing.Size(231, 20);
            this.WorldName.TabIndex = 0;
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(169, 102);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 1;
            this.Save.Text = "Save";
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "World Name";
            // 
            // WorldProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 137);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.WorldName);
            this.Name = "WorldProperties";
            this.Text = "WorldProperties";
            ((System.ComponentModel.ISupportInitialize)(this.WorldName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit WorldName;
        private DevExpress.XtraEditors.SimpleButton Save;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}