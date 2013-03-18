using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html.Controls
{
    public class HtmlCheckbox : HtmlControl
    {

        protected override HtmlNode CreateInput(UI.UIControlState state)
        {
            HtmlStandalone checkbox = new HtmlStandalone("input",
                    new HtmlAttribute("type", "checkbox"),
                    new HtmlAttribute("id", state.Name),
                    new HtmlAttribute("name", state.Name),
                    new HtmlAttribute("value", "True"));

            if (Convert.ToBoolean(state.Value))
            {
                checkbox.Attributes.AddLast("checked", "checked");
            }

            return checkbox;
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
