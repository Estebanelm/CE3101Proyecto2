using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace BancaTec
{
    public partial class Transferencia
    {
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public int CuentaEmisora { get; set; }
        public int CuentaReceptora { get; set; }
        public string Moneda { get; set; }
        public int Id { get; set; }
        [XmlIgnore]
        public char Estado { get; set; }
        [XmlIgnore]
        public virtual Cuenta CuentaEmisoraNavigation { get; set; }
        [XmlIgnore]
        public virtual Cuenta CuentaReceptoraNavigation { get; set; }

        public static List<Transferencia> GetTransferencias(int numcuenta)
        {
            List<Transferencia> listatransferenciasobj = new List<Transferencia>();
            using (var db = new BancaTecContext())
            {
                var listatransferencias = db.Transferencia
                    .Where(b => b.CuentaEmisora == numcuenta && b.CuentaReceptora == numcuenta && b.Estado.Equals('A'));
                foreach (var movimiento in listatransferencias)
                {
                    listatransferenciasobj.Add(movimiento);
                }
            }
            return listatransferenciasobj;
        }

        public static void AddTransferencia(Transferencia trans)
        {
            using (var db = new BancaTecContext())
            {
                db.Transferencia.Add(trans);
                db.SaveChanges();
            }
        }

        public static void UpdateTransferencia(Transferencia trans)
        {
            using (var db = new BancaTecContext())
            {
                var transferencia = db.Transferencia
                                .Where(b => b.Id == trans.Id)
                                .FirstOrDefault();
                if (transferencia != null)
                {
                    foreach (PropertyInfo property in typeof(Transferencia).GetProperties())
                    {
                        if (!property.PropertyType.AssemblyQualifiedName.Contains("ICollection") && !(property.Name == "Estado") && !property.PropertyType.AssemblyQualifiedName.Contains("Cuenta"))
                        {
                            property.SetValue(transferencia, property.GetValue(trans, null), null);
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

        public static void DeleteTransferencia(int id)
        {
            using (var db = new BancaTecContext())
            {
                var transferencia = db.Transferencia
                                .Where(b => b.Id == id)
                                .FirstOrDefault();
                if (transferencia != null)
                {
                    transferencia.Estado = 'I';
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
