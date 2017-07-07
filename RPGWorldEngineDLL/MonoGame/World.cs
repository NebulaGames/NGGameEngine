using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;
using System.Xml.XPath;
using NebulaGames.RPGWorld.MonoGame.Extensions;
using System.ComponentModel;
using NebulaGames.RPGWorld.MonoGame.Managers;
using ACT.Core.Extensions;

namespace NebulaGames.RPGWorld.MonoGame
{
    public class World : DrawableGameComponent
    {
        BackgroundWorker _HeavyWorker = new BackgroundWorker();
        SpriteFont _TextFont;
        Camera _MainCamera;
        
        // Define Managers
        TextureManager _TextureManager;
        AnimationManager _AnimationManager;
        AudioManager _AudioManager;
        EdgeManager _EdgeManager;
        SettingsManager _SettingsManager;
        FontManager _FontManager;
        WindowManager _WindowManager;
        MouseManager _MouseManager;
        // ViewPort Information
        private Vector2 _ViewPort;

        // Current Sceen Top Left Unused?
        private int _X, _Y;
        private List<Point> _CurrentScreens = new List<Point>();
        private Dictionary<Point, Screen> _ScreenBuffer = new Dictionary<Point, Screen>();

        private SpriteBatch _LocalBatch;

        private bool _Loading = false;
        private int _LoadedPercentage = 0;

        DateTime _DateLastFetchScreens = DateTime.Now;

