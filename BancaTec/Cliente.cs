using System;
using System.Collections.Generic;

namespace BancaTec
{
    public partial class Cliente
    {
        public Cliente()
        {
            Cuenta = new HashSet<Cuenta>();
            Pago = new HashSet<Pago>();
            Prestamo = new HashSet<Prestamo>();
        }

        public string Nombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PriApellido { get; set; }
        public string SegApellido { get; set; }
        public string Cedula { get; set; }
        public string Tipo { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public decimal Ingreso { get; set; }
        public char Estado { get; set; }
        public string Contrasena { get; set; }

        public virtual ICollection<Cuenta> Cuenta { get; set; }
        public virtual ICollection<Pago> Pago { get; set; }
        public virtual ICollection<Prestamo> Prestamo { get; set; }
    }
}
