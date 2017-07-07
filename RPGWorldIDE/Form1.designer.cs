namespace NebulaGames.RPGWorld

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.LoadedBitmapImages = new DevExpress.XtraEditors.ListBoxControl();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.LoadDirectoryButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.FinalizedData = new DevExpress.XtraTreeList.TreeList();
            this.NodeName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.AddNodeButton = new DevExpress.XtraEditors.SimpleButton();
            this.TransferButton = new System.Windows.Forms.Button();
            this.CreateSpriteDefinition = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.LoadedBitmapImages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FinalizedData)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadedBitmapImages
            // 
            this.LoadedBitmapImages.Location = new System.Drawing.Point(12, 60);
            this.LoadedBitmapImages.Name = "LoadedBitmapImages";
            this.LoadedBitmapImages.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.LoadedBitmapImages.Size = new System.Drawing.Size(190, 306);
            this.LoadedBitmapImages.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // LoadDirectoryButton
            // 
            this.LoadDirectoryButton.Location = new System.Drawing.Point(12, 31);
            this.LoadDirectoryButton.Name = "LoadDirectoryButton";
            this.LoadDirectoryButton.Size = new System.Drawing.Size(100, 23);
            this.LoadDirectoryButton.TabIndex = 1;
            this.LoadDirectoryButton.Text = "Load Directory";
            this.LoadDirectoryButton.UseVisualStyleBackColor = true;
            this.LoadDirectoryButton.Click += new System.EventHandler(this.LoadDirectoryButton_Click);
            // 
            // FinalizedData
            // 
            this.FinalizedData.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.NodeName});
            this.FinalizedData.Location = new System.Drawing.Point(243, 60);
            this.FinalizedData.Name = "FinalizedData";
            this.FinalizedData.OptionsBehavior.DragNodes = true;
            this.FinalizedData.Size = new System.Drawing.Size(271, 306);
            this.FinalizedData.TabIndex = 3;
            // 
            // NodeName
            // 
            this.NodeName.Caption = "Name";
            this.NodeName.FieldName = "Name";
            this.NodeName.Name = "NodeName";
            this.NodeName.Visible = true;
            this.NodeName.VisibleIndex = 0;
            // 
            // AddNodeButton
            // 
            this.AddNodeButton.Location = new System.Drawing.Point(243, 31);
            this.AddNodeButton.Name = "AddNodeButton";
            this.AddNodeButton.Size = new System.Drawing.Size(75, 23);
            this.AddNodeButton.TabIndex = 4;
            this.AddNodeButton.Text = "Add Node";
            this.AddNodeButton.Click += new System.EventHandler(this.AddNodeButton_Click);
            // 
            // TransferButton
            // 
            this.TransferButton.AutoSize = true;
            this.TransferButton.BackColor = System.Drawing.Color.White;
            this.TransferButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TransferButton.BackgroundImage")));
            this.TransferButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TransferButton.FlatAppearance.BorderSize = 0;
            this.TransferButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.TransferButton.Location = new System.Drawing.Point(209, 60);
            this.TransferButton.Name = "TransferButton";
            this.TransferButton.Size = new System.Drawing.Size(27, 202);
            this.TransferButton.TabIndex = 2;
            this.TransferButton.UseVisualStyleBackColor = false;
            this.TransferButton.Click += new System.EventHandler(this.TransferButton_Click);
            // 
            // CreateSpriteDefinition
            // 
            this.CreateSpriteDefinition.Location = new System.Drawing.Point(387, 31);
            this.CreateSpriteDefinition.Name = "CreateSpriteDefinition";
            this.CreateSpriteDefinition.Size = new System.Drawing.Size(126, 23);
            this.CreateSpriteDefinition.TabIndex = 5;
            this.CreateSpriteDefinition.Text = "Create Sprite Definition";
            this.CreateSpriteDefinition.Click += new System.EventHandler(this.CreateSpriteDefinition_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(525, 378);
            this.Controls.Add(this.CreateSpriteDefinition);
            this.Controls.Add(this.AddNodeButton);
            this.Controls.Add(this.FinalizedData);
            this.Controls.Add(this.TransferButton);
            this.Controls.Add(this.LoadDirectoryButton);
            this.Controls.Add(this.LoadedBitmapImages);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.LoadedBitmapImages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FinalizedData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ListBoxControl LoadedBitmapImages;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button LoadDirectoryButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private DevExpress.XtraTreeList.TreeList FinalizedData;
        private DevExpress.XtraTreeList.Columns.TreeListColumn NodeName;
        private DevExpress.XtraEditors.SimpleButton AddNodeButton;
        private System.Windows.Forms.Button TransferButton;
        private DevExpress.XtraEditors.SimpleButton CreateSpriteDefinition;
    }
}

