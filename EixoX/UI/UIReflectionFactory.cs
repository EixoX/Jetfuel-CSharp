using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EixoX.UI
{
    public class UIReflectionFactory
        : UIControlFactory
    {
        private string _NamespacePrefix;
        private string _ControlPrefix;

        public UIReflectionFactory(string namespacePrefix, string controlPrefix)
        {
            this._NamespacePrefix = namespacePrefix;
            this._ControlPrefix = controlPrefix;
        }

        public UIControl CreateControlFor(UIControlAttribute attribute)
        {
            string typeName = string.Concat(
                _NamespacePrefix,
                ".",
                attribute.GetType().Name.Replace("UI", _ControlPrefix));

            Type type = Type.GetType(typeName);

            ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);

            object instance = constructor.Invoke(null);

            return (UIControl)instance;
        }
    }
}
