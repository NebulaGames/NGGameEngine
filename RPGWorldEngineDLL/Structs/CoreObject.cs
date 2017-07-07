using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebulaGames.RPGWorld.Structs
{
    public struct CoreObject
    {
        public string Name;
        public string InterfaceName;
        public string Location;
        public bool Builtin;
        public object BaseInstance;
        public System.Reflection.Assembly BaseAssembly;
        public bool Singleton;
        public int LoadPosition;
    }
}
