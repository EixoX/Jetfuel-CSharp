using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EixoX.Restrictions;

namespace EixoX.NFe
{
    public class Nfe
    {
        public string versao;
        public string id;
        public string item;

        /// <summary>
        /// Informar o código da UF do emitente do Documento Fiscal, utilizar a codificação do IBGE (Ex. SP->35, RS->43, etc.)
        /// </summary>
        [Required]
        public int emitenteUf;

        /// <summary>
        /// Informar o código numérico que compõe a Chave de Acesso. 
        /// Número aleatório gerado pelo emitente para cada NF-e para evitar acessos indevidos da NF-e.
        /// </summary>
        [Required]
        public int nfChaveAcesso;

        /// <summary>
        /// informar a natureza da operação de que decorrer a saída ou a entrada, tais como: venda, compra, transferência, 
        /// devolução, importação, consignação, remessa (para fins de demonstração, de industrialização outra), conforme previsto na alínea 'i', 
        /// inciso I, art. 19 do CONVÊNIO S/Nº, de 15 de dezembro de 1970.
        /// </summary>
        [Length(1, 60)]
        public string nfeNaturezaOperacao;

        /// <summary>
        /// Informar o indicador da forma de pagamento:
        /// 0 - pagamento à vista;
        /// 1 - pagamento à prazo;
        /// 2 - outros.
        /// </summary>
        public TipoFormaPagamento nfePagamento;

        /// <summary>
        /// Informar o código do Modelo do Documento Fiscal, código 55 para a NF-e ou código 65 para NFC-e. [23-12-13]
        /// </summary>
        public TipoModeloNfe nfeModelo;

        /// <summary>
        /// Informar a série do Documento Fiscal, informar 0 (zero) para série única. A emissão normal pode utilizar série de 0-889, a emissão em contingência SCAN deve utilizar série 900-999.
        /// </summary>
        [MinInclusive(0)]
        [MaxInclusive(999)]
        public int nfeSerie;

        /// <summary>
        /// Informar o Número do Documento Fiscal.
        /// </summary>
        [Required]
        public int nfeNumero;

        /// <summary>
        /// Informar a data de emissão do Documento Fiscal no padrão UTC - Universal Coordinated Time, onde TZD pode ser -02:00 (Fernando de Noronha), -03:00 (Brasília) ou -04:00(Manaus), no horário de verão serão - 01:00, -02:00 e -03:00. Ex.: 2010-08-19T13:00:15-03:00.
        /// Nota 1: No caso da NF-e, a informação da Hora de Emissão é opcional, podendo ser informada com zeros.
        /// Nota 2: A emissão da NFC-e deve ocorrer de forma on-line, realtime, com uma tolerância de até 5 minutos, devido ao sincronismo de horário do servidor da Empresa e o servidor da SEFAZ.
        /// Nota 3: A tolerância acima motivada pelo horário dos servidores, somada ao atraso permitido para a autorização da NFC-e acaba resultando em um atraso máximo de 10 minutos a ser controlado pela aplicação da SEFAZ. (alteração no tipo do campo) [23-12-13]
        /// </summary>
        [Required]
        public DateTime nfeDataEmissao;

        /// <summary>
        /// Informar a data e hora de Saída ou da Entrada da Mercadoria/Produto no padrão UTC - Universal Coordinated Time, onde TZD pode ser -02:00 (Fernando de Noronha), -03:00 (Brasília) ou -04:00(Manaus), no horário de verão serão - 01:00, -02:00 e -03:00. Ex.: 2010-08-19T13:00:15-03:00.
        /// Nota: Para a NFC-e este campo não deve existir.
        /// </summary>
        [Required]
        public DateTime nfeDataSaidaEntrada;

        /// <summary>
        /// Informar o código do tipo do Documento Fiscal:
        /// 0 - entrada;
        /// 1 - saída.
        /// </summary>
        public TipoNfe nfeTipo;


        /// <summary>
        /// Informar o identificador de local de destino da operação:
        /// 1 - Operação interna;
        /// 2 - Operação interestadual;
        /// 3 - Operação com exterior.
        /// (campo novo) [23-12-13]
        /// </summary>
        public TipoDestinoOperacao nfeDestinoOperacao;

        /// <summary>
        /// Informar o código do Município de Ocorrência do Fato Gerador do ICMS, que é o local onde ocorre a entrada ou saída da mercadoria, utilizar a Tabela do IBGE.
        /// </summary>
        public int icmsMunicipioOcorrencia;


        /// <summary>
        /// Informar o município de ocorrência do fato gerador do ICMS do transporte. Utilizar a Tabela do IBGE.
        /// </summary>
        public int icmsTransporteCodigoMunicipioGerador;

        /// <summary>
        /// Informar o código da forma de emissão
        /// </summary>
        public TipoFinalidadeNfe finalidade;

        /// <summary>
        /// Informar o código do dígito verificador - DV da Chave de Acesso da NF-e, o DV será calculado com a aplicação do algoritmo módulo 11 (base 2,9) da Chave de Acesso.
        /// </summary>
        public int nfeChaveAcessoDigitoVerificador;


    }
}
