using System;
using SIGCOMT.Domain.Core;

namespace SIGCOMT.Domain
{
    public class OrdenPedidoSap : EntityBase
    {
        public int DocNum { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public string DocStatus { get; set; }
        public DateTime DocDate { get; set; }
        public string Contado { get; set; }
    }
}