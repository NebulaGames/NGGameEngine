using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NebulaGames.RPGWorld.Interfaces;

namespace NebulaGames.RPGWorld
{
    /// <summary>
    /// This is the CORE Class.  It represents all of the Plugins
    /// </summary>
    public static class CurrentCore
    {
        public static List<System.Reflection.Assembly> AllAssemblies = new List<System.Reflection.Assembly>();
        public static List<Structs.CoreObject> ObjectHierarchy = new List<Structs.CoreObject>();
        public static bool Loaded = false;
        public static DateTime LastLoad = DateTime.Now;

        public static Dictionary<string, List<I_NGPlugin>> ObjectCache = new Dictionary<string, List<I_NGPlugin>>();
        public static Dictionary<string, Data.NGPluginConfig> PluginConfiguration = new Dictionary<string, Data.NGPluginConfig>();

        private static void LoadAllPlugins(string HelperDirectory = "")
        {
            string _RootDir = GameData.BaseDirectory;
            if (HelperDirectory != "") { _RootDir = HelperDirectory; }

            if (_RootDir.EndsWith("Data\\Plugins\\") == false)
            {
                _RootDir = _RootDir.EnsureDirectoryFormat() + "Data\\Plugins\\";
            }

            foreach (var file in System.IO.Directory.GetFiles(_RootDir, "*.dll", System.IO.SearchOption.AllDirectories))
            {
                string _PluginDirectory = file.Substring(0, file.LastIndexOf("\\"));
                string _PluginDirectoryName = _PluginDirectory.Replace(_PluginDirectory.Substring(0, _PluginDirectory.LastIndexOf("\\")), "").Replace("\\","");


                var _MainAssembly = System.Reflection.Assembly.LoadFile(file);
                AllAssemblies.Add(_MainAssembly);

                if (System.IO.File.Exists(_PluginDirectory.EnsureDirectoryFormat() + "pluginconfig.xml"))
                {
                    var _FileDataByteArray = Helper.IO.GetFile(_PluginDirectory.EnsureDirectoryFormat() + "pluginconfig.xml");
                    if (_FileDataByteArray == null) { continue; /*TODO LOG ERROR*/ }

                    System.Xml.Serialization.XmlSerializer _Serializer = new System.Xml.Serialization.XmlSerializer(typeof(Data.NGPluginConfig));
                    using (var _reader = new System.IO.StreamReader(new System.IO.MemoryStream(_FileDataByteArray)))
                    {
                        var _TmpData = (Data.NGPluginConfig)_Serializer.Deserialize(_reader);
                        if (_TmpData == null) { continue; }
                        PluginConfiguration.Add(_PluginDirectoryName, _TmpData);
                    }
                    _Serializer = null;
                    _FileDataByteArray = null;
                }

                foreach (var typ in _MainAssembly.ExportedTypes)
                {
                    if (typ.GetInterface("NebulaGames.RPGWorld.Interfaces.I_NGPlugin") != null)
                    {

                        Structs.CoreObject _CoreObj = new Structs.CoreObject();
                        _CoreObj.BaseAssembly = _MainAssembly;
                        _CoreObj.Builtin = false;
                        _CoreObj.InterfaceName = "NebulaGames.RPGWorld.Interfaces.I_NGPlugin";
                        _CoreObj.LoadPosition = 1;
                        _CoreObj.BaseInstance = _MainAssembly.CreateInstance(typ.FullName);

                        ObjectHierarchy.Add(_CoreObj);
                    }
                }
            }
        }
        private static void LoadAllObjects(string HelperDirectory = "")
        {
            LoadAllPlugins(HelperDirectory);

        }

        public static List<I_NGPlugin> GetObjects(string TypeName, string HelperDirectory = "")
        {
            if (Loaded)
            {
                if (ObjectCache.ContainsKey("NebulaGames.RPGWorld.Interfaces.I_NGPlugin") == false)
                {
                    ObjectCache.Add("NebulaGames.RPGWorld.Interfaces.I_NGPlugin", new List<NebulaGames.RPGWorld.Interfaces.I_NGPlugin>());
                    var _GetObjects = ObjectHierarchy.Where(x => x.InterfaceName == TypeName).Select(x => x.BaseInstance);

                    foreach (var o in _GetObjects)
                    {
                        var iplugin = (I_NGPlugin)o;
                        ObjectCache["NebulaGames.RPGWorld.Interfaces.I_NGPlugin"].Add(iplugin);
                    }
                }

                return ObjectCache["NebulaGames.RPGWorld.Interfaces.I_NGPlugin"].ToList();
            }
            else
            {
                LoadAllObjects(HelperDirectory);
                Loaded = true;
                return GetObjects(TypeName, HelperDirectory);
            }
        }
    }
}
