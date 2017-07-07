using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NebulaGames.RPGWorld.Data.Textures
{
    [XmlRoot(ElementName = "textures")]
    public class NGTextures
    {
        [XmlElement(ElementName = "texture")]
        public List<NGTexture> Textures { get; set; }
    }
}
