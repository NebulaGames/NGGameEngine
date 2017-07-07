using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebulaGames.RPGWorld.Enumerations
{
    [Flags()]
    public enum SystemTypesEnum
    {
        WINDOWS = 0,
        ANDROID = 1,
        IOS = 2,
        LINUX = 4,
        DEPENDANT = 8
    }
}
