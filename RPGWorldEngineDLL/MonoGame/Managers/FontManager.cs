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

namespace NebulaGames.RPGWorld.MonoGame.Managers
{
    public class FontManager : GameComponent
    {        
        public Dictionary<string, SpriteFont> _Fonts = new Dictionary<string, SpriteFont>();
        private SettingsManager _SettingsManager;

        public FontManager(Game XnaGame) : base(XnaGame)
        {
            this.Game.Components.Add(this);

            foreach (var t in this.Game.Components)
            {
                if (t is SettingsManager) { _SettingsManager = (SettingsManager)t; }
            }

            foreach (string _D in _SettingsManager.Fonts.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {                
                _Fonts.Add(_D, this.Game.Content.Load<SpriteFont>(_D));
            }
        }

        public SpriteFont GetFont(string Name)
        {
            return _Fonts[Name];
        }
    }
}
