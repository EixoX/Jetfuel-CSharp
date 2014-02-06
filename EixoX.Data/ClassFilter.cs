using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    public interface ClassFilter
    {
        bool FilterPass(object entity, object value);
        System.Collections.IEnumerable FilterPass(System.Collections.IEnumerable entities, object value);
    }
}
