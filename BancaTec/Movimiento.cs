﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace BancaTec
{
    public partial class Movimiento
    {
        public string Tipo { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public int NumCuenta { get; set; }
        public string Moneda { get; set; }
        public int Id { get; set; }
        [XmlIgnore]
        public char Estado { get; set; }
        [XmlIgnore]
        public virtual Cuenta NumCuentaNavigation { get; set; }

        public static List<Movimiento> GetMovimientos(int numcuenta)
        {
            List<Movimiento> listamovimientosobj = new List<Movimiento>();
            using (var db = new BancaTecContext())
            {
                var listamovimientos = db.Movimiento
                    .Where(b => b.NumCuenta == numcuenta && b.Estado.Equals('A'));
                foreach (var movimiento in listamovimientos)
                {
                    if (movimiento.Estado == 'A')
                    {
                        listamovimientosobj.Add(movimiento);
                    }
                }
            }
            return listamovimientosobj;
        }

        public static List<Movimiento> GetMovimientos()
        {
            List<Movimiento> lista_movimientos = new List<Movimiento>();
            using (var db = new BancaTecContext())
            {
                foreach (var movimiento in db.Movimiento)
                {
                    if (movimiento.Estado == 'A')
                    {
                        lista_movimientos.Add(movimiento);
                    }
                }
            }
            return lista_movimientos;
        }

        public static void AddMovimiento(Movimiento movi)
        {
            using (var db = new BancaTecContext())
            {
                db.Movimiento.Add(movi);
                db.SaveChanges();
            }
        }

        public static void UpdateMovimiento(Movimiento movi)
        {
            using (var db = new BancaTecContext())
            {
                var asesor = db.Movimiento
                                .Where(b => b.Id == movi.Id)
                                .FirstOrDefault();
                if (asesor != null)
                {
                    foreach (PropertyInfo property in typeof(Movimiento).GetProperties())
                    {
                        if (!property.PropertyType.AssemblyQualifiedName.Contains("ICollection") && !(property.Name == "Estado") && !property.PropertyType.AssemblyQualifiedName.Contains("Cuenta"))
                        {
                            property.SetValue(asesor, property.GetValue(movi, null), null);
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

        public static void DeleteMovimiento(int id)
        {
            using (var db = new BancaTecContext())
            {
                var asesor = db.Movimiento
                                .Where(b => b.Id == id)
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
