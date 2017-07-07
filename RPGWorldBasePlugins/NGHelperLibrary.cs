using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NebulaGames.RPGWorld.Structs;

namespace NebulaGames.RPGWorld.Plugins
{
    public class NGHelperLibrary : NebulaGames.RPGWorld.Interfaces.I_NGPlugin
    {

        #region Public Constructor
        public NGHelperLibrary()
        {
            _AvailableEvents.Add("OnIntroWindowShowing");
            _AvailableEvents.Add("OnIntroWindowShown");
            _AvailableEvents.Add("OnIntroWindowClosing");
            _AvailableEvents.Add("OnIntroWindowClosed");

            _VersionInfo.Authors = new string[] { "Mark Alicz" };
            _VersionInfo.Build = 1;
            _VersionInfo.BuildDate = new DateTime(2017, 6, 17);
            _VersionInfo.Comments = "First Example Plugin";
            _VersionInfo.CompatibilityHistory = new Dictionary<int, char>();
            _VersionInfo.Expirimental = false;
            _VersionInfo.Major = 1;
            _VersionInfo.Minor = 0;
            _VersionInfo.Release = true;

            _UpdateURLs.Add("http://www.nebulagames.net/NGHelperLibrary.aspx");
            _Name = "NGHelperLibrary";
            _Author = "Mark Alicz";
            _Help = "Get Help Young Man";
            _Description = "My New Plugin";
            _Email = "Mark@Alicz.Com";
            _Website = "www.nebulagames.net";

            _Commands.Add(new PluginMethod() { Arguments = new List<string>() { "CharacterClass" }, MethodName = "GetHighestCharacter", ReturnType = "CharacterClass" });

            _Windows.Add(new CustomIntroWindow());
        }

        #endregion

        #region Private Plugin Variables

        private string _Name = "";
        private string _Author = "";
        private string _Help = "";
        private string _Description = "";
        private string _Email = "";
        private string _Website = "";
        private Guid _GlobalID = Guid.NewGuid();
        private List<string> _AvailableEvents = new List<string>();
        private VersionInfo _VersionInfo = new VersionInfo();
        private Guid _InstallID = Guid.NewGuid();
        private List<string> _UpdateURLs = new List<string>();
        private List<PluginMethod> _Commands = new List<PluginMethod>();
        private Dictionary<string, Func<List<Object>>> _CommandAttachments = new Dictionary<string, Func<List<object>>>();
        private Dictionary<string, Func<List<Object>>> _EventAttachments = new Dictionary<string, Func<List<object>>>();
        private List<Interfaces.I_NGWindow> _Windows = new List<Interfaces.I_NGWindow>();
        private bool _WindowsEventsLoaded = false;
        #endregion

        #region public Plugin Properties

        public List<string> AvailableEvents
        {
            get
            {
                if (_WindowsEventsLoaded == false)
                {
                    _WindowsEventsLoaded = true;
                   // foreach(string windowName in _Windows.Select(x=>x.))
                }
                return _AvailableEvents;
            }
        }
        public string Name { get { return _Name; } }
        public string Author { get { return _Author; } }
        public string Help { get { return _Help; } }
        public string Description { get { return _Description; } }
        public Guid GlobalID { get { return _GlobalID; } }
        public VersionInfo Version { get { return _VersionInfo; } }
        public Guid InstallID { get { return _InstallID; } }
        public List<string> UpdateURLs { get { return _UpdateURLs; } }
        public string Email { get { return _Email; } }
        public string Website { get { return _Website; } }
        public List<PluginMethod> Commands { get { return _Commands; } }
        public List<Interfaces.I_NGWindow> Windows { get { return _Windows; } }

        public string Folder => throw new NotImplementedException();
        #endregion

        #region Plugin Methods
        public void AttachMethodToCommand(string CommandName, Func<List<object>> Method)
        {
            if (_CommandAttachments.ContainsKey(CommandName)) { _CommandAttachments[CommandName] = Method; }
            else { _CommandAttachments.Add(CommandName, Method); }
        }

        public void AttachMethodToEvent(string EventName, Func<List<object>> Method)
        {
            if (_EventAttachments.ContainsKey(EventName)) { _EventAttachments[EventName] = Method; }
            else { _EventAttachments.Add(EventName, Method); }
        }

        public List<string> GetCommandCallStack(string CommandName)
        {
            List<string> _TmpReturn = new List<string>();
            for (int keyPos = 0; keyPos < _CommandAttachments.Keys.Count; keyPos++)
            {
                _TmpReturn.Add(_CommandAttachments.Keys.ElementAt(keyPos));
            }
            return _TmpReturn;
        }

        public List<string> GetEventCallStack(string EventName)
        {
            List<string> _TmpReturn = new List<string>();
            for (int keyPos = 0; keyPos < _CommandAttachments.Keys.Count; keyPos++)
            {
                _TmpReturn.Add(_CommandAttachments.Keys.ElementAt(keyPos));
            }
            return _TmpReturn;
        }
        #endregion
    }
}
