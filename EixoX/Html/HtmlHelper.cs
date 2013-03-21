using EixoX.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    /// <summary>
    /// A helper class for html items.
    /// </summary>
    public static class HtmlHelper
    {
        /// <summary>
        /// Formats the input as an html text. (Fast Format, no char replacements).
        /// </summary>
        /// <param name="input">The value to format.</param>
        /// <returns>A formatted string.</returns>
        public static string HtmlFormat(object input)
        {
            if (input == null)
                return "";
            else
                return input.ToString()
                    .Replace("&", "&amp;")
                    .Replace("<", "&lt;")
                    .Replace(">", "&gt;")
                    .Replace("\"", "&quot;")
                    .Replace("'", "&apos;");
        }

        public static void WriteHtml<T>(EixoX.Collections.Tree<T> tree, System.IO.TextWriter writer)
        {
            HtmlComposite ulRoot = new HtmlComposite("ul");

            foreach (TreeNode<T> node in tree)
                ulRoot.Children.AddLast(GenerateHtml<T>(node));

            ulRoot.Write(writer);
        }

        private static HtmlComposite GenerateHtml<T>(EixoX.Collections.TreeNode<T> treeNode)
        {
            HtmlComposite li = new HtmlComposite("li");
            li.Children.AddLast(new HtmlSimple("span", treeNode.Value.ToString()));

            if (treeNode.Count > 0)
            {
                HtmlComposite ul = new HtmlComposite("ul");
                foreach (TreeNode<T> child in treeNode)
                    ul.Children.AddLast(GenerateHtml<T>(child));
                
                li.Children.AddLast(ul);
            }

            return li;
        }

        /// <summary>
        /// Formats the input as a value to be between double quotes. (Just changes the double quotes).
        /// </summary>
        /// <param name="input">The value to format.</param>
        /// <returns>A formatted string.</returns>
        public static string HtmlDoubleQuoted(object input)
        {
            if (input == null)
                return "";
            else
                return input.ToString().Replace("\"", "&quot;");
        }

        /// <summary>
        /// Assserts that the given name is a valid html name. (Only checks general expressions, not real valid tag names).
        /// </summary>
        /// <param name="name">The name to assert valid.</param>
        public static void AssertValidName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            else if (name.Contains("<"))
                throw new ArgumentException("Invalid name: " + name);
            else if (name.Contains(">"))
                throw new ArgumentException("Invalid name: " + name);
            else if (name.Contains(" ") || name.Contains("\t") || name.Contains("\r") || name.Contains("\n"))
                throw new ArgumentException("Invalid name: " + name);
        }
    }
}
