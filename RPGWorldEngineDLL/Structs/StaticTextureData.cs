using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameData = NebulaGames.RPGWorld.GameData;
using NebulaGames.RPGWorld.Structs;

namespace NebulaGames.RPGWorld.Structs
{
    public struct StaticTextureData
    {
        public Texture2D TextureData;
        public int xTexturePosition;
        public int yTexturePosition;
        public int widthTextureSize;
        public int heightTextureSize;
    }
}
