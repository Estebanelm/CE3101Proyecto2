using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operations
{
    public class Mora
    {
        private string NombreCompleto { set; get; }
        private string Cedula { get; set; }
        private int NumPrestamo { get; set; }
        private long Vencidas { get; set; }
        private decimal Monto { get; set; }
        private string Moneda { get; set; }

        public Mora(NpgsqlDataReader dr)
        {
            NombreCompleto = dr["nombrecompleto"].ToString();
            Cedula = dr["cedula"].ToString();
            NumPrestamo = (int)dr["numprestamo"];
            Vencidas = (long)dr["vencidas"];
            Monto = (decimal)dr["monto"];
            Moneda = dr["moneda"].ToString();
        }
    }
}
