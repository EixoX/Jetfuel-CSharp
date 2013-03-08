using System;
using System.Collections.Generic;
using System.Text;
/*
 * First Author: Rodrigo Portela (rodrigo.portela@gmail.com) in 2013-01-01
 */
namespace EixoX.Collections
{
    /// <summary>
    /// Represents the node of a tree that's also a tree.
    /// </summary>
    /// <typeparam name="T">The type of class stored on the node.</typeparam>
    public class TreeNode<T>
        : Tree<T>
    {
        private T _Value;

        /// <summary>
        /// Constructs a new tree node.
        /// </summary>
        /// <param name="value">The value to store.</param>
        public TreeNode(T value)
        {
            this._Value = value;
        }

        /// <summary>
        /// Constructs a new tree node.
        /// </summary>
        /// <param name="value">The value to store on it.</param>
        /// <param name="children">The enumeration of child node values.</param>
        public TreeNode(T value, IEnumerable<T> children)
            : base(children)
        {
            this._Value = value;
        }

        /// <summary>
        /// Gets the value of the node.
        /// </summary>
        public T Value
        {
            get { return this._Value; }
            set { this._Value = value; }
        }
    }
}
