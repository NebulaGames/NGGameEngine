using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebulaGames.RPGWorld.Enumerations
{
    public enum BrushShapes
    {
        Circle,
        Square
    }
    public enum CurrentEditorMode
    {
        Paint = 1,
        Fill = 2,
        PlaceObject = 3,
        NonPassable = 4
    }
    [Flags]
    public enum EditorPaintMode
    {
        Paint,
        Select
    }

    [Flags]
    public enum EditorMode
    {
        Background,
        Passable,
        Object
    }
    [Flags]
    public enum LayerType
    {
        Background,
        Passable,
        Objects,
        BoundingBox,
        Triggers
    }
    public enum LogLocation
    {
        File,
        MSSQL
    }

    public enum RelativePosition
    {
        Main = 4,
        Left = 3,
        TopLeft = 0,
        Top = 1,
        TopRight = 2, Right = 5, BottomRight = 8, Bottom = 7, BottomLeft = 6
    }

}
