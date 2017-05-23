using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

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
        public decimal MetaColones { get; set; }
        public decimal MetaDolares { get; set; }
        [XmlIgnore]
        public char Estado { get; set; }
        [XmlIgnore]
        public virtual ICollection<Prestamo> Prestamo { get; set; }

        public static Asesor GetAsesor(string cedula)
        {
            using (var db = new BancaTecContext())
            {
                var asesor = db.Asesor
                    .Where(b => b.Cedula == cedula && b.Estado.Equals('A'))
                    .FirstOrDefault();

                return asesor;
            }
        }

        public static List<Asesor> GetAsesores()
        {
            List<Asesor> lista_asesores = new List<Asesor>();
            using (var db = new BancaTecContext())
            {
                foreach (var asesor in db.Asesor)
                {
                    if (asesor.Estado == 'A')
                    {
                        lista_asesores.Add(asesor);
                    }
                }
            }
            return lista_asesores;
        }

        public static void AddAsesor(Asesor ase)
        {
            using (var db = new BancaTecContext())
            {
                var asesor = db.Asesor
                                .Where(b => b.Cedula == ase.Cedula)
                                .FirstOrDefault();
                if (asesor == null)
                {
                    db.Asesor.Add(ase);
                }
                else
                {
                    asesor.Estado = 'A';
                }
                db.SaveChanges();
            }
        }

        public static void UpdateAsesor(Asesor ase)
        {
            using (var db = new BancaTecContext())
            {
                var asesor = db.Asesor
                                .Where(b => b.Cedula == ase.Cedula)
                                .FirstOrDefault();
                if (asesor != null)
                {
                    foreach (PropertyInfo property in typeof(Asesor).GetProperties())
                    {
                        if (!property.PropertyType.AssemblyQualifiedName.Contains("ICollection") && !(property.Name == "Estado"))
                        {
                            property.SetValue(asesor, property.GetValue(ase, null), null);
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

        public static void DeleteAsesor(string cedula)
        {
            using (var db = new BancaTecContext())
            {
                var asesor = db.Asesor
                                .Where(b => b.Cedula == cedula)
                                .FirstOrDefault();
                if (asesor != null)
                {
                    asesor.Estado = 'I';
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