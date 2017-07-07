using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebulaGames.RPGWorld.Data.Textures
{
    public class NGTextureManager
    {
        public List<NGTextureFile> TextureFiles = new List<NGTextureFile>();

        public NGTextureFile LoadTextureFile(string PathToXML)
        {
            System.Xml.Serialization.XmlSerializer _Serializer = new System.Xml.Serialization.XmlSerializer(typeof(Data.GameConfiguration.NGGameConfiguration));
            using (var _reader = new System.IO.StreamReader(new System.IO.MemoryStream(Helper.IO.GetFile(PathToXML))))
            {
                var _TmpData = (Data.Textures.NGTextureFile)_Serializer.Deserialize(_reader);
                if (_TmpData == null) { throw new Exception("Error Loading The Game Config"); }

                if (TextureFiles.Where(x => x.ID == _TmpData.ID).Count() > 0) { return null; }
                else { TextureFiles.Add(_TmpData); return _TmpData; }
            }            
        }

        /// <summary>
        /// Loads all the Texture Data
        /// </summary>
        public void LoadAllTextures()
        {
            string _BaseTexturePath = GameData.TextureDirectory;

            var _AllDirectories = System.IO.Directory.GetDirectories(_BaseTexturePath);

            foreach(string directory in _AllDirectories)
            {
                string _InfoPath = directory.EnsureDirectoryFormat() + "info.xml";

                if (System.IO.File.Exists(_InfoPath))
                {
                    // Texture Exists Load The XML File
                    try
                    {
                        var _LoadedTextureFile = LoadTextureFile(_InfoPath);
                        // Load All The Actual Images;
                        foreach(var t in _LoadedTextureFile.Textures.Textures)
                        {
                            string _FullFilePath = directory.EnsureDirectoryFormat() + t.FileName.GetFileNameFromFullPath(true);
                            var _ImageID = GameData.ImageManager.AddImage(_LoadedTextureFile.Name, t.FileName, _FullFilePath, false);
                            t.ImageID = _ImageID;
                        }
                        
                    }
                    catch(Exception ex)
                    {
                        ErrorLogable.LogError(ex, "Error Loading XML Texture File: " + _InfoPath, ACT.Core.Enums.ErrorLevel.Severe);
                    }
                }
            }

        }
    }
}
