using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebulaGames.RPGWorld.Interfaces
{
    public interface I_NGPlugin
    {
        string Folder { get; }
        List<string> AvailableEvents { get; }
        string Name { get; }
        string Author { get; }
        string Help { get; }
        string Description { get; }
        Guid GlobalID { get; }
        Structs.VersionInfo Version { get; }
        Guid InstallID { get; }
        List<string> UpdateURLs { get; }
        string Email { get; }
        string Website { get; }
        List<Structs.PluginMethod> Commands { get; }
        List<string> GetEventCallStack(string EventName);
        List<string> GetCommandCallStack(string CommandName);
        void AttachMethodToEvent(string EventName, Func<List<object>> Method);
        void AttachMethodToCommand(string CommandName, Func<List<object>> Method);
        List<I_NGWindow> Windows { get; }

    }
}
