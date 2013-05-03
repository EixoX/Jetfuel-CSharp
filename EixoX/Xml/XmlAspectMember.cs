using EixoX.Adapters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Xml;


namespace EixoX.Xml
{
    public abstract class XmlAspectMember
        : AspectMember
    {
        private readonly string _LocalName;
        private readonly bool _Mandatory;

        public XmlAspectMember(ClassAcessor acessor, string localName, bool mandatory)
            : base(acessor)
        {
            this._LocalName = localName;
            this._Mandatory = mandatory;
        }


        /// <summary>
        /// Gets the local name of the member.
        /// </summary>
        public string LocalName
        {
            get { return this._LocalName; }
        }

        /// <summary>
        /// Indicates that the xml element is mandatory (it is always written, even if empty).
        /// </summary>
        public bool IsMandatory
        {
            get { return _Mandatory; }
        }


        protected abstract void WriteXml(object entity, XmlElement parent, IFormatProvider formatProvider, string localName, bool mandatory);
        protected abstract void ReadXml(object entity, XmlElement parent, IFormatProvider formatProvider, string localName, bool mandatory);

        /// <summary>
        /// Serializes the content of the member to an xml element.
        /// </summary>
        /// <param name="entity">The entity to read from.</param>
        /// <param name="parent">The xml element to write to.</param>
        /// <param name="defaultCulture">The default culture to use when formatting values.</param>
        public void WriteXml(object entity, XmlElement parent, IFormatProvider formatProvider)
        {
            WriteXml(entity, parent, formatProvider, _LocalName, _Mandatory);
        }

        /// <summary>
        /// Deserializes the content of the member from an xml element.
        /// </summary>
        /// <param name="entity">The entity to write to.</param>
        /// <param name="parent">The parent element to read from.</param>
        /// <param name="defaultCulture">The default culture to use when parsing.</param>
        public void ReadXml(object entity, XmlElement parent, IFormatProvider formatProvider)
        {
            ReadXml(entity, parent, formatProvider, _LocalName, _Mandatory);
        }


    }
}
