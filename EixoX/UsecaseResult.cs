using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX
{
    public class UsecaseResult<T>
    {
        public UsecaseResultType ResultType { get; set; }
        public T Result { get; set; }
        public Exception Exception { get; set; }
        public string Message { get; set; }
    }
}
