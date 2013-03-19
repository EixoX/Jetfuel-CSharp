using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.UI
{
    public class UIDatepicker : UIControlAttribute
    {
        public UIDatepicker(string defaultLabel, string defaultHint, Type classStorageType)
            : base(defaultLabel, defaultHint, classStorageType) { }
        public UIDatepicker(string defaultLabel, string defaultHint)
            : base(defaultLabel, defaultHint, null) { }
        public UIDatepicker(string defaultLabel, Type classStorageType)
            : base(defaultLabel, null, classStorageType) { }
        public UIDatepicker(Type classStorageType)
            : base(null, null, classStorageType) { }
        public UIDatepicker(string defaultLabel)
            : base(defaultLabel, null, null) { }
        public UIDatepicker() { }
    }
}
