using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    public class HtmlText: HtmlNode
    {
        public HtmlText(string value) { this.Text = value; }

        public string TagName
        {
            get { return "#text"; }
        }

        public HtmlAttributeCollection Attributes
        {
            get { return null; }
        }

        public string Text
        {
            get;
            set;
        }

        public bool IsStandalone
        {
            get { return true; }
        }

        public void Write(System.IO.TextWriter writer)
        {
            writer.Write(HtmlHelper.HtmlFormat(this.Text));
        }


        public HtmlNodeCollection Children
        {
            get { return null; }
        }
    }
}
