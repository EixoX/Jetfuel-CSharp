using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html.Controls
{
    public class HtmlRadioGroup: HtmlControl
    {
        protected override HtmlNode CreateInput(UI.UIControlState state)
        {
            HtmlComposite ul = new HtmlComposite("ul");

            foreach (KeyValuePair<object, object> item in state.Options)
            {
                HtmlComposite li = new HtmlComposite("li");
                ul.Children.AddLast(li);

                string id = state.Name + "_" + item.Key;

                li.Children.AddLast(new HtmlSimple("label", item.Value, new HtmlAttribute("for", id)));

                HtmlStandalone checkbox = new HtmlStandalone("input",
                    new HtmlAttribute("type", "checkbox"),
                    new HtmlAttribute("id", id),
                    new HtmlAttribute("name", state.Name),
                    new HtmlAttribute("value", item.Key));

                if (state.Value == item.Key)
                    checkbox.Attributes.AddLast("checked", "checked");

                li.Children.AddLast(checkbox);
            }

            return ul;
        }
    }
}
