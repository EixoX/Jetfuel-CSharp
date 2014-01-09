using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    /// <summary>
    /// Represents a standalone html element.
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class HtmlStandalone : Attribute, HtmlNode
    {
        private readonly string _TagName;
        private readonly HtmlAttributeCollection _Attributes = new HtmlAttributeCollection();
        
        /// <summary>
        /// Constructs an standalone html element.
        /// </summary>
        /// <param name="tagName">The tagname of the element.</param>
        /// <param name="attributes">The attribute list.</param>
        public HtmlStandalone(string tagName, params HtmlAttribute[] attributes)
        {
            this._TagName = tagName;
            foreach (HtmlAttribute att in attributes)
                _Attributes.AddLast(att);
        }

        /// <summary>
        /// Gets the tag name of the element.
        /// </summary>
        public string TagName
        {
            get { return this._TagName; }
        }

        /// <summary>
        /// Gets the attribute collection for the element.
        /// </summary>
        public HtmlAttributeCollection Attributes
        {
            get { return this._Attributes; }
        }

        /// <summary>
        /// Gets the null value of the text.
        /// </summary>
        public string Text
        {
            get { return null; }
        }


        /// <summary>
        /// Gets the true indicator that this is standalone.
        /// </summary>
        public bool IsStandalone
        {
            get { return true; }
        }

        /// <summary>
        /// Gets the null collection of children.
        /// </summary>
        public HtmlNodeCollection Children
        {
            get { return null; }
        }

        /// <summary>
        /// Writes the standalone element to a text writer.
        /// </summary>
        /// <param name="writer">The text writer to write to.</param>
        public void Write(System.IO.TextWriter writer)
        {
            writer.Write("<");
            writer.Write(_TagName);
            _Attributes.Write(writer);
            writer.Write(" />");
        }
    }
}
