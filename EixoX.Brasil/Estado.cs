using System;
using System.Collections.Generic;
using System.Text;
using EixoX.Data;

namespace EixoX.Brasil
{
    [DatabaseTable]
    public class Estado : BrasilDbModel<Estado>
    {
        [DatabaseColumn(DatabaseColumnKind.Unique)]
        public int CodigoIbge { get; set; }

        [DatabaseColumn(DatabaseColumnKind.Unique)]
        public int CodigoCep { get; set; }

        [DatabaseColumn]
        public string Sigla { get; set; }

        [DatabaseColumn]
        public string Nome { get; set; }

        public override string ToString()
        {
            return Sigla + " - " + Nome;
        }
    }
}
