using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace NebulaGames.RPGWorld.MonoGame.Animate
{
    public class AnimationFrame
    {
		#region Fields (8) 

        private bool _Default;
        private int? _Delay;
        private bool _Filp = false;
        private int _ID;
        private Rectangle _ImageLocation;
        private int _NextID;
        private List<int> _RandomFrames = new List<int>();

        public List<int> RandomFrames
        {
            get { return _RandomFrames; }
            set { _RandomFrames = value; }
        }
        private string _SoundName;

		#endregion Fields 

		#region Properties (8) 

        public bool Default
        {
            get { return _Default; }
            set { _Default = value; }
        }

        public int? Delay
        {
            get { return _Delay; }
            set { _Delay = value; }
        }

        public bool Filp
        {
            get { return _Filp; }
            set { _Filp = value; }
        }

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
               

        public Rectangle ImageLocation
        {
            get { return _ImageLocation; }
            set { _ImageLocation = value; }
        }

        public int NextID
        {
            get { return _NextID; }
            set { _NextID = value; }
        }

        public string SoundName
        {
            get { return _SoundName; }
            set { _SoundName = value; }
        }

		#endregion Properties 

		#region Methods (1) 

		// Public Methods (1) 

        public int GetNextFrame()
        {
            if (_RandomFrames.Count == 0)
            {
                return  _NextID;
            }
            else
            {
                return _RandomFrames[Helper.GetRandomNumber(0, _RandomFrames.Count-1)];
            }
        }

		#endregion Methods 
    }
}
