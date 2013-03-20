using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.UI
{
    public class UIMultiline : UIControlAttribute
    {
        public UIMultiline(string defaultLabel, string defaultHint)
            : base(defaultLabel, defaultHint, null) { }
        public UIMultiline(string defaultLabel)
            : base(defaultLabel, null, null) { }
        public UIMultiline() { }
    }
}
