using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace NebulaGames.RPGWorld
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            FinalizedData.Nodes.Add(new string[] { "Root" });
        }

        private void LoadDirectoryButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string File in System.IO.Directory.GetFiles(folderBrowserDialog1.SelectedPath))
                {
                    LoadedBitmapImages.Items.Add(File.Substring(File.LastIndexOf("\\") + 1) + " - " + File);
                }
            }


        }

        private void TransferButton_Click(object sender, EventArgs e)
        {
            if (FinalizedData.Selection[0].Level == 2)
            {

            tryagain:
                var IBR = InputBox.Show("Enter A Node Name");

                if (IBR.ReturnCode == System.Windows.Forms.DialogResult.OK)
                {
                    if (FinalizedData.FindNodeByFieldValue("Name", IBR.Text) != null)
                    {
                        MessageBox.Show("Node Found.  Please Enter A New Name");
                        goto tryagain;
                    }
                }

                //FinalizedData.n
            }
        }

        private void AddNodeButton_Click(object sender, EventArgs e)
        {
            var IBR = InputBox.Show("Enter A Node Name");

            if (IBR.ReturnCode == System.Windows.Forms.DialogResult.OK)
            {
                FinalizedData.Selection[0].Nodes.Add(new string[] { IBR.Text });
            }



        }

        private void LoadedBitmapImages_MouseMove(object sender, MouseEventArgs e)
        {

        }
        public enum Direction
        {
            Up,
            UpRight,
            Right,
            RightDown,
            Down,
            DownLeft,
            Left,
            UpLeft
        }

        private void CreateSpriteDefinition_Click(object sender, EventArgs e)
        {
            //SpriteDefinitionDX _S = new SpriteDefinitionDX();
            //if (_S.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) { return; }

            //string _ObjName = _S.SpriteName.Text;

            //var RootNode = FinalizedData.FindNodeByFieldValue("Name", "Root");

            //var ObjNode = RootNode.Nodes.Add(new string[] { _ObjName });

            //foreach (var s in _S.SpriteActions.Items)
            //{
            //   var ActionNode = ObjNode.Nodes.Add(new string[] { s.ToString() });

            //   for (int x = 0; x < 8; x++)
            //   {

            //   }
            //}

        }
    }
}
