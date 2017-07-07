namespace NebulaGames.RPGWorld.DataManager.ActorManager
{
    partial class ActorManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActorManager));
            this.LastActionStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainImageListBoxControl = new DevExpress.XtraEditors.ImageListBoxControl();
            this.MainImageList = new System.Windows.Forms.ImageList(this.components);
            this.GenericOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ImagePropertiesButton = new DevExpress.XtraBars.BarButtonItem();
            this.AddImageButton = new DevExpress.XtraBars.BarButtonItem();
            this.FirstRibbonGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.SaveToolbarButton = new DevExpress.XtraBars.BarButtonItem();
            this.ExportToolbarButton = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.LoadProjectFileMenuButton = new DevExpress.XtraBars.BarButtonItem();
            this.PageRibbon = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.FileMenu = new DevExpress.XtraBars.BarSubItem();
            this.Icons = new System.Windows.Forms.ImageList(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newActorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TreeListContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ActorsTreeList = new DevExpress.XtraTreeList.TreeList();
            this.ActorTreeID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.ActorName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.GenericFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.MainImageListBoxControl)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            this.TreeListContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ActorsTreeList)).BeginInit();
            this.SuspendLayout();
            // 
            // LastActionStatusLabel
            // 
            this.LastActionStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LastActionStatusLabel.Name = "LastActionStatusLabel";
            this.LastActionStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // MainImageListBoxControl
            // 
            this.MainImageListBoxControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainImageListBoxControl.ImageList = this.MainImageList;
            this.MainImageListBoxControl.Location = new System.Drawing.Point(239, 141);
            this.MainImageListBoxControl.Name = "MainImageListBoxControl";
            this.MainImageListBoxControl.Size = new System.Drawing.Size(777, 498);
            this.MainImageListBoxControl.TabIndex = 9;
            // 
            // MainImageList
            // 
            this.MainImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.MainImageList.ImageSize = new System.Drawing.Size(75, 75);
            this.MainImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // GenericOpenFileDialog
            // 
            this.GenericOpenFileDialog.Multiselect = true;
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.ImagePropertiesButton);
            this.ribbonPageGroup1.ItemLinks.Add(this.AddImageButton);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            // 
            // ImagePropertiesButton
            // 
            this.ImagePropertiesButton.Caption = "Properties";
            this.ImagePropertiesButton.Id = 4;
            this.ImagePropertiesButton.ImageOptions.LargeImage = global::NebulaGames.RPGWorld.IDE.Properties.Resource_En.Properties_Icon_With_Finder1;
            this.ImagePropertiesButton.Name = "ImagePropertiesButton";
            // 
            // AddImageButton
            // 
            this.AddImageButton.Caption = "Add Image";
            this.AddImageButton.Id = 6;
            this.AddImageButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("AddImageButton.ImageOptions.Image")));
            this.AddImageButton.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("AddImageButton.ImageOptions.LargeImage")));
            this.AddImageButton.Name = "AddImageButton";
            this.AddImageButton.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.AddImageButton_ItemClick);
            // 
            // FirstRibbonGroup
            // 
            this.FirstRibbonGroup.ItemLinks.Add(this.SaveToolbarButton);
            this.FirstRibbonGroup.ItemLinks.Add(this.ExportToolbarButton);
            this.FirstRibbonGroup.Name = "FirstRibbonGroup";
            // 
            // SaveToolbarButton
            // 
            this.SaveToolbarButton.Caption = "Save";
            this.SaveToolbarButton.Id = 2;
            this.SaveToolbarButton.ImageOptions.LargeImage = global::NebulaGames.RPGWorld.IDE.Properties.Resource_En.SaveIcon_With_Arrow1;
            this.SaveToolbarButton.Name = "SaveToolbarButton";
            // 
            // ExportToolbarButton
            // 
            this.ExportToolbarButton.Caption = "Export";
            this.ExportToolbarButton.Id = 3;
            this.ExportToolbarButton.ImageOptions.LargeImage = global::NebulaGames.RPGWorld.IDE.Properties.Resource_En.Export_Icon_Large_To_Small1;
            this.ExportToolbarButton.Name = "ExportToolbarButton";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.LoadProjectFileMenuButton);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            // 
            // LoadProjectFileMenuButton
            // 
            this.LoadProjectFileMenuButton.Caption = "Load";
            this.LoadProjectFileMenuButton.Id = 5;
            this.LoadProjectFileMenuButton.ImageOptions.LargeImage = global::NebulaGames.RPGWorld.IDE.Properties.Resource_En.LoadIcon_OpenFolder_Blue1;
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
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LastActionStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 639);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1016, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
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
            this.LoadProjectFileMenuButton,
            this.AddImageButton});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 7;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.PageRibbon});
            this.ribbonControl1.QuickToolbarItemLinks.Add(this.FileMenu);
            this.ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2010;
            this.ribbonControl1.Size = new System.Drawing.Size(1016, 141);
            this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Above;
            // 
            // FileMenu
            // 
            this.FileMenu.Caption = "File";
            this.FileMenu.Id = 1;
            this.FileMenu.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.FileMenu.Name = "FileMenu";
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
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // fromFileToolStripMenuItem
            // 
            this.fromFileToolStripMenuItem.Name = "fromFileToolStripMenuItem";
            this.fromFileToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.fromFileToolStripMenuItem.Text = "From ZIP File";
            // 
            // newActorToolStripMenuItem
            // 
            this.newActorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fromFileToolStripMenuItem,
            this.fromFolderToolStripMenuItem});
            this.newActorToolStripMenuItem.Name = "newActorToolStripMenuItem";
            this.newActorToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.newActorToolStripMenuItem.Text = "New Actor";
            this.newActorToolStripMenuItem.Click += new System.EventHandler(this.newActorToolStripMenuItem_Click);
            // 
            // fromFolderToolStripMenuItem
            // 
            this.fromFolderToolStripMenuItem.Name = "fromFolderToolStripMenuItem";
            this.fromFolderToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.fromFolderToolStripMenuItem.Text = "From Folder";
            this.fromFolderToolStripMenuItem.Click += new System.EventHandler(this.fromFolderToolStripMenuItem_Click);
            // 
            // TreeListContextMenu
            // 
            this.TreeListContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newActorToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.editToolStripMenuItem});
            this.TreeListContextMenu.Name = "contextMenuStrip1";
            this.TreeListContextMenu.Size = new System.Drawing.Size(131, 92);
            // 
            // ActorsTreeList
            // 
            this.ActorsTreeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.ActorTreeID,
            this.ActorName});
            this.ActorsTreeList.ContextMenuStrip = this.TreeListContextMenu;
            this.ActorsTreeList.Dock = System.Windows.Forms.DockStyle.Left;
            this.ActorsTreeList.Location = new System.Drawing.Point(0, 141);
            this.ActorsTreeList.Name = "ActorsTreeList";
            this.ActorsTreeList.OptionsBehavior.EnableFiltering = true;
            this.ActorsTreeList.OptionsBehavior.ImmediateEditor = false;
            this.ActorsTreeList.OptionsDragAndDrop.DragNodesMode = DevExpress.XtraTreeList.DragNodesMode.Single;
            this.ActorsTreeList.OptionsDragAndDrop.DropNodesMode = DevExpress.XtraTreeList.DropNodesMode.Standard;
            this.ActorsTreeList.SelectImageList = this.Icons;
            this.ActorsTreeList.Size = new System.Drawing.Size(239, 498);
            this.ActorsTreeList.StateImageList = this.Icons;
            this.ActorsTreeList.TabIndex = 8;
            // 
            // ActorTreeID
            // 
            this.ActorTreeID.Caption = "ID";
            this.ActorTreeID.FieldName = "ID";
            this.ActorTreeID.Name = "ActorTreeID";
            this.ActorTreeID.OptionsColumn.AllowEdit = false;
            this.ActorTreeID.Width = 108;
            // 
            // ActorName
            // 
            this.ActorName.Caption = "Actor Name";
            this.ActorName.FieldName = "ActorName";
            this.ActorName.MinWidth = 52;
            this.ActorName.Name = "ActorName";
            this.ActorName.Visible = true;
            this.ActorName.VisibleIndex = 0;
            // 
            // ActorManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 661);
            this.Controls.Add(this.MainImageListBoxControl);
            this.Controls.Add(this.ActorsTreeList);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "ActorManager";
            this.Text = "Actor Manager";
            ((System.ComponentModel.ISupportInitialize)(this.MainImageListBoxControl)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            this.TreeListContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ActorsTreeList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripStatusLabel LastActionStatusLabel;
        private DevExpress.XtraEditors.ImageListBoxControl MainImageListBoxControl;
        private System.Windows.Forms.ImageList MainImageList;
        private System.Windows.Forms.OpenFileDialog GenericOpenFileDialog;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem ImagePropertiesButton;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup FirstRibbonGroup;
        private DevExpress.XtraBars.BarButtonItem SaveToolbarButton;
        private DevExpress.XtraBars.BarButtonItem ExportToolbarButton;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.BarButtonItem LoadProjectFileMenuButton;
        private DevExpress.XtraBars.Ribbon.RibbonPage PageRibbon;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.BarSubItem FileMenu;
        private System.Windows.Forms.ImageList Icons;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newActorToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip TreeListContextMenu;
        private DevExpress.XtraTreeList.TreeList ActorsTreeList;
        private System.Windows.Forms.ToolStripMenuItem fromFolderToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog GenericFolderBrowserDialog;
        private DevExpress.XtraTreeList.Columns.TreeListColumn ActorTreeID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn ActorName;
        private DevExpress.XtraBars.BarButtonItem AddImageButton;
    }
}