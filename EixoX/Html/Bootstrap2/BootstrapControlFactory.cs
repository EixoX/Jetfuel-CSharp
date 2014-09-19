using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    public class BootstrapControlFactory
        : UI.UIReflectionFactory
    {
        private static BootstrapControlFactory _Instance;
        private BootstrapControlFactory() :
            base("EixoX.Html.Controls", "Bootstrap") { }
        public static BootstrapControlFactory Instance
        {
            get { return _Instance ?? (_Instance = new BootstrapControlFactory()); }
        }
    }

}
