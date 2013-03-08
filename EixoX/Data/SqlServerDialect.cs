using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    public class SqlServerDialect: AnsiDialect
    {
        public SqlServerDialect() : base('[', ']') { }

        protected override void AppendScopeIdentity(StringBuilder builder, DataAspect aspect)
        {
            builder.Append("SELECT SCOPE_IDENTITY()");
        }

        public override System.Data.IDbConnection CreateConnection(string connectionString)
        {
            return new System.Data.SqlClient.SqlConnection(connectionString);
        }
    }
}
