using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html.Controls
{
    public class HtmlCheckbox : HtmlControl
    {

        public override void Render(System.IO.TextWriter writer, UI.UIControlState state)
        {
            writer.WriteLine("<div>");
            writer.Write("<label for=\"");
            writer.Write(HtmlHelper.HtmlDoubleQuoted(state.Name));
            writer.Write("\">");
            writer.Write(HtmlHelper.HtmlFormat(state.Label));
            writer.WriteLine("</label>");
            writer.Write("<input type=\"checkbox\" value=\"
        }
        }
    }
}
