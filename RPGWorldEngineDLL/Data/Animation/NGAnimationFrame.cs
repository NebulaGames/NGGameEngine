using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using NebulaGames.RPGWorld;

namespace NebulaGames.RPGWorld.Data
{
    [XmlRoot(ElementName = "frame")]
    public class NGAnimationFrame
    {
        [XmlAttribute(AttributeName = "imageid")]
        public string ImageID { get; set; }

        [XmlAttribute(AttributeName = "imageidguid")]
        public string ImageIDGuid { get; set; }

        [XmlAttribute(AttributeName = "index")]
        public string Index { get; set; }

        // Comma Delimited Vector3 (Float Values)
        [XmlAttribute(AttributeName = "offset")]
        public string Offset { get; set; }

        [XmlAttribute(AttributeName = "delay")]
        public string Delay { get; set; }

        #region Conversion Properties (Non XML)


        public Texture2D GetImage
        {
            get
            {
                var _TexData = GameData.ImageManager.GetTexture2d(ImageIDValue);
                return _TexData;
            }
        }

        private ulong? _ImageID;
        [XmlIgnore()]
        public ulong ImageIDValue
        {
            get
            {
                if (_ImageID == null) { _ImageID = Convert.ToUInt64(ImageID); }
                return _ImageID.Value;
            }
        }

        private Guid? _ImageIDGuid;
        [XmlIgnore()]
        public Guid ImageIDGuidValue
        {
            get
            {
                if (_ImageIDGuid == null) { _ImageIDGuid = new Guid(ImageIDGuid); }
                return _ImageIDGuid.Value;
            }
        }

        private ushort? _Index;
        [XmlIgnore()]
        public ushort IndexValue
        {
            get
            {
                if (_Index == null) { _Index = Convert.ToUInt16(Index); }
                return _Index.Value;
            }
        }

        private Vector3? _OffsetValue;
        [XmlIgnore()]
        public Microsoft.Xna.Framework.Vector3 OffsetValue
        {
            get
            {
                if (_OffsetValue == null)
                {
                    var _OffsetData = Offset.SplitString(",");
                    _OffsetValue = new Vector3(_OffsetData[0].ToFloat(), _OffsetData[1].ToFloat(), _OffsetData[2].ToFloat());
                }

                return _OffsetValue.Value;
            }
        }

        private ushort? _Delay;
        [XmlIgnore()]
        public ushort DelayValue
        {
            get
            {
                if (_Delay == null) { _Delay = Convert.ToUInt16(Delay); }
                return _Delay.Value;
            }
        }

        #endregion
    }
}
