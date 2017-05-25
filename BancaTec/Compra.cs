using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace BancaTec
{
    public partial class Compra
    {
        public int NumTarjeta { get; set; }
        public string Comercio { get; set; }
        public decimal Monto { get; set; }
        public string Moneda { get; set; }
        public DateTime Fecha { get; set; }
        public int Id { get; set; }
        [XmlIgnore]
        public char Estado { get; set; }
        [XmlIgnore]
        public virtual Tarjeta NumTarjetaNavigation { get; set; }

        public static List<Compra> GetCompras(int numtarjeta, DateTime fechainicial, DateTime fechafinal)
        {
            List<Compra> listacompraobj = new List<Compra>();
            using (var db = new BancaTecContext())
            {
                var listacompra = db.Compra
                    .Where(b => b.NumTarjeta == numtarjeta && b.Estado.Equals('A') && b.Fecha >= fechainicial && b.Fecha <= fechafinal);
                foreach (var compra in listacompra)
                {
                    listacompraobj.Add(compra);
                }
            }
            return listacompraobj;
        }

        public static List<Compra> GetCompras()
        {
            List<Compra> lista_compras = new List<Compra>();
            using (var db = new BancaTecContext())
            {
                foreach (var compra in db.Compra)
                {
                    if (compra.Estado == 'A')
                    {
                        lista_compras.Add(compra);
                    }
                }
            }
            return lista_compras;
        }

        public static void AddCompra(Compra comp)
        {
            using (var db = new BancaTecContext())
            {
                db.Compra.Add(comp);
                db.SaveChanges();
            }
        }

        public static void UpdateCompra(Compra comp)
        {
            using (var db = new BancaTecContext())
            {
                var compra = db.Compra
                                .Where(b => b.Id == comp.Id)
                                .FirstOrDefault();
                if (compra != null)
                {
                    foreach (PropertyInfo property in typeof(Compra).GetProperties())
                    {
                        if (!property.PropertyType.AssemblyQualifiedName.Contains("ICollection") && !(property.Name == "Estado") && !property.PropertyType.AssemblyQualifiedName.Contains("Tarjeta"))
                        {
                            property.SetValue(compra, property.GetValue(comp, null), null);
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

        public static void DeleteCompra(int id)
        {
            using (var db = new BancaTecContext())
            {
                var compra = db.Compra
                                .Where(b => b.Id == id)
                                .FirstOrDefault();
                if (compra != null)
                {
                    compra.Estado = 'I';
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

