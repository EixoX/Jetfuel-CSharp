using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.UI
{
    public class UISingleline : UIControlAttribute
    {
        public UISingleline(string defaultLabel, string defaultHint)
            : base(defaultLabel, defaultHint, null) { }
        public UISingleline(string defaultLabel)
            : base(defaultLabel, null, null) { }
        public UISingleline() { }
    }
}
