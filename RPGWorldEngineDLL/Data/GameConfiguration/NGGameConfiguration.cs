using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NebulaGames.RPGWorld.Data.GameConfiguration
{
    [XmlRoot(ElementName = "gameconfiguration")]
    public class NGGameConfiguration
    {
        [XmlAttribute(AttributeName ="initialgame")]
        public string InitialGame { get; set; }

        [XmlElement(ElementName = "videoconfiguration")]
        public NGGameConfigVideoConfiguration VideoConfiguration { get; set; }

        [XmlElement(ElementName = "textures")]
        public NGGameConfigTextures Textures { get; set; }

        [XmlElement(ElementName = "plugins")]
        public NGGameConfigPlugins Plugins { get; set; }

        [XmlElement(ElementName = "games")]
        public NGGameConfigGames Games { get; set; }
    }
}
