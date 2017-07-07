using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NebulaGames.RPGWorld.Enumerations;
using System.Diagnostics;

namespace NebulaGames.RPGWorld.BaseClasses
{
    public class NGDataLogger_Base : Interfaces.I_NGDataLogger
    {
#if WINDOWS
        private void SaveMessageDataToRegistry()
        {
            string Timestamp = DateTime.Now.ToString("dd-MM-yyyy");

            string key = "HKEY_LOCAL_MACHINE\\SOFTWARE\\NebulaGames\\NGSlots\\";
            string valueName = "Last Error";
            Microsoft.Win32.Registry.SetValue(key, valueName, Timestamp, Microsoft.Win32.RegistryValueKind.String);
        }

        private bool SaveMessageDataToEventLog(LogLevel MessageLevel, int EventID, string Message, byte[] RawData)
        {
            if (!EventLog.SourceExists("NebulaGames Slots"))
                EventLog.CreateEventSource("NebulaGames Slots", "Application");

            EventLogEntryType _ELET;
            short _CategoryName = 1;

            #region Define The EventLog Entry Type
            if (MessageLevel == LogLevel.AssetMissingCritical || MessageLevel == LogLevel.CoreEngineCritical || MessageLevel == LogLevel.GeneralCritical || MessageLevel == LogLevel.PluginCritical)
            {
                _ELET = EventLogEntryType.Error;
                SaveMessageDataToRegistry();
            }
            else if (MessageLevel == LogLevel.PluginError || MessageLevel == LogLevel.GeneralError || MessageLevel == LogLevel.CoreEngineError || MessageLevel == LogLevel.AssetMissingError)
            {
                _ELET = EventLogEntryType.Error;
                SaveMessageDataToRegistry();
            }
            else
            {
                _ELET = EventLogEntryType.Warning;
            }
            #endregion

            if (MessageLevel == LogLevel.AssetMissingCritical || MessageLevel == LogLevel.AssetMissingError || MessageLevel == LogLevel.AssetMissingWarning)
            {
                _CategoryName = 2;
            }
            else if (MessageLevel == LogLevel.CoreEngineCritical || MessageLevel == LogLevel.CoreEngineError || MessageLevel == LogLevel.CoreEngineWarning)
            {
                _CategoryName = 3;
            }
            else if (MessageLevel == LogLevel.GeneralCritical || MessageLevel == LogLevel.GeneralError|| MessageLevel == LogLevel.GeneralWarning)
            {
                _CategoryName = 4;
            }
            else
            {
                _CategoryName = 1;
            }

            try
            {
                EventLog.WriteEntry("NebulaGames Slots", "", _ELET, EventID, _CategoryName, RawData);
                return true;
            }
            catch
            {
                try
                {
                    SaveMessageToFile(MessageLevel, EventID, Message + "||" + _CategoryName.ToString());
                    return true;
                }
                catch
                {
                    return false;
                }                
            }
        }

        private void SaveMessageToFile(LogLevel MessageLevel, int EventID, string MessageData)
        {
            string _BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (_BaseDirectory.EndsWith("\\") == false)
            {
                _BaseDirectory += "\\";
            }

            if (!System.IO.File.Exists(_BaseDirectory + "SystemLog.txt"))
            {
                System.IO.File.CreateText(_BaseDirectory + "SystemLog.txt");
            }

            System.IO.File.AppendAllText(_BaseDirectory + "SystemLog.txt", EventID.ToString() + "||" + MessageData + Environment.NewLine);
        }

        public virtual void LogMessage(LogLevel MessageLevel, params string[] DataList)
        {
            SaveMessageDataToEventLog(MessageLevel, -1, String.Join("**", DataList), null);
            if (MessageLevel == LogLevel.AssetMissingCritical || MessageLevel == LogLevel.CoreEngineCritical || MessageLevel == LogLevel.GeneralCritical)
            {
                throw new Exception("Critical Error Has Occurred: See Log For Details");                
            }
            else { return; }
        }

