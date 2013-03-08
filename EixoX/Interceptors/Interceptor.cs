using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Interceptors
{
    public interface Interceptor
    {
        object Intercept(object input);
    }
}
