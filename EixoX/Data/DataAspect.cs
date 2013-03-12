using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace EixoX.Data
{
    /// <summary>
    /// Represents an abstract data aspect with useful methods.
    /// </summary>
    public abstract class DataAspect
        : AbstractAspect<DataAspectMember>
    {
        private readonly int _IdentityOrdinal;
        private readonly LinkedList<int> _Uniques;
        private readonly LinkedList<int> _PrimaryKeys;
        private readonly string _StoredName;
        private readonly System.Reflection.ConstructorInfo _DefaultConstructor;

        /// <summary>
        /// Gets the stored name (the name of a column or a collection) from a data type.
        /// </summary>
        /// <param name="dataType">The data type to inspect.</param>
        /// <returns>The name of the data type.</returns>
        protected virtual string GetStoredName(Type dataType)
        {
            return dataType.Name;
        }

        /// <summary>
        /// Constructs a new abstract data aspect.
        /// </summary>
        /// <param name="dataType">The data type to store.</param>
        protected DataAspect(Type dataType)
            : base(dataType)
        {
            this._Uniques = new LinkedList<int>();
            this._PrimaryKeys = new LinkedList<int>();
            this._StoredName = GetStoredName(dataType);
            this._DefaultConstructor = dataType.GetConstructor(Type.EmptyTypes);

            int identityOrdinal = -1;
            int count = base.Count;
            for (int i = 0; i < count; i++)
            {
                DataAspectMember member = base[i];
                if (member.IsIdentity)
                {
                    if (identityOrdinal < 0)
                        identityOrdinal = i;
                    else
                        throw new ArgumentException(dataType + " can only have one identity.", "dataType");
                }
                else if (member.IsUnique)
                {
                    _Uniques.AddLast(i);
                }
                else if (member.IsPrimaryKey)
                {
                    _PrimaryKeys.AddLast(i);
                }
            }
            this._IdentityOrdinal = identityOrdinal;
        }

        /// <summary>
        /// Gets the stored (persisted) name of the entity.
        /// </summary>
        public string StoredName
        {
            get { return this._StoredName; }
        }

        /// <summary>
        /// Gets the identity ordinal of the data aspect.
        /// </summary>
        public int IdentityOrdinal
        {
            get { return this._IdentityOrdinal; }
        }

        /// <summary>
        /// Indicates that this data aspect has an identity.
        /// </summary>
        public bool HasIdentity
        {
            get { return this._IdentityOrdinal >= 0; }
        }

        /// <summary>
        /// Gets the identity member or null if no identity is present.
        /// </summary>
        public DataAspectMember IdentityMember
        {
            get { return this._IdentityOrdinal >= 0 ? base[_IdentityOrdinal] : null; }
        }

        /// <summary>
        /// Gets the ordinals of the unique members.
        /// </summary>
        public IEnumerable<int> UniqueMemberOrdinals
        {
            get { return this._Uniques; }
        }

        /// <summary>
        /// Indicates that this data aspect has unique members.
        /// </summary>
        public bool HasUniqueMembers
        {
            get { return this._Uniques.Count > 0; }
        }

        /// <summary>
        /// Gets an enumeration of unique members.
        /// </summary>
        public IEnumerable<DataAspectMember> UniqueMembers
        {
            get
            {
                for (LinkedListNode<int> node = _Uniques.First; node != null; node = node.Next)
                    yield return base[node.Value];
            }
        }

        /// <summary>
        /// Gets an enumeration of primary key ordinals.
        /// </summary>
        public IEnumerable<int> PrimaryKeyOrdinals
        {
            get { return _PrimaryKeys; }
        }

        /// <summary>
        /// Indicates that the data aspect has primary keys.
        /// </summary>
        public bool HasPrimaryKey
        {
            get { return this._PrimaryKeys.Count > 0; }
        }

        /// <summary>
        /// Gets an enumeration of primary key members.
        /// </summary>
        public IEnumerable<DataAspectMember> PrimaryKeys
        {
            get
            {
                for (LinkedListNode<int> node = _PrimaryKeys.First; node != null; node = node.Next)
                    yield return base[node.Value];
            }
        }

        /// <summary>
        /// Creates a primary key filter based on an entity.
        /// </summary>
        /// <param name="entity">The entity to read from.</param>
        /// <returns></returns>
        public ClassFilter CreatePrimaryKeyFilter(object entity)
        {
            if (_PrimaryKeys.Count == 0)
                return null;
            else
            {
                ClassFilterTerm first = new ClassFilterTerm(
                    this,
                    _PrimaryKeys.First.Value,
                    base[_PrimaryKeys.First.Value].GetValue(entity));

                if (_PrimaryKeys.Count == 1)
                    return first;
                else
                {
                    ClassFilterExpression exp = new ClassFilterExpression(first);
                    for (LinkedListNode<int> node = _PrimaryKeys.First.Next; node != null; node = node.Next)
                        exp.And(node.Value, base[node.Value].GetValue(entity));
                    return exp;
                }
            }
        }

        public int GetStoredNameOrdinal(string storedName)
        {
            int count = base.Count;
            for (int i = 0; i < count; i++)
                if (storedName.Equals(base[i].StoredName, StringComparison.OrdinalIgnoreCase))
                    return i;

            return -1;

        }

        /// <summary>
        /// Gets a new instance of the class.
        /// </summary>
        /// <returns>The new instance of the class.</returns>
        public object NewInstance()
        {
            return _DefaultConstructor.Invoke(null);
        }

        public IEnumerable<T> Transform<T>(IEnumerable<IDataRecord> records)
        {
            using (IEnumerator<IDataRecord> record = records.GetEnumerator())
            {
                if (record.MoveNext())
                {
                    int fieldCount = record.Current.FieldCount;
                    bool initializable = typeof(Initializable).IsAssignableFrom(DataType);
                    DataAspectMember[] members = new DataAspectMember[fieldCount];

                    for (int i = 0; i < fieldCount; i++)
                    {
                        int ordinal = GetStoredNameOrdinal(record.Current.GetName(i));
                        if (ordinal >= 0)
                            members[i] = this[ordinal];
                    }

                    do
                    {
                        T entity = (T)NewInstance();
                        for (int i = 0; i < fieldCount; i++)
                            if (members[i] != null && !record.Current.IsDBNull(i))
                                members[i].SetValue(entity, record.Current.GetValue(i));

                        if (initializable)
                            ((Initializable)entity).Initialize();

                        yield return entity;

                    } while (record.MoveNext());
                }
            }
        }
    }
}
