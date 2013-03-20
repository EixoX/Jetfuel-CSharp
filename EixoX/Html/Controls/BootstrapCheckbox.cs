using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html.Controls
{
    public class BootstrapCheckbox : BootstrapControl
    {
        public override void Render(System.IO.TextWriter writer, UI.UIControlState state, params HtmlAttribute[] attributes)
        {
            base.Render(writer, state, attributes);
        }

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
    }
}
