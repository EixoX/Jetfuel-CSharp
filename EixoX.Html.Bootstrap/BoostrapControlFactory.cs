using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    public class BoostrapControlFactory
        : UI.UIReflectionFactory
    {
        private static BoostrapControlFactory _Instance;
        private BoostrapControlFactory() :
            base("EixoX.Html.Controls", "Boostrap") { }
        public static BoostrapControlFactory Instance
        {
            get { return _Instance ?? (_Instance = new BoostrapControlFactory()); }
        }
    }

}
