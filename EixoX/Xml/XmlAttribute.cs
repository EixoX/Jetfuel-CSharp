using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Xml
{
    [Serializable]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public class XmlAttribute : Attribute
    {
        private XmlType _XmlType;
        private readonly string _Name;
        private readonly string _Culture;

        public XmlAttribute() { this._XmlType = Xml.XmlType.Element; }
        public XmlAttribute(XmlType type) { this._XmlType = type; }
        public XmlAttribute(XmlType type, string name)
        {
            this._XmlType = type;
            this._Name = name;
        }
        public XmlAttribute(XmlType type, string name, string culture)
        {
            this._XmlType = type;
            this._Name = name;
            this._Culture = culture;
        }

        public XmlType XmlType
        {
            get { return this._XmlType; }
        }

        public string Name
        {
            get { return this._Name; }
        }

        public string Culture
        {
            get { return this._Culture; }
        }
    }
}
