using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    /// <summary>
    /// Represents an html option.
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class HtmlOption : HtmlSimple
    {
        private object _value;

        /// <summary>
        /// Constructs an html option.
        /// </summary>
        /// <param name="value">The value of the option.</param>
        /// <param name="text">The text of the option.</param>
        public HtmlOption(object value, object text)
            : base("option", text)
        {
            this._value = value;
            this.Attributes.AddLast("value", value == null ? "" : value.ToString());
        }

        /// <summary>
        /// Gets or set the key for the option.
        /// </summary>
        public object Key
        {
            get { return this._value; }
            set
            {
                this._value = value;
                this.Attributes["value"] = value;
            }
        }
    }
}
