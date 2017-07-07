using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NebulaGames.RPGWorld.Images;
using ACT.Core.Extensions;

namespace NebulaGames.RPGWorld
{
    public partial class ImageManager : Form
    {

        public NG_ImageGroup CopiedGroup = null;
        public NG_Image CopiedImage = null;

        private NG_GroupCollection _GroupCollection = new NG_GroupCollection();
        private string RecentlySelectedNode = "";

        #region Treeview Functions
       
        private void InitTreeView()
        {
            ImagesTreeList.BeginUpdate();
            ImagesTreeList.Columns.Add();
            ImagesTreeList.Columns[0].Caption = "Name";
            ImagesTreeList.Columns[0].VisibleIndex = 0;
            ImagesTreeList.Columns.Add();
            ImagesTreeList.Columns[1].Caption = "ID";
            ImagesTreeList.Columns[1].VisibleIndex = 1;
            ImagesTreeList.Columns.Add();
            ImagesTreeList.Columns[2].Caption = "Tags";
            ImagesTreeList.Columns[2].VisibleIndex = 2;
            ImagesTreeList.Columns.Add();
            ImagesTreeList.Columns[3].Caption = "IsFolder";
            ImagesTreeList.Columns[3].VisibleIndex = 3;

            ImagesTreeList.Columns[1].Visible = false;
            ImagesTreeList.Columns[2].Visible = false;
            ImagesTreeList.Columns[3].Visible = false;
            ImagesTreeList.EndUpdate();
        }

        private void PopulateTreeView(NG_ImageGroup Group, DevExpress.XtraTreeList.Nodes.TreeListNode Parent)
        {
            var _GroupNode = ImagesTreeList.AppendNode(new object[] { Group.GroupName, Group.UID.ToString(), "", "1" }, Parent);
            _GroupNode.ImageIndex = 0;
            _GroupNode.SelectImageIndex = 1;

            foreach (var i in Group.Images)
            {
                var _ImageNode = ImagesTreeList.AppendNode(new object[] { i.Name + " (" + i.ImageName + ")", i.UID.ToString(), "", "0" }, _GroupNode.Id);
                _ImageNode.ImageIndex = 2;
                _ImageNode.SelectImageIndex = 3;
            }

            foreach (var sg in Group.Groups)
            {
                PopulateTreeView(sg, _GroupNode);
            }
        }
        
        private void PopulateTreeView()
        {
            ImagesTreeList.Nodes.Clear();

            ImagesTreeList.BeginUnboundLoad();

            var _RootNode = ImagesTreeList.Nodes.Add(new object[] { "Project Images", "Root", "", "1" });
            _RootNode.SelectImageIndex = 3;

            foreach (var t in _GroupCollection.Groups)
            {
                PopulateTreeView(t, _RootNode);
            }

            ImagesTreeList.EndUnboundLoad();

            if (RecentlySelectedNode != "")
            {
                ImagesTreeList.FocusedNode = ImagesTreeList.FindNodeByFieldValue("ID", RecentlySelectedNode);
            }
            //Reset Node Focus
        }
        
        #endregion

        public ImageManager()
        {
            InitializeComponent();
        }

        public void ImageManager_Load(object sender, EventArgs e)
        {

        }

        private void newGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Guid _GroupID;
            var _NewGroupName = InputBox.Show("Enter Group Name");
            if (_NewGroupName.ReturnCode == System.Windows.Forms.DialogResult.Cancel) { return; }

            if (ImagesTreeList.FocusedNode == null)
            {
                if (_GroupCollection.HasGroupName(_NewGroupName.Text))
                {
                    MessageBox.Show("Group Already Exists");
                    return;
                }
                _GroupCollection.AddGroup(_NewGroupName.Text);
            }
            else
            {
                if (ImagesTreeList.FocusedNode.GetValue("IsFolder").ToString() != "1")
                {
                    _GroupID = new Guid(ImagesTreeList.FocusedNode.ParentNode.GetValue("ID").ToString());
                }
                else
                {
                    if (ImagesTreeList.FocusedNode.GetValue("ID").ToString() == "Root") 
                    {
                        _GroupID = Guid.Empty;
                    }
                    else
                    {
                        _GroupID = new Guid(ImagesTreeList.FocusedNode.GetValue("ID").ToString());
                    }
                }

                if (_GroupID == Guid.Empty)
                {
                    NG_ImageGroup _NewGroup = new NG_ImageGroup();
                    _NewGroup.UID = Guid.NewGuid();
                    _NewGroup.GroupName = _NewGroupName.Text;
                    _NewGroup.ParentID = "";
                    _GroupCollection.Groups.Add(_NewGroup);

                    RecentlySelectedNode = _NewGroup.UID.ToString();
                }
                else
                {
                    var _Group = _GroupCollection.GetGroupByID(_GroupID.ToString());

                    NG_ImageGroup _NewGroup = new NG_ImageGroup();
                    _NewGroup.UID = Guid.NewGuid();
                    _NewGroup.GroupName = _NewGroupName.Text;
                    _NewGroup.ParentID = _Group.UID.ToString();
                    _Group.Groups.Add(_NewGroup);

                    RecentlySelectedNode = _NewGroup.UID.ToString();
                }
            }
            PopulateTreeView();
        }

