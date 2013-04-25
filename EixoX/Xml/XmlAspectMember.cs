using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
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
        private readonly string _FormatString;
        private readonly bool _Mandatory;
        private readonly XmlAspect _InnerAspect;
        private readonly ConstructorInfo _InnerConstructor;
        private bool _List;
        private bool _Composite;

        public XmlAspectMember(ClassAcessor acessor, XmlType xmlType, string xmlName, CultureInfo culture, string formatString, bool mandatory)
            : base(acessor)
        {
            this._XmlName = xmlName;
            this._XmlType = xmlType;
            this._Culture = culture;
            this._FormatString = formatString;
            this._Mandatory = mandatory;

            if (typeof(System.Collections.IList).IsAssignableFrom(acessor.DataType))
            {
                Type[] genericTypes = acessor.DataType.GetGenericArguments();
                if (genericTypes == null || genericTypes.Length != 1)
                    throw new ArgumentException("Collections need to be typed for this xml serialization");

                this._InnerConstructor = genericTypes[0].GetConstructor(Type.EmptyTypes);
                if (this._InnerConstructor == null)
                    throw new ArgumentException("The collection members must have a default constructor");


                this._InnerAspect = XmlAspect.GetInstance(genericTypes[0]);
                this._List = true;

            }
            else if (
                !acessor.DataType.IsEnum &&
                !acessor.DataType.IsValueType &&
                acessor.DataType != PrimitiveTypes.String)
            {
                this._InnerAspect = XmlAspect.GetInstance(acessor.DataType);
                this._InnerConstructor = acessor.DataType.GetConstructor(Type.EmptyTypes);
                if (this._InnerConstructor == null)
                    throw new ArgumentException("The class members must have a default constructor");

                this._Composite = true;
            }

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

        public override string GetTextValue(object entity, IFormatProvider formatProvider)
        {
            return base.GetTextValue(entity, formatProvider, this._FormatString);
        }
        /// <summary>
        /// Indicates that the xml element is mandatory (it is always written, even if empty).
        /// </summary>
        public bool IsMandatory
        {
            get { return _Mandatory; }
        }

        /// <summary>
        /// Serializes the content of the member to an xml element.
        /// </summary>
        /// <param name="entity">The entity to read from.</param>
        /// <param name="parent">The xml element to write to.</param>
        /// <param name="defaultCulture">The default culture to use when formatting values.</param>
        public void WriteXml(object entity, XmlElement parent, CultureInfo defaultCulture)
        {
            switch (this._XmlType)
            {
                case Xml.XmlType.Attribute:

                    string attributeValue = GetTextValueOrEmpty(entity, _Culture == null ? defaultCulture : _Culture, _FormatString, _Mandatory);

                    if (_Mandatory || !string.IsNullOrEmpty(attributeValue))
                        parent.SetAttribute(_XmlName, attributeValue);
                    break;

                case Xml.XmlType.Element:
                    if (_List)
                    {
                        object elementContent = GetValue(entity);
                        if (elementContent != null)
                        {
                            foreach (object childObject in ((System.Collections.IList)elementContent))
                            {
                                XmlElement element = parent.OwnerDocument.CreateElement(_XmlName);
                                parent.AppendChild(element);

                                foreach (XmlAspectMember childAspect in _InnerAspect)
                                    childAspect.WriteXml(childObject, element, defaultCulture);
                            }
                        }

                    }
                    else if (_Composite)
                    {
                        object elementContent = GetValue(entity);

                        if (elementContent == null)
                        {
                            if (_Mandatory)
                            {
                                XmlElement element = parent.OwnerDocument.CreateElement(_XmlName);
                                parent.AppendChild(element);
                            }
                        }
                        else
                        {

                            XmlElement element = parent.OwnerDocument.CreateElement(_XmlName);
                            parent.AppendChild(element);
                            foreach (XmlAspectMember aspectMember in _InnerAspect)
                                aspectMember.WriteXml(elementContent, element, defaultCulture);
                        }
                    }
                    else
                    {
                        string elementText = GetTextValueOrEmpty
                            (entity, _Culture == null ? defaultCulture : _Culture, _FormatString, _Mandatory);
                        if (string.IsNullOrEmpty(elementText))
                        {
                            if (_Mandatory)
                            {
                                XmlElement element = parent.OwnerDocument.CreateElement(_XmlName);
                                parent.AppendChild(element);
                            }
                        }
                        else
                        {
                            XmlElement element = parent.OwnerDocument.CreateElement(_XmlName);
                            parent.AppendChild(element);

                            element.AppendChild(element.OwnerDocument.CreateTextNode(elementText));
                        }
                    }
                    break;
                default:
                    throw new NotImplementedException("Unknown xmltype " + _XmlType);
            }
        }

        /// <summary>
        /// Deserializes the content of the member from an xml element.
        /// </summary>
        /// <param name="entity">The entity to write to.</param>
        /// <param name="parent">The parent element to read from.</param>
        /// <param name="defaultCulture">The default culture to use when parsing.</param>
        public void ReadXml(object entity, XmlElement parent, CultureInfo defaultCulture)
        {
            string memeberText;
            switch (this._XmlType)
            {
                case Xml.XmlType.Attribute:

                    memeberText = parent.GetAttribute(_XmlName);

                    if (!string.IsNullOrEmpty(memeberText))
                        SetParsedValue(entity, memeberText, _Culture == null ? defaultCulture : _Culture);

                    break;
                case Xml.XmlType.Element:
                    if (_List)
                    {
                        System.Collections.IList list = (System.Collections.IList)GetValue(entity);
                        if (list == null)
                        {
                            list = (System.Collections.IList)Activator.CreateInstance(DataType);
                            SetValue(entity, list);
                        }
                        foreach (XmlElement element in parent.GetElementsByTagName(_XmlName))
                        {
                            object memberValue = _InnerConstructor.Invoke(null);
                            foreach (XmlAspectMember xam in _InnerAspect)
                                xam.ReadXml(memberValue, element, _Culture == null ? defaultCulture : _Culture);
                            list.Add(memberValue);
                        }

                    }
                    else if (_Composite)
                    {
                        XmlElement element = parent[_XmlName];
                        if (element != null)
                        {
                            object memberValue = _InnerConstructor.Invoke(null);
                            foreach (XmlAspectMember xam in _InnerAspect)
                                xam.ReadXml(memberValue, element, defaultCulture);
                            SetValue(entity, memberValue);
                        }

                    }
                    else
                    {
                        XmlElement element = parent[_XmlName];
                        if (element != null)
                        {
                            memeberText = element.InnerText;
                            if (!string.IsNullOrEmpty(memeberText))
                                SetParsedValue(entity, memeberText, _Culture == null ? defaultCulture : _Culture);
                        }
                    }
                    break;
                default:
                    throw new NotImplementedException("Unknown xmltype " + _XmlType);
            }
        }



        public string GetTextValueOrEmpty(object entity, IFormatProvider formatProvider, string formatString, bool mandatory)
        {
            object memberValue = base.GetValue(entity);

            if (!mandatory && ValidationHelper.IsNullOrEmpty(memberValue))
                return null;

            else if (DataType == PrimitiveTypes.String)
            {
                return (string)memberValue;
            }
            else if (DataType.IsEnum)
            {
                return string.Format(formatString, (int)memberValue);
            }
            else if (DataType == PrimitiveTypes.DateTime)
            {
                return ((DateTime)memberValue).ToString(formatString, formatProvider);
            }
            else if (DataType == PrimitiveTypes.TimeSpan)
            {
                return memberValue.ToString();
            }
            else if (DataType == PrimitiveTypes.Guid)
            {
                return ((Guid)memberValue) == Guid.Empty ? null : memberValue.ToString();
            }
            else
            {
                return string.Format(
                    formatProvider,
                    formatString,
                    memberValue);
            }
        }
    }
}
