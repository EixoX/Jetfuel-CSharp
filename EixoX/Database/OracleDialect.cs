﻿using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Text;

namespace EixoX.Data
{
    public class OracleDialect : AnsiDialect
    {
        public OracleDialect() : base('"', '"') { }

        public override System.Data.IDbConnection CreateConnection(string connectionString)
        {
            return new OracleConnection(connectionString);
        }
    }
}
