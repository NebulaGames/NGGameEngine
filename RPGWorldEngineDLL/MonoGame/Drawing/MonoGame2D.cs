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
using System.Xml.XPath;

namespace NebulaGames.RPGWorld.MonoGame.Drawing
{
    public class MonoGame2D
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="R"></param>
        /// <param name="C"></param>
        /// <param name="LineWidth"></param>
        /// <param name="LocalBatch">Begin() Must be called First</param>
        /// <param name="PixelTexture"></param>
        public static void DrawRectangle(Rectangle R, Color C, int LineWidth, SpriteBatch LocalBatch, Texture2D PixelTexture)
        {            
            LocalBatch.Draw(PixelTexture, new Rectangle(R.X, R.Y, R.Width, LineWidth), C);
            LocalBatch.Draw(PixelTexture, new Rectangle(R.X, R.Y, LineWidth, R.Height), C);
            LocalBatch.Draw(PixelTexture, new Rectangle(R.X + R.Width - LineWidth, R.Y, LineWidth, R.Height), C);
            LocalBatch.Draw(PixelTexture, new Rectangle(R.X, R.Y + R.Height - LineWidth, R.Width, LineWidth), C);

        }
    }
}
