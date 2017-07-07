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
    [XmlRoot(ElementName = "animations")]
    public class NGWindowAnimations
    {
        [XmlElement(ElementName = "animation")]
        public List<NGWindowAnimation> Animations { get; set; }
    }
}
