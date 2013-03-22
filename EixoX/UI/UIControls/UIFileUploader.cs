using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.UI
{
    public class UIFileUploader : UIControlAttribute
    {
        public UIFileUploader(Type classStorageType)
            : base(null, null, classStorageType) { }
        public UIFileUploader(string defaultLabel)
            : base(defaultLabel, null, null) { }
        public UIFileUploader() { }
    }
}
