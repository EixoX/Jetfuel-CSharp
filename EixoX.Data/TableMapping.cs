using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Data
{
    public interface TableMapping : IEnumerable<ColumnMapping>
    {
        int Count { get; }

        ColumnMapping GetColumnMapping(int ordinal);
        ColumnMapping GetColumnMapping(string name);

        int GetIdentityOrdinal();
        ColumnMapping GetIdentityMapping();

        IEnumerable<int> GetUniqueOrdinals();
        IEnumerable<ColumnMapping> GetUniqueMappings();
        IEnumerable<string> GetUniqueNames();
        int GetUniqueCount();

        IEnumerable<int> GetPrimaryKeyOrdinals();
        IEnumerable<ColumnMapping> GetPrimaryKeyMappings();
        IEnumerable<string> GetPrimaryKeyNames();
        int GetPrimaryKeyCount();

    }
}
