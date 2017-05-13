using System;
using System.Collections.Generic;

namespace BancaTec
{
    public partial class Empleado
    {
        public Empleado()
        {
            EmpleadoRol = new HashSet<EmpleadoRol>();
        }

        public string Cedula { get; set; }
        public string Sucursal { get; set; }
        public string Nombre { get; set; }
        public string SegNombre { get; set; }
        public string PriApellido { get; set; }
        public string SegApellido { get; set; }
        public string Contrasena { get; set; }

        public virtual ICollection<EmpleadoRol> EmpleadoRol { get; set; }
    }
}
