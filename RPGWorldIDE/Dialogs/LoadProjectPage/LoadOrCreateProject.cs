using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NebulaGames.RPGWorld
{
    public partial class LoadOrCreateProject : UserControl
    {
        List<dynamic> ProjectTypes = new List<dynamic>();

        public LoadOrCreateProject()
        {
            InitializeComponent();


        }

        private void NewBlankProject_Click(object sender, EventArgs e)
        {

        }
    }
}
