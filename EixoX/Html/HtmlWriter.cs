using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    public class HtmlWriter
    {
        private System.IO.TextWriter _Writer;

        public HtmlWriter(System.IO.TextWriter writer)
        {
            this._Writer = writer;
        }

        public System.IO.TextWriter Writer
        {
            get { return this._Writer; }
        }


        public void WriteHtml(string html)
        {
            if (!string.IsNullOrEmpty(html))
            {
                _Writer.Write(
                    html
                    .Replace("&", "&amp;")
                    .Replace("<", "&lt;")
                    .Replace(">", "&gt;")
                    .Replace("\"", "&quot;")
                    .Replace("\'", "&apos;"));
            }
        }

        public void WriteBeginTag(string tagName, bool isEmpty, params HtmlAttribute[] attributes)
        {
            _Writer.Write('<');
            _Writer.Write(tagName);
            if (attributes != null && attributes.Length > 0)
            {
                for (int i = 0; i < attributes.Length; i++)
                {
                    _Writer.Write(' ');
                    _Writer.Write(attributes[i].Name);
                    _Writer.Write("=\"");
                    _Writer.Write(attributes[i].Value == null ? "" : attributes[i].Value.ToString().Replace("\"", "&quot;"));
                    _Writer.Write("\"");
                }
            }
            _Writer.Write(isEmpty ? " />" : ">");
        }

        public void WriteCloseTag(string tagName)
        {
            _Writer.Write("</");
            _Writer.Write(tagName);
            _Writer.Write(">");
        }

        public void WriteSimpleTag(string tagName, string content, params HtmlAttribute[] attributes)
        {
            WriteBeginTag(tagName, false, attributes);
            WriteHtml(content);
            WriteCloseTag(tagName);
        }

        public void WriteLine()
        {
            _Writer.WriteLine();
        }
    }
}
