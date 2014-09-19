using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    /// <summary>
    /// Represents an attribute collection.
    /// </summary>
    public class HtmlAttributeCollection
        : LinkedList<HtmlAttribute>
    {
        /// <summary>
        /// Constructs an attribute collection.
        /// </summary>
        public HtmlAttributeCollection() { }
        /// <summary>
        /// Constructs an attribute collection.
        /// </summary>
        /// <param name="collection">The collection to base on .</param>
        public HtmlAttributeCollection(IEnumerable<HtmlAttribute> collection) : base(collection) { }

        public HtmlAttributeCollection(params HtmlAttribute[] attributes) : base(attributes) { }

        /// <summary>
        /// Adds an attribute to the end.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        /// <returns>This collection.</returns>
        public HtmlAttributeCollection AddLast(string name, string value)
        {
            base.AddLast(new HtmlAttribute(name, value));
            return this;
        }

        /// <summary>
        /// Adds an attribute to the begining.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value ot the attribute.</param>
        /// <returns>This collection.</returns>
        public HtmlAttributeCollection AddFirst(string name, string value)
        {
            base.AddFirst(new HtmlAttribute(name, value));
            return this;
        }

        /// <summary>
        /// Gets or sets an attribute value. It's added last if not already there.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <returns>The value of the attribute.</returns>
        public object this[string name]
        {
            get
            {
                for (LinkedListNode<HtmlAttribute> node = this.First; node != null; node = node.Next)
                    if (name.Equals(node.Value.Name, StringComparison.OrdinalIgnoreCase))
                        return node.Value.Value;

                return null;
            }
            set
            {
                for (LinkedListNode<HtmlAttribute> node = this.First; node != null; node = node.Next)
                    if (name.Equals(node.Value.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        node.Value = new HtmlAttribute(name, value);
                        break;
                    }

                base.AddLast(new HtmlAttribute(name, value));
            }
        }

        /// <summary>
        /// Writes the attribute collection to a text writer.
        /// </summary>
        /// <param name="writer">The text writer to write on.</param>
        public void Write(System.IO.TextWriter writer)
        {
            for (LinkedListNode<HtmlAttribute> node = this.First; node != null; node = node.Next)
            {
                writer.Write(' ');
                node.Value.Write(writer);
            }
        }
    }
}
