using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACT.Core.Extensions;
using ACT.Core.Interfaces;

namespace NebulaGames.RPGWorld.Images
{
    public class NG_GroupCollection
    {
        public List<NG_ImageGroup> Groups = new List<NG_ImageGroup>();

        /// <summary>
        /// Has Group Name
        /// </summary>
        /// <param name="NewName">New Group Name</param>
        /// <returns></returns>
        public bool HasGroupName(string NewName)
        {
            if ((from x in Groups where x.GroupName.ToLower() == NewName.ToLower() select x).Count() != 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds a group to the collection
        /// </summary>
        /// <param name="NewGroupName">New Name Of Group</param>
        /// <param name="Tags">Optional Group Tags</param>
        public void AddGroup(string NewGroupName, string[] Tags = null)
        {
            NG_ImageGroup _Group = new NG_ImageGroup();
            _Group.GroupName = NewGroupName;
            _Group.Images = new List<NG_Image>();
            _Group.Tags = new List<string>();
            _Group.UID = Guid.NewGuid();
            _Group.ParentID = "";

            if (Tags != null)
            {
                _Group.Tags.AddRange(Tags);
            }
            Groups.Add(_Group);
        }

        public NG_ImageGroup GetGroupByID(string ID)
        {
            foreach (var g in Groups)
            {
                if (g.UID.ToString() == ID)
                {
                    return g;
                }
            }

            foreach (var g in Groups)
            {
                var _TmpGroup = g.GetGroupByID(ID);
                if (_TmpGroup != null)
                {
                    return _TmpGroup;
                }
            }
            return null;
        }

        public int GetIndexPosition(string ID)
        {
            var _GroupToDelete = GetGroupByID(ID);

            if (_GroupToDelete.ParentID == "")
            {
                return Groups.IndexOf(_GroupToDelete);
            }
            else
            {
                var _Groups = GetGroupByID(_GroupToDelete.ParentID);

                return _Groups.Groups.IndexOf(_GroupToDelete);
            }
        }

        public void DeleteGroupByID(string ID)
        {
            var _GroupToDelete = GetGroupByID(ID);

            if (_GroupToDelete.ParentID == "")
            {
                Groups.RemoveAt(GetIndexPosition(ID));
            }
            else
            {
                var _Groups = GetGroupByID(_GroupToDelete.ParentID);

                _Groups.Groups.RemoveAt(GetIndexPosition(ID));
            }
        }

        public void MoveGroupToGroup(string GroupID, string ToGroupID)
        {
            var _GroupToMove = GetGroupByID(GroupID);

            var _ParentGroup = GetGroupByID(ToGroupID);

            DeleteGroupByID(GroupID);
            if (_ParentGroup == null)
            {
                _GroupToMove.ParentID = "";
                Groups.Add(_GroupToMove);
            }
            else
            {
                _GroupToMove.ParentID = _ParentGroup.UID.ToString();
                _ParentGroup.Groups.Add(_GroupToMove);
            }
            
        }
        
    }

    [Serializable()]
    public class NG_ImageGroup 
    {
        public Guid UID;
        public string GroupName;
        public List<string> Tags = new List<string>();
        public List<NG_Image> Images = new List<NG_Image>();
        public List<NG_ImageGroup> Groups = new List<NG_ImageGroup>();
        public string ParentID;

        public NG_ImageGroup GetGroupByID(string ID)
        {
            NG_ImageGroup _TmpReturn = null;

            foreach(var g in Groups)
            {
                if (g.UID.ToString() == ID)
                {
                    _TmpReturn = g;
                    break;
                }
            }

            if (_TmpReturn == null)
            {
                foreach(var g in Groups)
                {
                    _TmpReturn = g.GetGroupByID(ID);
                    if (_TmpReturn != null)
                    {
                        break;
                    }
                }
            }

            return _TmpReturn;
        }

        public NG_Image GetImageByID(string ID)
        {
            foreach (var i in Images)
            {
                if (i.UID.ToString() == ID) { return i; }
            }
            return null;
        }

        public NG_Image GetImageByIndex(int Index)
        {
            foreach (var i in Images)
            {
                if (i.ImageIndex == Index) { return i; }
            }
            return null;
        }

        public NG_ImageGroup Copy(NG_ImageGroup NewParent = null)
        {
            NG_ImageGroup _NewGroup = new NG_ImageGroup();

            _NewGroup = _NewGroup.Clone();

            if (NewParent == null)
            {
                _NewGroup.ParentID = NewParent.UID.ToString();
                NewParent.Groups.Add(_NewGroup);
            }
            
            return _NewGroup;
        }
    }

    [Serializable()]
    public class NG_Image
    {
        public Guid UID;
        public byte[] ImageData;
        public string ImageName;
        public string Name;
        public List<string> Tags = new List<string>();
        public int ImageIndex;
        public NG_Image_Properties ImageProperties;
    }

    public class NG_Image_Properties
    {
        public bool UsageOption_TileSet;
        public bool UsageOption_Animation;
        public bool UsageOption_Icon;
        public bool UsageOption_Object;
        public bool UsageOption_Actor;
        public bool UsageOption_General;

    }
}