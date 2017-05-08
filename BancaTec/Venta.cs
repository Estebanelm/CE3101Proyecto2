using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BancaTec
{
    public class Venta
    {


        private int _Codigo;
        private int _Descuento;
        private double _Precio_total;
        private string _Codigo_sucursal;
        private int _Cedula_cajero;
        private double _Impuesto;

        public Venta()
        { }

        public Venta(HttpContext context)
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
            string _Descuento_temp = context.Request["Descuento"];
            _Descuento = int.Parse(_Descuento_temp);
            string _Precio_total_temp = context.Request["Precio_total"];
            _Precio_total = double.Parse(_Precio_total_temp);
            _Codigo_sucursal = context.Request["codigo_sucursal"];
            string _Cedula_cajero_temp = context.Request["Cedula_cajero"];
            _Cedula_cajero = int.Parse(_Cedula_cajero_temp);
            string _Impuesto_temp = context.Request["Impuesto"];
            if (_Impuesto_temp == null)
            {
                _Impuesto = 0;
            }
            else
            {
                _Impuesto = double.Parse(_Impuesto_temp);
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

        public int Descuento
        {
            get
            {
                return _Descuento;
            }

            set
            {
                _Descuento = value;
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

        public int Cedula_cajero
        {
            get
            {
                return _Cedula_cajero;
            }

            set
            {
                _Cedula_cajero = value;
            }
        }

        public double Impuesto
        {
            get
            {
                return _Impuesto;
            }

            set
            {
                _Impuesto = value;
            }
        }
    }
}
