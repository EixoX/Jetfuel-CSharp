using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    public class PostgresDialect
        : AnsiDialect
    {
        public PostgresDialect() : base('"', '"') { }



        public override System.Data.IDbConnection CreateConnection(string connectionString)
        {
            return new Npgsql.NpgsqlConnection(connectionString);
        }

        public override bool CanLimitRecords
        {
            get { return true; }
        }

        protected override string FormatBoolean(bool value)
        {
            return value ? "TRUE" : "FALSE";
        }
    }
}
