using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace EixoX.Data
{
    public class SqlServerDialect : AnsiDialect
    {
        public SqlServerDialect() : base('[', ']') { }



        public override void AppendScopeIdentity(StringBuilder builder, DataAspect aspect)
        {
            builder.Append("SELECT SCOPE_IDENTITY()");
        }

        public override System.Data.IDbConnection CreateConnection(string connectionString)
        {
            return new System.Data.SqlClient.SqlConnection(connectionString);
        }

        public override void AppendSortTerm(StringBuilder builder, DataAspect aspect, ClassSortTerm term)
        {
            if (term.Direction == SortDirection.Random)
                builder.Append(" NEWID()");
            else
                base.AppendSortTerm(builder, aspect, term);
        }

        public override bool CanLimitRecords
        {
            get { return true; }
        }

        public override DatabaseCommand CreateSelect(DataAspect aspect, ClassFilter filter, ClassSort sort, int pageSize, int pageOrdinal)
        {
            StringBuilder builder = new StringBuilder(255);
            int count = aspect.Count;

            builder.Append("SELECT ");
            if (pageSize > 0 && pageOrdinal >= 0)
            {
                builder.Append("TOP ");
                builder.Append((pageOrdinal + 1) * pageSize);
                builder.Append(' ');
            }

            AppendName(builder, aspect[0].StoredName);
            for (int i = 1; i < count; i++)
            {
                builder.Append(", ");
                AppendName(builder, aspect[i].StoredName);
            }
            builder.Append(" FROM ");
            AppendName(builder, aspect.StoredName);
            if (filter != null)
            {
                builder.Append(" WHERE ");
                AppendFilter(builder, aspect, filter);
            }
            if (sort != null)
            {
                builder.Append(" ORDER BY ");
                AppendSort(builder, aspect, sort);
            }
            return new DatabaseCommand(
                CommandType.Text,
                builder.ToString(),
                null);
        }

        public string CreateFunctionSelect(string functionName, params object[] paramValues)
        {
            StringBuilder builder = new StringBuilder(255);
            builder.Append("SELECT * FROM ");
            AppendName(builder, functionName);
            if (paramValues != null && paramValues.Length > 0)
            {
                builder.Append("(");
                AppendValue(builder, paramValues[0]);
                for (int i = 1; i < paramValues.Length; i++)
                {
                    builder.Append(", ");
                    AppendValue(builder, paramValues[i]);
                }
                builder.Append(")");
            }
            else
            {
                builder.Append("()");
            }

            return builder.ToString();

        }
    }
}
