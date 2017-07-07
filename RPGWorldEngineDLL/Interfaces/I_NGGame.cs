using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NebulaGames.RPGWorld.Interfaces
{
    public interface I_NGGame
    {
        Game GameRef { get; set; } 
        bool GameRunning { get; set; }
        bool GamePaused { get; set; }
        bool GameConnected { get; set; }
    }
}
