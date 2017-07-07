using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using NebulaGames.RPGWorld.Performance;
using Microsoft.Xna.Framework.Graphics;
using NebulaGames.RPGWorld.Attributes;

namespace NebulaGames.RPGWorld
{
    /// <summary>
    /// Extension of the GameData Class
    /// </summary>
    public static partial class GameData
    {
         /// <summary>
        /// Manages All Textures
        /// </summary>
        public static partial class ImageManager
        {
            #region EVENTS / DELEGATES
            public delegate void ProcessingUpdate(int Increment, int Total);
            public static event ProcessingUpdate OnProcessingUpdate;
            #endregion

            #region VARIABLES
            // Future Use Store Textures On Disk When Optimized To Save Memory Space
            // private static Dictionary<string, TextureData> _GameTextures { get; set; }

            /// <summary>
            /// RAW Images Non Optimized
            /// Format of this is the Source, Dictionary key=imagename value=texture2d
            /// </summary>
            public static Dictionary<ulong, ImageData> GameImages { get; set; }

            /// <summary>
            /// Used to Track Texture Usage.  More Usage Grants Better Optimization
            /// </summary>
            public static Dictionary<ulong, int> ImageUsageCount { get; set; }

            /// <summary>
            /// Optimized Textures Reference
            /// </summary>
            public static Dictionary<string, Texture2D> OptimizedImages = new Dictionary<string, Texture2D>();

            /// <summary>
            /// Contains Pointers To the Optimized Image Data That exists in the OptimizedTextures
            /// </summary>
            public static Dictionary<ulong, OptimizedData> OptimizedRefrences = new Dictionary<ulong, OptimizedData>();

            /// <summary>
            /// IS the Class Stil Processing
            /// </summary>
            public static bool IsProcessing = false;

            /// <summary>
            /// Has the System Images Been Loaded
            /// </summary>
            public static bool SystemImagesLoaded = false;

            /// <summary>
            /// Contains The Atlas Data For The Lookups Used In Optimization
            /// </summary>
            public static string ImageAtlasData { get; set; }

          

            #endregion

            #region PUBLIC METHODS
            
            [Attributes.SystemTypesSupported(Enumerations.SystemTypesEnum.WINDOWS | Enumerations.SystemTypesEnum.ANDROID | Enumerations.SystemTypesEnum.LINUX | Enumerations.SystemTypesEnum.IOS)]
            /// <summary>
            /// Init the Image Manager
            /// </summary>
            public static void InitImageManager()
            {
                LoadSystemImages();
                LoadPluginImages();
                LoadGameImages();                
            }

            [Attributes.SystemTypesSupported(Enumerations.SystemTypesEnum.DEPENDANT)]
            /// <summary>
            /// Gets the Game Image
            /// </summary>
            /// <param name="Path"></param>
            /// <returns></returns>
            public static Texture2D GetGameImageDirectly(string Path)
            {
                try
                {
                    var _TmpReturn = Texture2D.FromStream(GameGraphicsDevice, new System.IO.MemoryStream(Helper.IO.GetFile(Path)));
                    return _TmpReturn;
                }
                catch (Exception ex) { Helper.LogError(ex); return null; }
            }

            [Attributes.SystemTypesSupported(Enumerations.SystemTypesEnum.WINDOWS)]
            /// <summary>
            /// Load all the Game Specific Images
            /// </summary>
            public static void LoadGameImages()
            {
                foreach(var game in GameData.GameConfiguration.Games.Games)
                {
                    string _TmpDirectory = GameData.GamesDirectory + game.SourceName + "\\Images\\";

                    var _AllImageFiles = Helper.IO.GetDirectoryFiles(_TmpDirectory, Helper.IO.FileTypes.Png | Helper.IO.FileTypes.Jpg | Helper.IO.FileTypes.Bmp);

                    int _count = _AllImageFiles.Count();

                    foreach (var img in _AllImageFiles)
                    {
                        AddImage(game.SourceName, img.GetFileNameFromFullPath(false), img, true);
                        OnProcessingUpdate?.Invoke(1, _count);
                    }
                }
            }

