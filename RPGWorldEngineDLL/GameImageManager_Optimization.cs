using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NebulaGames.RPGWorld.Performance;
using Microsoft.Xna.Framework.Graphics;
using NebulaGames.RPGWorld.Attributes;
using Microsoft.Xna.Framework;

namespace NebulaGames.RPGWorld
{
    public static partial class GameData
    {
        public class OptimizedData
        {
            public string SpriteSheetName { get; set; }
            public Rectangle Location { get; set; }
        }

        public static partial class ImageManager
        {
            /// <summary>
            /// Optimize the Textures By Combining them into a single texture.
            /// </summary>
            public static void OptimizeImages()
            {
                // Ensure All the Usage Counts Exist
                foreach (ulong key in GameImages.Keys) { if (ImageUsageCount.ContainsKey(key) == false) { ImageUsageCount.Add(key, 0); } }

                // Compile To One Sprite Sheet
                Packer packer = new Packer();

                foreach (var Tex in ImageUsageCount.OrderBy(x => x.Value))
                {
                    ImageInfo _ti = new ImageInfo();
                    _ti.Height = GameImages[Tex.Key].BaseTexture.Height;
                    _ti.Width = GameImages[Tex.Key].BaseTexture.Width;
                    _ti.Source = GameImages[Tex.Key].OriginalFilePath;

                    packer.SourceTextures.Add(_ti);
                }

                packer.Process(2048, 0, false);
                var _UpdatedImages = packer.SaveAtlasses();
            }

        }
    }
}
