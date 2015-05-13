using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.NFe
{
    /// <summary>
    /// informar o formato de impressão do DANFE: 
    /// 0 - Sem geração de DANFE;
    /// 1 - DANFE normal, Retrato;
    /// 2 - DANFE normal, Paisagem;
    /// 3 - DANFE Simplificado;
    /// 4 - DANFE NFC-e;
    /// 5 - DANFE NFC-e em mensagem eletrônica (o envio de mensagem eletrônica pode ser feita de forma simultânea com a impressão do DANFE; usar o tpImp - 5 quando esta for a única forma de disponibilização do DANFE). (novo domínio) [23-12-13]
    /// </summary>
    public enum TipoImpressaoDanfe
    {
        Sem_DANFE = 0,
        Danfe_Normal_Retrato = 1,
        Danfe_Normal_Paisagem = 2,
        Danfe_Simplificado = 3,
        Danfe_NFCe = 4,
        /// <summary>
        /// DANFE NFC-e em mensagem eletrônica (o envio de mensagem eletrônica pode ser feita de forma simultânea com a impressão do DANFE; usar o tpImp - 5 quando esta for a única forma de disponibilização do DANFE). (novo domínio) [23-12-13]
        /// </summary>
        Danfe_NFCe_Mensagem_Eletronica = 5
    }
}
