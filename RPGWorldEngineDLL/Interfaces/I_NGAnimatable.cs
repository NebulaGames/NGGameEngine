using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace NebulaGames.RPGWorld.Interfaces
{
    public interface I_NGAnimatable
    {
        bool AnimationActive { get; set; }
        Dictionary<string,string> Textures { get; set; }
        Texture2D GetFrame(GameTime GameTimeObj);
        void ChangeAnimation(string AnimationName);
        bool FrontToBackToFront { get; set; }        
    }
}
