using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.NFe
{
    /// <summary>
    /// A TAG finNFe - Finalidade de emissão da NF-e.
    /// Na legislação vigente não há definições explícitas de quando deve ser 
    /// utilizado a finalidade 2 (NF-e complementar) e quando deverá ser utilizado a finalidade 3 (NF-e ajuste).
    /// </summary>
    public enum TipoFinalidadeNfe
    {
        Normal = 1,
        /// <summary>
        /// A nota complementar de imposto emitida pelo programa Cálculo de Notas Fiscais (FT4003). 
        /// Sendo neste caso, obrigatório referenciar nota de origem.
        /// </summary>
        Complementar = 2,
        /// <summary>
        /// A mesma será gerada como 3 (NF-e de ajuste) para nota complementar de entrada e nota de diferença de preço.
        /// </summary>
        Ajuste = 3,
        Devolucao_Mercadoria = 4
    }
}
