using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX
{
    public class UsecaseResult
    {
        public UsecaseResultType ResultType { get; set; }
        public object Result { get; set; }
        public Exception Exception { get; set; }
        public string Message { get; set; }
    }
}
