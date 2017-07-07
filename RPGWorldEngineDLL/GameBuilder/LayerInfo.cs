using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NebulaGames.RPGWorld;

namespace NebulaGames.RPGWorld.GameBuilder
{
    public class LayerInfo
    {
        public string Name;
        public bool Visible;
        public Enumerations.LayerType LayerType;
        public int ZPosition;
        public static Dictionary<string, LayerInfo> CreateDefault()
        {
            Dictionary<string, LayerInfo> _TmpReturn = new Dictionary<string, LayerInfo>();

            LayerInfo _Back = new LayerInfo();
            _Back.Name = "Background";
            _Back.Visible = true;
            _Back.LayerType = Enumerations.LayerType.Background;

            LayerInfo _Passable = new LayerInfo();
            _Passable.Name = "Passable";
            _Passable.Visible = true;
            _Passable.LayerType = Enumerations.LayerType.Passable;

            _TmpReturn.Add("Background", _Back);
            _TmpReturn.Add("Passable", _Passable);

            return _TmpReturn;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
