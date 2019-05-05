using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magisterka.Tests.Services
{

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class PlaceSearchResponse
    {

        private string statusField;

        private PlaceSearchResponseResult[] resultField;

        /// <remarks/>
        public string status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("result")]
        public PlaceSearchResponseResult[] result
        {
            get
            {
                return this.resultField;
            }
            set
            {
                this.resultField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PlaceSearchResponseResult
    {

        private string nameField;

        private string[] typeField;

        private string formatted_addressField;

        private PlaceSearchResponseResultGeometry geometryField;

        private decimal ratingField;

        private bool ratingFieldSpecified;

        private string iconField;

        private string referenceField;

        private string idField;

        private PlaceSearchResponseResultOpening_hours opening_hoursField;

        private PlaceSearchResponseResultPhoto photoField;

        private string place_idField;

        /// <remarks/>
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

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("type")]
        public string[] type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        public string formatted_address
        {
            get
            {
                return this.formatted_addressField;
            }
            set
            {
                this.formatted_addressField = value;
            }
        }

        /// <remarks/>
        public PlaceSearchResponseResultGeometry geometry
        {
            get
            {
                return this.geometryField;
            }
            set
            {
                this.geometryField = value;
            }
        }

        /// <remarks/>
        public decimal rating
        {
            get
            {
                return this.ratingField;
            }
            set
            {
                this.ratingField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ratingSpecified
        {
            get
            {
                return this.ratingFieldSpecified;
            }
            set
            {
                this.ratingFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string icon
        {
            get
            {
                return this.iconField;
            }
            set
            {
                this.iconField = value;
            }
        }

        /// <remarks/>
        public string reference
        {
            get
            {
                return this.referenceField;
            }
            set
            {
                this.referenceField = value;
            }
        }

        /// <remarks/>
        public string id
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
        public PlaceSearchResponseResultOpening_hours opening_hours
        {
            get
            {
                return this.opening_hoursField;
            }
            set
            {
                this.opening_hoursField = value;
            }
        }

        /// <remarks/>
        public PlaceSearchResponseResultPhoto photo
        {
            get
            {
                return this.photoField;
            }
            set
            {
                this.photoField = value;
            }
        }

        /// <remarks/>
        public string place_id
        {
            get
            {
                return this.place_idField;
            }
            set
            {
                this.place_idField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PlaceSearchResponseResultGeometry
    {

        private PlaceSearchResponseResultGeometryLocation locationField;

        private PlaceSearchResponseResultGeometryViewport viewportField;

        /// <remarks/>
        public PlaceSearchResponseResultGeometryLocation location
        {
            get
            {
                return this.locationField;
            }
            set
            {
                this.locationField = value;
            }
        }

        /// <remarks/>
        public PlaceSearchResponseResultGeometryViewport viewport
        {
            get
            {
                return this.viewportField;
            }
            set
            {
                this.viewportField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PlaceSearchResponseResultGeometryLocation
    {

        private decimal latField;

        private decimal lngField;

        /// <remarks/>
        public decimal lat
        {
            get
            {
                return this.latField;
            }
            set
            {
                this.latField = value;
            }
        }

        /// <remarks/>
        public decimal lng
        {
            get
            {
                return this.lngField;
            }
            set
            {
                this.lngField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PlaceSearchResponseResultGeometryViewport
    {

        private PlaceSearchResponseResultGeometryViewportSouthwest southwestField;

        private PlaceSearchResponseResultGeometryViewportNortheast northeastField;

        /// <remarks/>
        public PlaceSearchResponseResultGeometryViewportSouthwest southwest
        {
            get
            {
                return this.southwestField;
            }
            set
            {
                this.southwestField = value;
            }
        }

        /// <remarks/>
        public PlaceSearchResponseResultGeometryViewportNortheast northeast
        {
            get
            {
                return this.northeastField;
            }
            set
            {
                this.northeastField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PlaceSearchResponseResultGeometryViewportSouthwest
    {

        private decimal latField;

        private decimal lngField;

        /// <remarks/>
        public decimal lat
        {
            get
            {
                return this.latField;
            }
            set
            {
                this.latField = value;
            }
        }

        /// <remarks/>
        public decimal lng
        {
            get
            {
                return this.lngField;
            }
            set
            {
                this.lngField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PlaceSearchResponseResultGeometryViewportNortheast
    {

        private decimal latField;

        private decimal lngField;

        /// <remarks/>
        public decimal lat
        {
            get
            {
                return this.latField;
            }
            set
            {
                this.latField = value;
            }
        }

        /// <remarks/>
        public decimal lng
        {
            get
            {
                return this.lngField;
            }
            set
            {
                this.lngField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PlaceSearchResponseResultOpening_hours
    {

        private bool open_nowField;

        /// <remarks/>
        public bool open_now
        {
            get
            {
                return this.open_nowField;
            }
            set
            {
                this.open_nowField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class PlaceSearchResponseResultPhoto
    {

        private string photo_referenceField;

        private ushort widthField;

        private ushort heightField;

        private string html_attributionField;

        /// <remarks/>
        public string photo_reference
        {
            get
            {
                return this.photo_referenceField;
            }
            set
            {
                this.photo_referenceField = value;
            }
        }

        /// <remarks/>
        public ushort width
        {
            get
            {
                return this.widthField;
            }
            set
            {
                this.widthField = value;
            }
        }

        /// <remarks/>
        public ushort height
        {
            get
            {
                return this.heightField;
            }
            set
            {
                this.heightField = value;
            }
        }

        /// <remarks/>
        public string html_attribution
        {
            get
            {
                return this.html_attributionField;
            }
            set
            {
                this.html_attributionField = value;
            }
        }
    }


}
