using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NebulaGames.RPGWorld.Graphics;
using ACT.Core.Extensions;
using System.Xml.Linq;

namespace NebulaGames.RPGWorld.Assets
{

    public class Textures
    {
        public string PackageName;
        public string AuthorName;
        public string Description;
        public string UpdateDate;
        public string BaseDirectory;
        public string ID;
        public string Name;
        public virtual int GameID { get; set; }
        public Dictionary<string, TextureInfo> AllTextures = new Dictionary<string, TextureInfo>();
        public Dictionary<string, string> LoadErrors = new Dictionary<string, string>();

        public Textures(string FileName, string PackageName = "")
        {
            if (FileName.FileExists() == false) { throw new System.IO.FileNotFoundException(FileName); }
            BaseDirectory = FileName.GetDirectoryFromFileLocation();

            XElement xelement = XElement.Load(FileName);

            this.Name = xelement.Element("name").Value;
            this.AuthorName = xelement.Element("author").Value;
            this.ID = xelement.Element("id").Value;
            this.Description = xelement.Element("description").Value;
            this.UpdateDate = xelement.Element("date").Value;
            this.PackageName = PackageName;

            IEnumerable<XElement> TextureObjects = xelement.Elements().Where(x => x.Name.ToString() == "textures");
            foreach (var TextureObject in TextureObjects.Elements().Where(x => x.Name.ToString() == "texture"))
            {
                
                TextureInfo _NewTexture = new Assets.TextureInfo();
                _NewTexture.ID = TextureObject.Element("id").Value;
                _NewTexture.Name = TextureObject.Element("name").Value;
                _NewTexture.FileName = TextureObject.Element("filename").Value;

                if (System.IO.File.Exists(this.BaseDirectory.EnsureDirectoryFormat() + _NewTexture.FileName) == false)
                {
                    this.LoadErrors.Add("Error Locating Image File", this.BaseDirectory.EnsureDirectoryFormat() + _NewTexture.FileName);
                    continue;
                }

                _NewTexture.Description = TextureObject.Element("description").Value;
                _NewTexture.DisplayName = TextureObject.Element("displayname").Value;
                _NewTexture.AnimationSeq = TextureObject.Element("animationseq").Value.ToInt();
                _NewTexture.AnimationGroup = TextureObject.Element("animationgroup").Value;
                _NewTexture.AnimationDisplayTime = TextureObject.Element("animationdisplaytime").Value.ToInt();
                _NewTexture.Image = new CompressibleImage(System.Drawing.Image.FromFile(this.BaseDirectory.EnsureDirectoryFormat() + _NewTexture.FileName), System.Drawing.Imaging.ImageFormat.Png);
                
                AllTextures.Add(_NewTexture.Name, _NewTexture);
            }           
        }
    }

    public class TextureInfo
    {
        public virtual int GameID { get; set; }
        public string ID;
        public string Name;
        public string FileName;
        public string Description;
        public string DisplayName;
        public CompressibleImage Image;
        public string PackageName;
        public int AnimationSeq;
        public string AnimationGroup;
        public int AnimationDisplayTime;
        string _baseDirectory;

        public TextureInfo()
        {

        }
        public TextureInfo(string id, string name, string displayname, string description, string filename, string basedirectory)
        {
            ID = id; Name = name; DisplayName = displayname; Description = description; FileName = filename; _baseDirectory = basedirectory;
            
            if (System.IO.File.Exists(basedirectory.EnsureDirectoryFormat() + filename) == false)
            {
                throw new System.IO.FileNotFoundException("File Not Found " + basedirectory.EnsureDirectoryFormat() + filename);
            }

            Image = new CompressibleImage(System.Drawing.Image.FromFile(basedirectory.EnsureDirectoryFormat() + filename), System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
