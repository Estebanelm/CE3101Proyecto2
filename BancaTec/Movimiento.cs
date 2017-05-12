using System;
using System.Collections.Generic;

namespace BancaTec
{
    public partial class Movimiento
    {
        public string Tipo { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public int NumCuenta { get; set; }
        public string Moneda { get; set; }
        public int Id { get; set; }

        public virtual Cuenta NumCuentaNavigation { get; set; }
    }
}
