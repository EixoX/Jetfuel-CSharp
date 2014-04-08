using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    /// <summary>
    /// Represents an html node collection.
    /// </summary>
    [Serializable]
    public class HtmlNodeCollection
        : LinkedList<HtmlNode>
    {
        /// <summary>
        /// Constructs an empty node collection.
        /// </summary>
        public HtmlNodeCollection() { }
        /// <summary>
        /// Constructs a node collection.
        /// </summary>
        /// <param name="collection">The enumeration of nodes to base on.</param>
        public HtmlNodeCollection(IEnumerable<HtmlNode> collection) : base(collection) { }

        /// <summary>
        /// Gets the nodes with a specific tag name.
        /// </summary>
        /// <param name="tagName">The tag name to look for.</param>
        /// <returns>An enumeration of nodes with a given tag name.</returns>
        public IEnumerable<HtmlNode> WithTagName(string tagName)
        {
            if (!string.IsNullOrEmpty(tagName))
                foreach (HtmlNode node in this)
                    if (tagName.Equals(node.TagName, StringComparison.OrdinalIgnoreCase))
                        yield return node;
        }

        /// <summary>
        /// Gets the nodes with specific attribute.
        /// </summary>
        /// <param name="attributeName">The name of the attribute to look for.</param>
        /// <param name="attributeValue">The vaulue of the attribute to look for.</param>
        /// <returns>An enumeration of nodes with a given attribute.</returns>
        public IEnumerable<HtmlNode> WithAttribute(string attributeName, object attributeValue)
        {
            if (!string.IsNullOrEmpty(attributeName))
                foreach (HtmlNode node in this)
                    if (node.Attributes != null)
                        foreach (HtmlAttribute attribute in node.Attributes)
                            if (
                                attributeName.Equals(attribute.Name, StringComparison.OrdinalIgnoreCase) &&
                                attributeValue.Equals(attribute.Value))
                                yield return node;
        }

        /// <summary>
        /// Gets a specific node by it's id attribute.
        /// </summary>
        /// <param name="id">The value of the id attribute.</param>
        /// <returns>The node with a given id attribute.</returns>
        public HtmlNode WithId(string id)
        {
            using (IEnumerator<HtmlNode> e = WithAttribute("id", id).GetEnumerator())
            {
                return e.MoveNext() ? e.Current : null;
            }
        }
    }
}
