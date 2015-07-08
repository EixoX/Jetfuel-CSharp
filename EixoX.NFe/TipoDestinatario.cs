using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.NFe
{
    /// <summary>
    /// Indicador da IE do Destinatário
    /// Nota 1: No caso de NFC-e informar indIEDest=9 e não informar a tag IE do destinatário;
    /// Nota 2: No caso de operação com o Exterior informar indIEDest=9 e não informar a tag IE do destinatário;
    /// Nota 3: No caso de Contribuinte Isento de Inscrição (indIEDest=2), não informar a tag IE do destinatário.
    /// (campo novo) [23-12-13]
    /// </summary>
    public enum TipoDestinatario
    {

        /// <summary>
        /// 1 - Contribuinte ICMS (informar a tag IE do destinatário);
        /// </summary>
        Contribuinte_ICMS = 1,
        /// <summary>
        /// 2 - Contribuinte isento de Inscrição no cadastro de Contribuintes do ICMS - não informar a tag IE;
        /// </summary>
        Contribuinte_Isento_ICMS = 2,
        /// <summary>
        /// 9 - Não Contribuinte, que pode ou não possuir Inscrição Estadual no Cadastro de Contribuintes do ICMS - não informar a tag IE.
        /// </summary>
        Nao_Contribuinte_ICMS = 9
    }
}
