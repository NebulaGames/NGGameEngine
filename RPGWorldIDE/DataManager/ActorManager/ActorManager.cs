using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ACT.Core.Extensions;

namespace NebulaGames.RPGWorld.DataManager.ActorManager
{
    public partial class ActorManager : Form
    {
   //     List<NebulaGames.RPGWorld.Assets.ActorData> AllActorData = new List<NebulaGames.RPGWorld.Assets.ActorData>();

     //   NebulaGames.RPGWorld.Assets.ActorData _WorkingActorData;

        public ActorManager()
        {
            InitializeComponent();
            ActorsTreeList.Nodes.Add(new object[] { "", "Actors" });
        }

        private void newActorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var _Result = ACT.Core.Windows.InputBox.Show("Actor Name");
            if (_Result.ReturnCode == System.Windows.Forms.DialogResult.Cancel) { return; }

          //  _WorkingActorData = new NebulaGames.RPGWorld.Assets.ActorData();
            // CLEAR DISPLAY

         //   _WorkingActorData.Name = _Result.Text;            
        //    _WorkingActorData.BaseDirectoryPath = Constants.ActorData + _WorkingActorData.Name + "\\";
        //    _WorkingActorData.BaseDirectoryPath.CreateDirectoryStructure();
        }

        private void fromFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GenericFolderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            string FolderLocation = GenericFolderBrowserDialog.SelectedPath;
            string FolderName = GenericFolderBrowserDialog.SelectedPath.GetDirectoryName();
            string NewFolder = Constants.ActorDataDirectory;
        }

        private void AddImageButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        //    if (_WorkingActorData == null)
         //   {
          //      MessageBox.Show("No Active Actor"); return;
        //    }

            if (GenericOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) { return; }
            string _ImageName = GenericOpenFileDialog.FileName.ToLower();
            if (_ImageName.EndsWith("png") || _ImageName.EndsWith("jpg"))
            {
           //     System.IO.File.Copy(_ImageName, _WorkingActorData.BaseDirectoryPath);
            }
            else
            {
                MessageBox.Show("Invalid File Selected (PNG,JPG)"); return;
            }

        //    _WorkingActorData.Images.Add(_ImageName.GetFileNameWithoutExtension(), _ImageName.GetFileNameFromFullPath());
            // REFRESH MAIN SCREEN
       //     MainImageList.Images.Add(Image.FromFile(_WorkingActorData.BaseDirectoryPath + _ImageName.GetFileNameFromFullPath()));
        }
    }
}
