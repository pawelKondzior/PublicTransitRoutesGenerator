using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magisterka.Infrastructure.Shared.IoDto2017
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class linie
    {

        private linieLinia liniaField;

        /// <remarks/>
        public linieLinia linia
        {
            get
            {
                return this.liniaField;
            }
            set
            {
                this.liniaField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class linieLinia
    {

        private linieLiniaWariant[] wariantField;

        private string nazwaField;

        private string typField;

        private string wazny_odField;

        private string wazny_doField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("wariant")]
        public linieLiniaWariant[] wariant
        {
            get
            {
                return this.wariantField;
            }
            set
            {
                this.wariantField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string nazwa
        {
            get
            {
                return this.nazwaField;
            }
            set
            {
                this.nazwaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string typ
        {
            get
            {
                return this.typField;
            }
            set
            {
                this.typField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string wazny_od
        {
            get
            {
                return this.wazny_odField;
            }
            set
            {
                this.wazny_odField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string wazny_do
        {
            get
            {
                return this.wazny_doField;
            }
            set
            {
                this.wazny_doField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class linieLiniaWariant
    {

        private linieLiniaWariantPrzystanek[] przystanekField;

        private byte idField;

        private string nazwaField;

        private string objField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("przystanek")]
        public linieLiniaWariantPrzystanek[] przystanek
        {
            get
            {
                return this.przystanekField;
            }
            set
            {
                this.przystanekField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string nazwa
        {
            get
            {
                return this.nazwaField;
            }
            set
            {
                this.nazwaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string obj
        {
            get
            {
                return this.objField;
            }
            set
            {
                this.objField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class linieLiniaWariantPrzystanek
    {

        private linieLiniaWariantPrzystanekPrzystanek[] czasyField;

        private linieLiniaWariantPrzystanekTabliczka tabliczkaField;

        private uint idField;

        private string nazwaField;

        private string ulicaField;

        private string cechyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("przystanek", IsNullable = false)]
        public linieLiniaWariantPrzystanekPrzystanek[] czasy
        {
            get
            {
                return this.czasyField;
            }
            set
            {
                this.czasyField = value;
            }
        }

        /// <remarks/>
        public linieLiniaWariantPrzystanekTabliczka tabliczka
        {
            get
            {
                return this.tabliczkaField;
            }
            set
            {
                this.tabliczkaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string nazwa
        {
            get
            {
                return this.nazwaField;
            }
            set
            {
                this.nazwaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ulica
        {
            get
            {
                return this.ulicaField;
            }
            set
            {
                this.ulicaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string cechy
        {
            get
            {
                return this.cechyField;
            }
            set
            {
                this.cechyField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class linieLiniaWariantPrzystanekPrzystanek
    {

        private byte numerField;

        private uint idField;

        private string nazwaField;

        private byte czasField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte numer
        {
            get
            {
                return this.numerField;
            }
            set
            {
                this.numerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string nazwa
        {
            get
            {
                return this.nazwaField;
            }
            set
            {
                this.nazwaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte czas
        {
            get
            {
                return this.czasField;
            }
            set
            {
                this.czasField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class linieLiniaWariantPrzystanekTabliczka
    {

        private linieLiniaWariantPrzystanekTabliczkaDzien[] dzienField;

        private byte idField;

        private byte mcField;

        private string cechyField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("dzien")]
        public linieLiniaWariantPrzystanekTabliczkaDzien[] dzien
        {
            get
            {
                return this.dzienField;
            }
            set
            {
                this.dzienField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte mc
        {
            get
            {
                return this.mcField;
            }
            set
            {
                this.mcField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string cechy
        {
            get
            {
                return this.cechyField;
            }
            set
            {
                this.cechyField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class linieLiniaWariantPrzystanekTabliczkaDzien
    {

        private linieLiniaWariantPrzystanekTabliczkaDzienGodz[] godzField;

        private string nazwaField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("godz")]
        public linieLiniaWariantPrzystanekTabliczkaDzienGodz[] godz
        {
            get
            {
                return this.godzField;
            }
            set
            {
                this.godzField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string nazwa
        {
            get
            {
                return this.nazwaField;
            }
            set
            {
                this.nazwaField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class linieLiniaWariantPrzystanekTabliczkaDzienGodz
    {

        private linieLiniaWariantPrzystanekTabliczkaDzienGodzMin[] minField;

        private byte hField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("min")]
        public linieLiniaWariantPrzystanekTabliczkaDzienGodzMin[] min
        {
            get
            {
                return this.minField;
            }
            set
            {
                this.minField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte h
        {
            get
            {
                return this.hField;
            }
            set
            {
                this.hField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class linieLiniaWariantPrzystanekTabliczkaDzienGodzMin
    {

        private byte mField;

        private string oznField;

        private string przypField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte m
        {
            get
            {
                return this.mField;
            }
            set
            {
                this.mField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ozn
        {
            get
            {
                return this.oznField;
            }
            set
            {
                this.oznField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string przyp
        {
            get
            {
                return this.przypField;
            }
            set
            {
                this.przypField = value;
            }
        }
    }



}
