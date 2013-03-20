using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.UI
{
    public class UITimepicker : UIControlAttribute
    {
        public UITimepicker(string defaultLabel, string defaultHint, Type classStorageType)
            : base(defaultLabel, defaultHint, classStorageType) { }
        public UITimepicker(string defaultLabel, string defaultHint)
            : base(defaultLabel, defaultHint, null) { }
        public UITimepicker(string defaultLabel, Type classStorageType)
            : base(defaultLabel, null, classStorageType) { }
        public UITimepicker(Type classStorageType)
            : base(null, null, classStorageType) { }
        public UITimepicker(string defaultLabel)
            : base(defaultLabel, null, null) { }
        public UITimepicker() { }
    }
}
