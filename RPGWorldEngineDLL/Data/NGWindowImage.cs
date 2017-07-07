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
    public class NGWindowImage
    {
        [XmlAttribute(AttributeName = "imageid")]
        public string ImageID { get; set; }

        [XmlAttribute(AttributeName = "destinationrectangle")]
        public string DestinationRectangle { get; set; }

        [XmlAttribute(AttributeName = "behavior")]
        public string Behavior { get; set; }
    }
}
