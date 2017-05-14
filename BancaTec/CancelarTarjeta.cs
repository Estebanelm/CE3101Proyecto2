using System;
using System.Collections.Generic;

namespace BancaTec
{
    public partial class CancelarTarjeta
    {
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public int NumTarjeta { get; set; }
        public int Id { get; set; }
        public char Estado { get; set; }

        public virtual Tarjeta NumTarjetaNavigation { get; set; }
    }
}
