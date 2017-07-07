
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ACT.Core.Extensions;
using System.Configuration;
using GameGraphics = NebulaGames.RPGWorld.Graphics;
using GameBuilder = NebulaGames.RPGWorld.GameBuilder;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

namespace NebulaGames.RPGWorld
{
    public partial class MainIDE : Form
    {

        #region Private Helper Methods



        /// <summary>
        /// Load / Save The IDE Layout
        /// </summary>
        /// <param name="Load">Bool</param>
        private void LoadSaveLayout(bool Load)
        {
            if (Load)
            {
                try
                {
                    if (System.IO.Directory.Exists(_BaseLayoutDirectory.EnsureDirectoryFormat() + Environment.UserName))
                    {
                        if (System.IO.File.Exists(_BaseLayoutDirectory.EnsureDirectoryFormat() + Environment.UserName + "\\MainIDE.xml"))
                        {
                            MainIDEDocManager.RestoreLayoutFromXml(_BaseLayoutDirectory.EnsureDirectoryFormat() + Environment.UserName + "\\MainIDE.xml");
                        }
                    }
                }
                catch (Exception ex)
                {
                    ACT.Core.Helper.ErrorLogger.LogError("NebulaGames.RPGWorld.MainIDE.LoadSaveLayout", ex.Message, "Error Loading Layout", ex, ACT.Core.Enums.ErrorLevel.Warning);
                }
            }
            else
            {
                try
                {
                    if (!System.IO.Directory.Exists(_BaseLayoutDirectory.EnsureDirectoryFormat() + Environment.UserName)) { System.IO.Directory.CreateDirectory(_BaseLayoutDirectory.EnsureDirectoryFormat() + Environment.UserName); }

                    string _FullURL = _BaseLayoutDirectory.EnsureDirectoryFormat() + Environment.UserName + "\\MainIDE.xml";

                    MainIDEDocManager.SaveLayoutToXml(_FullURL);
                }
                catch (Exception ex)
                {
                    ACT.Core.Helper.ErrorLogger.LogError("NebulaGames.RPGWorld.MainIDE.LoadSaveLayout", ex.Message, "Error Saving Layout", ex, ACT.Core.Enums.ErrorLevel.Warning);
                }
            }
        }

        private void SaveWindowPosition()
        {
            Properties.Settings.Default.MyState = this.WindowState;

            if (this.WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.MySize = this.Size;
                Properties.Settings.Default.MyLoc = this.Location;
            }
            else
            {
                Properties.Settings.Default.MySize = this.RestoreBounds.Size;
                Properties.Settings.Default.MyLoc = this.RestoreBounds.Location;
            }

            Properties.Settings.Default.Save();
        }

        private void LoadWindowPosition()
        {
            this.Size = Properties.Settings.Default.MySize;
            this.Location = Properties.Settings.Default.MyLoc;
            this.WindowState = Properties.Settings.Default.MyState;
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadSystemTextures()
        {
            _TextureDIR = _TextureDIR.TrimStart(".\\");
            _TextureDIR = System.AppDomain.CurrentDomain.BaseDirectory.EnsureDirectoryFormat() + _TextureDIR;
            _TextureDIR = _TextureDIR.EnsureDirectoryFormat();

            var _Results = GameGraphics.ResourceManager.LoadSystemTextures(_TextureDIR);


            var _Packages = GameGraphics.ResourceManager.GetTexturePackages();

            foreach (string PackageName in _Packages)
            {
                var _ParentNode = PackageObjectsTreeList.AppendNode(new object[] { PackageName, "", PackageName, false, false, "" }, null);

                var _Textures = GameGraphics.ResourceManager.GetTexturesByPackageName(PackageName);
                foreach (var Texture in _Textures)
                {
                    PackageObjectsTreeList.AppendNode(new object[] { Texture.Name, Texture.FileName, PackageName, true, false, Texture.ID }, _ParentNode);
                }
            }
        }

        #endregion

        #region Local Variables
        Timer _WorldPainterTimer = new Timer();
        Timer _HoverTimer = new Timer();

        private Point _MainScreenOffset = new Point(0, 0);
        private Point _CurrentPosition = new Point(0, 0);
        private Point _CurrentMousePosition = new Point(0, 0);
        private Enumerations.CurrentEditorMode _CurrentEditorMode = Enumerations.CurrentEditorMode.Paint;
        private NebulaGames.RPGWorld.Enumerations.BrushShapes _CurrentBrushShape = NebulaGames.RPGWorld.Enumerations.BrushShapes.Circle;
        private GameBuilder.LayerInfo _CurrentLayerInfo;
        private System.Drawing.TextureBrush _TextureBrush;
        private System.Drawing.SolidBrush _SolidBrush;
        private System.Drawing.TextureBrush _TransparentBrush;

        private string PackageObjectSearchText, PackageTextureSearchText, PackageActorSearchText;
        private bool _StopDrawing = false;
        // Draw the Mini Map At Half Speed.  This is a Toggle Field
        private bool _MiniMapHalfSpeedDraw = false;

        private System.Drawing.Graphics _MiniMapGraphicsObject, _MainGraphicsObject;
        private System.Drawing.Graphics _ObjectPreviewGraphics;

        private string _BaseLayoutDirectory = Constants.LayoutDirectory;
        private string _TextureDIR = Constants.ImageDirectory;

        private List<NebulaGames.RPGWorld.Assets.PackageInfo> Packages = new List<NebulaGames.RPGWorld.Assets.PackageInfo>();
        private NebulaGames.RPGWorld.GameBuilder.EditorWorld _CurrentWorld;

        #region CHILD WINDOWS

        DataManager.ImageManager.ImageViewer PackageObjectsPreviewWindow = new DataManager.ImageManager.ImageViewer();
        // NebulaGames.RPGWorld.GameBuilder.PackageDataManager _PackageDataManager = new NebulaGames.RPGWorld.GameBuilder.PackageDataManager();
        DataManager.ActorManager.ActorManager _ActorManagerWindow = new DataManager.ActorManager.ActorManager();
        ProjectSettings _ProjectSettingsWindow = new ProjectSettings();

        Dialogs.ProgressBarWindow _ProgressBarWindow = new Dialogs.ProgressBarWindow();

        #endregion

        #endregion

        #region Constructors
        public MainIDE()
        {
            _BaseLayoutDirectory = _BaseLayoutDirectory.EnsureDirectoryFormat();
            InitializeComponent();
            LoadWindowPosition();
            LoadSaveLayout(true);
            //LoadSystemTextures();
            GameGraphics.ResourceManager.LoadSystemImages(AppDomain.CurrentDomain.BaseDirectory + "Resources\\");
            ToolSizeEditor.EditValue = "20";
            MapXPositionEditor.EditValue = "0";
            MapYPositionEditor.EditValue = "0";
            _TransparentBrush = new TextureBrush(GameGraphics.ResourceManager.GetSystemImage("TransparentSquare"));
            #region Set Default Variables

            OpenProjectDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory.EnsureDirectoryFormat() + "Data\\Projects\\";
            _WorldPainterTimer.Tick += _WorldPainterTimer_Tick;
            _WorldPainterTimer.Interval = 30;
            _WorldPainterTimer.Enabled = true;

            PassableColorEditor.EditValue = Color.FromArgb(65330);


            #endregion
        }



        #endregion

        #region Form Methods
        private void MainIDE_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save The Current Layout
            LoadSaveLayout(false);
            SaveWindowPosition();
        }
        private void MainIDE_Shown(object sender, EventArgs e)
        {
            PerformRedraw();
        }
        #endregion

