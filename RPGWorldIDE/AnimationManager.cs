using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SysG = System.Drawing;

namespace NebulaGames.RPGWorld.IDE
{
    public partial class AnimationManager : Form
    {
        SysG.Graphics _MainGraphicsObject;
        Rectangle _MainPanelRect = new Rectangle();
        Point _CenterPoint = new Point();
        Timer _MainDrawTimer = new Timer();
        public AnimationManager()
        {
            InitializeComponent();

            _MainGraphicsObject = MainDrawingPanelControl.CreateGraphics();
            _MainPanelRect = new Rectangle(0, 0, MainDrawingPanelControl.Width, MainDrawingPanelControl.Height);
            _MainDrawTimer.Interval = 16;
            _MainDrawTimer.Tick += _MainDrawTimer_Tick;
            _MainDrawTimer.Enabled = true;            
        }

        private void _MainDrawTimer_Tick(object sender, EventArgs e)
        {
            DrawCurrentAnimation();

        }

        internal void DrawCrossHairs()
        {
            _MainGraphicsObject.Clear(Color.White);
            _MainGraphicsObject.DrawLine(Pens.Black, _MainPanelRect.Left, _MainPanelRect.Top, _MainPanelRect.Right, _MainPanelRect.Bottom);
            _MainGraphicsObject.DrawLine(Pens.Black, _MainPanelRect.Right, _MainPanelRect.Top, _MainPanelRect.Left, _MainPanelRect.Bottom);
        }

        internal void DrawCurrentAnimation()
        {
            DrawCrossHairs();

        }

        private void MainDrawingPanelControl_Resize(object sender, EventArgs e)
        {
            _MainPanelRect = new Rectangle(0, 0, MainDrawingPanelControl.Width, MainDrawingPanelControl.Height);
            DrawCurrentAnimation();
        }
    }
}
