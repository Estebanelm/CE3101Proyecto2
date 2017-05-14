using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BancaTec
{
    public partial class Cliente
    {
        public Cliente()
        {
            Cuenta = new HashSet<Cuenta>();
            Pago = new HashSet<Pago>();
            Prestamo = new HashSet<Prestamo>();
        }

        public string Nombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PriApellido { get; set; }
        public string SegApellido { get; set; }
        public string Cedula { get; set; }
        public string Tipo { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public decimal Ingreso { get; set; }
        public char Estado { get; set; }
        public string Contrasena { get; set; }

        public virtual ICollection<Cuenta> Cuenta { get; set; }
        public virtual ICollection<Pago> Pago { get; set; }
        public virtual ICollection<Prestamo> Prestamo { get; set; }

        public static Cliente GetCliente(string cedula)
        {
            using (var db = new BancaTecContext())
            {
                var cliente = db.Cliente
                    .Where(b => b.Cedula == cedula)
                    .FirstOrDefault();

                return cliente;
            }
        }

        public static List<Cliente> GetClientes()
        {
            List<Cliente> lista_clientes = new List<Cliente>();
            using (var db = new BancaTecContext())
            {
                foreach (var cliente in db.Cliente)
                {
                    lista_clientes.Add(cliente);
                }
            }
            return lista_clientes;
        }

        public static void AddCliente(Cliente clie)
        {
            using (var db = new BancaTecContext())
            {
                db.Cliente.Add(clie);
                db.SaveChanges();
            }
        }

        public static void UpdateCliente(Cliente clie)
        {
            using (var db = new BancaTecContext())
            {
                var cliente = db.Asesor
                                .Where(b => b.Cedula == clie.Cedula)
                                .FirstOrDefault();
                if (cliente != null)
                {
                    foreach (PropertyInfo property in typeof(Asesor).GetProperties())
                    {
                        if (!property.PropertyType.AssemblyQualifiedName.Contains("ICollection") && !(property.Name == "Estado"))
                        {
                            property.SetValue(cliente, property.GetValue(clie, null), null);
                        }
                    }
                }
                db.SaveChanges();
            }
        }

        public static void DeleteCliente(string cedula)
        {
            using (var db = new BancaTecContext())
            {
                var cliente = db.Asesor
                                .Where(b => b.Cedula == cedula)
                                .FirstOrDefault();
                if (cliente != null)
                {
                    cliente.Estado = 'I';
                }
                db.SaveChanges();
            }
        }
    }
}
