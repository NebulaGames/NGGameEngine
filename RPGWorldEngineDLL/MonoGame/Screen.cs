using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NebulaGames.RPGWorld.MonoGame
{
    public class Screen 
    {
        private Dictionary<Guid, Actor> _CurrentActors = new Dictionary<Guid, Actor>();

        private Dictionary<Point, bool> _PassableCompile = new Dictionary<Point, bool>();

        public Dictionary<Point, bool> PassableCompile
        {
            get { return _PassableCompile; }
            set { _PassableCompile = value; }
        }

        public Dictionary<Guid, MapObject> MapObjects = new Dictionary<Guid, MapObject>();

        private World _Owner;
        private Point _WorldLocation;

        public Screen(Point Location, World WorldManager)
        {
            _WorldLocation = Location;

        }

        public Point ReturnModifier(Point WorldPoint, Point CameraSize)
        {
            int _TmpOffsetX = (_WorldLocation.X * 800) - WorldPoint.X;
            int _TmpOffsetY = (_WorldLocation.Y * 600) - WorldPoint.Y;

            return new Point(_TmpOffsetX, _TmpOffsetY);
        }
    }
}
