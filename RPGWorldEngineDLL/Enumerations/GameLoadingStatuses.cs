using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebulaGames.RPGWorld.Enumerations
{
    [Flags()]
    public enum GameLoadingStatuses
    {
        SystemDataLoaded = 1,
        SystemImagesLoaded = 2,
        GameDataLoaded = 4,
        GameImagesLoaded = 8,
        PluginsLoaded = 16,
        PluginImagesLoaded = 32
    }
}
