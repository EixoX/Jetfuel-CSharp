using EixoX.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html.Controls
{
    public class HtmlColorPicker : HtmlControl
    {
        protected override HtmlNode CreateInput(UI.UIControlState state)
        {
            return new HtmlSimple(
                "input",
                state.Value,
                new HtmlAttribute("type", "text"),
                new HtmlAttribute("class", "colorPicker"),
                new HtmlAttribute("id", state.Name));

        }
    }
}
