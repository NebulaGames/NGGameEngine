using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ACT.Core.Types;
using ACT.Core.Extensions;
using System.Xml.Linq;
using NebulaGames.RPGWorld.Graphics;
namespace NebulaGames.RPGWorld.Assets
{

    public class Actors
    {
        public Actors() { }
        public Actors(string FileName, string PackageName = "")
        {
            if (FileName.FileExists() == false) { throw new System.IO.FileNotFoundException(FileName); }
            BaseDirectory = FileName.GetDirectoryFromFileLocation();

            XElement xelement = XElement.Load(FileName);

            this.ID = xelement.Element("id").Value;
            this.Name = xelement.Element("name").Value;
            this.DisplayName = xelement.Element("displayname").Value;
            this.Description = xelement.Element("description").Value;
            this.AuthorName = xelement.Element("author").Value;
            this.UpdateDate = xelement.Element("date").Value;
            this.PackageName = PackageName;

            IEnumerable<XElement> ActorObjects = xelement.Elements().Where(x => x.Name.ToString() == "actors");
            foreach (var ActorObject in ActorObjects.Elements().Where(x => x.Name.ToString() == "actor"))
            {
                ActorInfo _NewActor = new ActorInfo();
                _NewActor.ID = ActorObject.Element("id").Value;
                _NewActor.Name = ActorObject.Element("name").Value;
                _NewActor.DisplayName = ActorObject.Element("displayname").Value;
                _NewActor.Description = ActorObject.Element("description").Value;
                _NewActor.AuthorName = ActorObject.Element("author").Value;
                _NewActor.UpdateDate = ActorObject.Element("date").Value;
                _NewActor.DefaultFrameDelay = ActorObject.Element("defaultframedelay").Value.ToInt(0);

                IEnumerable<XElement> ActorAnimationObjects = ActorObject.Elements().Where(x => x.Name.ToString() == "animations");
                foreach (var ActorAnimationObject in ActorAnimationObjects.Elements().Where(x => x.Name.ToString() == "animation"))
                {
                    ActorAnimation _NewAnimation = new ActorAnimation();
                    _NewAnimation.Name = ActorAnimationObject.Element("name").Value;
                    _NewAnimation.Filename = ActorAnimationObject.Element("filename").Value;

                    if (System.IO.File.Exists(this.BaseDirectory + _NewAnimation.Filename) == false)
                    {
                        this.LoadErrors.Add(_NewActor.ID + "-" + _NewAnimation.Name + "-Error Locating Image File", this.BaseDirectory.EnsureDirectoryFormat() + _NewAnimation.Filename);
                        continue;
                    }
                    if (this.Images.ContainsKey(_NewAnimation.Filename))
                    {

                    }
                    else
                    {
                        this.Images.Add(_NewAnimation.Filename, new CompressibleImage(System.Drawing.Image.FromFile(this.BaseDirectory.EnsureDirectoryFormat() + _NewAnimation.Filename), System.Drawing.Imaging.ImageFormat.Png));
                    }

                    _NewAnimation.Direction = ActorAnimationObject.Element("direction").Value;

                    try
                    {
                        _NewAnimation.DirectionValue = (Enumerations.RelativePosition)Enum.Parse(typeof(Enumerations.RelativePosition), _NewAnimation.Direction);
                    }
                    catch
                    {
                        LoadErrors.Add(_NewActor.ID + "-Animation-" + _NewAnimation.Name + "Direction", "Animation: " + _NewAnimation.Name + " - Error Converting Direction");
                    }

                    IEnumerable<XElement> ActorAnimationFrameObjects = ActorAnimationObject.Elements().Where(x => x.Name.ToString() == "frames");
                    foreach (var ActorAnimationFrameObject in ActorAnimationFrameObjects.Elements().Where(x => x.Name.ToString() == "frame"))
                    {
                        ActorAnimationFrame _NewFrame = new ActorAnimationFrame();
                        _NewFrame.ID = ActorAnimationFrameObject.Attribute("id").Value.ToInt(-1);
                        if (ActorAnimationFrameObject.Attribute("nextid").Value.StartsWith("{"))
                        {
                            // TODO Add Split Method
                        }
                        else
                        {
                            _NewFrame.NextID.Add(ActorAnimationFrameObject.Attribute("nextid").Value.ToInt(-1));
                        }

                        var DrawingRec2 = ActorAnimationFrameObject.Attribute("imagerec").Value.ToRectangle();
                        if (DrawingRec2 == System.Drawing.Rectangle.Empty)
                        {
                            _NewFrame.ImageRect = Microsoft.Xna.Framework.Rectangle.Empty;
                            LoadErrors.Add(_NewActor.ID + "-Animation-" + _NewAnimation.Name + "-Frame-" + _NewFrame.ID.ToString(), "Invalid Source Rect");
                        }
                        else
                        {
                            _NewFrame.ImageRect = new Microsoft.Xna.Framework.Rectangle(DrawingRec2.X, DrawingRec2.Y, DrawingRec2.Width, DrawingRec2.Height);
                        }

                        try
                        {
                            var Anchor = ActorAnimationFrameObject.Attribute("anchor").Value;

                            string[] _Points = Anchor.SplitString(",", StringSplitOptions.RemoveEmptyEntries);
                            if (_Points.Length == 2)
                            {
                                _NewFrame.Anchor = new Vector2(_Points[0].ToInt(0), _Points[1].ToInt(0));
                            }
                            else
                            {
                                _NewFrame.Anchor = new Vector2(0, 0);
                            }
                        }
                        catch
                        {
                            _NewFrame.Anchor = new Vector2(0, 0);
                        }

                        try
                        {
                            _NewFrame.Delay = ActorAnimationFrameObject.Attribute("delay").Value.ToInt(_NewActor.DefaultFrameDelay);
                        }
                        catch
                        {
                            _NewFrame.Delay = _NewActor.DefaultFrameDelay;
                        }

                        _NewAnimation.Frames.Add(_NewFrame.ID, _NewFrame);
                    }

                    if (_NewActor.Animations.ContainsKey(_NewAnimation.Name + _NewAnimation.Direction))
                    {
                        LoadErrors.Add(_NewActor.ID + "-Animation-" + _NewAnimation.Name + "-DuplicateAnimationName", "AnimationName: " + _NewAnimation.Name  + _NewAnimation.Direction + " - Duplicate Animation Name Found");
                    }
                    else
                    {
                        _NewActor.Animations.Add(_NewAnimation.Name + _NewAnimation.Direction, _NewAnimation);
                    }
                }

                AllActors.Add(_NewActor.Name, _NewActor);
            }
        }

