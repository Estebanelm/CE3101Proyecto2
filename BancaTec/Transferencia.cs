using System;
using System.Collections.Generic;

namespace BancaTec
{
    public partial class Transferencia
    {
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public int CuentaEmisora { get; set; }
        public int CuentaReceptora { get; set; }
        public string Moneda { get; set; }
        public int Id { get; set; }

        public virtual Cuenta CuentaEmisoraNavigation { get; set; }
        public virtual Cuenta CuentaReceptoraNavigation { get; set; }
    }
}
