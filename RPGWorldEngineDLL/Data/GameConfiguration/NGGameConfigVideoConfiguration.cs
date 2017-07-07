using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NebulaGames.RPGWorld.Data.GameConfiguration
{
    [XmlRoot(ElementName = "videoconfiguration")]
    public class NGGameConfigVideoConfiguration
    {
        [XmlElement(ElementName = "resolution")]
        public List<NGGameConfigVideoConfigResolution> Resolution { get; set; }

        [XmlElement(ElementName = "fullscreen")]
        public List<NGGameConfigVideoConfigFullScreen> FullScreen { get; set; }
    }

    [XmlRoot(ElementName = "resolution")]
    public class NGGameConfigVideoConfigResolution
    {
        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
    }

    [XmlRoot(ElementName = "fullscreen")]
    public class NGGameConfigVideoConfigFullScreen
    {
        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
    }
}
