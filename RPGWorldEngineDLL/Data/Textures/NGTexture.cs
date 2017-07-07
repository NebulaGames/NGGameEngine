using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NebulaGames.RPGWorld.Data.Textures
{
    [XmlRoot(ElementName = "texture")]
    public class NGTexture
    {
        [XmlElement(ElementName = "id")]
        public string ID { get; set; }

        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "filename")]
        public string FileName { get; set; }

        [XmlElement(ElementName = "description")]
        public string Description { get; set; }

        [XmlElement(ElementName = "displayname")]
        public string DisplayName { get; set; }

        [XmlElement(ElementName = "animationseq")]
        public string AnimationSeq { get; set; }

        [XmlElement(ElementName = "animationgroup")]
        public string AnimationGroup { get; set; }

        [XmlElement(ElementName = "animationdisplaytime")]
        public string AnimationDisplayTime { get; set; }

        [XmlIgnore()]
        public ulong uID { get; set; }

        public ulong ImageID { get; set; }
    }
}
