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
    public partial class ProgressWindow : Form
    {
        public ProgressWindow()
        {
            InitializeComponent();
        }

        public void SetProgress(int P)
        {
            MainProgressBar.EditValue = P;
            MainProgressBar.Refresh();
        }
    }
}
