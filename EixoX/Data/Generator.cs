using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    public interface Generator
    {
        object Generate();
        DataScope GeneratorScope { get; }
    }
}
