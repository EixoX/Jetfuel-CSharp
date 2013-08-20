using EixoX.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace EixoX.Brasil
{
    [DatabaseTable]
    public class Bairro : BrasilDbModel<Bairro>
    {
        [DatabaseColumn]
        public int CodigoCepBairro { get; set; }
        [DatabaseColumn]
        public int CodigoCepCidade { get; set; }
        [DatabaseColumn]
        public string Nome { get; set; }
    }
}
