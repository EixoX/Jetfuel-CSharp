using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html.Controls
{
    public class HtmlFileUploader : HtmlControl
    {

        protected override HtmlNode CreateInput(UI.UIControlState state)
        {
            return new HtmlStandalone("input",
                new HtmlAttribute("type", "file"),
                new HtmlAttribute("name", state.Name),
                new HtmlAttribute("id", state.Name),
                new HtmlAttribute("value", state.Value));
        }
    }
}
