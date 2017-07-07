using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NebulaGames.RPGWorld.Interfaces;
using NebulaGames.RPGWorld.Structs;

namespace NebulaGames.RPGWorld.BaseClasses
{
    public class NGWindow_Base : Interfaces.I_NGWindow
    {
        protected bool _Enabled = false;
        protected string _WindowName = "";
        protected string _ID = "";
        protected int _UpdateOrder = 0;
        protected int _DrawOrder = 0;
        protected bool _Visible = false;
        protected List<I_NGWindowObject> _WindowObjects = new List<I_NGWindowObject>();
        protected Game _BaseGame;
        protected SpriteBatch _SpriteBatch;

        public virtual string WindowName { get; }
        public virtual string ID => _ID;
        public virtual List<I_NGWindowObject> WindowObjects => _WindowObjects;
        public virtual List<TextData> Text { get; set; }
        public virtual List<StaticTextureData> StaticTextures { get; set; }
        public virtual string Code { get; set; }
        public virtual Assembly CompiledCode { get; set; }

        public virtual bool Enabled { get { return _Enabled; } set { _Enabled = value; } }

        public virtual int UpdateOrder => _UpdateOrder;

        public int DrawOrder
        {
            get { return _DrawOrder; }
            set
            {
                _DrawOrder = value;
                if (DrawOrderChanged != null)
                    DrawOrderChanged(this, null);

                OnDrawOrderChanged(this, null);
            }
        }

        public bool Visible
        {
            get { return _Visible; }
            set
            {
                _Visible = value;

                VisibleChanged?.Invoke(this, null);

                OnVisibleChanged(this, null);
            }

        }
        protected virtual void OnVisibleChanged(object sender, EventArgs args)
        {
        }

        protected virtual void OnDrawOrderChanged(object sender, EventArgs args)
        {
        }

        public virtual event EventHandler<EventArgs> EnabledChanged;
        public virtual event EventHandler<EventArgs> UpdateOrderChanged;
        public virtual event EventHandler<EventArgs> DrawOrderChanged;
        public virtual event EventHandler<EventArgs> VisibleChanged;

        public virtual List<DataPacket> AddWindowObject(string ObjectID)
        {
            List<DataPacket> _TmpReturn = new List<DataPacket>();
            // Load The Window

            return _TmpReturn;
        }

        public virtual int CompareTo(I_NGWindow other)
        {
            if (other.ID == this.ID) { return 0; }
            else { return -1; }
        }

        public virtual void Draw(GameTime gameTime)
        {
            if (this._SpriteBatch == null)
            {
                //if (GameData.SpriteBatchManager.SpriteBatches.Count() == 0) { throw new Exception("No Valid Sprite Batches Defined, NGWindow_Base->Draw, " + Environment.StackTrace); }
            }
        }

        public virtual void Initialize()
        {
            //
        }

        public virtual List<DataPacket> InitWindow(Game mGame)
        {
            List<DataPacket> _TmpReturn = new List<DataPacket>();

            this._BaseGame = mGame;
            // LOAD XML

            return _TmpReturn;
        }

        public virtual void ProcessMouseClick(Vector2 MouseLocation, bool PiercingClick = false)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _WindowObjects = null;
                    Text = null;
                    StaticTextures = null;

                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~NGWindow_Base() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public virtual void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
