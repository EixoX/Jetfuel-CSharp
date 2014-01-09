using System;
using System.Collections.Generic;
using System.Text;
using EixoX.Data;

namespace EixoX.Brasil
{
    [DatabaseTable]
    public class Cidade : BrasilDbModel<Cidade>
    {
        [DatabaseColumn(DatabaseColumnKind.Unique)]
        public int CodigoCepCidade { get; set; }
        [DatabaseColumn(DatabaseColumnKind.Normal, "CodigoIbge")]
        public int CodigoIbgeCidade { get; set; }
        [DatabaseColumn]
        public int CodigoCepEstado { get; set; }
        [DatabaseColumn]
        public int CodigoIbgeEstado { get; set; }
        [DatabaseColumn]
        public string Nome { get; set; }
        [DatabaseColumn]
        public string SiglaEstado { get; set; }

        public override string ToString()
        {
            return this.Nome;
        }

    }
}
