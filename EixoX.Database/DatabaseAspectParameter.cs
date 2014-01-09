using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace EixoX.Database
{
    public class DatabaseAspectParameter
        : IDbDataParameter
    {
        private readonly IDbDataParameter _Base;
        private readonly DatabaseAspectMember _member;

        public DatabaseAspectParameter(IDbDataParameter param, DatabaseAspectMember member)
        {
            this._Base = param;
            this._member = member;
            this._Base.DbType = member.DbType;
        }

        public byte Precision
        {
            get { return _Base.Precision; }
            set { _Base.Precision = value; }
        }

        public byte Scale
        {
            get { return _Base.Scale; }
            set { _Base.Scale = value; }
        }

        public int Size
        {
            get { return _Base.Size; }
            set { _Base.Size = value; }
        }

        public DbType DbType
        {
            get { return _Base.DbType; }
            set { _Base.DbType = value; }
        }

        public ParameterDirection Direction
        {
            get { return _Base.Direction; }
            set { _Base.Direction = value; }
        }

        public bool IsNullable
        {
            get { return _Base.IsNullable; }
        }

        public string ParameterName
        {
            get { return _Base.ParameterName; }
            set { _Base.ParameterName = value; }
        }

        public string SourceColumn
        {
            get { return _Base.SourceColumn; }
            set { _Base.SourceColumn = value; }
        }

        public DataRowVersion SourceVersion
        {
            get { return _Base.SourceVersion; }
            set { _Base.SourceVersion = value; }
        }

        public object Value
        {
            get { return _Base.Value; }
            set { _Base.Value = value; }
        }


        public void ReadValue(object entity)
        {
            _Base.Value = _member.GetValue(entity);
        }

        public void WriteValue(object entity)
        {
            _member.SetValue(entity, _Base.Value);
        }
    }
}
