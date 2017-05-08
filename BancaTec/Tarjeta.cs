using System;
using System.Collections.Generic;

namespace BancaTec
{
    public partial class Tarjeta
    {
        public string CodigoSeg { get; set; }
        public DateTime FechaExp { get; set; }
        public long Saldo { get; set; }
        public string Tipo { get; set; }
        public int NumCuenta { get; set; }
        public char Estado { get; set; }
        public int Numero { get; set; }

        public virtual Cuenta NumCuentaNavigation { get; set; }
    }
}
