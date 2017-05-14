using System;
using BancaTec;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Operations
{
    public class Operations
    {
        #region Variables internas
        //TOdas estas variables son declaradas aquí para evitar hacerlo en todos los métodos que las ocupan
        private SqlConnection conn;
        private static string connString;
        private SqlCommand command;
        private static List<Empleado> empList;
        private ErrorHandler.ErrorHandler err;

        public Operations(string _connString)
        {
            err = new ErrorHandler.ErrorHandler();
            connString = _connString;
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
