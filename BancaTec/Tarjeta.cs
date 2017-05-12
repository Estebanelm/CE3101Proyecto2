using System;
using System.Collections.Generic;

namespace BancaTec
{
    public partial class Tarjeta
    {
        public Tarjeta()
        {
            CancelarTarjeta = new HashSet<CancelarTarjeta>();
            Compra = new HashSet<Compra>();
        }

        public string CodigoSeg { get; set; }
        public DateTime FechaExp { get; set; }
        public decimal? Saldo { get; set; }
        public string Tipo { get; set; }
        public int NumCuenta { get; set; }
        public char Estado { get; set; }
        public int Numero { get; set; }

        public virtual ICollection<CancelarTarjeta> CancelarTarjeta { get; set; }
        public virtual ICollection<Compra> Compra { get; set; }
        public virtual Cuenta NumCuentaNavigation { get; set; }
    }
}
