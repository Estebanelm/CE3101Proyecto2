using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BancaTec
{
    public class Sucursal
    {

        private string _Codigo;
        private string _Nombre;
        private int _Telefono;
        private string _Direccion;
        private int _Ced_administrador;

        public Sucursal()
        { }

        public Sucursal(HttpContext context)
        {
            _Codigo = context.Request["Codigo"];
            _Nombre = context.Request["Nombre"];
            string _Telefono_temp = context.Request["Telefono"];
            _Telefono = int.Parse(_Telefono_temp);
            _Direccion = context.Request["Direccion"];
            string _Ced_administrador_temp = context.Request["Ced_administrador"];
            if (_Ced_administrador_temp == null)
            {
                _Ced_administrador = 0;
            }
            else
            {
                _Ced_administrador = int.Parse(_Ced_administrador_temp);
            }
        }

        public string Codigo
        {
            get
            {
                return _Codigo;
            }

            set
            {
                _Codigo = value;
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

        public int Telefono
        {
            get
            {
                return _Telefono;
            }

            set
            {
                _Telefono = value;
            }
        }

        public string Direccion
        {
            get
            {
                return _Direccion;
            }

            set
            {
                _Direccion = value;
            }
        }

        public int Ced_administrador
        {
            get
            {
                return _Ced_administrador;
            }

            set
            {
                _Ced_administrador = value;
            }
        }
    }
}
