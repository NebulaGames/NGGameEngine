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
    [XmlRoot(ElementName = "animation")]
    public class NGWindowAnimation
    {
        [XmlAttribute(AttributeName = "animationid")]
        public string AnimationID { get; set; }

        [XmlAttribute(AttributeName = "destinationrectangle")]
        public string DestinationRectangle { get; set; }

        [XmlAttribute(AttributeName = "behavior")]
        public string Behavior { get; set; }

        [XmlAttribute(AttributeName = "movementkeys")]
        public string MovementKeys { get; set; }

    }
}
