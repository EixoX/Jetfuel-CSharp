using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace EixoX.Data
{
    public class OleDbDialect: AnsiDialect
    {
        public OleDbDialect() : base('[', ']') { }



        public override System.Data.IDbConnection CreateConnection(string connectionString)
        {
            return new System.Data.OleDb.OleDbConnection(connectionString);
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
    }
}
