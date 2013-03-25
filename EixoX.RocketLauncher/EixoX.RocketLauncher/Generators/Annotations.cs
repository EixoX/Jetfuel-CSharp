using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.RocketLauncher
{
    public static class Annotations
    {
        public static string DatabaseColumn
        {
            get { return "[DatabaseColumn]"; }
        }

        public static string DatabaseTable
        {
            get { return "[DatabaseTable]"; }
        }

        public static string DateGeneratorInsert
        {
            get { return "[GetDateGenerator(DataScope.Insert)]"; }
        }

        public static string DateGeneratorUpdate
        {
            get { return "[GetDateGenerator(DataScope.Update)]"; }
        }

        public static string MaxLength
        {
            get { return "[MaxLength({{value}})]"; }
        }
    }
}
