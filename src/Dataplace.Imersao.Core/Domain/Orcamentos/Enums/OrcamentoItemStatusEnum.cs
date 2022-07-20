using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataplace.Imersao.Core.Domain.Orcamentos.Enums
{
    public enum OrcamentoItemStatusEnum
    {
        Aberto,
        Fechado,
        Cancelado
    }

    public static class OrcamentoItemStatusEnumExtensions
    {
        public static string ToDataValue(this OrcamentoItemStatusEnum value)
        {
            return value == OrcamentoItemStatusEnum.Fechado ? "F" : "P";
        }
        public static OrcamentoItemStatusEnum ToOrcamentoItemStatusEnum(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return OrcamentoItemStatusEnum.Aberto;

            if (value == "P")
                return OrcamentoItemStatusEnum.Fechado;
            else
                return OrcamentoItemStatusEnum.Aberto;
        }
    }
}
