using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.UI
{
    public class UIPassword : UIControlAttribute
    {
        public UIPassword(string defaultLabel, string defaultHint)
            : base(defaultLabel, defaultHint, null) { }
        public UIPassword(string defaultLabel)
            : base(defaultLabel, null, null) { }
        public UIPassword() { }
    }
}
