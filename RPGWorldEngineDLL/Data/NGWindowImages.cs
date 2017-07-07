using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace NebulaGames.RPGWorld.Data
{
    [XmlRoot(ElementName = "images")]
    public class NGWindowImages
    {
        [XmlElement(ElementName = "image")]
        public List<NGWindowImage> Images { get; set; }
    }
}
