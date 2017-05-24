﻿using System;
using BancaTec;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Npgsql;

namespace Operations
{
    public class Operations
    {
        #region Variables internas
        //TOdas estas variables son declaradas aquí para evitar hacerlo en todos los métodos que las ocupan
        private static string connString;
        private ErrorHandler.ErrorHandler err;

        public Operations(string _connString)
        {
            err = new ErrorHandler.ErrorHandler();
            connString = _connString;
        }

        public decimal PMT(double interest, int numPeriods, double prevValue)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                // Connect to a PostgreSQL database
                conn.Open();
                string commandString = string.Format("SELECT public.pmt({0}, {1}, {2})", interest.ToString(), numPeriods.ToString(), prevValue.ToString());
                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand(commandString, conn);

                // Execute the query and obtain the value of the first column of the first row
                //Int64 count = (Int64)command.ExecuteScalar();
                NpgsqlDataReader dr = command.ExecuteReader();

                dr.Read();
                string asd = dr["pmt"].ToString();
                return decimal.Parse(asd);
            }
        }

        public List<Comision> ReporteComisiones()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                // Connect to a PostgreSQL database
                conn.Open();
                List<Comision> listaComisiones = new List<Comision>();
                string commandString = "SELECT * FROM \"reportecomisiones\"()";
                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand(commandString, conn);

                // Execute the query and obtain the value of the first column of the first row
                //Int64 count = (Int64)command.ExecuteScalar();
                NpgsqlDataReader dr = command.ExecuteReader();

                while(dr.Read())
                {
                    listaComisiones.Add(new Comision(dr));
                }
                return listaComisiones;
            }
        }

        public string GenerarCalendarioPagos(string cedula, int meses)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                // Connect to a PostgreSQL database
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = conn;

                string getPrestamo = "SELECT MAX(\"Numero\") FROM \"PRESTAMO\" WHERE \"CedCliente\"='" + cedula + "';";
                command.CommandText = getPrestamo;
                NpgsqlDataReader dr1 = command.ExecuteReader();
                dr1.Read();
                string numPrestamost = dr1[0].ToString();
                int numPrestamo = int.Parse(numPrestamost);
                dr1.Close();
                List <Comision> listaComisiones = new List<Comision>();
                string commandString = string.Format("SELECT * FROM \"calendariopagos\"({0}, {1})", numPrestamo.ToString(), meses.ToString());
                // Define a query returning a single row result set

                // Execute the query and obtain the value of the first column of the first row
                //Int64 count = (Int64)command.ExecuteScalar();
                command.CommandText = commandString;
                NpgsqlDataReader dr = command.ExecuteReader();

                dr.Read();
                string respuesta = dr["calendariopagos"].ToString();
                return respuesta;
            }
        }

        public List<Mora> ReporteDeMora(string cedula)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                // Connect to a PostgreSQL database
                conn.Open();
                List<Mora> listaMoras = new List<Mora>();
                string commandString = string.Format("SELECT * FROM \"reportedemora\"('{0}');", cedula);
                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand(commandString, conn);

                // Execute the query and obtain the value of the first column of the first row
                //Int64 count = (Int64)command.ExecuteScalar();
                NpgsqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    listaMoras.Add(new Mora(dr));
                }
                return listaMoras;
            }
        }

        public List<MoraFechas> ReporteDeMoraFechas(string cedula)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                // Connect to a PostgreSQL database
                conn.Open();
                List<MoraFechas> listaMoras = new List<MoraFechas>();
                string commandString = string.Format("SELECT * FROM \"reportedemorafechas\"('{0}')", cedula);
                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand(commandString, conn);

                // Execute the query and obtain the value of the first column of the first row
                //Int64 count = (Int64)command.ExecuteScalar();
                NpgsqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    listaMoras.Add(new MoraFechas(dr));
                }
                return listaMoras;
            }
        }

        public decimal CambioMoneda(decimal entrada, string inicial, string fin)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                // Connect to a PostgreSQL database
                conn.Open();
                List<MoraFechas> listaMoras = new List<MoraFechas>();
                string commandString = string.Format("select \"cambiomoneda\"({0}, '{1}', '{2}')", entrada.ToString(), inicial, fin);
                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand(commandString, conn);

                // Execute the query and obtain the value of the first column of the first row
                decimal resultado = (decimal)command.ExecuteScalar();
                return resultado;
            }
        }

        public string RealizarTransferencia(decimal monto, int emisora, int receptora, string moneda)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                // Connect to a PostgreSQL database
                conn.Open();
                List<Comision> listaComisiones = new List<Comision>();
                string commandString = string.Format("SELECT \"transferencia\"({0}, {1}, {2}, '{3}')", emisora.ToString(), receptora.ToString(), monto.ToString(), moneda);
                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand(commandString, conn);

                // Execute the query and obtain the value of the first column of the first row
                //Int64 count = (Int64)command.ExecuteScalar();
                NpgsqlDataReader dr = command.ExecuteReader();

                dr.Read();
                string respuesta = dr["calendariopagos"].ToString();
                return respuesta;
            }
        }

        public string PagoPrestamoOrdinarioCliente(int cuenta, int prestamo)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                // Connect to a PostgreSQL database
                conn.Open();
                List<Comision> listaComisiones = new List<Comision>();
                string commandString = string.Format("SELECT \"pagoprestamoordinariocliente\"({0}, {1})", cuenta.ToString(), prestamo.ToString());
                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand(commandString, conn);

                // Execute the query and obtain the value of the first column of the first row
                //Int64 count = (Int64)command.ExecuteScalar();
                NpgsqlDataReader dr = command.ExecuteReader();

                dr.Read();
                string respuesta = dr["pagoprestamoordinariocliente"].ToString();
                return respuesta;
            }
        }

        public string PagoPrestamoExtraordinarioCliente(int cuenta, int prestamo, int extra)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                // Connect to a PostgreSQL database
                conn.Open();
                List<Comision> listaComisiones = new List<Comision>();
                string commandString = string.Format("SELECT \"pagoprestamoextraordinariocliente\"({0}, {1}, {2})", cuenta.ToString(), prestamo.ToString(), extra.ToString());
                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand(commandString, conn);

                // Execute the query and obtain the value of the first column of the first row
                //Int64 count = (Int64)command.ExecuteScalar();
                NpgsqlDataReader dr = command.ExecuteReader();

                dr.Read();
                string respuesta = dr["pagoprestamoextraordinariocliente"].ToString();
                return respuesta;
            }
        }

        public string RealizarMovimiento(Movimiento mov)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                // Connect to a PostgreSQL database
                conn.Open();
                List<Comision> listaComisiones = new List<Comision>();
                string commandString = string.Format("SELECT \"realizarmovimiento\"('{0}', '{1}', {2}, {3})", mov.Tipo, mov.Moneda, mov.Monto.ToString(), mov.NumCuenta.ToString());
                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand(commandString, conn);

                // Execute the query and obtain the value of the first column of the first row
                //Int64 count = (Int64)command.ExecuteScalar();
                NpgsqlDataReader dr = command.ExecuteReader();

                dr.Read();
                string respuesta = dr["realizarmovimiento"].ToString();
                return respuesta;
            }
        }
        #endregion
        /// <summary>
        /// Get Exception if any
        /// </summary>
        /// <returns> Error Message</returns>
        public string GetException()
        {
            return err.ErrorMessage.ToString();
        }
    }
}
