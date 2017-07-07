using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NebulaGames.RPGWorld.Graphics;

namespace NebulaGames.RPGWorld
{
    public partial class TestWorldGenerator : Form
    {
        public TestWorldGenerator()
        {
            InitializeComponent();
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            Point RandomPoint = new Point();

            Random _R = new Random();

            RandomPoint.X = _R.Next(WorldBox.Width);
            RandomPoint.Y = _R.Next(WorldBox.Height);

            FastPixel _FP = new FastPixel(new Bitmap(WorldBox.Width, WorldBox.Height));

            List<Point> DonePoints = new List<Point>();
            List<Point> ToDoPoints = new List<Point>();

            _FP.SetPixel(RandomPoint, Color.Blue);




        }
    }
}
