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
        public MoraFechas()
        { }

        private string nombreCompleto;
        private string cedula;
        private int numPrestamo;
        private DateTime vencidas;
        private decimal monto;
        private string moneda;

        public string NombreCompleto
        {
            get
            {
                return nombreCompleto;
            }

            set
            {
                nombreCompleto = value;
            }
        }

        public string Cedula
        {
            get
            {
                return cedula;
            }

            set
            {
                cedula = value;
            }
        }

        public int NumPrestamo
        {
            get
            {
                return numPrestamo;
            }

            set
            {
                numPrestamo = value;
            }
        }

        public DateTime Vencidas
        {
            get
            {
                return vencidas;
            }

            set
            {
                vencidas = value;
            }
        }

        public decimal Monto
        {
            get
            {
                return monto;
            }

            set
            {
                monto = value;
            }
        }

        public string Moneda
        {
            get
            {
                return moneda;
            }

            set
            {
                moneda = value;
            }
        }

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
