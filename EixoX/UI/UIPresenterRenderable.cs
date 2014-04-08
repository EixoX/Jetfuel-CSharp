using EixoX.Html;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EixoX.UI
{
    public interface UIPresenterRenderable
    {
        void Render(System.IO.TextWriter writer, object entity, bool validateRestrictions, params HtmlAttribute[] attributes);

        void Render(System.IO.TextWriter writer, object entity, bool validateRestrictions);

        void Render(System.IO.TextWriter writer, object entity, bool validateRestrictions, params string[] names);

        void RenderGroup(System.IO.TextWriter writer, object entity, string groupName, bool validateRestrictions, params HtmlAttribute[] attributes);

        void RenderGroupFieldset(TextWriter writer, object entity, string groupName, bool validateRestrictions, params HtmlAttribute[] attributes);
    }
}
