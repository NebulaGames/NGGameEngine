using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using System.Xml.XPath;


namespace NebulaGames.RPGWorld.MonoGame.Managers
{
    public class SettingsManager : GameComponent
    {
        public string Fonts;
        public string WindowSize;

        public SettingsManager(Game XnaGame)
            : base(XnaGame)
        {
            this.Game.Components.Add(this);
            string Data = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "Data\\Settings.xml");

            System.IO.MemoryStream _stream = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(Data));

            XPathDocument _doc = new XPathDocument(_stream);
            XPathNavigator _nav = _doc.CreateNavigator();
            XPathExpression _expr;

            _expr = _nav.Compile("/game");

            XPathNodeIterator _Iter = _nav.Select(_expr);

            _Iter.MoveNext();

            var _Nav2 = _Iter.Current.Clone();

            Fonts = _Nav2.SelectSingleNode("fonts").Value;
            WindowSize = _Nav2.SelectSingleNode("windowsize").Value;
                       

        }

    }
}
