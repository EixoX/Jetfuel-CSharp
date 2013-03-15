using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Html
{
    public class HtmlPresentation : UI.UIPresenter
    {
        private readonly System.IO.TextWriter _Writer;

        public HtmlPresentation(System.IO.TextWriter writer)
        {
            this._Writer = writer;
        }

        public System.IO.TextWriter Writer
        {
            get { return this._Writer; }
        }
    }
}
