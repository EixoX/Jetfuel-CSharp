using EixoX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RocketLauncherDBCode
{
    public interface RocketLauncherViewee : Viewee
    {
        bool YesOrNo(string message);
    }
}
