using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NebulaGames.RPGWorld.Data
{
    [XmlRoot(ElementName = "windows")]
    public class NGWindows
    {
        [XmlElement(ElementName = "window")]
        public List<NGWindow> Windows { get; set; }

    
    }
}

