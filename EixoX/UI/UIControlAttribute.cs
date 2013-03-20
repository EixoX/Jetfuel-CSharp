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
        public UIControlAttribute(string defaultLabel, Type choiceSource)
            : this(defaultLabel, null, choiceSource) { }
        public UIControlAttribute(string defaultLabel)
            : this(defaultLabel, null, null) { }
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
