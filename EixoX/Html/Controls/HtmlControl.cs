using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html.Controls
{
    public abstract class HtmlControl : UI.UIControl
    {

        public void Render(UI.UIControlState state, object output)
        {
            Render((System.IO.TextWriter)output, state);
        }

        public abstract void Render(System.IO.TextWriter writer, UI.UIControlState state);
    }
}