        /// <summary>
        /// Add File
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ImagesTreeList.FocusedNode == null)
            {
                MessageBox.Show("Please Select A Group To Add the Image To.");
                return;
            }

            if (ImagesTreeList.FocusedNode.GetValue("ID").ToString() == "Root")
            {
                MessageBox.Show("You can only add images to groups not the parent container");
                return;
            }

            if (GenericOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) { return; }

            var _TmpReturn = InputBox.Show("Enter A Unique Image Name (This will be the Key)");
            if (_TmpReturn.ReturnCode == System.Windows.Forms.DialogResult.Cancel) { return; }

            foreach (var f in GenericOpenFileDialog.FileNames)
            {
                try
                {
                    var _File = System.IO.File.ReadAllBytes(f);
                    int _TmpPosition;

                    try
                    {
                        System.Drawing.Image _IMG = System.Drawing.Image.FromStream(new System.IO.MemoryStream(_File));
                        MainImageList.Images.Add(_IMG);
                        _TmpPosition = MainImageList.Images.Count - 1;
                    }
                    catch
                    {
                        MessageBox.Show("Invalid Image");
                        return;
                    }

                    NG_Image _Image = new NG_Image();
                    _Image.ImageData = _File;
                    _Image.ImageName = f.GetFileNameWithoutExtension();
                    _Image.UID = Guid.NewGuid();
                    _Image.Name = _TmpReturn.Text;

                    Guid _GroupID;

                    if (ImagesTreeList.FocusedNode.GetValue("IsFolder").ToString() != "1")
                    {
                        _GroupID = new Guid(ImagesTreeList.FocusedNode.ParentNode.GetValue("ID").ToString());
                    }
                    else
                    {
                        if (ImagesTreeList.FocusedNode.GetValue("ID").ToString() != "Root")
                        {
                            _GroupID = new Guid(ImagesTreeList.FocusedNode.GetValue("ID").ToString());
                        }
                        else { return; }
                    }

                    var _Group = _GroupCollection.GetGroupByID(_GroupID.ToString());

                    if (_Group == null) { MessageBox.Show("Odd Error Locating Group"); return; }

                    _Image.ImageIndex = _TmpPosition;
                    _Group.Images.Add(_Image);

                    PopulateTreeView();
                }
                catch { }
            }
        }

        private void ImageManager_Load_1(object sender, EventArgs e)
        {
            InitTreeView();
        }

        private void ImagesTreeList_NodeChanged(object sender, DevExpress.XtraTreeList.NodeChangedEventArgs e)
        {

        }

        private void ImagesTreeList_TextChanged(object sender, EventArgs e)
        {
           // MessageBox.Show("TEST");
        }

        private void ImagesTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node != null)
            {
           //     RecentlySelectedNode = e.Node.GetValue("ID").ToString();
            }
        }

        private void ImageManager_Shown(object sender, EventArgs e)
        {
            InitTreeView();
        }

        private void ImagesTreeList_Click(object sender, EventArgs e)
        {
            try
            {
                RecentlySelectedNode = ImagesTreeList.FocusedNode.GetValue("ID").ToString();
                if (ImagesTreeList.FocusedNode.GetValue("IsFolder").ToString() == "1")
                {
                    MainImageList.Images.Clear();
                    MainImageListBoxControl.Items.Clear();
                    var _ImageGroup = _GroupCollection.GetGroupByID(ImagesTreeList.FocusedNode.GetValue("ID").ToString());
                    foreach (var i in _ImageGroup.Images)
                    {
                        MainImageList.Images.Add(System.Drawing.Image.FromStream(new System.IO.MemoryStream(i.ImageData)));
                        i.ImageIndex = MainImageList.Images.Count - 1;
                        MainImageListBoxControl.Items.Add(i.Name + " (" + i.ImageName + ")", MainImageList.Images.Count - 1);
                    }
                }
                else
                {
                    RecentlySelectedNode = ImagesTreeList.FocusedNode.ParentNode.GetValue("ID").ToString();
                }
            }
            catch { }
        }

        private void ImagesTreeList_DoubleClick(object sender, EventArgs e)
        {
            try
            {
               
                //RecentlySelectedNode = ImagesTreeList.FocusedNode.GetValue("ID").ToString();
                //if (ImagesTreeList.FocusedNode.GetValue("IsFolder").ToString() == "0")
                //{
                    // Show all Images
                //}

                //MessageBox.Show(RecentlySelectedNode);
            }
            catch { }
        }

        private void MainImageListBoxControl_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }

        private void MainImageListBoxControl_DoubleClick(object sender, EventArgs e)
        {
            DevExpress.XtraEditors.Controls.ImageListBoxItem _Item = (DevExpress.XtraEditors.Controls.ImageListBoxItem)MainImageListBoxControl.SelectedItem;

            var _Group = _GroupCollection.GetGroupByID(RecentlySelectedNode);
            if (_Group == null)
            {
                // try Grabbing Parent
                
            }
            var _SelectedImage = _GroupCollection.GetGroupByID(RecentlySelectedNode).GetImageByIndex(_Item.ImageIndex);

            DataManager.ImageManager.ImageViewer _IV = new DataManager.ImageManager.ImageViewer(_SelectedImage.Name, _SelectedImage.ImageData, new Size(250,250));
            _IV.Show();
        }

        private void SaveToolbarButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // File Format

        }

        private void ImagesTreeList_DragDrop(object sender, DragEventArgs e)
        {
            DevExpress.XtraTreeList.Nodes.TreeListNode DestinationNode ;

            Point pt = ((DevExpress.XtraTreeList.TreeList)sender).PointToClient(new Point(e.X, e.Y));
            //DevExpress.XtraTreeList.Nodes.TreeListNode DestinationNode = ((DevExpress.XtraTreeList.Nodes.TreeListNode)sender).Calc
            var _Info = ImagesTreeList.CalcHitInfo(pt);
            gto:
            DestinationNode = _Info.Node;
            if (DestinationNode == null) { goto gto; }
          
            DevExpress.XtraTreeList.Nodes.TreeListNode _Node = (DevExpress.XtraTreeList.Nodes.TreeListNode)e.Data.GetData("DevExpress.XtraTreeList.Nodes.TreeListNode");

            StartGetNodeID:
            
            var _DestinationID = DestinationNode.GetValue("ID").ToString();            
            var _NodeID = _Node.GetValue("ID").ToString();

            if (DestinationNode.GetValue("IsFolder").ToString().ToLower() == "1")
            {
                var _Group = _GroupCollection.GetGroupByID(_DestinationID);

                if (_Node.GetValue("IsFolder").ToString().ToLower() == "1")
                {
                    _GroupCollection.MoveGroupToGroup(_Node.GetValue("ID").ToString(), _DestinationID);
                }
            }
            else
            {
                DestinationNode = DestinationNode.ParentNode;
                goto StartGetNodeID;
            }
            e.Effect = DragDropEffects.None;
            PopulateTreeView();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopiedImage = null;
            CopiedGroup = null;

            if (ImagesTreeList.FocusedNode.GetValue("ID").ToString() == "Root")
            {
                MessageBox.Show("You can only copy sub groups or images not the root group");
                LastActionStatusLabel.Text = "Copy Failed";
                return;
            }
            Guid _GroupID;

            if (ImagesTreeList.FocusedNode.GetValue("IsFolder").ToString() != "1")
            {
                _GroupID = new Guid(ImagesTreeList.FocusedNode.ParentNode.GetValue("ID").ToString());
                var _GroupToCopy = _GroupCollection.GetGroupByID(_GroupID.ToString());

                CopiedGroup = _GroupToCopy.Copy();
            }
            else
            {
                if (ImagesTreeList.FocusedNode.GetValue("ID").ToString() != "Root")
                {
                    _GroupID = new Guid(ImagesTreeList.FocusedNode.GetValue("ID").ToString());
                }
                else { return; }
            }
        }

        private void ImagesTreeList_HiddenEditor(object sender, EventArgs e)
        {
           // ImagesTreeList.OptionsBehavior.Editable = false;
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ImagesTreeList.FocusedNode != null)
            {
                
            }
        }

        
    }
}
