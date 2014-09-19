using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html.Controls
{
    public class Bootstrap3Datepicker : Bootstrap3Control
    {

        protected override HtmlNode CreateInput(UI.UIControlState state)
        {
            DateTime value = Convert.ToDateTime(state.Value);
            return new HtmlStandalone("input",
                new HtmlAttribute("type", "text"),
                new HtmlAttribute("name", state.Name),
                new HtmlAttribute("id", state.Name),
                new HtmlAttribute("value", value != DateTime.MinValue ? string.Format(state.FormatProvider, "{0:d}", state.Value) : ""),
                new HtmlAttribute("class", "form-control datepicker"));
        }
    }
}
