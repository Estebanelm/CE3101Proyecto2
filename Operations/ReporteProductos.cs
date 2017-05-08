using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Operations
{
    public class ReporteProductos
    {
        private string _Nombre;
        private double _Costo;
        private int _Cantidad;

        public ReporteProductos()
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
