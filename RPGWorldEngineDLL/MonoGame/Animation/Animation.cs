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
    


    public class Animation
    {
		#region Fields (7) 

        private int _DefaultDelay = 100;
        private Enumerations.RelativePosition _Direction;
        private Dictionary<int, int> _FrameIndex = new Dictionary<int, int>();
        private Rectangle _BoundingBox;

        public Rectangle BoundingBox
        {
            get { return _BoundingBox; }
            set { _BoundingBox = value; }
        }
        private List<AnimationFrame> _Frames = new List<AnimationFrame>();
        private string _Name;
        private string _TextureFile;
        private Texture2D _TextureRef;

		#endregion Fields 

		#region Properties (7) 

        public int DefaultDelay
        {
            get { return _DefaultDelay; }
            set { _DefaultDelay = value; }
        }

        public Enumerations.RelativePosition Direction
        {
            get { return _Direction; }
            set { _Direction = value; }
        }

        public Dictionary<int, int> FrameIndex
        {
            get { return _FrameIndex; }
            set { _FrameIndex = value; }
        }

        public List<AnimationFrame> Frames
        {
            get { return _Frames; }
            set { _Frames = value; }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public string TextureFile
        {
            get { return _TextureFile; }
            set { _TextureFile = value; }
        }

        public Texture2D TextureRef
        {
            get { return _TextureRef; }
            set { _TextureRef = value; }
        }

		#endregion Properties 
    }

    
}
