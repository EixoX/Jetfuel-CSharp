using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EixoX.UI
{
    public class UIReflectionFactory
        : UIControlFactory
    {
        private readonly string _NamespacePrefix;
        private readonly string _ControlPrefix;
        private Assembly referenceAssembly;

        public UIReflectionFactory(string namespacePrefix, string controlPrefix)
        {
            this._NamespacePrefix = namespacePrefix;
            this._ControlPrefix = controlPrefix;
        }

        protected virtual Assembly GetAssembly()
        {
            return GetType().Assembly;
        }

        public UIControl CreateControlFor(UIControlAttribute attribute)
        {
            string typeName = string.Concat(
                _NamespacePrefix,
                ".",
                attribute.GetType().Name.Replace("UI", _ControlPrefix));

            if (referenceAssembly == null)
                referenceAssembly = GetAssembly();

            Type type = referenceAssembly.GetType(typeName);

            ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);

            object instance = constructor.Invoke(null);

            return (UIControl)instance;
        }
    }
}
