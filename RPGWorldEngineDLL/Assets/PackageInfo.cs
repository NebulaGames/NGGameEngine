using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NebulaGames.RPGWorld.Graphics;
using ACT.Core.Extensions;
using ACT.Core.Dynamic.XML;
using System.Xml.Linq;

namespace NebulaGames.RPGWorld.Assets
{
    public class PackageInfo
    {
        public string Name;
        public string Author;
        public string DisplayName;
        public string BaseDirectory;
        public DateTime EditDate;

        public List<Objects> LoadedObjects = new List<Objects>();
        public List<Textures> LoadedTextures = new List<Textures>();
        public List<Actors> LoadedActors = new List<Actors>();

        public Dictionary<string, string> LoadErrors = new Dictionary<string, string>();

        public PackageInfo(string FileName)
        {
            BaseDirectory = FileName.GetDirectoryFromFileLocation();

            XElement xelement = XElement.Load(FileName);
           
            this.Name = xelement.Element("packagename").Value;
            this.Author = xelement.Element("author").Value;
            this.DisplayName = xelement.Element("displayname").Value;

            try
            {
                this.EditDate = Convert.ToDateTime(xelement.Element("date").Value);
            }
            catch
            {
                this.EditDate = DateTime.MinValue;
            }

            IEnumerable<XElement> PackageObjects = xelement.Elements().Where(x => x.Name.ToString() == "objects");
            foreach (var packageobject in PackageObjects.Elements().Where(x => x.Name.ToString() == "object"))
            {
                string _ObjectLocation = BaseDirectory.NavigateUpDirectory(2, true);

                Objects _NewObject = new Assets.Objects(_ObjectLocation + "objects\\" + packageobject.Element("name").Value + "\\info.xml", this.Name);
                LoadedObjects.Add(_NewObject);                
            }

            IEnumerable<XElement> TextureObjects = xelement.Elements().Where(x => x.Name.ToString() == "textures");
            foreach (var textureobject in TextureObjects.Elements().Where(x => x.Name.ToString() == "texture"))
            {
                string _ObjectLocation = BaseDirectory.NavigateUpDirectory(2, true);

                Textures _NewTextures = new Assets.Textures(_ObjectLocation + "textures\\" + textureobject.Element("name").Value + "\\info.xml", this.Name);
                LoadedTextures.Add(_NewTextures);
            }

            IEnumerable<XElement> ActorObjects = xelement.Elements().Where(x => x.Name.ToString() == "actors");
            foreach (var actorobject in ActorObjects.Elements().Where(x => x.Name.ToString() == "actor"))
            {
                string _ObjectLocation = BaseDirectory.NavigateUpDirectory(2, true);

                Actors _NewActors = new Assets.Actors(_ObjectLocation + "actors\\" + actorobject.Element("name").Value + "\\info.xml", this.Name);
                LoadedActors.Add(_NewActors);
            }

            ActorObjects = null;
            TextureObjects = null;
            PackageObjects = null;
            xelement = null;
        }

        public TextureInfo FindTextureByID(string ID)
        {
            foreach (Textures T in LoadedTextures)
            {
                try
                {
                    var _TmpReturn = T.AllTextures.Where(x => x.Value.ID == ID).First().Value;
                    if (_TmpReturn != null)
                    {
                        return _TmpReturn;
                    }
                }
                catch { }
            }

            return null;
        }

        public ObjectInfo FindObjectByID(string ID)
        {
            foreach (Objects T in LoadedObjects)
            {
                try
                {
                    var _TmpReturn = T.AllObjects.Where(x => x.Value.ID == ID).First().Value;
                    if (_TmpReturn != null)
                    {
                        return _TmpReturn;
                    }
                }
                catch { }
            }

            return null;
        }
    }
}
