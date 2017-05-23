using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace BancaTec
{
    public partial class EmpleadoRol
    {
        public string CedulaEmpledo { get; set; }
        public string NombreRol { get; set; }
        [XmlIgnore]
        public int Id { get; set; }
        [XmlIgnore]
        public char Estado { get; set; }

        [XmlIgnore]
        public virtual Empleado CedulaEmpledoNavigation { get; set; }
        [XmlIgnore]
        public virtual Rol NombreRolNavigation { get; set; }

        public static void AddEmpleadoRol(EmpleadoRol emperol)
        {
            using (var db = new BancaTecContext())
            {
                var empleadoRol = db.EmpleadoRol
                                    .Where(b => b.CedulaEmpledo == emperol.CedulaEmpledo && b.NombreRol == emperol.NombreRol)
                                    .FirstOrDefault();
                if (empleadoRol == null)
                {
                    db.EmpleadoRol.Add(emperol);
                }
                else
                {
                    empleadoRol.NombreRol = emperol.NombreRol;
                }
                db.SaveChanges();
            }
        }

        public static EmpleadoRol GetEmpleadoRol(string cedulaempleado)
        {
            if (cedulaempleado == null)
            {
                return null;
            }
            else
            {
                using (var db = new BancaTecContext())
                {
                    var emplerol = db.EmpleadoRol
                        .Where(b => b.CedulaEmpledo == cedulaempleado && b.Estado.Equals('A'))
                        .FirstOrDefault();

                    return emplerol;
                }
            }
        }
    }
}
