using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX
{
    public interface ListerViewee : Viewee
    {
        void OnEmptyList();
    }
}
