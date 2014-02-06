using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    /// <summary>
    /// Represents a class delete command.
    /// </summary>
    public abstract class ClassDelete : ClassFilterWhere<ClassDelete>
    {
        private ClassSchema _ClassSchema;
        private ClassFilter _WhereFirst;
        private ClassFilter _WhereLast;

        public ClassDelete(ClassSchema schema)
        {
            this._ClassSchema = schema;
        }

        public ClassDelete Where(ClassFilter filter)
        {
            throw new NotImplementedException();
        }

        public ClassDelete Where(string name, ClassFilterComparison comparison, object value)
        {
            throw new NotImplementedException();
        }

        public ClassDelete Where(string name, object value)
        {
            throw new NotImplementedException();
        }

        public ClassDelete Where(int ordinal, ClassFilterComparison comparison, object value)
        {
            throw new NotImplementedException();
        }

        public ClassDelete Where(int ordinal, object value)
        {
            throw new NotImplementedException();
        }

        public ClassDelete And(ClassFilter filter)
        {
            throw new NotImplementedException();
        }

        public ClassDelete And(string name, ClassFilterComparison comparison, object value)
        {
            throw new NotImplementedException();
        }

        public ClassDelete And(string name, object value)
        {
            throw new NotImplementedException();
        }

        public ClassDelete And(int ordinal, ClassFilterComparison comparison, object value)
        {
            throw new NotImplementedException();
        }

        public ClassDelete And(int ordinal, object value)
        {
            throw new NotImplementedException();
        }

        public ClassDelete Or(ClassFilter filter)
        {
            throw new NotImplementedException();
        }

        public ClassDelete Or(string name, ClassFilterComparison comparison, object value)
        {
            throw new NotImplementedException();
        }

        public ClassDelete Or(string name, object value)
        {
            throw new NotImplementedException();
        }

        public ClassDelete Or(int ordinal, ClassFilterComparison comparison, object value)
        {
            throw new NotImplementedException();
        }

        public ClassDelete Or(int ordinal, object value)
        {
            throw new NotImplementedException();
        }
    }
}
