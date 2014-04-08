using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    public class BootstrapControlFactory
        : UI.UIReflectionFactory
    {
        private static Dictionary<string, BootstrapControlFactory> _Instances;
        
        private BootstrapControlFactory(string controlPrefix) :
            base("EixoX.Html.Controls", "Bootstrap" + controlPrefix) { }

        public static BootstrapControlFactory GetInstance(string controlPrefix)
        {
            if (_Instances == null)
                _Instances = new Dictionary<string, BootstrapControlFactory>();

            if (!_Instances.ContainsKey(controlPrefix))
                _Instances.Add(controlPrefix, new BootstrapControlFactory(controlPrefix));

            return _Instances[controlPrefix];
        }
    }

}
