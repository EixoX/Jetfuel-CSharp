using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    public struct HtmlAttribute
    {
        public string Name;
        public object Value;

        public HtmlAttribute(string name, object value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}
