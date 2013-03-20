using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.UI
{
    public class UICheckbox : UIControlAttribute
    {
        public UICheckbox(string defaultLabel, string defaultHint)
            : base(defaultLabel, defaultHint, null) { }
        public UICheckbox(string defaultLabel)
            : base(defaultLabel, null, null) { }
        public UICheckbox() { }
    }
}
