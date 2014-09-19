using EixoX.Collections;
using EixoX.Data;
using System;
using System.Collections;
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

        private static UIControlChoices GetControlChoices(Type choiceSource)
        {
            if (choiceSource == null)
                return null;
            else if (choiceSource.IsEnum)
                return new UIControlEnumChoices(choiceSource);
            else if (typeof(UIControlChoices).IsAssignableFrom(choiceSource))
                return (UIControlChoices)Activator.CreateInstance(choiceSource);
            else
                return new UIControlClassStorageChoices(choiceSource);
        }

        public UIControlAttribute(string defaultLabel, string defaultHint, Type choiceSource)
        {
            this._DefaultLabel = defaultLabel;
            this._DefaultHint = defaultHint;
            this._ControlChoices = GetControlChoices(choiceSource);
        }

        public UIControlAttribute(int[] array)
        {
            this._ControlChoices = new UIControlArrayChoices(array);
        }

        public UIControlAttribute(string defaultLabel, int[] array)
        {
            this._DefaultLabel = defaultLabel;
            this._ControlChoices = new UIControlArrayChoices(array);
        }

        public UIControlAttribute(int minInclusive, int maxExclusive)
        {
            int[] aux = new int[maxExclusive - minInclusive];
            int j = 0;
            for (int i = minInclusive; i < maxExclusive; i++)
                aux[j++] = i;

            this._ControlChoices = new UIControlArrayChoices(aux);
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
