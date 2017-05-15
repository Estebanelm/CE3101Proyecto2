using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

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
        public char Estado { get; set; }
        [XmlIgnore]
        public virtual ICollection<EmpleadoRol> EmpleadoRol { get; set; }

        public static Empleado GetEmpleado(string cedula)
        {
            using (var db = new BancaTecContext())
            {
                var asesor = db.Empleado
                    .Where(b => b.Cedula == cedula)
                    .FirstOrDefault();

                return asesor;
            }
        }

        public static List<Empleado> GetEmpleados()
        {
            List<Empleado> lista_empleados = new List<Empleado>();
            using (var db = new BancaTecContext())
            {
                foreach (var empleado in db.Empleado)
                {
                    lista_empleados.Add(empleado);
                }
            }
            return lista_empleados;
        }

        public static void AddEmpleado(Empleado empl)
        {
            using (var db = new BancaTecContext())
            {
                db.Empleado.Add(empl);
                db.SaveChanges();
            }
        }

        public static void UpdateEmpleado(Empleado empl)
        {
            using (var db = new BancaTecContext())
            {
                var empleado = db.Empleado
                                .Where(b => b.Cedula == empl.Cedula)
                                .FirstOrDefault();
                if (empleado != null)
                {
                    foreach (PropertyInfo property in typeof(Empleado).GetProperties())
                    {
                        if (!property.PropertyType.AssemblyQualifiedName.Contains("ICollection") && !(property.Name == "Estado"))
                        {
                            property.SetValue(empleado, property.GetValue(empl, null), null);
                        }
                    }
                }
                db.SaveChanges();
            }
        }

        public static void DeleteEmpleado(string cedula)
        {
            using (var db = new BancaTecContext())
            {
                var empleado = db.Empleado
                                .Where(b => b.Cedula == cedula)
                                .FirstOrDefault();
                if (empleado != null)
                {
                    empleado.Estado = 'I';
                }
                db.SaveChanges();
            }
        }
    }
}
