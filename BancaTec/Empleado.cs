// Author - Anshu Dutta
// Contact - anshu.dutta@gmail.com
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace BancaTec
{
    public class Empleado
    {
        private string _nombre;
        private string _pri_apellido;
        private string _seg_apellido;
        private int _cedula;
        private double _salario_por_hora;
        private string _fecha_inicio;
        private string _fecha_nacimiento;
        private string _codigo_sucursal;
        private string _contrasena;

        public Empleado()
        { }
        
        public Empleado(HttpContext context)
        {
            _nombre = context.Request["Nombre"];
            _pri_apellido = context.Request["Pri_apellido"];
            _seg_apellido = context.Request["Seg_apellido"];
            if (_seg_apellido == null)
            {
                _seg_apellido = "";
            }
            string cedula_temp = context.Request["Cedula"];
            _cedula = int.Parse(cedula_temp);
            string salario_temp = context.Request["Salario_por_hora"];
            _salario_por_hora = double.Parse(salario_temp);
            _fecha_inicio = context.Request["Fecha_inicio"];
            _fecha_nacimiento = context.Request["Fecha_nacimiento"];
            _codigo_sucursal = context.Request["Codigo_sucursal"];
            _contrasena = context.Request["Contrasena"];
            if (_contrasena == null)
            {
                _contrasena = "";
            }

        }

        /// <summary>
        /// Property First Name
        /// </summary>
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        /// <summary>
        /// Property Last Name
        /// </summary>
        public string Pri_apellido
        {
            get { return _pri_apellido; }
            set { _pri_apellido = value; }
        }
        /// <summary>
        /// Property Employee Code
        /// </summary>
        public int Cedula
        {
            get { return _cedula; }
            set { _cedula = value; }
        }
        /// <summary>
        /// Property Designation
        /// </summary>
        public string Seg_apellido
        {
            get { return _seg_apellido; }
            set {_seg_apellido = value;}
        }

        public double Salario_por_hora
        {
            get {return _salario_por_hora;}
            set
            {
                _salario_por_hora = value;
            }
        }

        public string Fecha_inicio
        {
            get{return _fecha_inicio;}
            set
            {
                _fecha_inicio = value;
            }
        }

        public string Fecha_nacimiento
        {
            get
            {
                return _fecha_nacimiento;
            }

            set
            {
                _fecha_nacimiento = value;
            }
        }

        public string Sucursal
        {
            get
            {
                return _codigo_sucursal;
            }

            set
            {
                _codigo_sucursal = value;
            }
        }

        public string Contrasena
        {
            get
            {
                return _contrasena;
            }

            set
            {
                _contrasena = value;
            }
        }

        /// <summary>
        /// Method - Returns Employee Full Name
        /// </summary>
        /// <returns></returns>
        public string getEmployeeName()
        {
            string fullName = Nombre + ' ' + Pri_apellido + ' ' + Seg_apellido;
            return fullName;
        }

    }
}
