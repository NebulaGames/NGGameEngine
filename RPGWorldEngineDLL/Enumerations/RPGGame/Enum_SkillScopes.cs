using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebulaGames.RPGWorld.Enumerations.RPGGame
{
    [Flags()]
    public enum SkillScopes
    {        
        Self = 1,
        Ally = 2,
        Enemy = 4,
        Object = 8,
        Alive = 16,
        Dead = 32,
        None = 64
    }

    internal static class SkillScopesText
    {
        public static string GetText(SkillScopes scope)
        {
            string _TmpReturn = "";

            if ((scope & SkillScopes.None) == SkillScopes.None) { _TmpReturn += "None,"; }
            if ((scope & SkillScopes.Self) == SkillScopes.Self) { _TmpReturn += "Self,"; }
            if ((scope & SkillScopes.Ally) == SkillScopes.Ally) { _TmpReturn += "Ally,"; }
            if ((scope & SkillScopes.Enemy) == SkillScopes.Enemy) { _TmpReturn += "Enemy,"; }
            if ((scope & SkillScopes.Object) == SkillScopes.Object) { _TmpReturn += "Object,"; }
            if ((scope & SkillScopes.Alive) == SkillScopes.Alive) { _TmpReturn += "Alive,"; }
            if ((scope & SkillScopes.Dead) == SkillScopes.Dead) { _TmpReturn += "Dead,"; }

            return _TmpReturn.Substring(0, _TmpReturn.Length - 1);
        }
    }
}
