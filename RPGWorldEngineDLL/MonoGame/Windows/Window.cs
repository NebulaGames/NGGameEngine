using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using NebulaGames.RPGWorld.MonoGame.Extensions;
using System.Xml.XPath;
using NebulaGames.RPGWorld.MonoGame.Managers;

namespace NebulaGames.RPGWorld.MonoGame.Windows

{
    public class Window 
    {
        private List<DrawableWindowComponent> _DrawableComponents = new List<DrawableWindowComponent>();

        private Rectangle _ScreenPosition = new Rectangle();

        public void MoveTo(int x,int y)
        {
            _ScreenPosition.X = x;
            _ScreenPosition.Y = y;
        }
        public Rectangle ScreenPosition
        {
            get { return _ScreenPosition; }
            set { _ScreenPosition = value; }
        }
        private Edge _EdgeDefinition;
        private Managers.WindowManager _WindowManager;
        public bool _IsMoving;
        public Managers.WindowManager WindowManager
        {
            get { return _WindowManager; }
            set { _WindowManager = value; }
        }
        private EdgeManager _EdgeManager;
        private ActorManager _ActorManager;
        private TextureManager _TextureManager;
        private Texture2D _BackGroundTexture;
        private Texture2D _EdgeTexture;
        private Texture2D _Buffer;
        private bool _Active;        
        private string _Name;

        private RenderTarget2D _InternalRenderTarget;

        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }

        public Window(Game XnaGame, string Name)
        {
            _Name = Name;
            _Buffer = new Texture2D(XnaGame.GraphicsDevice, 2000, 2000);
        
            foreach (var t in XnaGame.Components)
            {
                if (t is Managers.WindowManager)
                {
                    _WindowManager = (Managers.WindowManager)t;
                }
                else if (t is EdgeManager)
                {
                    _EdgeManager  = (EdgeManager)t;
                }
                else if (t is TextureManager)
                {
                    _TextureManager = (TextureManager)t;
                }
                else if (t is ActorManager)
                {
                    _ActorManager = (ActorManager)t;
                }
            }
            LoadXML();
        }