        public string ID;
        public string Name;
        public string DisplayName;
        public string Description;
        public string AuthorName;
        public string UpdateDate;

        public string PackageName;
        public string BaseDirectory;

        public virtual int GameID { get; set; }
        public Dictionary<string, ActorInfo> AllActors = new Dictionary<string, ActorInfo>();
        public Dictionary<string, string> LoadErrors = new Dictionary<string, string>();
        public Dictionary<string, CompressibleImage> Images = new Dictionary<string, CompressibleImage>();
    }

    public class ActorInfo
    {
        public string ID;
        public string Name;
        public string DisplayName;
        public string Description;
        public string AuthorName;
        public string UpdateDate;
        public int DefaultFrameDelay;

        public ActorInfo() { }

        public Dictionary<string, ActorAnimation> Animations = new Dictionary<string, ActorAnimation>();
    }

    public class ActorAnimation
    {
        public string Name;
        public string Filename;
        public string Direction;
        public Enumerations.RelativePosition DirectionValue = Enumerations.RelativePosition.Main;
        public Dictionary<int, ActorAnimationFrame> Frames = new Dictionary<int, ActorAnimationFrame>();
        public string ID
        {
            get
            {
                return Name + Direction;
            }
        }
    }

    //[Serializable()]
    //public class ActorData
    //{
    //    /// <summary>
    //    /// Name of the Actor
    //    /// </summary>
    //    public string Name;

    //    /// <summary>
    //    /// Root Directory For This Actor Definition
    //    /// </summary>
    //    public string BaseDirectoryPath;

    //    /// <summary>
    //    /// Globally Unique ID of This Actor
    //    /// </summary>
    //    public string ID;

    //    /// <summary>
    //    /// Locally unique ID of this Actor
    //    /// </summary>
    //    public int UID;

    //    /// <summary>
    //    /// Dictionary of Images
    //    /// </summary>
    //    public ACTDictionary<string, string> Images = new ACTDictionary<string, string>();

    //    /// <summary>
    //    /// List of Animations Available
    //    /// </summary>
    //   // public List<ActorAnimation> Animations = new List<ActorAnimation>();
    //}

    //[Serializable()]
    //public class ActorInfo
    //{
    //    /// <summary>
    //    /// Name of the Animation
    //    /// </summary>
    //    public string Name;

    //    /// <summary>
    //    /// Name of the Image Index
    //    /// </summary>
    //    public int ImageIndex;

    //    /// <summary>
    //    /// Maps To NebulaGames.RPGWorld.RelativePosition
    //    /// </summary>
    //    public int Direction;

    //    /// <summary>
    //    /// Default Animation Anchor
    //    /// </summary>
    //    public Vector2 Anchor = new Vector2();

    //    /// <summary>
    //    /// List of the Animation Data / Frames
    //    /// </summary>
    //    public List<ActorAnimationData> AnimationData = new List<ActorAnimationData>();
    //}

    [Serializable()]
    public class ActorAnimationFrame
    {
        public int ID;
        public List<int> NextID = new List<int>();
        public Vector2 Anchor = new Vector2();
        public Rectangle ImageRect = new Rectangle();
        public int Delay;
        public bool FlipHorizontal = false;
        public bool FlipVertical = false;

    }
}
