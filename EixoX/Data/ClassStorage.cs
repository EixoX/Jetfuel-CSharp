using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    /// <summary>
    /// Represents a class storage for a specific type.
    /// </summary>
    /// <typeparam name="T">The type of class to store.</typeparam>
    public class ClassStorage<T>
    {
        private readonly System.Threading.Mutex _Mutex;
        private readonly ClassStorageEngine _Engine;
        private readonly DataAspect _Aspect;

        /// <summary>
        /// Construct a class storage.
        /// </summary>
        /// <param name="engine">The class storage engine.</param>
        /// <param name="aspect">The data aspect to use.</param>
        public ClassStorage(ClassStorageEngine engine, DataAspect aspect)
        {
            this._Engine = engine;
            this._Aspect = aspect;
            this._Mutex = new System.Threading.Mutex();
        }

        /// <summary>
        /// Gets the data aspect.
        /// </summary>
        public DataAspect Aspect { get { return this._Aspect; } }

        /// <summary>
        /// Creates a delete command.
        /// </summary>
        /// <returns>A delete command.</returns>
        public ClassDelete CreateDelete() { return new ClassDelete(this._Engine, _Aspect); }
        /// <summary>
        /// Creates an insert command.
        /// </summary>
        /// <returns>An insert command.</returns>
        public ClassInsert CreateInsert() { return new ClassInsert(this._Engine, _Aspect); }
        /// <summary>
        /// Creates an update command.
        /// </summary>
        /// <returns>An update command.</returns>
        public ClassUpdate CreateUpdate() { return new ClassUpdate(this._Engine, _Aspect); }
        /// <summary>
        /// Creates a select command.
        /// </summary>
        /// <returns>A select command.</returns>
        public ClassSelect<T> Select() { return new ClassSelect<T>(this._Engine, _Aspect); }

        /// <summary>
        /// Creates a select member command.
        /// </summary>
        /// <param name="memberOrdinal">The ordinal position of the member to retrieve.</param>
        /// <returns>A select member wrapper</returns>
        public ClassSelectMember SelectMember(int memberOrdinal)
        {
            return new ClassSelectMember(this._Engine, this._Aspect, memberOrdinal);
        }

        /// <summary>
        /// Creates a select member command.
        /// </summary>
        /// <param name="memberName">The name of the member to retrieve.</param>
        /// <returns>A select member wrapper</returns>
        public ClassSelectMember SelectMember(string memberName)
        {
            return SelectMember(_Aspect.GetOrdinalOrException(memberName));
        }

        #region Helpers
        /// <summary>
        /// Enumerates values for an insert or update excluding specific member.
        /// </summary>
        /// <param name="aspect">The data aspect of the class.</param>
        /// <param name="entity">The entity to query.</param>
        /// <param name="scope">The scope of the command.</param>
        /// <param name="ordinal">The ordinal position to exclude.</param>
        /// <returns>An enumeration of aspect member values.</returns>
        private IEnumerable<AspectMemberValue> EnumerateValuesExcludingMember(DataAspect aspect, object entity, DataScope scope, int ordinal)
        {
            int count = aspect.Count;
            int identityOrdinal = aspect.IdentityOrdinal;
            for (int i = 0; i < count; i++)
                if (i != identityOrdinal && i != ordinal && aspect[i].IncludeInScope(scope))
                    yield return new AspectMemberValue(
                        aspect,
                        i,
                        aspect[i].GetValue(entity, scope));
        }
        /// <summary>
        /// Enumerates values for an update by primary key.
        /// </summary>
        /// <param name="aspect">The data aspect to use.</param>
        /// <param name="entity">The entity to query.</param>
        /// <returns>An enumeration of aspect member values.</returns>
        private IEnumerable<AspectMemberValue> EnumerateValuesForUpdateByPrimaryKey(DataAspect aspect, object entity)
        {
            int count = aspect.Count;
            int identityOrdinal = aspect.IdentityOrdinal;
            for (int i = 0; i < count; i++)
                if (i != identityOrdinal && !aspect[i].IsPrimaryKey && aspect[i].IncludeInScope(DataScope.Update))
                    yield return new AspectMemberValue(
                        aspect,
                        i,
                        aspect[i].GetValue(entity, DataScope.Update));
        }
        #endregion


        /// <summary>
        /// Inserts an entity.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        /// <returns>The numer of items affected.</returns>
        public int Insert(T entity)
        {
            _Mutex.WaitOne();
            try
            {
                object identityValue;

                int identityOrdinal = _Aspect.IdentityOrdinal;
                int result = _Engine.Insert(_Aspect, EnumerateValuesExcludingMember(_Aspect, entity, DataScope.Insert, identityOrdinal), out identityValue);

                if (identityOrdinal >= 0)
                {
                    _Aspect[identityOrdinal].SetValue(entity, identityValue);
                }

                return result;
            }
            finally
            {
                _Mutex.ReleaseMutex();
            }
        }
        /// <summary>
        /// Bulk inserts an enumeration of entities.
        /// </summary>
        /// <param name="entities">The entities to insert.</param>
        /// <returns>The number of items affected.</returns>
        public int Insert(IEnumerable<T> entities)
        {
            return _Engine.Insert(_Aspect, entities);
        }

        /// <summary>
        /// Ensures that a given entity has the identity value set.
        /// </summary>
        /// <param name="entity">The entity to ensure the identity.</param>
        /// <param name="identityValue">The value of the identity.</param>
        /// <returns>True if found.</returns>
        public bool EnsureIdentityValue(T entity)
        {
            int identityOrdinal = _Aspect.IdentityOrdinal;
            if (identityOrdinal < 0)
                throw new ArgumentException(_Aspect + " has no identity aspect.", "entity");

            object identityValue = _Aspect[identityOrdinal].GetValue(entity);
            if (!ValidationHelper.IsNullOrEmpty(identityValue))
                return true;

            //using unique Members
            foreach (int uniqueOrdinal in _Aspect.UniqueMemberOrdinals)
            {
                object uniqueValue = _Aspect[uniqueOrdinal].GetValue(entity);
                if (!ValidationHelper.IsNullOrEmpty(uniqueValue))
                {
                    identityValue = _Engine.GetMemberValue(_Aspect, identityOrdinal, new ClassFilterTerm(_Aspect, uniqueOrdinal, uniqueValue));
                    if (!ValidationHelper.IsNullOrEmpty(identityValue))
                    {
                        _Aspect[identityOrdinal].SetValue(entity, identityValue);
                        return true;
                    }
                }
            }
            //using primary keys
            if (_Aspect.HasPrimaryKey)
            {
                identityValue = _Engine.GetMemberValue(_Aspect, identityOrdinal, _Aspect.CreatePrimaryKeyFilter(entity));
                if (!ValidationHelper.IsNullOrEmpty(identityValue))
                {
                    _Aspect[identityOrdinal].SetValue(entity, identityValue);
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Executes a direct update by identity on the entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>The number of items affected.</returns>
        public int UpdateByIdentity(T entity)
        {
            int identityOrdinal = _Aspect.IdentityOrdinal;
            object identityValue = _Aspect[identityOrdinal].GetValue(entity);
            return _Engine.Update(
                _Aspect,
                EnumerateValuesExcludingMember(_Aspect, entity, DataScope.Update, identityOrdinal),
                new ClassFilterTerm(_Aspect, identityOrdinal, identityValue));
        }

        /// <summary>
        /// Executes a direct update by a unique member.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <param name="memberOrdinal">The member ordinal to use as filter.</param>
        /// <returns>The number of items affected.</returns>
        public int UpdateByMember(T entity, int memberOrdinal)
        {
            object memberValue = _Aspect[memberOrdinal].GetValue(entity);
            return _Engine.Update(
                _Aspect,
                EnumerateValuesExcludingMember(_Aspect, entity, DataScope.Update, memberOrdinal),
                new ClassFilterTerm(_Aspect, memberOrdinal, memberValue));
        }

        /// <summary>
        /// Executes a direct update by a unique member.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <param name="memberName">The name of the member.</param>
        /// <returns>The number of items affected.</returns>
        public int UpdateByMember(T entity, string memberName)
        {
            return UpdateByMember(entity, _Aspect.GetOrdinalOrException(memberName));
        }

        /// <summary>
        /// Updates an entity by it's primary key.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>The number of items affected.</returns>
        public int UpdateByPrimaryKey(T entity)
        {
            return _Engine.Update(
                _Aspect,
                EnumerateValuesForUpdateByPrimaryKey(_Aspect, entity),
                _Aspect.CreatePrimaryKeyFilter(entity));
        }

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>The number of items affected.</returns>
        public int Update(T entity)
        {
            _Mutex.WaitOne();
            try
            {
                int identityOrdinal = _Aspect.IdentityOrdinal;
                //has identity ? 
                if (identityOrdinal >= 0)
                {
                    if (!EnsureIdentityValue(entity))
                        throw new ArgumentException("Unable to locate the identity of " + _Aspect, "entity");

                    return _Engine.Update(
                        _Aspect,
                        EnumerateValuesExcludingMember(_Aspect, entity, DataScope.Update, identityOrdinal),
                        new ClassFilterTerm(_Aspect, identityOrdinal, _Aspect[identityOrdinal].GetValue(entity)));
                }
                else //non identity update
                {
                    foreach (int uniqueOrdinal in _Aspect.UniqueMemberOrdinals)
                    {
                        object uniqueValue = _Aspect[uniqueOrdinal].GetValue(entity);
                        if (!ValidationHelper.IsNullOrEmpty(uniqueValue))
                        {
                            return _Engine.Update(
                                _Aspect,
                                EnumerateValuesExcludingMember(_Aspect, entity, DataScope.Update, uniqueOrdinal),
                                new ClassFilterTerm(_Aspect, uniqueOrdinal, uniqueValue));
                        }
                    }

                    //primary key update
                    if (_Aspect.HasPrimaryKey)
                    {
                        return _Engine.Update(
                            _Aspect,
                            EnumerateValuesForUpdateByPrimaryKey(_Aspect, entity),
                            _Aspect.CreatePrimaryKeyFilter(entity));
                    }
                    else //just can't update
                    {
                        throw new ArgumentException("Can't find members to update " + _Aspect, "entity");
                    }
                }
            }
            finally
            {
                _Mutex.ReleaseMutex();
            }
        }
        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>The number of items affected.</returns>
        public int Delete(T entity)
        {
            _Mutex.WaitOne();
            try
            {
                //delete by identity
                int identityOrdinal = _Aspect.IdentityOrdinal;
                if (identityOrdinal >= 0)
                {
                    object identityValue = _Aspect[identityOrdinal].GetValue(entity);
                    if (!ValidationHelper.IsNullOrEmpty(identityValue))
                    {
                        return _Engine.Delete(_Aspect, new ClassFilterTerm(_Aspect, identityOrdinal, identityValue));
                    }
                }
                //delete by unique member
                foreach (int uniqueOrdinal in _Aspect.UniqueMemberOrdinals)
                {
                    object uniqueValue = _Aspect[uniqueOrdinal].GetValue(entity);
                    if (!ValidationHelper.IsNullOrEmpty(uniqueValue))
                    {
                        return _Engine.Delete(_Aspect, new ClassFilterTerm(_Aspect, uniqueOrdinal, uniqueValue));
                    }
                }
                //has primary keys
                if (_Aspect.HasPrimaryKey)
                {
                    return _Engine.Delete(_Aspect, _Aspect.CreatePrimaryKeyFilter(entity));
                }
                else
                {
                    //can't delete
                    throw new ArgumentException("Unable to find members to delete " + _Aspect, "entity");
                }
            }
            finally
            {
                _Mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Checks if any entity of that type exists.
        /// </summary>
        /// <returns>True if successful.</returns>
        public bool Exists() { return _Engine.Exists(_Aspect, null); }
        /// <summary>
        /// Checks if a specific entity exists.
        /// </summary>
        /// <param name="entity">The name of the entity to check.</param>
        /// <returns>True if successful.</returns>
        public virtual bool Exists(T entity)
        {
            _Mutex.WaitOne();
            try
            {
                //find by identity
                int identityOrdinal = _Aspect.IdentityOrdinal;
                if (identityOrdinal >= 0)
                {
                    object identityValue = _Aspect[identityOrdinal].GetValue(entity);
                    if (!ValidationHelper.IsNullOrEmpty(identityValue))
                    {
                        return _Engine.Exists(_Aspect, new ClassFilterTerm(_Aspect, identityOrdinal, identityValue));
                    }
                }
                //find by unique member
                foreach (int uniqueOrdinal in _Aspect.UniqueMemberOrdinals)
                {
                    object uniqueValue = _Aspect[uniqueOrdinal].GetValue(entity);
                    if (!ValidationHelper.IsNullOrEmpty(uniqueValue))
                    {
                        return _Engine.Exists(_Aspect, new ClassFilterTerm(_Aspect, uniqueOrdinal, uniqueValue));
                    }
                }
                //find by primary key
                if (_Aspect.HasPrimaryKey)
                {
                    return _Engine.Exists(_Aspect, _Aspect.CreatePrimaryKeyFilter(entity));
                }
                else
                {
                    throw new ArgumentException("Unable to locate members for exists test for " + _Aspect, "entity");
                }
            }
            finally
            {
                _Mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// Counts the number of entities.
        /// </summary>
        /// <returns>The number of entities.</returns>
        public long Count() { return _Engine.Count(_Aspect, null); }

        /// <summary>
        /// Saves (updates or inserts) an entity.
        /// </summary>
        /// <param name="entity">The entity to save.</param>
        /// <returns>The number of records affected.</returns>
        public int Save(T entity)
        {
            int identityOrdinal = _Aspect.IdentityOrdinal;
            object identityValue;

            _Mutex.WaitOne();
            try
            {
                
                //has identity ? 
                if (identityOrdinal >= 0)
                {
                    if (EnsureIdentityValue(entity))
                        return _Engine.Update(
                            _Aspect,
                            EnumerateValuesExcludingMember(_Aspect, entity, DataScope.Update, identityOrdinal),
                            new ClassFilterTerm(_Aspect, identityOrdinal, _Aspect[identityOrdinal].GetValue(entity)));
                    else //insert with identity
                    {
                        int result = _Engine.Insert(
                            _Aspect,
                            EnumerateValuesExcludingMember(_Aspect, entity, DataScope.Insert, identityOrdinal),
                            out identityValue);

                        _Aspect[identityOrdinal].SetValue(entity, identityValue);
                        return result;
                    }
                }
                else //non identity update
                {
                    foreach (int uniqueOrdinal in _Aspect.UniqueMemberOrdinals)
                    {
                        object uniqueValue = _Aspect[uniqueOrdinal].GetValue(entity);
                        if (!ValidationHelper.IsNullOrEmpty(uniqueValue))
                        {
                            ClassFilterTerm uniqueFilter = new ClassFilterTerm(_Aspect, uniqueOrdinal, uniqueValue);
                            if (_Engine.Exists(_Aspect, uniqueFilter))
                                return _Engine.Update(
                                    _Aspect,
                                    EnumerateValuesExcludingMember(_Aspect, entity, DataScope.Update, uniqueOrdinal),
                                    uniqueFilter);
                        }
                    }

                    //primary key update
                    ClassFilter pkFilter = _Aspect.CreatePrimaryKeyFilter(entity);
                    if (pkFilter != null)
                        if (_Engine.Exists(_Aspect, pkFilter))
                            return _Engine.Update(
                                _Aspect,
                                EnumerateValuesForUpdateByPrimaryKey(_Aspect, entity),
                                pkFilter);

                    //just can't update
                    return _Engine.Insert(
                        _Aspect,
                        EnumerateValuesExcludingMember(_Aspect, entity, DataScope.Insert, -1),
                        out identityValue);
                }
            }
            finally
            {
                _Mutex.ReleaseMutex();
            }
        }


        public T WithIdentity(object value)
        {
            if (_Aspect.IdentityOrdinal < 0)
                throw new ArgumentException(_Aspect.ToString() + " has no identity");

            using (IEnumerator<T> items = _Engine.Select<T>(
                this._Aspect,
                new ClassFilterTerm(_Aspect, _Aspect.IdentityOrdinal, value),
                null,
                0,
                0).GetEnumerator())
            {
                return items.MoveNext() ?
                    items.Current :
                    default(T);
            }
        }

        public T WithMember(string memberName, object value)
        {
            using (IEnumerator<T> items = _Engine.Select<T>(
                this._Aspect,
                new ClassFilterTerm(_Aspect, _Aspect.GetOrdinalOrException(memberName), value),
                null,
                0,
                0).GetEnumerator())
            {
                return items.MoveNext() ?
                    items.Current :
                    default(T);
            }
        }


        public ClassSelect<T> Search(string filter)
        {
            ClassFilter searchFilter = _Engine.CreateSearchFilter(this._Aspect, filter);
            return searchFilter != null ? Select().Where(searchFilter) : Select();
        }
    }
}
