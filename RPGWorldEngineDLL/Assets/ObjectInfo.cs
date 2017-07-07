using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ACT.Core.Extensions;
using NebulaGames.RPGWorld.Graphics;
using System.Xml.Linq;

namespace NebulaGames.RPGWorld.Assets
{
    public class ObjectAnimationFrame
    {
        public string Name;
        public int ID;
        public int NextID;
        public int Delay;
        public Rectangle Source;
    }

    public class ObjectInfo
    {
        public virtual int GameID { get; set; }
        public string ID;
        public string Name;
        public string FileName;
        public string Description;
        public string DisplayName;
        public Rectangle StaticSource;
        public int FrameDelay;
        public string SoundName;
        public bool IsAnimated = false;

        public CompressibleImage Image;
        public List<ObjectAnimationFrame> AnimationFrames = new List<ObjectAnimationFrame>();
    }

    public class Objects
    {
        public string PackageName;
        public string Name;
        public string ID;
        public string AuthorName;
        public string Description;
        public string UpdateDate;
        public string BaseDirectory;
        public string DisplayName;
        public virtual int GameID { get; set; }

        public Dictionary<string, ObjectInfo> AllObjects = new Dictionary<string, ObjectInfo>();
        public Dictionary<string, string> LoadErrors = new Dictionary<string, string>();

        public Objects(string FileName, string PackageName = "")
        {
            if (FileName.FileExists() == false) { throw new System.IO.FileNotFoundException(FileName); }
            BaseDirectory = FileName.GetDirectoryFromFileLocation();

            XElement xelement = XElement.Load(FileName);

            this.Name = xelement.Element("name").Value;
            this.AuthorName = xelement.Element("author").Value;
            this.ID = xelement.Element("id").Value;
            this.Description = xelement.Element("description").Value;
            this.UpdateDate = xelement.Element("date").Value;
            this.DisplayName = xelement.Element("displayname").Value;
            this.PackageName = PackageName;

            IEnumerable<XElement> Objects = xelement.Elements().Where(x => x.Name.ToString() == "object");
            foreach (var Object in Objects)
            {
                ObjectInfo _NewObject = new ObjectInfo();
                _NewObject.ID = Object.Element("id").Value;
                _NewObject.Name = Object.Element("name").Value;
                _NewObject.Description = Object.Element("description").Value;
                _NewObject.DisplayName = Object.Element("displayname").Value;
                _NewObject.FileName = Object.Element("filename").Value;
                _NewObject.FrameDelay = Object.Element("framedelay").Value.ToInt(0);
                _NewObject.SoundName = Object.Element("sound").Value;

                _NewObject.Image = new CompressibleImage(System.Drawing.Image.FromFile(this.BaseDirectory.EnsureDirectoryFormat() + _NewObject.FileName), System.Drawing.Imaging.ImageFormat.Png);


                var DrawingRec = Object.Element("staticsource").Value.ToRectangle();
                if (DrawingRec == System.Drawing.Rectangle.Empty)
                {
                    _NewObject.StaticSource = Microsoft.Xna.Framework.Rectangle.Empty;
                    LoadErrors.Add(this.Name, "Invalid Static Source");
                }
                else
                {
                    _NewObject.StaticSource = new Microsoft.Xna.Framework.Rectangle(DrawingRec.X, DrawingRec.Y, DrawingRec.Width, DrawingRec.Height);
                }

                foreach (var ObjectFrame in Objects.Elements().Where(x => x.Name.ToString() == "frames").Elements().Where(x => x.Name.ToString() == "frame"))
                {
                    ObjectAnimationFrame _NewFrame = new ObjectAnimationFrame();
                    _NewFrame.ID = ObjectFrame.Element("id").Value.ToInt(0);
                    _NewFrame.Name = ObjectFrame.Element("name").Value;
                    _NewFrame.NextID = ObjectFrame.Element("nextid").Value.ToInt(0);
                    _NewFrame.Delay = ObjectFrame.Element("delay").Value.ToInt(0);
                    var DrawingRec2 = Object.Element("staticsource").Value.ToRectangle();
                    if (DrawingRec2 == System.Drawing.Rectangle.Empty)
                    {
                        _NewFrame.Source = Microsoft.Xna.Framework.Rectangle.Empty;
                        LoadErrors.Add(_NewObject.Name + " " + _NewFrame.ID.ToString(), "Invalid Frame Source");
                    }
                    else
                    {
                        _NewFrame.Source = new Microsoft.Xna.Framework.Rectangle(DrawingRec2.X, DrawingRec2.Y, DrawingRec2.Width, DrawingRec2.Height);
                    }

                    _NewObject.AnimationFrames.Add(_NewFrame);
                }

                AllObjects.Add(_NewObject.Name, _NewObject);
            }

        }
    }
}
