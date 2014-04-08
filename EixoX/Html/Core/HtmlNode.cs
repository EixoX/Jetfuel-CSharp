using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    /// <summary>
    /// Represents an html node.
    /// </summary>
    public interface HtmlNode
    {
        /// <summary>
        /// Gets the tagname of the node.
        /// </summary>
        string TagName { get; }
        /// <summary>
        /// Gets the attribute collection or null if none is present.
        /// </summary>
        HtmlAttributeCollection Attributes { get; }
        /// <summary>
        /// Gets the children of the element or null.
        /// </summary>
        HtmlNodeCollection Children { get; }
        /// <summary>
        /// Gets the text of the element if present.
        /// </summary>
        string Text { get; }
        /// <summary>
        /// Indicates that this is a standalone element.
        /// </summary>
        bool IsStandalone { get; }
        /// <summary>
        /// Writes the element o a text writer.
        /// </summary>
        /// <param name="writer">The text writer to write to.</param>
        void Write(System.IO.TextWriter writer);
        
    }
}
