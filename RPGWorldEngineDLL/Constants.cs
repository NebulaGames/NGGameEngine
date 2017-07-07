using System;
using System.Collections.Generic;
using ACT.Core.Extensions;
using System.Text;
using System.Drawing;
using System.Dynamic;
using System.Xml.Linq;

namespace NebulaGames.RPGWorld
{
    public static class Constants
    {
        #region Static Variables

        internal static string FileName;
        public static string Name;
        public static string ApplicationName;
        public static string Version;
        public static string BackgroundColor;
        public static SolidBrush BackGroundBrush = new SolidBrush(Color.FromArgb(0, 255, 0));
        public static System.Drawing.Font ControlFont
        {
            get
            {
                return new Font(Font, 12);
            }
        }

        public static string DataDirectory = "";
        public static string LayoutDirectory = "";
        public static string ProjectTemplateDirectory = "";
        public static string ActorDataDirectory = "";
        public static string ImageDirectory = "";
        public static string MusicDirectory = "";
        public static string SoundEffectDirectory = "";
        public static string ExportDirectory = "";
        public static string SpriteFontDirectory = "";
        public static string Font = "verdana";

        public static bool DataDirectoryAbsolute = false;
        public static bool LayoutDirectoryAbsolute = false;
        public static bool ProjectTemplateDirectoryAbsolute = false;
        public static bool ActorDataDirectoryAbsolute = false;
        public static bool ImageDirectoryAbsolute = false;
        public static bool MusicDirectoryAbsolute = false;
        public static bool SoundEffectDirectoryAbsolute = false;
        public static bool ExportDirectoryAbsolute = false;
        public static bool SpriteFontDirectoryAbsolute = false;

        #endregion

        public static void LoadData(string BaseDirectory)
        {
            FileName = BaseDirectory.EnsureDirectoryFormat() + "Data\\Configuration\\Settings.xml";
            if (FileName.FileExists() == false) { throw new System.IO.FileNotFoundException(FileName); }
            BaseDirectory = FileName.GetDirectoryFromFileLocation();

            XElement xelement = XElement.Load(FileName);

            Name = xelement.Element("name").Value;
            ApplicationName = xelement.Element("applicationname").Value;
            Version = xelement.Element("version").Value;
            BackgroundColor = xelement.Element("backgroundcolor").Value;

            try
            {
                string[] _RGBColor = BackgroundColor.SplitString(",", StringSplitOptions.RemoveEmptyEntries);
                BackGroundBrush = new SolidBrush(Color.FromArgb(_RGBColor[0].ToInt(), _RGBColor[1].ToInt(), _RGBColor[2].ToInt()));
            }
            catch { }

            #region DataDirectory
            try { DataDirectoryAbsolute = Convert.ToBoolean(xelement.Element("datadirectory").Attribute("absolute").Value); }
            catch { }

            if (DataDirectoryAbsolute)
            {
                DataDirectory = xelement.Element("datadirectory").Value;
            }
            else
            {
                DataDirectory = BaseDirectory.EnsureDirectoryFormat() + xelement.Element("datadirectory").Value;
            }
            #endregion

            #region LayoutDirectory
            try { LayoutDirectoryAbsolute = Convert.ToBoolean(xelement.Element("layoutdirectory").Attribute("absolute").Value); }
            catch { }
            if (LayoutDirectoryAbsolute)
            {
                LayoutDirectory = xelement.Element("layoutdirectory").Value;
            }
            else
            {
                LayoutDirectory = BaseDirectory.EnsureDirectoryFormat() + xelement.Element("layoutdirectory").Value;
            }
            #endregion

            #region ProjectTemplateDirectory
            try { ProjectTemplateDirectoryAbsolute = Convert.ToBoolean(xelement.Element("projecttemplatedirectory").Attribute("absolute").Value); }
            catch { }
            if (ProjectTemplateDirectoryAbsolute)
            {
                ProjectTemplateDirectory = xelement.Element("projecttemplatedirectory").Value;
            }
            else
            {
                ProjectTemplateDirectory = BaseDirectory.EnsureDirectoryFormat() + xelement.Element("projecttemplatedirectory").Value;
            }
            #endregion

            #region ActorDataDirectory
            try { ActorDataDirectoryAbsolute = Convert.ToBoolean(xelement.Element("actordatadirectory").Attribute("absolute").Value); }
            catch { }
            if (ActorDataDirectoryAbsolute)
            {
                ActorDataDirectory = xelement.Element("actordatadirectory").Value;
            }
            else
            {
                ActorDataDirectory = BaseDirectory.EnsureDirectoryFormat() + xelement.Element("actordatadirectory").Value;
            }
            #endregion

            #region ImageDirectory
            try { ImageDirectoryAbsolute = Convert.ToBoolean(xelement.Element("imagedirectory").Attribute("absolute").Value); }
            catch { }
            if (ImageDirectoryAbsolute)
            {
                ImageDirectory = xelement.Element("imagedirectory").Value;
            }
            else
            {
                ImageDirectory = BaseDirectory.EnsureDirectoryFormat() + xelement.Element("imagedirectory").Value;
            }
            #endregion

            #region MusicDirectory
            try { MusicDirectoryAbsolute = Convert.ToBoolean(xelement.Element("musicdirectory").Attribute("absolute").Value); }
            catch { }
            if (MusicDirectoryAbsolute)
            {
                MusicDirectory = xelement.Element("musicdirectory").Value;
            }
            else
            {
                MusicDirectory = BaseDirectory.EnsureDirectoryFormat() + xelement.Element("musicdirectory").Value;
            }
            #endregion

            #region SoundEffectDirectory
            try { SoundEffectDirectoryAbsolute = Convert.ToBoolean(xelement.Element("soundeffectdirectory").Attribute("absolute").Value); }
            catch { }
            if (SoundEffectDirectoryAbsolute)
            {
                SoundEffectDirectory = xelement.Element("soundeffectdirectory").Value;
            }
            else
            {
                SoundEffectDirectory = BaseDirectory.EnsureDirectoryFormat() + xelement.Element("soundeffectdirectory").Value;
            }
            #endregion

            #region ExportDirectory
            try { ExportDirectoryAbsolute = Convert.ToBoolean(xelement.Element("exportdirectory").Attribute("absolute").Value); }
            catch { }
            if (ExportDirectoryAbsolute)
            {
                ExportDirectory = xelement.Element("exportdirectory").Value;
            }
            else
            {
                ExportDirectory = BaseDirectory.EnsureDirectoryFormat() + xelement.Element("exportdirectory").Value;
            }
            #endregion

            #region SpriteFontDirectory
            try { SpriteFontDirectoryAbsolute = Convert.ToBoolean(xelement.Element("spritefontdirectory").Attribute("absolute").Value); }
            catch { }
            if (SpriteFontDirectoryAbsolute)
            {
                SpriteFontDirectory = xelement.Element("spritefontdirectory").Value;
            }
            else
            {
                SpriteFontDirectory = BaseDirectory.EnsureDirectoryFormat() + xelement.Element("spritefontdirectory").Value;
            }
            #endregion
            

        }
    }
}
