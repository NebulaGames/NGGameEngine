using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NebulaGames.RPGWorld.Enumerations.RPGGame;

namespace NebulaGames.RPGWorld.Interfaces.RPGGame
{
    public interface I_Skill
    {
        // Public Cloud Data
        string Author { get; }
        string LicenseInformation { get; }
        Guid ID { get; }

        // Settable Properties
        string Name { get; set; }
        string Description { get; set; }
        SkillTypes SkillType { get; set; }
        string CustomSkillType { get; set; }


    }
}
