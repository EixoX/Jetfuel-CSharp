using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    public struct HtmlInputOption
    {
        public object Key;
        public object Value;

        public HtmlInputOption(object key, object value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}
