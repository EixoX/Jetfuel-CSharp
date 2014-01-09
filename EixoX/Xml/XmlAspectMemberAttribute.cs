using EixoX.Adapters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace EixoX.Xml
{
    public class XmlAspectMemberAttribute
        : XmlAspectMember
    {
        private SimpleAdapter _Adapter;

        public XmlAspectMemberAttribute(ClassAcessor acessor, string localName, bool mandatory, SimpleAdapter adapter)
            : base(acessor, localName, mandatory)
        {
            this._Adapter = adapter;
        }

        protected override void WriteXml(object entity, XmlElement parent, IFormatProvider formatProvider, string localName, bool mandatory)
        {
            object value = GetValue(entity);
            if (_Adapter.IsEmpty(value))
            {
                if (mandatory)
                {
                    parent.SetAttribute(localName, "");
                }
            }
            else
            {
                string content = _Adapter.FormatObject(value, formatProvider);
                parent.SetAttribute(localName, content);
            }

        }

        protected override void ReadXml(object entity, XmlElement parent, IFormatProvider formatProvider, string localName, bool mandatory)
        {
            string content = parent.GetAttribute(localName);
            object value = _Adapter.ParseObject(content, formatProvider);
            SetValue(entity, value);
        }
    }
}
