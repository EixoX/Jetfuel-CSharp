using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EixoX.Restrictions;

namespace EixoX.NFe
{
    public class NfeDestinatario
    {
        [CpfOrCnpj]
        [Required]
        ///informar o CNPJ do destinatário, sem formatação ou máscara
        public long cnpjCpf;

        /// <summary>
        /// No caso de operação com o exterior, ou para comprador estrangeiro informar a tag "idEstrangeiro", com o número do passaporte ou outro documento legal para identificar pessoa estrangeira.
        /// Nota: Campo aceita valor Nulo.
        /// (campo novo) [23-12-13]
        /// </summary>
        public string idEstrangeiro;

        /// <summary>
        /// Informar a razão social do destinatário, pode ser omitida no caso de NFC-e. [23-12-13]
        /// </summary>
        [Length(2, 60)]
        public string nome;
        /// <summary>
        /// Informar o logradouro do destinatário, o grupo de informações do endereço do destinatário pode ser omitido no caso de NFC-e. [23-12-13]
        /// </summary>
        [Length(2, 60)]
        public string logradouro;
        /// <summary>
        /// Informar o número do endereço do destinatário, campo obrigatório. Informar S/N ou . (ponto) ou - (traço) para evitar falha de schema XML quando não houver número.
        /// </summary>
        [Length(1, 60)]
        public string numero;
        /// <summary>
        /// Informar o complemento do endereço do destinatário, pode ser omitido
        /// </summary>
        [Length(2, 60)]
        public string complemento;
        /// <summary>
        /// Informar o bairro do endereço do destinatário
        /// </summary>
        public string bairro;
        /// <summary>
        /// Informar o nome do município
        /// </summary>
        public string municipio;
        /// <summary>
        /// Informar o código do município na codificação do IBGE com 7 dígitos
        /// </summary>
        public string municipioIbge;
        /// <summary>
        /// Sigla da UF
        /// </summary>
        public string uf;
        /// <summary>
        /// Informar o CEP, sem formatação ou máscara, pode ser omitido
        /// </summary>
        public int cep;
        /// <summary>
        /// Informar o telefone com DDD + número, sem formatação
        /// </summary>
        public long fone;
        /// <summary>
        /// Indicador da IE do Destinatário
        /// </summary>
        public TipoDestinatario tipo;
        /// <summary>
        /// Informar a Inscrição SUFRAMA do destinatário, sem formatação ou máscara, se existir.
        /// </summary>
        public long suframa;
        /// <summary>
        /// Inscrição Municipal do Tomador do Serviço, campo opcional, pode ser informado na NF-e conjugada, com itens de produtos sujeitos ao ICMS e itens de serviços sujeitos ao ISSQN (campo novo) [23-12-13]
        /// </summary>
        public long inscricaoMunicipal;
        /// <summary>
        /// Informar o e-mail do destinatário, pode ser omitido
        /// </summary>
        [MaxLength(60)]
        public string email;
    }
}
