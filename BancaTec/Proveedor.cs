using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BancaTec
{
    public class Proveedor
    {


        private int _Cedula;
        private string _Tipo;
        private string _Nombre;

        public Proveedor()
        { }

        public Proveedor(HttpContext context)
        {
            string _Cedula_temp = context.Request["Cedula"];
            _Cedula = int.Parse(_Cedula_temp);
            _Tipo = context.Request["Tipo"];
            _Nombre = context.Request["Nombre"];

        }

        public int Cedula
        {
            get
            {
                return _Cedula;
            }

            set
            {
                _Cedula = value;
            }
        }

        public string Tipo
        {
            get
            {
                return _Tipo;
            }

            set
            {
                _Tipo = value;
            }
        }

        public string Nombre
        {
            get
            {
                return _Nombre;
            }

            set
            {
                _Nombre = value;
            }
        }
    }
}
