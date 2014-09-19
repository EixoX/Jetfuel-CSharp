using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    public class Bootstrap3ControlFactory
        : UI.UIReflectionFactory
    {
        private static Bootstrap3ControlFactory _Instance;
        private Bootstrap3ControlFactory() :
            base("EixoX.Html.Controls", "Bootstrap3") { }
        public static Bootstrap3ControlFactory Instance
        {
            get { return _Instance ?? (_Instance = new Bootstrap3ControlFactory()); }
        }
    }

}
