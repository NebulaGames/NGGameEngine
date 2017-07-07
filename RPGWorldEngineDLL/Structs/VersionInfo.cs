using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebulaGames.RPGWorld.Structs
{
    public class VersionInfo
    {
        public Byte Major;
        public Byte Minor;
        public Byte Build;
        public bool Release;
        public bool Expirimental;
        public Dictionary<int, char> CompatibilityHistory;
        public string[] Authors;
        public DateTime BuildDate;
        public string Comments;
    }
}
