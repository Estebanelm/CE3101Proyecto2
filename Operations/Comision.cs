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
        private string NombreCompleto { get; set; }
        private decimal MetaColones { get; set; }
        private decimal MetaDolares { get; set; }
        private decimal TotalColones { get; set; }
        private decimal TotalDolares { get; set; }
        private decimal ComisionColones { get; set; }
        private decimal ComisionDolares { get; set; }

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
