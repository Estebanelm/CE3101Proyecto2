using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BancaTec
{
    public class Productos_en_compra
    {

        private int _Codigo_producto;
        private int _Codigo_compra;
        private int _Cantidad;
        private double _Precio_total;

        public Productos_en_compra()
        { }

        public Productos_en_compra(HttpContext context)
        {
            string _Codigo_producto_temp = context.Request["Codigo_producto"];
            _Codigo_producto = int.Parse(_Codigo_producto_temp);
            string _Codigo_compra_temp = context.Request["Codigo_compra"];
            _Codigo_compra = int.Parse(_Codigo_compra_temp);
            string _Cantidad_temp = context.Request["Cantidad"];
            _Cantidad = int.Parse(_Cantidad_temp);
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

        public int Codigo_producto
        {
            get
            {
                return _Codigo_producto;
            }

            set
            {
                _Codigo_producto = value;
            }
        }

        public int Codigo_compra
        {
            get
            {
                return _Codigo_compra;
            }

            set
            {
                _Codigo_compra = value;
            }
        }

        public int Cantidad
        {
            get
            {
                return _Cantidad;
            }

            set
            {
                _Cantidad = value;
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
