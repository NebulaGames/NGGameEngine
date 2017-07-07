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
using ACT.Core.Extensions;
using NebulaGames.RPGWorld.MonoGame.Windows;

namespace NebulaGames.RPGWorld.MonoGame.Managers
{
    public class WindowManager : DrawableGameComponent
    {
        private Camera _WorkingCamera;

        public Camera WorkingCamera
        {
            get { return _WorkingCamera; }           
        }

        public List<Window> Windows = new List<Window>();

        public SpriteBatch WindowBatch;

        public WindowManager(Game XnaGame)
            : base(XnaGame)
        {
            this.DrawOrder = 100;
            this.Game.Components.Add(this);
            foreach (var t in this.Game.Components)
            {
                if (t is Camera)
                {
                    _WorkingCamera = (Camera)t;
                }
            }

            WindowBatch = new SpriteBatch(this.Game.GraphicsDevice);

            foreach (string _D in System.IO.Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + "Data\\Windows\\"))
            {
                Window _NewWindow = new Window(XnaGame, _D.GetDirectoryName());
                _NewWindow.Active = true;

                Windows.Add(_NewWindow);
            }

        }
        private Point _LastDownPoint;
        private Point _CurrentDownPoint;

        private bool _MouseDown;
        private int _MouseDelay = 0;

        public void StoreMouseState(GameTime gameTime)
        {
            if (_MouseDelay > 10)
            {
                _MouseDelay = 0;
                MouseState _ms = Mouse.GetState();

                if (_ms.LeftButton == ButtonState.Released)
                {
                    _MouseDown = false;
                    var W = from t in Windows where t._IsMoving == true select t;
                    foreach (var w in W) { w._IsMoving = false; }
                }
                else
                {
                    if (_MouseDown == false)
                    {
                        _MouseDown = true;
                        _CurrentDownPoint.X = _ms.X;
                        _CurrentDownPoint.Y = _ms.Y;
                        _LastDownPoint.X = 0;
                        _LastDownPoint.Y = 0;
                    }
                    else
                    {

                        _LastDownPoint.X = _CurrentDownPoint.X;
                        _LastDownPoint.Y = _CurrentDownPoint.Y;
                        _CurrentDownPoint.X = _ms.X;
                        _CurrentDownPoint.Y = _ms.Y;
                    }
                }
            }
            else
            {
                _MouseDelay += gameTime.ElapsedGameTime.Milliseconds;
            }
        }
        private Point _InitialOffset;

        public override void Update(GameTime gameTime)
        {
            var W = from t in Windows where t.Active == true select t;
            StoreMouseState(gameTime);

            foreach (var w in W)
            {
                w.Update(gameTime);

                // Perform the Mouse Checks
                //  MouseState _ms = Mouse.GetState();
                if (w._IsMoving)
                {

                    w.MoveTo((_CurrentDownPoint.X - _InitialOffset.X), (_CurrentDownPoint.Y -_InitialOffset.Y));
                }
                else
                {
                    
                        if (_CurrentDownPoint.Y > w.ScreenPosition.Top && _CurrentDownPoint.Y < w.ScreenPosition.Top + 20 && _CurrentDownPoint.X > w.ScreenPosition.X && _CurrentDownPoint.X < w.ScreenPosition.Right - 10)
                        {
                            if (_MouseDown)
                            {
                                _InitialOffset.X = _CurrentDownPoint.X - w.ScreenPosition.Left;
                                _InitialOffset.Y = _CurrentDownPoint.Y - w.ScreenPosition.Top;
                                w._IsMoving = true;
                            }
                        }
                        else { w._IsMoving = false; }
                    
                }
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            var W = from t in Windows where t.Active == true select t;

            foreach (var w in W)
            {
                w.Draw(gameTime, WindowBatch);
            }
            base.Draw(gameTime);
        }

     
    }
}
