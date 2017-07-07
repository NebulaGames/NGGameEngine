using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ACT.Core.Extensions;

namespace NebulaGames.RPGWorld.MonoGame.Extensions
{
    public static class MonoGameExtensionMethods
    {
        #region Methods (12)

        // Public Methods (12) 

        public static List<Microsoft.Xna.Framework.Point> ToVisiblePoints(this Microsoft.Xna.Framework.Rectangle x)
        {
            List<Microsoft.Xna.Framework.Point> _TmpReturn = new List<Microsoft.Xna.Framework.Point>();
            _TmpReturn.Add(new Microsoft.Xna.Framework.Point(x.X, x.Y));
            _TmpReturn.Add(new Microsoft.Xna.Framework.Point(x.Right, x.Bottom));
            return _TmpReturn;
        }

        public static string ParseXMLFunction(this string S)
        {
            string _TmpString = S;
            if (S.StartsWith("{="))
            {
                _TmpString = S.Replace("{=", "").Replace("}", "");
            }
            else { return S; }

            if (_TmpString == "NEWGUID")
            {
                return Guid.NewGuid().ToString();
            }
            else
            {
                return _TmpString;
            }
        }


        public static Microsoft.Xna.Framework.Point FromString(this Microsoft.Xna.Framework.Point x, string Value)
        {

            Value = Value.Replace("{X=", "").Replace("Y=", "").Replace("}", "");
            var _DLS = Value.Split(",".ToCharArray());
            return new Microsoft.Xna.Framework.Point(Convert.ToInt32(_DLS[0]), Convert.ToInt32(_DLS[1]));

        }
               
        
        public static int Difference(this Microsoft.Xna.Framework.Rectangle x, Microsoft.Xna.Framework.Rectangle y)
        {
            return Math.Abs(x.X - y.X) + Math.Abs(x.Y - y.Y) + Math.Abs(x.Width - y.Width) + Math.Abs(x.Height - y.Height);
        }

        public static Microsoft.Xna.Framework.Rectangle FromTwoPoints(this Microsoft.Xna.Framework.Rectangle x, Microsoft.Xna.Framework.Point A, Microsoft.Xna.Framework.Point B)
        {
            int _MinX, _MinY;
            int _MaxX, _MaxY;

            if (A.X < B.X)
                {
                _MinX = A.X;
                _MaxX = B.X;
            }
            else{
                _MinX = B.X;
                _MaxX = A.X;
            }

            if (A.Y < B.Y){
                _MinY = A.Y;
                _MaxY = B.Y;
            }
            else{
                _MinY = B.Y;
                _MaxY = A.Y;
            }

            return new Microsoft.Xna.Framework.Rectangle(_MinX,_MinY,_MaxX-_MinX, _MaxY-_MinY);
        }

        public static Microsoft.Xna.Framework.Rectangle FromString(this Microsoft.Xna.Framework.Rectangle R, string Value)
        {
            var _tmpLocationArray = Value.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            R.X = Convert.ToInt32(_tmpLocationArray[0]);
            R.Y = Convert.ToInt32(_tmpLocationArray[1]);
            R.Width = Convert.ToInt32(_tmpLocationArray[2]);
            R.Height = Convert.ToInt32(_tmpLocationArray[3]);

            return R;

        }
         

        public static Microsoft.Xna.Framework.Point ToPoint(this Microsoft.Xna.Framework.Vector2 V)
        {
            return new Microsoft.Xna.Framework.Point((int)V.X, (int)V.Y);
        }

        public static string ToScreenKey(this Microsoft.Xna.Framework.Point Pn, Enumerations.RelativePosition P)
        {
            int _tx, _ty;
            _tx = Pn.X;
            _ty = Pn.Y;

            StringBuilder _SB = new StringBuilder();

            if (P == Enumerations.RelativePosition.Left)
            {
                _tx--;
            }
            else if (P == Enumerations.RelativePosition.Bottom)
            {
                _ty++;
            }
            else if (P == Enumerations.RelativePosition.Right)
            {
                _tx++;
            }
            else if (P == Enumerations.RelativePosition.Top)
            {
                _ty--;
            }
            else if (P == Enumerations.RelativePosition.TopLeft)
            {
                _tx--; _ty--;
            }
            else if (P == Enumerations.RelativePosition.TopRight)
            {
                _tx++;
                _ty--;
            }
            else if (P == Enumerations.RelativePosition.BottomRight)
            {
                _tx++;
                _ty++;
            }
            else if (P == Enumerations.RelativePosition.BottomLeft)
            {
                _tx--; _ty++;
            }

            _SB.Append(_tx);
            _SB.Append("-");
            _SB.Append(_ty);

            return _SB.ToString();
        }

        public static Microsoft.Xna.Framework.Point ToXnaPoint(this Size x, string Value)
        {
            Value = Value.Replace("{Width=", "").Replace("Height=", "").Replace("}", "");
            var _DLS = Value.Split(",".ToCharArray());
            return new Microsoft.Xna.Framework.Point(Convert.ToInt32(_DLS[0]), Convert.ToInt32(_DLS[1]));

        }
        public static Microsoft.Xna.Framework.Vector2 ToVector2(this Microsoft.Xna.Framework.Point P)
        {
            return new Microsoft.Xna.Framework.Vector2(P.X, P.Y);
        }
        public static Microsoft.Xna.Framework.Point WorldPointToScreenPoint(this Microsoft.Xna.Framework.Point P)
        {
            Microsoft.Xna.Framework.Point _TmpTopLeftPoint = new Microsoft.Xna.Framework.Point();
            bool _IsNegative = false;
            var _TmpX = (double)(P.X / (double)800);

            if (_TmpX < 0)
            {
                _IsNegative = true;
                _TmpX = _TmpX * -1;
                _TmpX = Math.Ceiling(_TmpX);
            }
            else
            {
                _TmpX = Math.Floor(_TmpX);
            }

            if (_IsNegative) { _TmpX = _TmpX * -1; }

            _IsNegative = false;

            var _TmpY = (double)(P.Y / (double)600);

            if (_TmpY < 0)
            {
                _IsNegative = true;
                _TmpY = _TmpY * -1;
                _TmpY = Math.Ceiling(_TmpY);
            }
            else
            {
                _TmpY = Math.Floor(_TmpY);
            }


            if (_IsNegative) { _TmpY = _TmpY * -1; }

            _IsNegative = false;

            _TmpTopLeftPoint.X = (int)_TmpX;
            _TmpTopLeftPoint.Y = (int)_TmpY;

            return _TmpTopLeftPoint;
        }

        #endregion Methods
    }


}
