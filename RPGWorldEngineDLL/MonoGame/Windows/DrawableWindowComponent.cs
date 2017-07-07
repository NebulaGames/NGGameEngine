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
using NebulaGames.RPGWorld.MonoGame.Extensions;
using System.Xml.XPath;

namespace NebulaGames.RPGWorld.MonoGame.Windows
{
    public interface DrawableWindowComponent : IDrawable, IUpdateable
    {
         Window ParentWindow { get; set;}
         SpriteBatch WindowBatch { get; set; }
         Game XnaGame { get; set; }
    }
}
