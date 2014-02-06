using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EixoX.Data
{
    public sealed class ClassSchema
    {
        private readonly ClassAcessor[] _Acessors;
        private readonly Type _DataType;

        private ClassSchema(Type dataType)
        {
            this._DataType = dataType;
            FieldInfo[] fields = dataType.GetFields();
            PropertyInfo[] properties = dataType.GetProperties();
            this._Acessors = new ClassAcessor[fields.Length + properties.Length];
            for (int i = 0; i < fields.Length; i++)
                _Acessors[i] = new ClassField(fields[i]);
            for (int i = 0; i < properties.Length; i++)
                _Acessors[fields.Length + i] = new ClassProperty(properties[i]);
        }


        public ClassAcessor this[int ordinal]
        {
            get { return this._Acessors[ordinal]; }
        }

        public int IndexOf(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                int l = _Acessors.Length;
                for (int i = 0; i < l; i++)
                    if (String.Equals(name, _Acessors[i].Name, StringComparison.OrdinalIgnoreCase))
                        return i;
            }

            return -1;
        }
    }
}
