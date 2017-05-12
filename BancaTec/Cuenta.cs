using System;
using System.Collections.Generic;

namespace BancaTec
{
    public partial class Cuenta
    {
        public Cuenta()
        {
            Movimiento = new HashSet<Movimiento>();
            Tarjeta = new HashSet<Tarjeta>();
            TransferenciaCuentaEmisoraNavigation = new HashSet<Transferencia>();
            TransferenciaCuentaReceptoraNavigation = new HashSet<Transferencia>();
        }

        public string Tipo { get; set; }
        public string Moneda { get; set; }
        public string Descripcion { get; set; }
        public string CedCliente { get; set; }
        public char Estado { get; set; }
        public int NumCuenta { get; set; }
        public decimal Saldo { get; set; }

        public virtual ICollection<Movimiento> Movimiento { get; set; }
        public virtual ICollection<Tarjeta> Tarjeta { get; set; }
        public virtual ICollection<Transferencia> TransferenciaCuentaEmisoraNavigation { get; set; }
        public virtual ICollection<Transferencia> TransferenciaCuentaReceptoraNavigation { get; set; }
        public virtual Cliente CedClienteNavigation { get; set; }
    }
}
