using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;

namespace EixoX.Xml
{
    public class XmlAspectMemberComposite
        : XmlAspectMember
    {
        private readonly XmlAspect _Aspect;
        private readonly ConstructorInfo _Constructor;

        public XmlAspectMemberComposite(ClassAcessor acessor, string localName, bool mandatory, XmlAspect aspect, ConstructorInfo constructor)
            : base(acessor, localName, mandatory)
        {
            this._Aspect = aspect;
            this._Constructor = constructor;
        }

        protected override void WriteXml(object entity, System.Xml.XmlElement parent, IFormatProvider formatProvider, string localName, bool mandatory)
        {
            object value = GetValue(entity);
            if (value == null)
            {
                if (mandatory)
                {
                    XmlElement element = parent.OwnerDocument.CreateElement(localName);
                    parent.AppendChild(element);
                }
            }
            else
            {
                XmlElement element = parent.OwnerDocument.CreateElement(localName);
                parent.AppendChild(element);

                foreach (XmlAspectMember member in _Aspect)
                    member.WriteXml(value, element, formatProvider);
            }
        }

        protected override void ReadXml(object entity, System.Xml.XmlElement parent, IFormatProvider formatProvider, string localName, bool mandatory)
        {
            XmlElement element = parent[localName];
            if (element == null)
                return;

            object value = GetValue(entity);
            if (value == null)
            {
                value = _Constructor.Invoke(null);
                SetValue(entity, value);
            }
            foreach (XmlAspectMember member in _Aspect)
                member.ReadXml(value, element, formatProvider);
        }
    }
}
