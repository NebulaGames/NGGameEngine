using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using NebulaGames.RPGWorld.MonoGame.Extensions;
using ACT.Core.Extensions;
using NebulaGames.RPGWorld.Graphics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace NebulaGames.RPGWorld.GameBuilder
{

    public class EditorWorld
    {
        #region Fields (9)

        private bool _Changed = false;

        public bool Changed
        {
            get { return _Changed; }
            set { _Changed = value; }
        }

        /// <summary>
        /// This is the current Image that the editor viewing.  This can Span Multiple Screens
        /// </summary>
        private Image _BackBufferImage;
        /// <summary>
        /// This is the Graphics Object that The Editor Uses To Perform All Drawing Actions This Points to the _BackBufferImage
        /// </summary>
        private System.Drawing.Graphics _G;

        private Image _WorkingLayerImage;
        private System.Drawing.Graphics _WorkingLayerGraphics;

        public System.Drawing.Color NonPassableColor = System.Drawing.ColorTranslator.FromHtml("#00FF32");

        /// <summary>
        /// Image For Writing A Single Screen To
        /// </summary>
        private Image _SingleScreenImage = new Bitmap(800, 600);
        private System.Drawing.Graphics _SingleScreenGraphicsObject;
        /// <summary>
        /// This is the MiniMap Buffer Image
        /// </summary>
        private Image _MiniMapBufferImage = new Bitmap(800 * 3, 600 * 3);
        /// <summary>
        /// This is the MiniMap Buffer Graphics Object
        /// </summary>
        private System.Drawing.Graphics _MiniMapBufferGraphics;
        /// <summary>
        /// This is a Reference to the Base Directory Of The Application
        /// </summary>
        private string _BaseDirectory, _ApplicationDirectory, _FileName, _LayersDirectory, _ObjectDirectory;
        /// <summary>
        /// Because every Editor Screen is always 800,600 pixels in size when drawing the Current Screen 
        /// to the Editor we will need to offset the drawing methods by these numbers depending on the editor width/height
        /// </summary> 
        public int OffsetX, OffsetY, EditorScreenWidth, EditorScreenHeight;
        /// <summary>
        /// this is the Currently Selected Map Object
        /// </summary>
        public Guid SelectedMapObject = Guid.Empty;

        /// <summary>
        /// The World Name
        /// </summary>
        public string WorldName;
        /// <summary>
        /// The Author Name
        /// </summary>
        public string AuthorName;

        /// <summary>
        /// World File Name
        /// </summary>
        public string WorldFileName
        {
            get
            {
                if (_FileName == "")
                {
                    _FileName = WorldName.EnsureValidWindowsFileName("-");
                }
                return _FileName;
            }
        }

        /// <summary>
        /// World
        /// </summary>
        public string WorldFileFullPath
        {
            get
            {
                return BaseDirectory.EnsureDirectoryFormat() + WorldFileName;
            }
        }

        /// <summary>
        /// Sets the FileName.  Throws An Error On Any Invalid Location
        /// </summary>
        /// <param name="FullPathToFile"></param>
        public void SetFileName(string FullPathToFile)
        {
            if (FullPathToFile.GetDirectoryFromFileLocation().DirectoryExists() == false)
            {
                throw new System.IO.DirectoryNotFoundException("Invalid Directory Specified");
            }

            _FileName = FullPathToFile.GetFileNameFromFullPath();
            _BaseDirectory = FullPathToFile.GetDirectoryFromFileLocation();

            // SAVE
        }

        /// <summary>
        /// This is the Editors Screens.  Every Screen Represents a World Location. 
        /// </summary>
        public Dictionary<string, EditorScreen> Screens = new Dictionary<string, EditorScreen>();
        /// <summary>
        /// This is the Editors Layers.  By Default There Are Always 2 Layers Passable, and Background
        /// </summary>
        private Dictionary<string, LayerInfo> _Layers = new Dictionary<string, LayerInfo>();

        /// <summary>
        /// Current Working Layer
        /// </summary>
        private string _CurrentLayer;

        public string CurrentLayer
        {
            get { return _CurrentLayer; }
            set
            {
                if (_Layers.ContainsKey(value) == false)
                {
                    ErrorLogable.LogError(null, "Error Setting Layer: " + value, ACT.Core.Enums.ErrorLevel.Warning);
                    return;
                }
                _CurrentLayer = value;
            }
        }

        #endregion Fields

        #region Properties
        public Dictionary<string, LayerInfo> Layers
        {
            get
            {
                return _Layers;
            }
        }
        public string BaseDirectory
        {
            get
            {
                return _BaseDirectory;
            }
        }

        public System.Drawing.Graphics WorkingLayerGraphics
        {
            get
            {
                return _WorkingLayerGraphics;
            }
        }
        #endregion

        #region Constructors (2)
        /// <summary>
        /// Empty Constructor For New Worlds
        /// </summary>
        /// <param name="name">World Name</param>
        /// <param name="EditorWidth">Editor Width</param>
        /// <param name="EditorHeight">Editor Height</param>
        public EditorWorld(string name, int EditorWidth, int EditorHeight)
        {
            WorldName = name;
            _Layers = LayerInfo.CreateDefault();

            EditorScreen _TmpScreen = new EditorScreen(0, 0, this, true);
            Screens.Add(GetKey(0, 0, Enumerations.RelativePosition.Main), _TmpScreen);

            EditorScreenWidth = EditorWidth;
            EditorScreenHeight = EditorHeight;
            OffsetX = (EditorScreenWidth - 800) / 2;
            OffsetY = (EditorScreenHeight - 600) / 2;

            _BackBufferImage = new Bitmap(EditorScreenWidth, EditorScreenHeight);
            _WorkingLayerImage = new Bitmap(800, 600);

            // _G = The Whole Map Drawn Including the Sides
            _G = System.Drawing.Graphics.FromImage(_BackBufferImage);
            // _WorkingLayerGraphics = Current Layer Being Edited (Also used for Saving Screen Layer Info)
            _WorkingLayerGraphics = System.Drawing.Graphics.FromImage(_WorkingLayerImage);
            // _MiniMapBufferGraphics = Minimap Image Used For Displaying the Minimap
            _MiniMapBufferGraphics = System.Drawing.Graphics.FromImage(_MiniMapBufferImage);
            // _SingleScreenGraphicsObject = Single Screen (Main) Drawn.  Used For Updating Only The Editing Part
            _SingleScreenGraphicsObject = System.Drawing.Graphics.FromImage(_SingleScreenImage);
        }

        public EditorWorld(string FileName, int EditorWidth, int EditorHeight, Point FocusPoint)
        {
            if (System.IO.File.Exists(FileName) == false) { throw new Exception("World File Not Found"); }

            _ApplicationDirectory = AppDomain.CurrentDomain.BaseDirectory.EnsureDirectoryFormat();
            _BaseDirectory = FileName.Substring(0, FileName.LastIndexOf("\\") + 1).EnsureDirectoryFormat();
            _LayersDirectory = _BaseDirectory + "layers\\";
            _ObjectDirectory = _BaseDirectory + "objects\\";

            // LOAD FILE NEW FORMAT
             dynamic Settings = ACT.Core.Dynamic.Encoder.LoadFromFile(FileName);

             NonPassableColor = System.Drawing.ColorTranslator.FromHtml(Settings.nonpassablecolor);

            #region Load The Layers
            foreach (string name in Settings.layers.GetMemberNames)
            {
                if (name == "name") { continue; }
                dynamic _TmpLayerData = Settings.layers.GetObject(name);
                LayerInfo _NewLayer = new LayerInfo();
                _NewLayer.Name = _TmpLayerData.Name;
                _NewLayer.Visible = Convert.ToBoolean(_TmpLayerData.visible);
                _NewLayer.LayerType = Enum.Parse(typeof(Enumerations.LayerType), _TmpLayerData.type);
                _Layers.Add(_NewLayer.Name, _NewLayer);
            }
            #endregion

            #region Load The Screens
            foreach (string name in Settings.screens.GetMemberNames)
            {

                if (name == "name") { continue; }
                dynamic _TmpScreenData = Settings.screens.GetObject(name);

                string[] PositionData = name.SplitString("-", StringSplitOptions.RemoveEmptyEntries);
                if (PositionData.Length != 2)
                {
                    ErrorLogable.LogError(null, "Invalid PositionData-Name", ACT.Core.Enums.ErrorLevel.Severe);
                    continue;
                }
                EditorScreen _NewScreen = new EditorScreen(Convert.ToInt32(PositionData[0]), Convert.ToInt32(PositionData[1]), this);

                #region Load the Objects
                foreach (string objname in _TmpScreenData.objects.GetMemberNames)
                {
                    if (objname == "name") { continue; }
                    dynamic MapObjectInfo = _TmpScreenData.objects.GetObject(objname);
                    try
                    {
                        MapObject _NewMapObject = new MapObject();
                        _NewMapObject.ID = new Guid(objname.Trim());
                        string _MapImageLocation = _ObjectDirectory + _NewMapObject.ID + ".png";

                        if (!System.IO.File.Exists(_MapImageLocation))
                        {
                            ErrorLogable.LogError(null, "Error Locating Object: " + _NewMapObject.ID.ToString(), ACT.Core.Enums.ErrorLevel.Critical);
                            continue;
                        }

                        string _Location = MapObjectInfo.location;
                        string _Size = MapObjectInfo.size;

                        string[] _Points = _Location.SplitString(",", StringSplitOptions.RemoveEmptyEntries);
                        string[] _Sizes = _Location.SplitString(",", StringSplitOptions.RemoveEmptyEntries);

                        _NewMapObject.DrawLocation = new Point(Convert.ToInt32(_Points[0]), Convert.ToInt32(_Points[1]));
                        _NewMapObject.ImageSize = new Size(Convert.ToInt32(_Sizes[0]), Convert.ToInt32(_Sizes[1]));
                        _NewMapObject.Position = Enum.Parse(typeof(NebulaGames.RPGWorld.Enumerations.RelativePosition), MapObjectInfo.relativeposition);
                        _NewMapObject.ResourceName = "";
                        _NewMapObject.Screen = _NewScreen;
                        _NewScreen.MapObjects.Add(_NewMapObject);

                        Graphics.CompressibleImage _TmpMapObject = new CompressibleImage(Image.FromFile(_MapImageLocation), System.Drawing.Imaging.ImageFormat.Png);

                        ResourceManager.AddObject(_NewMapObject.ID.ToString(), _TmpMapObject);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogable.LogError(ex, "Error Loading Object: " + name, ACT.Core.Enums.ErrorLevel.Critical);
                    }
                }
                #endregion

                #region Load the Layer Images
                foreach (string TmpLayerName in _Layers.Keys)
                {
                    if (System.IO.File.Exists(_LayersDirectory + TmpLayerName + "\\" + name + ".png"))
                    {
                        _NewScreen.AddUpdateLayerImage(TmpLayerName.ToLower(), Image.FromFile(_LayersDirectory + TmpLayerName + "\\" + name + ".png"));
                    }
                }
                #endregion

                Screens.Add(name, _NewScreen);
            }
            #endregion

            EditorScreenWidth = EditorWidth;
            EditorScreenHeight = EditorHeight;

            OffsetX = (EditorScreenWidth - 800) / 2;
            OffsetY = (EditorScreenHeight - 600) / 2;

            _BackBufferImage = new Bitmap(EditorScreenWidth, EditorScreenHeight);
            _G = System.Drawing.Graphics.FromImage(_BackBufferImage);
            _MiniMapBufferGraphics = System.Drawing.Graphics.FromImage(_MiniMapBufferImage);
            _SingleScreenGraphicsObject = System.Drawing.Graphics.FromImage(_SingleScreenImage);
        }

        #endregion Constructors

        #region Methods (12)

        #region Layer Manipulation
        /// <summary>
        /// Adds A Layer To The World.  Note: All Layers Are Added To The End Call Reorder Layer Method To Change Layer Order.
        /// </summary>
        /// <param name="Name">Layer Name</param>
        /// <param name="Visible">If It Is Visible</param>
        /// <returns></returns>
        public bool AddLayer(LayerInfo NewLayer)
        {
            if (_Layers.ContainsKey(NewLayer.Name.ToLower()) == true) { ErrorLogable.LogError(null, "Duplicate Layer Found"); return false; }

            _Layers.Add(NewLayer.Name.ToLower(), NewLayer);

            return true;
        }

        /// <summary>
        /// Sets the Working Layer
        /// </summary>
        /// <param name="LayerName"></param>
        /// <param name="X">Map X Location</param>
        /// <param name="Y">Map Y Location</param>
        public void SetWorkingLayer(string LayerName, int X, int Y)
        {
            string _Key = GetKey(X, Y, Enumerations.RelativePosition.Main);

            if (!Screens.ContainsKey(_Key))
            {
                EditorScreen _New = new EditorScreen(X, Y, this, true);
                Screens.Add(_Key, _New);
            }

            _WorkingLayerImage = Screens[_Key].GetLayerImage(LayerName);
            if (_WorkingLayerGraphics != null) { _WorkingLayerGraphics.Dispose(); }
            _WorkingLayerGraphics = System.Drawing.Graphics.FromImage(_WorkingLayerImage);
            
            _CurrentLayer = LayerName;
        }

        public void UpdateWorkingLayer(int X, int Y)
        {
            string _Key = GetKey(X, Y, Enumerations.RelativePosition.Main);

            if (!Screens.ContainsKey(_Key))
            {
                ErrorLogable.LogError(null, "The Screen was Not Found: " + _Key, ACT.Core.Enums.ErrorLevel.Critical);
                throw new Exception("The Specified Screen Was Not Found");
            }

            Screens[_Key].UpdateLayerImage(_CurrentLayer, _WorkingLayerImage);
        }

        #endregion

        #region Size / Location / Position Info - Manipulation
        /// <summary>
        /// Change The Editor Size
        /// </summary>
        /// <param name="EditorWidth">Editors Panel Width</param>
        /// <param name="EditorHeight">Editors Panel Height</param>
        public void ChangeEditorSize(int EditorWidth, int EditorHeight)
        {
            EditorScreenWidth = EditorWidth;
            EditorScreenHeight = EditorHeight;

            //  lock (this)
            //  {
            _BackBufferImage = new Bitmap(EditorScreenWidth, EditorScreenHeight);
            _G = System.Drawing.Graphics.FromImage(_BackBufferImage);

            OffsetX = (EditorScreenWidth - 800) / 2;
            OffsetY = (EditorScreenHeight - 600) / 2;
            //  }          
        }

        /// <summary>
        /// The The Max X,Y and Return it as a Point
        /// </summary>
        /// <returns>Point</returns>
        public Point GetMax()
        {
            int _x = 0, _y = 0;
            foreach (var S in Screens.Keys)
            {
                var P = ParseScreenKey(S);
                if (P.X > _x) { _x = P.X; }
                if (P.Y > _y) { _y = P.Y; }
            }

            return new System.Drawing.Point(_x, _y);
        }

        /// <summary>
        /// Get the Min X,Y Return as a Point
        /// </summary>
        /// <returns>System.Drawing.Point</returns>
        public Point GetMin()
        {
            int _x = 0, _y = 0;
            foreach (var S in Screens.Keys)
            {
                var P = ParseScreenKey(S);
                if (P.X < _x) { _x = P.X; }
                if (P.Y < _y) { _y = P.Y; }
            }

            return new System.Drawing.Point(_x, _y);
        }
        #endregion

        #region Drawing Methods (Whole Map / MiniMap / Single Screen / Current Layer)

        /// <summary>
        /// Draw A Single Screen That Which Is 800,600
        /// </summary>
        /// <param name="X">Position X In The World</param>
        /// <param name="Y">Position Y In The World</param>
        /// <returns></returns>
        public Image DrawLocation_SingleScreen(int X, int Y, bool DrawObjects)
        {
            _SingleScreenGraphicsObject.Clear(Color.FromArgb(0, 255, 0));
            #region Draw Each Of The Layers
            foreach (var Layer in _Layers)
            {
                if (Layer.Value.Visible)
                {
                    if (Screens.ContainsKey(GetKey(X, Y, Enumerations.RelativePosition.Main)))
                    {

                        _SingleScreenGraphicsObject.DrawImage(Screens[GetKey(X, Y, Enumerations.RelativePosition.Main)].GetLayerImage(Layer.Key.ToLower()), new Point(0, 0));
                    }
                }
            }
            #endregion

            if (DrawObjects)
            {
                #region Draw Objects On Screen
                var AllObjs = from QMapObject in Screens.Values.SelectMany(x => x.MapObjects)
                              where QMapObject.Screen.InRange(X, Y, OffsetX, OffsetY)
                              orderby QMapObject.DrawLocation.Y
                              select new { QMapObject, QMapObject.Screen.X, QMapObject.Screen.Y };

                foreach (var MapO in AllObjs)
                {
                    Enumerations.RelativePosition _RPos = EditorScreen.GetRelativePos(X, Y, MapO.X, MapO.Y);

                    if (_RPos == Enumerations.RelativePosition.Main)
                    {
                        Point _DrawPoint = new Point(MapO.QMapObject.DrawLocation.X, MapO.QMapObject.DrawLocation.Y);
                        _SingleScreenGraphicsObject.DrawImage(ResourceManager.GetMapObjectImageStart(MapO.QMapObject.ID.ToString()), _DrawPoint);
                    }
                }
                #endregion

                #region Draw The Selected Object

                if (SelectedMapObject != Guid.Empty)
                {
                    var SelectedObjs = from QMapObject in Screens.Values.SelectMany(x => x.MapObjects)
                                       where QMapObject.ID == SelectedMapObject
                                       select QMapObject;
                    var FirstObject = SelectedObjs.ToList()[0];

                    _SingleScreenGraphicsObject.DrawRectangle(Pens.White, new Rectangle(new Point(FirstObject.DrawLocation.X + OffsetX, FirstObject.DrawLocation.Y + OffsetY), FirstObject.ImageSize));

                }

                #endregion
            }
            // Draw The Bounding Box for Refrence
            _SingleScreenGraphicsObject.DrawRectangle(Pens.Black, new Rectangle(-1, -1, 800, 600));

            return _SingleScreenImage;
        }

        /// <summary>
        /// Draws the specified Location.  This Should Never Be Called Consecutavly.  The Editor Only calls It
        /// One Time The After It Updates The Editor Screens With Any changes.  Also On A resize or A Paint
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public Image DrawLocation(int X, int Y)
        {
            _G.Clear(Color.FromArgb(255, 255, 255));
            #region Draw Each Of The Layers
            foreach (var Layer in _Layers)
            {
                if (Layer.Value.Visible)
                {
                    if (Screens.ContainsKey(GetKey(X, Y, Enumerations.RelativePosition.Main)))
                    {
                        _G.DrawImage(Screens[GetKey(X, Y, Enumerations.RelativePosition.Main)].GetLayerImage(Layer.Key.ToLower()), new Point(OffsetX, OffsetY));
                    }
                    if (Screens.ContainsKey(GetKey(X, Y, Enumerations.RelativePosition.Left)))
                    {
                        _G.DrawImage(Screens[GetKey(X, Y, Enumerations.RelativePosition.Left)].GetLayerImage(Layer.Key.ToLower()), new Rectangle(0, OffsetY, OffsetX, 600), new Rectangle(800 - OffsetX, 0, OffsetX, 600), GraphicsUnit.Pixel);
                    }
                    if (Screens.ContainsKey(GetKey(X, Y, Enumerations.RelativePosition.Right)))
                    {
                        _G.DrawImage(Screens[GetKey(X, Y, Enumerations.RelativePosition.Right)].GetLayerImage(Layer.Key.ToLower()), new Rectangle(800 + OffsetX, OffsetY, OffsetX, 600), new Rectangle(0, 0, OffsetX, 600), GraphicsUnit.Pixel);
                    }
                    if (Screens.ContainsKey(GetKey(X, Y, Enumerations.RelativePosition.Bottom)))
                    {
                        _G.DrawImage(Screens[GetKey(X, Y, Enumerations.RelativePosition.Bottom)].GetLayerImage(Layer.Key.ToLower()), new Rectangle(OffsetX, 600 + OffsetY, 800, OffsetY), new Rectangle(0, 0, 800, OffsetY), GraphicsUnit.Pixel);
                    }
                    if (Screens.ContainsKey(GetKey(X, Y, Enumerations.RelativePosition.Top)))
                    {
                        _G.DrawImage(Screens[GetKey(X, Y, Enumerations.RelativePosition.Top)].GetLayerImage(Layer.Key.ToLower()), new Rectangle(OffsetX, 0, 800, OffsetY), new Rectangle(0, 600 - OffsetY, 800, OffsetY), GraphicsUnit.Pixel);
                    }
                    if (Screens.ContainsKey(GetKey(X, Y, Enumerations.RelativePosition.TopLeft)))
                    {
                        _G.DrawImage(Screens[GetKey(X, Y, Enumerations.RelativePosition.TopLeft)].GetLayerImage(Layer.Key.ToLower()), new Rectangle(0, 0, OffsetX, OffsetY), new Rectangle(800 - OffsetX, 600 - OffsetY, OffsetX, OffsetY), GraphicsUnit.Pixel);
                    }
                    if (Screens.ContainsKey(GetKey(X, Y, Enumerations.RelativePosition.TopRight)))
                    {
                        _G.DrawImage(Screens[GetKey(X, Y, Enumerations.RelativePosition.TopRight)].GetLayerImage(Layer.Key.ToLower()), new Rectangle(800 + OffsetX, 0, OffsetX, OffsetY), new Rectangle(0, 600 - OffsetY, OffsetX, OffsetY), GraphicsUnit.Pixel);
                    }
                    if (Screens.ContainsKey(GetKey(X, Y, Enumerations.RelativePosition.BottomRight)))
                    {
                        _G.DrawImage(Screens[GetKey(X, Y, Enumerations.RelativePosition.BottomRight)].GetLayerImage(Layer.Key.ToLower()), new Rectangle(800 + OffsetX, 600 + OffsetY, OffsetX, OffsetY), new Rectangle(0, 0, OffsetX, OffsetY), GraphicsUnit.Pixel);
                    }
                    if (Screens.ContainsKey(GetKey(X, Y, Enumerations.RelativePosition.BottomLeft)))
                    {
                        _G.DrawImage(Screens[GetKey(X, Y, Enumerations.RelativePosition.BottomLeft)].GetLayerImage(Layer.Key.ToLower()), new Rectangle(0, 600 + OffsetY, OffsetX, OffsetY), new Rectangle(800 - OffsetX, 0, OffsetX, OffsetY), GraphicsUnit.Pixel);
                    }
                }
            }
            #endregion

            #region Draw The Objects

            var AllObjs = from QMapObject in Screens.Values.SelectMany(x => x.MapObjects)
                          where QMapObject.Screen.InRange(X, Y, OffsetX, OffsetY)
                          orderby QMapObject.DrawLocation.Y
                          select new { QMapObject, QMapObject.Screen.X, QMapObject.Screen.Y };

            foreach (var MapO in AllObjs)
            {
                Enumerations.RelativePosition _RPos = EditorScreen.GetRelativePos(X, Y, MapO.X, MapO.Y);

                if (_RPos == Enumerations.RelativePosition.Main)
                {
                    Point _DrawPoint = new Point(MapO.QMapObject.DrawLocation.X + OffsetX, MapO.QMapObject.DrawLocation.Y + OffsetY);
                    _G.DrawImage(ResourceManager.GetMapObjectImageStart(MapO.QMapObject.ID.ToString()), _DrawPoint);
                }
                else if (_RPos == Enumerations.RelativePosition.Right)
                {
                    Point _DrawPoint = new Point(MapO.QMapObject.DrawLocation.X + 800 + OffsetX, MapO.QMapObject.DrawLocation.Y + OffsetY);
                    _G.DrawImage(ResourceManager.GetMapObjectImageStart(MapO.QMapObject.ID.ToString()), _DrawPoint);
                }
                else if (_RPos == Enumerations.RelativePosition.Left)
                {
                    Point _DrawPoint = new Point(MapO.QMapObject.DrawLocation.X - 800, MapO.QMapObject.DrawLocation.Y + OffsetY);
                    _G.DrawImage(ResourceManager.GetMapObjectImageStart(MapO.QMapObject.ID.ToString()), _DrawPoint);
                }
                else if (_RPos == Enumerations.RelativePosition.Top)
                {
                    Point _DrawPoint = new Point(MapO.QMapObject.DrawLocation.X + OffsetX, MapO.QMapObject.DrawLocation.Y - 600);
                    _G.DrawImage(ResourceManager.GetMapObjectImageStart(MapO.QMapObject.ID.ToString()), _DrawPoint);
                }
                else if (_RPos == Enumerations.RelativePosition.Bottom)
                {
                    Point _DrawPoint = new Point(MapO.QMapObject.DrawLocation.X + OffsetX, MapO.QMapObject.DrawLocation.Y + 600);
                    _G.DrawImage(ResourceManager.GetMapObjectImageStart(MapO.QMapObject.ID.ToString()), _DrawPoint);
                }
            }



            #endregion

            #region Draw The Selected Object

            if (SelectedMapObject != Guid.Empty)
            {
                var SelectedObjs = from QMapObject in Screens.Values.SelectMany(x => x.MapObjects)
                                   where QMapObject.ID == SelectedMapObject
                                   select QMapObject;
                var FirstObject = SelectedObjs.ToList()[0];

                _G.DrawRectangle(Pens.White, new Rectangle(new Point(FirstObject.DrawLocation.X + OffsetX, FirstObject.DrawLocation.Y + OffsetY), FirstObject.ImageSize));

            }

            #endregion

            // Draw The Bounding Box for Refrence
            _G.DrawRectangle(Pens.Black, new Rectangle(OffsetX - 1, OffsetY - 1, 800, 600));

            return _BackBufferImage;
        }

        /// <summary>
        /// Draws A MiniMap For The Editor To Use
        /// </summary>
        /// <param name="LayersToDraw"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public Image DrawMiniMap(Enumerations.LayerType LayersToDraw, int X, int Y)
        {
            if (_MiniMapBufferGraphics == null)
            {
                _MiniMapBufferGraphics = System.Drawing.Graphics.FromImage(_MiniMapBufferImage);
            }

            _MiniMapBufferGraphics.Clear(Color.White);
            DrawLocation(X, Y);
            _MiniMapBufferGraphics.DrawImage(_BackBufferImage, new Point(0, 0));
            return _MiniMapBufferImage;
        }

        #endregion

        #region Static Methods Like GetKey(int,int, P)
        /// <summary>
        /// Gets the String Representation Of The X,Y Coordinates and the Relative Position
        /// </summary>
        /// <param name="X">World X</param>
        /// <param name="Y">World Y</param>
        /// <param name="P">Visible X,Y Relative Position To X,Y</param>
        /// <returns></returns>
        public static string GetKey(int X, int Y, Enumerations.RelativePosition P)
        {
            int _tx, _ty;
            _tx = X;
            _ty = Y;

            StringBuilder _SB = new StringBuilder();

            if (P == Enumerations.RelativePosition.Left)
            {
                _tx--;
            }
            else if (P == Enumerations.RelativePosition.Bottom)
            {
                _ty++;
            }
            else if (P == Enumerations.RelativePosition.Right)
            {
                _tx++;
            }
            else if (P == Enumerations.RelativePosition.Top)
            {
                _ty--;
            }
            else if (P == Enumerations.RelativePosition.TopLeft)
            {
                _tx--; _ty--;
            }
            else if (P == Enumerations.RelativePosition.TopRight)
            {
                _tx++;
                _ty--;
            }
            else if (P == Enumerations.RelativePosition.BottomRight)
            {
                _tx++;
                _ty++;
            }
            else if (P == Enumerations.RelativePosition.BottomLeft)
            {
                _tx--; _ty++;
            }

            _SB.Append(_tx);
            _SB.Append("-");
            _SB.Append(_ty);

            return _SB.ToString();
        }
        #endregion



       

        /// <summary>
        /// Makes the Passable Image Transparent.
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        public void MakePassableTransparent(int X, int Y)
        {
            Bitmap _tmp = new Bitmap(Screens[GetKey(X, Y, Enumerations.RelativePosition.Main)].GetLayerImage("passable"));
            System.Drawing.Graphics _TmpG = System.Drawing.Graphics.FromImage(Screens[GetKey(X, Y, Enumerations.RelativePosition.Main)].GetLayerImage("passable"));

            _tmp.MakeTransparent(Color.FromArgb(0, 255, 0));
            _TmpG.Clear(Color.Transparent);
            _TmpG.DrawImage((Image)_tmp, 0, 0);

            _tmp.Dispose();
            _TmpG.Dispose();
        }

        /// <summary>
        /// Parse the Screen Key Return a Point
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static Point ParseScreenKey(string Key)
        {
            int _FIndex = Key.IndexOf("-");
            if (_FIndex == 0) { _FIndex = Key.IndexOf("-", 1); }
            Point _TmpReturn = new System.Drawing.Point(Convert.ToInt32(Key.Substring(0, _FIndex)), Convert.ToInt32(Key.Substring(_FIndex + 1)));
            return _TmpReturn;
        }

        /// <summary>
        /// Removes A Map Object From The Desgnated Screen
        /// </summary>
        /// <param name="ID">MapObject ID</param>
        /// <param name="X">Screen X</param>
        /// <param name="Y">Screen Y</param>
        public void RemoveMapObject(Guid ID, int X, int Y)
        {
            foreach (var mo in Screens[GetKey(X, Y, Enumerations.RelativePosition.Main)].MapObjects)
            {
                if (mo.ID == ID)
                {
                    Screens[GetKey(X, Y, Enumerations.RelativePosition.Main)].MapObjects.Remove(mo);
                    return;
                }
            }
        }


        public void AddMapObject(int X, int Y, MapObject MapO)
        {
            string _Key = GetKey(X, Y, Enumerations.RelativePosition.Main);

            if (!Screens.ContainsKey(_Key))
            {
                EditorScreen _New = new EditorScreen(X, Y, this, true);
                Screens.Add(_Key, _New);
            }
            MapO.Screen = Screens[_Key];
            Screens[_Key].MapObjects.Add(MapO);

            Screens[_Key].MapObjects = Screens[_Key].MapObjects.OrderBy(x => x.DrawLocation.Y).ToList();
        }

        public void Save(string FileName, bool OverWrite)
        {
            if (System.IO.File.Exists(FileName) && OverWrite == false) { return; }

            if (System.IO.File.Exists(FileName))
            {
                System.IO.File.Delete(FileName);
            }

            #region Generate The XML File

            StringBuilder _Builder = new StringBuilder("<gg2world ");

            _Builder.Append("worldname=\"");
            _Builder.Append(WorldName);
            _Builder.Append("\" minpoint=\"");
            _Builder.Append(GetMin().ToString());
            _Builder.Append("\" maxpoint=\"");
            _Builder.Append(GetMax().ToString());
            _Builder.Append("\" >\r\n");

            // Append Screen Info
            foreach (var S in Screens.Values)
            {
                _Builder.Append("\t" + S.ExportToXML());
            }

            _Builder.Append("</gg2world>\r\n");

            #endregion

            // Get the Static Variables for the Save Operations
            string _BaseDirectory = FileName.Substring(0, FileName.LastIndexOf("\\"));
            Guid _TmpDirGuid = Guid.NewGuid();
            string _TmpDir = _TmpDirGuid.ToString().Substring(0, 10);

            // Create Directory If Needed
            if (!System.IO.Directory.Exists(_BaseDirectory))
            {
                System.IO.Directory.CreateDirectory(_BaseDirectory);
            }

            // Write the XML File
            System.IO.File.AppendAllText(FileName, _Builder.ToString());

            // Create the TempDirectories 
            System.IO.Directory.CreateDirectory(_BaseDirectory + "\\" + _TmpDir + "_Back" + "\\");
            System.IO.Directory.CreateDirectory(_BaseDirectory + "\\" + _TmpDir + "_Pass" + "\\");
            System.IO.Directory.CreateDirectory(_BaseDirectory + "\\" + _TmpDir + "_Objects" + "\\");

            // Save All Images in Zip File to Temp Directory
            foreach (var S in Screens.Keys)
            {
                var _Screen = Screens[S];

                //_Screen.BackgroundImage.Save(_BaseDirectory + "\\" + _TmpDir + "_Back" + "\\" + S + "_Back.png");
                //_Screen.PassableLayer.Save(_BaseDirectory + "\\" + _TmpDir + "_Pass" + "\\" + S + "_Pass.png");

                //GG2DLib.ResourceManager.ImageMethods.SaveImageAsJpg(_Screen.BackgroundImage, _BaseDirectory + "\\" + _TmpDir + "_Back" + "\\" + S + "_Back.png", 95L);
                //GG2DLib.ResourceManager.ImageMethods.SaveImageAsJpg(_Screen.PassableLayer, _BaseDirectory + "\\" + _TmpDir + "_Pass" + "\\" + S + "_Pass.png", 95L);
                //  _Screen.BackgroundImage.Save(_BaseDirectory + "\\" + _TmpDir + "_Back" + "\\" + S + "_Back.jpg", jgpEncoder, _Params);
                //_Screen.PassableLayer.Save(_BaseDirectory + "\\" + _TmpDir + "_Pass" + "\\" + S + "_Pass.jpg", jgpEncoder, _Params);
            }

            // Save All The Objects
            foreach (string _K in ResourceManager.GetMapObjectKeys())
            {
                var _TmpImg = ResourceManager.GetMapObjectImageStart(_K);
                _TmpImg.Save(_BaseDirectory + "\\" + _TmpDir + "_Objects" + "\\" + _K.Substring(_K.LastIndexOf("\\") + 1) + "_Object.png", System.Drawing.Imaging.ImageFormat.Png);
                ResourceManager.GetMapObjectImageEnd(_K);

            }


            // Create The Zip File for the Images
            if (System.IO.File.Exists(_BaseDirectory + "\\" + WorldName + "_Data.zip")) { System.IO.File.Delete(_BaseDirectory + "\\" + WorldName + "_Data.zip"); }

            ICSharpCode.SharpZipLib.Zip.ZipFile _Z = ICSharpCode.SharpZipLib.Zip.ZipFile.Create(_BaseDirectory + "\\" + WorldName + "_Data.zip");


            // Add The Backgrounds To The Zip File
            _Z.BeginUpdate();
            foreach (string _F in System.IO.Directory.GetFiles(_BaseDirectory + "\\" + _TmpDir + "_Back" + "\\"))
            {
                _Z.Add(_F, "Back\\" + _F.Substring(_F.LastIndexOf("\\") + 1));

            }
            _Z.CommitUpdate();

            // Add The Passable To The Zip File
            _Z.BeginUpdate();
            foreach (string _F in System.IO.Directory.GetFiles(_BaseDirectory + "\\" + _TmpDir + "_Pass" + "\\"))
            {
                _Z.Add(_F, "Pass\\" + _F.Substring(_F.LastIndexOf("\\") + 1));
            }
            _Z.CommitUpdate();

            // Add The Objects To The Zip File
            _Z.BeginUpdate();
            foreach (string _F in System.IO.Directory.GetFiles(_BaseDirectory + "\\" + _TmpDir + "_Objects" + "\\"))
            {
                _Z.Add(_F, "Objects\\" + _F.Substring(_F.LastIndexOf("\\") + 1));
            }
            _Z.CommitUpdate();

            _Z.Close();

            // Cleanup Temp Directories
            System.IO.Directory.Delete(_BaseDirectory + "\\" + _TmpDir + "_Back" + "\\", true);
            System.IO.Directory.Delete(_BaseDirectory + "\\" + _TmpDir + "_Pass" + "\\", true);
            System.IO.Directory.Delete(_BaseDirectory + "\\" + _TmpDir + "_Objects" + "\\", true);

            return;
        }

        #endregion Methods
    }
}
