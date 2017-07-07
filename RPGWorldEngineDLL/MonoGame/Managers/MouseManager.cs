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
using ACT.Core.Extensions;
using System.Xml.XPath;
using NebulaGames.RPGWorld.Delegates;
using NebulaGames.RPGWorld.Graphics;
namespace NebulaGames.RPGWorld.MonoGame.Managers
{
    public class MouseManager : DrawableGameComponent
    {
        public TextureManager _TextureManager;
        public bool AllowSelect = true;
        public bool LeftMouseDown = false;
        public Point LeftMousePoint;
        public Point LastClickPoint;
        public Point CurrentHoverPoint;
        public SpriteBatch _LocalBatch;

        public Rectangle SelectedScreenArea;
        public MouseManager(Game XnaGame)
            : base(XnaGame)
        {
            this.Game.Components.Add(this);
            this.DrawOrder = 20;
            _LocalBatch = new SpriteBatch(this.Game.GraphicsDevice);

            var ResultsLinq = from x in this.Game.Components where x is TextureManager select x;
            _TextureManager = (TextureManager)ResultsLinq.First();
        }

        public override void Update(GameTime gameTime)
        {
            MouseState _CurrentState = Mouse.GetState();
            #region Left
            if (LeftMouseDown)
            {
                if (_CurrentState.LeftButton == ButtonState.Released)
                {
                    _Selecting = false;
                    LeftMouseDown = false;
                    LastClickPoint = new Point(_CurrentState.X, _CurrentState.Y);
                    if (AllowSelect)
                    {
                        SelectedScreenArea = SelectedScreenArea.FromTwoPoints(LeftMousePoint, LastClickPoint);
                        if (OnMouseSelectedAreaChange != null)
                        {
                            MouseEventArgs _Mev = new MouseEventArgs();
                            _Mev.mouseState = _CurrentState;
                            _Mev.SelectedArea = SelectedScreenArea;
                            _Mev.LastClickLocation = LastClickPoint;
                            _Mev.gameTime = gameTime;
                            OnMouseSelectedAreaChange(this, _Mev);
                        }
                    }

                }
                else
                {
                    CurrentHoverPoint = new Point(_CurrentState.X, _CurrentState.Y);

                    if (_Selecting || SelectedScreenArea.Difference(SelectedScreenArea.FromTwoPoints(LeftMousePoint, CurrentHoverPoint)) > 50)
                    {
                        SelectedScreenArea = SelectedScreenArea.FromTwoPoints(LeftMousePoint, CurrentHoverPoint);
                        _Selecting = true;
                        if (OnMouseSelectedAreaChange != null)
                        {
                            MouseEventArgs _Mev = new MouseEventArgs();
                            _Mev.mouseState = _CurrentState;
                            _Mev.SelectedArea = SelectedScreenArea;
                            _Mev.LastClickLocation = LastClickPoint;
                            _Mev.gameTime = gameTime;
                            OnMouseSelectedAreaChange(this, _Mev);
                        }
                    }
                }
            }
            else
            {
                if (_CurrentState.LeftButton == ButtonState.Pressed)
                {
                    LeftMouseDown = true;

                    LeftMousePoint = new Point(_CurrentState.X, _CurrentState.Y);
                    SelectedScreenArea = SelectedScreenArea.FromTwoPoints(LeftMousePoint, LeftMousePoint);
                }
            }
            #endregion

            #region Right
            if (_RightMouseDown)
            {
                if (_CurrentState.RightButton == ButtonState.Released)
                {
                    if (OnMouseRightClick != null)
                    {
                        _RightMouseDown = false;
                        MouseEventArgs _Mev = new MouseEventArgs();
                        _Mev.mouseState = _CurrentState;
                        _Mev.SelectedArea = SelectedScreenArea;
                        _Mev.LastClickLocation = LastClickPoint;
                        _Mev.gameTime = gameTime;
                        OnMouseRightClick(this, _Mev);
                    }
                }
            }
            else
            {
                if (_CurrentState.RightButton == ButtonState.Pressed)
                {
                    _RightMouseDown = true;
                }

            }


            #endregion

            base.Update(gameTime);
        }
        bool _Selecting = false;
        bool _RightMouseDown = false;

        public override void Draw(GameTime gameTime)
        {
            if (AllowSelect && _Selecting)
            {
                _LocalBatch.Begin();
                Drawing.MonoGame2D.DrawRectangle(SelectedScreenArea, Color.Yellow, 3, _LocalBatch, _TextureManager.GetTexture("LineTexture"));
                _LocalBatch.End();
            }
            base.Draw(gameTime);
        }

        public event MouseSelectedAreaChanged OnMouseSelectedAreaChange;

        public event MouseRightClick OnMouseRightClick;


    }
}
