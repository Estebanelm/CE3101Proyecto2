using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Operations
{
    public class ReporteProductosSucursal
    {
        private string _Nombre;
        private string _Descripcion;
        private double _Costo;
        private int _Cantidad;

        public ReporteProductosSucursal()
        { }

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

        public double Costo
        {
            get
            {
                return _Costo;
            }

            set
            {
                _Costo = value;
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
    }
}
