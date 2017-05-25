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
        [XmlIgnore]
        public char Estado { get; set; }
        [XmlIgnore]
        public virtual ICollection<EmpleadoRol> EmpleadoRol { get; set; }

        public static Empleado GetEmpleado(string cedula)
        {
            using (var db = new BancaTecContext())
            {
                var asesor = db.Empleado
                    .Where(b => b.Cedula == cedula && b.Estado.Equals('A'))
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
                    if (empleado.Estado == 'A')
                    {
                        lista_empleados.Add(empleado);
                    }
                }
            }
            return lista_empleados;
        }

        public static void AddEmpleado(Empleado empl)
        {
            using (var db = new BancaTecContext())
            {
                var empleado = db.Empleado
                                .Where(b => b.Cedula == empl.Cedula)
                                .FirstOrDefault();
                if (empleado == null)
                {
                    db.Empleado.Add(empl);
                }
                else
                {
                    empleado.Estado = 'A';
                }
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
                            if (property.Name == "Contrasena")
                            {
                                if (empl.Contrasena != null)
                                {
                                    property.SetValue(empleado, property.GetValue(empl, null), null);
                                }
                            }
                            else
                            {
                                property.SetValue(empleado, property.GetValue(empl, null), null);
                            }
                        }
                    }
                }
                else
                {
                    throw (new Exception());
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
                var empleadosrol = empleado.EmpleadoRol;
                foreach (var item in empleadosrol)
                {
                    item.Estado = 'I';
                }
                if (empleado != null)
                {
                    empleado.Estado = 'I';
                }
                else
                {
                    throw (new Exception("No se encontro instancia"));
                }
                db.SaveChanges();
            }
        }
    }
}
