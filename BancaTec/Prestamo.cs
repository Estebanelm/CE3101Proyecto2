﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
        public char Estado { get; set; }
        public int Numero { get; set; }
        public string Moneda { get; set; }

        public virtual ICollection<Pago> Pago { get; set; }
        public virtual Asesor CedAsesorNavigation { get; set; }
        public virtual Cliente CedClienteNavigation { get; set; }

        public static Prestamo GetPrestamo(int numeroPrestamo)
        {
            using (var db = new BancaTecContext())
            {
                var prestamo = db.Prestamo
                    .Where(b => b.Numero == numeroPrestamo)
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
                    lista_prestamos.Add(prestamo);
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
                    foreach (PropertyInfo property in typeof(Asesor).GetProperties())
                    {
                        if (!property.PropertyType.AssemblyQualifiedName.Contains("ICollection") && !(property.Name == "Estado") && !property.PropertyType.AssemblyQualifiedName.Contains("Asesor") && !property.PropertyType.AssemblyQualifiedName.Contains("Cliente"))
                        {
                            property.SetValue(prestamo, property.GetValue(prest, null), null);
                        }
                    }
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
                if (prestamo != null)
                {
                    prestamo.Estado = 'I';
                }
                db.SaveChanges();
            }
        }
    }
}
