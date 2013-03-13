using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    /// <summary>
    /// Attribute used to generate the field's html input 
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class HtmlInputAttribute : Attribute
    {
        private readonly string _fieldset;
        private readonly HtmlInputType _inputType;
        private readonly HtmlInputOptionSource _OptionSource;
        private bool _inferType = false;

        public HtmlInputAttribute()
        {
            this._inferType = true;
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

        /// <summary>
        /// Checks if the framework will try to infer the type
        /// </summary>
        public bool InferType
        {
            get { return this._inferType; }
        }

        public string Fieldset
        {
            get { return this._fieldset; }
        }

        /// <summary>
        /// The HTML input type (checkbox, text, textarea, etc)
        /// </summary>
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
