using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.IO;
using ACT.Core.Extensions;
using NebulaGames.RPGWorld.MonoGame.Extensions;


namespace NebulaGames.RPGWorld.MonoGame.Managers
{
    public class ActorManager// : GameComponent
    {
        private Dictionary<string, string> _Actors = new Dictionary<string, string>();

        public ActorManager( string BaseDirectory)
            //: base(XnaGame)
        {
            // Search Folders


            string[] _SubDirectories = Directory.GetDirectories(BaseDirectory.FormatDirectory() + "Actors");

            foreach (string _SubDirectory in _SubDirectories)
            {
                if (File.Exists(_SubDirectory.FormatDirectory() + "data.xml"))
                {
                    if (_Actors.ContainsKey(_SubDirectory.GetDirectoryName()))
                    {
                        _Actors.Remove(_SubDirectory.GetDirectoryName());
                    }

                    _Actors.Add(_SubDirectory.GetDirectoryName(), File.ReadAllText(_SubDirectory.FormatDirectory() + "data.xml"));
                }
                else
                {
                    ErrorLogable.LogError(new FileNotFoundException("data.xml was not found in : " + _SubDirectory), "Could not load data file for: " + _SubDirectory);
                }
            }

            // Search Zip
        }
    }
}
