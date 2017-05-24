using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Operations
{
    public class Mora
    {
        public Mora()
        { }

        private string nombreCompleto;
        private string cedula;
        private string moneda;
        private int numPrestamo;
        private long vencidas;
        private decimal monto;
        

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

        public long Vencidas
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

        

        public Mora(NpgsqlDataReader dr)
        {
            NombreCompleto = (string)dr["nombrecompleto"];
            Cedula = (string)dr["cedula"];
            NumPrestamo = (int)dr["numprestamo"];
            Vencidas = (long)dr["vencidas"];
            Monto = (decimal)dr["monto"];
            Moneda = (string)dr["moneda"];
        }
    }
}
