using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.NFe
{
    /// <summary>
    /// Informar o indicador de presença do comprador no estabelecimento comercial no momento da operação: 
    /// 0 - Não se aplica (por exemplo, Nota Fiscal complementar ou de ajuste);
    /// 1 - Operação presencial;
    /// 2 - Operação não presencial, pela Internet;
    /// 3 - Operação não presencial, Teleatendimento;
    /// 4 - NFC-e em operação com entrega a domicílio;
    /// 9 - Operação não presencial, outros.
    /// </summary>
    public enum TipoPresencaConsumidorFinal
    {
        /// <summary>
        /// 0 - Não se aplica (por exemplo, Nota Fiscal complementar ou de ajuste);
        /// </summary>
        Nao_Se_Aplica = 0,
        /// <summary>
        /// 1 - Operação presencial;
        /// </summary>
        Operacao_Presencial = 1,
        /// <summary>
        /// 2 - Operação não presencial, pela Internet;
        /// </summary>
        Nao_Presencial_Internet = 2,
        /// <summary>
        /// 3 - Operação não presencial, Teleatendimento;
        /// </summary>
        Nao_Presencial_Teleatendimento = 3,
        /// <summary>
        /// 4 - NFC-e em operação com entrega a domicílio;
        /// </summary>
        Nao_Presencial_NFCe_Entrega_Domicilio = 4,
        /// <summary>
        /// 9 - Operação não presencial, outros.
        /// </summary>
        Nao_Presencial_Outros = 9
    }
}
