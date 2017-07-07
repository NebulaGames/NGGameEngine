using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GameData = NebulaGames.RPGWorld.GameData;
using NebulaGames.RPGWorld.Structs;

namespace NebulaGames.RPGWorld.Interfaces
{
    public interface I_NGWindow : IGameComponent, IUpdateable, IComparable<I_NGWindow>, IDisposable, IDrawable
    {
        string WindowName { get; }
        string ID { get; }
        List<I_NGWindowObject> WindowObjects { get; }
        List<Structs.DataPacket> AddWindowObject(string ObjectID);        
        List<Structs.DataPacket> InitWindow(Game mGame);
        List<TextData> Text { get; set; }
        List<StaticTextureData> StaticTextures { get; set; }
        void ProcessMouseClick(Vector2 MouseLocation, bool PiercingClick = false);
        string Code { get; set; }
        System.Reflection.Assembly CompiledCode { get; set; }


    }
}
