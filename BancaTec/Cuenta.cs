using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BancaTec
{
    public partial class Cuenta
    {
        public Cuenta()
        {
            Movimiento = new HashSet<Movimiento>();
            Tarjeta = new HashSet<Tarjeta>();
            TransferenciaCuentaEmisoraNavigation = new HashSet<Transferencia>();
            TransferenciaCuentaReceptoraNavigation = new HashSet<Transferencia>();
        }

        public string Tipo { get; set; }
        public string Moneda { get; set; }
        public string Descripcion { get; set; }
        public string CedCliente { get; set; }
        public char Estado { get; set; }
        public int NumCuenta { get; set; }
        public decimal Saldo { get; set; }

        public virtual ICollection<Movimiento> Movimiento { get; set; }
        public virtual ICollection<Tarjeta> Tarjeta { get; set; }
        public virtual ICollection<Transferencia> TransferenciaCuentaEmisoraNavigation { get; set; }
        public virtual ICollection<Transferencia> TransferenciaCuentaReceptoraNavigation { get; set; }
        public virtual Cliente CedClienteNavigation { get; set; }

        public static List<Cuenta> GetCuentas(string cedula)
        {
            List<Cuenta> listaCuentasobj = new List<Cuenta>();
            using (var db = new BancaTecContext())
            {
                var listaCuentas = db.Cuenta
                    .Where(b => b.CedCliente == cedula);

                foreach (var cuen in listaCuentas)
                {
                    listaCuentasobj.Add(cuen);
                }

                return listaCuentasobj;
            }
        }

        public static List<Cuenta> GetCuentas()
        {
            List<Cuenta> listaCuentasobj = new List<Cuenta>();
            using (var db = new BancaTecContext())
            {
                foreach (var cuen in db.Cuenta)
                {
                    listaCuentasobj.Add(cuen);
                }

                return listaCuentasobj;
            }
        }

        public static Cuenta GetCuenta(int num)
        {
            using (var db = new BancaTecContext())
            {
                var cuenta = db.Cuenta
                    .Where(b => b.NumCuenta == num)
                    .FirstOrDefault();

                return cuenta;
            }
        }

        public static void AddCuenta(Cuenta cuen)
        {
            using (var db = new BancaTecContext())
            {
                db.Cuenta.Add(cuen);
                db.SaveChanges();
            }
        }

        public static void UpdateCuenta(Cuenta cuen)
        {
            using (var db = new BancaTecContext())
            {
                var cuenta = db.Cuenta
                                .Where(b => b.NumCuenta == cuen.NumCuenta)
                                .FirstOrDefault();
                if (cuenta != null)
                {
                    foreach (PropertyInfo property in typeof(Asesor).GetProperties())
                    {
                        if (!property.PropertyType.AssemblyQualifiedName.Contains("ICollection") && !(property.Name == "Estado") && !property.PropertyType.AssemblyQualifiedName.Contains("Cliente"))
                        {
                            property.SetValue(cuenta, property.GetValue(cuen, null), null);
                        }
                    }
                }
                db.SaveChanges();
            }
        }

        public static void DeleteCuenta(int numero)
        {
            using (var db = new BancaTecContext())
            {
                var cuenta = db.Cuenta
                                .Where(b => b.NumCuenta == numero)
                                .FirstOrDefault();
                if (cuenta != null)
                {
                    cuenta.Estado = 'I';
                }
                db.SaveChanges();
            }
        }
    }
}
