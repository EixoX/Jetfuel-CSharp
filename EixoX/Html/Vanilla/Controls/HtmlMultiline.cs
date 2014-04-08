using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html.Controls
{
    public class HtmlMultiline : HtmlControl
    {

        protected override HtmlNode CreateInput(UI.UIControlState state)
        {
            return new HtmlSimple("textarea", 
                state.Value,
                new HtmlAttribute("id", state.Name),
                new HtmlAttribute("name", state.Name),
                new HtmlAttribute("rows", "5"),
                new HtmlAttribute("cols", "40"));
        }
    }
}
