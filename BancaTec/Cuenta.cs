using System;
using System.Collections.Generic;

namespace BancaTec
{
    public partial class Cuenta
    {
        public Cuenta()
        {
            Tarjeta = new HashSet<Tarjeta>();
        }

        public string Tipo { get; set; }
        public string Moneda { get; set; }
        public string Descripcion { get; set; }
        public string CedCliente { get; set; }
        public char Estado { get; set; }
        public int NumCuenta { get; set; }

        public virtual ICollection<Tarjeta> Tarjeta { get; set; }
        public virtual Cliente CedClienteNavigation { get; set; }
    }
}
