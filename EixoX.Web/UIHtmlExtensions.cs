using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EixoX.UI;
using EixoX.Html;

namespace EixoX.Web
{
    public static class UIHtmlExtensions
    {

        public static HtmlComposite AppendSimple(this HtmlComposite element, string tagName, object content, params HtmlAttribute[] attributes)
        {
            element.Children.AddLast(new HtmlSimple(tagName, content, attributes));
            return element;
        }

        public static HtmlComposite AppendText(this HtmlComposite element, string text)
        {
            if (!string.IsNullOrEmpty(text))
                element.Children.AddLast(new HtmlText(text));
            return element;
        }

        public static HtmlComposite AppendText(this HtmlComposite element, object value)
        {
            if (value != null)
                element.Children.AddLast(new HtmlText(value.ToString()));
            return element;
        }

        public static HtmlComposite AppendText(this HtmlComposite element, string formatString, object value)
        {
            if (value != null)
                element.Children.AddLast(new HtmlText(
                    string.IsNullOrEmpty(formatString) ?
                    value.ToString() :
                    string.Format(formatString, value)
                    ));
            return element;
        }


        public static HtmlComposite AppendStandalone(this HtmlComposite element, string tagName, params HtmlAttribute[] attributes)
        {
            element.Children.AddLast(new HtmlStandalone(tagName, attributes));
            return element;
        }

        public static HtmlComposite AppendComposite(this HtmlComposite element, string tagName, params HtmlAttribute[] attributes)
        {
            HtmlComposite composite = new HtmlComposite(tagName, attributes);
            element.Children.AddLast(composite);
            return composite;
        }

        public static HtmlNode AppendAttribute(this HtmlNode node, string name, object value)
        {
            node.Attributes.AddLast(new HtmlAttribute(name, value));
            return node;
        }

        public static void AppendHtmlOption(this HtmlComposite composite, object value, string text, bool selected)
        {
            HtmlSimple option = new HtmlSimple("option", text,
                new HtmlAttribute("value", value));

            if (selected)
                option.Attributes.AddLast("selected", "selected");

            composite.Children.AddLast(option);
        }

    }
}
