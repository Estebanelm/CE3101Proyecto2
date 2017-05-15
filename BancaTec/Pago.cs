using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace BancaTec
{
    public partial class Pago
    {
        public decimal Monto { get; set; }
        public int NumPrestamo { get; set; }
        public DateTime Fecha { get; set; }
        public string CedCliente { get; set; }
        public decimal MontoInteres { get; set; }
        public string Estado { get; set; }
        public int PagosRestantes { get; set; }
        public decimal Extraordinario { get; set; }
        [XmlIgnore]
        public virtual Cliente CedClienteNavigation { get; set; }
        [XmlIgnore]
        public virtual Prestamo NumPrestamoNavigation { get; set; }

        public static List<Pago> GetPagos(string codigo, string tipo)
        {
            List<Pago> listapagoobj = new List<Pago>();
            using (var db = new BancaTecContext())
            {
                if (tipo == "cliente")
                {
                    var listapagos = db.Pago
                            .Where(b => b.CedCliente == codigo);
                    foreach (var pago in listapagos)
                    {
                        listapagoobj.Add(pago);
                    }
                }
                else if (tipo == "prestamo")
                {
                    var listapagos = db.Pago
                            .Where(b => b.NumPrestamo == int.Parse(codigo));
                    foreach (var pago in listapagos)
                    {
                        listapagoobj.Add(pago);
                    }
                }
                return listapagoobj;
            }
        }

        public static List<Pago> GetPagos()
        {
            List<Pago> lista_pagos = new List<Pago>();
            using (var db = new BancaTecContext())
            {
                foreach (var pago in db.Pago)
                {
                    lista_pagos.Add(pago);
                }
            }
            return lista_pagos;
        }

        public static void AddPago(Pago pag)
        {
            using (var db = new BancaTecContext())
            {
                db.Pago.Add(pag);
                db.SaveChanges();
            }
        }

        public static void UpdatePago(Pago pag)
        {
            using (var db = new BancaTecContext())
            {
                var pago = db.Pago
                                .Where(b => b.NumPrestamo == pag.NumPrestamo && b.Fecha == pag.Fecha)
                                .FirstOrDefault();
                if (pago != null)
                {
                    foreach (PropertyInfo property in typeof(Pago).GetProperties())
                    {
                        if (!property.PropertyType.AssemblyQualifiedName.Contains("ICollection") && !(property.Name == "Estado") && !property.PropertyType.AssemblyQualifiedName.Contains("Cliente") && !property.PropertyType.AssemblyQualifiedName.Contains("Prestamo"))
                        {
                            property.SetValue(pago, property.GetValue(pag, null), null);
                        }
                    }
                }
                db.SaveChanges();
            }
        }

        public static void DeletePago(int numeroPrestamo, DateTime fecha)
        {
            using (var db = new BancaTecContext())
            {
                var pago = db.Pago
                                .Where(b => b.NumPrestamo == numeroPrestamo && b.Fecha == fecha)
                                .FirstOrDefault();
                if (pago != null)
                {
                    pago.Estado = "Pagado";
                }
                db.SaveChanges();
            }
        }
    }
}
