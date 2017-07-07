using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NebulaGames.RPGWorld.DataManager.ImageManager
{
    public partial class ImageViewer : Form
    {
        byte[] _ImageData;
        string _ImageName;

        public ImageViewer() { }
        public ImageViewer(string ImageName, byte[] ImageData, Size WindowSize, FormStartPosition StartPosition = FormStartPosition.CenterScreen, int Left = 0, int Top = 0)
        {
            InitializeComponent();

            this.Width = WindowSize.Width;
            this.Height = WindowSize.Height;
            _ImageData = ImageData;
            _ImageName = ImageName;

            //PicturePreviewBox.
            PicturePreviewBox.Image = Image.FromStream(new System.IO.MemoryStream(ImageData));
            
            if (StartPosition == FormStartPosition.Manual)
            {
                this.Left = Left;
                this.Top = Top;
            }
            else
            {
                this.StartPosition = StartPosition;
            }
            
        }

        public void InitData(string ImageName, byte[] ImageData, Size WindowSize, FormStartPosition StartPosition = FormStartPosition.CenterScreen, int Left = 0, int Top = 0)
        {

            this.Width = WindowSize.Width;
            this.Height = WindowSize.Height;
            _ImageData = ImageData;

            if (_ImageName != ImageName)
            {
                _ImageName = ImageName;
                PicturePreviewBox.Image = Image.FromStream(new System.IO.MemoryStream(ImageData));
            }

            if (StartPosition == FormStartPosition.Manual)
            {
                this.Left = Left;
                this.Top = Top;
            }
            else
            {
                this.StartPosition = StartPosition;
            }
        }
        
    }
}
