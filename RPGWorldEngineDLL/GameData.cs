using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace NebulaGames.RPGWorld
{
    /// <summary>
    /// Partial Game Data Class
    /// </summary>
    public static partial class GameData
    {
        #region Variables
                
        /// <summary>
        /// Game Configuration
        /// </summary>
        public static Data.GameConfiguration.NGGameConfiguration GameConfiguration;

        /// <summary>
        /// Game Loaded Flag
        /// </summary>
        public static Enumerations.GameLoadingStatuses LoadingStatus { get; set; }

        #region Directories

        /// <summary>
        /// Base Directory Of This Game Engine
        /// </summary>
        public static string BaseDirectory { get; set; }

        /// <summary>
        /// Gets the Data Directory
        /// </summary>
        public static string DataDirectory { get { return BaseDirectory.EnsureDirectoryFormat() + "Data\\"; } }

        /// <summary>
        /// Gets the Games Directory
        /// </summary>
        public static string GamesDirectory { get { return DataDirectory.EnsureDirectoryFormat() + "Games\\"; } }

        /// <summary>
        /// Gets the Plugin Directory
        /// </summary>
        public static string PluginDirectory { get { return DataDirectory.EnsureDirectoryFormat() + "Plugins\\"; } }

        /// <summary>
        /// Gets the Plugin Directory
        /// </summary>
        public static string SystemDirectory { get { return DataDirectory.EnsureDirectoryFormat() + "Images\\System\\"; } }

        /// <summary>
        /// Gets the Plugin Directory
        /// </summary>
        public static string IconDirectory { get { return DataDirectory.EnsureDirectoryFormat() + "Images\\Icons\\"; } }

        /// <summary>
        /// Gets the Plugin Directory
        /// </summary>
        public static string TextureDirectory { get { return DataDirectory.EnsureDirectoryFormat() + "Textures\\"; } }

        /// <summary>
        /// Gets the Plugin Directory
        /// </summary>
        public static string ObjectDirectory { get { return DataDirectory.EnsureDirectoryFormat() + "Objects\\"; } }

        #endregion

        /// <summary>
        /// Active Game String
        /// </summary>
        public static string ActiveGame { get; set; }

        [Attributes.SystemTypesSupported(Enumerations.SystemTypesEnum.DEPENDANT)]
        /// <summary>
        /// Gets the Active Game In a File System Appropriate Format
        /// </summary>
        public static string ActiveGameFileSystemAppropriate
        {
            get
            {
                return ActiveGame.ToFileSystemAppropriate();
            }
        }

        /// <summary>
        /// The Base Game Graphics Device Ref
        /// </summary>
        public static GraphicsDevice GameGraphicsDevice { get; set; }

        #endregion

        #region METHODS
        [Attributes.SystemTypesSupported(Enumerations.SystemTypesEnum.WINDOWS)]
        /// <summary>
        /// Loads The Game Data Depending On The Platform
        /// </summary>
        /// <returns></returns>
        public static bool LoadGameData()
        {
#if WINDOWS
            return LoadGameDataWindows();
#elif ANDROID
            return LoadGameDataAndroid();
#else
            return false;
#endif

        }

        [Attributes.SystemTypesSupported(Enumerations.SystemTypesEnum.WINDOWS)]
        /// <summary>
        /// Init Game From The Game Object,  Loads the Textures, and Loads the XML Data
        /// </summary>
        /// <param name="BaseGameInfo"></param>
        /// <returns></returns>
        public static bool InitGame(Microsoft.Xna.Framework.Game BaseGameInfo)
        {
            BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            GameGraphicsDevice = BaseGameInfo.GraphicsDevice;

            // Loads the XML Game Data 
            if (LoadGameData() == false) { return false; }
            ActiveGame = GameConfiguration.InitialGame;
            LoadingStatus = Enumerations.GameLoadingStatuses.SystemDataLoaded;

            // Load the System Images
            ImageManager.LoadSystemImages();

            LoadingStatus = LoadingStatus | Enumerations.GameLoadingStatuses.SystemImagesLoaded;

            PluginManager.LoadPlugins();
            LoadingStatus = LoadingStatus | Enumerations.GameLoadingStatuses.PluginsLoaded;
            return true;
        }

        [Attributes.SystemTypesSupported(Enumerations.SystemTypesEnum.WINDOWS)]
        /// <summary>
        /// Loads Game Data For Windows Specific Code
        /// </summary>
        /// <returns></returns>
        internal static bool LoadGameDataWindows()
        {
            GameConfiguration = null;
            string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory + "Data\\";

            System.Xml.Serialization.XmlSerializer _Serializer = new System.Xml.Serialization.XmlSerializer(typeof(Data.GameConfiguration.NGGameConfiguration));
            using (var _reader = new System.IO.StreamReader(new System.IO.MemoryStream(Helper.IO.GetFile(BaseDirectory + "GameData.xml"))))
            {
                var _TmpData = (Data.GameConfiguration.NGGameConfiguration)_Serializer.Deserialize(_reader);
                if (_TmpData == null) { throw new Exception("Error Loading The Game Config"); }
                GameConfiguration = _TmpData;
            }
            _Serializer = null;

            return true;
        }

        /// <summary>
        /// Loads the Game Data For Android Specific Logic
        /// </summary>
        /// <returns></returns>
        internal static bool LoadGameDataAndroid()
        {
            var _UserStoreDirectory = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication();

            string _BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            return true;
        }
                
        #endregion

    }
}
