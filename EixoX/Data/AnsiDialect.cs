using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using EixoX.Reflection;
/*
 * First Author: Rodrigo Portela (rodrigo.portela@gmail.com) in 2013-01-01
 */
namespace EixoX.Data
{
    /// <summary>
    /// Represents an abstract database dialect based on an ansi dialect.
    /// </summary>
    public abstract class AnsiDialect : DatabaseDialect
    {
        private readonly char _NamePrefix;
        private readonly char _NameSuffix;
        private static readonly char[] InvalidNameChars = new char[] { '@', '-', '!', '\'', '"', '+', '{', '}' };

        /// <summary>
        /// Constructs an ansi dialect.
        /// </summary>
        /// <param name="namePrefix">The char to use as prefix for names.</param>
        /// <param name="nameSuffix">The char to use as suffix for names.</param>
        public AnsiDialect(char namePrefix, char nameSuffix)
        {
            this._NamePrefix = namePrefix;
            this._NameSuffix = nameSuffix;
        }

        /// <summary>
        /// Appends a table name to a string builder.
        /// </summary>
        /// <param name="builder">The builder to append to.</param>
        /// <param name="aspect">The aspect to extract the storage name from.</param>
        protected virtual void AppendTableName(StringBuilder builder, DataAspect aspect)
        {
            string tableName = aspect.StoredName;

            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException("aspect");

            if (tableName.IndexOfAny(InvalidNameChars) >= 0)
                throw new ArgumentException("Invalid name chars on " + tableName, "aspect");

            builder.Append(_NamePrefix);
            builder.Append(tableName);
            builder.Append(_NameSuffix);

        }

        /// <summary>
        /// Appends a column name to a string builder.
        /// </summary>
        /// <param name="builder">The builder to append to.</param>
        /// <param name="aspect">The data aspect of the class.</param>
        /// <param name="ordinal">The ordinal position of the member.</param>
        protected virtual void AppendColumnName(StringBuilder builder, DataAspect aspect, int ordinal)
        {
            string columnName = aspect[ordinal].StoredName;

            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("ordinal");

            if (columnName.IndexOfAny(InvalidNameChars) >= 0)
                throw new ArgumentException("Invalid name chars on " + columnName, "ordinal");

            builder.Append(_NamePrefix);
            builder.Append(columnName);
            builder.Append(_NameSuffix);
        }

