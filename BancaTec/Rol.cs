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
        public char Estado { get; set; }
        [XmlIgnore]
        public virtual ICollection<EmpleadoRol> EmpleadoRol { get; set; }

        public static Rol GetRol(string nombre)
        {
            using (var db = new BancaTecContext())
            {
                var rol = db.Rol
                    .Where(b => b.Nombre == nombre)
                    .FirstOrDefault();

                return rol;
            }
        }

        public static List<Rol> GetRoles()
        {
            List<Rol> lista_roles = new List<Rol>();
            using (var db = new BancaTecContext())
            {
                foreach (var rol in db.Rol)
                {
                    lista_roles.Add(rol);
                }
            }
            return lista_roles;
        }

        public static void AddRol(Rol rol)
        {
            using (var db = new BancaTecContext())
            {
                db.Rol.Add(rol);
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
                db.SaveChanges();
            }
        }
    }
}
