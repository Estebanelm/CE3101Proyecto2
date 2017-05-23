using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operations
{
    public class MoraFechas
    {
        private string NombreCompleto { set; get; }
        private string Cedula { get; set; }
        private int NumPrestamo { get; set; }
        private DateTime Vencidas { get; set; }
        private decimal Monto { get; set; }
        private string Moneda { get; set; }

        public MoraFechas(NpgsqlDataReader dr)
        {
            NombreCompleto = dr["nombrecompleto"].ToString();
            Cedula = dr["cedula"].ToString();
            NumPrestamo = (int)dr["numprestamo"];
            Vencidas = (DateTime)dr["vencidas"];
            Monto = (decimal)dr["monto"];
            Moneda = dr["moneda"].ToString();
        }
    }
}
