using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    /// <summary>
    /// A useful helper class for a paged select result.
    /// </summary>
    /// <typeparam name="TClass">The type of class returned.</typeparam>
    public sealed class ClassSelectResult<TClass> : List<TClass>
    {
        private readonly ClassSelect<TClass> _Select;
        private readonly int _pageSize;
        private readonly int _pageOrdinal;
        private readonly long _pageCount;
        private readonly long _recordCount;

        /// <summary>
        /// Constructs a new class select result.
        /// </summary>
        /// <param name="select">The select command</param>
        public ClassSelectResult(ClassSelect<TClass> select)
            : base(select)
        {
            this._Select = select;
            this._pageSize = select.PageSize;
            this._pageOrdinal = select.PageOrdinal;
            this._recordCount = select.Count();
            this._pageCount = _pageSize > 0 ? (long)(Math.Ceiling((double)(_recordCount) / (double)(_pageSize))) : 0;
        }

        /// <summary>
        /// Gets the original select command.
        /// </summary>
        public ClassSelect<TClass> Select { get { return this._Select; } }

        /// <summary>
        /// Gets the page size.
        /// </summary>
        public int PageSize
        {
            get { return this._pageSize; }
        }

        /// <summary>
        /// Gets the page ordinal.
        /// </summary>
        public int PageOrdinal
        {
            get { return this._pageOrdinal; }
        }

        /// <summary>
        /// Gets the page count.
        /// </summary>
        public long PageCount
        {
            get { return this._pageCount; }
        }

        /// <summary>
        /// Gets the record count.
        /// </summary>
        public long RecordCount
        {
            get { return this._recordCount; }
        }

        /// <summary>
        /// Indicates that it has more pages.
        /// </summary>
        public bool HasMorePages
        {
            get { return _pageOrdinal >= 0 && _pageOrdinal < (_pageCount - 1) && _recordCount != _pageSize; }
        }

        /// <summary>
        /// Gets the next page of the results.
        /// </summary>
        /// <returns>A new data select result.</returns>
        public ClassSelectResult<TClass> NextPage()
        {
            _Select.Page(_pageSize, _pageOrdinal + 1);
            return new ClassSelectResult<TClass>(_Select);
        }
        /// <summary>
        /// Indicates that it has previous pages.
        /// </summary>
        public bool HasPreviousPages
        {
            get { return _pageOrdinal > 0; }
        }


        public int FirstRecordOrdinal
        {
            get { return this._pageSize * this._pageOrdinal; }
        }

        public int LastRecordOrdinal
        {
            get { return (int)Math.Min(this._recordCount, _pageSize * (_pageOrdinal + 1)); }
        }
    }
}
