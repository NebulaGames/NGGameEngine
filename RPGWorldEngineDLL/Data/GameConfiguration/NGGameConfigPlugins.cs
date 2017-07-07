using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NebulaGames.RPGWorld.Data.GameConfiguration
{
    [XmlRoot(ElementName = "plugins")]
    public class NGGameConfigPlugins
    {
        [XmlElement(ElementName ="plugin")]
        public List<NGGameConfigPlugin> Plugins { get; set; }
    }

    [XmlRoot(ElementName ="plugin")]
    public class NGGameConfigPlugin
    {
        [XmlIgnore()]
        public int OrderInt { get { return Convert.ToInt32(Order); } }

        [XmlAttribute(AttributeName = "order")]
        public string Order { get; set; }

        [XmlAttribute(AttributeName ="gamename")]
        public string GameName { get; set; }

        [XmlAttribute(AttributeName = "sourcename")]
        public string SourceName { get; set; }

        [XmlAttribute(AttributeName ="name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "enabled")]
        public string Enabled { get; set; }

        [XmlIgnore()]
        List<Plugins.plugin> PluginMetaData { get; set; }
    }
}
