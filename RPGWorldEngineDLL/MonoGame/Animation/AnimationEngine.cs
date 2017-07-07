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
using NebulaGames.RPGWorld.MonoGame.Managers;

namespace NebulaGames.RPGWorld.MonoGame.Animate
{
    public class AnimationEngine
    {
        #region Fields (7)

        private AnimationManager _AnimationManagerRef;

        private string _CurrentAnimation = "";
        private Enumerations.RelativePosition _CurrentFacePos;
        private int _CurrentFrame = 0;
        private int _DefaultDelay;
        private Game _Game;
        private TextureManager _TextureManager;
        private string _ActorName;
        #endregion Fields

        #region Constructors (1)


        public AnimationEngine(Game BaseGame, string ActorName)
        {
            _Game = BaseGame;
            _ActorName=ActorName;
            foreach (var x in _Game.Components)
            {
                if (x is TextureManager)
                {
                    _TextureManager = (TextureManager)x;
                }
                else if (x is AnimationManager)
                {
                    _AnimationManagerRef = (AnimationManager)x;
                }
            }
        }

        public AnimationEngine(Game BaseGame, string Data, string ActorName)
        {
            _Game = BaseGame;
            _ActorName=ActorName;
            foreach (var x in _Game.Components)
            {
                if (x is TextureManager)
                {
                    _TextureManager = (TextureManager)x;
                }
                else if (x is AnimationManager)
                {
                    _AnimationManagerRef = (AnimationManager)x;
                }
            }

            // Try and Load the Data
            _AnimationManagerRef.LoadData(Data, ActorName);
        }


        #endregion Constructors

        #region Properties (5)



        public string CurrentAnimation
        {
            get { return _CurrentAnimation; }
        }

        public int CurrentFrame
        {
            get { return _CurrentFrame; }
        }

        public int DefaultDelay
        {
            get { return _DefaultDelay; }
            set { _DefaultDelay = value; }
        }

        public TextureManager TextureManager
        {
            get { return _TextureManager; }
            set { _TextureManager = value; }
        }

        #endregion Properties

        #region Methods (4)

        // Public Methods (4) 
        DateTime _LastAdvancement = DateTime.Now;

        public Enumerations.RelativePosition GetFacing()
        {
            var _CAnimation = _AnimationManagerRef.GetActorAnimations(_ActorName).Animations[_CurrentAnimation][_CurrentFacePos];
            return _CAnimation.Direction;
        }
        public void AdvanceFrame()
        {

            var _CAnimation = _AnimationManagerRef.GetActorAnimations(_ActorName).Animations[_CurrentAnimation][_CurrentFacePos];

            if ((DateTime.Now - _LastAdvancement) > new TimeSpan(0, 0, 0, 0, (int)_CAnimation.Frames[_CurrentFrame].Delay))
            {

                _CurrentFrame = _CAnimation.FrameIndex[_CAnimation.Frames[_CurrentFrame].GetNextFrame()];
                _LastAdvancement = DateTime.Now;
            }

        }

        public Texture2D GetCurrentTexture()
        {
            return _AnimationManagerRef.GetActorAnimations(_ActorName).Animations[_CurrentAnimation][_CurrentFacePos].TextureRef;
        }

        public AnimationFrame GetCurrentFrame()
        {
            return _AnimationManagerRef.GetActorAnimations(_ActorName).Animations[_CurrentAnimation][_CurrentFacePos].Frames[_CurrentFrame];
        }
        public Animation GetCurrentAnimation()
        {
            return _AnimationManagerRef.GetActorAnimations(_ActorName).Animations[_CurrentAnimation][_CurrentFacePos];
        }



        public void SetAnimation(string Name, Enumerations.RelativePosition Position)
        {
            if (_CurrentFacePos == Position && Name == _CurrentAnimation)
            {
                return;
            }
            _CurrentFacePos = Position;
            _CurrentAnimation = Name;
            _CurrentFrame = 0;
            return;



        }

        #endregion Methods
    }
}
