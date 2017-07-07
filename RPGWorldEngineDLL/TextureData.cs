using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NebulaGames.RPGWorld.Performance;
using Microsoft.Xna.Framework.Graphics;
using NebulaGames.RPGWorld.Attributes;

namespace NebulaGames.RPGWorld
{
    public static partial class GameData
    {

        public class TexturePackage
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string Author { get; set; }
            public string Description { get; set; }
            public DateTime Date { get; set; }

        }

        /// <summary>
        /// Image DATA Class
        /// </summary>
        public class ImageData
        {
            public ulong ID { get; private set; }
            public string GID { get; set; }
            public string Description { get; set; }
            public string DisplayName { get; set; }

            public string OriginalFilePath { get; set; }
            public string FileName { get; set; }
            public string Extension { get; set; }
            
            public Texture2D BaseTexture { get; set; }

            public bool Optimized { get; set; }

            public Dictionary<int, string> OptimizedLocations = new Dictionary<int, string>();

            public ImageData(ulong ImageID)
            {
                ID = ImageID;
            }
        }
    }
}
