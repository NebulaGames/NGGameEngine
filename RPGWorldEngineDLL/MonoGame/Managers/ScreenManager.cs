using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace NebulaGames.RPGWorld.MonoGame.Managers
{
    public class ScreenManager : GameComponent
    {

        TextureManager _TextureManagerRef;

        public ScreenManager(Game XnaGame)
            : base(XnaGame)
        {
            this.Game.Components.Add(this);

              foreach (var x in XnaGame.Components)
            {
                if (x is TextureManager)
                {
                    _TextureManagerRef = (TextureManager)x;
                    break;
                }               
            }

            
        }

        private Dictionary<string, Screen> _Screens = new Dictionary<string, Screen>();

        public Texture2D GetScreenBackGround(Point P)
        {
           return _TextureManagerRef.GetTexture(World.GetKey(P.X, P.Y, Enumerations.RelativePosition.Main));
        }

        public Dictionary<string, Screen> Screens
        {
            get { return _Screens; }
        }

    }
}
