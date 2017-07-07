using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NebulaGames.RPGWorld.Interfaces;
using NebulaGames.RPGWorld.Structs;

namespace NebulaGames.RPGWorld.Plugins
{
    public class NGPerformanceMonitor : NebulaGames.RPGWorld.Interfaces.I_NGPlugin
    {
        public List<string> AvailableEvents
        {
            get
            {
                return new List<string>() { "OnFPSSegmentCalculated", "OnFPGTicl" };
            }
        }

        public string Name => "NGPerformanceMonitor";

        public string Author => "Mark Alicz";

        public string Help => Helper.LoadPluginHelp(this);

        public string Description => Helper.GetPluginHelpElement(Help, "Description");

        public Guid GlobalID => new Guid("a604839e-f8db-4372-a27f-5e2c2b9d8d19");

        private VersionInfo _VInfo = new VersionInfo();
        public VersionInfo Version {
            get
            {
                if (_VInfo == null)
                {
                    _VInfo = new VersionInfo() { Authors = new string[] { "Mark Alicz" }, Build = 1, BuildDate = Convert.ToDateTime("6/25/2017"), Comments = "Plugin to Help Monitor Game Performance", Expirimental = false, Major = 1, Minor = 2, Release = true };
                    //_VInfo.Value.CompatibilityHistory = new Dictionary<int, char>();
                    // TODO                    
                }
                return _VInfo;
                
            }
        }

        public Guid InstallID => throw new NotImplementedException();

        public List<string> UpdateURLs => throw new NotImplementedException();

        public string Email => throw new NotImplementedException();

        public string Website => throw new NotImplementedException();

        public List<PluginMethod> Commands => throw new NotImplementedException();

        public List<I_NGWindow> Windows => throw new NotImplementedException();

        public string Folder => throw new NotImplementedException();

        public void AttachMethodToCommand(string CommandName, Func<List<object>> Method)
        {
            throw new NotImplementedException();
        }

        public void AttachMethodToEvent(string EventName, Func<List<object>> Method)
        {
            throw new NotImplementedException();
        }

        public List<string> GetCommandCallStack(string CommandName)
        {
            throw new NotImplementedException();
        }

        public List<string> GetEventCallStack(string EventName)
        {
            throw new NotImplementedException();
        }
    }
}
