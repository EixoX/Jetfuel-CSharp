using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html.Controls
{
    public class HtmlDropdown : HtmlControl
    {
        protected override HtmlNode CreateInput(UI.UIControlState state)
        {
            HtmlComposite select = new HtmlComposite("select",
                new HtmlAttribute("id", state.Name),
                new HtmlAttribute("name", state.Name));

            foreach (KeyValuePair<object, object> item in state.Options)
            {
                HtmlSimple option = new HtmlSimple("option", item.Value);
                if (item.Key == state.Value)
                    option.Attributes.AddLast("selected", "selected");

                select.Children.AddLast(option);
            }

            return select;
        }

        protected override bool WriteLabel
        {
            get { return true; }
        }

        protected override bool WriteHint
        {
            get { return true; }
        }
    }
}
