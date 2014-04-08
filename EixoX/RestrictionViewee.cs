using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX
{
    public interface RestrictionViewee : Viewee
    {
        void OnViolatedRestrictions();
    }
}
