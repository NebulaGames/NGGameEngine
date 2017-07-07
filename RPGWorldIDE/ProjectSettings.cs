using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NebulaGames.RPGWorld
{
    public partial class ProjectSettings : Form
    {
        public ProjectSettings()
        {
            InitializeComponent();
            this.Shown += ProjectSettings_Shown;
            this.ResizeEnd += ProjectSettings_ResizeEnd;
        }

        void ProjectSettings_ResizeEnd(object sender, EventArgs e)
        {
            DataDirectory.Width = DataDirectory.Parent.Width - 5;           
        }

        private void ProjectSettings_Shown(Object sender, EventArgs e)
        {
            DataDirectory.Width = DataDirectory.Parent.Width - 5;           

        }
    }
}
