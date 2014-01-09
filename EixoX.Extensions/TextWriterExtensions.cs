using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using EixoX.Html;

namespace System
{
    public static class TextWriterExtensions
    {
        public static void WriteHtmlAttribute(this TextWriter writer, string name, object value)
        {
            writer.Write(name);
            writer.Write("=\"");
            writer.Write(value == null ? "": value.ToString().Replace("\"", "&quot;"));
            writer.Write("\"");
        }

        public static void WriteHtmlTag(this TextWriter writer, string tagName, bool isEmpty, params HtmlAttribute[] attributes)
        {
            writer.Write('<');
            writer.Write(tagName);
            for (int i = 0; i < attributes.Length; i++)
            {
                writer.Write(' ');
                writer.Write(attributes[i].Name);
                writer.Write("=\"");
                writer.Write(attributes[i].Value == null ? "" : attributes[i].Value.ToString().Replace("\"", "&quot;"));
                writer.Write('"');
            }
            writer.Write(isEmpty ? " />" : ">");
        }

        public static void WriteHtmlCloseTag(this TextWriter writer, string tagName)
        {
            writer.Write("</");
            writer.Write(tagName);
            writer.Write(">");
        }

    }
}
