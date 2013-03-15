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
