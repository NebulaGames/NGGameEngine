using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NebulaGames.RPGWorld.Dialogs.LoadProjectPage
{
    public partial class LoadOrCreateProjectDialog : Form
    {
        public LoadOrCreateProjectDialog()
        {
            InitializeComponent();

            var _ProjectTypes = NebulaGames.RPGWorld.IDE.SpriteBuilder_Support.LoadListOfConfigurationFiles(AppDomain.CurrentDomain.BaseDirectory + "Data\\Configuration\\ProjectTemplates\\");
        
        
        }

        private void loadOrCreateProject1_Load(object sender, EventArgs e)
        {

        }
    }
}
