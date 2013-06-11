using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX
{
    public interface IContract
    {
        void OnException(Exception ex);
    }
}
