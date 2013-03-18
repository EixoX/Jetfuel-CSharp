using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html.Controls
{
    public class HtmlSingleline : HtmlControl
    {
        protected override HtmlNode CreateInput(UI.UIControlState state)
        {
            return new HtmlStandalone("input",
                new HtmlAttribute("type", "text"),
                new HtmlAttribute("name", state.Name),
                new HtmlAttribute("id", state.Name),
                new HtmlAttribute("value", state.Value));
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
