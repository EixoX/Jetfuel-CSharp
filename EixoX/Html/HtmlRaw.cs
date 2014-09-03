using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    public class HtmlRaw
        : HtmlNode
    {

        private readonly string content;

        public HtmlRaw(string content)
        {
            this.content = content;
        }

        public string TagName
        {
            get { return "#raw"; }
        }

        public HtmlAttributeCollection Attributes
        {
            get { return null; }
        }

        public HtmlNodeCollection Children
        {
            get { return null; }
        }

        public string Text
        {
            get { return content; }
        }

        public bool IsStandalone
        {
            get { return true; }
        }

        public void Write(System.IO.TextWriter writer)
        {
            if (this.content != null)
                writer.Write(this.content);
        }
    }
}
