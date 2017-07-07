
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NebulaGames.RPGWorld.MonoGame.Extensions;

namespace NebulaGames.RPGWorld.MonoGame
{
    public class Camera : GameComponent
    {
        public Vector2 TopLeftPoint;
        public Vector2 Size;
        public Rectangle Rec(){ return new Rectangle((int)TopLeftPoint.X,(int)TopLeftPoint.Y, (int)Size.X, (int)Size.Y);}

        public Camera(Game XnaGame)
            : base(XnaGame)
        {
            this.Game.Components.Add(this);
        }
        public List<Point> GetVisibleScreens()
        {
            List<Point> _TmpReturn = new List<Point>();

            Point _TmpTopLeftPoint = new Point();
            Point _TmpBottomRightPoint = new Point();
                        
            _TmpTopLeftPoint = GetTopLeft();
            _TmpBottomRightPoint = GetBottomRight();
                   

            for (int x = _TmpTopLeftPoint.X; x <= _TmpBottomRightPoint.X; x++)
            {
                for (int y = _TmpTopLeftPoint.Y; y <= _TmpBottomRightPoint.Y; y++)
                {
                    Point _NewPoint = new Point(x, y);
                    _TmpReturn.Add(_NewPoint);
                }
            }
            return _TmpReturn;

        }

        public Point GetTopLeft()
        {  
            return TopLeftPoint.ToPoint().WorldPointToScreenPoint();         
        
        }

        public Point GetBottomRight()
        {
            Point _TmpBottomRightPoint = new Point();

            _TmpBottomRightPoint.X = (int)TopLeftPoint.X + (int)Size.X;
            _TmpBottomRightPoint.Y = (int)TopLeftPoint.Y + (int)Size.Y;

            return _TmpBottomRightPoint.WorldPointToScreenPoint();
           
        }

        public List<Point> GetNeededScreens()
        {
            List<Point> _TmpReturn = new List<Point>();

            Point _TmpTopLeftPoint = new Point();
            Point _TmpBottomRightPoint = new Point();

            _TmpTopLeftPoint = GetTopLeft();
            _TmpBottomRightPoint = GetBottomRight();

            for (int x = _TmpTopLeftPoint.X-2; x < _TmpBottomRightPoint.X+2; x++)
            {
                for (int y = _TmpTopLeftPoint.Y-2; y < _TmpBottomRightPoint.Y+2; y++)
                {
                    Point _NewPoint = new Point(x, y);
                    _TmpReturn.Add(_NewPoint);
                }
            }
            return _TmpReturn;
        }

        public Point? ToScreenPoint(Point WorldPoint)
        {
            if (Rec().Contains(WorldPoint))
            {
                int NewX, NewY;
                Point _TmpPoint = TopLeftPoint.ToPoint();

                if (_TmpPoint.X < 0)
                {
                    NewX = (_TmpPoint.X * -1) + WorldPoint.X;
                }
                else
                {
                    NewX = Math.Abs(_TmpPoint.X - WorldPoint.X);
                }

                if (_TmpPoint.Y < 0)
                {
                    NewY = (_TmpPoint.Y * -1) + WorldPoint.Y;
                }
                else
                {
                    NewY = Math.Abs(_TmpPoint.Y - WorldPoint.Y);
                }

                return new Point(NewX, NewY);
            }
            else { return null; }
        }
               
    }
}
