using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NebulaGames.RPGWorld.Data
{
    [XmlRoot(ElementName = "window")]
    public class NGWindow
    {
        [XmlAttribute(AttributeName = "x")]
        public string X { get; set; }

        [XmlAttribute(AttributeName = "y")]
        public string Y { get; set; }

        [XmlAttribute(AttributeName = "width")]
        public string Width { get; set; }

        [XmlAttribute(AttributeName = "height")]
        public string Height { get; set; }

        [XmlAttribute(AttributeName = "backgroundcolor")]
        public string BackgroundColor { get; set; }

        [XmlAttribute(AttributeName = "bordersize")]
        public string BorderSize { get; set; }

        [XmlAttribute(AttributeName = "bordercolor")]
        public string BorderColor { get; set; }

        [XmlAttribute(AttributeName = "backimage")]
        public string BackImage { get; set; }

        [XmlElement(ElementName = "images")]
        public List<NGWindowImages> WindowImages { get; set; }

        [XmlElement(ElementName = "animations")]
        public List<NGWindowAnimations> WindowAnimations { get; set; }
    }
}
