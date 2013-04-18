using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
