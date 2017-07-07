using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebulaGames.RPGWorld
{
    /// <summary>
    /// This is the HELPER Class and Contains Various Methods to Assist in the Development.  Many of these methods are used to process
    /// System Dependant functions (Android vs Windows) etc.
    /// </summary>
    public static partial class Helper
    {
        public static class IO
        {
            #region IO Methods

            public enum FileTypes
            {
                Png,
                Jpg,
                Bmp
            }

            /// <summary>
            /// Gets All Of the Files From a Path Based On the File Types
            /// </summary>
            /// <param name="Path"></param>
            /// <returns></returns>
            [Attributes.SystemTypesSupported(Enumerations.SystemTypesEnum.WINDOWS)]
            public static string[] GetDirectoryFiles(string Path, FileTypes fileTypes)
            {
#if WINDOWS
                List<string> _FoundFiles = new List<string>();
                var _AllFiles = System.IO.Directory.GetFiles(Path, "*.*", System.IO.SearchOption.AllDirectories);
                List<string> _FileTypesFound = new List<string>();

                foreach (FileTypes p in Enum.GetValues(typeof(FileTypes)))
                {
                    if ((fileTypes & p) == p) { _FileTypesFound.Add(p.ToString()); }
                }

                foreach (string file in _AllFiles)
                {
                    foreach (string ft in _FileTypesFound)
                    {
                        
                        if (file.ToLower().EndsWith(ft.ToLower()))
                        {
                            _FoundFiles.Add(file);
                        }
                    }
                }

                return _FoundFiles.ToArray();
#else

#endif

                }


            /// <summary>
            /// Gets a Files Data Depending On the Type Of Platform
            /// </summary>
            /// <param name="Path">Full Path to The File</param>
            /// <returns>returns a byte[]</returns>
            [Attributes.SystemTypesSupported(Enumerations.SystemTypesEnum.WINDOWS)]
            public static byte[] GetFile(string Path)
            {

#if WINDOWS
                return System.IO.File.ReadAllBytes(Path);
#elif ANDROID
            throw new Exception("File Not Found-Not Implemented");
#else
            return null;
#endif

            }

            /// <summary>
            /// Gets the File Stream Based On The Path To The File
            /// </summary>
            /// <param name="Path">Full Path To The File</param>
            /// <returns>System.IO.Stream</returns>
            [Attributes.SystemTypesSupported(Enumerations.SystemTypesEnum.WINDOWS)]
            public static System.IO.Stream GetFileStream(string Path)
            {
#if WINDOWS
                return new System.IO.MemoryStream(GetFile(Path));
#elif ANDROID
            throw new Exception("File Not Found-Not Implemented");
#else
            return null;
#endif
            }

            [Attributes.SystemTypesSupported(Enumerations.SystemTypesEnum.WINDOWS)]
            public static string GetFileText(string Path)
            {
#if WINDOWS
                return (new System.IO.StreamReader(Path)).ReadToEnd();
#else
            throw new Exception("Not Implemented");
#endif
            }

            #endregion
        }

        #region EXTENSIONS
        [Attributes.SystemTypesSupported(Enumerations.SystemTypesEnum.WINDOWS)]
        /// <summary>
        /// Gets the Active Game In a File System Appropriate Format
        /// </summary>
        public static string ToFileSystemAppropriate(this string x)
        {

#if WINDOWS
            return x.Replace(" ", "").Replace(".", "");
#else
               return ActiveGame.Replace(" ","").Replace(".","");
#endif

        }

        /// <summary>
        /// Gets the File Name From The Full Path
        /// </summary>
        /// <param name="x">the Full Path To The File</param>
        /// <param name="IncludeExtensions">Include the File Extension?</param>
        /// <returns>FileName Without an Extension</returns>
        [Attributes.SystemTypesSupported(Enumerations.SystemTypesEnum.WINDOWS | Enumerations.SystemTypesEnum.ANDROID | Enumerations.SystemTypesEnum.IOS | Enumerations.SystemTypesEnum.LINUX)]
        public static string GetFileNameFromFullPath(this string x, bool IncludeExtensions)
        {
#if WINDOWS
            string _TmpReturn = x.Substring(x.LastIndexOf("\\"));
            _TmpReturn = _TmpReturn.Replace("\\", "");
            if (IncludeExtensions == false)
            {
                _TmpReturn = _TmpReturn.Substring(0, _TmpReturn.IndexOf("."));
            }
            return _TmpReturn;
#else
            string _TmpReturn = x.Substring(x.LastIndexOf("\\"));
            _TmpReturn = _TmpReturn.Replace("\\", "");
            if (IncludeExtensions == false)
            {
                _TmpReturn = _TmpReturn.Substring(0, _TmpReturn.IndexOf("."));
            }
            return _TmpReturn;
#endif
        }

        /// <summary>
        /// Gets the File Name From The Full Path
        /// </summary>
        /// <param name="x">the Full Path To The File</param>
        /// <param name="IncludeExtensions">Include the File Extension?</param>
        /// <returns>FileName Without an Extension</returns>
        [Attributes.SystemTypesSupported(Enumerations.SystemTypesEnum.WINDOWS | Enumerations.SystemTypesEnum.ANDROID | Enumerations.SystemTypesEnum.IOS | Enumerations.SystemTypesEnum.LINUX)]
        public static string GetLastFolderFromFullPath(this string x)
        {
#if WINDOWS
            int _LastPos = x.LastIndexOf("\\");

            if (_LastPos == -1) { return x; }
            x = x.Substring(0, 17);

            _LastPos = x.LastIndexOf("\\");
            x = x.Substring(_LastPos + 1);

            return x;
#else
            string _TmpReturn = x.Substring(x.LastIndexOf("\\"));
            _TmpReturn = _TmpReturn.Replace("\\", "");
            if (IncludeExtensions == false)
            {
                _TmpReturn = _TmpReturn.Substring(0, _TmpReturn.IndexOf("."));
            }
            return _TmpReturn;
#endif
        }

        /// <summary>
        /// This Ensures A Path Has the Trailing Backslash Needed or Slash Depending On The System Environment
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [Attributes.SystemTypesSupported(Enumerations.SystemTypesEnum.WINDOWS | Enumerations.SystemTypesEnum.ANDROID | Enumerations.SystemTypesEnum.IOS | Enumerations.SystemTypesEnum.LINUX)]
        public static string EnsureDirectoryFormat(this string x)
        {
#if WINDOWS
            if (x.EndsWith("\\")) { return x; }
            else { return x + "\\"; }
#elif ANDROID
           if (x.EndsWith("\\")) { return x; }
            else { return x + "\\"; }
#else
           if (x.EndsWith("\\")) { return x; }
            else { return x + "\\"; }
#endif

        }

        /// <summary>
        /// Gets a Ulong Hash Code For a Given String
        /// </summary>
        /// <param name="x">String You Want the Hash Code For</param>
        /// <returns></returns>
        [Attributes.SystemTypesSupported(Enumerations.SystemTypesEnum.WINDOWS | Enumerations.SystemTypesEnum.ANDROID | Enumerations.SystemTypesEnum.IOS | Enumerations.SystemTypesEnum.LINUX)]
        public static ulong GetHashCodeUlong(this string x)
        {
            byte[] _TmpByteArray = new byte[16];
            ulong _TmpReturn = 0;

            byte[] data = Encoding.Unicode.GetBytes(x);

            _TmpByteArray[4] = data[0];
            _TmpByteArray[6] = data[data.Length - 2];
            _TmpByteArray[8] = data[4];
            _TmpByteArray[10] = data[(int)(data.Length / 2)];

            _TmpReturn = BitConverter.ToUInt64(_TmpByteArray, 0);
            byte lastValue = 0;
            int Iteration = 0;
            foreach (byte b in data)
            {
                _TmpReturn += (ulong)(b * Iteration);

                if (b - lastValue < 0)
                {
                    _TmpReturn = _TmpReturn - ((ulong)(lastValue - b));
                }
                else
                {
                    _TmpReturn = _TmpReturn + ((ulong)(b - lastValue));
                }

                Iteration++;

            }
            _TmpReturn = (ulong)(_TmpReturn - (ulong)x.GetHashCode());


            foreach (byte b in data.Reverse())
            {
                _TmpReturn += b;
                if (b - lastValue < 0)
                {
                    _TmpReturn = _TmpReturn - ((ulong)(lastValue - b));
                }
                else
                {
                    _TmpReturn = _TmpReturn + ((ulong)(b - lastValue));
                }


            }
            return _TmpReturn;

        }

        /// <summary>
        /// Splits a String
        /// </summary>
        /// <param name="x"></param>
        /// <param name="Delimiter"></param>
        /// <returns></returns>
        [Attributes.SystemTypesSupported(Enumerations.SystemTypesEnum.WINDOWS | Enumerations.SystemTypesEnum.LINUX | Enumerations.SystemTypesEnum.IOS | Enumerations.SystemTypesEnum.ANDROID)]
        public static List<string> SplitString(this string x, string Delimiter)
        {
            return x.Split(Delimiter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList<string>();
        }

        /// <summary>
        /// Converts To Float
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [Attributes.SystemTypesSupported(Enumerations.SystemTypesEnum.WINDOWS | Enumerations.SystemTypesEnum.LINUX | Enumerations.SystemTypesEnum.IOS | Enumerations.SystemTypesEnum.ANDROID)]
        public static float ToFloat(this string x)
        {
            return (float)Convert.ToDouble(x);
        }

        /// <summary>
        /// Converts To Int
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        [Attributes.SystemTypesSupported(Enumerations.SystemTypesEnum.WINDOWS | Enumerations.SystemTypesEnum.LINUX | Enumerations.SystemTypesEnum.IOS | Enumerations.SystemTypesEnum.ANDROID)]
        public static int ToInt(this string x)
        {
            return Convert.ToInt32(x);
        }

        #endregion

        #region Error Logging

        /// <summary>
        /// Logs An Error Self Explanitory
        /// </summary>
        /// <param name="ErrorText"></param>
        /// <param name="ErrorMessage"></param>
        /// <param name="ClassName"></param>
        /// <param name="StackTrace"></param>
        [Attributes.SystemTypesSupported(Enumerations.SystemTypesEnum.WINDOWS)]
        public static void LogError(string ErrorText, string ErrorMessage, string ClassName, string StackTrace = "")
        {
#if WINDOWS
            try
            {
                string _ErrorXML = "<error><errortext>" + ErrorText + "</errortext><message>" + ErrorMessage + "</message><classname>" + ClassName + "</classname><stacktrace>" + StackTrace + "</stacktrace></error>" + Environment.NewLine;
                System.IO.File.AppendAllText(GameData.BaseDirectory.EnsureDirectoryFormat() + "ErrorLog.txt", _ErrorXML, Encoding.Unicode);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Creating Log File: " + ex.Message);
            }
#else
             throw new Exception("System Not Implemented");
#endif
        }

        [Attributes.SystemTypesSupported(Enumerations.SystemTypesEnum.WINDOWS)]
        public static void LogError(Exception ex)
        {
#if WINDOWS
            LogError(ex.Message, "Source: " + ex.Source + ", Inner Exception: " + ex.InnerException.Message, ex.TargetSite.Name, ex.StackTrace);
#else
            throw new Exception("System Not Implemented");
#endif
        }

        #endregion

        #region RANDOM METHODS
        private static System.Security.Cryptography.RNGCryptoServiceProvider _Rand = new System.Security.Cryptography.RNGCryptoServiceProvider();

        /// <summary>
        /// Gets a Random Number Securely
        /// </summary>
        /// <param name="Min">Min Int Value</param>
        /// <param name="Max">Max Int Value</param>
        /// <returns></returns>
        [Attributes.SystemTypesSupported(Enumerations.SystemTypesEnum.WINDOWS)]
        public static int GetRandomNumber(int Min, int Max)
        {
#if WINDOWS

            uint scale = uint.MaxValue;
            while (scale == uint.MaxValue)
            {
                byte[] four_bytes = new byte[4];
                _Rand.GetBytes(four_bytes);
                scale = BitConverter.ToUInt32(four_bytes, 0);
            }
            return (int)(Min + (Max - Min) *
                (scale / (double)uint.MaxValue));
#endif
        }


        /// <summary>
        /// Gets a Random String Of ANSII Characters
        /// </summary>
        /// <param name="length">Length of the Random String</param>
        /// <returns></returns>
        [Attributes.SystemTypesSupported(Enumerations.SystemTypesEnum.WINDOWS)]
        public static string RandomString(int length)
        {
#if WINDOWS
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[GetRandomNumber(0, s.Length)]).ToArray());
#endif
        }
        #endregion

        #region Plugin Helper Methods

        /// <summary>
        /// Returns the HELP File associated to this IPlugin Object.  \Plugin\Assembly\help\pluginname.help
        /// </summary>
        /// <param name="p">NGPlugin Object</param>
        /// <returns>File Contents as String</returns>
        [Attributes.SystemTypesSupported(Enumerations.SystemTypesEnum.DEPENDANT)]
        public static string LoadPluginHelp(Interfaces.I_NGPlugin p)
        {
            string _PluginFolder = GameData.PluginDirectory + p.Folder + "\\" + p.Name.ToFileSystemAppropriate() + ".help";

            return IO.GetFileText(_PluginFolder);
        }

        /// <summary>
        /// Parse the Description out from the Help TEXT
        /// </summary>
        /// <param name="HelpText"></param>
        /// <returns></returns>
        [Attributes.SystemTypesSupported(Enumerations.SystemTypesEnum.DEPENDANT)]
        public static string GetPluginHelpElement(string HelpText, string TagName)
        {
            if (HelpText == "") { return ""; }

            int _StartPoint = HelpText.IndexOf(TagName + ":");
            if (_StartPoint == -1) return "";
            _StartPoint += (TagName + ":").Length;

            int _EndPoint = HelpText.IndexOf(":" + TagName, _StartPoint);
            if (_EndPoint == -1) return "";

            if (_EndPoint - _StartPoint < 1) { return ""; }

            return HelpText.Substring(_StartPoint, _EndPoint - _StartPoint);
        }

        #endregion
    }
}
