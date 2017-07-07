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
using NebulaGames.RPGWorld.MonoGame.Managers;

namespace NebulaGames.RPGWorld.MonoGame.Windows.DynamicTest
{
    public class FPS : DrawableWindowComponent
    {
        private SpriteFont VideoFont;
        private float _ElapsedTime, _TotalFrames, _Fps;
        private bool _ShowFPS;
        private string _FontName = "MainFont";
        private FontManager _FontManager;

        public FPS(Game XnaGame, Window Parent)
        {
            this.XnaGame = XnaGame;
            this.ParentWindow = Parent;
            this.WindowBatch = Parent.WindowManager.WindowBatch;

            foreach (var t in this._XnaGame.Components)
            {
                if (t is FontManager)
                {
                    _FontManager = (FontManager)t;
                }
            }

            VideoFont = _FontManager.GetFont(_FontName);
        }

        public void Draw(GameTime gameTime)
        {
            this.WindowBatch.Begin();
            
            this.WindowBatch.DrawString(VideoFont,
                "FPS=" + _Fps.ToString(),
                new Vector2(0, this.XnaGame.GraphicsDevice.Viewport.Height - VideoFont.LineSpacing),
                Color.Red,
                0f,
                Vector2.Zero,
                1.0f,
                SpriteEffects.None,
                0);
            this.WindowBatch.End();
        }

        public int DrawOrder
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler<EventArgs> DrawOrderChanged;

        public bool Visible
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler<EventArgs> VisibleChanged;

        public bool Enabled
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler<EventArgs> EnabledChanged;

        public void Update(GameTime gameTime)
        {
            _ElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _TotalFrames++;

            if (_ElapsedTime >= 1.0f)
            {
                _Fps = _TotalFrames;
                _TotalFrames = 0;
                _ElapsedTime = 0;
            }
        }

        public int UpdateOrder
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler<EventArgs> UpdateOrderChanged;

        Window _Parent;
        public Window ParentWindow
        {
            get
            {
                return _Parent;
            }
            set
            {
                _Parent = value;
            }
        }
        SpriteBatch _WindowBatch;
        public SpriteBatch WindowBatch
        {
            get
            {
                return _WindowBatch;
            }
            set
            {
                _WindowBatch = value;
            }
        }
        private Game _XnaGame;
        public Game XnaGame
        {
            get
            {
                return _XnaGame;
            }
            set
            {
                _XnaGame = value;
            }
        }
    }
}
