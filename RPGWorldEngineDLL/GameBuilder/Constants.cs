using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace NebulaGames.RPGWorld.GameBuilder
{
    public static class Constants
    {

        public static SolidBrush BackGroundBrush = new SolidBrush(Color.FromArgb(0, 255, 0));

        public static Point[] SurroundPoints = new Point[] { new Point(-1, -1), new Point(0, -1), new Point(1, -1), new Point(-1, 0), new Point(0, 0), new Point(1, 0), new Point(-1, 1), new Point(0, 1), new Point(1, 1) };

        public static Dictionary<int, ImageAttributes> BitmapAttributesCache = new Dictionary<int, ImageAttributes>();

        public static ImageAttributes GetTransparentBitmapAttributes(int PercentTransparent)
        {

            if (BitmapAttributesCache.ContainsKey(PercentTransparent)) { return BitmapAttributesCache[PercentTransparent]; }

            System.Drawing.Imaging.ImageAttributes _IAttr = new System.Drawing.Imaging.ImageAttributes();

            System.Drawing.Imaging.ColorMatrix ClrMatrix = new System.Drawing.Imaging.ColorMatrix();
            #region Setup Color Matrix For .25 Transparent
            ClrMatrix.Matrix00 = 1.0f;
            ClrMatrix.Matrix01 = 0.0f;
            ClrMatrix.Matrix02 = 0.0f;
            ClrMatrix.Matrix03 = 0.0f;
            ClrMatrix.Matrix04 = 0.0f;
            ClrMatrix.Matrix10 = 0.0f;
            ClrMatrix.Matrix11 = 1.0f;
            ClrMatrix.Matrix12 = 0.0f;
            ClrMatrix.Matrix13 = 0.0f;
            ClrMatrix.Matrix14 = 0.0f;
            ClrMatrix.Matrix20 = 0.0f;
            ClrMatrix.Matrix21 = 0.0f;
            ClrMatrix.Matrix22 = 1.0f;
            ClrMatrix.Matrix23 = 0.0f;
            ClrMatrix.Matrix24 = 0.0f;
            ClrMatrix.Matrix30 = 0.0f;
            ClrMatrix.Matrix31 = 0.0f;
            ClrMatrix.Matrix32 = 0.0f;
            ClrMatrix.Matrix33 = (float)((float)PercentTransparent / (float)100);
            ClrMatrix.Matrix34 = 0.0f;
            ClrMatrix.Matrix40 = 0.0f;
            ClrMatrix.Matrix41 = 0.0f;
            ClrMatrix.Matrix42 = 0.0f;
            ClrMatrix.Matrix43 = 0.0f;
            ClrMatrix.Matrix44 = 1.0f;

            _IAttr.SetColorMatrix(ClrMatrix, System.Drawing.Imaging.ColorMatrixFlag.Default, System.Drawing.Imaging.ColorAdjustType.Bitmap);
            BitmapAttributesCache.Add(PercentTransparent, _IAttr);
            return BitmapAttributesCache[PercentTransparent];

            #endregion
        }
    }
}
