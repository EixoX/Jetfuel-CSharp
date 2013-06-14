using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html.Controls
{
    public class BootstrapDatepicker : BootstrapControl
    {

        protected override HtmlNode CreateInput(UI.UIControlState state)
        {
            DateTime value = Convert.ToDateTime(state.Value, System.Globalization.CultureInfo.CurrentCulture);
            return new HtmlStandalone("input",
                new HtmlAttribute("type", "date"),
                new HtmlAttribute("name", state.Name),
                new HtmlAttribute("id", state.Name),
                new HtmlAttribute("value", value != DateTime.MinValue ? value.ToShortDateString() : ""),
                new HtmlAttribute("class", "datepicker"));
        }   
    }
}
