using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operations
{
    public class Comision
    {
        public Comision()
        { }

        private string nombreCompleto;
        private decimal metaColones; 
        private decimal metaDolares;
        private decimal totalColones;
        private decimal totalDolares;
        private decimal comisionColones;
        private decimal comisionDolares;

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

        public decimal MetaColones
        {
            get
            {
                return metaColones;
            }

            set
            {
                metaColones = value;
            }
        }

        public decimal MetaDolares
        {
            get
            {
                return metaDolares;
            }

            set
            {
                metaDolares = value;
            }
        }

        public decimal TotalColones
        {
            get
            {
                return totalColones;
            }

            set
            {
                totalColones = value;
            }
        }

        public decimal TotalDolares
        {
            get
            {
                return totalDolares;
            }

            set
            {
                totalDolares = value;
            }
        }

        public decimal ComisionColones
        {
            get
            {
                return comisionColones;
            }

            set
            {
                comisionColones = value;
            }
        }

        public decimal ComisionDolares
        {
            get
            {
                return comisionDolares;
            }

            set
            {
                comisionDolares = value;
            }
        }

        public Comision(NpgsqlDataReader dr)
        {
            NombreCompleto = dr["nombrecompleto"].ToString();
            MetaColones = (decimal)dr["metacolones"];
            MetaDolares = (decimal)dr["metadolares"];
            TotalColones = (decimal)dr["totalcolones"];
            TotalDolares = (decimal)dr["totaldolares"];
            ComisionColones = (decimal)dr["comisioncolones"];
            ComisionDolares = (decimal)dr["comisiondolares"];
        }
    }
}
