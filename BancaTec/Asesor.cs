using System;
using System.Collections.Generic;

namespace BancaTec
{
    public partial class Asesor
    {
        public Asesor()
        {
            Prestamo = new HashSet<Prestamo>();
        }

        public string Cedula { get; set; }
        public DateTime FechaNac { get; set; }
        public string Nombre { get; set; }
        public string SegNombre { get; set; }
        public string PriApellido { get; set; }
        public string SegApellido { get; set; }
        public char Estado { get; set; }
        public decimal MetaColones { get; set; }
        public decimal MetaDolares { get; set; }

        public virtual ICollection<Prestamo> Prestamo { get; set; }
    }
}
