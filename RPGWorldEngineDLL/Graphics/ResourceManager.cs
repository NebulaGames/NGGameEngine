using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;


using System.Linq;
using ACT.Core.Extensions;
using NebulaGames.RPGWorld.Assets;

namespace NebulaGames.RPGWorld.Graphics
{
    public class LoadResults
    {
        public long TotalBytesLoaded;
        public int TotalImagesLoaded;
        public int TotalImagesFailed;
    }
    public static class ResourceManager
    {
        static Dictionary<string, CompressibleImage> _MapObjectCache = new Dictionary<string, CompressibleImage>();
        static Dictionary<string, CompressibleImage> _SystemImageCache = new Dictionary<string, CompressibleImage>();
        static Dictionary<string, TextureInfo> _SystemTextureCache = new Dictionary<string, TextureInfo>();

        /// <summary>
        /// Loads All The System Resources Found In The Directory Specified
        /// </summary>
        /// <param name="BaseDirectory"></param>
        /// <returns></returns>
        public static LoadResults LoadSystemImages(string BaseDirectory)
        {
            LoadResults _TmpReturn = new LoadResults();

            if (Directory.Exists(BaseDirectory))
            {
                _SystemImageCache.Clear();
                // Load All Objects Found
                foreach (string _ObjFile in Directory.GetFiles(BaseDirectory))
                {
                    if (_ObjFile.EndsWith(".png") == false) { continue; }

                    System.IO.FileInfo _TmpFileInfo = new FileInfo(_ObjFile);
                    _TmpReturn.TotalBytesLoaded += _TmpFileInfo.Length;
                    _TmpFileInfo = null;

                    try
                    {
                        CompressibleImage _TmpImage = new CompressibleImage(Image.FromFile(_ObjFile), System.Drawing.Imaging.ImageFormat.Png);
                        _SystemImageCache.Add(_ObjFile.GetFileName(false), _TmpImage);
                        _TmpReturn.TotalImagesLoaded++;
                    }
                    catch (Exception ex)
                    {
                        ErrorLogable.LogError(ex, "Error Loading Resource: " + BaseDirectory + _ObjFile, ACT.Core.Enums.ErrorLevel.Critical);
                        _TmpReturn.TotalImagesFailed++;
                    }
                }
            }
            else { _TmpReturn.TotalImagesLoaded = -1; ErrorLogable.LogError(null, "Error Loading Resources From Directory: " + BaseDirectory, ACT.Core.Enums.ErrorLevel.Critical); }

            return _TmpReturn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BaseDirectory"></param>
        /// <returns></returns>
        public static LoadResults LoadSystemTextures(string BaseDirectory)
        {
            string[] _TextureDirectories = System.IO.Directory.GetDirectories(BaseDirectory);
            LoadResults _TmpReturn = new LoadResults();

            for (int x = 0; x < _TextureDirectories.Count(); x++)
            {
                if (!System.IO.File.Exists(_TextureDirectories[x].EnsureDirectoryFormat() + "info.txt")) { continue; }

                dynamic TextureInfo = ACT.Core.Dynamic.Encoder.LoadFromFile(_TextureDirectories[x].EnsureDirectoryFormat() + "info.txt");

                var _MemberNames = TextureInfo.backgrounds.GetMemberNames;

                for (int xx = 0; xx < _MemberNames.Count; xx++)
                {
                    if (_MemberNames[xx] == "name") { continue; }
                    TextureInfo _TmpNewTexture = new Assets.TextureInfo();
                    _TmpNewTexture.FileName = TextureInfo.backgrounds.GetObject(_MemberNames[xx]).GetObject("filename");

                    if (!System.IO.File.Exists(_TextureDirectories[x].EnsureDirectoryFormat() + _TmpNewTexture.FileName))
                    {
                        _TmpReturn.TotalImagesFailed++;
                        ErrorLogable.LogError(null, "Image File Not Found: " + _TmpNewTexture.FileName, ACT.Core.Enums.ErrorLevel.Critical);
                        continue;
                    }
                    _TmpReturn.TotalImagesLoaded++;

                    _TmpNewTexture.PackageName = _TextureDirectories[x].GetDirectoryName();
                    _TmpNewTexture.Description = TextureInfo.backgrounds.GetObject(_MemberNames[xx]).GetObject("description");
                    _TmpNewTexture.DisplayName = TextureInfo.backgrounds.GetObject(_MemberNames[xx]).GetObject("displayname");
                    _TmpNewTexture.FileName = TextureInfo.backgrounds.GetObject(_MemberNames[xx]).GetObject("filename");
                    _TmpNewTexture.ID = TextureInfo.backgrounds.GetObject(_MemberNames[xx]).GetObject("id");
                    _TmpNewTexture.Name = TextureInfo.backgrounds.GetObject(_MemberNames[xx]).GetObject("name");
                    _TmpNewTexture.Image = new CompressibleImage(new Bitmap(_TextureDirectories[x].EnsureDirectoryFormat() + _TmpNewTexture.FileName), System.Drawing.Imaging.ImageFormat.Png);
                    _SystemTextureCache.Add(_TmpNewTexture.Name, _TmpNewTexture);
                }
            }

            return _TmpReturn;
        }

        /// <summary>
        /// Return All The Distinct Packages as a List<string>
        /// </summary>
        /// <returns></returns>
        public static List<string> GetTexturePackages()
        {
            var x = from tex in _SystemTextureCache.Values select tex.PackageName;
            var ty = x.Distinct().ToList<string>();
            return ty;
        }

        /// <summary>
        /// Return All The Textures With The PackageName Specified
        /// </summary>
        /// <param name="Name">Package Name</param>
        /// <returns></returns>
        public static List<TextureInfo> GetTexturesByPackageName(string Name)
        {
            var x = from tex in _SystemTextureCache.Values where tex.PackageName == Name select tex;
            var ty = x.ToList<TextureInfo>();
            return ty;
        }

        /// <summary>
        /// Returns the Decompressed Image For The Texture Specified
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Image GetTextureImage(string id)
        {
            var tex = from x in _SystemTextureCache.Values where x.ID == id select x.Image;
            return tex.First().GetDecompressedImage();
        }

        /// <summary>
        /// Get The UnCompressed System Image
        /// </summary>
        /// <param name="ResourceName"></param>
        /// <returns></returns>
        public static Image GetSystemImage(string ResourceName)
        {
            if (_SystemImageCache.ContainsKey(ResourceName))
            {
                return _SystemImageCache[ResourceName].GetDecompressedImage();
            }
            else if (_SystemImageCache.ContainsKey(ResourceName.Replace(".png", "").Replace(".Png","")))
            {
                return _SystemImageCache[ResourceName.Replace(".png","")].GetDecompressedImage();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Load All The Objects Found In The BaseDirectory.  Searchs SubFolders As Well;
        /// </summary>
        /// <param name="BaseDirectory"></param>
        public static void LoadObjects(string BaseDirectory)
        {
            if (Directory.Exists(BaseDirectory + "Objects\\"))
            {
                _MapObjectCache.Clear();
                // Load All Objects Found
                foreach (string _ObjFile in Directory.GetFiles(BaseDirectory + "Objects\\"))
                {
                    if (_ObjFile.EndsWith(".png") || _ObjFile.EndsWith(".bmp"))
                    {
                        Image _TImage = Image.FromFile(_ObjFile);
                        _MapObjectCache.Add(_ObjFile.Substring(_ObjFile.LastIndexOf("\\") + 1), new CompressibleImage(_TImage, System.Drawing.Imaging.ImageFormat.Png));
                    }
                }
            }
        }

        public static void AddObject(string Name, CompressibleImage Obj)
        {
            if (_MapObjectCache.ContainsKey(Name))
            {
                _MapObjectCache[Name] = Obj;
            }
            else
            {
                _MapObjectCache.Add(Name, Obj);
            }
        }
        public static List<string> GetMapObjectKeys()
        {
            return _MapObjectCache.Keys.ToList<string>();
        }

        public static void CompressResources()
        {
            foreach (var o in _MapObjectCache.Values)
            {
                if (o.IsCompressed == false)
                {
                    o.ClearDecompressedImage();
                }
            }
        }

        /// <summary>
        /// Starts the Get Image Process..  Must Call End after Using Image To Restore Memory Gains
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static Image GetMapObjectImageStart(string Key)
        {
            return _MapObjectCache[Key].GetDecompressedImage();
        }
        public static void GetMapObjectImageEnd(string Key)
        {
            _MapObjectCache[Key].ClearDecompressedImage();
        }
    }
}