        public override void Update(GameTime gameTime)
        {
            // Update The Screens
            if ((DateTime.Now - _DateLastFetchScreens).Milliseconds > 500)
            {
                _CurrentScreens = _MainCamera.GetNeededScreens();
                _DateLastFetchScreens = DateTime.Now;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _LocalBatch.Begin();
            if (_Loading)
            {
                _LocalBatch.Draw(_TextureManager.GetTexture("0-0"), new Vector2(0, 0), Color.White);
                foreach (var O in _ScreenBuffer[new Point(0, 0)].MapObjects)
                {
                    _LocalBatch.Draw(_TextureManager.GetTexture(O.Value.ResourceName), new Vector2(O.Value.DrawLocation.X, O.Value.DrawLocation.Y), Color.White);
                }
            }
            else
            {
                //SpriteFont _SF = new SpriteFont(
                
                List<Point> _ScreenPoints = _MainCamera.GetVisibleScreens();
                foreach (var P in _ScreenPoints)
                {
                    if (_ScreenBuffer.ContainsKey(P))
                    {
                        Screen _TmpScreen = _ScreenBuffer[P];

                        Point _OffsetValues = _TmpScreen.ReturnModifier(_MainCamera.TopLeftPoint.ToPoint(), _MainCamera.Size.ToPoint());

                        _LocalBatch.Draw(_TextureManager.GetTexture(P.ToScreenKey(Enumerations.RelativePosition.Main)), new Vector2(_OffsetValues.X, _OffsetValues.Y), Color.White);

                        // Draw The Map Objects

                        foreach (var mok in _TmpScreen.MapObjects.Keys)
                        {
                            var mo = _TmpScreen.MapObjects[mok];

                            _LocalBatch.Draw(_TextureManager.GetTexture(mo.ResourceName), new Vector2(mo.DrawLocation.X + _OffsetValues.X, mo.DrawLocation.Y + _OffsetValues.Y), Color.White);
                        }
                    }
                    else
                    {
                        
                    }
                }
               
              //  _LocalBatch.Draw(_TextureManager.GetTexture("0-0"), new Vector2(0, 0), Color.White);
            }
            _LocalBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="GameViewPort"></param>
        /// <param name="MainGameClass"></param>
        /// <param name="StartingXPos"></param>
        /// <param name="StartingYPos"></param>
        public World(Vector2 GameViewPort, Game XnaGame, Camera MainCamera)
            : base(XnaGame)
        {
            _MainCamera = MainCamera;

            _SettingsManager = new SettingsManager(XnaGame);
            _TextureManager = new TextureManager(XnaGame);
            _MouseManager = new MouseManager(XnaGame);

            _FontManager = new FontManager(XnaGame);
            _AnimationManager = new AnimationManager(XnaGame);
            _AudioManager = new AudioManager(XnaGame);            
            _LocalBatch = new SpriteBatch(XnaGame.GraphicsDevice);
            _EdgeManager = new EdgeManager(XnaGame);
            _WindowManager = new WindowManager(XnaGame);
            

            _TextureManager.LoadTexture("ProgressBar", AppDomain.CurrentDomain.BaseDirectory);

            _ViewPort = GameViewPort;

            _CurrentScreens.Clear();
            ReconcileScreenBuffer();

         

            _HeavyWorker.DoWork += new DoWorkEventHandler(_HeavyWorker_DoWork);
            _HeavyWorker.ProgressChanged += new ProgressChangedEventHandler(_HeavyWorker_ProgressChanged);
            _HeavyWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_HeavyWorker_RunWorkerCompleted);

            _TextFont = this.Game.Content.Load<SpriteFont>("MainFont");
        }

        void _HeavyWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void _HeavyWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void _HeavyWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

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
        public static Point ParseScreenKey(string Key)
        {
            int _FIndex = Key.IndexOf("-");
            if (_FIndex == 0) { _FIndex = Key.IndexOf("-", 1); }
            Point _TmpReturn = new Point(Convert.ToInt32(Key.Substring(0, _FIndex)), Convert.ToInt32(Key.Substring(_FIndex + 1)));
            return _TmpReturn;
        }

        public void Load(string WorldDataLocation, string WorldName)
        {
            XPathDocument _doc = new XPathDocument(WorldDataLocation.FormatDirectory() + WorldName + ".xml");
            XPathNavigator _nav = _doc.CreateNavigator();
            XPathExpression _expr;

            _expr = _nav.Compile("/gg2world");

            XPathNodeIterator _Iter = _nav.Select(_expr);

            _Iter.MoveNext();

            var _Nav2 = _Iter.Current.Clone();
            WorldName = _Nav2.GetAttribute("worldname", "");

            _expr = _nav.Compile("/gg2world/screen");

            XPathNodeIterator _ScreenIterator = null; _Nav2 = null;
            _ScreenIterator = _nav.Select(_expr);

            ICSharpCode.SharpZipLib.Zip.ZipFile _Zip = new ICSharpCode.SharpZipLib.Zip.ZipFile(WorldDataLocation.FormatDirectory() + WorldName + "_Data.zip");

            
            while (_ScreenIterator.MoveNext())
            {

                var _TmpScreenNavigator = _ScreenIterator.Current.Clone();
                string _tmpKey = _TmpScreenNavigator.GetAttribute("key", "");

                #region Load the Images into Memory

                Point _t = ParseScreenKey(_tmpKey);
                Screen _NewScreen = new Screen(_t, this);

                var _IStream = _Zip.GetInputStream(_Zip.GetEntry("Back/" + _tmpKey + "_Back.png"));
                
                System.IO.MemoryStream _MemStream = ((System.IO.Stream)_IStream).ToMemoryStream();
                // Load The Texture in The TextureManager
                _TextureManager.LoadTexture(_tmpKey, _MemStream);
                
                _MemStream.Dispose();

                var _IPassStream = _Zip.GetInputStream(_Zip.GetEntry("Pass/" + _tmpKey + "_Pass.png"));
                _MemStream = ((System.IO.Stream)_IPassStream).ToMemoryStream();
                // Load The Texture in The TextureManager
                _TextureManager.LoadTexture(_tmpKey + "(PASS)", _MemStream);

                _MemStream.Dispose();

                #endregion

                var _TmpPassableNavigator = _TmpScreenNavigator.SelectSingleNode("passablecompile");
                string _CompiledPassable = _TmpPassableNavigator.Value;

                int PassableCompilePosition = 0;
                for (int x = 0; x < 800; x = x + 50)
                {
                    for (int y = 0; y < 600; y = y + 50)
                    {
                        bool _TmpBool = false;
                        if (_CompiledPassable.Substring(PassableCompilePosition, 1) == "1")
                        {
                            _TmpBool = true;
                        }

                        _NewScreen.PassableCompile.Add(new Point(x, y), _TmpBool);
                        PassableCompilePosition++;
                    }
                }

                #region Load the MapObjects
                //var _exprmo = .Compile("/mapobject");


                var _IterS = _TmpScreenNavigator.SelectChildren("mapobject", "");
                while (_IterS.MoveNext())
                {
                    MapObject _MO = new MapObject();

                    var _TmpNav2 = _IterS.Current.Clone();

                    string _DL = _TmpNav2.GetAttribute("drawlocation", "");
                    Point _P = new Point();
                    _MO.DrawLocation = _P.FromString(_DL);

                    _DL = _TmpNav2.GetAttribute("imagesize", "");
                    System.Drawing.Size _Sz = new System.Drawing.Size();
                    _MO.ImageSize = _Sz.ToXnaPoint(_DL);

                    _MO.Position = (Enumerations.RelativePosition)Enum.Parse(typeof(Enumerations.RelativePosition), _TmpNav2.GetAttribute("position", ""));
                    _MO.ResourceName = _TmpNav2.GetAttribute("resourcename", "");

                    if (!_TextureManager.HasTexture(_MO.ResourceName))
                    {
                        var _IObjectStream = _Zip.GetInputStream(_Zip.GetEntry("Objects/" + _MO.ResourceName + "_Object.png"));
                        _MemStream = ((System.IO.Stream)_IObjectStream).ToMemoryStream();
                        // Load The Texture in The TextureManager
                        _TextureManager.LoadTexture(_MO.ResourceName, _MemStream);

                        _MemStream.Dispose();
                    }
                    _MO.ID = new Guid(_TmpNav2.GetAttribute("id", ""));

                    _NewScreen.MapObjects.Add(_MO.ID, _MO);

                }
                #endregion


                this._ScreenBuffer.Add(_t, _NewScreen);

            }


        }

        public void ReconcileScreenBuffer()
        {
            for (int cx = -1600; cx <= 1600; cx = cx + 800)
            {
                for (int cy = -1200; cy <= 1200; cy = cy + 600)
                {
                    _CurrentScreens.Add(new Point(((int)_MainCamera.TopLeftPoint.X + cx) / 800, ((int)_MainCamera.TopLeftPoint.Y + cy) / 600));
                }
            }
        }
               
        

      
    }
}
