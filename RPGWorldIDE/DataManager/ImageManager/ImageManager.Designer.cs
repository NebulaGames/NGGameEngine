namespace NebulaGames.RPGWorld
{
    partial class ImageManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageManager));
            this.MainImageList = new System.Windows.Forms.ImageList(this.components);
            this.ImagesTreeList = new DevExpress.XtraTreeList.TreeList();
            this.TreeListContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Icons = new System.Windows.Forms.ImageList(this.components);
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.FileMenu = new DevExpress.XtraBars.BarSubItem();
            this.SaveToolbarButton = new DevExpress.XtraBars.BarButtonItem();
            this.ExportToolbarButton = new DevExpress.XtraBars.BarButtonItem();
            this.ImagePropertiesButton = new DevExpress.XtraBars.BarButtonItem();
            this.LoadProjectFileMenuButton = new DevExpress.XtraBars.BarButtonItem();
            this.PageRibbon = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.FirstRibbonGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.GenericOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.MainImageListBoxControl = new DevExpress.XtraEditors.ImageListBoxControl();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.LastActionStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.ImagesTreeList)).BeginInit();
            this.TreeListContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainImageListBoxControl)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainImageList
            // 
            this.MainImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.MainImageList.ImageSize = new System.Drawing.Size(75, 75);
            this.MainImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ImagesTreeList
            // 
            this.ImagesTreeList.ContextMenuStrip = this.TreeListContextMenu;
            this.ImagesTreeList.Dock = System.Windows.Forms.DockStyle.Left;
            this.ImagesTreeList.DragNodesMode = DevExpress.XtraTreeList.TreeListDragNodesMode.Standard;
            this.ImagesTreeList.Location = new System.Drawing.Point(0, 140);
            this.ImagesTreeList.Name = "ImagesTreeList";
            this.ImagesTreeList.OptionsBehavior.DragNodes = true;
            this.ImagesTreeList.OptionsBehavior.EnableFiltering = true;
            this.ImagesTreeList.OptionsBehavior.ImmediateEditor = false;
            this.ImagesTreeList.SelectImageList = this.Icons;
            this.ImagesTreeList.Size = new System.Drawing.Size(255, 568);
            this.ImagesTreeList.StateImageList = this.Icons;
            this.ImagesTreeList.TabIndex = 1;
            this.ImagesTreeList.NodeChanged += new DevExpress.XtraTreeList.NodeChangedEventHandler(this.ImagesTreeList_NodeChanged);
            this.ImagesTreeList.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.ImagesTreeList_FocusedNodeChanged);
            this.ImagesTreeList.HiddenEditor += new System.EventHandler(this.ImagesTreeList_HiddenEditor);
            this.ImagesTreeList.TextChanged += new System.EventHandler(this.ImagesTreeList_TextChanged);
            this.ImagesTreeList.Click += new System.EventHandler(this.ImagesTreeList_Click);
            this.ImagesTreeList.DragDrop += new System.Windows.Forms.DragEventHandler(this.ImagesTreeList_DragDrop);
            this.ImagesTreeList.DoubleClick += new System.EventHandler(this.ImagesTreeList_DoubleClick);
            // 
            // TreeListContextMenu
            // 
            this.TreeListContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newImageToolStripMenuItem,
            this.newGroupToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.editToolStripMenuItem});
            this.TreeListContextMenu.Name = "contextMenuStrip1";
            this.TreeListContextMenu.Size = new System.Drawing.Size(135, 114);
            // 
            // newImageToolStripMenuItem
            // 
            this.newImageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fromFileToolStripMenuItem});
            this.newImageToolStripMenuItem.Name = "newImageToolStripMenuItem";
            this.newImageToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.newImageToolStripMenuItem.Text = "New Image";
            // 
            // fromFileToolStripMenuItem
            // 
            this.fromFileToolStripMenuItem.Name = "fromFileToolStripMenuItem";
            this.fromFileToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.fromFileToolStripMenuItem.Text = "From File";
            this.fromFileToolStripMenuItem.Click += new System.EventHandler(this.fromFileToolStripMenuItem_Click);
            // 
            // newGroupToolStripMenuItem
            // 
            this.newGroupToolStripMenuItem.Name = "newGroupToolStripMenuItem";
            this.newGroupToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.newGroupToolStripMenuItem.Text = "New Group";
            this.newGroupToolStripMenuItem.Click += new System.EventHandler(this.newGroupToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // Icons
            // 
            this.Icons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Icons.ImageStream")));
            this.Icons.TransparentColor = System.Drawing.Color.Transparent;
            this.Icons.Images.SetKeyName(0, "Folder.jpg");
            this.Icons.Images.SetKeyName(1, "FolderSelected.jpg");
            this.Icons.Images.SetKeyName(2, "Image.jpg");
            this.Icons.Images.SetKeyName(3, "ImageSelected.jpg");
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.FileMenu,
            this.SaveToolbarButton,
            this.ExportToolbarButton,
            this.ImagePropertiesButton,
            this.LoadProjectFileMenuButton});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 6;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.PageRibbon});
            this.ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2010;
            this.ribbonControl1.Size = new System.Drawing.Size(1007, 140);
            this.ribbonControl1.Toolbar.ItemLinks.Add(this.FileMenu);
            // 
            // FileMenu
            // 
            this.FileMenu.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.FileMenu.Caption = "File";
            this.FileMenu.Id = 1;
            this.FileMenu.Name = "FileMenu";
            // 
            // SaveToolbarButton
            // 
            this.SaveToolbarButton.Caption = "Save";
            this.SaveToolbarButton.Id = 2;
            this.SaveToolbarButton.LargeGlyph = global::NebulaGames.RPGWorld.IDE.Properties.Resource_En.SaveIcon_With_Arrow1;
            this.SaveToolbarButton.Name = "SaveToolbarButton";
            this.SaveToolbarButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.SaveToolbarButton_ItemClick);
            // 
            // ExportToolbarButton
            // 
            this.ExportToolbarButton.Caption = "Export";
            this.ExportToolbarButton.Id = 3;
            this.ExportToolbarButton.LargeGlyph = global::NebulaGames.RPGWorld.IDE.Properties.Resource_En.Export_Icon_Large_To_Small1;
            this.ExportToolbarButton.Name = "ExportToolbarButton";
            // 
            // ImagePropertiesButton
            // 
            this.ImagePropertiesButton.Caption = "Properties";
            this.ImagePropertiesButton.Id = 4;
            this.ImagePropertiesButton.LargeGlyph = global::NebulaGames.RPGWorld.IDE.Properties.Resource_En.Properties_Icon_With_Finder1;
            this.ImagePropertiesButton.Name = "ImagePropertiesButton";
            // 
            // LoadProjectFileMenuButton
            // 
            this.LoadProjectFileMenuButton.Caption = "Load";
            this.LoadProjectFileMenuButton.Id = 5;
            this.LoadProjectFileMenuButton.LargeGlyph = global::NebulaGames.RPGWorld.IDE.Properties.Resource_En.LoadIcon_OpenFolder_Blue1;
            this.LoadProjectFileMenuButton.Name = "LoadProjectFileMenuButton";
            // 
            // PageRibbon
            // 
            this.PageRibbon.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup2,
            this.FirstRibbonGroup,
            this.ribbonPageGroup1});
            this.PageRibbon.Name = "PageRibbon";
            this.PageRibbon.Text = "File Methods";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.LoadProjectFileMenuButton);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            // 
            // FirstRibbonGroup
            // 
            this.FirstRibbonGroup.ItemLinks.Add(this.SaveToolbarButton);
            this.FirstRibbonGroup.ItemLinks.Add(this.ExportToolbarButton);
            this.FirstRibbonGroup.Name = "FirstRibbonGroup";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.ImagePropertiesButton);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            // 
            // GenericOpenFileDialog
            // 
            this.GenericOpenFileDialog.Multiselect = true;
            // 
            // MainImageListBoxControl
            // 
            this.MainImageListBoxControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainImageListBoxControl.ImageList = this.MainImageList;
            this.MainImageListBoxControl.Location = new System.Drawing.Point(255, 140);
            this.MainImageListBoxControl.Name = "MainImageListBoxControl";
            this.MainImageListBoxControl.Size = new System.Drawing.Size(752, 568);
            this.MainImageListBoxControl.TabIndex = 2;
            this.MainImageListBoxControl.SelectedIndexChanged += new System.EventHandler(this.MainImageListBoxControl_SelectedIndexChanged);
            this.MainImageListBoxControl.DoubleClick += new System.EventHandler(this.MainImageListBoxControl_DoubleClick);
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(255, 140);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(12, 568);
            this.splitterControl1.TabIndex = 4;
            this.splitterControl1.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LastActionStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 708);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1007, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // LastActionStatusLabel
            // 
            this.LastActionStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LastActionStatusLabel.Name = "LastActionStatusLabel";
            this.LastActionStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // ImageManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1007, 730);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.MainImageListBoxControl);
            this.Controls.Add(this.ImagesTreeList);
            this.Controls.Add(this.ribbonControl1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "ImageManager";
            this.Text = "Image Manager";
            this.Load += new System.EventHandler(this.ImageManager_Load_1);
            this.Shown += new System.EventHandler(this.ImageManager_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.ImagesTreeList)).EndInit();
            this.TreeListContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MainImageListBoxControl)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList MainImageList;
        private DevExpress.XtraTreeList.TreeList ImagesTreeList;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage PageRibbon;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup FirstRibbonGroup;
        private System.Windows.Forms.ContextMenuStrip TreeListContextMenu;
        private System.Windows.Forms.ToolStripMenuItem newImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGroupToolStripMenuItem;
        private System.Windows.Forms.ImageList Icons;
        private System.Windows.Forms.OpenFileDialog GenericOpenFileDialog;
        private DevExpress.XtraBars.BarSubItem FileMenu;
        private DevExpress.XtraEditors.ImageListBoxControl MainImageListBoxControl;
        private DevExpress.XtraBars.BarButtonItem SaveToolbarButton;
        private DevExpress.XtraBars.BarButtonItem ExportToolbarButton;
        private DevExpress.XtraBars.BarButtonItem ImagePropertiesButton;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem LoadProjectFileMenuButton;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel LastActionStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
    }
}