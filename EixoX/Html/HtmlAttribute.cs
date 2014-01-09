using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    /// <summary>
    /// Represents an HTML Attribute.
    /// </summary>
    public struct HtmlAttribute
    {
        private readonly string _Name;
        private readonly object _Value;

        /// <summary>
        /// Constructs an html attribute.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        public HtmlAttribute(string name, object value)
        {
            HtmlHelper.AssertValidName(name);
            this._Name = name;
            this._Value = value;
        }

        /// <summary>
        /// Gets the name of the attribute.
        /// </summary>
        public string Name { get { return this._Name; } }
        /// <summary>
        /// Gets the value of the attribute.
        /// </summary>
        public object Value { get { return this._Value; } }

        /// <summary>
        /// Writes the attribute to a text writer.
        /// </summary>
        /// <param name="writer">The text writer to write to.</param>
        public void Write(System.IO.TextWriter writer)
        {
            writer.Write(_Name);
            writer.Write("=\"");
            writer.Write(HtmlHelper.HtmlDoubleQuoted(_Value));
            writer.Write("\"");
        }
    }
}
