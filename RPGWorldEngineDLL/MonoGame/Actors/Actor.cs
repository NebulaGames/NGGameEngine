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
using NebulaGames.RPGWorld.MonoGame.Animate;
using NebulaGames.RPGWorld.MonoGame.Managers;
using NebulaGames.RPGWorld.Delegates;
using NebulaGames.RPGWorld.MonoGame.Extensions;
using NebulaGames.RPGWorld.Graphics;
namespace NebulaGames.RPGWorld.MonoGame
{

    public abstract class Actor : DrawableGameComponent
    {
        #region Fields (4)

        private Point _WorldPosition = new Point(10, 10);

        public Point WorldPosition
        {
            get { return _WorldPosition; }
            set { _WorldPosition = value; }
        }
        private int _SpeedPerSecond = 200;

        public int SpeedPerSecond
        {
            get { return _SpeedPerSecond; }
            set { _SpeedPerSecond = value; }
        }
        private AnimationManager _AnimationManagerRef;
        public AnimationEngine _AnimationEngine;
        Camera _MainCamera;
        private string _ID;
        private SpriteBatch _LocalBatch;
        private string _Name;
        private ScreenManager _ScreenManager;
        private bool _Selected;
        private bool _UserControl = true;
        private MouseManager _MouseManagerRef;
        private TextureManager _TextureManager;
        private Rectangle GotoRectangle;
        private List<Point> InScreens = new List<Point>();

        #endregion Fields

        #region Constructors (2)

        public Actor(Game XnaGame, string ActorName, SpriteBatch LocalBatch)
            : base(XnaGame)
        {
            Name = ActorName;

            var _TmpResults = from x in this.Game.Components where x is MouseManager select x;
            _MouseManagerRef = (MouseManager)_TmpResults.First();

            foreach (var x in XnaGame.Components)
            {
                if (x is AnimationManager)
                {
                    _AnimationManagerRef = (AnimationManager)x;
                }
                else if (x is ScreenManager)
                {
                    _ScreenManager = (ScreenManager)x;
                }
                else if (x is Camera)
                {
                    _MainCamera = (Camera)x;
                }
                else if (x is TextureManager)
                {
                    _TextureManager = (TextureManager)x;
                }
                if (_AnimationManagerRef != null && _ScreenManager != null) { break; }

            }

            if (!_AnimationManagerRef.ActorAnimations.Any(aa => aa.ActorName == Name))
            {
                _AnimationEngine = new AnimationEngine(this.Game, System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "Data\\Actors\\" + Name + "\\data.xml"), Name);
            }
            else
            {
                _AnimationEngine = new AnimationEngine(this.Game, Name);
            }

            _LocalBatch = LocalBatch;
            _MouseManagerRef.OnMouseSelectedAreaChange += new MouseSelectedAreaChanged(_MouseManagerRef_OnMouseSelectedAreaChange);
            _MouseManagerRef.OnMouseRightClick += new MouseRightClick(_MouseManagerRef_OnMouseRightClick);
            //  _ScreenManager.Screens[_WorldPosition.WorldPointToScreenPoint()].
        }

        void _MouseManagerRef_OnMouseRightClick(object sender, MouseEventArgs e)
        {

            if (_Selected)
            {
                //    WorldPosition.WorldPointToScreenPoint
                GotoRectangle = new Rectangle(e.mouseState.X, e.mouseState.Y, 10, 10);
                _Selected = false;
            }
        }
        Color OverRideColor = Color.White;
        void _MouseManagerRef_OnMouseSelectedAreaChange(object sender, MouseEventArgs e)
        {
            //  OverRideColor = Color.LightGreen;
            // var f = _AnimationEngine.GetCurrentFrame();
            Point? _TmpPoint = (Point?)_MainCamera.ToScreenPoint(new Point(_WorldPosition.X + _AnimationEngine.GetCurrentAnimation().BoundingBox.X, _WorldPosition.Y + _AnimationEngine.GetCurrentAnimation().BoundingBox.Y));

            if (_TmpPoint != null)
            {
                if (_MouseManagerRef.SelectedScreenArea.Contains((Point)_TmpPoint))
                {
                    _Selected = true;
                }
            }
        }

