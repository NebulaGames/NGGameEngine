using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NebulaGames.RPGWorld.Dialogs
{
    public partial class LayerInfo : Form
    {
        NebulaGames.RPGWorld.GameBuilder.EditorWorld WorldRef;
        public NebulaGames.RPGWorld.GameBuilder.LayerInfo LayerInformationResult;
        bool _IsNew = false;

        public LayerInfo()
        {
            InitializeComponent();
        }

        private void LayerInfo_Load(object sender, EventArgs e)
        {

        }

        public DialogResult Show(NebulaGames.RPGWorld.GameBuilder.EditorWorld CurrentWorld)
        {
            WorldRef = CurrentWorld;
            LayerName.Text = "";
            LayerType.SelectedIndex = -1;
            IsVisible.Checked = false;
            _IsNew = true;
            return this.ShowDialog();
        }

        public DialogResult Show(NebulaGames.RPGWorld.GameBuilder.EditorWorld CurrentWorld, NebulaGames.RPGWorld.GameBuilder.LayerInfo CurrentLayerInfo)
        {
            WorldRef = CurrentWorld;
            LayerName.Text = CurrentLayerInfo.Name;
            LayerType.SelectedIndex = LayerType.Properties.Items.GetItemIndexByValue(CurrentLayerInfo.LayerType.ToString());
            IsVisible.Checked = CurrentLayerInfo.Visible;
            _IsNew = false;
            return this.ShowDialog();
        }
        private void SaveLayerButton_Click(object sender, EventArgs e)
        {
            if (_IsNew)
            {
                if (WorldRef.Layers.ContainsKey(LayerName.Text.ToLower()))
                {
                    MessageBox.Show("Please Enter A Unique Layer Name");
                    return;
                }
            }

            LayerInformationResult = new NebulaGames.RPGWorld.GameBuilder.LayerInfo();

            LayerInformationResult.Name = LayerName.Text;
            LayerInformationResult.LayerType = (NebulaGames.RPGWorld.Enumerations.LayerType)Enum.Parse(typeof(NebulaGames.RPGWorld.Enumerations.LayerType), LayerType.EditValue.ToString());
            LayerInformationResult.Visible = IsVisible.Checked;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
