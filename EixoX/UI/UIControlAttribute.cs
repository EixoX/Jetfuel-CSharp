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
        private readonly UIControlChoices _ControlChoices;

        public UIControlAttribute(string defaultLabel, string defaultHint, Type choiceSource)
        {
            this._DefaultLabel = defaultLabel;
            this._DefaultHint = defaultHint;
            this._ControlChoices =
                choiceSource == null ? null :
                (choiceSource.IsEnum ?
                (UIControlChoices)new UIControlEnumChoices(choiceSource) :
                new UIControlClassStorageChoices(choiceSource));
        }

        public UIControlAttribute(string defaultLabel, string defaultHint)
            : this(defaultLabel, defaultHint, null) { }
<<<<<<< HEAD
        public UIControlAttribute(string defaultLabel, Type classStorageType)
            : this(defaultLabel, null, classStorageType) { }
        public UIControlAttribute(string defaultLabel)
            : this(defaultLabel, null, null) { }
        public UIControlAttribute(Type classStorageType)
            : this(null, null, classStorageType) { }
        
        public UIControlAttribute() { }
=======

        public UIControlAttribute(string defaultLabel, Type choiceSource)
            : this(defaultLabel, null, choiceSource) { }

        public UIControlAttribute(string defaultLabel)
            : this(defaultLabel, null, null) { }
>>>>>>> 55ece08a0808c1e079fc6f101c68aee85f6e6e8e

        public UIControlAttribute()
            : this(null, null, null) { }

        public string DefaultLabel { get { return this._DefaultLabel; } }
        public string DefaultHint { get { return this._DefaultHint; } }
        public UIControlChoices ControlChoices { get { return this._ControlChoices; } }

        public IEnumerable<KeyValuePair<object, object>> GetChoices()
        {
            return _ControlChoices == null ? null : _ControlChoices.GetChoices();
        }
    }
}
