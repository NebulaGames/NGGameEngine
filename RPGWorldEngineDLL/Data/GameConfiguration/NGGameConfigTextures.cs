using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NebulaGames.RPGWorld.Data.GameConfiguration
{
    [XmlRoot(ElementName = "textures")]
    public class NGGameConfigTextures
    {
        [XmlElement("directories")]
        public NGGameConfigTextureDirectories Directories { get; set; }

        [XmlElement("advanced")]
        public NGGameConfigTextureAdvanced Advanced { get; set; }
    }


    [XmlRoot(ElementName = "directories")]
    public class NGGameConfigTextureDirectories
    {
        [XmlElement(ElementName = "directory")]
        public List<NGGameConfigTextureDirectoriesDirectory> Directories { get; set; }
    }

    [XmlRoot(ElementName = "advanced")]
    public class NGGameConfigTextureAdvanced
    {
        [XmlElement(ElementName = "cachesize")]
        public List<NGGameConfigTextureAdvancedCacheSize> CacheSize { get; set; }
    }

    [XmlRoot(ElementName = "directory")]
    public class NGGameConfigTextureDirectoriesDirectory
    {

        [XmlAttribute(AttributeName = "absolutepath")]
        public string AbsolutePath { get; set; }

        [XmlAttribute(AttributeName = "sourcename")]
        public string SourceName { get; set; }

        [XmlAttribute(AttributeName = "subfolders")]
        public string SubFolders { get; set; }

        [XmlAttribute(AttributeName = "relativepath")]
        public string RelativePath { get; set; }

        [XmlIgnore()]
        public bool GetSubFolders { get { if (SubFolders.ToLower() == "true" || SubFolders.ToLower() == "1" || SubFolders.ToLower() == "yes" || SubFolders.ToLower() == "y") { return true; } else { return false; } } }
    }

    [XmlRoot(ElementName = "cachesize")]
    public class NGGameConfigTextureAdvancedCacheSize
    {
        [XmlAttribute(AttributeName = "value")]
        public string CacheSize { get; set; }
    }
}
