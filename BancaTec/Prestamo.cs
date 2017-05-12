using System;
using System.Collections.Generic;

namespace BancaTec
{
    public partial class Prestamo
    {
        public double Interes { get; set; }
        public decimal SaldoOrig { get; set; }
        public decimal SaldoActual { get; set; }
        public string CedCliente { get; set; }
        public string CedAsesor { get; set; }
        public char Estado { get; set; }
        public int Numero { get; set; }
        public string Moneda { get; set; }

        public virtual Pago Pago { get; set; }
        public virtual Asesor CedAsesorNavigation { get; set; }
        public virtual Cliente CedClienteNavigation { get; set; }
    }
}
