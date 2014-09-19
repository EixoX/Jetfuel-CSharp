using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    public class HtmlBuilder
    {
        public readonly StringBuilder content;
        private bool openTag;

        public HtmlBuilder() { this.content = new StringBuilder(); }
        public HtmlBuilder(int capacity) { this.content = new StringBuilder(capacity); }


        public HtmlBuilder AppendRaw(object item)
        {
            this.content.Append(item);
            return this;
        }

        public HtmlBuilder AppendRaw(string content)
        {
            this.content.Append(content);
            return this;
        }

        public HtmlBuilder OpenTag(string tagName)
        {
            if (this.openTag)
            {
                this.content.Append('>');
            }
            this.content.Append('<');
            this.content.Append(tagName);
            this.openTag = true;
            return this;
        }

        public HtmlBuilder AppendText(string text)
        {
            if (openTag)
            {
                openTag = false;
                this.content.Append('>');
            }
            if (text != null)
                this.content.Append(HtmlHelper.HtmlFormat(text));
            return this;
        }

        public HtmlBuilder AppendText(object value)
        {
            if (value != null)
                return AppendText(value.ToString());
            else
                return this;
        }

        public HtmlBuilder AppendText(IFormatProvider formatProvider, string formatString, params object[] items)
        {
            return AppendText(string.Format(formatProvider, formatString, items));
        }

        public HtmlBuilder AppendText(string formatString, params object[] items)
        {
            return AppendText(string.Format(formatString, items));
        }


        public HtmlBuilder AppendAttribute(string name, string value)
        {
            if (!openTag)
            {
                throw new Exception("Can't write attributes if no tags are open");
            }

            this.content.Append(' ');
            this.content.Append(name);
            this.content.Append("=\"");
            if (!string.IsNullOrEmpty(value))
            {
                this.content.Append(value.Replace("\"", "&quot;"));
            }
            this.content.Append("\"");
            return this;
        }

        public HtmlBuilder AppendAttribute(string name, IFormatProvider formatProvider, string formatString, params object[] values)
        {
            return AppendAttribute(name, string.Format(formatProvider, formatString, values));
        }

        public HtmlBuilder AppendAttribute(string name, string formatString, params object[] values)
        {
            return AppendAttribute(name, string.Format(formatString, values));
        }

        public HtmlBuilder AppendAttribute(string name, object value)
        {
            return AppendAttribute(name, value == null ? "" : value.ToString());
        }

        public override string ToString()
        {
            return this.content.ToString();
        }

        public override int GetHashCode()
        {
            return this.content.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.content.Equals(obj);
        }
    }
}
