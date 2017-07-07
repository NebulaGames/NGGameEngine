using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NebulaGames.RPGWorld.Enumerations;

namespace NebulaGames.RPGWorld.Interfaces
{
    public interface I_NGDataLogger
    {
        void LogMessage(LogLevel MessageLevel, params string[] DataList);
        void LogMessage(LogLevel MessageLevel, object[] Data);
        void LogMessage(LogLevel MessageLevel, string Msg, Exception ex);
        void LogMessage(LogLevel MessageLevel, string Msg, string ClassName);
        void LogMessage(LogLevel MessageLevel, string Msg, string ClassName, string StackTrace);
    }
}
