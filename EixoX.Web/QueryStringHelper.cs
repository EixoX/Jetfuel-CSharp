using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.Web
{
    public class QueryStringHelper
    {
        public static string Build(params Tuple<string, string>[] tuples)
        {
            string[] parameters = new string[tuples.Length];
            for (int i = 0; i < parameters.Length; i++)
                parameters[i] = tuples[i].Item1 + "=" + tuples[i].Item2;

            return String.Join("&", parameters);
        }
    }
}
