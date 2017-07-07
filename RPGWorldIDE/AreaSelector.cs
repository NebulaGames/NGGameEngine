using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NebulaGames.RPGWorld
{
    public partial class AreaSelector : Form
    {
        public AreaSelector()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void ExecuteButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void AreaSelector_FormClosing(object sender, FormClosingEventArgs e)
        {
          
        }
    }
}
