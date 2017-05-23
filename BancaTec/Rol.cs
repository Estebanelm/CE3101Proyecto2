using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

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
        [XmlIgnore]
        public char Estado { get; set; }
        [XmlIgnore]
        public virtual ICollection<EmpleadoRol> EmpleadoRol { get; set; }

        public static Rol GetRol(string nombre)
        {
            if (nombre == null)
            {
                return null;
            }
            else
            {
                using (var db = new BancaTecContext())
                {
                    var rol = db.Rol
                        .Where(b => b.Nombre == nombre && b.Estado.Equals('A'))
                        .FirstOrDefault();

                    return rol;
                }
            }
        }

        public static List<Rol> GetRoles()
        {
            List<Rol> lista_roles = new List<Rol>();
            using (var db = new BancaTecContext())
            {
                foreach (var rol in db.Rol)
                {
                    if (rol.Estado == 'A')
                    {
                        lista_roles.Add(rol);
                    }
                }
            }
            return lista_roles;
        }

        public static void AddRol(Rol rol)
        {
            using (var db = new BancaTecContext())
            {
                var rolViejo = db.Rol
                                .Where(b => b.Nombre == rol.Nombre)
                                .FirstOrDefault();
                if (rolViejo == null)
                {
                    db.Rol.Add(rol);
                }
                else
                {
                    rolViejo.Estado = 'A';
                }
                db.SaveChanges();
            }
        }

        public static void UpdateRol(Rol ro)
        {
            using (var db = new BancaTecContext())
            {
                var rol = db.Rol
                                .Where(b => b.Nombre == ro.Nombre)
                                .FirstOrDefault();
                if (rol != null)
                {
                    foreach (PropertyInfo property in typeof(Rol).GetProperties())
                    {
                        if (!property.PropertyType.AssemblyQualifiedName.Contains("ICollection") && !(property.Name == "Estado"))
                        {
                            property.SetValue(rol, property.GetValue(ro, null), null);
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

        public static void DeleteRol(string nombre)
        {
            using (var db = new BancaTecContext())
            {
                var rol = db.Rol
                                .Where(b => b.Nombre == nombre)
                                .FirstOrDefault();
                if (rol != null)
                {
                    rol.Estado = 'I';
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
