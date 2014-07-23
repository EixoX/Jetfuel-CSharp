using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Xml;
using EixoX;
using EixoX.Adapters;
using System.Collections;

namespace EixoX.Xml
{
    public class XmlAspect
        : AbstractAspect<XmlAspectMember>
    {

        private readonly string _XmlName;
        private readonly CultureInfo _Culture;

        protected XmlAspect(Type dataType)
            : base(dataType)
        {
            XmlAttribute xa = GetAttribute<XmlAttribute>(true);
            if (xa == null)
            {
                this._XmlName = dataType.Name;
                this._Culture = CultureInfo.InvariantCulture;
            }
            else
            {
                this._XmlName = string.IsNullOrEmpty(xa.Name) ? dataType.Name : xa.Name;
                this._Culture =
                    string.IsNullOrEmpty(xa.Culture) ?
                    CultureInfo.InvariantCulture :
                    CultureInfo.GetCultureInfo(xa.Culture);
            }
        }

        protected override bool CreateAspectFor(ClassAcessor acessor, out XmlAspectMember member)
        {
            XmlAttribute xa = acessor.GetAttribute<XmlAttribute>(true);
            if (xa == null)
            {
                member = null;
                return false;
            }
            else
            {
                SimpleAdapter adapter = SimpleAdapters.CreateInstance(
                    acessor.DataType,
                    xa.FormatString,
                    string.IsNullOrEmpty(xa.Culture) ? null :
                    CultureInfo.GetCultureInfo(xa.Culture)
                    );

                if (adapter != null)
                {
                    switch (xa.XmlType)
                    {
                        case XmlType.Attribute:
                            member = new XmlAspectMemberAttribute(
                                acessor,
                                string.IsNullOrEmpty(xa.Name) ? acessor.Name : xa.Name,
                                xa.IsMandatory,
                                adapter);
                            return true;

                        case XmlType.CDATA:
                            member = new XmlAspectMemberCDATA(
                                acessor,
                                string.IsNullOrEmpty(xa.Name) ? acessor.Name : xa.Name,
                                xa.IsMandatory,
                                adapter);
                            return true;

                        case XmlType.Element:
                            member = new XmlAspectMemberText(
                                acessor,
                                string.IsNullOrEmpty(xa.Name) ? acessor.Name : xa.Name,
                                xa.IsMandatory,
                                adapter);
                            return true;

                        default:
                            throw new NotImplementedException("Unable to serialize primitive type with " + xa.XmlType);

                    }
                }
                else if (typeof(IList).IsAssignableFrom(acessor.DataType))
                {
                    member = new XmlAspectMemberList(
                        acessor,
                        string.IsNullOrEmpty(xa.Name) ? acessor.Name : xa.Name,
                        xa.IsMandatory);
                    return true;
                }
                else
                {
                    member = new XmlAspectMemberComposite(
                        acessor,
                        string.IsNullOrEmpty(xa.Name) ? acessor.Name : xa.Name,
                        xa.IsMandatory,
                        GetInstance(acessor.DataType),
                        acessor.DataType.GetConstructor(Type.EmptyTypes));
                }

                return true;
            }

        }


        private static Dictionary<Type, XmlAspect> _Instances;

        public static XmlAspect GetInstance(Type dataType)
        {
            XmlAspect aspect;

            if (_Instances == null)
            {
                _Instances = new Dictionary<Type, XmlAspect>();
                aspect = new XmlAspect(dataType);
                _Instances.Add(dataType, aspect);
            }
            else
            {
                if (!_Instances.TryGetValue(dataType, out aspect))
                {
                    aspect = new XmlAspect(dataType);
                    _Instances.Add(dataType, aspect);
                }
            }

            return aspect;
        }

        public string XmlName
        {
            get { return this._XmlName; }
        }

        public CultureInfo Culture
        {
            get { return this._Culture; }
        }

        public void ReadXml(object entity, XmlElement element)
        {
            ReadXml(entity, element, _Culture);
        }

        public void ReadXml(object entity, XmlElement element, IFormatProvider formatProvider)
        {
            if (_XmlName.Equals(element.LocalName, StringComparison.OrdinalIgnoreCase))
            {
                foreach (XmlAspectMember xam in this)
                    xam.ReadXml(entity, element, formatProvider);
            }
            else
                throw new ArgumentException("Expected element with name " + _XmlName + " and got " + element.LocalName);
        }

        public object ReadXml(XmlElement element)
        {
            return ReadXml(element, _Culture);
        }
        public object ReadXml(XmlElement element, IFormatProvider formatProvider)
        {
            object entity = Activator.CreateInstance(this.DataType);
            ReadXml(entity, element, formatProvider);
            return entity;
        }

        public object ReadXml(XmlDocument document)
        {
            return ReadXml(document, _Culture);
        }
        public object ReadXml(XmlDocument document, IFormatProvider formatProvider)
        {
            return ReadXml(document.DocumentElement, formatProvider);
        }

        public void WriteXml(object entity, XmlElement parent)
        {
            WriteXml(entity, parent, _Culture);
        }
        public void WriteXml(object entity, XmlElement parent, IFormatProvider formatProvider)
        {
            XmlElement element = parent.OwnerDocument.CreateElement(this._XmlName);
            parent.AppendChild(element);
            foreach (XmlAspectMember xam in this)
                xam.WriteXml(entity, element, formatProvider);
        }

        public XmlDocument WriteXml(object entity)
        {
            return WriteXml(entity, _Culture);
        }
        public XmlDocument WriteXml(object entity, IFormatProvider formatProvider)
        {
            XmlDocument document = new XmlDocument();
            XmlElement element = document.CreateElement(this.XmlName);
            document.AppendChild(element);
            foreach (XmlAspectMember xam in this)
                xam.WriteXml(entity, element, formatProvider);
            return document;
        }


    }


    public class XmlAspect<T> : XmlAspect
    {
        private XmlAspect() : base(typeof(T)) { }

        private static XmlAspect<T> _Instance;

        public static XmlAspect<T> Instance
        {
            get { return _Instance ?? (_Instance = new XmlAspect<T>()); }
        }
    }


}
