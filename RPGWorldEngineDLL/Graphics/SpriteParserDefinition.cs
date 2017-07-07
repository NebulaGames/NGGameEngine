using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GG2DLib.ResourceManager
{
    public class SpriteParserDefinitionManager
    {
        public struct PositionDefinition
        {
            public int X;
            public int Y;
            public int Width;
            public int Height;
            public string Name;
        }

        public PositionDefinition[] Definitions;
    }
}
