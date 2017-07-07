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
    [XmlRoot(ElementName = "image")]
    public class NGImage
    {
        [XmlAttribute(AttributeName = "id")]
        public string ID { get; set; }

        [XmlAttribute(AttributeName = "scope")]
        public string Scope { get; set; }

        [XmlAttribute(AttributeName = "storage")]
        public string Storage { get; set; }

        [XmlAttribute(AttributeName = "file" )]
        public string FileName { get; set; }

        [XmlAttribute(AttributeName = "sourcerectangle")]
        public string SourceRectangle { get; set; } 

        [XmlIgnore()]
        public string ImageDirectory { get; set; }

        [XmlIgnore()]
        public Texture2D TextureRef { get; set; }
    }
}
