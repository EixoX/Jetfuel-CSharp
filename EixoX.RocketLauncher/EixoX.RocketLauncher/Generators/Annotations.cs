using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.RocketLauncher
{
    /// <summary>
    /// Annotations available for using in attributes
    /// </summary>
    public static class Annotations
    {
        public static string CustomDatabaseColumn(string name)
        {
            return String.Format("[DatabaseColumn(\"{0}\")]", name);
        }

        public static string DatabaseColumn
        {
            get { return "[DatabaseColumn]"; }
        }

        public static string DatabaseIdentityColumn
        {
            get { return "[DatabaseColumn(DatabaseColumnKind.Identity)]"; }
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

        public static string UISingleLine
        {
            get { return "[UISingleline]"; }
        }

        public static string MaxLength
        {
            get { return "[MaxLength({{value}})]"; }
        }

        public static string UIHidden 
        {
            get { return "[UIHidden]"; }
        }
    }
}
