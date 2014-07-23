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
    }
}
