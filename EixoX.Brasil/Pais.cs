using System;
using System.Collections.Generic;
using System.Text;
using EixoX.Data;

namespace EixoX.Brasil
{
    [DatabaseTable]
    public class Pais : BrasilDbModel<Pais>
    {
        [DatabaseColumn(DatabaseColumnKind.Unique)]
        public int CodigoBacen { get; set; }

        [DatabaseColumn]
        public string Nome { get; set; }
    }
}
