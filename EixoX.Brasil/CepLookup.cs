using System;
using System.Collections.Generic;
using System.Text;
using EixoX.Data;
namespace EixoX.Brasil
{
    [DatabaseTable]
    public class CepLookup : BrasilDbModel<CepLookup>
    {
        [DatabaseColumn(DatabaseColumnKind.Unique)]
        public int Cep { get; set; }

        [DatabaseColumn]
        public int CodigoCepEstado { get; set; }

        [DatabaseColumn]
        public int CodigoCepCidade { get; set; }

        [DatabaseColumn]
        public int CodigoCepBairro { get; set; }

        [DatabaseColumn]
        public int CodigoIbgeCidade { get; set; }

        [DatabaseColumn]
        public int CodigoIbgeEstado { get; set; }

        [DatabaseColumn]
        public string Estado { get; set; }
        [DatabaseColumn]
        public string SiglaEstado { get; set; }
        [DatabaseColumn]
        public string Cidade { get; set; }
        [DatabaseColumn]
        public string Bairro { get; set; }
        [DatabaseColumn]
        public string Logradouro { get; set; }


        public static CepLookup FromCep(int cep)
        {
            return cep < 1000 ? null : BrasilDb<CepLookup>.Instance.WithMember("Cep", cep);
        }
    }
}
