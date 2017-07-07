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
    [XmlRoot(ElementName = "frames")]
    public class NGAnimationFrames
    {     

        [XmlElement(ElementName = "frame")]
        public List<NGAnimationFrame> Frames { get; set; }
               
    }
}