        public Actor(Game XnaGame, SpriteBatch LocalBatch)
            : base(XnaGame)
        {

        }

        #endregion Constructors

        #region Properties (2)

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        #endregion Properties

        #region Methods (2)

        // Public Methods (2) 

        public override void Draw(GameTime gameTime)
        {

            if (_MainCamera.Rec().Contains(_WorldPosition))
            {
                Point _TmpPoint = (Point)_MainCamera.ToScreenPoint(_WorldPosition);

                _LocalBatch.Begin();
               
                var f = _AnimationEngine.GetCurrentFrame();

                if (_Selected)
                {
                    List<Point> _BoundBoxPoints = _AnimationEngine.GetCurrentAnimation().BoundingBox.ToVisiblePoints();
                    //_MainCamera.ToScreenPoint(
                    Point _TmpBoundPoint = new Point(_TmpPoint.X + _BoundBoxPoints[0].X, _TmpPoint.Y + _BoundBoxPoints[0].Y);
                    Drawing.MonoGame2D.DrawRectangle(new Rectangle(_TmpBoundPoint.X, _TmpBoundPoint.Y, _AnimationEngine.GetCurrentAnimation().BoundingBox.Width, _AnimationEngine.GetCurrentAnimation().BoundingBox.Height), Color.Green, 2, _LocalBatch, _TextureManager.GetTexture("LineTexture"));

                }

                if (f.Filp == true)
                {
                    _LocalBatch.Draw(_AnimationEngine.GetCurrentTexture(), new Vector2(_TmpPoint.X, _TmpPoint.Y), f.ImageLocation, OverRideColor, 0, new Vector2(0, 0), new Vector2(1, 1), SpriteEffects.FlipHorizontally, 0);
                }
                else
                {
                    _LocalBatch.Draw(_AnimationEngine.GetCurrentTexture(), new Rectangle(_TmpPoint.X, _TmpPoint.Y, f.ImageLocation.Width, f.ImageLocation.Height), f.ImageLocation, OverRideColor);
                }
               

                _LocalBatch.End();


            }


            // base.Draw(gameTime);
        }
        public void ReCalculateScreens()
        {
            InScreens.Clear();
            //_AnimationEngine.GetCurrentFrame().
            Point _TopLeft = _WorldPosition.WorldPointToScreenPoint();
            Point _BottomRight = new Point(_WorldPosition.X + _AnimationEngine.GetCurrentAnimation().BoundingBox.Width, _WorldPosition.Y + _AnimationEngine.GetCurrentAnimation().BoundingBox.Height).WorldPointToScreenPoint();
            for (int x = _TopLeft.X; x < _BottomRight.X; x++)
            {
                for (int y = _TopLeft.Y; y < _BottomRight.Y; y++)
                {
                    Point _NewPoint = new Point(x, y);
                    InScreens.Add(_NewPoint);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (_UserControl && _Selected)
            {
                int _TmpSpeed = (int)(((float)gameTime.ElapsedGameTime.Milliseconds / (float)1000) * _SpeedPerSecond);
                Point _TmpChange = new Point();

                if (Keyboard.GetState().IsKeyDown(Keys.Up)) { _TmpChange.Y -= _TmpSpeed; }
                if (Keyboard.GetState().IsKeyDown(Keys.Right)) { _TmpChange.X += _TmpSpeed; }
                if (Keyboard.GetState().IsKeyDown(Keys.Left)) { _TmpChange.X -= _TmpSpeed; }
                if (Keyboard.GetState().IsKeyDown(Keys.Down)) { _TmpChange.Y += _TmpSpeed; }

                this._WorldPosition.X += _TmpChange.X;
                this._WorldPosition.Y += _TmpChange.Y;

                SetAnimationBasedOnMovement(_TmpChange);
                _AnimationEngine.AdvanceFrame();
            }
            else
            {
                // Preform AI
                PerformAI(gameTime);
            }

            base.Update(gameTime);
        }
        public void SetAnimationBasedOnMovement(Point _TmpChange)
        {

            if (_TmpChange.X > 0 && _TmpChange.Y < 0)
            {
                _AnimationEngine.SetAnimation("Walking", Enumerations.RelativePosition.TopRight);
            }
            else if (_TmpChange.X == 0 && _TmpChange.Y < 0)
            {
                _AnimationEngine.SetAnimation("Walking", Enumerations.RelativePosition.Top);
            }
            else if (_TmpChange.X < 0 && _TmpChange.Y < 0)
            {
                _AnimationEngine.SetAnimation("Walking", Enumerations.RelativePosition.TopLeft);
            }
            else if (_TmpChange.X < 0 && _TmpChange.Y == 0)
            {
                _AnimationEngine.SetAnimation("Walking", Enumerations.RelativePosition.Left);
            }
            else if (_TmpChange.X < 0 && _TmpChange.Y > 0)
            {
                _AnimationEngine.SetAnimation("Walking", Enumerations.RelativePosition.BottomLeft);
            }
            else if (_TmpChange.X == 0 && _TmpChange.Y > 0)
            {
                _AnimationEngine.SetAnimation("Walking", Enumerations.RelativePosition.Bottom);
            }
            else if (_TmpChange.X > 0 && _TmpChange.Y > 0)
            {
                _AnimationEngine.SetAnimation("Walking", Enumerations.RelativePosition.BottomRight);
            }
            else if (_TmpChange.X > 0 && _TmpChange.Y == 0)
            {
                _AnimationEngine.SetAnimation("Walking", Enumerations.RelativePosition.Right);
            }
            else if (_TmpChange.X == 0 && _TmpChange.Y == 0)
            {
                _AnimationEngine.SetAnimation("Standing", Enumerations.RelativePosition.Main);
                // _AnimationEngine.SetAnimation("Standing", _AnimationEngine.GetFacing());
            }
        }
        public virtual void PerformAI(GameTime gameTime)
        {
            if (GotoRectangle.Contains(this.WorldPosition))
            {
                GotoRectangle = Rectangle.Empty;
            }

            if (GotoRectangle != Rectangle.Empty)
            {
                int _TmpSpeed = (int)(((float)gameTime.ElapsedGameTime.Milliseconds / (float)1000) * _SpeedPerSecond);
                Point _TmpChange = new Point();

                if (GotoRectangle.X > WorldPosition.X)
                {
                    if (GotoRectangle.Y > WorldPosition.Y)
                    {
                        _TmpChange.X += _TmpSpeed;
                        _TmpChange.Y += _TmpSpeed;
                    }
                    else if (GotoRectangle.Y < WorldPosition.Y)
                    {

                        _TmpChange.X += _TmpSpeed;
                        _TmpChange.Y -= _TmpSpeed;
                    }
                    else
                    {
                        // Y is Equal Move Stright
                        _TmpChange.X += _TmpSpeed;
                    }
                }
                else if (GotoRectangle.X < WorldPosition.X)
                {
                    if (GotoRectangle.Y > WorldPosition.Y)
                    {
                        _TmpChange.X -= _TmpSpeed;
                        _TmpChange.Y += _TmpSpeed;
                    }
                    else if (GotoRectangle.Y < WorldPosition.Y)
                    {

                        _TmpChange.X -= _TmpSpeed;
                        _TmpChange.Y -= _TmpSpeed;
                    }
                    else
                    {
                        // Y is Equal Move Stright
                        _TmpChange.X -= _TmpSpeed;
                    }
                }
                else
                {
                    if (GotoRectangle.Y > WorldPosition.Y)
                    {
                        _TmpChange.Y += _TmpSpeed;
                    }
                    else if (GotoRectangle.Y < WorldPosition.Y)
                    {
                        _TmpChange.Y -= _TmpSpeed;
                    }
                }

                this._WorldPosition.X += _TmpChange.X;
                this._WorldPosition.Y += _TmpChange.Y;


                SetAnimationBasedOnMovement(_TmpChange);
                _AnimationEngine.AdvanceFrame();

            }
        }

        #endregion Methods


    }
}
