﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace NebulaGames.RPGWorld.MonoGame.Actors.Dynamic
{
    public class Peon : Actor, ICloneable
    {
        [NonSerialized]
        SpriteBatch _LocalBatch;

        public Peon(Game XnaGame, string ActorName, SpriteBatch LocalBatchRef)
            : base(XnaGame, ActorName, LocalBatchRef)
        {
            _LocalBatch = LocalBatchRef;
        }



        public object Clone()
        {
            return base.MemberwiseClone();
            //return this.clo 
        //    throw new NotImplementedException();
        }
    }
}
