﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html.Controls
{
    public class BootstrapDatepicker : BootstrapControl
    {

        protected override HtmlNode CreateInput(UI.UIControlState state)
        {
            return new HtmlStandalone("input",
                new HtmlAttribute("type", "date"),
                new HtmlAttribute("name", state.Name),
                new HtmlAttribute("id", state.Name),
                new HtmlAttribute("value", Convert.ToDateTime(state.Value).ToString("yyyy-MM-dd")),
                new HtmlAttribute("class", "datepicker"));
        }   
    }
}
