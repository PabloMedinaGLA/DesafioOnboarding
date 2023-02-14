using desafio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desafio.Domain.Entities
{
    public class Pedidos
    {
       
        public Guid Id { get; set; }
        public int? NumeroDePedido { get; set; }
        public string CicloDelPedido { get; set; }
        public long? CodigoDeContratoInterno { get; set; }
        public int? EstadoDelPedido { get; set; }
        public string CuentaCorriente { get; set; }
        public DateTime Cuando { get; set; }

    }
}