        #region View Windows Toggle Functionality

        private void ShowMiniMapToggle_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ShowMiniMapToggle.Checked)
            {
                MiniMapPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
            }
            else
            {
                MiniMapPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            }
        }

        private void ShowPackageObjectsToggle_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ShowPackageObjectsToggle.Checked)
            {
                PackageExplorerPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
            }
            else
            {
                PackageExplorerPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            }
        }

        private void ShowLayersToggle_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ShowLayersToggle.Checked)
            {
                LayersPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
            }
            else
            {
                LayersPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            }
        }

        private void ShowItemPropertiesToggle_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ShowItemPropertiesToggle.Checked)
            {
                PropertiesPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
            }
            else
            {
                PropertiesPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            }
        }

        private void PackageExplorerPanel_VisibilityChanged(object sender, DevExpress.XtraBars.Docking.VisibilityChangedEventArgs e)
        {
            if (PackageExplorerPanel.Visibility == DevExpress.XtraBars.Docking.DockVisibility.Hidden)
            {
                if (ShowPackageObjectsToggle.Checked)
                {
                    ShowPackageObjectsToggle.Checked = false;
                }
            }
            else
            {
                if (!ShowPackageObjectsToggle.Checked)
                {
                    ShowPackageObjectsToggle.Checked = true;
                }
            }
        }

        private void MiniMapPanel_VisibilityChanged(object sender, DevExpress.XtraBars.Docking.VisibilityChangedEventArgs e)
        {
            if (MiniMapPanel.Visibility == DevExpress.XtraBars.Docking.DockVisibility.Hidden)
            {
                if (ShowMiniMapToggle.Checked)
                {
                    ShowMiniMapToggle.Checked = false;
                }
            }
            else
            {
                if (!ShowMiniMapToggle.Checked)
                {
                    ShowMiniMapToggle.Checked = true;
                }
            }
        }

        private void LayersPanel_VisibilityChanged(object sender, DevExpress.XtraBars.Docking.VisibilityChangedEventArgs e)
        {
            if (LayersPanel.Visibility == DevExpress.XtraBars.Docking.DockVisibility.Hidden)
            {
                if (ShowLayersToggle.Checked)
                {
                    ShowLayersToggle.Checked = false;
                }
            }
            else
            {
                if (!ShowLayersToggle.Checked)
                {
                    ShowLayersToggle.Checked = true;
                }
            }
        }

        private void PropertiesPanel_VisibilityChanged(object sender, DevExpress.XtraBars.Docking.VisibilityChangedEventArgs e)
        {
            if (PropertiesPanel.Visibility == DevExpress.XtraBars.Docking.DockVisibility.Hidden)
            {
                if (ShowItemPropertiesToggle.Checked)
                {
                    ShowItemPropertiesToggle.Checked = false;
                }
            }
            else
            {
                if (!ShowItemPropertiesToggle.Checked)
                {
                    ShowItemPropertiesToggle.Checked = true;
                }
            }
        }

        #endregion

        void PerformRedraw()
        {
            DrawEditorMenuButtons();
            UpdateStatusBar();
        }

        void ChangedLocation()
        {

        }

        /// <summary>
        /// Draw The Main Screen
        /// </summary>
        void DrawMainScreen(bool OnlyOutside = false, bool InsideOnly = false)
        {
            if (_CurrentWorld == null) { return; }
            //Draw The Outside when the MouseOver Effect Is In Place
            if (OnlyOutside)
            {
                _MainGraphicsObject.DrawImage(_CurrentWorld.DrawLocation(_CurrentPosition.X, _CurrentPosition.Y), new Point(0, 0));
            }
            else
            {
                if (InsideOnly == false)
                {
                    // _CurrentBufferGraphicsObject.DrawImage(_CurrentWorld.DrawLocation_SingleScreen(_CurrentPosition.X, _CurrentPosition.Y, true), new Point(0, 0));
                    _MainGraphicsObject.DrawImage(_CurrentWorld.DrawLocation(_CurrentPosition.X, _CurrentPosition.Y), new Point(0, 0));
                }
            }

            // _MainGraphicsObject.DrawImage(_BufferImage, _MainScreenOffset);
        }

        void _WorldPainterTimer_Tick(object sender, EventArgs e)
        {
            if (_StopDrawing) { return; }

            if (_MiniMapGraphicsObject == null || _MainGraphicsObject == null)
            {
                int _TmpX = (int)(MainDrawingPanel.Width / 2) - 400;
                int _TmpY = (int)(MainDrawingPanel.Height / 2) - 300;

                _MainScreenOffset = new Point(_TmpX, _TmpY);
                _MiniMapGraphicsObject = MiniMapDrawPanel.CreateGraphics();
                _MainGraphicsObject = MainDrawingPanel.CreateGraphics();
            }

            if (_ObjectPreviewGraphics == null)
            {
                _ObjectPreviewGraphics = ObjectPreviewPanel.CreateGraphics();
            }

            if (_MiniMapGraphicsObject == null || _MainGraphicsObject == null || _ObjectPreviewGraphics == null) { throw new Exception("Error Creating Graphical Interface"); }

            if (_CurrentWorld != null)
            {
                if (_MiniMapHalfSpeedDraw)
                {
                    _MiniMapGraphicsObject.DrawImage(_CurrentWorld.DrawMiniMap(NebulaGames.RPGWorld.Enumerations.LayerType.Background | NebulaGames.RPGWorld.Enumerations.LayerType.Objects, _CurrentPosition.X, _CurrentPosition.Y), new Rectangle(0, 0, MiniMapDrawPanel.Width, MiniMapDrawPanel.Height));
                    _MiniMapHalfSpeedDraw = false;
                }
                else { _MiniMapHalfSpeedDraw = true; }

                DrawMainScreen(false, true);
            }
        }

        void UpdateStatusBar()
        {
            StatusBarCurrentEditorMode.Caption = "Editor Mode: " + _CurrentEditorMode.TryToString();
        }

        void SetCursorStyle()
        {
            if (_CurrentEditorMode == Enumerations.CurrentEditorMode.Paint)
            {
                MainDrawingPanel.Cursor = Cursors.Cross;
            }
        }

        void DrawEditorMenuButtons()
        {
            if (_CurrentEditorMode == Enumerations.CurrentEditorMode.Paint)
            {
                FillModeButton.ItemAppearance.Normal.Options.UseBackColor = false;
                ObjectModeButton.ItemAppearance.Normal.Options.UseBackColor = false;
                NonPassableModeButton.ItemAppearance.Normal.Options.UseBackColor = false;
                FillModeButton.ItemAppearance.Normal.ForeColor = Color.White;
                ObjectModeButton.ItemAppearance.Normal.ForeColor = Color.White;
                NonPassableModeButton.ItemAppearance.Normal.ForeColor = Color.White;

                if (PaintModeButton.ItemAppearance.Normal.Options.UseBackColor == true) { return; }

                PaintModeButton.ItemAppearance.Normal.Options.UseBackColor = true;
                PaintModeButton.ItemAppearance.Normal.BackColor = Color.White;
                PaintModeButton.ItemAppearance.Normal.ForeColor = Color.Black;

            }
            else if (_CurrentEditorMode == Enumerations.CurrentEditorMode.Fill)
            {
                PaintModeButton.ItemAppearance.Normal.Options.UseBackColor = false;
                ObjectModeButton.ItemAppearance.Normal.Options.UseBackColor = false;
                NonPassableModeButton.ItemAppearance.Normal.Options.UseBackColor = false;
                PaintModeButton.ItemAppearance.Normal.ForeColor = Color.White;
                ObjectModeButton.ItemAppearance.Normal.ForeColor = Color.White;
                NonPassableModeButton.ItemAppearance.Normal.ForeColor = Color.White;

                if (FillModeButton.ItemAppearance.Normal.Options.UseBackColor == true) { return; }

                FillModeButton.ItemAppearance.Normal.Options.UseBackColor = true;
                FillModeButton.ItemAppearance.Normal.BackColor = Color.White;
                FillModeButton.ItemAppearance.Normal.ForeColor = Color.Black;
            }
            else if (_CurrentEditorMode == Enumerations.CurrentEditorMode.PlaceObject)
            {
                PaintModeButton.ItemAppearance.Normal.Options.UseBackColor = false;
                FillModeButton.ItemAppearance.Normal.Options.UseBackColor = false;
                NonPassableModeButton.ItemAppearance.Normal.Options.UseBackColor = false;
                PaintModeButton.ItemAppearance.Normal.ForeColor = Color.White;
                FillModeButton.ItemAppearance.Normal.ForeColor = Color.White;
                NonPassableModeButton.ItemAppearance.Normal.ForeColor = Color.White;

                if (ObjectModeButton.ItemAppearance.Normal.Options.UseBackColor == true) { return; }

                ObjectModeButton.ItemAppearance.Normal.Options.UseBackColor = true;
                ObjectModeButton.ItemAppearance.Normal.BackColor = Color.White;
                ObjectModeButton.ItemAppearance.Normal.ForeColor = Color.Black;
            }
            else if (_CurrentEditorMode == Enumerations.CurrentEditorMode.NonPassable)
            {
                PaintModeButton.ItemAppearance.Normal.Options.UseBackColor = false;
                FillModeButton.ItemAppearance.Normal.Options.UseBackColor = false;
                ObjectModeButton.ItemAppearance.Normal.Options.UseBackColor = false;
                PaintModeButton.ItemAppearance.Normal.ForeColor = Color.White;
                FillModeButton.ItemAppearance.Normal.ForeColor = Color.White;
                ObjectModeButton.ItemAppearance.Normal.ForeColor = Color.White;

                if (NonPassableModeButton.ItemAppearance.Normal.Options.UseBackColor == true) { return; }

                NonPassableModeButton.ItemAppearance.Normal.Options.UseBackColor = true;
                NonPassableModeButton.ItemAppearance.Normal.BackColor = Color.White;
                NonPassableModeButton.ItemAppearance.Normal.ForeColor = Color.Black;
            }

            if (_CurrentBrushShape == NebulaGames.RPGWorld.Enumerations.BrushShapes.Circle)
            {
                ToolBrushShapeCircle.ItemAppearance.Normal.Options.UseBackColor = false;
                ToolBrushShapeCircle.ItemAppearance.Normal.ForeColor = Color.White;

                if (ToolBrushShapeSquare.ItemAppearance.Normal.Options.UseBackColor == true) { return; }

                ToolBrushShapeSquare.ItemAppearance.Normal.Options.UseBackColor = true;
                ToolBrushShapeSquare.ItemAppearance.Normal.BackColor = Color.White;
                ToolBrushShapeSquare.ItemAppearance.Normal.ForeColor = Color.Black;
            }
            else if (_CurrentBrushShape == NebulaGames.RPGWorld.Enumerations.BrushShapes.Square)
            {
                ToolBrushShapeSquare.ItemAppearance.Normal.Options.UseBackColor = false;
                ToolBrushShapeSquare.ItemAppearance.Normal.ForeColor = Color.White;

                if (ToolBrushShapeCircle.ItemAppearance.Normal.Options.UseBackColor == true) { return; }

                ToolBrushShapeCircle.ItemAppearance.Normal.Options.UseBackColor = true;
                ToolBrushShapeCircle.ItemAppearance.Normal.BackColor = Color.White;
                ToolBrushShapeCircle.ItemAppearance.Normal.ForeColor = Color.Black;
            }
        }

        private void PaintModeButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_CurrentEditorMode == Enumerations.CurrentEditorMode.Paint) { return; }

            _CurrentEditorMode = Enumerations.CurrentEditorMode.Paint;
            PerformRedraw();
        }

        private void FillModeButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_CurrentEditorMode == Enumerations.CurrentEditorMode.Fill) { return; }

            _CurrentEditorMode = Enumerations.CurrentEditorMode.Fill;
            PerformRedraw();
        }

        private void ObjectModeButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_CurrentEditorMode == Enumerations.CurrentEditorMode.PlaceObject) { return; }

            _CurrentEditorMode = Enumerations.CurrentEditorMode.PlaceObject;
            PerformRedraw();
        }

        private void NonPassableModeButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_CurrentEditorMode == Enumerations.CurrentEditorMode.NonPassable) { return; }

            _CurrentEditorMode = Enumerations.CurrentEditorMode.NonPassable;
            PerformRedraw();
        }

        private void OpenProjectButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var _Result = OpenProjectDialog.ShowDialog();

            if (_Result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            string _FullFilePath = OpenProjectDialog.FileName;

            _CurrentWorld = new NebulaGames.RPGWorld.GameBuilder.EditorWorld(_FullFilePath, MainDrawingPanel.Width, MainDrawingPanel.Height, new Point(0, 0));
            _CurrentPosition = new Point(0, 0);

            PopulateLayers();

            var _LayerInfo = (GameBuilder.LayerInfo)LayersListBoxControl.SelectedItem;
            _CurrentLayerInfo = _LayerInfo;
            _CurrentWorld.SetWorkingLayer((LayersListBoxControl.SelectedItem as GameBuilder.LayerInfo).Name, _CurrentPosition.X, _CurrentPosition.Y);
            //

            //   _CurrentBackgroundGraphicsObject = Graphics.FromImage(_CurrentWorld.GetWorkingImage(NebulaGames.RPGWorld.Enumerations.EditorMode.Background, _CurrentPosition.X, _CurrentPosition.Y));
            //    _CurrentPassableGraphicsObject = Graphics.FromImage(_CurrentWorld.GetWorkingImage(NebulaGames.RPGWorld.Enumerations.EditorMode.Passable, _CurrentPosition.X, _CurrentPosition.Y));

        }

        private void PopulateLayers()
        {
            int _Index = LayersListBoxControl.SelectedIndex;

            LayersListBoxControl.Items.Clear();

            foreach (string x in _CurrentWorld.Layers.Keys)
            {
                LayersListBoxControl.Items.Add(_CurrentWorld.Layers[x]);
            }

            if (_Index != -1)
            {
                LayersListBoxControl.SelectedIndex = _Index;
            }
            else
            {
                LayersListBoxControl.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Arrow Draw Position Specifies What is currently Drawn on the screen. 
        /// 0=None, 1=Left, 2=Top, 3=Right, 4=Down
        /// </summary>
        byte ArrowDrawPosition = 0;
        int _LastMoveX, _LastMoveY;

        private void MainDrawingPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_CurrentWorld == null) { return; }
            _HoverTimer.Enabled = false;
            //   if (LayersListBoxControl.SelectedIndex == -1) { return; }
            //   if (_CurrentBufferGraphicsObject == null) { return; }



            #region Location Adjustment - Sets the Images etc for moving the Current Position By Clicking The Sides Of The Interface
            if (e.Location.X < 26)
            {
                if (ArrowDrawPosition != 1)
                {
                    DrawMainScreen(true);
                    MainDrawingPanel.Cursor = Cursors.Hand;
                    _MainGraphicsObject.FillRectangle(Brushes.Black, new Rectangle(0, 0, 25, MainDrawingPanel.Height));
                    _MainGraphicsObject.DrawImage(GameGraphics.ResourceManager.GetSystemImage("ArrowLeft"), new Point(1, MainDrawingPanel.Height / 2 - 10));
                    ArrowDrawPosition = 1;
                }
            }
            else if (e.Location.Y < 26)
            {
                if (ArrowDrawPosition != 2)
                {
                    DrawMainScreen(true);
                    MainDrawingPanel.Cursor = Cursors.Hand;
                    _MainGraphicsObject.FillRectangle(Brushes.Black, new Rectangle(0, 0, MainDrawingPanel.Width, 25));
                    _MainGraphicsObject.DrawImage(GameGraphics.ResourceManager.GetSystemImage("ArrowUp"), new Point(MainDrawingPanel.Width / 2 - 10, 1));
                    ArrowDrawPosition = 2;
                }
            }
            else if (e.Location.X > MainDrawingPanel.Width - 26)
            {
                if (ArrowDrawPosition != 3)
                {
                    DrawMainScreen(true);
                    MainDrawingPanel.Cursor = Cursors.Hand;
                    _MainGraphicsObject.FillRectangle(Brushes.Black, new Rectangle(MainDrawingPanel.Width - 25, 0, MainDrawingPanel.Width, MainDrawingPanel.Height));
                    _MainGraphicsObject.DrawImage(GameGraphics.ResourceManager.GetSystemImage("ArrowRight"), new Point(MainDrawingPanel.Width - 20, MainDrawingPanel.Height / 2 - 10));
                    ArrowDrawPosition = 3;
                }
            }
            else if (e.Location.Y > MainDrawingPanel.Height - 26)
            {
                if (ArrowDrawPosition != 4)
                {
                    DrawMainScreen(true);
                    MainDrawingPanel.Cursor = Cursors.Hand;
                    _MainGraphicsObject.FillRectangle(Brushes.Black, new Rectangle(0, MainDrawingPanel.Height - 25, MainDrawingPanel.Width, MainDrawingPanel.Height));
                    _MainGraphicsObject.DrawImage(GameGraphics.ResourceManager.GetSystemImage("ArrowDown"), new Point(MainDrawingPanel.Width / 2 - 10, MainDrawingPanel.Height - 20));
                    ArrowDrawPosition = 4;
                }
            }
            else
            {
                SetCursorStyle();
                DrawMainScreen(true);
                ArrowDrawPosition = 0;
            }
            #endregion

            //StatusBarMouseState.Caption = "Mouse: Moving";

            if (_CurrentEditorMode == Enumerations.CurrentEditorMode.Paint)
            {
                if (Convert.ToBoolean(UseTextureBrushEditor.EditValue) == true)
                {
                    if (_TextureBrush == null) { return; }
                }
                else
                {
                    if (_SolidBrush == null)
                    {
                        return;
                    }
                }

                int _size = Convert.ToInt32(ToolSizeEditor.EditValue.ToString().ToInt());

                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {

                    if (_CurrentBrushShape == NebulaGames.RPGWorld.Enumerations.BrushShapes.Square)
                    {
                        if (_CurrentLayerInfo.LayerType == NebulaGames.RPGWorld.Enumerations.LayerType.Passable)
                        {
                            _CurrentWorld.WorkingLayerGraphics.FillRectangle(_SolidBrush, e.X - _CurrentWorld.OffsetX - (_size / 2), e.Y - _CurrentWorld.OffsetY - (_size / 2), _size, _size);
                        }
                        else
                        {
                            if (Convert.ToBoolean(UseTextureBrushEditor.EditValue) == true)
                            {
                                _CurrentWorld.WorkingLayerGraphics.FillRectangle(_TextureBrush, e.X - _CurrentWorld.OffsetX - (_size / 2), e.Y - _CurrentWorld.OffsetY - (_size / 2), _size, _size);
                            }
                            else
                            {
                                _CurrentWorld.WorkingLayerGraphics.FillRectangle(_SolidBrush, e.X - _CurrentWorld.OffsetX - (_size / 2), e.Y - _CurrentWorld.OffsetY - (_size / 2), _size, _size);
                            }

                        }
                    }
                    else
                    {
                        // SMOOTHING OPERATION
                        if (_LastMoveX > 0 && _LastMoveY > 0)
                        {
                            if (NebulaGames.RPGWorld.MonoGame.XNAMath.CalculateDistanceBetween2Points(new Microsoft.Xna.Framework.Vector2(e.X, e.Y), new Microsoft.Xna.Framework.Vector2(_LastMoveX, _LastMoveY)) > _size)
                            {
                                var _mid = NebulaGames.RPGWorld.MonoGame.XNAMath.CalculateMidPoint(new Microsoft.Xna.Framework.Vector2(e.X, e.Y), new Microsoft.Xna.Framework.Vector2(_LastMoveX, _LastMoveY));

                                if (_CurrentLayerInfo.LayerType == NebulaGames.RPGWorld.Enumerations.LayerType.Passable)
                                {
                                    _CurrentWorld.WorkingLayerGraphics.FillEllipse(_SolidBrush, _mid.X - _CurrentWorld.OffsetX - (_size / 2), _mid.Y - _CurrentWorld.OffsetY - (_size / 2), _size, _size);
                                }
                                else
                                {
                                    if (Convert.ToBoolean(UseTextureBrushEditor.EditValue) == true)
                                    {
                                        _CurrentWorld.WorkingLayerGraphics.FillEllipse(_TextureBrush, _mid.X - _CurrentWorld.OffsetX - (_size / 2), _mid.Y - _CurrentWorld.OffsetY - (_size / 2), _size, _size);
                                    }
                                    else
                                    {
                                        _CurrentWorld.WorkingLayerGraphics.FillEllipse(_SolidBrush, _mid.X - _CurrentWorld.OffsetX - (_size / 2), _mid.Y - _CurrentWorld.OffsetY - (_size / 2), _size, _size);
                                    }
                                }
                            }
                        }

                        if (_CurrentLayerInfo.LayerType == NebulaGames.RPGWorld.Enumerations.LayerType.Passable)
                        {
                            _CurrentWorld.WorkingLayerGraphics.FillEllipse(_SolidBrush, e.X - _CurrentWorld.OffsetX - (_size / 2), e.Y - _CurrentWorld.OffsetY - (_size / 2), _size, _size);
                        }
                        else
                        {
                            if (Convert.ToBoolean(UseTextureBrushEditor.EditValue) == true)
                            {
                                _CurrentWorld.WorkingLayerGraphics.FillEllipse(_TextureBrush, e.X - _CurrentWorld.OffsetX - (_size / 2), e.Y - _CurrentWorld.OffsetY - (_size / 2), _size, _size);
                            }
                            else
                            {
                                _CurrentWorld.WorkingLayerGraphics.FillEllipse(_SolidBrush, e.X - _CurrentWorld.OffsetX - (_size / 2), e.Y - _CurrentWorld.OffsetY - (_size / 2), _size, _size);
                            }
                        }
                    }
                    _LastMoveX = e.X;
                    _LastMoveY = e.Y;
                    // _DrawBoxGraphics.DrawImage(
                    // Only Draw A Portion For Performance.
                    DrawMainScreen(false, true);

                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    if (_CurrentBrushShape == NebulaGames.RPGWorld.Enumerations.BrushShapes.Square)
                    {

                        _CurrentWorld.WorkingLayerGraphics.FillRectangle(_TransparentBrush, e.X - _CurrentWorld.OffsetX - (_size / 2), e.Y - _CurrentWorld.OffsetY - (_size / 2), _size, _size);

                    }
                    else
                    {
                        // SMOOTHING OPERATION
                        if (_LastMoveX > 0 && _LastMoveY > 0)
                        {
                            if (NebulaGames.RPGWorld.MonoGame.XNAMath.CalculateDistanceBetween2Points(new Microsoft.Xna.Framework.Vector2(e.X, e.Y), new Microsoft.Xna.Framework.Vector2(_LastMoveX, _LastMoveY)) > _size)
                            {
                                var _mid = NebulaGames.RPGWorld.MonoGame.XNAMath.CalculateMidPoint(new Microsoft.Xna.Framework.Vector2(e.X, e.Y), new Microsoft.Xna.Framework.Vector2(_LastMoveX, _LastMoveY));
                                _CurrentWorld.WorkingLayerGraphics.FillEllipse(_TransparentBrush, _mid.X - _CurrentWorld.OffsetX - (_size / 2), _mid.Y - _CurrentWorld.OffsetY - (_size / 2), _size, _size);
                            }

                            _CurrentWorld.WorkingLayerGraphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                            _CurrentWorld.WorkingLayerGraphics.FillEllipse(_TransparentBrush, e.X - _CurrentWorld.OffsetX - (_size / 2), e.Y - _CurrentWorld.OffsetY - (_size / 2), _size, _size);
                        }
                        /*
                        for (int y = -radius; y <= radius; y++)
                            for (int x = -radius; x <= radius; x++)
                                if (x * x + y * y <= radius * radius)
                                    setpixel(origin.x + x, origin.y + y);
                         */
                        _CurrentWorld.WorkingLayerGraphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                        _CurrentWorld.WorkingLayerGraphics.FillEllipse(_TransparentBrush, e.X - _CurrentWorld.OffsetX - (_size / 2), e.Y - _CurrentWorld.OffsetY - (_size / 2), _size, _size);

                        _LastMoveX = e.X;
                        _LastMoveY = e.Y;

                        DrawMainScreen(false, true);
                    }
                }
                else
                {
                    _LastMoveX = 0;
                    _LastMoveY = 0;
                }
            }
            /*
        else if (_CurrentEditMode == NebulaGames.RPGWorld.Enumerations.EditorMode.Passable)
        {
            int _size = _BrushSize;

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {

                if (_CurrentBrushShape == NebulaGames.RPGWorld.Enumerations.BrushShapes.Square)
                {
                    _PG.FillRectangle(Brushes.Red, e.X - _WorkingWorld._OffsetX - (_size / 2), e.Y - _WorkingWorld._OffsetY - (_size / 2), _size, _size);
                }
                else
                {
                    _PG.FillEllipse(Brushes.Red, e.X - _WorkingWorld._OffsetX - (_size / 2), e.Y - _WorkingWorld._OffsetY - (_size / 2), _size, _size);
                }
                ReDraw();

            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (_CurrentBrushShape == NebulaGames.RPGWorld.Enumerations.BrushShapes.Square)
                {
                    _PG.FillRectangle(NebulaGames.RPGWorld.GameBuilder.Constants.BackGroundBrush, e.X - _WorkingWorld._OffsetX - (_size / 2), e.Y - _WorkingWorld._OffsetY - (_size / 2), _size, _size);
                }
                else
                {

                    _PG.FillEllipse(NebulaGames.RPGWorld.GameBuilder.Constants.BackGroundBrush, e.X - _WorkingWorld._OffsetX - (_size / 2), e.Y - _WorkingWorld._OffsetY - (_size / 2), _size, _size);
                }

                _WorkingWorld.MakePassableTransparent(_X, _Y);
                ReDraw();
            }
        }
         * */
        }


        private void MainIDE_ResizeBegin(object sender, EventArgs e)
        {
            _StopDrawing = true;
        }

        private void MainIDE_ResizeEnd(object sender, EventArgs e)
        {
            _StopDrawing = false;

            _MiniMapGraphicsObject.Dispose();
            _MainGraphicsObject.Dispose();

            if (_MiniMapGraphicsObject == null || _MainGraphicsObject == null)
            {
                _MiniMapGraphicsObject = MiniMapDrawPanel.CreateGraphics();
                _MainGraphicsObject = MainDrawingPanel.CreateGraphics();
            }

            if (_MiniMapGraphicsObject == null || _MainGraphicsObject == null) { throw new Exception("Error Creating Graphical Interface"); }

        }

        private void MainDrawingPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (_CurrentWorld == null) { return; }
            bool _ChangeRequested = false;

            string _MainKey = GameBuilder.EditorWorld.GetKey(_CurrentPosition.X, _CurrentPosition.Y, NebulaGames.RPGWorld.Enumerations.RelativePosition.Main);

            if (e.Location.X < 26)
            {
                _CurrentPosition.X--; _ChangeRequested = true;
            }
            else if (e.Location.Y < 26)
            {
                _CurrentPosition.Y--; _ChangeRequested = true;
            }
            else if (e.Location.X > MainDrawingPanel.Width - 26)
            {
                _CurrentPosition.X++; _ChangeRequested = true;
            }
            else if (e.Location.Y > MainDrawingPanel.Height - 26)
            {
                _CurrentPosition.Y++; _ChangeRequested = true;
            }

            MapXPositionEditor.EditValue = _CurrentPosition.X.ToString();
            MapYPositionEditor.EditValue = _CurrentPosition.Y.ToString();

            if (_ChangeRequested)
            {
                var _LayerInfo = (GameBuilder.LayerInfo)LayersListBoxControl.SelectedItem;
                _CurrentWorld.SetWorkingLayer(_LayerInfo.Name, _CurrentPosition.X, _CurrentPosition.Y);
            }

            //_CurrentWorld.Screens[_MainKey].la
            //_CurrentWorld.Screens[_CurrentWorld.get]
        }

        private void MapPositionEditor_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                bool _ChangeRequested = false;
                if (_CurrentPosition.X.ToString() != MapXPositionEditor.EditValue.ToString())
                {
                    _ChangeRequested = true;
                }

                if (_CurrentPosition.Y.ToString() != MapYPositionEditor.EditValue.ToString())
                {
                    _ChangeRequested = true;
                }

                if (_ChangeRequested)
                {
                    // Change Map Location X
                }
            }
            catch { }
        }

        private void ToolBrushShapeSquare_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _CurrentBrushShape = NebulaGames.RPGWorld.Enumerations.BrushShapes.Square;
            PerformRedraw();
        }

        private void ToolBrushShapeCircle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _CurrentBrushShape = NebulaGames.RPGWorld.Enumerations.BrushShapes.Circle;
            PerformRedraw();
        }

        private void PackageObjectsTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node == null) { return; }
            var x = e.Node["IsTexture"];
            try
            {
                if (Convert.ToBoolean(x) == true)
                {
                    _TextureBrush = new TextureBrush(GameGraphics.ResourceManager.GetTextureImage(e.Node["ID"].ToString()));
                }
            }
            catch { }
        }

        private void LayersListBoxControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LayersListBoxControl.SelectedIndex == -1) { return; }
            // if (_CurrentBufferGraphicsObject == null) { return; }

            _CurrentLayerInfo = (GameBuilder.LayerInfo)LayersListBoxControl.SelectedItem;
            _CurrentWorld.SetWorkingLayer(_CurrentLayerInfo.Name, _CurrentPosition.X, _CurrentPosition.Y);
            DrawMainScreen();

            if (_CurrentLayerInfo.LayerType == NebulaGames.RPGWorld.Enumerations.LayerType.Passable)
            {
                _SolidBrush = new SolidBrush(_CurrentWorld.NonPassableColor);
            }
        }

        private void BrushColorEditor_EditValueChanged(object sender, EventArgs e)
        {
            var _Color = (Color)BrushColorEditor.EditValue;
            _SolidBrush = new SolidBrush(_Color);
        }

        private void NewLayerButton_Click(object sender, EventArgs e)
        {
            Dialogs.LayerInfo _NewLayer = new Dialogs.LayerInfo();
            var _Results = _NewLayer.Show(_CurrentWorld);
            if (_Results == System.Windows.Forms.DialogResult.OK)
            {
                _CurrentWorld.AddLayer(_NewLayer.LayerInformationResult);
            }
            else
            {
                MessageBox.Show("Add Layer Canceled");
            }
            PopulateLayers();
        }

        private void LayerPropertiesButton_Click(object sender, EventArgs e)
        {
            NebulaGames.RPGWorld.Dialogs.LayerInfo _LayerInfo = new Dialogs.LayerInfo();
            _CurrentLayerInfo = (GameBuilder.LayerInfo)LayersListBoxControl.SelectedItem;
            var _Results = _LayerInfo.Show(_CurrentWorld, _CurrentLayerInfo);

            _CurrentLayerInfo.Name = _LayerInfo.LayerInformationResult.Name;
            _CurrentLayerInfo.LayerType = _LayerInfo.LayerInformationResult.LayerType;
            _CurrentLayerInfo.Visible = _LayerInfo.LayerInformationResult.Visible;
        }

        private void MoveLayerUpButton_Click(object sender, EventArgs e)
        {
            int _SelectedIndex = LayersListBoxControl.SelectedIndex;
            if (_SelectedIndex == 0) { return; }

        }

        private void ManageActorsButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _ActorManagerWindow.Show();

        }

        private void NewProjectButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _CurrentWorld = new NebulaGames.RPGWorld.GameBuilder.EditorWorld("New World", MainDrawingPanel.Width, MainDrawingPanel.Height);
            _CurrentPosition = new Point(0, 0);

            PopulateLayers();

            var _LayerInfo = (GameBuilder.LayerInfo)LayersListBoxControl.SelectedItem;
            _CurrentLayerInfo = _LayerInfo;
            _CurrentWorld.SetWorkingLayer((LayersListBoxControl.SelectedItem as GameBuilder.LayerInfo).Name, _CurrentPosition.X, _CurrentPosition.Y);
            WorkingFile.Caption += _LayerInfo.Name;
        }

        /// <summary>
        /// This Method Loads the Data From The Package File Selected.  It also Calls the ReloadPackages Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadPackageButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenProjectDialog.InitialDirectory = Constants.DataDirectory.EnsureDirectoryFormat() + "Packages\\";

            var _Result = OpenProjectDialog.ShowDialog();

            if (_Result == System.Windows.Forms.DialogResult.Cancel) { return; }
            if (OpenProjectDialog.FileName.ToLower().EndsWith(".xml") == false) { return; }

            string _FullFilePath = OpenProjectDialog.FileName;

            _ProgressBarWindow.ProgressBarMessage.Text = "Loading Package Data";
            _ProgressBarWindow.MainProgressBar.Value = 0;
            _ProgressBarWindow.Show();

            Guid _PID = ACT.Core.Windows.ControlMethods.StartProgessBar(this, _ProgressBarWindow.MainProgressBar, 50, 1);

            NebulaGames.RPGWorld.Assets.PackageInfo _PackageInfo = new NebulaGames.RPGWorld.Assets.PackageInfo(_FullFilePath);

            Packages.Add(_PackageInfo);

            ReloadPackages();

            ACT.Core.Windows.ControlMethods.StopProgressBar(_PID);
            _ProgressBarWindow.Hide();
        }

        /// <summary>
        /// Reloads The Packages Into The TreeLists
        /// Loads Textures, Objects, and Actors.
        /// </summary>
        private void ReloadPackages(bool Objects = true, bool Textures = true, bool Actors = true)
        {
            foreach (var _PackageInfo in Packages)
            {
                if (Objects)
                {
                    PackageObjectsTreeList.Nodes.Clear();
                    var _ParentObjectsPackage = PackageObjectsTreeList.AppendNode(new object[] { _PackageInfo.Name, "", _PackageInfo.Name, false, true, "" }, null);
                    foreach (var _Objects in _PackageInfo.LoadedObjects)
                    {
                        var _Parent = PackageObjectsTreeList.AppendNode(new object[] { _Objects.Name, "", _PackageInfo.Name, false, false, "" }, _ParentObjectsPackage);
                        foreach (var Object in _Objects.AllObjects)
                        {
                            bool _Add = false;
                            if (PackageObjectSearchText.NullOrEmpty()==false) 
                            {
                                if (Object.Value.Name.ToLower().Contains(PackageObjectSearchText.ToLower()))
                                {
                                    _Add = true;
                                }
                                else
                                {
                                    _Add = false;
                                }
                            }
                            else 
                            {
                                _Add = true;
                            }

                            if (_Add)
                            {
                                PackageObjectsTreeList.AppendNode(new object[] { Object.Value.Name, Object.Value.FileName.GetFileNameFromFullPath(), _PackageInfo.Name, true, false, Object.Value.ID }, _Parent);
                            }
                            else
                            {
                                _Parent.Visible = false;
                            }
                        }
                    }
                    if (PackageObjectSearchText.NullOrEmpty() == false)
                    {
                        PackageObjectsTreeList.ExpandAll();
                    }
                }

                if (Textures)
                {
                    PackageTexturesTreeList.Nodes.Clear();
                    var _ParentTexturePackage = PackageTexturesTreeList.AppendNode(new object[] { _PackageInfo.Name, "", _PackageInfo.Name, false, true, "" }, null);

                    foreach (var _Textures in _PackageInfo.LoadedTextures)
                    {
                        var _Parent = PackageTexturesTreeList.AppendNode(new object[] { _Textures.Name, "", _PackageInfo.Name, false, false, "" }, _ParentTexturePackage);

                        foreach (var Texture in _Textures.AllTextures)
                        {
                            PackageTexturesTreeList.AppendNode(new object[] { Texture.Value.Name, Texture.Value.FileName.GetFileNameFromFullPath(), _PackageInfo.Name, true, false, Texture.Value.ID }, _Parent);
                        }
                    }
                }

                if (Actors)
                {
                    PackageActorsTreeList.Nodes.Clear();
                    var _ParentActorPackage = PackageActorsTreeList.AppendNode(new object[] { _PackageInfo.Name, "", _PackageInfo.Name, false, true, "" }, null);

                    foreach (var _Actors in _PackageInfo.LoadedActors)
                    {
                        var _Parent = PackageActorsTreeList.AppendNode(new object[] { _Actors.Name, "", _PackageInfo.Name, false, true, "" }, _ParentActorPackage);
                        foreach (var Actor in _Actors.AllActors)
                        {
                            var _AnimationParent = PackageActorsTreeList.AppendNode(new object[] { Actor.Value.Name, "", _PackageInfo.Name, true, false, Actor.Value.ID }, _Parent);

                            foreach (var _Animation in Actor.Value.Animations)
                            {
                                PackageActorsTreeList.AppendNode(new object[] { _Animation.Value.Name + "-" + _Animation.Value.Direction, "", _PackageInfo.Name, false, false, _Animation.Value.ID }, _AnimationParent);
                            }
                        }
                    }
                }
            }
        }

        void _HoverTimer_Tick(object sender, EventArgs e)
        {
            //PackageObjectsPreviewWindow.InitData()
            PackageObjectsPreviewWindow.Show();
        }

        #region Texture - Nodes Highlight and Preview - Search Tree
        private void PackageTexturesTreeList_MouseHover(object sender, EventArgs e)
        {

        }

        private TreeListNode hotTrackNodeTextures = null;
        private TreeListNode HotTrackNodeTextures
        {
            get { return hotTrackNodeTextures; }
            set
            {
                if (hotTrackNodeTextures != value)
                {
                    TreeListNode prevHotTrackNode = hotTrackNodeTextures;
                    hotTrackNodeTextures = value;
                    if (PackageTexturesTreeList.ActiveEditor != null)
                        PackageTexturesTreeList.PostEditor();
                    PackageTexturesTreeList.RefreshNode(prevHotTrackNode);
                    PackageTexturesTreeList.RefreshNode(hotTrackNodeTextures);
                }
            }
        }
        private void PackageTexturesTreeList_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node == HotTrackNodeTextures)
            {
                e.Appearance.BackColor = Color.LightBlue;
                if (hotTrackNodeTextures != null)
                {
                    string PackageName = "";
                    if (hotTrackNodeTextures.GetValue("IsTexture").ToString().ToLower() == "true")
                    {
                        PackageName = hotTrackNodeTextures.ParentNode.GetValue("PackageName").ToString();
                        var _Package = Packages.Where(x => x.Name == PackageName).First();
                        var _Texture = _Package.FindTextureByID(hotTrackNodeTextures.GetValue("ID").ToString());

                        _ObjectPreviewGraphics.DrawImage(_Texture.Image.GetDecompressedImage(), 0, 0, MiniMapDrawPanel.Width, MiniMapDrawPanel.Height);
                    }
                }
            }
        }

        private void PackageTexturesTreeList_MouseMove(object sender, MouseEventArgs e)
        {
            TreeList treelist = sender as DevExpress.XtraTreeList.TreeList;
            TreeListHitInfo info = treelist.CalcHitInfo(new Point(e.X, e.Y));
            HotTrackNodeTextures = info.HitInfoType == HitInfoType.Cell ? info.Node : null;
        }

        private void PackageTexturesTreeList_MouseLeave(object sender, EventArgs e)
        {
            HotTrackNodeTextures = null;
        }

        bool _SearchChanged = false;
        // SEARCHING METHODS BELOW
        private void SearchPackageObjectsTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)27)
            {
                SearchPackageObjectsTextbox.Text = "";
                PackageObjectSearchText = "";
                if (_SearchChanged)
                {
                    ReloadPackages(true, false, false);
                    PackageObjectsTreeList.ExpandAll();
                }
                _SearchChanged = false;
                return;
            }

            if (SearchPackageObjectsTextbox.Text.Length > 2)
            {
                PackageObjectSearchText = SearchPackageObjectsTextbox.Text;
                ReloadPackages(true, false, false);
                _SearchChanged = true;
            }
            else
            {                
                PackageObjectSearchText = "";
                if (_SearchChanged)
                {
                    ReloadPackages(true, false, false);
                }
                _SearchChanged = false;
            }
        }
        #endregion

        #region TrackingObjectNodes Highlight and Preview

        private TreeListNode hotTrackNodeObjects = null;
        private TreeListNode HotTrackNodeObjects
        {
            get { return hotTrackNodeObjects; }
            set
            {
                if (hotTrackNodeObjects != value)
                {
                    TreeListNode prevHotTrackNode = hotTrackNodeObjects;
                    hotTrackNodeObjects = value;
                    if (PackageObjectsTreeList.ActiveEditor != null)
                        PackageObjectsTreeList.PostEditor();
                    PackageObjectsTreeList.RefreshNode(prevHotTrackNode);
                    PackageObjectsTreeList.RefreshNode(hotTrackNodeObjects);
                }
            }
        }

        private void PackageObjectsTreeList_MouseHover(object sender, EventArgs e)
        {
            //_HoverTimer.Interval = 1000;
            //  _HoverTimer.Start();
            //_HoverTimer.Tick += _HoverTimer_Tick;

        }

        /// <summary>
        /// Draws the Node Style and the Image Preview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PackageObjectsTreeList_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node == HotTrackNodeObjects)
            {
                e.Appearance.BackColor = Color.LightBlue;
                if (hotTrackNodeObjects != null)
                {
                    string PackageName = "";
                    if (hotTrackNodeObjects.GetValue("IsObject").ToString().ToLower() == "true")
                    {
                        PackageName = hotTrackNodeObjects.ParentNode.GetValue("PackageName").ToString();
                        var _Package = Packages.Where(x => x.Name == PackageName).First();
                        var _Object = _Package.FindObjectByID(hotTrackNodeObjects.GetValue("ID").ToString());

                        _ObjectPreviewGraphics.DrawImage(_Object.Image.GetDecompressedImage(), new System.Drawing.Rectangle(0, 0, MiniMapDrawPanel.Width, MiniMapDrawPanel.Height), _Object.StaticSource.Left, _Object.StaticSource.Top, _Object.StaticSource.Width, _Object.StaticSource.Height, GraphicsUnit.Pixel);
                    }
                }
            }
        }

        /// <summary>
        /// Tracks the Mouse Movement In the Objects Tree List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PackageObjectsTreeList_MouseMove(object sender, MouseEventArgs e)
        {
            TreeList treelist = sender as DevExpress.XtraTreeList.TreeList;
            TreeListHitInfo info = treelist.CalcHitInfo(new Point(e.X, e.Y));
            HotTrackNodeObjects = info.HitInfoType == HitInfoType.Cell ? info.Node : null;
        }

        /// <summary>
        /// Sets the Currently Tracked Item To Null
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PackageObjectsTreeList_MouseLeave(object sender, EventArgs e)
        {
            HotTrackNodeObjects = null;
        }
        #endregion

        private void EditSystemSettingsButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _ProjectSettingsWindow.Show();
        }

        private void ManageTexturesButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void ManageObjectsButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }




    }
}