        private void LoadXML()
        {
            XPathDocument _doc = new XPathDocument(AppDomain.CurrentDomain.BaseDirectory + "Data\\Windows\\" + _Name + "\\Data.xml");

            XPathNavigator _nav = _doc.CreateNavigator();
            XPathExpression _expr;

            _expr = _nav.Compile("/window");

            XPathNodeIterator _Iter = _nav.Select(_expr);

            _Iter.MoveNext();

            var _Nav2 = _Iter.Current.Clone();
                      
            _EdgeDefinition = _EdgeManager.GetEdge(_Nav2.SelectSingleNode("edge").Value);
            _EdgeTexture = _TextureManager.GetTexture(_EdgeDefinition.Name + "_Edge");

            _ScreenPosition= _ScreenPosition.FromString(_Nav2.SelectSingleNode("startingposition").Value);
            _BackGroundTexture = _TextureManager.GetTexture(_Nav2.SelectSingleNode("background").Value);

            _expr = _nav.Compile("/window/buttons/button");

            XPathNodeIterator _ScreenIterator = null; _Nav2 = null;
            _ScreenIterator = _nav.Select(_expr);
            
            while (_ScreenIterator.MoveNext())
            {

            //    var _TmpScreenNavigator = _ScreenIterator.Current.Clone();
             //   string _tmpKey = _TmpScreenNavigator.GetAttribute("key", "");
            }

            _expr = _nav.Compile("/window/code");

            XPathNodeIterator _CodeIterator = null;
            XPathNavigator _CodeNav = _Iter.Current.Clone(); ;
            _CodeIterator = _CodeNav.Select(_expr);

            while (_CodeIterator.MoveNext())
            {
                
                var _TmpCodeNavigator = _CodeIterator.Current.Clone();

                var ObjectIterator = _TmpCodeNavigator.SelectChildren( XPathNodeType.All);

                while (ObjectIterator.MoveNext())
                {
                    var _TmpClassNavigator = ObjectIterator.Current.Clone();
                    string _TmpName = _TmpClassNavigator.Name;
                    string _Assemblies = _TmpClassNavigator.GetAttribute("assemblies", "");
                    string _FullClassName = _TmpClassNavigator.GetAttribute("fullclassname", "");

                    CodeManager.CompileInMemory(_TmpName, _TmpClassNavigator.Value, _Assemblies.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));


                    System.Reflection.Assembly _T = CodeManager.GetObject(_TmpName);

                    object _TmpObj;
                    _TmpObj = _T.CreateInstance(_FullClassName, false, System.Reflection.BindingFlags.CreateInstance, null, new object[] { this.WindowManager.Game, this }, null, null);



                    _DrawableComponents.Add((DrawableWindowComponent)_TmpObj);
                }
                
            }
        }

        public bool IsActive = false;

        public void Draw(GameTime gameTime, SpriteBatch _Batch)
        {
            
            _Batch.Begin();
           // _EdgeDefinition.ImageFile
            //_EdgeDefinition.
            _Batch.Draw(_BackGroundTexture, _ScreenPosition, Color.White);

            _Batch.Draw(_EdgeTexture, new Rectangle(_ScreenPosition.Left, _ScreenPosition.Top, _EdgeDefinition.ImageLocation[Enumerations.RelativePosition.TopLeft].Width,
                _EdgeDefinition.ImageLocation[Enumerations.RelativePosition.TopLeft].Height), _EdgeDefinition.ImageLocation[Enumerations.RelativePosition.TopLeft], Color.White);
            //Top Right
            _Batch.Draw(_EdgeTexture, new Rectangle(_ScreenPosition.Right - _EdgeDefinition.ImageLocation[Enumerations.RelativePosition.TopRight].Width, _ScreenPosition.Top, _EdgeDefinition.ImageLocation[Enumerations.RelativePosition.TopRight].Width,
               _EdgeDefinition.ImageLocation[Enumerations.RelativePosition.TopRight].Height), _EdgeDefinition.ImageLocation[Enumerations.RelativePosition.TopRight], Color.White);

            _Batch.Draw(_EdgeTexture, new Rectangle(_ScreenPosition.Left, _ScreenPosition.Bottom - _EdgeDefinition.ImageLocation[Enumerations.RelativePosition.BottomLeft].Height, _EdgeDefinition.ImageLocation[Enumerations.RelativePosition.BottomLeft].Width,
               _EdgeDefinition.ImageLocation[Enumerations.RelativePosition.BottomLeft].Height), _EdgeDefinition.ImageLocation[Enumerations.RelativePosition.BottomLeft], Color.White);

            _Batch.Draw(_EdgeTexture, new Rectangle(_ScreenPosition.Right - _EdgeDefinition.ImageLocation[Enumerations.RelativePosition.BottomRight].Width, _ScreenPosition.Bottom - _EdgeDefinition.ImageLocation[Enumerations.RelativePosition.BottomRight].Height, _EdgeDefinition.ImageLocation[Enumerations.RelativePosition.BottomRight].Width,
               _EdgeDefinition.ImageLocation[Enumerations.RelativePosition.BottomRight].Height), _EdgeDefinition.ImageLocation[Enumerations.RelativePosition.BottomRight], Color.White);

            _Batch.Draw(_EdgeTexture, new Rectangle(_ScreenPosition.Left + _EdgeDefinition.ImageLocation[Enumerations.RelativePosition.TopLeft].Width, _ScreenPosition.Top, 
                                                        _EdgeDefinition.ImageLocation[Enumerations.RelativePosition.Top].Width + _ScreenPosition.Width -
                                                        _EdgeDefinition.ImageLocation[Enumerations.RelativePosition.TopLeft].Width - _EdgeDefinition.ImageLocation[Enumerations.RelativePosition.TopRight].Width
                                            ,
                _EdgeDefinition.ImageLocation[Enumerations.RelativePosition.Top].Height), _EdgeDefinition.ImageLocation[Enumerations.RelativePosition.TopLeft], Color.White);
         

            _Batch.End();

            foreach (var c in _DrawableComponents)
            {
                c.Draw(gameTime);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var c in _DrawableComponents)
            {
                c.Update(gameTime);
            }
        }
    }
}
