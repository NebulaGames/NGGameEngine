using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NebulaGames.RPGWorld.Data.GameConfiguration
{
    [XmlRoot(ElementName ="games")]
    public class NGGameConfigGames
    {
        [XmlElement(ElementName ="game")]
        public List<NGGameConfigGame> Games { get; set; }
    }

    [XmlRoot(ElementName ="game")]
    public class NGGameConfigGame
    {
        [XmlAttribute(AttributeName ="name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "sourcename")]
        public string SourceName { get; set; }

        [XmlAttribute(AttributeName = "enabled")]
        public string Enabled { get; set; }        
    }
}
