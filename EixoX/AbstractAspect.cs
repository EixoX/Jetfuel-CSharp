using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace EixoX
{
    public abstract class AbstractAspect<TMember> : Aspect
        where TMember : AspectMember
    {
        private readonly List<TMember> _Members;
        private readonly Type _DataType;

        protected abstract bool CreateAspectFor(ClassAcessor acessor, out TMember member);
        protected virtual void BeforeInitializeMembers() { }
        protected virtual void AfterInitializeMembers() { }

        public AbstractAspect(Type dataType)
        {
            this._DataType = dataType;
            BeforeInitializeMembers();

            FieldInfo[] fields = dataType.GetFields();
            PropertyInfo[] properties = dataType.GetProperties();
            TMember member;

            this._Members = new List<TMember>(fields.Length + properties.Length);
            for (int i = 0; i < fields.Length; i++)
            {

                if (CreateAspectFor(new ClassField(fields[i]), out member))
                    _Members.Add(member);
            }
            for (int i = 0; i < properties.Length; i++)
            {
                if (CreateAspectFor(new ClassProperty(properties[i]), out member))
                    _Members.Add(member);
            }

            AfterInitializeMembers();
        }

        public Type DataType { get { return this._DataType; } }
        public string Name { get { return this._DataType.Name; } }
        public string FullName { get { return this._DataType.FullName; } }

        public TMember this[int ordinal]
        {
            get { return _Members[ordinal]; }
        }

        public int Count { get { return this._Members.Count; } }

        public int GetOrdinal(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                int count = _Members.Count;
                for (int i = 0; i < count; i++)
                    if (name.Equals(_Members[i].Name, StringComparison.OrdinalIgnoreCase))
                        return i;
            }
            return -1;
        }
        public int GetOrdinalOrException(string name)
        {
            int ordinal = GetOrdinal(name);
            if (ordinal < 0)
                throw new ArgumentOutOfRangeException("name", name + " is not on aspect " + _DataType.FullName);
            else
                return ordinal;
        }

        public bool HasMember(string name)
        {
            return GetOrdinal(name) >= 0;
        }

        public TMember this[string name]
        {
            get { return _Members[GetOrdinalOrException(name)]; }
        }

        public override string ToString()
        {
            return _DataType.FullName;
        }


        public AspectMember GetMember(int ordinal)
        {
            return _Members[ordinal];
        }

        public AspectMember GetMember(string name)
        {
            return _Members[GetOrdinalOrException(name)];
        }

        public IEnumerable<AspectMember> GetMembers()
        {
            int count = _Members.Count;
            for (int i = 0; i < count; i++)
                yield return _Members[i];
        }
    }
}
