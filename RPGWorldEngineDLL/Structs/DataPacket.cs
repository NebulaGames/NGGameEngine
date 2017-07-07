using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NebulaGames.RPGWorld.Structs
{
    [Serializable()]
    [XmlRoot("datapacket")]
    public struct DataPacket
    {
        [XmlElement("errorcode")]
        public string ErrorCode;
        [XmlElement("errormessage")]
        public string ErrorMessage;
        [XmlElement("errorcodeint")]
        public int ErrorCodeInt;
        [XmlElement("dateandtime")]
        public DateTime DateAndTime;
        [XmlElement("stacktrace")]
        public string StackTrace;
    }
}
