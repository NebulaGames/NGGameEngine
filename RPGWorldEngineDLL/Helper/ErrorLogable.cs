using System;
using System.Collections.Generic;

using System.Text;

namespace NebulaGames.RPGWorld
{
    public static class ErrorLogable
    {
        private static Enumerations.LogLocation _LogLocation;

        static ErrorLogable()
        {
            _LogLocation = Enumerations.LogLocation.File;
        }

        public static void LogError(Exception ex, string Error, ACT.Core.Enums.ErrorLevel SeverityLevel = ACT.Core.Enums.ErrorLevel.Informational, string ApplicationName = "NebulaGames.RPGWorld")
        {
            bool _Verbosity = false;
            try
            {
                _Verbosity = Convert.ToBoolean(ACT.Core.Windows.Properties.GetProperty("NebulaGames.RPGWorld", "VerboseDebugging"));
            }
            catch { }

            if (SeverityLevel != ACT.Core.Enums.ErrorLevel.Critical && _Verbosity == false) { return; }

            if (_LogLocation == Enumerations.LogLocation.File)
            {
                if (!System.IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Errors\\")) { System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Errors\\"); }

                var _FS = System.IO.File.OpenWrite(AppDomain.CurrentDomain.BaseDirectory + "Errors\\" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".txt");

                System.IO.StreamWriter _SQ = new System.IO.StreamWriter(_FS);

                if (ex == null)
                {

                    _SQ.WriteLine(Error);
                }
                else
                {
                    _SQ.WriteLine("****************" + DateTime.Now.ToString() + "****************************");
                    _SQ.WriteLine(ex.Message);
                    _SQ.WriteLine(Error);
                    _SQ.WriteLine("*****************      END ERROR      ***************************");
                }
                

                _SQ.Close();
                _SQ.Dispose();
                _FS.Dispose();

            }
        }
    }
}
