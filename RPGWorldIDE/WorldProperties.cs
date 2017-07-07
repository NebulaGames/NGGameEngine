using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NebulaGames.RPGWorld.GameBuilder;

namespace NebulaGames.RPGWorld
{
    public partial class WorldProperties : Form
    {
        public EditorWorld WorldInfo;

        public WorldProperties(EditorWorld _WorldInfo)
        {
            InitializeComponent();
            WorldInfo = _WorldInfo;
            WorldName.Text = _WorldInfo.WorldName;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            WorldInfo.WorldName = WorldName.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
