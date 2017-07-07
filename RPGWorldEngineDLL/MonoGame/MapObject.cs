using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NebulaGames.RPGWorld.MonoGame
{
    public struct MapObject
    {
        public string ResourceName;
        public Point DrawLocation;
        public Enumerations.RelativePosition Position;
        public Point ImageSize;        
        public Guid ID;

        public bool HasPoint(int X, int Y)
        {
            Rectangle _R = new Rectangle(DrawLocation.X, DrawLocation.Y, ImageSize.X, ImageSize.Y);
            if (_R.Contains(new Point(X, Y)))
            {
                return true;
            }
            return false;
        }



        public void CalculateDrawPosition(int Width, int Height, Point Location)
        {
            Point _DrawPosition = Point.Zero;

            if (Position == Enumerations.RelativePosition.TopLeft) { _DrawPosition = Location; }
            else if (Position == Enumerations.RelativePosition.Top) { _DrawPosition = new Point(Location.X - (Width / 2), Location.Y); }
            else if (Position == Enumerations.RelativePosition.TopRight) { _DrawPosition = new Point(Location.X - Width, Location.Y); }
            else if (Position == Enumerations.RelativePosition.Left) { _DrawPosition = new Point(Location.X, Location.Y - (Height / 2)); }

            else if (Position == Enumerations.RelativePosition.Main) { _DrawPosition = new Point(Location.X - (Width / 2), Location.Y - (Height / 2)); }
            else if (Position == Enumerations.RelativePosition.Right) { _DrawPosition = new Point(Location.X - Width, Location.Y - (Height / 2)); }
            else if (Position == Enumerations.RelativePosition.BottomLeft) { _DrawPosition = new Point(Location.X, Location.Y - Height); }
            else if (Position == Enumerations.RelativePosition.Bottom) { _DrawPosition = new Point(Location.X - (Width / 2), Location.Y - Height); }
            else if (Position == Enumerations.RelativePosition.BottomRight) { _DrawPosition = new Point(Location.X - Width, Location.Y - Height); }

            DrawLocation = _DrawPosition;
            ImageSize = new Point(Width, Height);
            ID = Guid.NewGuid();
        }

    }
}
