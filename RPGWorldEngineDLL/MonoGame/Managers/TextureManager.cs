
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.IO;
using NebulaGames.RPGWorld.MonoGame.Extensions;
using ACT.Core.Extensions;


namespace NebulaGames.RPGWorld.MonoGame.Managers
{
    public class TextureManager : GameComponent
    {

        public World _WorldRef;

        public void ManageBackGroundImages()
        {

        }

        public TextureManager(Game XnaGame)
            : base(XnaGame)
        {
            XnaGame.Components.Add(this);
            foreach (var c in this.Game.Components)
            {
                if (c is World)
                {
                    _WorldRef = (World)c;
                }
            }

            // Load All Other Textures

            foreach (string x in System.IO.Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "Data\\textures\\"))
            {
                if (x.ToLower().EndsWith(".png"))
                {
                    LoadTexture(x.GetFileName(false), x);
                }
            }
        }

        public bool HasTexture(string Value)
        {

            return _Textures.ContainsKey(Value);
            
        }
        private Dictionary<string, Texture2D> _Textures = new Dictionary<string, Texture2D>();

        public void LoadTexture(string Name, Stream InputStream)
        {
            if (_Textures.ContainsKey(Name)) { ErrorLogable.LogError(null, "Duplicate Load Texture: " + Name); return; }

            _Textures.Add(Name, Texture2D.FromStream(this.Game.GraphicsDevice, InputStream));
        }

        public void LoadTexture(string Name, string Location)
        {
            // Return Out (Log Duplicate Effort)
            if (_Textures.ContainsKey(Name)) { ErrorLogable.LogError(null, "Duplicate Load Texture: " + Name); return; }

            if (System.IO.File.Exists(Location + Name + ".png"))
            {
               // System.IO.FileStream _tmp = new FileStream("asdasd",  FileMode.Open);

                _Textures.Add(Name, Texture2D.FromStream(this.Game.GraphicsDevice, System.IO.File.Open(Location + Name + ".png", FileMode.Open)));
            }
            else if (System.IO.File.Exists(Location))
            {
                _Textures.Add(Name, Texture2D.FromStream(this.Game.GraphicsDevice, System.IO.File.Open(Location, FileMode.Open)));
            }
            //_Textures.Add(Name, this.Game.Content.Load<Texture2D>(Name));
        }

        public Texture2D GetTexture(string Name)
        {
            if (_Textures.ContainsKey(Name)) { return _Textures[Name]; }
            else
            {
                ErrorLogable.LogError(null, "Texture Not Found: " + Name);
                throw new Exception("Texture Not Found");
            }

        }


    }
}
