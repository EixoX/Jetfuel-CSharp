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
        private string _FormatString;

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

        public XmlAttribute(string name)
        {
            this._XmlType = Xml.XmlType.Element;
            this._Name = name;
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

        public string FormatString
        {
            get
            {
                return this._FormatString;
            }
            set
            {
                _FormatString = value;
            }
        }

        public bool IsMandatory { get; set; }
    }
}
