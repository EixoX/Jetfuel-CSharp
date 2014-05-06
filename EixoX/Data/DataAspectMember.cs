using System;
using System.Collections.Generic;
using System.Text;


namespace EixoX.Data
{
    /// <summary>
    /// Represents a data member that can be used on data storage operations.
    /// </summary>
    public class DataAspectMember
        : AspectMember
    {
        private readonly string _StoredName;
        private readonly bool _Identity;
        private readonly bool _Unique;
        private readonly bool _PrimaryKey;
        private readonly bool _Nullable;
        private readonly Generator _Generator;
        private readonly string _Caption;

        /// <summary>
        /// Constructs the data member.
        /// </summary>
        /// <param name="acessor">The member acessor for get and set values.</param>
        /// <param name="storedName">The stored name of the class or null if it's the accessor name.</param>
        /// <param name="identity">The identity flag.</param>
        /// <param name="unique">The unique member flag (only used if not an identity).</param>
        /// <param name="primaryKey">The primary key flag (only used if not an identity or unique).</param>
        /// <param name="nullable">The nullable flag.</param>
        /// <param name="generator">The value generators for storage io.</param>
        public DataAspectMember(
            ClassAcessor acessor,
            string storedName,
            bool identity,
            bool unique,
            bool primaryKey,
            bool nullable,
            Generator generator,
            string caption)
            : base(acessor)
        {
            this._StoredName = string.IsNullOrEmpty(storedName) ? acessor.Name : storedName;
            this._Identity = identity;
            this._Unique = unique;
            this._PrimaryKey = primaryKey;
            this._Nullable = nullable;
            this._Generator = generator;
            this._Caption = string.IsNullOrEmpty(caption) ? acessor.Name : caption;
        }

        /// <summary>
        /// Gets the storage alias for this member.
        /// </summary>
        public string StoredName { get { return this._StoredName; } }

        /// <summary>
        /// Indicates that this member is an identity.
        /// </summary>
        public bool IsIdentity { get { return this._Identity; } }

        /// <summary>
        /// Indicates that this member is unique.
        /// </summary>
        public bool IsUnique { get { return this._Unique; } }

        /// <summary>
        /// Indicates that this member is part of a primary key composition.
        /// </summary>
        public bool IsPrimaryKey { get { return this._PrimaryKey; } }

        /// <summary>
        /// Indicates that this member may be null.
        /// </summary>
        public bool IsNullable { get { return this._Nullable; } }

        /// <summary>
        /// Gets the value generator for this member.
        /// </summary>
        public Generator Generator { get { return this._Generator; } }

        /// <summary>
        /// Gets the caption (friendly) name.
        /// </summary>
        public string Caption { get { return this._Caption; } }

        /// <summary>
        /// Gets the data value (or null) for an entity.
        /// </summary>
        /// <param name="entity">The entity to read from.</param>
        /// <returns>The data value (or null if nullable and empty).</returns>
        public override object GetValue(object entity)
        {
            object value = base.GetValue(entity);
            return _Nullable && ValidationHelper.IsNullOrEmpty(value) ?
                null : value;
        }

        public bool IncludeInScope(DataScope scope)
        {
            if (this._Generator == null)
                return true;
            else if (this._Generator.GeneratorScope == DataScope.Save)
                return true;
            else return this._Generator.GeneratorScope == scope;
        }

        /// <summary>
        /// Gets the data value for a specific scope.
        /// </summary>
        /// <param name="entity">The entity to read from.</param>
        /// <param name="scope">The scope of the read.</param>
        /// <returns>The original or generated value.</returns>
        /// <remarks>The original class is updated if a value is generated.</remarks>
        public object GetValue(object entity, DataScope scope)
        {
            object value = base.GetValue(entity);

            if (_Generator != null && (_Generator.GeneratorScope == scope || scope == DataScope.Save))
            {
                if (ValidationHelper.IsNullOrEmpty(value))
                {
                    value = _Generator.Generate();
                    SetValue(entity, value);
                }
                return value;
            }
            else
            {
                return _Nullable && ValidationHelper.IsNullOrEmpty(value) ? null : value;
            }


        }
    }
}