            /// <summary>
            /// Load Plugin Images
            /// </summary>
            public static void LoadPluginImages()
            {
                foreach(var plugin in GameData.GameConfiguration.Plugins.Plugins)
                {
                    string _PluginImageDirectory = GameData.PluginDirectory + plugin.SourceName + "\\Images\\";
                    var _AllImageFiles = Helper.IO.GetDirectoryFiles(_PluginImageDirectory, Helper.IO.FileTypes.Bmp | Helper.IO.FileTypes.Jpg | Helper.IO.FileTypes.Png);

                    int _count = _AllImageFiles.Count();

                    foreach (var img in _AllImageFiles)
                    {
                        AddImage(plugin.Name, img.GetFileNameFromFullPath(false), img, true);
                        OnProcessingUpdate?.Invoke(1, _count); 
                    }
                }
            }

            /// <summary>
            /// Load all the System Images
            /// </summary>
            public static void LoadSystemImages()
            {
                IsProcessing = true;
               

                LoadImagesFromDirectory(GameData.SystemDirectory, "System", true, "*.png");

                //foreach (var texDir in GameData.GameConfiguration.Textures.Directories.Directories)
                //{
                //    if (_BaseDirectory.ToLower().Contains((GameData.BaseDirectory.EnsureDirectoryFormat() + texDir.RelativePath).ToLower()) == false)
                //    {
                //        LoadImagesFromDirectory(GameData.BaseDirectory.EnsureDirectoryFormat() + texDir.RelativePath, texDir.SourceName, texDir.GetSubFolders, "*.png");
                //    }
                //}

                SystemImagesLoaded = true;
                IsProcessing = false;
            }

            ///// <summary>
            ///// Load all the Texture Images
            ///// </summary>
            //public static void LoadTextureImages()
            //{
            //    IsProcessing = true;
            //    string _BaseDirectory = GameData.BaseDirectory.EnsureDirectoryFormat();
            //    _BaseDirectory += "Data\\Images\\Textures\\";

            //    LoadImagesFromDirectory(_BaseDirectory, "TextureRoot", true, "*.png");

            //    foreach (var texDir in GameData.GameConfiguration.Textures.Directories.Directories)
            //    {
            //        if (_BaseDirectory.ToLower().Contains((GameData.BaseDirectory.EnsureDirectoryFormat() + texDir.RelativePath).ToLower()) == false)
            //        {
            //            LoadImagesFromDirectory(GameData.BaseDirectory.EnsureDirectoryFormat() + texDir.RelativePath, texDir.SourceName, texDir.GetSubFolders, "*.png");
            //        }
            //    }

            //    SystemImagesLoaded = true;
            //    IsProcessing = false;
            //}

            /// <summary>
            /// Get the Encoded Image File Name (Source = FileName (ToLower))
            /// </summary>
            /// <param name="Source"></param>
            /// <param name="FileName"></param>
            /// <returns></returns>
            public static string GetEncodedImageName(string Source, string FileName, Enumerations.VideoModes VideoMode = Enumerations.VideoModes.Normal)
            {
                return Source.ToLower() + "_" + FileName.ToLower() + "_" + VideoMode.ToString();
            }

            /// <summary>
            /// Gets the ULong
            /// </summary>
            /// <param name="Source"></param>
            /// <param name="FileName"></param>
            /// <param name="VideoMode"></param>
            /// <returns></returns>
            public static ulong GetUlongEncodedImageID(string Source, string FileName, Enumerations.VideoModes VideoMode = Enumerations.VideoModes.Normal)
            {
                return (Source.ToLower() + "_" + FileName.ToLower() + "_" + VideoMode.ToString()).GetHashCodeUlong();
            }

