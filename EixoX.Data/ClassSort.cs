using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    public sealed class ClassSort
    {
        private readonly ClassSchema _Schema;
        private readonly int _Ordinal;
        private readonly ClassSortDirection _Direction;
        private ClassSort _Next;

        public ClassSort(ClassSchema schema, int ordinal, ClassSortDirection direction)
        {
            this._Schema = schema;
            this._Ordinal = ordinal;
            this._Direction = direction;
        }

        public sealed ClassSchema Schema { get { return this._Schema; } }
        public sealed int Ordinal { get { return this._Ordinal; } }
        public sealed ClassSortDirection Direction { get { return this._Direction; } }
        public ClassSort Next { get { return this._Next; } set { this._Next = value; } }
    }
}
