﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html.Controls
{
    public class BoostrapHidden : BoostrapControl
    {
        public override void Render(System.IO.TextWriter writer, UI.UIControlState state, params HtmlAttribute[] attributes)
        {
            CreateInput(state).Write(writer);
        }

        protected override HtmlNode CreateInput(UI.UIControlState state)
        {
            return new HtmlStandalone("input",
                new HtmlAttribute("type", "hidden"),
                new HtmlAttribute("name", state.Name),
                new HtmlAttribute("id", state.Name),
                new HtmlAttribute("value", state.Value));
        }
    }
}