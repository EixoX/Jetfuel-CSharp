using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.NFe
{
    /// <summary>
    /// Informar o identificador de local de destino da operação:
    /// 1 - Operação interna;
    /// 2 - Operação interestadual;
    /// 3 - Operação com exterior.
    /// (campo novo) [23-12-13]
    /// </summary>
    public enum TipoDestinoOperacao
    {
        Operacao_Interna = 1,
        Operacao_Interestadual = 2,
        Operacao_Exterior = 3
    }
}
