using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACT.Core.Extensions;
using ACT.Core.Exceptions;
using System.Dynamic;

namespace NebulaGames.RPGWorld.IDE
{
    public static class SpriteBuilder_Support
    {
        /// <summary>
        /// Returns All The Textures In The Textures Directory
        /// </summary>
        /// <param name="BaseDirectory"></param>
        /// <param name="FileTypes"></param>
        /// <param name="LoadedTextures"></param>
        /// <returns></returns> 
        public static Dictionary<string, ContentFileInfo> LoadTextures(string BaseDirectory, string[] FileTypes = null, Dictionary<string, ContentFileInfo> LoadedTextures = null)
        {
            Dictionary<string, ContentFileInfo> TmpReturn = new Dictionary<string, ContentFileInfo>();

            if (LoadedTextures != null)
            {
                foreach (var key in LoadedTextures.Keys)
                {
                    TmpReturn.Add(key, LoadedTextures[key]);
                }
            }

            if (FileTypes == null) { FileTypes = new string[] { "png", "jpg", "bmp" }; }

            if (BaseDirectory.DirectoryExists() == false)
            {
                throw new InvalidPathException(BaseDirectory);
            }

            // Load Files In Folder
            // Folder Should contain info.txt describing the images
            var _Files = System.IO.Directory.GetFiles(BaseDirectory);
            var _InfoFile = from f in _Files where f.ToLower().EndsWith("info.txt") select f;

            if (_InfoFile != null && _InfoFile.Count() == 1)
            {
                dynamic _TextureData = ACT.Core.Dynamic.Encoder.LoadFromFile(BaseDirectory.EnsureDirectoryFormat() + _InfoFile.First().GetFileNameFromFullPath());
                string _PackageName = _TextureData.name;
                string _PackageDescription = _TextureData.description;

                // Loop Through Each Background Definition
                foreach (var ObjectName in _TextureData.backgrounds.GetMemberNames)
                {
                    if (ObjectName == "name") { continue; }
                    var _FileName = _TextureData.backgrounds.GetObject(ObjectName).filename;
                    string _Description = _TextureData.backgrounds.GetObject(ObjectName).description;
                    var img_fs = from fs in _Files where FileTypes.Contains(fs.ToLower().Right(3)) select fs.ToString();

                    if (TmpReturn.ContainsKey(_FileName) == false)
                    {
                        ContentFileInfo _New = new ContentFileInfo();
                        _New.PackageName = _PackageName;
                        _New.Directory = BaseDirectory;
                        _New.FileName = _FileName;
                        _New.FileDescription = _Description;

                        TmpReturn.Add(_PackageName + "\\" + _FileName, _New);
                    }

                }
            }

            // Loop Through Each Folder
            foreach (var s in System.IO.Directory.GetDirectories(BaseDirectory))
            {
                TmpReturn = LoadTextures(s, FileTypes, TmpReturn);
            }

            return TmpReturn;
        }

        public static List<dynamic> LoadListOfConfigurationFiles(string Directory)
        {
            var _TmpReturn = new List<dynamic>();

            foreach (var file in System.IO.Directory.GetFiles(Directory).Where(x => x.ToLower().EndsWith("txt")))
            {
                var _TmpDynamic = ACT.Core.Dynamic.Encoder.LoadFromFile(file);

                if (_TmpDynamic != null)
                {
                    _TmpReturn.Add(_TmpDynamic);
                }
            }

            return _TmpReturn;
        }
    }
}
