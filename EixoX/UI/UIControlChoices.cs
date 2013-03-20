using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.UI
{
    public interface UIControlChoices
    {
        IEnumerable<KeyValuePair<object, object>> GetChoices();
    }
}
