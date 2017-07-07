namespace NebulaGames.RPGWorld
{
    partial class AreaSelector
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
            this.label1 = new System.Windows.Forms.Label();
            this.LeftScreens = new System.Windows.Forms.TextBox();
            this.RightScreens = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.UpScreens = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.DownScreens = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ExecuteButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Left Screens";
            // 
            // LeftScreens
            // 
            this.LeftScreens.Location = new System.Drawing.Point(97, 17);
            this.LeftScreens.Name = "LeftScreens";
            this.LeftScreens.Size = new System.Drawing.Size(57, 20);
            this.LeftScreens.TabIndex = 1;
            this.LeftScreens.Text = "2";
            // 
            // RightScreens
            // 
            this.RightScreens.Location = new System.Drawing.Point(97, 43);
            this.RightScreens.Name = "RightScreens";
            this.RightScreens.Size = new System.Drawing.Size(57, 20);
            this.RightScreens.TabIndex = 3;
            this.RightScreens.Text = "2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Right Screens";
            // 
            // UpScreens
            // 
            this.UpScreens.Location = new System.Drawing.Point(97, 69);
            this.UpScreens.Name = "UpScreens";
            this.UpScreens.Size = new System.Drawing.Size(57, 20);
            this.UpScreens.TabIndex = 5;
            this.UpScreens.Text = "2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Up Screens";
            // 
            // DownScreens
            // 
            this.DownScreens.Location = new System.Drawing.Point(97, 95);
            this.DownScreens.Name = "DownScreens";
            this.DownScreens.Size = new System.Drawing.Size(57, 20);
            this.DownScreens.TabIndex = 7;
            this.DownScreens.Text = "2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Down Screens";
            // 
            // ExecuteButton
            // 
            this.ExecuteButton.Location = new System.Drawing.Point(41, 129);
            this.ExecuteButton.Name = "ExecuteButton";
            this.ExecuteButton.Size = new System.Drawing.Size(75, 23);
            this.ExecuteButton.TabIndex = 8;
            this.ExecuteButton.Text = "Execute";
            this.ExecuteButton.UseVisualStyleBackColor = true;
            this.ExecuteButton.Click += new System.EventHandler(this.ExecuteButton_Click);
            // 
            // AreaSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(165, 164);
            this.Controls.Add(this.ExecuteButton);
            this.Controls.Add(this.DownScreens);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.UpScreens);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.RightScreens);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LeftScreens);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AreaSelector";
            this.Text = "Area";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AreaSelector_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button ExecuteButton;
        public System.Windows.Forms.TextBox LeftScreens;
        public System.Windows.Forms.TextBox RightScreens;
        public System.Windows.Forms.TextBox UpScreens;
        public System.Windows.Forms.TextBox DownScreens;
    }
}