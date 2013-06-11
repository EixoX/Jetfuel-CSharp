using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    public interface PreparedClassDelete
    {
        int Execute(DataAspect aspect, object entity);
    }
}