        /// <summary>
        /// Apppends a column value to a string builder.
        /// </summary>
        /// <param name="builder">The string builder to append to.</param>
        /// <param name="value">The value to append.</param>
        protected virtual void AppendColumnValue(StringBuilder builder, object value)
        {
            if (value == null)
                builder.Append("NULL");
            else if (value is string || value is char)
            {
                builder.Append('\'');
                builder.Append(value.ToString().Replace("'", "''"));
                builder.Append('\'');
            }
            else if (ValidationHelper.IsNumericDiscrete(value))
            {
                builder.Append(value);
            }
            else if (ValidationHelper.IsNumericFloatingPoint(value))
            {
                builder.Append(((IFormattable)value).ToString(null, System.Globalization.CultureInfo.InvariantCulture));
            }
            else if (value is DateTime)
            {
                builder.Append('\'');
                builder.Append(((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss"));
                builder.Append('\'');
            }
            else if (value is TimeSpan || value is Guid)
            {
                builder.Append('\'');
                builder.Append(value);
                builder.Append('\'');
            }
            else if (value is byte[])
            {
                builder.Append('\'');
                builder.Append(Convert.ToBase64String((byte[])value));
                builder.Append('\'');
            }
            else if (value is System.Collections.IEnumerable)
            {
                builder.Append("(");
                bool prependComma = false;
                foreach (object o in ((System.Collections.IEnumerable)value))
                {
                    if (prependComma)
                        builder.Append(", ");
                    else
                        prependComma = true;

                    AppendColumnValue(builder, o);
                }
                builder.Append(")");
            }
            else
                throw new ArgumentException("Unable to serialize to sql " + value.GetType(), "value");
        }

        protected virtual void AppendFilter(StringBuilder builder, DataAspect aspect, ClassFilter filter)
        {
            if (filter is ClassFilterTerm)
                AppendFilterTerm(builder, aspect, (ClassFilterTerm)filter);
            else if (filter is ClassFilterNode)
                AppendFilterNodes(builder, aspect, (ClassFilterNode)filter);
            else if (filter is ClassFilterExpression)
                AppendFilterExpression(builder, aspect, (ClassFilterExpression)filter);
            else
                throw new ArgumentException("Unknown filter type: " + filter.GetType(), "filter");
        }

        protected virtual void AppendFilterTerm(StringBuilder builder, DataAspect aspect, ClassFilterTerm term)
        {
            AppendColumnName(builder, aspect, term.Ordinal);
            switch (term.Comparison)
            {
                case FilterComparison.EqualTo:
                    if (term.Value == null)
                        builder.Append(" IS NULL");
                    else
                    {
                        builder.Append(" = ");
                        AppendColumnValue(builder, term.Value);
                    }
                    break;
                case FilterComparison.GreaterOrEqual:
                    builder.Append(" >= ");
                    AppendColumnValue(builder, term.Value);
                    break;
                case FilterComparison.GreaterThan:
                    builder.Append(" > ");
                    AppendColumnValue(builder, term.Value);
                    break;
                case FilterComparison.InCollection:
                    builder.Append(" IN ");
                    AppendColumnValue(builder, term.Value);
                    break;
                case FilterComparison.Like:
                    builder.Append(" LIKE ");
                    AppendColumnValue(builder, term.Value);
                    break;
                case FilterComparison.LowerOrEqual:
                    builder.Append(" <= ");
                    AppendColumnValue(builder, term.Value);
                    break;
                case FilterComparison.LowerThan:
                    builder.Append(" < ");
                    AppendColumnValue(builder, term.Value);
                    break;
                case FilterComparison.NotEqualTo:
                    if (term.Value == null)
                        builder.Append(" IS NOT NULL");
                    else
                    {
                        builder.Append(" != ");
                        AppendColumnValue(builder, term.Value);
                    }
                    break;
                case FilterComparison.NotInCollection:
                    builder.Append(" NOT IN ");
                    AppendColumnValue(builder, term.Value);
                    break;
                case FilterComparison.NotLike:
                    builder.Append(" NOT LIKE ");
                    AppendColumnValue(builder, term.Value);
                    break;
                default:
                    throw new ArgumentException("Unknown filter comparion " + term.Comparison, "term");
            }
        }

        protected virtual void AppendFilterNodes(StringBuilder builder, DataAspect aspect, ClassFilterNode node)
        {
            AppendFilter(builder, aspect, node.Filter);
            if (node.Next != null)
            {
                switch (node.Operation)
                {
                    case FilterOperation.And:
                        builder.Append(" AND ");
                        break;
                    case FilterOperation.Or:
                        builder.Append(" OR ");
                        break;
                    default:
                        throw new ArgumentException("Unknown filter operation " + node.Operation, "node");
                }

                AppendFilterNodes(builder, aspect, node.Next);
            }
        }

        protected virtual void AppendFilterExpression(StringBuilder builder, DataAspect aspect, ClassFilterExpression expression)
        {
            builder.Append("(");
            AppendFilterNodes(builder, aspect, expression.First);
            builder.Append(")");
        }

        protected virtual void AppendSort(StringBuilder builder, DataAspect aspect, ClassSort sort)
        {
            if (sort is ClassSortNode)
                AppendSortNodes(builder, aspect, (ClassSortNode)sort);
            else if (sort is ClassSortTerm)
                AppendSortTerm(builder, aspect, (ClassSortTerm)sort);
            else if (sort is ClassSortExpression)
                AppendSortExpression(builder, aspect, (ClassSortExpression)sort);
            else
                throw new ArgumentException("Unknown sort type " + sort.GetType(), "sort");
        }

        protected virtual void AppendSortTerm(StringBuilder builder, DataAspect aspect, ClassSortTerm term)
        {
            AppendColumnName(builder, aspect, term.Ordinal);
            if (term.Direction == SortDirection.Ascending)
                builder.Append(" DESC");
        }

        protected virtual void AppendSortNodes(StringBuilder builder, DataAspect aspect, ClassSortNode node)
        {
            AppendSort(builder, aspect, node.Term);
            if (node.Next != null)
            {
                builder.Append(", ");
                AppendSortNodes(builder, aspect, node.Next);
            }
        }

        protected virtual void AppendSortExpression(StringBuilder builder, DataAspect aspect, ClassSortExpression expression)
        {
            AppendSortNodes(builder, aspect, expression.First);
        }

        protected virtual void AppendScopeIdentity(StringBuilder builder, DataAspect aspect)
        {
            builder.Append("SELECT MAX(");
            AppendColumnName(builder, aspect, aspect.IdentityOrdinal);
            builder.Append(") FROM ");
            AppendTableName(builder, aspect);
        }

        public abstract IDbConnection CreateConnection(string connectionString);

        public virtual bool CanOffsetRecords
        {
            get { return false; }
        }

        public virtual bool CanLimitRecords
        {
            get { return false; }
        }

        public virtual DatabaseCommand CreateDelete(DataAspect aspect, ClassFilter filter)
        {
            StringBuilder builder = new StringBuilder(255);
            builder.Append("DELETE FROM ");
            AppendTableName(builder, aspect);
            if (filter != null)
            {
                builder.Append(" WHERE ");
                AppendFilter(builder, aspect, filter);
            }
            return new DatabaseCommand(
                CommandType.Text,
                builder.ToString(),
                null);
        }

        public virtual DatabaseCommand CreateUpdate(DataAspect aspect, IEnumerable<AspectMemberValue> values, ClassFilter filter)
        {
            StringBuilder builder = new StringBuilder(512);
            builder.Append("UPDATE ");
            AppendTableName(builder, aspect);
            builder.Append(" SET ");
            bool prependComma = false;
            foreach (AspectMemberValue value in values)
            {
                if (prependComma)
                    builder.Append(", ");
                else
                    prependComma = true;

                AppendColumnName(builder, aspect, value.Ordinal);
                builder.Append(" = ");
                AppendColumnValue(builder, value.Value);
            }
            if (filter != null)
            {
                builder.Append(" WHERE ");
                AppendFilter(builder, aspect, filter);
            }
            return new DatabaseCommand(
                CommandType.Text,
                builder.ToString(),
                null);
        }

        public virtual DatabaseCommand CreateInsert(DataAspect aspect, IEnumerable<AspectMemberValue> values, out bool hasScopeIdentity)
        {
            StringBuilder builder = new StringBuilder(512);
            StringBuilder valueBuilder = new StringBuilder(255);
            builder.Append("INSERT INTO  ");
            AppendTableName(builder, aspect);
            builder.Append(" (");

            bool prependComma = false;
            foreach (AspectMemberValue value in values)
            {
                if (prependComma)
                {
                    builder.Append(", ");
                    valueBuilder.Append(", ");
                }
                else
                    prependComma = true;

                AppendColumnName(builder, aspect, value.Ordinal);
                AppendColumnValue(valueBuilder, value.Value);
            }

            builder.Append(") VALUES (");
            builder.Append(valueBuilder);

            if (aspect.HasIdentity)
            {
                hasScopeIdentity = true;
                builder.Append("); ");
                AppendScopeIdentity(builder, aspect);
            }
            else
            {
                hasScopeIdentity = false;
                builder.Append(")");
            }

            return new DatabaseCommand(
                CommandType.Text,
                builder.ToString(),
                null);
        }

        public virtual DatabaseCommand CreateInsert(DataAspect aspect, System.Collections.IEnumerable entities)
        {
            StringBuilder builder = new StringBuilder(1024);
            int identityOrdinal = aspect.IdentityOrdinal;
            int count = aspect.Count;

            builder.Append("INSERT INTO ");
            AppendTableName(builder, aspect);
            builder.Append("(");

            bool prependComma = false;

            for (int i = 0; i < count; i++)
                if (i != identityOrdinal)
                {
                    if (prependComma)
                        builder.Append(", ");
                    else
                        prependComma = true;

                    AppendColumnName(builder, aspect, i);
                }

            builder.Append(") VALUES ");
            prependComma = false;

            foreach (object entity in entities)
            {
                if (prependComma)
                    builder.Append(", ");
                else
                    prependComma = true;

                builder.Append("(");

                bool innerComma = false;
                for (int i = 0; i < count; i++)
                    if (i != identityOrdinal)
                    {
                        if (innerComma)
                            builder.Append(", ");
                        else
                            innerComma = true;


                        AppendColumnValue(builder, aspect[i].GetValue(entity, DataScope.Insert));
                    }

                builder.Append(")");
            }



            return new DatabaseCommand(
                CommandType.Text,
                builder.ToString(),
                null);
        }

        public virtual DatabaseCommand CreateSelect(DataAspect aspect, ClassFilter filter, ClassSort sort, int pageSize, int pageOrdinal)
        {
            StringBuilder builder = new StringBuilder(255);
            int count = aspect.Count;

            builder.Append("SELECT ");
            AppendColumnName(builder, aspect, 0);
            for (int i = 1; i < count; i++)
            {
                builder.Append(", ");
                AppendColumnName(builder, aspect, i);
            }
            builder.Append(" FROM ");
            AppendTableName(builder, aspect);
            if (filter != null)
            {
                builder.Append(" WHERE ");
                AppendFilter(builder, aspect, filter);
            }
            if (sort != null)
            {
                builder.Append(" ORDER BY ");
                AppendSort(builder, aspect, sort);
            }
            return new DatabaseCommand(
                CommandType.Text,
                builder.ToString(),
                null);
        }

        public virtual DatabaseCommand CreateSelectMember(DataAspect aspect, int ordinal, ClassFilter filter, ClassSort sort, int pageSize, int pageOrdinal)
        {
            StringBuilder builder = new StringBuilder(255);
            int count = aspect.Count;

            builder.Append("SELECT ");
            AppendColumnName(builder, aspect, ordinal);
            builder.Append(" FROM ");
            AppendTableName(builder, aspect);
            if (filter != null)
            {
                builder.Append(" WHERE ");
                AppendFilter(builder, aspect, filter);
            }
            if (sort != null)
            {
                builder.Append(" ORDER BY ");
                AppendSort(builder, aspect, sort);
            }
            return new DatabaseCommand(
                CommandType.Text,
                builder.ToString(),
                null);
        }

        public virtual DatabaseCommand CreateSelectCount(DataAspect aspect, ClassFilter filter)
        {
            StringBuilder builder = new StringBuilder(255);
            int count = aspect.Count;

            builder.Append("SELECT COUNT(*) FROM ");
            AppendTableName(builder, aspect);
            if (filter != null)
            {
                builder.Append(" WHERE ");
                AppendFilter(builder, aspect, filter);
            }
            return new DatabaseCommand(
                CommandType.Text,
                builder.ToString(),
                null);
        }

        public virtual DatabaseCommand CreateSelectExists(DataAspect aspect, ClassFilter filter)
        {
            StringBuilder builder = new StringBuilder(255);
            int count = aspect.Count;

            builder.Append("SELECT 1 WHERE EXISTS (SELECT * FROM ");
            AppendTableName(builder, aspect);
            if (filter != null)
            {
                builder.Append(" WHERE ");
                AppendFilter(builder, aspect, filter);
            }
            builder.Append(")");
            return new DatabaseCommand(
                CommandType.Text,
                builder.ToString(),
                null);
        }
    }
}
