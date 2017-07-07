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
    [XmlRoot(ElementName = "pluginconfig")]
    public class NGPluginConfig
    {
        [XmlAttribute(AttributeName = "compiled")]
        public string Compiled { get; set; }

        [XmlAttribute(AttributeName = "gamename")]
        public string GameName { get; set; }

        [XmlElement(ElementName = "images")]
        public NGImages Images { get; set; }

        [XmlElement(ElementName = "animations")]
        public NGAnimations Animations { get; set; }

        [XmlElement(ElementName = "windows")]
        public NGWindows Windows { get; set; }
    }
}
