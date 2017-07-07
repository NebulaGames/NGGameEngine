using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace NebulaGames.RPGWorld.Data
{
    [XmlRoot(ElementName = "animation")]
    public class NGAnimation
    {
        [XmlAttribute(AttributeName = "id")]
        public string ID { get; set; }

        [XmlAttribute(AttributeName = "idguid")]
        public string IDGuid { get; set; }

        [XmlAttribute(AttributeName = "scope")]
        public string Scope { get; set; }

        [XmlElement(ElementName = "frames")]
        public List<NGAnimationFrames> Frames { get; set; }

        [XmlIgnore()]
        public NGAnimationFrames FramesFirst { get { return Frames[0]; } }

        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "animationmode")]
        public string AnimationMode { get; set; }

        private int _CurrentFrame = 0;
        public NGAnimationFrame NextFrame()
        {
            if (AnimationModeValue == Enumerations.AnimationModeEnum.FrontToBackToFront)
            {
                return NextFrameFTBTF();
            }
            else if (AnimationModeValue == Enumerations.AnimationModeEnum.FrontToBack)
            {
                return NextFrameFTB();
            }
            else //if (AnimationModeValue == Enumerations.AnimationModeEnum.BackToFront)
            {
                return NextFrameBTF();
            }
        }

        int _FrameDirection = 1;
        private NGAnimationFrame NextFrameFTBTF()
        {
            if (_CurrentFrame == Frames[0].Frames.Count() - 1)
            {
                _FrameDirection = -1;
                return Frames[0].Frames[_CurrentFrame--];

            }
            else if (_CurrentFrame == 0)
            {
                _FrameDirection = 1;
                _CurrentFrame++;
            }
            else
            {
                if (_FrameDirection > 0) { _CurrentFrame++; }
                else { _CurrentFrame--; }
            }

            return Frames[0].Frames[_CurrentFrame];
        }

        private NGAnimationFrame NextFrameFTB()
        {
            if (_CurrentFrame == Frames[0].Frames.Count() - 1)
            {
                _CurrentFrame = _CurrentFrame =0;

            }           
            else
            {
                _CurrentFrame++;
            }

            return Frames[0].Frames[_CurrentFrame];
        }

        private NGAnimationFrame NextFrameBTF()
        {
            if (_CurrentFrame == 0)
            {
                _CurrentFrame = Frames[0].Frames.Count() - 1;

            }
            else
            {
                _CurrentFrame--;
            }

            return Frames[0].Frames[_CurrentFrame];
        }

        [XmlIgnore()]
        public Enumerations.AnimationModeEnum AnimationModeValue
        {
            get
            {
                var _tMode = (Enumerations.AnimationModeEnum)Enum.Parse(typeof(Enumerations.AnimationModeEnum), AnimationMode);
                return _tMode;
            }
        }

        private ulong? _ID;
        [XmlIgnore()]
        public ulong IDValue
        {
            get
            {
                if (_ID == null) { _ID = Convert.ToUInt64(ID); }
                return _ID.Value;
            }
        }

        private Guid? _IDGuid;
        [XmlIgnore()]
        public Guid IDGuidValue
        {
            get
            {
                if (_IDGuid == null) { _IDGuid = new Guid(IDGuid); }
                return _IDGuid.Value;
            }
        }


    }
}
