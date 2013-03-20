using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EixoX.UI
{
    [Serializable]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public abstract class UIControlAttribute : Attribute
    {

        private readonly string _DefaultLabel;
        private readonly string _DefaultHint;
        private readonly Type _ClassStorageType;
        private readonly object _ClassStorageInstance;
        private readonly MethodInfo _ClassStorageSelectMethod;

        private object GetClassStorageInstace(Type classStorageType)
        {
            //locates the default constructor.
            ConstructorInfo defaultConstructor = classStorageType.GetConstructor(Type.EmptyTypes);
            if (defaultConstructor != null)
                return defaultConstructor.Invoke(null);

            //locates the usual "Instance" property for singletons
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


        public IEnumerable<KeyValuePair<object, object>> GetControlOptions()
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

        public UIControlAttribute(string defaultLabel, string defaultHint, Type classStorageType)
        {
            this._DefaultLabel = defaultLabel;
            this._DefaultHint = defaultHint;
            this._ClassStorageType = classStorageType;
            this._ClassStorageInstance = classStorageType == null ? null : GetClassStorageInstace(classStorageType);
            this._ClassStorageSelectMethod = classStorageType == null ? null : classStorageType.GetMethod("Select");
        }

        public UIControlAttribute(string defaultLabel, string defaultHint)
            : this(defaultLabel, defaultHint, null) { }
        public UIControlAttribute(string defaultLabel, Type classStorageType)
            : this(defaultLabel, null, classStorageType) { }
        public UIControlAttribute(string defaultLabel)
            : this(defaultLabel, null, null) { }
        public UIControlAttribute(Type classStorageType)
            : this(null, null, classStorageType) { }
        
        public UIControlAttribute() { }


        public string DefaultLabel { get { return this._DefaultLabel; } }
        public string DefaultHint { get { return this._DefaultHint; } }
        public Type ClassStorageType { get { return this._ClassStorageType; } }
        public object ClassStorageInstance { get { return this._ClassStorageInstance; } }
    }
}
