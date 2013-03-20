using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html.Controls
{
    public class BootstrapTimepicker
        : BootstrapControl
    {
        protected override HtmlNode CreateInput(UI.UIControlState state)
        {
            return new HtmlStandalone("input",
                new HtmlAttribute("type", "text"),
                new HtmlAttribute("id", state.Name),
                new HtmlAttribute("name", state.Name),
                new HtmlAttribute("value", state.Value),
                new HtmlAttribute("class", "timepicker"));
        }
    }
}
