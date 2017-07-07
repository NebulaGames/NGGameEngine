using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebulaGames.RPGWorld
{
    public static class PluginManager
    {
        /// <summary>
        /// Contains all of the Plugins Loaded Into The System
        /// </summary>
        public static Dictionary<string, Data.NGPluginConfig> PluginConfiguration = new Dictionary<string, Data.NGPluginConfig>();

        /// <summary>
        /// Load All The Appropriate Plugins
        /// </summary>
        public static void LoadPlugins()
        {            
            foreach(var plugin in GameData.GameConfiguration.Plugins.Plugins.Where(x=>x.GameName.ToLower() == GameData.ActiveGame.ToLower() || x.GameName.ToLower() == "*").OrderBy(x=>x.OrderInt))
            {
                System.Xml.Serialization.XmlSerializer _Serializer = new System.Xml.Serialization.XmlSerializer(typeof(Data.GameConfiguration.NGGameConfiguration));
                using (var _reader = new System.IO.StreamReader(new System.IO.MemoryStream(Helper.IO.GetFile(GameData.BaseDirectory.EnsureDirectoryFormat() + "Data\\Plugins\\" + plugin.SourceName + "\\pluginconfig.xml"))))
                {
                    var _TmpData = (Data.NGPluginConfig)_Serializer.Deserialize(_reader);
                    if (_TmpData == null) { throw new Exception("Error Loading The Game Config"); }
                    PluginConfiguration.Add(plugin.SourceName, _TmpData);
                }
                _Serializer = null;
            }
        }


    }
}
