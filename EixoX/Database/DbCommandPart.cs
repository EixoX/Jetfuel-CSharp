using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Database
{
    public interface DbCommandPart
    {
        void AppendSql(DbDialect dialect, StringBuilder builder);
    }
}
