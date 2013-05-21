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




        public static HtmlComposite GenerateHtmlTreeNode<T>(EixoX.Collections.TreeNode<T> treeNode, string identityPrefix, ClassAcessor identityMember)
        {
            HtmlComposite li = new HtmlComposite("li");
            if (identityMember != null)
                li.Attributes.AddLast("id", identityPrefix + identityMember.GetValue(treeNode.Value));

            li.Children.AddLast(new HtmlSimple("span", treeNode.Value));

            if (treeNode.Count > 0)
                li.Children.AddLast(GenerateHtmlTree<T>(treeNode, identityPrefix, identityMember));

            return li;
        }

        public static HtmlComposite GenerateHtmlTree<T>(Tree<T> tree, string identityPrefix, ClassAcessor identityMember)
        {
            HtmlComposite ul = new HtmlComposite("ul");
            foreach (TreeNode<T> child in tree)
                ul.Children.AddLast(GenerateHtmlTreeNode<T>(child, identityPrefix, identityMember));
            return ul;
        }

        private static HtmlComposite GenerateHtmlTreeNode<T>(EixoX.Collections.TreeNode<T> treeNode)
        {
            return GenerateHtmlTreeNode<T>(treeNode, null, null);
        }

        public static HtmlComposite GenerateHtmlTree<T>(Tree<T> tree)
        {
            return GenerateHtmlTree<T>(tree, null, null);
        }

        public static HtmlComposite GenerateHtmlTreeWithId<T>(Tree<T> tree)
        {
            Data.DatabaseAspect<T> dbAspect = Data.DatabaseAspect<T>.Instance;

            return GenerateHtmlTree<T>(tree, dbAspect.IdentityMember.Name, dbAspect.IdentityMember);
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

        public static string OptionHtml(object value, object label)
        {
            return OptionHtml(value, label, false);
        }

        public static string OptionHtml(object value, object label, bool selected)
        {
            return string.Concat("<option value=\"", value.ToString(), "\"", (selected ? " selected=\"selected\" " : ""), "\">", label.ToString(), "</option>");
        }
    }
}
