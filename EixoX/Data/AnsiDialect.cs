using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

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
        /// Appends a name to a string builder.
        /// </summary>
        /// <param name="builder">The builder to append to.</param>
        /// <param name="name">The name to write.</param>
        public virtual void AppendName(StringBuilder builder, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("aspect");

            if (name.IndexOfAny(InvalidNameChars) >= 0)
                throw new ArgumentException("Invalid name chars on " + name, "aspect");

            builder.Append(_NamePrefix);
            builder.Append(name);
            builder.Append(_NameSuffix);

        }


        /// <summary>
        /// Apppends a column value to a string builder.
        /// </summary>
        /// <param name="builder">The string builder to append to.</param>
        /// <param name="value">The value to append.</param>
        public virtual void AppendValue(StringBuilder builder, object value)
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
            else if (value is bool)
            {
                builder.Append('\'');
                builder.Append(Convert.ToInt32(value));
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

                    AppendValue(builder, o);
                }
                builder.Append(")");
            }
            else if (value is Enum)
            {
                builder.Append((int)value);
            }
            else
                throw new ArgumentException("Unable to serialize to sql " + value.GetType(), "value");
        }

        public virtual void AppendFilter(StringBuilder builder, DataAspect aspect, ClassFilter filter)
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

        public virtual void AppendFilterTerm(StringBuilder builder, DataAspect aspect, ClassFilterTerm term)
        {
            AppendName(builder, aspect[term.Ordinal].StoredName);
            switch (term.Comparison)
            {
                case FilterComparison.EqualTo:
                    if (term.Value == null)
                        builder.Append(" IS NULL");
                    else
                    {
                        builder.Append(" = ");
                        AppendValue(builder, term.Value);
                    }
                    break;
                case FilterComparison.GreaterOrEqual:
                    builder.Append(" >= ");
                    AppendValue(builder, term.Value);
                    break;
                case FilterComparison.GreaterThan:
                    builder.Append(" > ");
                    AppendValue(builder, term.Value);
                    break;
                case FilterComparison.InCollection:
                    builder.Append(" IN ");
                    AppendValue(builder, term.Value);
                    break;
                case FilterComparison.Like:
                    builder.Append(" LIKE ");
                    AppendValue(builder, term.Value);
                    break;
                case FilterComparison.LowerOrEqual:
                    builder.Append(" <= ");
                    AppendValue(builder, term.Value);
                    break;
                case FilterComparison.LowerThan:
                    builder.Append(" < ");
                    AppendValue(builder, term.Value);
                    break;
                case FilterComparison.NotEqualTo:
                    if (term.Value == null)
                        builder.Append(" IS NOT NULL");
                    else
                    {
                        builder.Append(" != ");
                        AppendValue(builder, term.Value);
                    }
                    break;
                case FilterComparison.NotInCollection:
                    builder.Append(" NOT IN ");
                    AppendValue(builder, term.Value);
                    break;
                case FilterComparison.NotLike:
                    builder.Append(" NOT LIKE ");
                    AppendValue(builder, term.Value);
                    break;
                default:
                    throw new ArgumentException("Unknown filter comparion " + term.Comparison, "term");
            }
        }

        public virtual void AppendFilterNodes(StringBuilder builder, DataAspect aspect, ClassFilterNode node)
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

        public virtual void AppendFilterExpression(StringBuilder builder, DataAspect aspect, ClassFilterExpression expression)
        {
            builder.Append("(");
            AppendFilterNodes(builder, aspect, expression.First);
            builder.Append(")");
        }

        public virtual void AppendSort(StringBuilder builder, DataAspect aspect, ClassSort sort)
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

        public virtual void AppendSortTerm(StringBuilder builder, DataAspect aspect, ClassSortTerm term)
        {
            AppendName(builder, aspect[term.Ordinal].StoredName);
            if (term.Direction == SortDirection.Descending)
                builder.Append(" DESC");
        }

        public virtual void AppendSortNodes(StringBuilder builder, DataAspect aspect, ClassSortNode node)
        {
            AppendSort(builder, aspect, node.Term);
            if (node.Next != null)
            {
                builder.Append(", ");
                AppendSortNodes(builder, aspect, node.Next);
            }
        }

        public virtual void AppendSortExpression(StringBuilder builder, DataAspect aspect, ClassSortExpression expression)
        {
            AppendSortNodes(builder, aspect, expression.First);
        }

        public virtual void AppendScopeIdentity(StringBuilder builder, DataAspect aspect)
        {
            builder.Append("SELECT MAX(");
            AppendName(builder, aspect.IdentityMember.StoredName);
            builder.Append(") FROM ");
            AppendName(builder, aspect.StoredName);
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
            AppendName(builder, aspect.StoredName);
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
            AppendName(builder, aspect.StoredName);
            builder.Append(" SET ");
            bool prependComma = false;
            foreach (AspectMemberValue value in values)
            {
                if (prependComma)
                    builder.Append(", ");
                else
                    prependComma = true;

                AppendName(builder, aspect[value.Ordinal].StoredName);
                builder.Append(" = ");
                AppendValue(builder, value.Value);
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
            AppendName(builder, aspect.StoredName);
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

                AppendName(builder, aspect[value.Ordinal].StoredName);
                AppendValue(valueBuilder, value.Value);
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
            AppendName(builder, aspect.StoredName);
            builder.Append("(");

            bool prependComma = false;

            for (int i = 0; i < count; i++)
                if (i != identityOrdinal)
                {
                    if (prependComma)
                        builder.Append(", ");
                    else
                        prependComma = true;

                    AppendName(builder, aspect[i].StoredName);
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


                        AppendValue(builder, aspect[i].GetValue(entity, DataScope.Insert));
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
            AppendName(builder, aspect[0].StoredName);
            for (int i = 1; i < count; i++)
            {
                builder.Append(", ");
                AppendName(builder, aspect[i].StoredName);
            }
            builder.Append(" FROM ");
            AppendName(builder, aspect.StoredName);
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
            AppendName(builder, aspect[ordinal].StoredName);
            builder.Append(" FROM ");
            AppendName(builder, aspect.StoredName);
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
            AppendName(builder, aspect.StoredName);
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
            AppendName(builder, aspect.StoredName);
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
