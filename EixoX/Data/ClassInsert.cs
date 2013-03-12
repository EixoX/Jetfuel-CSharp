using System;
using System.Collections.Generic;
using System.Text;

/*
 * First Author: Rodrigo Portela (rodrigo.portela@gmail.com) in 2013-01-01
 */
namespace EixoX.Data
{
    /// <summary>
    /// Wrapps an insert command.
    /// </summary>
    public class ClassInsert
    {
        private readonly LinkedList<AspectMemberValue> _Values;
        private readonly ClassStorageEngine _Storage;
        private readonly DataAspect _Aspect;

        /// <summary>
        /// Construct class insert.
        /// </summary>
        /// <param name="storage">The storage to use.</param>
        /// <param name="aspect">The aspect to use.</param>
        public ClassInsert(ClassStorageEngine storage, DataAspect aspect)
        {
            this._Storage = storage;
            this._Aspect = aspect;
            this._Values = new LinkedList<AspectMemberValue>();
        }

        /// <summary>
        /// Gets the insert values.
        /// </summary>
        public LinkedList<AspectMemberValue> Values
        {
            get { return this._Values; }
        }

        /// <summary>
        /// Gets the class storage.
        /// </summary>
        public ClassStorageEngine Storage
        {
            get { return this._Storage; }
        }

        /// <summary>
        /// Gets the data aspect.
        /// </summary>
        public DataAspect Aspect
        {
            get { return this._Aspect; }
        }

        /// <summary>
        /// Adds a member value to insert.
        /// </summary>
        /// <param name="ordinal">The ordinal position of the member.</param>
        /// <param name="value">The value of the member.</param>
        /// <returns>The ClassInsert.</returns>
        public ClassInsert Add(int ordinal, object value)
        {
            this._Values.AddLast(new AspectMemberValue(_Aspect, ordinal, value));
            return this;
        }

        /// <summary>
        /// Adds a member value to insert.
        /// </summary>
        /// <param name="name">The name of the member.</param>
        /// <param name="value">The value of the member.</param>
        /// <returns>The ClassInsert.</returns>
        public ClassInsert Add(string name, object value)
        {
            this._Values.AddLast(new AspectMemberValue(_Aspect, name, value));
            return this;
        }

        /// <summary>
        /// Executes the insert.
        /// </summary>
        /// <returns>The number of items affected.</returns>
        public int Insert()
        {
            object identityValue;
            return _Storage.Insert(_Aspect, _Values, out identityValue);
        }

        /// <summary>
        /// Executes the insert.
        /// </summary>
        /// <param name="identityValue">The value of the scope identity if present.</param>
        /// <returns>THe numer of items affected.</returns>
        public int Insert(out object identityValue)
        {
            return _Storage.Insert(_Aspect, _Values, out identityValue);
        }

        /// <summary>
        /// Executes the insert.
        /// </summary>
        /// <returns>The scope identity of the insertion.</returns>
        public object InsertWithScopeIdentity()
        {
            object identityValue;
            _Storage.Insert(_Aspect, _Values, out identityValue);
            return identityValue;
        }

    }
}
