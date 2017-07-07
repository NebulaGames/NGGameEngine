namespace NebulaGames.RPGWorld
{
    partial class SpriteDefinition
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
            this.SpriteName = new DevExpress.XtraEditors.TextEdit();
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.ActionName = new DevExpress.XtraEditors.TextEdit();
            this.SpriteActions = new DevExpress.XtraEditors.ListBoxControl();
            this.RemoveSpriteAction = new DevExpress.XtraEditors.SimpleButton();
            this.AddActionButton = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.FrameDelay = new DevExpress.XtraEditors.TextEdit();
            this.DoneButton = new DevExpress.XtraEditors.SimpleButton();
            this.CancelButton = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.SpriteName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpriteActions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrameDelay.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // SpriteName
            // 
            this.SpriteName.Location = new System.Drawing.Point(76, 9);
            this.SpriteName.Name = "SpriteName";
            this.SpriteName.Size = new System.Drawing.Size(271, 20);
            this.SpriteName.StyleController = this.styleController1;
            this.SpriteName.TabIndex = 0;
            // 
            // styleController1
            // 
            this.styleController1.LookAndFeel.SkinName = "Office 2010 Silver";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 13);
            this.labelControl1.StyleController = this.styleController1;
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Sprite Name";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 38);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(52, 13);
            this.labelControl2.StyleController = this.styleController1;
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Add Action";
            // 
            // ActionName
            // 
            this.ActionName.Location = new System.Drawing.Point(76, 35);
            this.ActionName.Name = "ActionName";
            this.ActionName.Size = new System.Drawing.Size(105, 20);
            this.ActionName.StyleController = this.styleController1;
            this.ActionName.TabIndex = 2;
            // 
            // SpriteActions
            // 
            this.SpriteActions.Location = new System.Drawing.Point(187, 35);
            this.SpriteActions.Name = "SpriteActions";
            this.SpriteActions.Size = new System.Drawing.Size(160, 95);
            this.SpriteActions.StyleController = this.styleController1;
            this.SpriteActions.TabIndex = 4;
            // 
            // RemoveSpriteAction
            // 
            this.RemoveSpriteAction.Location = new System.Drawing.Point(272, 137);
            this.RemoveSpriteAction.Name = "RemoveSpriteAction";
            this.RemoveSpriteAction.Size = new System.Drawing.Size(75, 23);
            this.RemoveSpriteAction.StyleController = this.styleController1;
            this.RemoveSpriteAction.TabIndex = 5;
            this.RemoveSpriteAction.Text = "Remove";
            // 
            // AddActionButton
            // 
            this.AddActionButton.Location = new System.Drawing.Point(117, 61);
            this.AddActionButton.Name = "AddActionButton";
            this.AddActionButton.Size = new System.Drawing.Size(64, 23);
            this.AddActionButton.StyleController = this.styleController1;
            this.AddActionButton.TabIndex = 6;
            this.AddActionButton.Text = "Add Action";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 143);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 13);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "Frame Delay";
            // 
            // FrameDelay
            // 
            this.FrameDelay.Location = new System.Drawing.Point(76, 140);
            this.FrameDelay.Name = "FrameDelay";
            this.FrameDelay.Size = new System.Drawing.Size(105, 20);
            this.FrameDelay.StyleController = this.styleController1;
            this.FrameDelay.TabIndex = 7;
            // 
            // DoneButton
            // 
            this.DoneButton.Location = new System.Drawing.Point(106, 176);
            this.DoneButton.Name = "DoneButton";
            this.DoneButton.Size = new System.Drawing.Size(75, 23);
            this.DoneButton.StyleController = this.styleController1;
            this.DoneButton.TabIndex = 9;
            this.DoneButton.Text = "Done";
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(187, 176);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.StyleController = this.styleController1;
            this.CancelButton.TabIndex = 10;
            this.CancelButton.Text = "Cancel";
            // 
            // SpriteDefinition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 211);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.DoneButton);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.FrameDelay);
            this.Controls.Add(this.AddActionButton);
            this.Controls.Add(this.RemoveSpriteAction);
            this.Controls.Add(this.SpriteActions);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.ActionName);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.SpriteName);
            this.Name = "SpriteDefinition";
            this.Text = "SpriteDefinition";
            ((System.ComponentModel.ISupportInitialize)(this.SpriteName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpriteActions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FrameDelay.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit SpriteName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit ActionName;
        private DevExpress.XtraEditors.ListBoxControl SpriteActions;
        private DevExpress.XtraEditors.SimpleButton RemoveSpriteAction;
        private DevExpress.XtraEditors.SimpleButton AddActionButton;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit FrameDelay;
        private DevExpress.XtraEditors.SimpleButton DoneButton;
        private DevExpress.XtraEditors.SimpleButton CancelButton;
        private DevExpress.XtraEditors.StyleController styleController1;
    }
}