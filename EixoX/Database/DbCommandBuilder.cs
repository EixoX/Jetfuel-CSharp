using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace EixoX.Database
{
    public class DbCommandBuilder
    {
        private StringBuilder _builder;
        private CommandType _Type;
        private LinkedList<object> _ParameterValues;

        public DbCommandBuilder(CommandType commandType)
        {
            this._Type = commandType;
            this._builder = new StringBuilder();
            this._ParameterValues = new LinkedList<object>();
        }

        public DbCommandBuilder(CommandType commandType, int capacity)
        {
            this._Type = commandType;
            this._builder = new StringBuilder();
            this._ParameterValues = new LinkedList<object>();
        }

        public DbCommandBuilder AppendRaw(string text)
        {
            _builder.Append(text);
            return this;
        }


    }
}
