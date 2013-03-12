using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    public class HtmlAttributeCollection
        : LinkedList<HtmlAttribute>
    {
        public HtmlAttributeCollection AddLast(string name, string value)
        {
            base.AddLast(new HtmlAttribute(name, value));
            return this;
        }

        public HtmlAttributeCollection AddFirst(string name, string value)
        {
            base.AddFirst(new HtmlAttribute(name, value));
            return this;
        }
    }
}
