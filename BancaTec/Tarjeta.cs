using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BancaTec
{
    public partial class Tarjeta
    {
        public Tarjeta()
        {
            CancelarTarjeta = new HashSet<CancelarTarjeta>();
            Compra = new HashSet<Compra>();
        }

        public string CodigoSeg { get; set; }
        public DateTime FechaExp { get; set; }
        public decimal? Saldo { get; set; }
        public string Tipo { get; set; }
        public int NumCuenta { get; set; }
        public char Estado { get; set; }
        public int Numero { get; set; }

        public virtual ICollection<CancelarTarjeta> CancelarTarjeta { get; set; }
        public virtual ICollection<Compra> Compra { get; set; }
        public virtual Cuenta NumCuentaNavigation { get; set; }

        public static Tarjeta GetTarjeta(int numtarjeta)
        {
            using (var db = new BancaTecContext())
            {
                var tarjeta = db.Tarjeta
                    .Where(b => b.Numero == numtarjeta)
                    .FirstOrDefault();

                return tarjeta;
            }
        }

        public static List<Tarjeta> GetTarjetas()
        {
            List<Tarjeta> lista_tarjetas = new List<Tarjeta>();
            using (var db = new BancaTecContext())
            {
                foreach (var tarjeta in db.Tarjeta)
                {
                    lista_tarjetas.Add(tarjeta);
                }
            }
            return lista_tarjetas;
        }

        public static void AddTarjeta(Tarjeta tarj)
        {
            using (var db = new BancaTecContext())
            {
                db.Tarjeta.Add(tarj);
                db.SaveChanges();
            }
        }

        public static void UpdateTarjeta(Tarjeta tarj)
        {
            using (var db = new BancaTecContext())
            {
                var tarjeta = db.Tarjeta
                                .Where(b => b.Numero == tarj.Numero)
                                .FirstOrDefault();
                if (tarjeta != null)
                {
                    foreach (PropertyInfo property in typeof(Asesor).GetProperties())
                    {
                        if (!property.PropertyType.AssemblyQualifiedName.Contains("ICollection") && !(property.Name == "Estado") && !property.PropertyType.AssemblyQualifiedName.Contains("Cuenta"))
                        {
                            property.SetValue(tarjeta, property.GetValue(tarj, null), null);
                        }
                    }
                }
                db.SaveChanges();
            }
        }

        public static void DeleteTarjeta(int numero)
        {
            using (var db = new BancaTecContext())
            {
                var tarjeta = db.Tarjeta
                                .Where(b => b.Numero == numero)
                                .FirstOrDefault();
                if (tarjeta != null)
                {
                    tarjeta.Estado = 'I';
                }
                db.SaveChanges();
            }
        }
    }
}
