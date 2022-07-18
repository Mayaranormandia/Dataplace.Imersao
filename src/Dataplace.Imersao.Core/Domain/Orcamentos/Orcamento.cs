using Dataplace.Imersao.Core.Domain.Excepions;
using Dataplace.Imersao.Core.Domain.Orcamentos.Enums;
using Dataplace.Imersao.Core.Domain.Orcamentos.ValueObjects;
using System;
using System.Collections.Generic;

namespace Dataplace.Imersao.Core.Domain.Orcamentos
{
    public class Orcamento
    {
        private Orcamento(string cdEmpresa, string cdFilial, string numOrcamento, OrcamentoCliente cliente, 
            string usuario, OrcamentoVendedor vendedor, OrcamentoTabelaPreco tabelaPreco)
        {

            CdEmpresa = cdEmpresa;
            CdFilial = cdFilial;
            Cliente = cliente;
            NumOrcamento = numOrcamento;
            Usuario = usuario;
            Vendedor = vendedor;
            TabelaPreco = tabelaPreco;

            // default
            Situacao = OrcamentoStatusEnum.Aberto;
            DtOrcamento = DateTime.Now;
            ValotTotal = 0;
            Itens = new List<OrcamentoItem>();

        }

        public string CdEmpresa { get; private set; }
        public string CdFilial { get; private set; }
        public string NumOrcamento { get; private set; }
        public OrcamentoCliente Cliente { get; private set; }
        public DateTime DtOrcamento { get; private set; }
        public decimal ValotTotal { get; private set; }
        public OrcamentoValidade Validade { get; private set; }
        public OrcamentoTabelaPreco TabelaPreco { get; private set; }
        public DateTime? DtFechamento { get; private set; }
        public OrcamentoVendedor Vendedor { get; private set; }
        public string Usuario { get; private set; }
        public OrcamentoStatusEnum Situacao { get; private set; }
        public ICollection<OrcamentoItem> Itens { get; private set; }
        public string Quantidade { get; private set; }

        public void FecharOrcamento()
        {
            if (Situacao == OrcamentoStatusEnum.Fechado)
                throw new DomainException("Orçamento já está fechado!");

            Situacao = OrcamentoStatusEnum.Fechado;
            DtFechamento = DateTime.Now.Date;
        }

        public void ReabrirOrcamento()
        {
            if (Situacao == OrcamentoStatusEnum.Aberto)
                throw new DomainException("Orçamento já está fechado!");

            Situacao = OrcamentoStatusEnum.Aberto;
            DtFechamento = null;
        }
        public void CancelamentoOrcamento()
        {

            switch (Situacao)
            {
                case OrcamentoStatusEnum.Aberto:
                    throw new DomainException("Orçamento não pode ser cancelado pois está aberto!");
                   
                case OrcamentoStatusEnum.Fechado:
                    Situacao = OrcamentoStatusEnum.Cancelado;
                    break;
                }


        }
        public void InsercaoItemOrcamento()
        {
            if (Situacao != OrcamentoStatusEnum.Aberto)
                throw new DomainException("Orçamento está fechado, reabra para inserir itens!");

            var produto = new OrcamentoProduto(TpRegistroEnum.ProdutoFinal, "256");
            var preco = new OrcamentoItemPrecoTotal(50, 40);
            var item = new OrcamentoItem("05", "02", 250, produto, 10, preco);

            Itens.Add(item);
        }

            public void DefinirValidade(int diasValidade)
        {
            this.Validade = new OrcamentoValidade(this, diasValidade);
        }

        #region validations

        public List<string> Validations;
        public bool IsValid()
        {
            Validations = new List<string>();
    

            if (string.IsNullOrEmpty(CdEmpresa))
                Validations.Add("Código da empresa é requirido!");

            if (string.IsNullOrEmpty(CdFilial))
                Validations.Add("Código da filial é requirido!");


            if (string.IsNullOrEmpty(Quantidade))
                Validations.Add("A quantidade do item é requirida!");


            if (string.IsNullOrEmpty(NumOrcamento))
                Validations.Add("Número de orçamento é requirido!");

            if (string.IsNullOrEmpty(OrcamentoProduto.ProdutoFinal))
                Validations.Add("Código do produto é requirido!");



            if (Validations.Count > 0)
                return false;
            else
                return true;


        }

        #endregion

        #region factory methods
        public static class Factory
        {

            public static Orcamento Orcamento(string cdEmpresa, string cdFilial, string numOrcamento, OrcamentoCliente cliente , string usuario, OrcamentoVendedor vendedor, OrcamentoTabelaPreco tabelaPreco)
            {
                return new Orcamento(cdEmpresa, cdFilial, numOrcamento, cliente, usuario, vendedor, tabelaPreco);
            }
            public static Orcamento OrcamentoRapido(string cdEmpresa, string cdFilial,string numOrcamento, string usuario, OrcamentoVendedor vendedor, OrcamentoTabelaPreco tabelaPreco)
            {
                return new Orcamento(cdEmpresa, cdFilial, numOrcamento, null, usuario, vendedor, tabelaPreco);
            }
            public static Orcamento ItemOrcamento(string cdEmpresa, string cdFilial, string numOrcamento, OrcamentoCliente cliente, string usuario, OrcamentoVendedor vendedor, OrcamentoTabelaPreco tabelaPreco)
            {
                return new Orcamento(cdEmpresa, cdFilial, numOrcamento, cliente, usuario, vendedor, tabelaPreco);
            }

            public static OrcamentoItem OrcamentoItem(string produto, int quantidade, decimal preco)
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
