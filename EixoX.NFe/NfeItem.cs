using EixoX.Restrictions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EixoX.NFe
{
    public class NfeItem
    {
        /// <summary>
        /// Informar o código do produto ou serviço. Preencher com CFOP, caso se trate de itens não relacionados com mercadorias/produtos e que o contribuinte não possua codificação própria. Formato ”CFOP9999”.
        /// </summary>
        [Length(1, 60)]
        public string produtoCodigo;

        /// <summary>
        /// Informar o GTIN (Global Trade Item Number) do produto, antigo código EAN ou código de barras. Preencher com o código GTIN-8, GTIN-12, GTIN-13 ou GTIN-14 (antigos códigos EAN, UPC e DUN-14), não informar o conteúdo da TAG em caso de o produto não possuir este código.
        /// </summary>
        [MaxLength(14)]
        public string produtoGTIN;

        /// <summary>
        /// Informar a descrição do produto ou serviço.
        /// </summary>
        [Length(1,120)]
        public string produtoDescricao;

        /// <summary>
        /// informar o Código NCM com 8 dígitos; informar a posição do capítulo do NCM (as duas primeiras posições do NCM) quando a operação não for de comércio exterior (importação/ exportação) ou o produto não seja tributado pelo IPI; se for serviços, informar 00.
        /// </summary>
        public int produtoNCM;

        /// <summary>
        /// Informar com a Codificação NVE - Nomenclatura de Valor Aduaneiro e Estatística, Codificação opcional que detalha alguns NCM (campo novo) [23-12-13]
        /// </summary>
        public string produtoNVE;
        /// <summary>
        /// Informar de acordo com o código EX da TIPI se houver para o NCM do produto.
        /// </summary>
        public string produtoExTIPI;

        /// <summary>
        /// Informar o CFOP - Código Fiscal de Operações e Prestações.
        /// </summary>
        public int produtoCFOP;

        /// <summary>
        /// Informar a unidade de comercialização do produto (Ex. pc, und, dz, kg, etc.).
        /// </summary>
        public string produtoUnidadeComercializacao = "un";

        /// <summary>
        /// Informar a quantidade de comercialização do produto já formatado com ponto decimal. A quantidade de casas decimais pode variar de 0 a 4.
        /// </summary>
        public decimal produtoQuantidadeComercializacao;

        /// <summary>
        /// Informar o valor unitário de comercialização do produto já formatado com ponto decimal, campo meramente informativo, o contribuinte pode utilizar a precisão desejada (0-10 decimais). Para efeitos de cálculo, o valor unitário será obtido pela divisão do valor do produto pela quantidade comercial.
        /// </summary>
        public decimal produtoValorUnitario;

        /// <summary>
        /// Informar o valor total bruto do produto ou serviços.
        /// </summary>
        public decimal produtoValorTotal;

        /// <summary>
        /// Informar o GTIN (Global Trade Item Number) da unidade de tributação do produto, antigo código EAN ou código de barras. Preencher com o código GTIN-8, GTIN-12, GTIN-13 ou GTIN-14 (antigos códigos EAN, UPC e DUN-14), não informar o conteúdo da TAG em caso de o produto não possuir este código.
        /// </summary>
        public string produtoGTINtributacao;

        /// <summary>
        /// Informar a unidade de tributação do produto (Ex. pc, und, dz, kg, etc.).
        /// </summary>
        [MaxLength(6)]
        public string produtoUnidateTributacao;

        /// <summary>
        /// Informar a quantidade de tributação do produto já formatado com ponto decimal. A quantidade de casas decimais pode variar de 0 a 4.
        /// </summary>
        public decimal produtoQuantidadeTributacao;

        /// <summary>
        /// Informar o valor unitário de tributação do produto já formatado com ponto decimal, campo meramente informativo, o contribuinte pode utilizar a precisão desejada (0-10 decimais). Para efeitos de cálculo, o valor unitário será obtido pela divisão do valor do produto pela quantidade tributável.
        /// </summary>
        public decimal produtoValorUnitarioTributacao;

        /// <summary>
        /// Informar o valor do Frete, o Frete deve ser rateado entre os itens de produto.
        /// </summary>
        public decimal produtoValorFrete;

        /// <summary>
        /// Informar o valor do Seguro, o Seguro deve ser rateado entre os itens de produto.
        /// </summary>
        public decimal produtoValorSeguro;

        /// <summary>
        /// Informar o valor do desconto do item de produto ou serviço.
        /// </summary>
        public decimal produtoValorDesconto;

        /// <summary>
        /// Informar o valor de outras despesas acessórias do item de produto ou serviço.
        /// </summary>
        public decimal produtoValorOutro;

        /// <summary>
        /// Este campo deverá ser preenchido com: 0 - o valor do item (vProd) não compõe o valor total da NF-e (vProd) 1 - o valor do item (vProd) compõe o valor total da NF-e.
        /// </summary>
        public bool produtoCompoeValorTotalNfe = true;

        /// <summary>
        /// informar o número do pedido de compra, o campo é de livre uso do emissor.
        /// </summary>
        [MaxLength(15)]
        public string produtoCodigoPedido;
        
        /// <summary>
        /// Informar o número do item do pedido de compra, o campo é de livre uso do emissor. (Atenção: tipo Alterado para string na chamada.) [28-09-13]
        /// </summary>
        [MaxLength(6)]
        public string produtoCodigoItemPedido;

        /// <summary>
        /// Informar o Número de controle da FCI - Ficha de Conteúdo de Importação com formatação, ex.: B01F70AF-10BF-4B1F-848C-65FF57F616FE (campo novo) [28-09-13]
        /// </summary>
        [MaxLength(36)]
        public string produtoFCI;

        /// <summary>
        /// Pode ser utilizado para complementar a descrição e informações adicionais do produto.
        /// Não é permitido informação de caracteres de formatação (CR, LF, TAB, etc.), assim o usuário pode colocar caracteres que identificam o final linha para melhorar a visualização e a aplicação de impressão do DANFE tratar como quebra de linha, ex.: ***, /, |, etc.
        /// A consulta web da NF-e ainda não está mostrando as informações adicionais do produto, necessário reportar o problema para a SEFAZ resolver.
        /// </summary>
        [MaxLength(500)]
        public string produtoInformacoesAdicionais;

        /// <summary>
        /// Pode ser utilizado para complementar a descrição e informações adicionais do produto.
        /// Não é permitido informação de caracteres de formatação (CR, LF, TAB, etc.), assim o usuário pode colocar caracteres que identificam o final linha para melhorar a visualização e a aplicação de impressão do DANFE tratar como quebra de linha, ex.: ***, /, |, etc.
        /// A consulta web da NF-e ainda não está mostrando as informações adicionais do produto, necessário reportar o problema para a SEFAZ resolver.
        /// </summary>
        public double mercadoriaDevolvidaPercentual;

        /// <summary>
        /// Informar Valor do IPI devolvido. (campo novo) [23-12-13]
        /// </summary>
        public double mercadoriaDevolvidaIPI;
    }
}
