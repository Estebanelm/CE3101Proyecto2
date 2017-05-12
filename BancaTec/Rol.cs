using System;
using System.Collections.Generic;

namespace BancaTec
{
    public partial class Rol
    {
        public Rol()
        {
            EmpleadoRol = new HashSet<EmpleadoRol>();
        }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<EmpleadoRol> EmpleadoRol { get; set; }
    }
}
