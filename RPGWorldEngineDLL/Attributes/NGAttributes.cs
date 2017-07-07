using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NebulaGames.RPGWorld.Enumerations;

namespace NebulaGames.RPGWorld.Attributes
{
    [System.AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class SystemTypesSupported : Attribute
    {        
        
        public SystemTypesEnum SystemTypes;
        public bool ValidConfiguration = false;

        // This is a positional argument
        public SystemTypesSupported(SystemTypesEnum systemTypes)
        {
            this.SystemTypes = systemTypes;
            
            if ((systemTypes & SystemTypesEnum.WINDOWS) == SystemTypesEnum.WINDOWS)
            {
                ValidConfiguration = true;
            }
            else if ((systemTypes & SystemTypesEnum.ANDROID) == SystemTypesEnum.ANDROID)
            {
                ValidConfiguration = true;
            }
            else if ((systemTypes & SystemTypesEnum.IOS) == SystemTypesEnum.IOS)
            {
                ValidConfiguration = true;
            }

        }
        
    }
}
