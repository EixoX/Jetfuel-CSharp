using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html.Controls
{
    public class Bootstrap3Singleline : Bootstrap3Control
    {

        protected override HtmlNode CreateInput(UI.UIControlState state)
        {
            return new HtmlStandalone("input",
                new HtmlAttribute("type", "text"),
                new HtmlAttribute("name", state.Name),
                new HtmlAttribute("id", state.Name),
                new HtmlAttribute("value", state.Value),
                new HtmlAttribute("placeholder", state.Control.GetGlobalizationTerm("placeholder")),
                new HtmlAttribute("class", "form-control"));
        }
    }
}
