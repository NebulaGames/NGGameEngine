using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace NebulaGames.RPGWorld.MonoGame
{
    public static class XNAMath
    {
        public static Vector2 CalculateMidPoint(Vector2 A, Vector2 B)
        {
            var x = (A.X + B.X) / 2;
            var y = (A.Y + B.Y) / 2;

            return new Vector2(x, y);
        }

        public static float CalculateDistanceBetween2Points(Vector2 A, Vector2 B)
        {
            return (float)(Math.Sqrt(Math.Pow(Math.Abs(A.X - B.X), 2) + Math.Pow(Math.Abs(A.Y - B.Y), 2)));
        }

    }
}
