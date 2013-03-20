using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EixoX.UI
{
    public class UIControlClassStorageChoices : UIControlChoices
    {
        private readonly Type _ClassStorageType;
        private readonly object _ClassStorageInstance;
        private readonly MethodInfo _ClassStorageSelectMethod;

        public Type ClassStorageType { get { return this._ClassStorageType; } }
        public object ClassStorageInstance { get { return this._ClassStorageInstance; } }

        public UIControlClassStorageChoices(Type classStorageType)
        {
            this._ClassStorageType = classStorageType;
            this._ClassStorageInstance = GetClassStorageInstace(classStorageType);
            this._ClassStorageSelectMethod = classStorageType.GetMethod("Select");
        }

        private object GetClassStorageInstace(Type classStorageType)
        {
            // locates the default constructor.
            ConstructorInfo defaultConstructor = classStorageType.GetConstructor(Type.EmptyTypes);
            if (defaultConstructor != null)
                return defaultConstructor.Invoke(null);

            // locates the usual "Instance" property for singletons
            PropertyInfo instanceProperty = classStorageType.GetProperty("Instance");
            if (instanceProperty != null)
            {
                //makes sure that it's a static method
                MethodInfo propGetMethod = instanceProperty.GetGetMethod();
                if (propGetMethod.IsStatic)
                    return propGetMethod.Invoke(null, null);
            }

            //Throws the unable to instantiate exception.
            throw new ArgumentException("Unable to located the default constructor or a static property named 'Instance' for " + classStorageType);
        }

        public IEnumerable<KeyValuePair<object, object>> GetChoices()
        {
            if (_ClassStorageInstance == null || _ClassStorageSelectMethod == null)
                return null;
            else
            {
                object classSelect = _ClassStorageSelectMethod.Invoke(_ClassStorageInstance, null);
                MethodInfo toOptions = classSelect.GetType().GetMethod("ToOptions");
                return (IEnumerable<KeyValuePair<object, object>>)toOptions.Invoke(classSelect, null);
            }
        }
    }
}
