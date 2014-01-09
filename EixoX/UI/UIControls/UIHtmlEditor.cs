using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.UI
{
    public class UIHtmlEditor : UIControlAttribute
    {
        public UIHtmlEditor(string defaultLabel, string defaultHint)
            : base(defaultLabel, defaultHint, null) { }
        public UIHtmlEditor(string defaultLabel)
            : base(defaultLabel, null, null) { }
        public UIHtmlEditor() { }
    }
}
