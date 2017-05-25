using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

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
        public int NumCuenta { get; set; }
        public decimal Saldo { get; set; }
        [XmlIgnore]
        public char Estado { get; set; }
        [XmlIgnore]
        public virtual ICollection<Movimiento> Movimiento { get; set; }
        [XmlIgnore]
        public virtual ICollection<Tarjeta> Tarjeta { get; set; }
        [XmlIgnore]
        public virtual ICollection<Transferencia> TransferenciaCuentaEmisoraNavigation { get; set; }
        [XmlIgnore]
        public virtual ICollection<Transferencia> TransferenciaCuentaReceptoraNavigation { get; set; }
        [XmlIgnore]
        public virtual Cliente CedClienteNavigation { get; set; }

        public static List<Cuenta> GetCuentas(string cedula)
        {
            List<Cuenta> listaCuentasobj = new List<Cuenta>();
            using (var db = new BancaTecContext())
            {
                var listaCuentas = db.Cuenta
                    .Where(b => b.CedCliente == cedula && b.Estado.Equals('A'));

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
                    if (cuen.Estado == 'A')
                    {
                        listaCuentasobj.Add(cuen);
                    }
                }
                return listaCuentasobj;
            }
        }

        public static Cuenta GetCuenta(int num)
        {
            using (var db = new BancaTecContext())
            {
                var cuenta = db.Cuenta
                    .Where(b => b.NumCuenta == num && b.Estado.Equals('A'))
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
                    foreach (PropertyInfo property in typeof(Cuenta).GetProperties())
                    {
                        if (!property.PropertyType.AssemblyQualifiedName.Contains("ICollection") && !(property.Name == "Estado") && !property.PropertyType.AssemblyQualifiedName.Contains("Cliente"))
                        {
                            property.SetValue(cuenta, property.GetValue(cuen, null), null);
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

        public static void DeleteCuenta(int numero)
        {
            using (var db = new BancaTecContext())
            {
                var cuenta = db.Cuenta
                                .Where(b => b.NumCuenta == numero)
                                .FirstOrDefault();
                var tarjetas = cuenta.Tarjeta;
                foreach (var item in tarjetas)
                {
                    BancaTec.Tarjeta.DeleteTarjeta(item.Numero);
                }
                if (cuenta != null)
                {
                    cuenta.Estado = 'I';
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
