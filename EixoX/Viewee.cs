using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX
{
    public interface Viewee
    {
        void OnException(Exception ex);
    }

    public interface ListerViewee : Viewee
    {
        void OnEmptyList();
    }

    public interface RestrictionViewee : Viewee
    {
        void OnViolatedRestrictions();
    }
}
