using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using NebulaGames.RPGWorld.Graphics;

namespace NebulaGames.RPGWorld.GameBuilder
{

    /// <summary>
    /// All Graphics Are Stored As Graphics.CompressableImage
    /// Layers Are Always Stored As LowerCase
    /// </summary>
    public class EditorScreen
    {
        #region Fields (8)

        private EditorWorld WorldPointer;

        private Dictionary<string, Graphics.CompressibleImage> _ScreenImages = new Dictionary<string, Graphics.CompressibleImage>();
        // Compiled Version Of The Passable Layer Based On The World Precision
        public List<bool> _CompiledPassable = new List<bool>();
        private List<MapObject> _MapObjects = new List<MapObject>();

        // World Location
        private int _X;
        private int _Y;
        private string _Key;
                
        #endregion Fields

        #region Properties



        #endregion

        #region Constructors (1)

        public EditorScreen(int XPos, int YPos, EditorWorld world, bool Create = false)
        {
            if (world == null) { return; }

            _Key = EditorWorld.GetKey(XPos, YPos, Enumerations.RelativePosition.Main);
            //BaseDirectory = world.BaseDirectory;
            _X = XPos;
            _Y = YPos;

            if (Create)
            {
                _ScreenImages.Add("background", new Graphics.CompressibleImage((Image)new Bitmap(800, 600), System.Drawing.Imaging.ImageFormat.Png));
                _ScreenImages.Add("passable", new Graphics.CompressibleImage((Image)new Bitmap(800, 600), System.Drawing.Imaging.ImageFormat.Png));
            }
        }

        #endregion Constructors

        #region Properties (5)

        public List<MapObject> MapObjects
        {
            get { return _MapObjects; }
            set { _MapObjects = value; }
        }

        public int X
        {
            get { return _X; }
            set { _X = value; }
        }

        public int Y
        {
            get { return _Y; }
            set { _Y = value; }
        }

        #endregion Properties

        #region Methods (5)

        /// <summary>
        /// Adds the Image to the Layer.  Usefull when creating the object.  Also Creates The Layer If Not Found
        /// </summary>
        /// <param name="LayerName"></param>
        /// <param name="Img"></param>
        public void AddUpdateLayerImage(string LayerName, Image Img)
        {
            LayerName = LayerName.ToLower();

            if (_ScreenImages.ContainsKey(LayerName))
            {
                _ScreenImages[LayerName] = new Graphics.CompressibleImage(Img, System.Drawing.Imaging.ImageFormat.Png);
            }
            else
            {
                _ScreenImages.Add(LayerName, new Graphics.CompressibleImage(Img, System.Drawing.Imaging.ImageFormat.Png));
            }
        }

        /// <summary>
        /// Get The CompressedImage
        /// </summary>
        /// <param name="LayerName"></param>
        /// <returns></returns>
        public Graphics.CompressibleImage GetLayerCompressedImage(string LayerName)
        {
            if (_ScreenImages.ContainsKey(LayerName.ToLower()))
            {
                return _ScreenImages[LayerName.ToLower()];
            }
            else
            {
                ErrorLogable.LogError(null, "Layer Image Not Found: " + LayerName, ACT.Core.Enums.ErrorLevel.Severe);
                return null;
            }
        }

        /// <summary>
        /// Get the UnCompressed Image From The Layers
        /// </summary>
        /// <param name="LayerName"></param>
        /// <returns></returns>
        public Image GetLayerImage(string LayerName)
        {
            if (_ScreenImages.ContainsKey(LayerName.ToLower()))
            {
                return _ScreenImages[LayerName.ToLower()].GetDecompressedImage();
            }
            else
            {
                Image _TmpNewImage = new Bitmap(800, 600);
                _ScreenImages.Add(LayerName.ToLower(), new CompressibleImage(_TmpNewImage, System.Drawing.Imaging.ImageFormat.Png));

                return _ScreenImages[LayerName.ToLower()].GetDecompressedImage();
            }

        }

