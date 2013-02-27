using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Expressions
{
    public interface ClassPagerBased<TClass>
    {
        int PageSize { get; }
        int PageOrdinal { get; }
        TClass Page(int pageSize, int pageOrdinal);
    }
}
