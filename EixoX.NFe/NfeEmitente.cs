using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EixoX.Restrictions;

namespace EixoX.NFe
{
    public class NfeEmitente
    {
        /// <summary>
        /// Informar o CNPJ do emitente, sem formatação ou máscara
        /// </summary>
        [CpfOrCnpj]
        [Required]
        public long cnpjCpf;
        /// <summary>
        /// Informar a razão social do emitente
        /// </summary>
        [Length(2, 60)]
        public string razaoSocial;
        /// <summary>
        /// Informar o nome fantasia do emitente, pode ser omitido
        /// </summary>
        [MaxLength(60)]
        public string nomeFantasia;
        /// <summary>
        /// Informar o logradouro do emitente
        /// </summary>
        [Length(2, 60)]
        public string logradouro;
        /// <summary>
        /// Informar o número do endereço do emitente, campo obrigatório. Informar S/N ou . (ponto) ou - (traço) 
        /// para evitar falha de schema XML quando não houver número.
        /// </summary>
        [Length(1, 60)]
        public string numero;

        /// <summary>
        /// Informar o complemento do endereço do emitente, pode ser omitido
        /// </summary>
        [MaxLength(60)]
        public string complemento;

        /// <summary>
        /// Informar o bairro do endereço do emitente
        /// </summary>
        [Length(2, 60)]
        public string bairro;

        /// <summary>
        /// Informar o código do município na codificação do IBGE com 7 dígitos
        /// </summary>
        [Required]
        public int municipioIbge;

        /// <summary>
        /// Informar o nome do município
        /// </summary>
        [Length(2, 60)]
        public string municipio;

        /// <summary>
        /// Informar a sigla da UF
        /// </summary>
        [Length(2)]
        public string uf;

        /// <summary>
        /// Informar o CEP, passou a ser de informação obrigatória a partir da NT 2011/004.
        /// </summary>
        [Required]
        public int cep;

        /// <summary>
        /// Informar o telefone com DDD + número, sem formatação (campo com tamanho aumentado)
        /// </summary>
        public long fone;

        /// <summary>
        /// Informar a IE do emitente, sem formatação ou máscara
        /// </summary>
        [Required]
        public long ie;

        /// <summary>
        /// Informar a IE ST, sem formatação ou máscara, quando praticar alguma operação como substituto tributário
        /// </summary>
        public long ieST;

        /// <summary>
        /// Informar a Inscrição Municipal, sem formatação ou máscara, quando emitir NF conjugada (prestação de serviço com fornecimento de peças)
        /// </summary>
        public long im;

        /// <summary>
        /// Informar o CNAE Fiscal, este campo deve ser informado em conjunto com o campo IM e vice-versa, a informação de um e omissão do outro resulta em falha de Schema XML
        /// </summary>
        public int cnae;

        /// <summary>
        /// Informar o Código de Regime Tributário - CRT, valores válidos: 1 - Simples Nacional; 2 - Simples Nacional - excesso de sublimite de receita bruta; 3 - Regime Normal (campo novo).
        /// </summary>
        [Required]
        public int crt;



    }
}
