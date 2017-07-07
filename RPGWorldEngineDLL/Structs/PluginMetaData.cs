using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebulaGames.RPGWorld.Structs
{
    public struct PluginMethod
    {
        public string MethodName;
        public List<string> Arguments;
        public string ReturnType;
    }
    public struct PluginMetaData
    {
        public List<PluginMethod> PublicStaticMethods;
        public string Name;
        public string Author;
        public string Help;
        public string TypeName;
    }
}
