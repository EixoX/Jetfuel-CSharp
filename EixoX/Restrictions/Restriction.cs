using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Restrictions
{
    public interface Restriction
    {
        bool Validate(object input);
        string GetRestrictionMessage(object input);
    }
}
