using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BancaTec
{
    public class Producto
    {


        private bool _Exento;
        private int _Codigo_barras;
        private string _Nombre;
        private string _Descripcion;
        private int _Impuesto;
        private double _Precio_compra;
        private int _Descuento;
        private string _Codigo_sucursal;
        private int _Cantidad;
        private double _Precio_venta;
        private int _Cedula_proveedor;

        public Producto()
        { }

        public Producto(HttpContext context)
        {
            string _Exento_temp = context.Request["Exento"];
            _Exento = bool.Parse(_Exento_temp);
            string _Codigo_barras_temp = context.Request["Codigo_barras"];
            _Codigo_barras = int.Parse(_Codigo_barras_temp);
            _Nombre = context.Request["Nombre"];
            _Descripcion = context.Request["Descripcion"];
            string _Impuesto_temp = context.Request["Impuesto"];
            _Impuesto = int.Parse(_Impuesto_temp);
            string _Precio_compra_temp = context.Request["Precio_compra"];
            _Precio_compra = double.Parse(_Precio_compra_temp);
            string _Descuento_temp = context.Request["Descuento"];
            _Descuento = int.Parse(_Descuento_temp);
            _Codigo_sucursal = context.Request["Codigo_sucursal"];
            string _Cantidad_temp = context.Request["Cantidad"];
            _Cantidad = int.Parse(_Cantidad_temp);
            string _Precio_venta_temp = context.Request["Precio_venta"];
            _Precio_venta = double.Parse(_Precio_venta_temp);
            string _Proveedor_temp = context.Request["Cedula_proveedor"];
            if (_Proveedor_temp == null)
            {
                _Cedula_proveedor = 0;
            }
            else
            {
                _Cedula_proveedor = int.Parse(_Proveedor_temp);
            }

        }

        public bool Exento
        {
            get
            {
                return _Exento;
            }

            set
            {
                _Exento = value;
            }
        }

        public int Codigo_barras
        {
            get
            {
                return _Codigo_barras;
            }

            set
            {
                _Codigo_barras = value;
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

        public int Impuesto
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

        public double Precio_compra
        {
            get
            {
                return _Precio_compra;
            }

            set
            {
                _Precio_compra = value;
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

        public double Precio_venta
        {
            get
            {
                return _Precio_venta;
            }

            set
            {
                _Precio_venta = value;
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
    }
}
