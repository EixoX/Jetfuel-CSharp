using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace EixoX.Database
{
    public class DatabaseCommand
    {
        private readonly StringBuilder _Text;
        private readonly CommandType _Type;
        private IDbCommand _Command;

        public void AppendRaw(string text)
        {
            this._Text.Append(text);
        }

        protected abstract string CreateParameterName(int index);

        public void AppendValue(object value)
        {
            IDataParameter param = _Command.CreateParameter();
            param.ParameterName = CreateParameterName(_Command.Parameters.Count);
            param.Value = value;
            
            _Text.Append(param.ParameterName);
            _Command.Parameters.Add(param);
        }

        public int ExecuteNonQuery()
        {

        }
    }
}
