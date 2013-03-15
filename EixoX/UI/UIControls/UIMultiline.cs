using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.UI
{
    public class UIMultiline : UIControlAttribute
    {
        public UIMultiline(string defaultLabel, string defaultHint, Type classStorageType)
            : base(defaultLabel, defaultHint, classStorageType) { }
        public UIMultiline(string defaultLabel, string defaultHint)
            : base(defaultLabel, defaultHint, null) { }
        public UIMultiline(string defaultLabel, Type classStorageType)
            : base(defaultLabel, null, classStorageType) { }
        public UIMultiline(Type classStorageType)
            : base(null, null, classStorageType) { }
        public UIMultiline() { }
    }
}
