using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Database
{
    public interface DatabaseAspectAdapter
    {
        bool IsEmpty(object value);
        void AppendFormatObject(StringBuilder builder, object value);
        System.Data.DbType DbType { get; }
    }
}
