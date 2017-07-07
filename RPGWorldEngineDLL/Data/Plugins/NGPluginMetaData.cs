using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebulaGames.RPGWorld.Data.Plugins
{

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class plugin
    {

        private pluginVersioninfo versioninfoField;

        private string nameField;

        /// <remarks/>
        public pluginVersioninfo versioninfo
        {
            get
            {
                return this.versioninfoField;
            }
            set
            {
                this.versioninfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class pluginVersioninfo
    {

        private pluginVersioninfoCompatibilityhistory compatibilityhistoryField;

        private string[] authorsField;

        private string descriptionField;

        private byte majorField;

        private byte minorField;

        private byte buildField;

        private bool releaseField;

        private bool expirimentalField;

        private string builddateField;

        /// <remarks/>
        public pluginVersioninfoCompatibilityhistory compatibilityhistory
        {
            get
            {
                return this.compatibilityhistoryField;
            }
            set
            {
                this.compatibilityhistoryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("author", IsNullable = false)]
        public string[] authors
        {
            get
            {
                return this.authorsField;
            }
            set
            {
                this.authorsField = value;
            }
        }

        /// <remarks/>
        public string description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte major
        {
            get
            {
                return this.majorField;
            }
            set
            {
                this.majorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte minor
        {
            get
            {
                return this.minorField;
            }
            set
            {
                this.minorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte build
        {
            get
            {
                return this.buildField;
            }
            set
            {
                this.buildField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool release
        {
            get
            {
                return this.releaseField;
            }
            set
            {
                this.releaseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool expirimental
        {
            get
            {
                return this.expirimentalField;
            }
            set
            {
                this.expirimentalField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string builddate
        {
            get
            {
                return this.builddateField;
            }
            set
            {
                this.builddateField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class pluginVersioninfoCompatibilityhistory
    {

        private pluginVersioninfoCompatibilityhistoryHistory historyField;

        /// <remarks/>
        public pluginVersioninfoCompatibilityhistoryHistory history
        {
            get
            {
                return this.historyField;
            }
            set
            {
                this.historyField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class pluginVersioninfoCompatibilityhistoryHistory
    {

        private pluginVersioninfoCompatibilityhistoryHistoryBuild buildField;

        /// <remarks/>
        public pluginVersioninfoCompatibilityhistoryHistoryBuild build
        {
            get
            {
                return this.buildField;
            }
            set
            {
                this.buildField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class pluginVersioninfoCompatibilityhistoryHistoryBuild
    {

        private byte majorField;

        private byte minorField;

        private byte buildField;

        private string compatibilityField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte major
        {
            get
            {
                return this.majorField;
            }
            set
            {
                this.majorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte minor
        {
            get
            {
                return this.minorField;
            }
            set
            {
                this.minorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte build
        {
            get
            {
                return this.buildField;
            }
            set
            {
                this.buildField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string compatibility
        {
            get
            {
                return this.compatibilityField;
            }
            set
            {
                this.compatibilityField = value;
            }
        }
    }


}