        public virtual void LogMessage(LogLevel MessageLevel, object[] Data)
        {
            string[] _DataS = new string[Data.Length]; int x = 0;
            foreach(object o in Data) { _DataS[x] = o.ToString(); x++; }

            SaveMessageDataToEventLog(MessageLevel, -1, String.Join("**", _DataS), null);
            if (MessageLevel == LogLevel.AssetMissingCritical || MessageLevel == LogLevel.CoreEngineCritical || MessageLevel == LogLevel.GeneralCritical)
            {
                throw new Exception("Critical Error Has Occurred: See Log For Details");
            }
            else { return; }
        }

        public virtual void LogMessage(LogLevel MessageLevel, string Msg, Exception ex)
        {       

            SaveMessageDataToEventLog(MessageLevel, -1, Msg + ex.Message, null);
            if (MessageLevel == LogLevel.AssetMissingCritical || MessageLevel == LogLevel.CoreEngineCritical || MessageLevel == LogLevel.GeneralCritical)
            {
                throw new Exception("Critical Error Has Occurred: See Log For Details");
            }
            else { return; }
        }

        public virtual void LogMessage(LogLevel MessageLevel, string Msg, string ClassName)
        {
            SaveMessageDataToEventLog(MessageLevel, -1, ClassName + Msg, null);
            if (MessageLevel == LogLevel.AssetMissingCritical || MessageLevel == LogLevel.CoreEngineCritical || MessageLevel == LogLevel.GeneralCritical)
            {
                throw new Exception("Critical Error Has Occurred: See Log For Details");
            }
            else { return; }
        }

        public virtual void LogMessage(LogLevel MessageLevel, string Msg, string ClassName, string StackTrace)
        {
            SaveMessageDataToEventLog(MessageLevel, -1, ClassName + "**" + Msg + "**" + StackTrace, null);
            if (MessageLevel == LogLevel.AssetMissingCritical || MessageLevel == LogLevel.CoreEngineCritical || MessageLevel == LogLevel.GeneralCritical)
            {
                throw new Exception("Critical Error Has Occurred: See Log For Details");
            }
            else { return; }
        }
#elif ANDROID
        public virtual void LogMessage(LogLevel MessageLevel, params string[] DataList)
        {

        }

        public virtual void LogMessage(LogLevel MessageLevel, object[] Data)
        {

        }

        public virtual void LogMessage(LogLevel MessageLevel, string Msg, Exception ex)
        {
            throw new NotImplementedException();
        }

        public virtual void LogMessage(LogLevel MessageLevel, string Msg, string ClassName)
        {
            throw new NotImplementedException();
        }

        public virtual void LogMessage(LogLevel MessageLevel, string Msg, string ClassName, string StackTrace)
        {
            throw new NotImplementedException();
        }
#elif IOS
        public virtual void LogMessage(LogLevel MessageLevel, params string[] DataList)
        {

        }

        public virtual void LogMessage(LogLevel MessageLevel, object[] Data)
        {

        }

        public virtual void LogMessage(LogLevel MessageLevel, string Msg, Exception ex)
        {
            throw new NotImplementedException();
        }

        public virtual void LogMessage(LogLevel MessageLevel, string Msg, string ClassName)
        {
            throw new NotImplementedException();
        }

        public virtual void LogMessage(LogLevel MessageLevel, string Msg, string ClassName, string StackTrace)
        {
            throw new NotImplementedException();
        }
#else
        public virtual void LogMessage(LogLevel MessageLevel, params string[] DataList)
        {

        }

        public virtual void LogMessage(LogLevel MessageLevel, object[] Data)
        {

        }

        public virtual void LogMessage(LogLevel MessageLevel, string Msg, Exception ex)
        {
            throw new NotImplementedException();
        }

        public virtual void LogMessage(LogLevel MessageLevel, string Msg, string ClassName)
        {
            throw new NotImplementedException();
        }

        public virtual void LogMessage(LogLevel MessageLevel, string Msg, string ClassName, string StackTrace)
        {
            throw new NotImplementedException();
        }
#endif


    }
}
