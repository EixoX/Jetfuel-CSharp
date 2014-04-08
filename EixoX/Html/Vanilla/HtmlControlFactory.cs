using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    public class HtmlControlFactory
        : UI.UIReflectionFactory
    {
        private static HtmlControlFactory _Instance;
        private HtmlControlFactory() :
            base("EixoX.Html.Controls", "Html") { }
        public static HtmlControlFactory Instance
        {
            get { return _Instance ?? (_Instance = new HtmlControlFactory()); }
        }
    }

}
