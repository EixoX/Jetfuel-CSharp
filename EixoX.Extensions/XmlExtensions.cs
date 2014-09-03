using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Xml
{
    public static class XmlExtensions
    {
        public static IEnumerable<XmlElement> ChildElements(this XmlElement element)
        {
            foreach (XmlNode child in element.ChildNodes)
                if (child is XmlElement)
                    yield return child as XmlElement;
        }



        public static XmlElement AppendSimple(this XmlElement parent, string localName, string text)
        {
            XmlElement child = parent.OwnerDocument.CreateElement(localName);
            child.AppendChild(parent.OwnerDocument.CreateTextNode(text));
            parent.AppendChild(child);
            return parent;
        }

        public static XmlElement AppendSimple(this XmlElement element, string localName, object value, IFormatProvider formatProvider)
        {
            return AppendSimple(element, localName, string.Format(formatProvider, "{0}", value));
        }

        public static XmlElement AppendSimple(this XmlElement element, string localName, object value)
        {
            return AppendSimple(element, localName, value, System.Globalization.CultureInfo.InvariantCulture);
        }

        public static string GetChildText(this XmlElement element, string localName)
        {
            XmlElement child = element[localName];
            return child == null ? null : child.InnerText;
        }

        public static T GetChildValue<T>(this XmlElement element, string localName, IFormatProvider formatProvider)
        {
            return (T)Convert.ChangeType(GetChildText(element, localName), typeof(T), formatProvider);
        }

        public static T GetChildValue<T>(this XmlElement element, string localName)
        {
            return GetChildValue<T>(element, localName, System.Globalization.CultureInfo.InvariantCulture);
        }

        public static TimeSpan GetChildTimeSpan(this XmlElement element, string localName)
        {
            string childText = GetChildText(element, localName);
            return string.IsNullOrEmpty(childText) ? TimeSpan.Zero : TimeSpan.Parse(childText);
        }

        public static XmlElement AppendElement(this XmlElement element, string localName)
        {
            XmlElement child = element.OwnerDocument.CreateElement(localName);
            element.AppendChild(child);
            return child;
        }
    }
}
