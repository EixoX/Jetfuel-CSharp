using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.NFe
{
    /// <summary>
    /// Informar o código da forma de emissão:
    /// 1 - Emissão normal (não em contingência);
    /// 2 - Contingência FS-IA, com impressão do DANFE em formulário de segurança;
    /// 3 - Contingência SCAN (Sistema de Contingência do Ambiente Nacional) Desativação prevista para 30/06/2014;
    /// 4 - Contingência DPEC (Declaração Prévia da Emissão em Contingência);
    /// 5 - Contingência FS-DA, com impressão do DANFE em formulário de segurança;
    /// 6 - Contingência SVC-AN (SEFAZ Virtual de Contingência do AN);
    /// 7 - Contingência SVC-RS (SEFAZ Virtual de Contingência do RS);
    /// 9 - Contingência off-line da NFC-e (as demais opções de contingência são válidas também para a NFC-e);
    /// Nota 1: Para a NFC-e somente estão disponíveis e são válidas as opções de contingência 5 e 9.
    /// Nota 2: SVC-AN e SVC-RS substituem o SCAN - NT 2013/007. (novo domínio) [23-12-13]
    /// </summary>
    public enum TipoEmissao
    {
        /// <summary>
        /// Emissão normal (não em contingência);
        /// </summary>
        Normal = 1,
        /// <summary>
        /// Contingência FS-IA, com impressão do DANFE em formulário de segurança;
        /// </summary>
        Contingencia_FS_IA = 2,
        /// <summary>
        /// Contingência SCAN (Sistema de Contingência do Ambiente Nacional) Desativação prevista para 30/06/2014;
        /// </summary>
        Contingencia_SCAN = 3,
        /// <summary>
        /// Contingência DPEC (Declaração Prévia da Emissão em Contingência);
        /// </summary>
        Contingencia_DPEC = 4,
        /// <summary>
        /// Contingência FS-DA, com impressão do DANFE em formulário de segurança;
        /// </summary>
        Contingencia_FS_DA = 5,
        /// <summary>
        /// Contingência SVC-AN (SEFAZ Virtual de Contingência do AN);
        /// </summary>
        Contingencia_SVC_AN = 6,
        /// <summary>
        /// Contingência SVC-RS (SEFAZ Virtual de Contingência do RS);
        /// </summary>
        Contingencia_SVC_RS = 7,
        /// <summary>
        /// Contingência off-line da NFC-e (as demais opções de contingência são válidas também para a NFC-e);
        /// </summary>
        Contingencia_Offline_NFCe = 9
    }
}
