using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desafio.Domain.Entities
{
    public class Pedido
    {
        public int Id { get; set; }
        public int NumeroPedido { get; set; }
        public string CicloDelPedido { get; set; }
        public Int64 CodigoDeContratoInterno { get; set; }
        public DateTime Fecha { get; set; }
        public EstadoPedido EstadoPedido { get; set; }

    }
}
