using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;

namespace EixoX.Xml
{
    public class XmlAspectMemberList
        : XmlAspectMember
    {
        private readonly XmlAspect _Aspect;
        private readonly ConstructorInfo _Constructor;

        public XmlAspectMemberList(ClassAcessor acessor, string localName, bool mandatory, XmlAspect aspect, ConstructorInfo constructor)
            : base(acessor, localName, mandatory)
        {
            this._Aspect = aspect;
            this._Constructor = constructor;
        }

        public XmlAspectMemberList(ClassAcessor acessor, string localName, bool mandatory)
            : base(acessor, localName, mandatory)
        {
            Type[] genericTypes = base.DataType.GetGenericArguments();
            if (genericTypes.Length != 1)
            {
                throw new ArgumentException("List serialization must be with IList of one generic type: " + DataType);
            }

            _Constructor = genericTypes[0].GetConstructor(Type.EmptyTypes);
            if (_Constructor == null)
            {
                throw new ArgumentException("List serialization requires that the components have a default constructor: " + genericTypes[0]);

            }

            this._Aspect = XmlAspect.GetInstance(genericTypes[0]);

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
                foreach (object obj in ((System.Collections.IList)value))
                {
                    XmlElement element = parent.OwnerDocument.CreateElement(localName);
                    parent.AppendChild(element);
                    foreach (XmlAspectMember xam in _Aspect)
                        xam.WriteXml(obj, element, formatProvider);
                }
            }
        }

        protected override void ReadXml(object entity, System.Xml.XmlElement parent, IFormatProvider formatProvider, string localName, bool mandatory)
        {
            XmlNodeList nodes = parent.GetElementsByTagName(localName);
            if (nodes.Count > 0)
            {
                IList list = (IList)GetValue(entity);
                if (list == null)
                {
                    list = (IList)Activator.CreateInstance(DataType);
                    SetValue(entity, list);
                }
                foreach (XmlElement element in nodes)
                {
                    object child = _Constructor.Invoke(null);
                    foreach (XmlAspectMember xam in _Aspect)
                        xam.ReadXml(child, element, formatProvider);
                    list.Add(child);
                }
            }
        }
    }
}
