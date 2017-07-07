using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebulaGames.RPGWorld.Enumerations
{
    [Flags]
    public enum SpriteBatchPurpose
    {
        Any,
        Window,
        WindowObject,
        Player,
        Actor,
        OffScreenBuffer,
        SpecialEffects,
        Particles
    }
}
