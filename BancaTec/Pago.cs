using System;
using System.Collections.Generic;

namespace BancaTec
{
    public partial class Pago
    {
        public decimal Monto { get; set; }
        public int NumPrestamo { get; set; }
        public DateTime Fecha { get; set; }
        public string CedCliente { get; set; }
        public decimal MontoInteres { get; set; }
        public string Estado { get; set; }
        public int PagosRestantes { get; set; }
        public decimal Extraordinario { get; set; }

        public virtual Cliente CedClienteNavigation { get; set; }
        public virtual Prestamo NumPrestamoNavigation { get; set; }
    }
}
