using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    /// <summary>
    /// Represents a simple html element.
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class HtmlSimple : Attribute, HtmlNode
    {
        private readonly string _TagName;
        private readonly HtmlAttributeCollection _Attributes = new HtmlAttributeCollection();
        private object _Value;

        /// <summary>
        /// Constructs a simple html element.
        /// </summary>
        /// <param name="tagName">The tag name of the element.</param>
        /// <param name="value">The inner text or content of the element.</param>
        /// <param name="attributes">The element's attributes.</param>
        public HtmlSimple(string tagName, object value, params HtmlAttribute[] attributes)
        {
            this._TagName = tagName;
            this._Value = value;
            foreach (HtmlAttribute att in attributes)
                _Attributes.AddLast(att);
        }

        /// <summary>
        /// Gets the tagname of the element.
        /// </summary>
        public string TagName
        {
            get { return this._TagName; }
        }

        /// <summary>
        /// Gets the attribute colletion of the element.
        /// </summary>
        public HtmlAttributeCollection Attributes
        {
            get { return this._Attributes; }
        }

        /// <summary>
        /// Gets the text content of the element.
        /// </summary>
        public string Text
        {
            get { return _Value == null ? null : _Value.ToString(); }
        }

        /// <summary>
        /// Gets or sets the actual content of the element.
        /// </summary>
        public object Value
        {
            get { return this._Value; }
            set { this._Value = value; }
        }

        /// <summary>
        /// Indicates that this element is not standalone.
        /// </summary>
        public bool IsStandalone
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the null collection of children.
        /// </summary>
        public HtmlNodeCollection Children
        {
            get { return null; }
        }

        /// <summary>
        /// Writes the simple element to a text writer.
        /// </summary>
        /// <param name="writer">The text writer to write to.</param>
        public void Write(System.IO.TextWriter writer)
        {
            writer.Write("<");
            writer.Write(_TagName);
            _Attributes.Write(writer);
            writer.Write(">");
            writer.Write(HtmlHelper.HtmlFormat(_Value));
            writer.Write("</");
            writer.Write(_TagName);
            writer.Write(">");
        }
    }
}
