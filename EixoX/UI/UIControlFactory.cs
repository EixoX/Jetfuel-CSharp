using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.UI
{
    public interface UIControlFactory
    {
        UIControl CreateControlFor(UIControlAttribute attribute);
    }
}
