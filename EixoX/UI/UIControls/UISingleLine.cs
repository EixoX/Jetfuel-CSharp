using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.UI
{
    public class UISingleline : UIControlAttribute
    {
        public UISingleline(string defaultLabel, string defaultHint, Type classStorageType)
            : base(defaultLabel, defaultHint, classStorageType) { }
        public UISingleline(string defaultLabel, string defaultHint)
            : base(defaultLabel, defaultHint, null) { }
        public UISingleline(string defaultLabel, Type classStorageType)
            : base(defaultLabel, null, classStorageType) { }
        public UISingleline(Type classStorageType)
            : base(null, null, classStorageType) { }
        public UISingleline(string defaultLabel)
            : base(defaultLabel, null, null) { }
        public UISingleline() { }
    }
}
