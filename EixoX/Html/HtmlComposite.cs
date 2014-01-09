using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    /// <summary>
    /// Represents a composite html element.
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class HtmlComposite : Attribute, HtmlNode
    {
        private readonly string _TagName;
        private readonly HtmlAttributeCollection _Attributes = new HtmlAttributeCollection();
        private readonly HtmlNodeCollection _Children = new HtmlNodeCollection();

        /// <summary>
        /// Constructs a composite html element.
        /// </summary>
        /// <param name="tagName">The tag name.</param>
        /// <param name="attributes">The tag attributes.</param>
        public HtmlComposite(string tagName, params HtmlAttribute[] attributes)
        {
            this._TagName = tagName;
            foreach (HtmlAttribute att in attributes)
                _Attributes.AddLast(att);
        }

        /// <summary>
        /// Gets the tag name.
        /// </summary>
        public string TagName
        {
            get { return this._TagName; }
        }

        /// <summary>
        /// Gets the tag attributes.
        /// </summary>
        public HtmlAttributeCollection Attributes
        {
            get { return this._Attributes; }
        }

        /// <summary>
        /// Gets NULL as tag text.
        /// </summary>
        public string Text
        {
            get { return null; }
        }

        /// <summary>
        /// Gets a false indicator that the tag stands alone.
        /// </summary>
        public bool IsStandalone
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the children collection.
        /// </summary>
        public HtmlNodeCollection Children
        {
            get { return this._Children; }
        }

        /// <summary>
        /// Writes the tag to a text writer.
        /// </summary>
        /// <param name="writer">The text writer to write on.</param>
        public void Write(System.IO.TextWriter writer)
        {
            writer.Write("<");
            writer.Write(_TagName);
            _Attributes.Write(writer);
            writer.Write(">");
            foreach (HtmlNode child in _Children)
            {
                child.Write(writer);
            }
            writer.Write("</");
            writer.Write(this._TagName);
            writer.Write(">");
        }


    }
}
