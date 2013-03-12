using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class HtmlInputAttribute : Attribute
    {
        private readonly string _fieldset;
        private readonly HtmlInputType _inputType;
        private readonly HtmlInputOptionSource _OptionSource;

        public HtmlInputAttribute()
        {
            this._inputType = HtmlInputType.Text;
        }

        public HtmlInputAttribute(HtmlInputType inputType)
        {
            this._inputType = inputType;
        }

        public HtmlInputAttribute(HtmlInputType inputType, Type optionSourceType)
        {
            this._inputType = inputType;
            this._OptionSource = (HtmlInputOptionSource)(optionSourceType.GetConstructor(Type.EmptyTypes).Invoke(null));
        }
        public HtmlInputAttribute(string fieldset, HtmlInputType inputType)
        {
            this._fieldset = fieldset;
            this._inputType = inputType;
        }

        public HtmlInputAttribute(string fieldset, HtmlInputType inputType, Type optionSourceType)
        {
            this._fieldset = fieldset;
            this._inputType = inputType;
            this._OptionSource = (HtmlInputOptionSource)(optionSourceType.GetConstructor(Type.EmptyTypes).Invoke(null));
        }

        public string Fieldset
        {
            get { return this._fieldset; }
        }

        public HtmlInputType InputType
        {
            get { return this._inputType; }
        }

        public HtmlInputOptionSource OptionSource
        {
            get { return this._OptionSource; }
        }
    }
}
