using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;


namespace EixoX.Xml
{
    public class XmlAspectMember
        : AspectMember
    {
        private readonly XmlType _XmlType;
        private readonly string _XmlName;
        private readonly CultureInfo _Culture;


        public XmlAspectMember(ClassAcessor acessor, XmlType xmlType, string xmlName, CultureInfo culture)
            : base(acessor)
        {
            this._XmlName = xmlName;
            this._XmlType = xmlType;
            this._Culture = culture;
        }

        public XmlType XmlType
        {
            get { return this._XmlType; }
        }

        public string XmlName
        {
            get { return this._XmlName; }
        }

        public CultureInfo Culture
        {
            get { return this._Culture; }
        }

    }
}
