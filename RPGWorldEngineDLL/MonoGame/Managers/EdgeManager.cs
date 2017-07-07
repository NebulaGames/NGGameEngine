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
using ACT.Core.Extensions;

namespace NebulaGames.RPGWorld.MonoGame.Managers
{
    public class Edge
    {
        #region Fields (4)

        string _ID;
        string _ImageFile;
        Dictionary<Enumerations.RelativePosition, Rectangle> _ImageLocation = new Dictionary<Enumerations.RelativePosition, Rectangle>();
        string _Name;

        #endregion Fields

        #region Properties (4)

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string ImageFile
        {
            get { return _ImageFile; }
            set { _ImageFile = value; }
        }

        public Dictionary<Enumerations.RelativePosition, Rectangle> ImageLocation
        {
            get { return _ImageLocation; }
            set { _ImageLocation = value; }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        #endregion Properties

        #region Methods (1)

        // Public Methods (1) 

        public void Load(string XMLFile)
        {
            XPathDocument _doc = new XPathDocument(XMLFile);
            XPathNavigator _nav = _doc.CreateNavigator();
            XPathExpression _expr;

            _expr = _nav.Compile("/edgefile");

            XPathNodeIterator _Iter = _nav.Select(_expr);

            _Iter.MoveNext();

            var _Nav2 = _Iter.Current.Clone();
            Name = _Nav2.GetAttribute("name", "");
            ID = _Nav2.GetAttribute("id", "").ParseXMLFunction();
            ImageFile = _Nav2.GetAttribute("imagefile", "");
            _expr = _nav.Compile("/edgefile/edge");

            XPathNodeIterator _ScreenIterator = null; _Nav2 = null;
            _ScreenIterator = _nav.Select(_expr);

            while (_ScreenIterator.MoveNext())
            {

                var _EdgeNav = _ScreenIterator.Current.Clone();
                Rectangle _R = new Rectangle();
                _R = _R.FromString(_EdgeNav.GetAttribute("imagerec", ""));
                Enumerations.RelativePosition _P = (Enumerations.RelativePosition)Enum.Parse(typeof(Enumerations.RelativePosition), _EdgeNav.GetAttribute("direction", ""));

                _ImageLocation.Add(_P, _R);
            }
        }

        #endregion Methods
    }





    public class EdgeManager : GameComponent
    {
        #region Fields (2)

        public Dictionary<string, Edge> _Edges = new Dictionary<string, Edge>();
        private TextureManager _TextureManagerRef;

        #endregion Fields

        #region Constructors (1)

        public Edge GetEdge(string Name)
        {
            if (_Edges.ContainsKey(Name)) { return _Edges[Name]; }
            return null;
        }

        public EdgeManager(Game XnaGame)
            : base(XnaGame)
        {
            XnaGame.Components.Add(this);

            foreach (var x in this.Game.Components)
            {
                if (x is TextureManager)
                {
                    _TextureManagerRef = (TextureManager)x;
                }
            }
            // Load All Components

            string[] _Directories = System.IO.Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + "Data\\Edges\\");

            foreach (var _Directory in _Directories)
            {
                if (!System.IO.File.Exists(_Directory.FormatDirectory() + "data.xml"))
                {
                    continue;
                }
                else
                {
                    Edge _TmpEdge = new Edge();
                    _TmpEdge.Load(_Directory.FormatDirectory() + "data.xml");

                    if (!_TextureManagerRef.HasTexture(_TmpEdge.Name + "_Edge"))
                    {
                        _TextureManagerRef.LoadTexture(_TmpEdge.Name + "_Edge", _Directory.FormatDirectory() + _TmpEdge.ImageFile);
                    }

                    _Edges.Add(_Directory.GetDirectoryName(), _TmpEdge);
                }
            }
        }

        #endregion Constructors
    }

}
