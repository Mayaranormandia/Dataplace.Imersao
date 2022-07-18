using Dataplace.Imersao.Core.Domain.Orcamentos.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataplace.Imersao.Core.Domain.Orcamentos.ValueObjects
{
    public class OrcamentoProduto
    {
        public OrcamentoProduto(TpRegistroEnum tpRegistro, string cdRegistro)
        {
            TpProduto = tpRegistro;
            CdProduto = cdRegistro;
        }

        public static string ProdutoFinal { get; internal set; }
        public TpRegistroEnum TpProduto { get; }
        public string CdProduto { get; }
    }
}
