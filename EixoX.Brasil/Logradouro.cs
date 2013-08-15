using System;
using System.Collections.Generic;
using System.Text;
using EixoX.Data;
namespace EixoX.Brasil
{

    [DatabaseTable]
    public class Logradouro : BrasilDbModel<Logradouro>
    {
        [DatabaseColumn(DatabaseColumnKind.Unique)]
        public int Cep { get; set; }
        [DatabaseColumn]
        public int CodigoCepLogradouro { get; set; }
        [DatabaseColumn]
        public int CodigoCepBairro { get; set; }
        [DatabaseColumn]
        public int CodigoCepTipo { get; set; }
        [DatabaseColumn]
        public string Nome { get; set; }

    }
}
