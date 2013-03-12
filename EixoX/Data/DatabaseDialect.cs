using System;
using System.Collections.Generic;
using System.Text;
using System.Data;


namespace EixoX.Data
{
    public interface DatabaseDialect
    {
        IDbConnection CreateConnection(string connectionString);
        bool CanOffsetRecords { get; }
        bool CanLimitRecords { get; }
        DatabaseCommand CreateDelete(DataAspect aspect, ClassFilter filter);
        DatabaseCommand CreateUpdate(DataAspect aspect, IEnumerable<AspectMemberValue> values, ClassFilter filter);
        DatabaseCommand CreateInsert(DataAspect aspect, IEnumerable<AspectMemberValue> values, out bool hasScopeIdentity);
        DatabaseCommand CreateInsert(DataAspect aspect, System.Collections.IEnumerable entities);
        DatabaseCommand CreateSelect(DataAspect aspect, ClassFilter filter, ClassSort sort, int pageSize, int pageOrdinal);
        DatabaseCommand CreateSelectMember(DataAspect aspect, int ordinal, ClassFilter filter, ClassSort sort, int pageSize, int pageOrdinal);
        DatabaseCommand CreateSelectCount(DataAspect aspect, ClassFilter filter);
        DatabaseCommand CreateSelectExists(DataAspect aspect, ClassFilter filter);

    }
}
