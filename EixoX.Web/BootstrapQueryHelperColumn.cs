using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EixoX.Web
{
    public class BootstrapQueryHelperColumn
    {
        public string Name { get; set; }
        public string Caption { get; set; }
        public string Filter { get; set; }
        public Formatters.Formatter Formatter { get; set; }
        public bool IsHtmlRaw { get; set; }
        public string CssStyle { get; set; }
        public string CssClass { get; set; }

        public BootstrapQueryHelperColumn SetCssStyle(string value)
        {
            this.CssStyle = value;
            return this;
        }

        public BootstrapQueryHelperColumn SetCssClass(string value)
        {
            this.CssClass = value;
            return this;
        }
    }
}