        /// <summary>
        /// Updates the Screens Layer Image
        /// </summary>
        /// <param name="LayerName">Name Of The Layer To Update</param>
        /// <param name="NewLayerImage">New Image</param>
        public void UpdateLayerImage(string LayerName, Image NewLayerImage)
        {
            if (_ScreenImages.ContainsKey(LayerName.ToLower()))
            {
                _ScreenImages[LayerName.ToLower()] = new CompressibleImage(NewLayerImage, System.Drawing.Imaging.ImageFormat.Png);
            }
            else
            {
                ErrorLogable.LogError(null, "Layer Image Not Found: " + LayerName, ACT.Core.Enums.ErrorLevel.Severe);
                throw new Exception("Error Saving Image To Layer: Layer Not Found: " + LayerName);
            }
        }
        /// <summary>
        /// Add A Layer To The Editor Screen.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public bool AddLayer(string Name)
        {
            Name = Name.ToLower();
            if (_ScreenImages.ContainsKey(Name) == true) { ErrorLogable.LogError(null, "Editor Screen Already Contains This Layer: " + Name); return false; }
            _ScreenImages.Add(Name, new Graphics.CompressibleImage((Image)new Bitmap(800, 600), System.Drawing.Imaging.ImageFormat.Png));
            return true;
        }

        public void Compile()
        {
            _CompiledPassable.Clear();
            FastPixel _FP;
            _FP = new FastPixel(new Bitmap(GetLayerImage("passable")));
            _FP.Lock();
            for (int x = 0; x < 800; x = x + 50)
            {
                for (int y = 0; y < 600; y = y + 50)
                {
                    bool _HasNonPassablePixel = false;

                    for (int _TmpX = x; _TmpX < x + 50; _TmpX++)
                    {
                        for (int _TmpY = y; _TmpY < y + 50; _TmpY++)
                        {
                            if (_FP.GetPixel(_TmpX, _TmpY).ToArgb() == Color.Red.ToArgb())
                            {
                                _HasNonPassablePixel = true;
                                break;
                            }
                        }
                        if (_HasNonPassablePixel == true) { break; }
                    }

                    _CompiledPassable.Add(_HasNonPassablePixel);
                }
            }
            _FP.Unlock(false);
            _FP = null;
        }

        public string ExportToXML()
        {
            StringBuilder _Result = new StringBuilder("<screen");

            _Result.Append(" key=\"");
            _Result.Append(_Key);
            _Result.Append("\">\r\n");

            foreach (var M in MapObjects)
            {
                _Result.Append("\t" + M.ExportToXML());
            }

            Compile();

            _Result.Append("\t<passablecompile>");
            foreach (var b in _CompiledPassable)
            {
                _Result.Append(Convert.ToInt32(b).ToString());
            }
            _Result.Append("</passablecompile>\r\n");

            _Result.Append("</screen>\r\n");

            return _Result.ToString();
        }

        public static Enumerations.RelativePosition GetRelativePos(int XPos, int YPos, int TestX, int TestY)
        {
            if (TestX == XPos && TestY == YPos) { return Enumerations.RelativePosition.Main; }
            foreach (var o in Enum.GetValues(typeof(Enumerations.RelativePosition)))
            {
                if (TestX == XPos + Constants.SurroundPoints[(int)o].X && TestY == YPos + Constants.SurroundPoints[(int)o].Y)
                {
                    return (Enumerations.RelativePosition)Enum.Parse(typeof(Enumerations.RelativePosition), o.ToString());
                }
            }

            throw new Exception("Not Part Of This Area");
        }

        public bool InRange(int XPos, int YPos, int OffsetX, int OffsetY)
        {
            if (X == XPos && Y == YPos) { return true; }
            for (int x = 0; x < 9; x++)
            {
                if (Constants.SurroundPoints[x].X + XPos == X & Constants.SurroundPoints[x].Y + YPos == Y)
                {
                    return true;
                }
            }
            return false;
        }

        public MapObject TryGetOneMapObject(int X, int Y)
        {
            var MapO = from QMapObject in _MapObjects where QMapObject.HasPoint(X, Y) orderby QMapObject.DrawLocation.Y select QMapObject;

            if (MapO.Count() > 0)
            {
                return MapO.ToArray()[MapO.Count() - 1];
            }

            return null;
        }

        #endregion Methods
    }
}
