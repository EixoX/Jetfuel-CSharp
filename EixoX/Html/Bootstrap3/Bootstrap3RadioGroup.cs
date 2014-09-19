using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html.Controls
{
    public class Bootstrap3RadioGroup: Bootstrap3Control
    {
        protected override HtmlNode CreateInput(UI.UIControlState state)
        {
            HtmlComposite ul = new HtmlComposite("ul", new HtmlAttribute("class", "unstyled"));

            foreach (KeyValuePair<object, object> item in state.Options)
            {
                HtmlComposite li = new HtmlComposite("li");
                ul.Children.AddLast(li);
                
                
                string id = state.Name + "_" + item.Key;
                HtmlComposite label = new HtmlComposite("label", new HtmlAttribute("class", "checkbox"));
                li.Children.AddLast(label);

                HtmlStandalone checkbox = new HtmlStandalone("input",
                    new HtmlAttribute("type", "radio"),
                    new HtmlAttribute("id", id),
                    new HtmlAttribute("name", state.Name),
                    new HtmlAttribute("value", item.Key));

                if (state.Value.Equals(item.Key))
                    checkbox.Attributes.AddLast("checked", "checked");


                label.Children.AddLast(checkbox);
                label.Children.AddLast(new HtmlText(" " + item.Value));

            }

            return ul;
        }
    }
}
