using System;
using System.Collections.Generic;
using System.Text;
using EixoX.Data;

namespace EixoX.Brasil
{
    [DatabaseTable]
    public class Microregiao : BrasilDbModel<Microregiao>
    {
        [DatabaseColumn(DatabaseColumnKind.Unique)]
        public int CodigoIbge { get; set; }
        [DatabaseColumn]
        public int CodigoIbgeEstado { get; set; }
        [DatabaseColumn]
        public int CodigoIbgeMesoregiao { get; set; }
        [DatabaseColumn]
        public string Nome { get; set; }

    }
}
