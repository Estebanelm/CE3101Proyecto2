using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace BancaTec
{
    public partial class Prestamo
    {
        public Prestamo()
        {
            Pago = new HashSet<Pago>();
        }

        public double Interes { get; set; }
        public decimal SaldoOrig { get; set; }
        public decimal SaldoActual { get; set; }
        public string CedCliente { get; set; }
        public string CedAsesor { get; set; }
        public int Numero { get; set; }
        public string Moneda { get; set; }
        [XmlIgnore]
        public char Estado { get; set; }
        [XmlIgnore]
        public virtual ICollection<Pago> Pago { get; set; }
        [XmlIgnore]
        public virtual Asesor CedAsesorNavigation { get; set; }
        [XmlIgnore]
        public virtual Cliente CedClienteNavigation { get; set; }

        public static Prestamo GetPrestamo(int numeroPrestamo)
        {
            using (var db = new BancaTecContext())
            {
                var prestamo = db.Prestamo
                    .Where(b => b.Numero == numeroPrestamo && b.Estado.Equals('A'))
                    .FirstOrDefault();

                return prestamo;
            }
        }

        public static List<Prestamo> GetPrestamos()
        {
            List<Prestamo> lista_prestamos = new List<Prestamo>();
            using (var db = new BancaTecContext())
            {
                foreach (var prestamo in db.Prestamo)
                {
                    if (prestamo.Estado == 'A')
                    {
                        lista_prestamos.Add(prestamo);
                    }
                }
            }
            return lista_prestamos;
        }

        public static void AddPrestamo(Prestamo prest)
        {
            using (var db = new BancaTecContext())
            {
                db.Prestamo.Add(prest);
                db.SaveChanges();
            }
        }

        public static void UpdatePrestamo(Prestamo prest)
        {
            using (var db = new BancaTecContext())
            {
                var prestamo = db.Prestamo
                                .Where(b => b.Numero == prest.Numero)
                                .FirstOrDefault();
                if (prestamo != null)
                {
                    foreach (PropertyInfo property in typeof(Prestamo).GetProperties())
                    {
                        if (!property.PropertyType.AssemblyQualifiedName.Contains("ICollection") && !(property.Name == "Estado") && !property.PropertyType.AssemblyQualifiedName.Contains("Asesor") && !property.PropertyType.AssemblyQualifiedName.Contains("Cliente"))
                        {
                            property.SetValue(prestamo, property.GetValue(prest, null), null);
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

        public static void DeletePrestamo(int numeroPrestamo)
        {
            using (var db = new BancaTecContext())
            {
                var prestamo = db.Prestamo
                                .Where(b => b.Numero == numeroPrestamo)
                                .FirstOrDefault();
                if (prestamo.SaldoActual == 0)
                {
                    if (prestamo != null)
                    {
                        prestamo.Estado = 'I';
                    }
                    else
                    {
                        throw (new Exception("No se encontro instancia"));
                    }
                }
                else
                {
                    throw (new Exception("Tiene saldos pendientes"));
                }
                db.SaveChanges();
            }
        }
    }
}
