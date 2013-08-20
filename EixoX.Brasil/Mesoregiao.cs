using System;
using System.Collections.Generic;
using System.Text;
using EixoX.Data;
namespace EixoX.Brasil
{
    [DatabaseTable]
    public class Mesoregiao : BrasilDbModel<Mesoregiao>
    {
        [DatabaseColumn(DatabaseColumnKind.Unique)]
        public int CodigoIbge { get; set; }

        [DatabaseColumn]
        public int CodigoIbgeEstado { get; set; }

        [DatabaseColumn]
        public string Nome { get; set; }

    }
}
