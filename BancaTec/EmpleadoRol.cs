using System;
using System.Collections.Generic;

namespace BancaTec
{
    public partial class EmpleadoRol
    {
        public string CedulaEmpledo { get; set; }
        public string NombreRol { get; set; }
        public int Id { get; set; }

        public virtual Empleado CedulaEmpledoNavigation { get; set; }
        public virtual Rol NombreRolNavigation { get; set; }
    }
}
