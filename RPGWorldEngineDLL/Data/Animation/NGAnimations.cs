using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace NebulaGames.RPGWorld.Data
{
    [XmlRoot(ElementName = "animations")]
    public class NGAnimations
    {       
        [XmlElement(ElementName = "animation")]
        public List<NGAnimation> Animations { get; set; }
                
        public void AddAnimation(NGAnimation NewAnimation)
        {
            if (Animations.Where(x => x.IDValue == NewAnimation.IDValue).Count() > 0)
            {
                int _IPos = RemoveAnimation(NewAnimation.IDValue);

                if (_IPos != -1) { Animations.Insert(_IPos, NewAnimation); }
                else { Animations.Add(NewAnimation); }
            }
            else
            {
                Animations.Add(NewAnimation);
            }

            UpdateCache();
        }

        public int RemoveAnimation(UInt64 ID)
        {
            int _FoundIndex = -1;
            for (int iPos = 0; iPos < Animations.Count; iPos++)
            {
                if (Animations[iPos].IDValue == ID) { _FoundIndex = iPos; }
            }
            if (_FoundIndex > -1)
            {
                Animations.RemoveAt(_FoundIndex);
            }
            UpdateCache();
            return _FoundIndex;
        }

        private Dictionary<string, int> _AnimationNameToIndexCache = new Dictionary<string, int>();

        private void UpdateCache()
        {
            _AnimationNameToIndexCache.Clear();

            for(int iPos=0; iPos < Animations.Count(); iPos++)
            {
                _AnimationNameToIndexCache.Add(Animations[iPos].Name, iPos);
            }
        }

        public NGAnimation GetByName(string Name)
        {
            NGAnimation _TmpReturn = null;
            _TmpReturn = Animations[_AnimationNameToIndexCache[Name]];
            return _TmpReturn;
        }
    }
}
