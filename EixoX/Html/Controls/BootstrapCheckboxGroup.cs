using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html.Controls
{
    public class BootstrapCheckboxGroup : BootstrapControl
    {

        private bool IsChecked(System.Collections.IEnumerable items, object value)
        {
            if (items == null)
                return false;

            Type conversionType = value.GetType();

            foreach (object i in items)
                if (value.Equals(Convert.ChangeType(i, conversionType)))
                    return true;
            return false;
        }

        protected override HtmlNode CreateInput(UI.UIControlState state)
        {
            HtmlComposite ul = new HtmlComposite("ul");

            System.Collections.IEnumerable items =
                state.Value is string ?
                ((string)state.Value).Split(',') :
                (System.Collections.IEnumerable)state.Value;

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

                if (IsChecked(items, item.Key))
                    checkbox.Attributes.AddLast("checked", "checked");

                li.Children.AddLast(checkbox);
            }

            return ul;
        }
    }
}
