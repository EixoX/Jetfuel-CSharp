using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Xml;
using EixoX;

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
                member = new XmlAspectMember(
                    acessor,
                    xa.XmlType,
                    string.IsNullOrEmpty(xa.Name) ? acessor.Name : xa.Name,
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


        public void ReadXml(object entity, XmlElement element)
        {
            if (_XmlName.Equals(element.Name, StringComparison.OrdinalIgnoreCase))
            {
                ExecuteReadXml(entity, element);
            }
            else
                throw new ArgumentException("Expected element with name " + _XmlName + " and got " + element.Name);
        }

        private void ExecuteWriteXml(object entity, XmlElement element)
        {
            foreach (XmlAspectMember member in this)
            {

                switch (member.XmlType)
                {
                    case XmlType.Attribute:
                        element.SetAttribute(
                            member.XmlName,
                            member.GetTextValue(entity, member.Culture == null ? _Culture : member.Culture));
                        break;
                    case XmlType.Element:
                        //is this a value type, a string or enum?
                        if (member.DataType.IsValueType || member.DataType.IsEnum || member.DataType == PrimitiveTypes.String)
                        {
                            XmlElement child = element.OwnerDocument.CreateElement(member.XmlName);
                            element.AppendChild(child);
                            child.AppendChild(
                                child.OwnerDocument.CreateTextNode(
                                member.GetTextValue(entity, member.Culture == null ? _Culture : member.Culture)));


                        }
                        //Is this a collection object?
                        else if (typeof(System.Collections.IList).IsAssignableFrom(member.DataType))
                        {
                            object memberValue = member.GetValue(entity);

                            if (memberValue != null)
                            {
                                Type[] genericTypes = member.DataType.GetGenericArguments();
                                if (genericTypes == null || genericTypes.Length != 1)
                                    throw new ArgumentException("Collections need to be typed for this xml serialization");

                                XmlAspect innerAspect = GetInstance(genericTypes[0]);

                                foreach (object childObject in ((System.Collections.IList)memberValue))
                                {
                                    XmlElement childElement = element.OwnerDocument.CreateElement(member.XmlName);
                                    element.AppendChild(childElement);
                                    innerAspect.ExecuteWriteXml(childObject, childElement);
                                }
                            }

                        } // a generic composite object
                        else
                        {

                            XmlElement childElement = element.OwnerDocument.CreateElement(member.XmlName);
                            element.AppendChild(childElement);

                            object memberValue =
                                member.GetValue(entity);

                            if (memberValue != null)
                            {
                                XmlAspect childAspect = XmlAspect.GetInstance(member.DataType);
                                childAspect.ExecuteWriteXml(memberValue, childElement);
                            }
                        }
                        break;
                    default:
                        throw new ArgumentException("Unknown xml type " + member.XmlType);
                }

            }
        }

        private void ExecuteReadXml(object entity, XmlElement element)
        {
            foreach (XmlAspectMember member in this)
            {
                try
                {
                    switch (member.XmlType)
                    {
                        case XmlType.Attribute:
                            member.SetParsedValue(
                                entity,
                                element.GetAttribute(member.XmlName),
                                member.Culture == null ? _Culture : member.Culture);

                            break;
                        case XmlType.Element:
                            //is this a value type, a string or enum?
                            if (member.DataType.IsValueType || member.DataType.IsEnum || member.DataType == PrimitiveTypes.String)
                            {
                                XmlElement child = element[member.XmlName];
                                if (child == null)
                                    break;

                                member.SetParsedValue(
                                    entity,
                                    child.InnerText,
                                    member.Culture == null ? _Culture : member.Culture);
                            }
                            //Is this a collection object?
                            else if (typeof(System.Collections.IList).IsAssignableFrom(member.DataType))
                            {
                                XmlNodeList children = element.GetElementsByTagName(member.XmlName);
                                if (children == null || children.Count == 0)
                                    break;

                                Type[] genericTypes = member.DataType.GetGenericArguments();
                                if (genericTypes == null || genericTypes.Length != 1)
                                    throw new ArgumentException("Collections need to be typed for this xml serialization");

                                ConstructorInfo constructor = genericTypes[0].GetConstructor(Type.EmptyTypes);
                                if (constructor == null)
                                    throw new ArgumentException("The collection members must have a default constructor");


                                XmlAspect innerAspect = GetInstance(genericTypes[0]);

                                System.Collections.IList memberValue = (System.Collections.IList)member.GetValue(entity);
                                if (memberValue == null)
                                {
                                    memberValue = (System.Collections.IList)Activator.CreateInstance(member.DataType);
                                    member.SetValue(entity, memberValue);
                                }

                                int count = children.Count;
                                for (int i = 0; i < count; i++)
                                {
                                    object memberInstance = constructor.Invoke(null);
                                    innerAspect.ExecuteReadXml(memberInstance, children[i] as XmlElement);
                                    memberValue.Add(memberInstance);
                                }

                            } // a generic composite object
                            else
                            {
                                XmlElement child = element[member.XmlName];
                                if (child == null)
                                    break;

                                object memberValue =
                                    member.GetValue(entity) ?? (memberValue = Activator.CreateInstance(member.DataType));

                                XmlAspect childAspect = XmlAspect.GetInstance(member.DataType);

                                childAspect.ExecuteReadXml(memberValue, child);
                                member.SetValue(entity, memberValue);
                            }
                            break;
                        default:
                            throw new ArgumentException("Unknown xml type " + member.XmlType);
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Pau no membro " + member.Name + " da classe " + this.FullName, ex);
                }
            }
        }

        public void WriteXml(object entity, XmlElement parent)
        {
            XmlElement element = parent.OwnerDocument.CreateElement(this._XmlName);
            parent.AppendChild(element);
            ExecuteWriteXml(entity, element);
        }

        public XmlDocument CreateXmlDocument(object entity)
        {
            XmlDocument document = new XmlDocument();
            XmlElement element = document.CreateElement(this.XmlName);
            document.AppendChild(element);
            ExecuteWriteXml(entity, element);
            return document;
        }

        public object ReadXml(XmlElement element)
        {
            object entity = Activator.CreateInstance(this.DataType);
            ReadXml(entity, element);
            return entity;
        }

        public object ReadXml(XmlDocument document)
        {
            return ReadXml(document.DocumentElement);
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
