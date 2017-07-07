using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using NebulaGames.RPGWorld.MonoGame.Extensions;
using NebulaGames.RPGWorld;

namespace NebulaGames.RPGWorld.GameBuilder
{
    public class MapObject
    {
        #region Data Members (6)

        public Point DrawLocation;
        public Guid ID;
        public Size ImageSize;
        public Enumerations.RelativePosition Position;
        public string ResourceName;
        public EditorScreen Screen;

        #endregion Data Members

        #region Methods (3)

        public void CalculateDrawPosition(int Width, int Height, Point Location)
        {
            Point _DrawPosition = Point.Empty;

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
            ImageSize = new Size(Width, Height);
            ID = Guid.NewGuid();
        }

        public string ExportToXML()
        {
            StringBuilder _Result = new StringBuilder("<mapobject");

            _Result.Append(" resourcename=\"");
            _Result.Append(ResourceName.Substring(ResourceName.LastIndexOf("\\") + 1));
            _Result.Append("\" drawlocation=\"");
            _Result.Append(DrawLocation.ToString());
            _Result.Append("\" position=\"");
            _Result.Append(Position.ToString());
            _Result.Append("\" imagesize=\"");
            _Result.Append(ImageSize.ToString());
            _Result.Append("\" id=\"");
            _Result.Append(ID.ToString());
            _Result.Append("\" />\r\n");

            return _Result.ToString();

        }

        public bool HasPoint(int X, int Y)
        {
            Rectangle _R = new Rectangle(DrawLocation, ImageSize);
            if (_R.Contains(new Point(X, Y)))
            {
                return true;
            }
            return false;
        }

        #endregion Methods
    }
}
