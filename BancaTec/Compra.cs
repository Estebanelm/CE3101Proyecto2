using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BancaTec
{
    public class Compra
    {

        private int _Codigo;
        private int _Cedula_proveedor;
        private string _Descripcion;
        private string _Foto;
        private string _Fecha_Registro;
        private string _Fecha_real;
        private string _Codigo_sucursal;
        private double _Precio_total;

        public Compra()
        { }

        public Compra(HttpContext context)
        {
            if (context.Request["Codigo"] == null)
            {
                _Codigo = 0;
            }
            else
            {
                string _codigo_temp = context.Request["Codigo"];
                _Codigo = int.Parse(_codigo_temp);
            }
            string _Cedula_proveedor_temp = context.Request["Cedula_proveedor"];
            _Cedula_proveedor = int.Parse(_Cedula_proveedor_temp);
            _Descripcion = context.Request["Descripcion"];
            _Foto = context.Request["Foto"];
            _Fecha_Registro = context.Request["Fecha_registro"];
            _Fecha_real = context.Request["Fecha_real"];
            _Codigo_sucursal = context.Request["Codigo_sucursal"];
            string _Precio_total_temp = context.Request["Precio_total"];
            if (_Precio_total_temp == null)
            {
                _Precio_total = 0;
            }
            else
            {
                _Precio_total = double.Parse(_Precio_total_temp);
            }

        }

        public int Codigo
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

        public int Cedula_proveedor
        {
            get
            {
                return _Cedula_proveedor;
            }

            set
            {
                _Cedula_proveedor = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return _Descripcion;
            }

            set
            {
                _Descripcion = value;
            }
        }

        public string Foto
        {
            get
            {
                return _Foto;
            }

            set
            {
                _Foto = value;
            }
        }

        public string Fecha_Registro
        {
            get
            {
                return _Fecha_Registro;
            }

            set
            {
                _Fecha_Registro = value;
            }
        }

        public string Fecha_real
        {
            get
            {
                return _Fecha_real;
            }

            set
            {
                _Fecha_real = value;
            }
        }

        public string Codigo_sucursal
        {
            get
            {
                return _Codigo_sucursal;
            }

            set
            {
                _Codigo_sucursal = value;
            }
        }

        public double Precio_total
        {
            get
            {
                return _Precio_total;
            }

            set
            {
                _Precio_total = value;
            }
        }
    }
}
