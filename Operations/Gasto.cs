using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Operations
{
    public class Gasto
    {
        private string _fecha;
        private string _proveedor;
        private string _descripcion;
        private double _monto;

        public Gasto ()
        { }

        public string Fecha
        {
            get
            {
                return _fecha;
            }

            set
            {
                _fecha = value;
            }
        }

        public string Proveedor
        {
            get
            {
                return _proveedor;
            }

            set
            {
                _proveedor = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return _descripcion;
            }

            set
            {
                _descripcion = value;
            }
        }

        public double Monto
        {
            get
            {
                return _monto;
            }

            set
            {
                _monto = value;
            }
        }
    }
}