            /// <summary>
            /// Add Image To the Image Manager
            /// </summary>
            /// <param name="Source"></param>
            /// <param name="ImageName"></param>
            /// <param name="FileLocation"></param>
            /// <param name="Replace"></param>
            public static ulong AddImage(string Source, string ImageName, string FileLocation, bool Replace = false)
            {
                if ((GameData.LoadingStatus & Enumerations.GameLoadingStatuses.GameDataLoaded) == Enumerations.GameLoadingStatuses.GameDataLoaded) { throw new Exception("Game Not Initialized!"); }

                ulong _FileNameEncoded = GetUlongEncodedImageID(Source, ImageName);
                bool _contains = GameImages.ContainsKey(_FileNameEncoded);

                if ((_contains == true && Replace == true) || _contains == false)
                {
                    Texture2D _NewTexture = Texture2D.FromStream(GameGraphicsDevice, new System.IO.FileStream(FileLocation, System.IO.FileMode.Open));

                    ImageData _TextureData = new ImageData(_FileNameEncoded);
                    _TextureData.BaseTexture = _NewTexture;
                    _TextureData.FileName = ImageName;
                    _TextureData.Optimized = false;
                    _TextureData.OptimizedLocations = new Dictionary<int, string>();
                    _TextureData.OriginalFilePath = FileLocation;
                    _TextureData.Extension = FileLocation.Substring(FileLocation.LastIndexOf(".") + 1);

                    if (_contains == false)
                    {
                        GameImages.Add(_FileNameEncoded, _TextureData);
                    }
                    else
                    {
                        GameImages[_FileNameEncoded] = _TextureData;
                    }
                }

                return _FileNameEncoded;
            }

            // *************************************************************************************************************************
            #region Get Image Methods

            /// <summary>
            /// Gets the Texture 2d.  Looks at both optimized data and or Non Optimized Data
            /// </summary>
            /// <param name="Source"></param>
            /// <param name="FileName"></param>
            /// <returns></returns>
            public static Texture2D GetTexture2d(string Source, string FileName, Enumerations.VideoModes VideoMode = Enumerations.VideoModes.Normal)
            {
                return GetTexture2d(GetUlongEncodedImageID(Source, FileName, VideoMode));
            }

            /// <summary>
            /// Gets the Texture From The ImageID
            /// </summary>
            /// <param name="ImageID"></param>
            /// <returns></returns>
            public static Texture2D GetTexture2d(ulong ImageID)
            {
                if (GameImages.ContainsKey(ImageID) == false) { return null; }

                if (GameImages[ImageID].Optimized == false)
                {
                    if (ImageUsageCount.ContainsKey(ImageID) == false) { ImageUsageCount.Add(ImageID, 0); }
                    ImageUsageCount[ImageID] = ImageUsageCount[ImageID] + 1;
                    return GameImages[ImageID].BaseTexture;
                }
                else
                {
                    if (ImageUsageCount.ContainsKey(ImageID) == false) { ImageUsageCount.Add(ImageID, 0); }
                    ImageUsageCount[ImageID] = ImageUsageCount[ImageID] + 1;

                    return OptimizedImages[OptimizedRefrences[ImageID].SpriteSheetName];                    
                }
            }

            #endregion
            // *************************************************************************************************************************

            
            [SystemTypesSupported(Enumerations.SystemTypesEnum.WINDOWS)]
            /// <summary>
            /// Loads All the Images In The Specified Directory
            /// </summary>
            /// <param name="DirectoryRoot">Directory To Start Search In</param>
            /// <param name="Source">Name For This Load</param>
            /// <param name="SubFolders">Include Sub Folders In Search</param>
            /// <param name="Pattern">Image Pattern To Match</param>
            private static void LoadImagesFromDirectory(string DirectoryRoot, string Source, bool SubFolders = true, string Pattern = ".png")
            {
                System.IO.SearchOption _SearchOption;
                if (SubFolders == true) { _SearchOption = System.IO.SearchOption.AllDirectories; }
                else { _SearchOption = System.IO.SearchOption.TopDirectoryOnly; }

                var _AllFiles = System.IO.Directory.GetFiles(DirectoryRoot, "*.*", _SearchOption);
                List<string> MatchingFiles = new List<string>();

                string[] _PatternData = Pattern.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                foreach(string pattern in _PatternData)
                {
                    MatchingFiles.AddRange(_AllFiles.Where(x => x.EndsWith(pattern)));
                }
                
                int _count = MatchingFiles.Count();

                foreach (var img in MatchingFiles)
                {
                    AddImage(Source, img.GetFileNameFromFullPath(false), img, true);
                    if (OnProcessingUpdate != null) { OnProcessingUpdate(1, _count); }
                }
            }

            #endregion
        }
    }
}
