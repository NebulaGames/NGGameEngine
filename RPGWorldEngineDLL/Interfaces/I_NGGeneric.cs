using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebulaGames.RPGWorld.Interfaces
{
    public interface I_NGGeneric
    {
        List<Structs.PluginMethod> AvailableMethods { get; }
        object ExecuteMethod(string MethodName, params object[] Parameters);
        Structs.DataPacket LastError();
        bool HasError();
    }
}
