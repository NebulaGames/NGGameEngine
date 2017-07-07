using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebulaGames.RPGWorld.Enumerations
{
    public static class EnumerationHelper
    {
        public static string GetText(object enumObject)
        {
            if (enumObject is RPGGame.SkillScopes)            
            {
                return RPGGame.SkillScopesText.GetText((RPGGame.SkillScopes)enumObject);
            }
            return "";
        }
    }
}
