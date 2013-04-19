using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Xml;


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
                this._XmlName = xa.Name;
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
                member = new XmlAspectMember(
                    acessor,
                    xa.XmlType,
                    xa.Name,
                    string.IsNullOrEmpty(xa.Culture) ?
                    null :
                    System.Globalization.CultureInfo.GetCultureInfo(xa.Culture));

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

        private void ReadXmlCollection(object entity, XmlElement element)
        {
            ICollection<object> collection = (ICollection<object>)entity;

            Type[] genericTypes = this.DataType.GetGenericArguments();
            if (genericTypes == null || genericTypes.Length != 1)
                throw new ArgumentException("Collections need to be typed for this xml serialization");

            ConstructorInfo constructor = genericTypes[0].GetConstructor(Type.EmptyTypes);
            if (constructor == null)
                throw new ArgumentException("The collection members must have a default constructor");

            XmlAspect innerAspect = GetInstance(genericTypes[0]);


            foreach (XmlElement child in element.GetElementsByTagName(innerAspect._XmlName))
            {
                object entry = constructor.Invoke(null);
                innerAspect.ReadXml(entry, element);
                collection.Add(entry);
            }
        }
        private void WriteXmlCollection(object entity, XmlElement element)
        {
            ICollection<object> collection = (ICollection<object>)entity;

            Type[] genericTypes = this.DataType.GetGenericArguments();
            if (genericTypes == null || genericTypes.Length != 1)
                throw new ArgumentException("Collections need to be typed for this xml serialization");

            if (collection != null)
            {
                XmlAspect innerAspect = GetInstance(genericTypes[0]);

                foreach (object child in collection)
                {
                    innerAspect.WriteXml(child, element);
                }
            }
        }

        private void ReadXmlMembers(object entity, XmlElement element)
        {
            foreach (XmlAspectMember member in this)
            {
                switch (member.XmlType)
                {
                    case XmlType.Attribute:
                        member.SetValue(
                            entity,
                            element.GetAttribute(member.XmlName),
                            member.Culture == null ? _Culture : member.Culture);
                        break;
                    case XmlType.Element:

                        XmlElement child = element[member.XmlName];
                        if (child == null)
                            member.SetValue(entity, null);

                        else
                        {
                            //Parse a primitive type
                            if (member.DataType.IsPrimitive)
                            {
                                member.SetValue(
                                    entity,
                                    child.InnerText,
                                    member.Culture == null ? _Culture : member.Culture);

                            }
                            //Locate the schema and use it
                            else
                            {
                                object instance = Activator.CreateInstance(member.DataType);
                                XmlAspect.GetInstance(member.DataType).ReadXml(instance, child);
                                member.SetValue(
                                    entity,
                                    instance);
                            }
                        }
                        break;
                    default:
                        throw new ArgumentException("Unknown xml type " + member.XmlType);
                }
            }
        }
        private void WriteXmlMembers(object entity, XmlElement element)
        {
            foreach (XmlAspectMember member in this)
            {
                switch (member.XmlType)
                {
                    case XmlType.Attribute:
                        element.SetAttribute(
                            member.XmlName,
                            string.Format(
                            member.Culture == null ? _Culture : member.Culture,
                            "{0}",
                            member.GetValue(entity)));

                        break;
                    case XmlType.Element:

                        //Write a primitive type
                        if (member.DataType.IsPrimitive)
                        {
                            XmlElement childElement = element.OwnerDocument.CreateElement(member.XmlName);
                            childElement.AppendChild(
                                element.OwnerDocument.CreateTextNode(
                                    string.Format(
                                        member.Culture == null ? _Culture : member.Culture,
                                        "{0}",
                                        member.GetValue(entity))));

                        }
                        //Locate the schema and use it
                        else
                        {

                            XmlAspect.GetInstance(member.DataType).WriteXml(
                                member.GetValue(entity),
                                element);
                        }
                        break;
                    default:
                        throw new ArgumentException("Unknown xml type " + member.XmlType);
                }
            }
        }

        public void ReadXml(object entity, XmlElement element)
        {
            if (_XmlName.Equals(element.Name, StringComparison.OrdinalIgnoreCase))
            {
                //Is this a collection object?
                if (typeof(System.Collections.ICollection).IsAssignableFrom(this.DataType))
                {
                    ReadXmlCollection(entity, element);
                }
                else
                {
                    ReadXmlMembers(entity, element);
                }
            }
            else
                throw new ArgumentException("Expected element with name " + _XmlName + " and got " + element.Name);
        }

        public void WriteXml(object entity, XmlElement parent)
        {
            XmlElement element = parent.OwnerDocument.CreateElement(this._XmlName);
            parent.AppendChild(element);

            //Is this a collection object?
            if (typeof(System.Collections.ICollection).IsAssignableFrom(this.DataType))
            {
                WriteXmlCollection(entity, element);
            }
            else
            {
                WriteXmlMembers(entity, element);
            }
        }

        public object ReadXml(XmlElement element)
        {
            object entity = Activator.CreateInstance(this.DataType);
            ReadXml(entity, element);
            return entity;
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
