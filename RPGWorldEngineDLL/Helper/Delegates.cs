using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace NebulaGames.RPGWorld.Delegates
{
    public struct MouseEventArgs
    {
        public GameTime gameTime;
        public Rectangle SelectedArea;        
        public MouseState mouseState;
        public Point LastClickLocation;
    }

    public delegate void MouseSelectedAreaChanged(object sender, MouseEventArgs e);
    public delegate void MouseRightClick(object sender, MouseEventArgs e);
}
