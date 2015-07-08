using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.NFe
{
    /// <summary>
    /// Informar o código de identificação do processo de emissão da NF-e: Identificador do processo de emissão da NF-e:
    /// </summary>
    public enum TipoProcessoEmissao
    {
        /// <summary>
        /// 0 - emissão de NF-e com aplicativo do contribuinte;
        /// </summary>
        Aplicativo_Contribuinte = 0,
        /// <summary>
        /// 1 - emissão de NF-e avulsa pelo Fisco;
        /// </summary>
        Avulsa_Fisco = 1,
        /// <summary>
        /// 2 - emissão de NF-e avulsa, pelo contribuinte com seu certificado digital, através do site do Fisco;
        /// </summary>
        Avulsa_Contribuinte_Site_Fisco = 2,
        /// <summary>
        /// 3 - emissão NF-e pelo contribuinte com aplicativo fornecido pelo Fisco.
        /// </summary>
        Aplicativo_Fisco = 3
    }

}
