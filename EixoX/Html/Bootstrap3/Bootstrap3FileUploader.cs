using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html.Controls
{
    public class Bootstrap3FileUploader
        : Bootstrap3Control
    {
        protected override HtmlNode CreateInput(UI.UIControlState state)
        {
            return new HtmlStandalone("input",
                new HtmlAttribute("type", "file"),
                new HtmlAttribute("id", state.Name),
                new HtmlAttribute("name", state.Name),
                new HtmlAttribute("value", state.Value),
                new HtmlAttribute("class", "formp-control file"));
        }
    }
}
