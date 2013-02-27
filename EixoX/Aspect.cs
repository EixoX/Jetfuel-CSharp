using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX
{
    public interface Aspect
    {
        Type DataType { get; }
        string Name { get; }
        string FullName { get; }
        int Count { get; }
        AspectMember GetMember(int ordinal);
        AspectMember GetMember(string name);
        int GetOrdinal(string name);
        int GetOrdinalOrException(string name);
        bool HasMember(string name);
        IEnumerable<AspectMember> GetMembers();
    }
}
