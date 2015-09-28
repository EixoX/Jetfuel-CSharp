using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace EixoX.Helpers
{
    public static class XmlHelper
    {


        public static XmlElement GetFirstChild(XmlNode node, string tagName)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.NodeType == XmlNodeType.Element && tagName.Equals(child.LocalName, StringComparison.OrdinalIgnoreCase))
                    return (XmlElement)child;
            }
            return null;
        }


        public static XmlElement FollowPath(XmlNode source, params string[] tagNames)
        {
            XmlElement p = GetFirstChild(source, tagNames[0]);
            for (int i = 1; p != null && i < tagNames.Length; i++)
                p = GetFirstChild(p, tagNames[i]);
            return p;
        }


        public static string GetText(XmlElement source, string childName)
        {
            XmlElement child = GetFirstChild(source, childName);
            return child == null ? null : child.InnerText;
        }

        public static int GetInt32(XmlElement source, string childName, IFormatProvider formatProvider)
        {
            string txt = GetText(source, childName);
            return string.IsNullOrEmpty(txt) ? 0 : int.Parse(txt, formatProvider);
        }

        public static int GetInt32(XmlElement source, string childName)
        {
            string txt = GetText(source, childName);
            return string.IsNullOrEmpty(txt) ? 0 : int.Parse(txt);
        }

        public static long GetInt64(XmlElement source, string childName, IFormatProvider formatProvider)
        {
            string txt = GetText(source, childName);
            return string.IsNullOrEmpty(txt) ? 0L : long.Parse(txt, formatProvider);
        }

        public static long GetInt64(XmlElement source, string childName)
        {
            string txt = GetText(source, childName);
            return string.IsNullOrEmpty(txt) ? 0L : long.Parse(txt);
        }

        public static double GetDouble(XmlElement source, string childName, IFormatProvider formatProvider)
        {
            string txt = GetText(source, childName);
            return string.IsNullOrEmpty(txt) ? 0.0 : double.Parse(txt, formatProvider);
        }

        public static double GetDouble(XmlElement source, string childName)
        {
            string txt = GetText(source, childName);
            return string.IsNullOrEmpty(txt) ? 0.0 : double.Parse(txt);
        }

        public static decimal GetDecimal(XmlElement source, string childName, IFormatProvider formatProvider)
        {
            string txt = GetText(source, childName);
            return string.IsNullOrEmpty(txt) ? 0M : decimal.Parse(txt, formatProvider);
        }

        public static decimal GetDecimal(XmlElement source, string childName)
        {
            string txt = GetText(source, childName);
            return string.IsNullOrEmpty(txt) ? 0M : decimal.Parse(txt);
        }

        public static DateTime GetDateTime(XmlElement source, string childName, IFormatProvider formatProvider)
        {
            string txt = GetText(source, childName);
            return string.IsNullOrEmpty(txt) ? DateTime.MinValue : DateTime.Parse(txt, formatProvider);
        }

        public static DateTime GetDateTime(XmlElement source, string childName)
        {
            string txt = GetText(source, childName);
            return string.IsNullOrEmpty(txt) ? DateTime.MinValue : DateTime.Parse(childName);
        }

        public static IEnumerable<XmlElement> EnumerateElements(XmlElement source, string childName)
        {
            foreach (XmlNode child in source.ChildNodes)
            {
                if (child.NodeType == XmlNodeType.Element && childName.Equals(child.LocalName, StringComparison.OrdinalIgnoreCase))
                    yield return (XmlElement)child;
            }
        }
    }
}
