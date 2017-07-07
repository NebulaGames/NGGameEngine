using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Xml;
using System.Xml.XPath;
using ACT.Core.Extensions;
using NebulaGames.RPGWorld.MonoGame.Extensions;
using NebulaGames.RPGWorld.MonoGame.Animate;

namespace NebulaGames.RPGWorld.MonoGame.Managers
{

    public class ActorAnimations
    {
		#region Fields (2) 

        private Dictionary<string, Dictionary<Enumerations.RelativePosition, Animation>> _Animations = new Dictionary<string, Dictionary<Enumerations.RelativePosition, Animation>>();
        public string ActorName;

		#endregion Fields 

		#region Properties (1) 

        public Dictionary<string, Dictionary<Enumerations.RelativePosition, Animation>> Animations
        {
            get { return _Animations; }
        }

		#endregion Properties 
    }
    public class AnimationManager : GameComponent
    {
        TextureManager _TextureManagerRef;

        public AnimationManager(Game XnaGame)
            : base(XnaGame)
        {
            XnaGame.Components.Add(this);

            foreach (var x in XnaGame.Components)
            {
                if (x is TextureManager)
                {
                    _TextureManagerRef = (TextureManager)x;
                    break;
                }
            }
        }
        public ActorAnimations GetActorAnimations(string ActorName)
        {
            var x = from AA in _ActorAnimations where AA.ActorName == ActorName select AA;
            return x.First();
        }
        private List<ActorAnimations> _ActorAnimations = new List<ActorAnimations>();

        public List<ActorAnimations> ActorAnimations
        {
            get { return _ActorAnimations; }
            set { _ActorAnimations = value; }
        }


        public void LoadData(string Data, string ActorName)
        {
            // Check If Actor Animations Where Already Loaded
            if (_ActorAnimations.Any(aa => aa.ActorName == ActorName)) { return; }

            // In Not Load Them
            ActorAnimations _Animations = new ActorAnimations();
            _Animations.ActorName = ActorName;

            // Load The File Into Memory
            using (System.IO.MemoryStream _stream = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(Data)))
            {
                XPathDocument _doc = new XPathDocument(_stream);
                XPathNavigator _nav = _doc.CreateNavigator();
                XPathExpression _expr;

                _expr = _nav.Compile("/animation");

                XPathNodeIterator _Iter = _nav.Select(_expr);

                _Iter.MoveNext();

                var _Nav2 = _Iter.Current.Clone();
                int _DefaultDelay = Convert.ToInt32(_Nav2.GetAttribute("defaultframedelay", ""));
                Rectangle _TmpBoundingBox = new Rectangle() ;
                _TmpBoundingBox = _TmpBoundingBox.FromString(_Nav2.GetAttribute("boundingbox", ""));
                _expr = _nav.Compile("/animation/set");

                _Iter = null; _Nav2 = null;
                _Iter = _nav.Select(_expr);

                while (_Iter.MoveNext())
                {
                    Animation _Animation = new Animation();

                    var _tmpnav = _Iter.Current.Clone();
                    _Animation.Name = _tmpnav.GetAttribute("name", "");
                    _Animation.TextureFile = _tmpnav.GetAttribute("texturefile", "");
                    _Animation.DefaultDelay = _DefaultDelay;
                    _Animation.BoundingBox = _TmpBoundingBox;
                    if (!_TextureManagerRef.HasTexture(_Animation.TextureFile))
                    {
                        _TextureManagerRef.LoadTexture(_Animation.TextureFile, AppDomain.CurrentDomain.BaseDirectory + "Actors\\" + ActorName + "\\");
                    }
                    _Animation.TextureRef = _TextureManagerRef.GetTexture(_Animation.TextureFile);

                    _Animation.Direction = (Enumerations.RelativePosition)Enum.Parse(typeof(Enumerations.RelativePosition), _tmpnav.GetAttribute("direction", ""));

                    var _IterS = _tmpnav.SelectChildren("frame", "");
                    while (_IterS.MoveNext())
                    {
                        AnimationFrame _TmpFrame = new AnimationFrame();

                        var _TmpNav2 = _IterS.Current.Clone();

                        var _tmpLocationArray = _TmpNav2.GetAttribute("imagerec", "").Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        _TmpFrame.ImageLocation = new Rectangle(Convert.ToInt32(_tmpLocationArray[0]), Convert.ToInt32(_tmpLocationArray[1]), Convert.ToInt32(_tmpLocationArray[2]), Convert.ToInt32(_tmpLocationArray[3]));

                        _TmpFrame.ID = Convert.ToInt32(_TmpNav2.GetAttribute("id", ""));

                        var _nextid = _TmpNav2.GetAttribute("nextid", "");
                        if (_nextid.StartsWith("rand"))
                        {
                            _nextid = _nextid.Replace("rand{", "").Replace("}", "");
                            var _nextarray = _nextid.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            foreach (var ni in _nextarray)
                            {
                                _TmpFrame.RandomFrames.Add(Convert.ToInt32(ni));
                            }
                        }
                        else
                        {
                            _TmpFrame.NextID = Convert.ToInt32(_nextid);
                        }

                        try
                        {
                            _TmpFrame.Delay = Convert.ToInt32(_TmpNav2.GetAttribute("delay", ""));
                        }
                        catch { }

                        try
                        {
                            _TmpFrame.Filp = Convert.ToBoolean(_TmpNav2.GetAttribute("flip", ""));
                        }
                        catch { }

                        _Animation.Frames.Add(_TmpFrame);
                        _Animation.FrameIndex.Add(_TmpFrame.ID, _Animation.Frames.Count - 1);
                    }

                    if (!_Animations.Animations.ContainsKey(_Animation.Name))
                    {
                        _Animations.Animations.Add(_Animation.Name, new Dictionary<Enumerations.RelativePosition, Animation>());
                    }

                    if (!_Animations.Animations[_Animation.Name].ContainsKey(_Animation.Direction))
                    {
                        _Animations.Animations[_Animation.Name].Add(_Animation.Direction, _Animation);
                    }
                }
                _ActorAnimations.Add(_Animations);
            }
        }
    }
}
