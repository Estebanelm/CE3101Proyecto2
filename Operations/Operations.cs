using System;
using BancaTec;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

//namespace Operations
//{
//    public class Operations
//    {
//        #region Variables internas
//        //TOdas estas variables son declaradas aquí para evitar hacerlo en todos los métodos que las ocupan
//        private SqlConnection conn;
//        private static string connString;
//        private SqlCommand command;       
//        private static List<Empleado> empList;
//        private static List<Sucursal> sucList;
//        private static List<Categoria> catList;
//        private static List<Compra> comList;
//        private static List<Horas> horList;
//        private static List<Producto> produList;
//        private static List<Productos_en_compra> producompList;
//        private static List<Productos_en_venta> produvenList;
//        private static List<Proveedor> proveList;
//        //private static List<Rol> rolList;
//        private static List<Venta> venList;
//        private ErrorHandler.ErrorHandler err;

//        public Operations(string _connString)
//        {
//            err = new ErrorHandler.ErrorHandler();
//            connString = _connString;            
//        }
//        #endregion
//        #region Operaciones sobre empleados
//        /// <summary>
//        /// Database INSERT - Add an Employee
//        /// </summary>
//        /// <param name="emp"></param>
//        public bool AddEmpleado(Empleado emp)
//        {
//            try
//            {
//                using (conn)
//                {
//                    //using parametirized query
//                    string sqlInserString =
//                    "INSERT INTO Empleado (Nombre, Pri_apellido, Seg_apellido, Cedula, Fecha_inicio, Salario_por_hora, Fecha_nacimiento, Codigo_sucursal, Contrasena) VALUES (@nombre, @pri_apellido, @seg_apellido, @cedula, @fecha_inicio, @salario_por_hora, @fecha_nacimiento, @codigo_sucursal, @contrasena)";
                   
//                    conn = new SqlConnection(connString);
                    
//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();
//                    command.CommandText = sqlInserString;
                    
//                    SqlParameter Nombreparam = new SqlParameter("@nombre", emp.Nombre);
//                    SqlParameter Pri_apellidoparam = new SqlParameter("@pri_apellido", emp.Pri_apellido);
//                    SqlParameter Cedulaparam = new SqlParameter("@cedula", emp.Cedula.ToString());
//                    SqlParameter Seg_apellidoparam = new SqlParameter("@seg_apellido", emp.Seg_apellido);
//                    SqlParameter Fecha_inicioparam = new SqlParameter("@fecha_inicio", emp.Fecha_inicio);
//                    SqlParameter Salario_por_horaparam = new SqlParameter("@salario_por_hora", emp.Salario_por_hora.ToString());
//                    SqlParameter Fecha_nacimientoparam = new SqlParameter("@fecha_nacimiento", emp.Fecha_nacimiento);
//                    SqlParameter Codigo_sucursalparam = new SqlParameter("@codigo_sucursal", emp.Sucursal);
//                    SqlParameter Contrasenaparam = new SqlParameter("@contrasena", emp.Contrasena);

//                    command.Parameters.AddRange(new SqlParameter[]{Nombreparam,Pri_apellidoparam, Seg_apellidoparam, Cedulaparam, Fecha_inicioparam, Salario_por_horaparam, Fecha_nacimientoparam, Codigo_sucursalparam, Contrasenaparam});
//                    command.ExecuteNonQuery();
//                    command.Connection.Close();
//                    return true;
//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Database UPDATE - Update an Employee
//        /// </summary>
//        /// <param name="emp"></param>
//        public bool UpdateEmpleado(Empleado emp)
//        {
//            try
//            {
//                using (conn)
//                {
//                    string sqlUpdateString =
//                    "UPDATE Empleado SET Nombre=@nombre, Pri_apellido=@pri_apellido, Seg_apellido=@seg_apellido, Fecha_inicio=@fecha_inicio, Salario_por_hora=@salario_por_hora, Fecha_nacimiento=@fecha_nacimiento, Codigo_sucursal=@codigo_sucursal, Contrasena=@contrasena WHERE Cedula=@cedula ";
//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();
//                    command.CommandText = sqlUpdateString;

//                    SqlParameter Nombreparam = new SqlParameter("@nombre", emp.Nombre);
//                    SqlParameter Pri_apellidoparam = new SqlParameter("@pri_apellido", emp.Pri_apellido);
//                    SqlParameter Cedulaparam = new SqlParameter("@cedula", emp.Cedula.ToString());
//                    SqlParameter Seg_apellidoparam = new SqlParameter("@seg_apellido", emp.Seg_apellido);
//                    SqlParameter Fecha_inicioparam = new SqlParameter("@fecha_inicio", emp.Fecha_inicio);
//                    SqlParameter Salario_por_horaparam = new SqlParameter("@salario_por_hora", emp.Salario_por_hora.ToString());
//                    SqlParameter Fecha_nacimientoparam = new SqlParameter("@fecha_nacimiento", emp.Fecha_nacimiento);
//                    SqlParameter Codigo_sucursalparam = new SqlParameter("@codigo_sucursal", emp.Sucursal);
//                    SqlParameter Contrasenaparam = new SqlParameter("@contrasena", emp.Contrasena);

//                    command.Parameters.AddRange(new SqlParameter[]{Nombreparam,Pri_apellidoparam, Seg_apellidoparam, Cedulaparam, Fecha_inicioparam, Salario_por_horaparam, Fecha_nacimientoparam, Codigo_sucursalparam, Contrasenaparam});
//                    command.ExecuteNonQuery();
//                    #region  Revision de que se hizo bien
//                    string buscarEmpleado = "SELECT * FROM Empleado WHERE Cedula=@cedula";
//                    string usuarioencontrado = "";
//                    command.CommandText = buscarEmpleado;
//                    SqlDataReader reader = command.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        usuarioencontrado = reader[0].ToString();
//                    }
//                    command.Connection.Close();
//                    if (usuarioencontrado != "")
//                    {
//                        return true;
//                    }
//                    else
//                    {
//                        return false;
//                    }
//                    #endregion
//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//       /// <summary>
//       /// Database DELETE - Delete an Employee
//       /// </summary>
//       /// <param name="cedula"></param>
//        public bool DeleteEmpleado(int cedula)
//        {
//            try
//            {
//                using (conn)
//                {

                    
                    
//                    string sqlDeleteString = "DELETE FROM Empleado WHERE Cedula=@cedula ";

//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();

//                    SqlParameter cedulaparam = new SqlParameter("@cedula", cedula);
//                    command.Parameters.Add(cedulaparam);
//                    #region Borrando de sucursal
//                    string borradodeadmin = "UPDATE Sucursal SET Ced_administrador=NULL WHERE Ced_administrador=@cedula";
//                    command.CommandText = borradodeadmin;
//                    command.ExecuteNonQuery();
//                    #endregion
//                    #region Borrando de venta
//                    string borradodecajero = "UPDATE Venta SET Cedula_cajero=NULL WHERE Cedula_cajero=@cedula";
//                    command.CommandText = borradodecajero;
//                    command.ExecuteNonQuery();
//                    #endregion
//                    #region Borrando de horas
//                    string sqlDeletefromHoras = "DELETE FROM Horas WHERE Ced_empleado=@cedula ";
//                    command.CommandText = sqlDeletefromHoras;
//                    #endregion
//                    command.ExecuteNonQuery();
//                    command.CommandText = sqlDeleteString;
//                    command.ExecuteNonQuery();
//                    command.Connection.Close();
//                    return true;
//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Database SELECT - Get an employee
//        /// </summary>
//        /// <param name="ID"></param>
//        /// <returns></returns>
//        public Empleado GetEmpleado(int ID)
//        {
//            try
//            {
//                if (empList==null)
//                {
//                    empList = GetEmpleados();
//                }
//                // enumerate through all employee list
//                // and select the concerned employee
//                foreach (Empleado emp in empList)
//                {
//                    if (emp.Cedula==ID)
//                    {
//                        return emp;
//                    }
//                }
//                return null;
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Method - Get list of all employees
//        /// </summary>
//        /// <returns>Employee</returns>
//        public List<Empleado> GetEmpleados()
//        {
//            try
//            {
//                using (conn)
//                {
//                    empList = new List<Empleado>();

//                    conn = new SqlConnection(connString);

//                    string sqlSelectString = "SELECT * FROM Empleado";
//                    command = new SqlCommand(sqlSelectString, conn);
//                    command.Connection.Open();

//                    SqlDataReader reader = command.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        Empleado emp = new Empleado();
//                        emp.Nombre = reader[0].ToString();
//                        emp.Pri_apellido = reader[1].ToString();
//                        emp.Seg_apellido = reader[2].ToString();
//                        string cedula_temp = reader[3].ToString();
//                        emp.Cedula = int.Parse(cedula_temp);                  
//                        emp.Fecha_inicio = reader[4].ToString();
//                        string salario_temp = reader[5].ToString();
//                        emp.Salario_por_hora = double.Parse(salario_temp);
//                        emp.Fecha_nacimiento = reader[6].ToString();
//                        emp.Sucursal = reader[7].ToString();
//                        emp.Contrasena = reader[8].ToString();
//                        empList.Add(emp);
//                    }
//                    command.Connection.Close();
//                    return empList;
//                }
                
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        #endregion
//        #region Operaciones sobre sucursales
//        /// <summary>
//        /// Database INSERT - Add a Sucursal
//        /// </summary>
//        /// <param name="suc"></param>
//        public bool AddSucursal(Sucursal suc)
//        {
//            try
//            {
//                using (conn)
//                {
//                    //using parametirized query

//                    string sqlInserString = "";
//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();
                    

//                    SqlParameter Codigoparam = new SqlParameter("@codigo", suc.Codigo);
//                    SqlParameter Nombreparam = new SqlParameter("@nombre", suc.Nombre);
//                    SqlParameter Telefonoparam = new SqlParameter("@telefono", suc.Telefono);
//                    SqlParameter Direccionparam = new SqlParameter("@direccion", suc.Direccion);
//                    SqlParameter Ced_administradorparam = new SqlParameter("@ced_administrador", suc.Ced_administrador);
//                    if (suc.Ced_administrador == 0)
//                    {
//                        sqlInserString = "INSERT INTO Sucursal (Codigo, Nombre, Telefono, Direccion, Ced_administrador) VALUES (@codigo, @nombre, @telefono, @direccion, NULL)";
//                    }
//                    else
//                    {
//                        sqlInserString = "INSERT INTO Sucursal (Codigo, Nombre, Telefono, Direccion, Ced_administrador) VALUES (@codigo, @nombre, @telefono, @direccion, @ced_administrador)";
//                    }
//                    command.CommandText = sqlInserString;
//                    command.Parameters.AddRange(new SqlParameter[] { Codigoparam, Nombreparam, Telefonoparam, Direccionparam, Ced_administradorparam});
//                    command.ExecuteNonQuery();
//                    command.Connection.Close();
//                    return true;

//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Database UPDATE - Update a Sucursal
//        /// </summary>
//        /// <param name="suc"></param>
//        public bool UpdateSucursal(Sucursal suc)
//        {
//            try
//            {
//                using (conn)
//                {
//                    string sqlUpdateString = "";
//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();

//                    SqlParameter Codigoparam = new SqlParameter("@codigo", suc.Codigo);
//                    SqlParameter Nombreparam = new SqlParameter("@nombre", suc.Nombre);
//                    SqlParameter Telefonoparam = new SqlParameter("@telefono", suc.Telefono.ToString());
//                    SqlParameter Direccionparam = new SqlParameter("@direccion", suc.Direccion);
//                    SqlParameter Ced_administradorparam= new SqlParameter("@ced_administrador", suc.Ced_administrador.ToString());
//                    if (suc.Ced_administrador == 0)
//                    {
//                        sqlUpdateString =
//                            "UPDATE Sucursal SET Nombre=@nombre, Telefono=@telefono, Direccion=@direccion, Ced_administrador=NULL WHERE Codigo=@codigo";
//                    }
//                    else
//                    {
//                        sqlUpdateString =
//                    "UPDATE Sucursal SET Nombre=@nombre, Telefono=@telefono, Direccion=@direccion, Ced_administrador=@ced_administrador WHERE Codigo=@codigo";
//                    }
//                    command.CommandText = sqlUpdateString;

//                    command.Parameters.AddRange(new SqlParameter[] { Codigoparam, Nombreparam, Telefonoparam, Direccionparam, Ced_administradorparam});
//                    command.ExecuteNonQuery();
//                    #region  Revision de que se hizo bien
//                    string buscarSucursal = "SELECT * FROM Sucursal WHERE Codigo=@codigo";
//                    string sucursalencontrada = "";
//                    command.CommandText = buscarSucursal;
//                    SqlDataReader reader = command.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        sucursalencontrada = reader[0].ToString();
//                    }
//                    command.Connection.Close();
//                    if (sucursalencontrada != "")
//                    {
//                        return true;
//                    }
//                    else
//                    {
//                        return false;
//                    }
//                    #endregion
//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Database DELETE - Delete a Sucursal
//        /// </summary>
//        /// <param name="codigo"></param>
//        public bool DeleteSucursal(string codigo)
//        {
//            try
//            {
//                using (conn)
//                {
//                    string sqlDeleteString =
//                    "DELETE FROM Sucursal WHERE Codigo=@codigo";

//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();
                    

//                    SqlParameter codigoparam = new SqlParameter("@codigo", codigo);
//                    command.Parameters.Add(codigoparam);

//                    #region Borrado de empleados
//                    string buscarempleados = "SELECT * FROM Empleado WHERE Codigo_sucursal=@codigo";
//                    command.CommandText = buscarempleados;
//                    List<int> listacedulas = new List<int>();
//                    SqlDataReader reader = command.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        string cedulanueva_temp = reader[3].ToString();
//                        int cedulanueva = int.Parse(cedulanueva_temp);
//                        listacedulas.Add(cedulanueva);
//                    }
//                    reader.Close();
//                    foreach (int cedulabuscar in listacedulas)
//                    {
//                        DeleteEmpleado(cedulabuscar);
//                    }

//                    #endregion
//                    #region Borrando de compra
//                    string borradodesucursal = "UPDATE Compra SET Codigo_sucursal=0 WHERE Codigo_sucursal=@codigo";
//                    command.CommandText = borradodesucursal;
//                    command.ExecuteNonQuery();
//                    #endregion
//                    #region Borrado de productos
//                    BorrarProductosSucursal(codigo, command);
//                    #endregion
//                    #region Borrando de venta
//                    string borradodesucursalventa = "UPDATE Venta SET Codigo_sucursal=0 WHERE Codigo_sucursal=@codigo";
//                    command.CommandText = borradodesucursalventa;
//                    command.ExecuteNonQuery();
//                    #endregion
//                    command.CommandText = sqlDeleteString;
//                    command.ExecuteNonQuery();
//                    command.Connection.Close();
//                    return true;
//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        public void BorrarProductosSucursal(string codigo, SqlCommand command)
//        {
//            string sqlbuscarProductos = "SELECT * FROM Producto WHERE Codigo_sucursal=@codigo";
//            command.CommandText = sqlbuscarProductos;
//            List<int> listaCodigoBarras = new List<int>();
//            SqlDataReader reader = command.ExecuteReader();
//            while (reader.Read())
//            {
//                string codigobarrastemp = reader[1].ToString();
//                int codigobarras = int.Parse(codigobarrastemp);
//                listaCodigoBarras.Add(codigobarras);
//            }
//            reader.Close();
//            if (listaCodigoBarras.Count != 0)
//            {
//                foreach (int codigoBarrasBorrar in listaCodigoBarras)
//                {
//                    string sqlborrarProductosCompra = "UPDATE Productos_en_compra SET Codigo_producto=NULL WHERE Codigo_producto='" + listaCodigoBarras.ToString() + "'";
//                    command.CommandText = sqlborrarProductosCompra;
//                    command.ExecuteNonQuery();
//                    string sqlborrarProductosVenta = "UPDATE Productos_en_venta SET Codigo_producto=NULL WHERE Codigo_producto='" + listaCodigoBarras.ToString() + "'";
//                    command.CommandText = sqlborrarProductosVenta;
//                    command.ExecuteNonQuery();
//                }
//            }
//            string sqlborradoProductos = "DELETE FROM Producto WHERE Codigo_sucursal=@codigo";
//            command.CommandText = sqlborradoProductos;
//            command.ExecuteNonQuery();
//        }
//        /// <summary>
//        /// Database SELECT - Get a sucursal
//        /// </summary>
//        /// <param name="codigo"></param>
//        /// <returns></returns>
//        public Sucursal GetSucursal(string codigo)
//        {
//            try
//            {
//                if (sucList == null)
//                {
//                    sucList = GetSucursales();
//                }
//                // enumerate through all employee list
//                // and select the concerned employee
//                foreach (Sucursal suc in sucList)
//                {
//                    if (suc.Codigo == codigo)
//                    {
//                        return suc;
//                    }
//                }
//                return null;
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Method - Get list of all employees
//        /// </summary>
//        /// <returns>Employee</returns>
//        public List<Sucursal> GetSucursales()
//        {
//            try
//            {
//                using (conn)
//                {
//                    sucList = new List<Sucursal>();

//                    conn = new SqlConnection(connString);

//                    string sqlSelectString = "SELECT * FROM Sucursal";
//                    command = new SqlCommand(sqlSelectString, conn);
//                    command.Connection.Open();

//                    SqlDataReader reader = command.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        Sucursal suc = new Sucursal();
//                        suc.Codigo = reader[0].ToString();
//                        suc.Nombre = reader[1].ToString();
//                        string _Telefono_temp = reader[2].ToString();
//                        suc.Telefono = int.Parse(_Telefono_temp);
//                        suc.Direccion = reader[3].ToString();
//                        string _Ced_administrador_temp = reader[4].ToString();
//                        if (_Ced_administrador_temp == "")
//                        {
//                            suc.Ced_administrador = 0;
//                        }
//                        else
//                        {
//                            suc.Ced_administrador = int.Parse(_Ced_administrador_temp);
//                        }
//                        sucList.Add(suc);
                        
//                    }
//                    command.Connection.Close();
//                    return sucList;
//                }

//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        #endregion
//        #region Operaciones sobre categorias
//        /// <summary>
//        /// Database INSERT - Add a Categoria
//        /// </summary>
//        /// <param name="cat"></param>
//        public bool AddCategoria(Categoria cat)
//        {
//            try
//            {
//                using (conn)
//                {
//                    //using parametirized query
//                    string sqlInserString =
//                    "INSERT INTO Categoria (Descripcion, Codigo_producto) VALUES (@descripcion, @codigo_producto)";
//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();
//                    command.CommandText = sqlInserString;

//                    SqlParameter Descripcionparam = new SqlParameter("@descripcion", cat.Descripcion);
//                    SqlParameter Codigo_productoparam = new SqlParameter("@codigo_producto", cat.Codigo_producto.ToString());
                    
//                    command.Parameters.AddRange(new SqlParameter[] { Descripcionparam, Codigo_productoparam});
//                    command.ExecuteNonQuery();
//                    command.Connection.Close();
//                    return true;

//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Database UPDATE - Update a Categoria
//        /// </summary>
//        /// <param name="cat"></param>
//        public bool UpdateCategoria(Categoria cat)
//        {
//            try
//            {
//                using (conn)
//                {
//                    string sqlUpdateString =
//                    "UPDATE Categoria SET Descripcion=@descripcion, Codigo_producto=@codigo_producto WHERE ID=@id ";
//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();
//                    command.CommandText = sqlUpdateString;

//                    SqlParameter IDparam = new SqlParameter("@id", cat.ID.ToString());
//                    SqlParameter Descripcionparam = new SqlParameter("@descripcion", cat.Descripcion);
//                    SqlParameter Codigo_productoparam = new SqlParameter("@codigo_producto", cat.Codigo_producto.ToString());

//                    command.Parameters.AddRange(new SqlParameter[] { IDparam, Descripcionparam, Codigo_productoparam });
//                    command.ExecuteNonQuery();

//                    #region  Revision de que se hizo bien
//                    string buscarCategoria = "SELECT * FROM Categoria WHERE ID=@id";
//                    string categoriaencontrada = "";
//                    command.CommandText = buscarCategoria;
//                    SqlDataReader reader = command.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        categoriaencontrada = reader[0].ToString();
//                    }
//                    command.Connection.Close();
//                    if (categoriaencontrada != "")
//                    {
//                        return true;
//                    }
//                    else
//                    {
//                        return false;
//                    }
//                    #endregion
//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Database DELETE - Delete a Categoria
//        /// </summary>
//        /// <param name="iD"></param>
//        public bool DeleteCategoria(int iD)
//        {
//            try
//            {
//                using (conn)
//                {
//                    string sqlDeleteString =
//                    "DELETE FROM Categoria WHERE ID=@id ";

//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();
//                    command.CommandText = sqlDeleteString;

//                    SqlParameter IDparam = new SqlParameter("@id", iD);
//                    command.Parameters.Add(IDparam);
//                    command.ExecuteNonQuery();
//                    command.Connection.Close();
//                    return true;
//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Database SELECT - Get an employee
//        /// </summary>
//        /// <param name="ID"></param>
//        /// <returns></returns>
//        public Categoria GetCategoria(int ID)
//        {
//            try
//            {
//                if (catList == null)
//                {
//                    catList = GetCategorias();
//                }
//                // enumerate through all employee list
//                // and select the concerned employee
//                foreach (Categoria cat in catList)
//                {
//                    if (cat.ID == ID)
//                    {
//                        return cat;
//                    }
//                }
//                return null;
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Method - Get list of all Categorias
//        /// </summary>
//        /// <returns>Employee</returns>
//        public List<Categoria> GetCategorias()
//        {
//            try
//            {
//                using (conn)
//                {
//                    catList = new List<Categoria>();

//                    conn = new SqlConnection(connString);

//                    string sqlSelectString = "SELECT * FROM Categoria";
//                    command = new SqlCommand(sqlSelectString, conn);
//                    command.Connection.Open();

//                    SqlDataReader reader = command.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        Categoria cat = new Categoria();
//                        string ID_temp = reader[0].ToString();
//                        cat.ID = int.Parse(ID_temp);
//                        cat.Descripcion = reader[1].ToString();
//                        string _Codigo_producto_temp = reader[2].ToString();
//                        if (_Codigo_producto_temp == "")
//                        {
//                            cat.Codigo_producto = 0;
//                        }
//                        else
//                        {
//                            cat.Codigo_producto = int.Parse(_Codigo_producto_temp);
//                        }
//                        catList.Add(cat);
//                    }
//                    command.Connection.Close();
//                    return catList;
//                }

//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        #endregion
//        #region Operaciones sobre compras
//        /// <summary>
//        /// Database INSERT - Add a Compra
//        /// </summary>
//        /// <param name="com"></param>
//        public bool AddCompra(Compra com)
//        {
//            try
//            {
//                using (conn)
//                {
//                    //using parametirized query
//                    string sqlInserString =
//                    "INSERT INTO Compra (Cedula_proveedor, Descripcion, Foto, Fecha_registro, Fecha_real, Codigo_sucursal, Precio_total) VALUES (@cedula_proveedor, @descripcion, @foto, @fecha_registro, @fecha_real, @codigo_sucursal, @precio_total)";

//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();
//                    command.CommandText = sqlInserString;

//                    SqlParameter Cedula_proveedorparam = new SqlParameter("@cedula_proveedor", com.Cedula_proveedor.ToString());
//                    SqlParameter Descripcionparam = new SqlParameter("@descripcion", com.Descripcion);
//                    SqlParameter Fotoparam = new SqlParameter("@foto", com.Foto);
//                    SqlParameter Fecha_registroparam = new SqlParameter("@fecha_registro", com.Fecha_Registro);
//                    SqlParameter Fecha_realparam = new SqlParameter("@fecha_real", com.Fecha_real);
//                    SqlParameter Codigo_sucursalparam = new SqlParameter("@codigo_sucursal", com.Codigo_sucursal);
//                    SqlParameter Precio_totalparam = new SqlParameter("@precio_total", com.Precio_total);
                    
//                    command.Parameters.AddRange(new SqlParameter[] { Cedula_proveedorparam, Descripcionparam, Fotoparam, Fecha_registroparam, Fecha_realparam, Codigo_sucursalparam, Precio_totalparam });
//                    command.ExecuteNonQuery();
//                    command.Connection.Close();
//                    return true;
//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Database UPDATE - Update a Compra
//        /// </summary>
//        /// <param name="com"></param>
//        public bool UpdateCompra(Compra com)
//        {
//            try
//            {
//                using (conn)
//                {
//                    string sqlUpdateString =
//                    "UPDATE Compra SET Cedula_proveedor=@cedula_proveedor, Descripcion=@descripcion, Foto=@foto, Fecha_registro=@fecha_registro, Fecha_real=@fecha_real, Codigo_sucursal=@codigo_sucursal WHERE Codigo=@codigo";
//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();
//                    command.CommandText = sqlUpdateString;

//                    SqlParameter Codigoparam = new SqlParameter("@codigo", com.Codigo.ToString());
//                    SqlParameter Cedula_proveedorparam = new SqlParameter("@cedula_proveedor", com.Cedula_proveedor.ToString());
//                    SqlParameter Descripcionparam = new SqlParameter("@descripcion", com.Descripcion);
//                    SqlParameter Fotoparam = new SqlParameter("@foto", com.Foto);
//                    SqlParameter Fecha_registroparam = new SqlParameter("@fecha_registro", com.Fecha_Registro);
//                    SqlParameter Fecha_realparam = new SqlParameter("@fecha_real", com.Fecha_real);
//                    SqlParameter Codigo_sucursalparam = new SqlParameter("@codigo_sucursal", com.Codigo_sucursal);
//                    SqlParameter Precio_totalparam = new SqlParameter("@precio_total", com.Precio_total);

//                    command.Parameters.AddRange(new SqlParameter[] { Codigoparam, Cedula_proveedorparam, Descripcionparam, Fotoparam, Fecha_registroparam, Fecha_realparam, Codigo_sucursalparam, Precio_totalparam });
//                    command.ExecuteNonQuery();
//                    #region  Revision de que se hizo bien
//                    string buscarCompra = "SELECT * FROM Compra WHERE Codigo=@codigo";
//                    string compraEncontrada = "";
//                    command.CommandText = buscarCompra;
//                    SqlDataReader reader = command.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        compraEncontrada = reader[0].ToString();
//                    }
//                    command.Connection.Close();
//                    if (compraEncontrada != "")
//                    {
//                        return true;
//                    }
//                    else
//                    {
//                        return false;
//                    }
//                    #endregion
//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Database DELETE - Delete a Compra
//        /// </summary>
//        /// <param name="codigo"></param>
//        public bool DeleteCompra(int codigo)
//        {
//            try
//            {
//                using (conn)
//                {
//                    string sqlDeleteString =
//                     "UPDATE Compra SET Activo=0 WHERE Codigo=@codigo";

//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();
                    

//                    SqlParameter Codigoparam = new SqlParameter("@codigo", codigo);
//                    command.Parameters.Add(Codigoparam);


//                    command.CommandText = sqlDeleteString;
//                    command.ExecuteNonQuery();
//                    #region  Recuperacion de datos
//                    string sqlGetCodigoSucursal = "SELECT Codigo_sucursal FROM Compra WHERE Codigo=@codigo";
//                    command.CommandText = sqlGetCodigoSucursal;
//                    SqlDataReader reader = command.ExecuteReader();
//                    reader.Read();
//                    string codigo_sucursal = reader[0].ToString();
//                    reader.Close();
//                    string sqlGetListaProductos = "SELECT Codigo_producto, Cantidad FROM Productos_en_compra WHERE Codigo_compra=@codigo";
//                    command.CommandText = sqlGetListaProductos;
//                    List<string> listaCodigosBarra = new List<string>();
//                    List<int> listaCantidades = new List<int>();
//                    SqlDataReader reader2 = command.ExecuteReader();
//                    while (reader2.Read())
//                    {
//                        string codigoBarra = reader2[0].ToString();
//                        string cantidadString = reader2[1].ToString();
//                        int cantidad = int.Parse(cantidadString);
//                        listaCodigosBarra.Add(codigoBarra);
//                        listaCantidades.Add(cantidad);
//                    }
//                    reader2.Close();
//                    for (int i = 0; i < listaCantidades.Count; i++)
//                    {
//                        string sqlUpdateProducto = "UPDATE Producto SET Cantidad=Cantidad-" + listaCantidades[i].ToString() + " WHERE Codigo_barras=" + listaCodigosBarra[i] + " AND Codigo_sucursal='" + codigo_sucursal + "' ";
//                        command.CommandText = sqlUpdateProducto;
//                        command.ExecuteNonQuery();
//                    }
//                    #endregion
//                    command.Connection.Close();
//                    return true;
//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Database SELECT - Get a Compra
//        /// </summary>
//        /// <param name="codigo"></param>
//        /// <returns></returns>
//        public Compra GetCompra(int codigo)
//        {
//            try
//            {
//                if (comList == null)
//                {
//                    comList = GetCompras();
//                }
//                // enumerate through all employee list
//                // and select the concerned employee
//                foreach (Compra com in comList)
//                {
//                    if (com.Codigo == codigo)
//                    {
//                        return com;
//                    }
//                }
//                return null;
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Method - Get list of all Compras
//        /// </summary>
//        /// <returns>Compra</returns>
//        public List<Compra> GetCompras()
//        {
//            try
//            {
//                using (conn)
//                {
//                    comList = new List<Compra>();

//                    conn = new SqlConnection(connString);

//                    string sqlSelectString = "SELECT * FROM Compra WHERE Activo=1";
//                    command = new SqlCommand(sqlSelectString, conn);
//                    command.Connection.Open();

//                    SqlDataReader reader = command.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        Compra com = new Compra();
//                        string _Codigo_temp = reader[0].ToString();
//                        com.Codigo = int.Parse(_Codigo_temp);
//                        string _Cedula_proveedor_temp = reader[1].ToString();
//                        com.Cedula_proveedor = int.Parse(_Cedula_proveedor_temp);
//                        com.Descripcion = reader[2].ToString();
//                        com.Foto = reader[3].ToString();
//                        com.Fecha_Registro = reader[4].ToString();
//                        com.Fecha_real = reader[5].ToString();
//                        com.Codigo_sucursal = reader[6].ToString();
//                        string _Precio_total_temp = reader[8].ToString();
//                        if (_Precio_total_temp == "")
//                        {
//                            com.Precio_total = 0;
//                        }
//                        else
//                        {
//                            com.Precio_total = double.Parse(_Precio_total_temp);
//                        }
//                        comList.Add(com);
//                    }
//                    command.Connection.Close();
//                    return comList;
//                }

//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        public bool AddCompraProductos(Compra com, HttpContext context)
//        {
//            try
//            {
//                using (conn)
//                {
//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();


//                    SqlParameter Codigo_sucursalparam = new SqlParameter("@codigo_sucursal", com.Codigo_sucursal);
//                    SqlParameter Cedula_proveedorparam = new SqlParameter("@cedula_proveedor", com.Cedula_proveedor);
//                    SqlParameter Descripcionparam = new SqlParameter("@descripcion", com.Descripcion);
//                    SqlParameter Fotoparam = new SqlParameter("@foto", com.Foto);
//                    SqlParameter Fecha_registroparam = new SqlParameter("@fecha_registro", com.Fecha_Registro);
//                    SqlParameter Fecha_realparam = new SqlParameter("@fecha_real", com.Fecha_real);

//                    command.Parameters.AddRange(new SqlParameter[] { Codigo_sucursalparam, Cedula_proveedorparam, Descripcionparam, Fotoparam, Fecha_registroparam, Fecha_realparam });

//                    string sqlAddCompra = "INSERT INTO Compra (Cedula_proveedor, Descripcion, Foto, Fecha_registro, Fecha_real, Codigo_sucursal) VALUES (@cedula_proveedor, @descripcion, @foto, @fecha_registro, @fecha_real, @codigo_sucursal)";
//                    command.CommandText = sqlAddCompra;
//                    command.ExecuteNonQuery();
//                    string sqlgetVentaID = "SELECT MAX(Codigo) FROM Compra ";
//                    command.CommandText = sqlgetVentaID;
//                    int CompraID = (Int32)command.ExecuteScalar();

//                    string listaproductosconcoma = context.Request["Productos"];
//                    string listaCantidadesComa = context.Request["Cantidad"];
//                    string[] listaProductosSep = listaproductosconcoma.Split(',');
//                    string[] listaCantidadesComaSep = listaCantidadesComa.Split(',');
//                    for (int i = 0; i < listaProductosSep.Length; i++)
//                    {
//                        string sqlGetPrecio = "SELECT Precio_venta, Cantidad FROM Producto WHERE Codigo_barras=" + listaProductosSep[i] + " AND Codigo_sucursal=@codigo_sucursal ";
//                        command.CommandText = sqlGetPrecio;
//                        SqlDataReader reader = command.ExecuteReader();
//                        reader.Read();
//                        string precioString = reader[0].ToString();
//                        string cantidadDisponibleString = reader[1].ToString();
//                        reader.Close();
//                        double precioProducto = double.Parse(precioString);
//                        string nuevoProductoenCompra = "INSERT INTO Productos_en_compra (Codigo_producto, Codigo_compra, Cantidad, Precio_total) VALUES (" + listaProductosSep[i] + ", " + CompraID.ToString() + ", " + listaCantidadesComaSep[i] + ", " + (int.Parse(listaCantidadesComaSep[i]) * precioProducto).ToString() + " ) ";
//                        command.CommandText = nuevoProductoenCompra;
//                        command.ExecuteNonQuery();
//                        int cantidadDisponible = int.Parse(cantidadDisponibleString);
//                        string sqlReduceCantidadProducto = "UPDATE Producto SET Cantidad=" + (cantidadDisponible + int.Parse(listaCantidadesComaSep[i])).ToString() + " WHERE Codigo_barras=" + listaProductosSep[i] + " AND Codigo_sucursal=@codigo_sucursal ";
//                        command.CommandText = sqlReduceCantidadProducto;
//                        command.ExecuteNonQuery();
//                    }
//                    string sqlgetCompraTotal = "SELECT SUM(Precio_total) FROM Productos_en_compra WHERE Codigo_compra=" + CompraID.ToString() + " ";
//                    command.CommandText = sqlgetCompraTotal;
//                    SqlDataReader reader2 = command.ExecuteReader();
//                    reader2.Read();
//                    string obtenerSuma = reader2[0].ToString();
//                    reader2.Close();
//                    double CompraTotal = double.Parse(obtenerSuma);
//                    string sqlUpdateCompra = "UPDATE Compra SET Precio_total=" + CompraTotal.ToString() + " WHERE Codigo=" + CompraID.ToString() + " ";
//                    command.CommandText = sqlUpdateCompra;
//                    command.ExecuteNonQuery();
//                    command.Connection.Close();
//                    return true;
//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }

//        public List<Gasto> GetGastos(string codigo_sucursal, string fecha_inicial, string fecha_final)
//        {
//            try
//            {
//                using (conn)
//                {
//                    List<Gasto> gastoList = new List<Gasto>();
//                    string sqlSelectString = "";
//                    conn = new SqlConnection(connString);
//                    if (codigo_sucursal == "")
//                    {
//                        sqlSelectString = "SELECT Compra.Fecha_real, Proveedor.Nombre, Compra.Descripcion, Compra.Precio_total " +
//                                          "FROM Compra " +
//                                          "LEFT JOIN Proveedor ON Compra.Cedula_proveedor=Proveedor.Cedula WHERE Fecha_real>'" + fecha_inicial + "' AND Fecha_real<'" + fecha_final + "' AND Activo=1 ORDER BY Compra.Fecha_real asc; ";
//                    }
//                    else
//                    {
//                        sqlSelectString = "SELECT Compra.Fecha_real, Proveedor.Nombre, Compra.Descripcion, Compra.Precio_total "+
//                                          "FROM Compra "+
//                                          "LEFT JOIN Proveedor ON Compra.Cedula_proveedor=Proveedor.Cedula WHERE Compra.Codigo_sucursal='" + codigo_sucursal + "' AND Fecha_real>'" + fecha_inicial + "' AND Fecha_real<'" + fecha_final + "' AND Activo=1 ORDER BY Compra.Fecha_real asc; ";
//                    }
//                    command = new SqlCommand(sqlSelectString, conn);
//                    command.Connection.Open();
//                    SqlDataReader reader = command.ExecuteReader();
//                    while (reader.Read())                    
//                    {
//                        Gasto gasto = new Gasto();
//                        gasto.Fecha = reader[0].ToString();
//                        gasto.Proveedor = reader[1].ToString();
//                        gasto.Descripcion = reader[2].ToString();
//                        string Precio_total_temp = reader[3].ToString();
//                        if (Precio_total_temp == "")
//                        {
//                            gasto.Monto = 0;
//                        }
//                        else
//                        {
//                            gasto.Monto = double.Parse(Precio_total_temp);
//                        }
//                        gastoList.Add(gasto);
//                    }
//                    reader.Close();
//                    command.Connection.Close();
//                    return gastoList;
//                }

//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        #endregion
//        #region Operaciones sobre ventas
//        /// <summary>
//        /// Database INSERT - Add a Venta
//        /// </summary>
//        /// <param name="ven"></param>
//        public bool AddVenta(Venta ven)
//        {
//            try
//            {
//                using (conn)
//                {
//                    //using parametirized query
//                    string sqlInserString =
//                    "INSERT INTO Venta (Descuento, Precio_total, Codigo_sucursal, Cedula_cajero, Impuesto) VALUES (@descuento, @precio_total, @codigo_sucursal, @cedula_cajero, @impuesto)";

//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();
//                    command.CommandText = sqlInserString;

//                    SqlParameter Descuentoparam = new SqlParameter("@descuento", ven.Descuento);
//                    SqlParameter Precio_totalparam = new SqlParameter("@precio_total", ven.Precio_total);
//                    SqlParameter Codigo_sucursalparam = new SqlParameter("@codigo_sucursal", ven.Codigo_sucursal);
//                    SqlParameter Cedula_cajeroparam = new SqlParameter("@cedula_cajero", ven.Cedula_cajero);
//                    SqlParameter Impuestoparam = new SqlParameter("@impuesto", ven.Impuesto);

//                    command.Parameters.AddRange(new SqlParameter[] { Descuentoparam, Precio_totalparam, Codigo_sucursalparam, Cedula_cajeroparam, Impuestoparam });
//                    command.ExecuteNonQuery();
//                    command.Connection.Close();
//                    return true;

//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Database UPDATE - Update a Venta
//        /// </summary>
//        /// <param name="ven"></param>
//        public bool UpdateVenta(Venta ven)
//        {
//            try
//            {
//                using (conn)
//                {
//                    string sqlUpdateString =
//                    "UPDATE Venta SET Descuento=@descuento, Precio_total=@precio_total, Codigo_sucursal=@codigo_sucursal, Cedula_cajero=@cedula_cajero, Impuesto=@impuesto WHERE Codigo=@codigo ";
//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();
//                    command.CommandText = sqlUpdateString;

//                    SqlParameter Descuentoparam = new SqlParameter("@descuento", ven.Descuento);
//                    SqlParameter Precio_totalparam = new SqlParameter("@precio_total", ven.Precio_total);
//                    SqlParameter Codigo_sucursalparam = new SqlParameter("@codigo_sucursal", ven.Codigo_sucursal);
//                    SqlParameter Cedula_cajeroparam = new SqlParameter("@cedula_cajero", ven.Cedula_cajero);
//                    SqlParameter Impuestoparam = new SqlParameter("@impuesto", ven.Impuesto);

//                    command.Parameters.AddRange(new SqlParameter[] { Descuentoparam, Precio_totalparam, Codigo_sucursalparam, Cedula_cajeroparam, Impuestoparam });
//                    command.ExecuteNonQuery();
//                    #region  Revision de que se hizo bien
//                    string buscarVenta = "SELECT * FROM Venta WHERE Codigo=@codigo";
//                    string ventaEncontrada = "";
//                    command.CommandText = buscarVenta;
//                    SqlDataReader reader = command.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        ventaEncontrada = reader[0].ToString();
//                    }
//                    command.Connection.Close();
//                    if (ventaEncontrada != "")
//                    {
//                        return true;
//                    }
//                    else
//                    {
//                        return false;
//                    }
//                    #endregion
//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Database DELETE - Delete a Venta
//        /// </summary>
//        /// <param name="codigo"></param>
//        public bool DeleteVenta(int codigo)
//        {
//            try
//            {
//                using (conn)
//                {
//                    string sqlDeleteString =
//                     "UPDATE Venta SET Activo=0 WHERE Codigo=@codigo";

//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();
//                    command.CommandText = sqlDeleteString;

//                    SqlParameter codigoparam = new SqlParameter("@codigo", codigo);
//                    command.Parameters.Add(codigoparam);
//                    command.ExecuteNonQuery();
//                    #region  Recuperacion de datos
//                    string sqlGetCodigoSucursal = "SELECT Codigo_sucursal FROM Venta WHERE Codigo=@codigo";
//                    command.CommandText = sqlGetCodigoSucursal;
//                    SqlDataReader reader = command.ExecuteReader();
//                    reader.Read();
//                    string codigo_sucursal = reader[0].ToString();
//                    reader.Close();
//                    string sqlGetListaProductos = "SELECT Codigo_producto, Cantidad FROM Productos_en_venta WHERE Codigo_venta=@codigo";
//                    command.CommandText = sqlGetListaProductos;
//                    List<string> listaCodigosBarra = new List<string>();
//                    List<int> listaCantidades = new List<int>();
//                    SqlDataReader reader2 = command.ExecuteReader();
//                    while(reader2.Read())
//                    {
//                        string codigoBarra = reader2[0].ToString();
//                        string cantidadString = reader2[1].ToString();
//                        int cantidad = int.Parse(cantidadString);
//                        listaCodigosBarra.Add(codigoBarra);
//                        listaCantidades.Add(cantidad);
//                    }
//                    reader2.Close();
//                    for (int i = 0; i < listaCantidades.Count; i++)
//                    {
//                        string sqlUpdateProducto = "UPDATE Producto SET Cantidad=Cantidad+" + listaCantidades[i].ToString() + " WHERE Codigo_barras=" + listaCodigosBarra[i] + " AND Codigo_sucursal='" + codigo_sucursal + "' ";
//                        command.CommandText = sqlUpdateProducto;
//                        command.ExecuteNonQuery();
//                    }
//                    #endregion
//                    command.Connection.Close();
//                    return true;
//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Database SELECT - Get an employee
//        /// </summary>
//        /// <param name="codigo"></param>
//        /// <returns></returns>
//        public Venta GetVenta(int codigo)
//        {
//            try
//            {
//                if (venList == null)
//                {
//                    venList = GetVentas();
//                }
//                // enumerate through all employee list
//                // and select the concerned employee
//                foreach (Venta ven in venList)
//                {
//                    if (ven.Codigo == codigo)
//                    {
//                        return ven;
//                    }
//                }
//                return null;
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Method - Get list of all Ventas
//        /// </summary>
//        /// <returns>Venta</returns>
//        public List<Venta> GetVentas()
//        {
//            try
//            {
//                using (conn)
//                {
//                    venList = new List<Venta>();

//                    conn = new SqlConnection(connString);

//                    string sqlSelectString = "SELECT * FROM Venta WHERE Activo=1";
//                    command = new SqlCommand(sqlSelectString, conn);
//                    command.Connection.Open();

//                    SqlDataReader reader = command.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        Venta ven = new Venta();
//                        string _Codigo_venta_temp = reader[0].ToString();
//                        ven.Codigo = int.Parse(_Codigo_venta_temp);
//                        string _Descuento_temp = reader[1].ToString();
//                        if (_Descuento_temp == "")
//                        {
//                            ven.Descuento = 0;
//                        }
//                        else
//                        {
//                            ven.Descuento = int.Parse(_Descuento_temp);
//                        }
//                        string _Precio_total_temp = reader[2].ToString();
//                        if (_Precio_total_temp == "")
//                        {
//                            ven.Precio_total = 0;
//                        }
//                        else
//                        {
//                            ven.Precio_total = double.Parse(_Precio_total_temp);
//                        }
//                        ven.Codigo_sucursal = reader[3].ToString();
//                        string _Cedula_cajero_temp = reader[4].ToString();
//                        if (_Cedula_cajero_temp == "")
//                        {
//                            ven.Cedula_cajero = 0;
//                        }
//                        else
//                        {
//                            ven.Cedula_cajero = int.Parse(_Cedula_cajero_temp);
//                        }
//                        string _Impuesto_temp = reader[6].ToString();
//                        if (_Impuesto_temp == "")
//                        {
//                            ven.Impuesto = 0;
//                        }
//                        else
//                        {
//                            ven.Impuesto = double.Parse(_Impuesto_temp);
//                        }
//                        venList.Add(ven);
//                    }
//                    command.Connection.Close();
//                    return venList;
//                }

//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        public bool AddVentaProductos(HttpContext context)
//        {
//            try
//            {
//                using (conn)
//                {
//                    string sucursaltemp = context.Request["Codigo_sucursal"];
//                    string cajerotemp = context.Request["Cedula_cajero"];
//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();
                    

//                    SqlParameter Codigo_sucursalparam = new SqlParameter("@codigo_sucursal", sucursaltemp);
//                    SqlParameter Cedula_cajeroparam = new SqlParameter("@cedula_cajero", int.Parse(cajerotemp));

//                    command.Parameters.AddRange(new SqlParameter[] { Codigo_sucursalparam, Cedula_cajeroparam });

//                    string sqlAddVenta = "INSERT INTO Venta (Codigo_sucursal, Cedula_cajero) VALUES (@codigo_sucursal, @cedula_cajero)";
//                    command.CommandText = sqlAddVenta;
//                    command.ExecuteNonQuery();
//                    string sqlgetVentaID = "SELECT MAX(Codigo) FROM Venta ";
//                    command.CommandText = sqlgetVentaID;
//                    int VentaID = (Int32)command.ExecuteScalar();

//                    string listaproductosconcoma = context.Request["Productos"];
//                    string listaCantidadesComa = context.Request["Cantidad"];
//                    string[] listaProductosSep = listaproductosconcoma.Split(',');
//                    string[] listaCantidadesComaSep = listaCantidadesComa.Split(',');
//                    for (int i = 0; i < listaProductosSep.Length; i++ )
//                    {
//                        string sqlGetPrecio = "SELECT Precio_venta, Cantidad FROM Producto WHERE Codigo_barras=" + listaProductosSep[i] + " AND Codigo_sucursal=@codigo_sucursal ";
//                        command.CommandText = sqlGetPrecio;
//                        SqlDataReader reader = command.ExecuteReader();
//                        reader.Read();
//                        string precioString = reader[0].ToString();
//                        string cantidadDisponibleString = reader[1].ToString();
//                        reader.Close();
//                        double precioProducto = double.Parse(precioString);
//                        string nuevoProductoenVenta = "INSERT INTO Productos_en_venta (Codigo_producto, Precio_individual, Cantidad, Codigo_venta, Precio_total) VALUES (" + listaProductosSep[i] + ", " + precioProducto.ToString() + ", " + listaCantidadesComaSep[i] + ", " + VentaID.ToString() + ", " + (int.Parse(listaCantidadesComaSep[i])*precioProducto).ToString() + " ) ";
//                        command.CommandText = nuevoProductoenVenta;
//                        command.ExecuteNonQuery();
//                        int cantidadDisponible = int.Parse(cantidadDisponibleString);
//                        string sqlReduceCantidadProducto = "UPDATE Producto SET Cantidad=" + (cantidadDisponible - int.Parse(listaCantidadesComaSep[i])).ToString() + " WHERE Codigo_barras=" + listaProductosSep[i] + " AND Codigo_sucursal=@codigo_sucursal ";
//                        command.CommandText = sqlReduceCantidadProducto;
//                        command.ExecuteNonQuery();
//                    }
//                    string sqlgetVentaTotal = "SELECT SUM(Precio_total) FROM Productos_en_venta WHERE Codigo_venta=" + VentaID.ToString() + " ";
//                    command.CommandText = sqlgetVentaTotal;
//                    SqlDataReader reader2 = command.ExecuteReader();
//                    reader2.Read();
//                    string obtenerSuma = reader2[0].ToString();
//                    reader2.Close();
//                    double VentaTotalSinDesc = double.Parse(obtenerSuma);
//                    string sqlgetDescuento = "SELECT Descuento, Impuesto FROM Producto WHERE Codigo_sucursal=@codigo_sucursal";
//                    command.CommandText = sqlgetDescuento;
//                    SqlDataReader reader1 = command.ExecuteReader();
//                    reader1.Read();
//                    string descuentoString = reader1[0].ToString();
//                    string impuestoString = reader1[1].ToString();
//                    reader1.Close();
//                    int Porcentajedescuento = int.Parse(descuentoString);
//                    double PorcentajedescuentoDecimales = Porcentajedescuento / 100.0;
//                    int Porcentajeimpuesto = int.Parse(impuestoString);
//                    double descuentoMoneda = VentaTotalSinDesc * (Porcentajedescuento / 100.0);
//                    double VentaTotalconDesc = VentaTotalSinDesc - descuentoMoneda;
//                    double impuestoMoneda = VentaTotalconDesc * (Porcentajeimpuesto/100.0);
//                    double Ventatotal = VentaTotalconDesc + impuestoMoneda;
//                    string sqlUpdateVenta = "UPDATE Venta SET Descuento=" + descuentoMoneda.ToString() + ", Precio_total=" + Ventatotal.ToString() + ", Impuesto=" + impuestoMoneda.ToString() + " WHERE Codigo=" + VentaID.ToString() + " ";
//                    command.CommandText = sqlUpdateVenta;
//                    command.ExecuteNonQuery();
//                    command.Connection.Close();
//                    return true;
//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        #endregion
//        #region Operaciones sobre Horas
//        /// <summary>
//        /// Database INSERT - Add a Horas
//        /// </summary>
//        /// <param name="hor"></param>
//        public bool AddHoras(Horas hor)
//        {
//            try
//            {
//                using (conn)
//                {
//                    //using parametirized query
//                    string sqlInserString =
//                    "INSERT INTO Horas (ID_semana, Horas_ordinarias, Horas_extras, Ced_empleado) VALUES (@id_semana, @horas_ordinarias, @horas_extras, @ced_empleado)";

//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();
//                    command.CommandText = sqlInserString;

//                    SqlParameter ID_semanaparam = new SqlParameter("@id_semana", hor.ID_semana);
//                    SqlParameter Horas_ordinariasparam = new SqlParameter("@horas_ordinarias", hor.Horas_ordinarias.ToString());
//                    SqlParameter Horas_extraparam = new SqlParameter("@horas_extras", hor.Horas_extras.ToString());
//                    SqlParameter Ced_empleadoparam = new SqlParameter("@ced_empleado", hor.Ced_empleado.ToString());

//                    command.Parameters.AddRange(new SqlParameter[] { ID_semanaparam, Horas_ordinariasparam, Horas_extraparam, Ced_empleadoparam });
//                    command.ExecuteNonQuery();
//                    command.Connection.Close();
//                    return true;

//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Database UPDATE - Update a Horas
//        /// </summary>
//        /// <param name="hor"></param>
//        public bool UpdateHoras(Horas hor)
//        {
//            try
//            {
//                using (conn)
//                {
//                    string sqlUpdateString =
//                    "UPDATE Horas SET Horas_ordinarias=@horas_ordinarias, Horas_extras=@horas_extras WHERE ID_semana=@id_semana AND Ced_empleado=@ced_empleado";
//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();
//                    command.CommandText = sqlUpdateString;

//                    SqlParameter ID_semanaparam = new SqlParameter("@id_semana", hor.ID_semana);
//                    SqlParameter Horas_ordinariasparam = new SqlParameter("@horas_ordinarias", hor.Horas_ordinarias.ToString());
//                    SqlParameter Horas_extraparam = new SqlParameter("@horas_extras", hor.Horas_extras.ToString());
//                    SqlParameter Ced_empleadoparam = new SqlParameter("@ced_empleado", hor.Ced_empleado.ToString());

//                    command.Parameters.AddRange(new SqlParameter[] { ID_semanaparam, Horas_ordinariasparam, Horas_extraparam, Ced_empleadoparam });
//                    command.ExecuteNonQuery();

//                    #region  Revision de que se hizo bien
//                    string buscarHoras = "SELECT * FROM Horas WHERE ID_semana=@id_semana AND Ced_empleado=@ced_empleado";
//                    string horasEncontradas = "";
//                    command.CommandText = buscarHoras;
//                    SqlDataReader reader = command.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        horasEncontradas = reader[0].ToString();
//                    }
//                    command.Connection.Close();
//                    if (horasEncontradas != "")
//                    {
//                        return true;
//                    }
//                    else
//                    {
//                        return false;
//                    }
//                    #endregion
//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Database DELETE - Delete a Horas
//        /// </summary>
//        /// <param name="semana"></param>
//        public bool DeleteHoras(string semana, int ced_empleado)
//        {
//            try
//            {
//                using (conn)
//                {
//                    string sqlDeleteString =
//                    "DELETE FROM Horas WHERE ID_semana=@id_semana AND Ced_empleado=@ced_empleado";

//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();
//                    command.CommandText = sqlDeleteString;

//                    SqlParameter ID_semanaparam = new SqlParameter("@id_semana", semana);
//                    SqlParameter Ced_empleadoparam = new SqlParameter("@ced_empleado", ced_empleado);
//                    command.Parameters.AddRange(new SqlParameter[] { ID_semanaparam, Ced_empleadoparam });
//                    command.ExecuteNonQuery();
//                    command.Connection.Close();
//                    return true;
//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Database SELECT - Get a Horas
//        /// </summary>
//        /// <param name="semana"></param>
//        /// <returns></returns>
//        public List<Horas> GetHoras(string semana)
//        {
//            try
//            {
//                List<Horas> listahorasSemana = new List<Horas>();
//                if (horList == null)
//                {
//                    horList = GetHorases();
//                }
//                // enumerate through all employee list
//                // and select the concerned employee
//                foreach (Horas hor in horList)
//                {
//                    if (hor.ID_semana == semana)
//                    {
//                        listahorasSemana.Add(hor);
//                    }
//                }
//                return listahorasSemana;
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Method - Get list of all Horas
//        /// </summary>
//        /// <returns>Horas</returns>
//        public List<Horas> GetHorases()
//        {
//            try
//            {
//                using (conn)
//                {
//                    horList = new List<Horas>();

//                    conn = new SqlConnection(connString);

//                    string sqlSelectString = "SELECT * FROM Horas";
//                    command = new SqlCommand(sqlSelectString, conn);
//                    command.Connection.Open();

//                    SqlDataReader reader = command.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        Horas hor = new Horas();
//                        hor.ID_semana = reader[0].ToString();
//                        string _horas_ordinarias_temp = reader[1].ToString();
//                        hor.Horas_ordinarias = int.Parse(_horas_ordinarias_temp);
//                        string _horas_extras_temp = reader[2].ToString();
//                        if (_horas_extras_temp == "")
//                        {
//                            hor.Horas_extras = 0;
//                        }
//                        else
//                        {
//                            hor.Horas_extras = int.Parse(_horas_extras_temp);
//                        }
//                        string _ced_empleado_temp = reader[3].ToString();
//                        hor.Ced_empleado = int.Parse(_ced_empleado_temp);
//                        horList.Add(hor);
//                    }
//                    command.Connection.Close();
//                    return horList;
//                }

//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        #endregion
//        #region Operaciones sobre productos
//        /// <summary>
//        /// Database INSERT - Add a Producto
//        /// </summary>
//        /// <param name="produ"></param>
//        public bool AddProducto(Producto produ)
//        {
//            try
//            {
//                using (conn)
//                {
//                    //using parametirized query
//                    string sqlInserString =
//                    "INSERT INTO Producto (Exento, Codigo_barras, Nombre, Descripcion, Impuesto, Precio_compra, Descuento, Codigo_sucursal, Cantidad, Precio_venta, Cedula_proveedor) VALUES (@exento, @codigo_barras, @nombre, @descripcion, @impuesto, @precio_compra, @descuento, @codigo_sucursal, @cantidad, @precio_venta, @cedula_proveedor)";

//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();
//                    command.CommandText = sqlInserString;

//                    SqlParameter Exentoparam = new SqlParameter("@exento", produ.Exento.ToString());
//                    SqlParameter Codigo_barrasparam = new SqlParameter("@codigo_barras", produ.Codigo_barras.ToString());
//                    SqlParameter Nombreparam = new SqlParameter("@nombre", produ.Nombre);
//                    SqlParameter Descripcionparam = new SqlParameter("@descripcion", produ.Descripcion);
//                    SqlParameter Impuestoparam = new SqlParameter("@impuesto", produ.Impuesto.ToString());
//                    SqlParameter Precio_compraparam = new SqlParameter("@precio_compra", produ.Precio_compra.ToString());
//                    SqlParameter Descuentoparam = new SqlParameter("@descuento", produ.Descuento.ToString());
//                    SqlParameter Codigo_sucursalparam = new SqlParameter("@codigo_sucursal", produ.Codigo_sucursal);
//                    SqlParameter Cantidadparam = new SqlParameter("@cantidad", produ.Cantidad.ToString());
//                    SqlParameter Precio_ventaparam = new SqlParameter("@precio_venta", produ.Precio_venta.ToString());
//                    SqlParameter Cedula_proveedorparam = new SqlParameter("@cedula_proveedor", produ.Cedula_proveedor.ToString());

//                    command.Parameters.AddRange(new SqlParameter[] { Exentoparam, Codigo_barrasparam, Nombreparam, Descripcionparam, Impuestoparam, Precio_compraparam, Descuentoparam, Codigo_sucursalparam, Cantidadparam, Precio_ventaparam, Cedula_proveedorparam });
//                    command.ExecuteNonQuery();
//                    command.Connection.Close();
//                    return true;

//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Database UPDATE - Update a Producto
//        /// </summary>
//        /// <param name="produ"></param>
//        public bool UpdateProducto(Producto produ)
//        {
//            try
//            {
//                using (conn)
//                {
//                    string sqlUpdateString =
//                    "UPDATE Producto SET Exento=@exento, Nombre=@nombre, Descripcion=@descripcion, Impuesto=@impuesto, Precio_compra=@precio_compra, Descuento=@descuento, Cantidad=@cantidad, Precio_venta=@precio_venta, Cedula_proveedor=@cedula_proveedor WHERE Codigo_barras=@codigo_barras AND Codigo_sucursal=@codigo_sucursal";
//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();
//                    command.CommandText = sqlUpdateString;

//                    SqlParameter Exentoparam = new SqlParameter("@exento", produ.Exento.ToString());
//                    SqlParameter Codigo_barrasparam = new SqlParameter("@codigo_barras", produ.Codigo_barras.ToString());
//                    SqlParameter Nombreparam = new SqlParameter("@nombre", produ.Nombre);
//                    SqlParameter Descripcionparam = new SqlParameter("@descripcion", produ.Descripcion);
//                    SqlParameter Impuestoparam = new SqlParameter("@impuesto", produ.Impuesto.ToString());
//                    SqlParameter Precio_compraparam = new SqlParameter("@precio_compra", produ.Precio_compra.ToString());
//                    SqlParameter Descuentoparam = new SqlParameter("@descuento", produ.Descuento.ToString());
//                    SqlParameter Codigo_sucursalparam = new SqlParameter("@codigo_sucursal", produ.Codigo_sucursal);
//                    SqlParameter Cantidadparam = new SqlParameter("@cantidad", produ.Cantidad.ToString());
//                    SqlParameter Precio_ventaparam = new SqlParameter("@precio_venta", produ.Precio_venta.ToString());
//                    SqlParameter Cedula_proveedorparam = new SqlParameter("@cedula_proveedor", produ.Cedula_proveedor.ToString());

//                    command.Parameters.AddRange(new SqlParameter[] { Exentoparam, Codigo_barrasparam, Nombreparam, Descripcionparam, Impuestoparam, Precio_compraparam, Descuentoparam, Codigo_sucursalparam, Cantidadparam, Precio_ventaparam, Cedula_proveedorparam });
//                    command.ExecuteNonQuery();

                    

//                    #region  Revision de que se hizo bien
//                    string buscarProducto = "SELECT * FROM Producto WHERE Codigo_barras=@codigo_barras AND Codigo_sucursal=@codigo_sucursal";
//                    string productoEncontrado = "";
//                    command.CommandText = buscarProducto;
//                    SqlDataReader reader = command.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        productoEncontrado = reader[0].ToString();
//                    }
//                    command.Connection.Close();
//                    if (productoEncontrado != "")
//                    {
//                        return true;
//                    }
//                    else
//                    {
//                        return false;
//                    }
//                    #endregion
//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Database DELETE - Delete a Producto
//        /// </summary>
//        /// <param name="codigo_barras"></param>
//        public bool DeleteProducto(int codigo_barras, string codigo_sucursal)
//        {
//            try
//            {
//                using (conn)
//                {
//                    string sqlDeleteString =
//                    "UPDATE Producto SET Cantidad=0  WHERE Codigo_barras=@codigo_barras AND Codigo_sucursal=@codigo_sucursal";

//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();
//                    command.CommandText = sqlDeleteString;

//                    SqlParameter Codigo_barrasparam = new SqlParameter("@codigo_barras", codigo_barras);
//                    SqlParameter Codigo_sucursalparam = new SqlParameter("@codigo_sucursal", codigo_sucursal);
//                    command.Parameters.AddRange(new SqlParameter[] { Codigo_barrasparam, Codigo_sucursalparam });
//                    command.ExecuteNonQuery();
//                    #region  Revision de que se hizo bien
//                    string buscarProducto = "SELECT * FROM Producto WHERE Codigo_barras=@codigo_barras AND Codigo_sucursal=@codigo_sucursal";
//                    string productoEncontrado = "";
//                    command.CommandText = buscarProducto;
//                    SqlDataReader reader = command.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        productoEncontrado = reader[0].ToString();
//                    }
//                    command.Connection.Close();
//                    if (productoEncontrado != "")
//                    {
//                        return true;
//                    }
//                    else
//                    {
//                        return false;
//                    }
//                    #endregion
//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Database SELECT - Get a Producto
//        /// </summary>
//        /// <param name="codigo_barras"></param>
//        /// <returns></returns>
//        public Producto GetProducto(int codigo_barras, string codigo_sucursal)
//        {
//            try
//            {
//                if (produList == null)
//                {
//                    produList = GetProductos();
//                }
//                // enumerate through all employee list
//                // and select the concerned employee
//                foreach (Producto produ in produList)
//                {
//                    if (produ.Codigo_barras == codigo_barras && produ.Codigo_sucursal == codigo_sucursal)
//                    {
//                        return produ;
//                    }
//                }
//                return null;
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Method - Get list of all employees
//        /// </summary>
//        /// <returns>Producto</returns>
//        public List<Producto> GetProductos()
//        {
//            try
//            {
//                using (conn)
//                {
//                    produList = new List<Producto>();

//                    conn = new SqlConnection(connString);

//                    string sqlSelectString = "SELECT * FROM Producto";
//                    command = new SqlCommand(sqlSelectString, conn);
//                    command.Connection.Open();

//                    SqlDataReader reader = command.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        Producto produ = new Producto();
//                        string _Exento_temp = reader[0].ToString();
//                        produ.Exento = bool.Parse(_Exento_temp);
//                        string _Codigo_barras_temp = reader[1].ToString();
//                        produ.Codigo_barras = int.Parse(_Codigo_barras_temp);
//                        produ.Nombre = reader[2].ToString();
//                        produ.Descripcion = reader[3].ToString();
//                        string _Impuesto_temp = reader[4].ToString();
//                        produ.Impuesto = int.Parse(_Impuesto_temp);
//                        string _Precio_compra_temp = reader[5].ToString();
//                        produ.Precio_compra = double.Parse(_Precio_compra_temp);
//                        string _Descuento_temp = reader[6].ToString();
//                        produ.Descuento = int.Parse(_Descuento_temp);
//                        produ.Codigo_sucursal = reader[7].ToString();
//                        string _Cantidad_temp = reader[8].ToString();
//                        if (_Cantidad_temp == "")
//                        {
//                            produ.Cantidad = 0;
//                        }
//                        else
//                        {
//                            produ.Cantidad = int.Parse(_Cantidad_temp);
//                        }
//                        string _Precio_venta_temp = reader[9].ToString();
//                        produ.Precio_venta = double.Parse(_Precio_venta_temp);
//                        string _Proveedor_temp = reader[10].ToString();
//                        if (_Proveedor_temp == "")
//                        {
//                            produ.Cedula_proveedor = 0;
//                        }
//                        else
//                        {
//                            produ.Cedula_proveedor = int.Parse(_Proveedor_temp);
//                        }
//                        produList.Add(produ);
//                    }
//                    command.Connection.Close();
//                    return produList;
//                }

//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        public List<ReporteProductos> GetProductosTodos()
//        {
//            try
//            {
//                using (conn)
//                {
//                    List<ReporteProductos> repoproduList = new List<ReporteProductos>();

//                    conn = new SqlConnection(connString);

//                    string sqlSelectString = "SELECT DISTINCT(Codigo_barras) FROM Producto";
//                    command = new SqlCommand(sqlSelectString, conn);
//                    command.Connection.Open();
//                    List<string> listaCodigos = new List<string>();
//                    SqlDataReader reader = command.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        string codigo = reader[0].ToString();
//                        listaCodigos.Add(codigo);
//                    }
//                    reader.Close();
//                    foreach (string codigoBarra in listaCodigos)
//                    {
//                        ReporteProductos produ = new ReporteProductos();
//                        string sqlGetSum = "SELECT SUM(Precio_compra), SUM(Cantidad) FROM Producto WHERE Codigo_barras=" + codigoBarra + " ";
//                        command.CommandText = sqlGetSum;
//                        SqlDataReader reader1 = command.ExecuteReader();
//                        reader1.Read();
//                        string costotemp = reader1[0].ToString();
//                        produ.Costo = double.Parse(costotemp);
//                        string cantidadtemp = reader1[1].ToString();
//                        produ.Cantidad = int.Parse(cantidadtemp);
//                        reader1.Close();
//                        string sqlGetNombre = "SELECT Nombre FROM Producto WHERE Codigo_barras=" + codigoBarra + " ";
//                        command.CommandText = sqlGetNombre;
//                        SqlDataReader reader2 = command.ExecuteReader();
//                        reader2.Read();
//                        produ.Nombre = reader2[0].ToString();
//                        reader2.Close();
//                        repoproduList.Add(produ);
//                    }
//                    command.Connection.Close();
//                    return repoproduList;
//                }

//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        public List<ReporteProductosSucursal> GetProductosporSucursal(string codigo_sucursal)
//        {
//            try
//            {
//                using (conn)
//                {
//                    List<ReporteProductosSucursal> repoproduList = new List<ReporteProductosSucursal>();

//                    string sqlGetProductos = "SELECT * FROM Producto WHERE Codigo_sucursal=@codigo_sucursal";
//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand(sqlGetProductos, conn);
                    
//                    SqlParameter Codigo_sucursalparam = new SqlParameter("@codigo_sucursal", codigo_sucursal);
//                    command.Parameters.AddRange(new SqlParameter[] { Codigo_sucursalparam });
//                    command.Connection.Open();
//                    SqlDataReader reader = command.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        ReporteProductosSucursal produ = new ReporteProductosSucursal();
//                        produ.Nombre = reader[2].ToString();
//                        produ.Descripcion = reader[3].ToString();
//                        string _Precio_compra_temp = reader[5].ToString();
//                        produ.Costo = double.Parse(_Precio_compra_temp);
//                        string _Cantidad_temp = reader[8].ToString();
//                        produ.Cantidad = int.Parse(_Cantidad_temp);
//                        if (produ.Cantidad != 0)
//                        {
//                            repoproduList.Add(produ);
//                        }
//                    }
//                    command.Connection.Close();
//                    return repoproduList;
//                }

//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        #endregion
//        #region Operaciones sobre productos en compra
//        /// <summary>
//        /// Database INSERT - Add a Producto en compra
//        /// </summary>
//        /// <param name="producomp"></param>
//        public bool AddProductocompra(Productos_en_compra producomp)
//        {
//            try
//            {
//                using (conn)
//                {
//                    //using parametirized query
//                    string sqlInserString =
//                    "INSERT INTO Productos_en_compra (Codigo_producto, Codigo_compra, Cantidad) VALUES (@codigo_producto, @codigo_compra, @cantidad)";

//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();
//                    command.CommandText = sqlInserString;

//                    SqlParameter Codigo_productoparam = new SqlParameter("@codigo_producto", producomp.Codigo_producto);
//                    SqlParameter Codigo_compraparam = new SqlParameter("@codigo_compra", producomp.Codigo_compra);
//                    SqlParameter Cantidadparam = new SqlParameter("@cantidad", producomp.Cantidad);
                    
//                    command.Parameters.AddRange(new SqlParameter[] { Codigo_productoparam, Codigo_compraparam, Cantidadparam });
//                    command.ExecuteNonQuery();
//                    command.Connection.Close();
//                    return true;

//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Database UPDATE - Update a Producto en compra
//        /// </summary>
//        /// <param name="producomp"></param>
//        public bool UpdateProductocompra(Productos_en_compra producomp)
//        {
//            try
//            {
//                using (conn)
//                {
                    
//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();
                    

//                    SqlParameter Codigo_productoparam = new SqlParameter("@codigo_producto", producomp.Codigo_producto);
//                    SqlParameter Codigo_compraparam = new SqlParameter("@codigo_compra", producomp.Codigo_compra);
//                    SqlParameter Cantidadparam = new SqlParameter("@cantidad", producomp.Cantidad);

//                    command.Parameters.AddRange(new SqlParameter[] { Codigo_productoparam, Codigo_compraparam, Cantidadparam });


//                    #region Update Cantidad producto
//                    string sqlGetCantidadOriginal = "SELECT Cantidad FROM Productos_en_compra WHERE Codigo_compra=@codigo_compra AND Codigo_producto=@codigo_producto ";
//                    command.CommandText = sqlGetCantidadOriginal;
//                    SqlDataReader readertemp3 = command.ExecuteReader();
//                    readertemp3.Read();
//                    string cantidadOriginalString = readertemp3[0].ToString();
//                    readertemp3.Close();
//                    int cantidadOriginal = int.Parse(cantidadOriginalString);
//                    string sqlGetCodigoSucursal = "SELECT Codigo_sucursal FROM Compra WHERE Codigo=@codigo_compra";
//                    command.CommandText = sqlGetCodigoSucursal;
//                    SqlDataReader readertemp = command.ExecuteReader();
//                    readertemp.Read();
//                    string codigo_sucursal = readertemp[0].ToString();
//                    readertemp.Close();
//                    string sqlGetPrecio = "SELECT Precio_venta, Cantidad FROM Producto WHERE Codigo_barras=@codigo_producto AND Codigo_sucursal='" + codigo_sucursal + "' ";
//                    command.CommandText = sqlGetPrecio;
//                    SqlDataReader readertemp2 = command.ExecuteReader();
//                    readertemp2.Read();
//                    string precioString = readertemp2[0].ToString();
//                    string cantidadDisponibleString = readertemp2[1].ToString();
//                    readertemp2.Close();
//                    double precioProducto = double.Parse(precioString);
//                    int cantidadDisponible = int.Parse(cantidadDisponibleString);
//                    string sqlReduceCantidadProducto = "UPDATE Producto SET Cantidad=" + (cantidadDisponible - cantidadOriginal + producomp.Cantidad).ToString() + " WHERE Codigo_barras=@codigo_producto AND Codigo_sucursal='" + codigo_sucursal + "' ";
//                    command.CommandText = sqlReduceCantidadProducto;
//                    command.ExecuteNonQuery();
//                    #endregion
//                    string sqlUpdateString =
//                    "UPDATE Productos_en_compra SET Cantidad=@cantidad, Precio_total=" + (producomp.Cantidad * precioProducto).ToString() + " WHERE Codigo_compra = @codigo_compra AND Codigo_producto = @codigo_producto";
//                    command.CommandText = sqlUpdateString;
//                    command.ExecuteNonQuery();

//                    string sqlgetCompraTotal = "SELECT SUM(Precio_total) FROM Productos_en_compra WHERE Codigo_compra=@codigo_compra ";
//                    command.CommandText = sqlgetCompraTotal;
//                    SqlDataReader reader2 = command.ExecuteReader();
//                    reader2.Read();
//                    string obtenerSuma = reader2[0].ToString();
//                    reader2.Close();
//                    double CompraTotal = double.Parse(obtenerSuma);
//                    string sqlUpdateCompra = "UPDATE Compra SET Precio_total=" + CompraTotal.ToString() + " WHERE Codigo=@codigo_compra ";
//                    command.CommandText = sqlUpdateCompra;
//                    command.ExecuteNonQuery();

                    
//                    #region  Revision de que se hizo bien
//                    string buscarProductoCompra = "SELECT * FROM Productos_en_compra WHERE Codigo_compra=@codigo_compra AND Codigo_producto=@codigo_producto";
//                    string productoCompraEncontrado = "";
//                    command.CommandText = buscarProductoCompra;
//                    SqlDataReader reader = command.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        productoCompraEncontrado = reader[0].ToString();
//                    }
//                    command.Connection.Close();
//                    if (productoCompraEncontrado != "")
//                    {
//                        return true;
//                    }
//                    else
//                    {
//                        return false;
//                    }
//                    #endregion


//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Database DELETE - Delete a Producto en compra
//        /// </summary>
//        /// <param name="codigo_compra"></param>
//        public bool DeleteProductocompra(int codigo_compra,int codigo_producto)
//        {
//            try
//            {
//                using (conn)
//                {
//                    string sqlDeleteString =
//                    "DELETE FROM Productos_en_compra WHERE Codigo_compra=@codigo_compra AND Codigo_producto=@codigo_producto";

//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();
//                    command.CommandText = sqlDeleteString;

//                    SqlParameter codigo_compraparam = new SqlParameter("@codigo_compra", codigo_compra);
//                    SqlParameter codigo_productoparam = new SqlParameter("@codigo_producto", codigo_producto);
//                    command.Parameters.AddRange(new SqlParameter[] { codigo_compraparam, codigo_productoparam });
//                    command.ExecuteNonQuery();
//                    command.Connection.Close();
//                    return true;
//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Database SELECT - Get a Producto en compra
//        /// </summary>
//        /// <param name="codigo_compra"></param>
//        /// <returns></returns>
//        public Productos_en_compra GetProducto_en_compra(int codigo_compra, int codigo_producto)
//        {
//            try
//            {
//                if (producompList == null)
//                {
//                    producompList = GetProductos_en_compras();
//                }
//                // enumerate through all employee list
//                // and select the concerned employee
//                foreach (Productos_en_compra producomp in producompList)
//                {
//                    if (producomp.Codigo_compra == codigo_compra && producomp.Codigo_producto == codigo_producto)
//                    {
//                        return producomp;
//                    }
//                }
//                return null;
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Method - Get list of all employees
//        /// </summary>
//        /// <returns>Employee</returns>
//        public List<Productos_en_compra> GetProductos_en_compras()
//        {
//            try
//            {
//                using (conn)
//                {
//                    producompList = new List<Productos_en_compra>();

//                    conn = new SqlConnection(connString);

//                    string sqlSelectString = "SELECT * FROM Productos_en_compra";
//                    command = new SqlCommand(sqlSelectString, conn);
//                    command.Connection.Open();

//                    SqlDataReader reader = command.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        Productos_en_compra producomp = new Productos_en_compra();
//                        string _Codigo_producto_temp = reader[0].ToString();
//                        if (_Codigo_producto_temp == "")
//                        {
//                            producomp.Codigo_producto = 0;
//                        }
//                        else
//                        {
//                            producomp.Codigo_producto = int.Parse(_Codigo_producto_temp);
//                        }
//                        string _Codigo_compra_temp = reader[1].ToString();
//                        producomp.Codigo_compra = int.Parse(_Codigo_compra_temp);
//                        string _Cantidad_temp = reader[2].ToString();
//                        producomp.Cantidad = int.Parse(_Cantidad_temp);
//                        string _Precio_total_temp = reader[3].ToString();
//                        if (_Precio_total_temp == "")
//                        {
//                            producomp.Precio_total = 0;
//                        }
//                        else
//                        {
//                            producomp.Precio_total = double.Parse(_Precio_total_temp);
//                        }
//                        producompList.Add(producomp);

//                    }
//                    command.Connection.Close();
//                    return producompList;
//                }

//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        #endregion
//        #region Operaciones sobre productos en venta
//        /// <summary>
//        /// Database INSERT - Add a Producto en venta
//        /// </summary>
//        /// <param name="produven"></param>
//        public bool AddProductoventa(Productos_en_venta produven)
//        {
//            try
//            {
//                using (conn)
//                {
//                    //using parametirized query
//                    string sqlInserString =
//                    "INSERT INTO Productos_en_venta (Codigo_producto, Precio_individual, Cantidad, Codigo_venta) VALUES (@codigo_producto, @precio_individual, @cantidad, @codigo_venta)";

//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();
//                    command.CommandText = sqlInserString;

//                    SqlParameter Codigo_productoparam = new SqlParameter("@codigo_producto", produven.Codigo_producto.ToString());
//                    SqlParameter Codigo_ventaparam = new SqlParameter("@codigo_venta", produven.Codigo_venta.ToString());
//                    SqlParameter Cantidadparam = new SqlParameter("@cantidad", produven.Cantidad.ToString());
//                    SqlParameter Precio_individualparam = new SqlParameter("@precio_individual", produven.Precio_individual.ToString());


//                    command.Parameters.AddRange(new SqlParameter[] { Codigo_productoparam, Precio_individualparam, Cantidadparam, Codigo_ventaparam});
//                    command.ExecuteNonQuery();
//                    command.Connection.Close();
//                    return true;

//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Database UPDATE - Update a Producto en venta
//        /// </summary>
//        /// <param name="produven"></param>
//        public bool UpdateProductoventa(Productos_en_venta produven)
//        {
//            try
//            {
//                using (conn)
//                {
                    
//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();
                    

//                    SqlParameter Codigo_productoparam = new SqlParameter("@codigo_producto", produven.Codigo_producto);
//                    SqlParameter Codigo_ventaparam = new SqlParameter("@codigo_venta", produven.Codigo_venta);
//                    SqlParameter Cantidadparam = new SqlParameter("@cantidad", produven.Cantidad);


//                    command.Parameters.AddRange(new SqlParameter[] { Codigo_productoparam, Cantidadparam, Codigo_ventaparam });

//                    string sqlGetCodigoSucursal = "SELECT Codigo_sucursal FROM Venta WHERE Codigo=@codigo_venta";
//                    command.CommandText = sqlGetCodigoSucursal;
//                    SqlDataReader readertemp = command.ExecuteReader();
//                    readertemp.Read();
//                    string codigo_sucursal = readertemp[0].ToString();
//                    readertemp.Close();
//                    string sqlGetCantidadOriginal = "SELECT Cantidad FROM Productos_en_venta WHERE Codigo_venta=@codigo_venta AND Codigo_producto=@codigo_producto";
//                    command.CommandText = sqlGetCantidadOriginal;
//                    SqlDataReader readertemp2 = command.ExecuteReader();
//                    readertemp2.Read();
//                    string cantidadOriginalString = readertemp2[0].ToString();
//                    readertemp2.Close();
//                    int cantidadOriginal = int.Parse(cantidadOriginalString);
//                    string sqlGetPrecio = "SELECT Precio_venta, Cantidad FROM Producto WHERE Codigo_barras=@codigo_producto AND Codigo_sucursal='" + codigo_sucursal + "' ";
//                    command.CommandText = sqlGetPrecio;
//                    SqlDataReader readertemp1 = command.ExecuteReader();
//                    readertemp1.Read();
//                    string precioString = readertemp1[0].ToString();
//                    string cantidadDisponibleString = readertemp1[1].ToString();
//                    readertemp1.Close();
//                    double precioProducto = double.Parse(precioString);
//                    int cantidadDisponible = int.Parse(cantidadDisponibleString);
//                    string sqlReduceCantidadProducto = "UPDATE Producto SET Cantidad=" + (cantidadDisponible + cantidadOriginal - produven.Cantidad).ToString() + " WHERE Codigo_barras=@codigo_producto AND Codigo_sucursal='" + codigo_sucursal + "' ";
//                    command.CommandText = sqlReduceCantidadProducto;
//                    command.ExecuteNonQuery();
//                    string sqlUpdateString =
//                    "UPDATE Productos_en_venta SET Cantidad=@cantidad, Precio_individual=" + precioProducto.ToString() + ", Precio_total=" + (precioProducto * produven.Cantidad).ToString() + " WHERE Codigo_venta=@codigo_venta AND Codigo_producto=@codigo_producto";
//                    command.CommandText = sqlUpdateString;
//                    command.ExecuteNonQuery();
//                    string sqlgetVentaTotal = "SELECT SUM(Precio_total) FROM Productos_en_venta WHERE Codigo_venta=@codigo_venta ";
//                    command.CommandText = sqlgetVentaTotal;
//                    SqlDataReader reader2 = command.ExecuteReader();
//                    reader2.Read();
//                    string obtenerSuma = reader2[0].ToString();
//                    reader2.Close();
//                    double VentaTotalSinDesc = double.Parse(obtenerSuma);
//                    string sqlgetDescuento = "SELECT Descuento, Impuesto FROM Producto WHERE Codigo_sucursal='" + codigo_sucursal + "' ";
//                    command.CommandText = sqlgetDescuento;
//                    SqlDataReader reader1 = command.ExecuteReader();
//                    reader1.Read();
//                    string descuentoString = reader1[0].ToString();
//                    string impuestoString = reader1[1].ToString();
//                    reader1.Close();
//                    int Porcentajedescuento = int.Parse(descuentoString);
//                    double PorcentajedescuentoDecimales = Porcentajedescuento / 100.0;
//                    int Porcentajeimpuesto = int.Parse(impuestoString);
//                    double descuentoMoneda = VentaTotalSinDesc * (Porcentajedescuento / 100.0);
//                    double VentaTotalconDesc = VentaTotalSinDesc - descuentoMoneda;
//                    double impuestoMoneda = VentaTotalconDesc * (Porcentajeimpuesto / 100.0);
//                    double Ventatotal = VentaTotalconDesc + impuestoMoneda;
//                    string sqlUpdateVenta = "UPDATE Venta SET Descuento=" + descuentoMoneda.ToString() + ", Precio_total=" + Ventatotal.ToString() + ", Impuesto=" + impuestoMoneda.ToString() + " WHERE Codigo=@codigo_venta ";
//                    command.CommandText = sqlUpdateVenta;
//                    command.ExecuteNonQuery();

//                    command.ExecuteNonQuery();


//                    #region  Revision de que se hizo bien
//                    string buscarProductoVenta = "SELECT * FROM Productos_en_venta WHERE Codigo_venta=@codigo_venta AND Codigo_producto=@codigo_producto";
//                    string productoVentaEncontrado = "";
//                    command.CommandText = buscarProductoVenta;
//                    SqlDataReader reader = command.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        productoVentaEncontrado = reader[0].ToString();
//                    }
//                    command.Connection.Close();
//                    if (productoVentaEncontrado != "")
//                    {
//                        return true;
//                    }
//                    else
//                    {
//                        return false;
//                    }
//                    #endregion
//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Database DELETE - Delete a Producto en venta
//        /// </summary>
//        /// <param name="codigo_venta"></param>
//        public void DeleteProductoventa(int codigo_venta, int codigo_producto)
//        {
//            try
//            {
//                using (conn)
//                {
//                    string sqlDeleteString =
//                    "DELETE FROM Productos_en_venta WHERE Codigo_venta=@codigo_venta AND Codigo_producto = codigo_producto";

//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();
//                    command.CommandText = sqlDeleteString;

//                    SqlParameter codigo_ventaparam = new SqlParameter("@codigo_venta", codigo_venta);
//                    SqlParameter codigo_productoparam = new SqlParameter("@codigo_producto", codigo_producto);
//                    command.Parameters.Add(codigo_ventaparam);
//                    command.ExecuteNonQuery();
//                    command.Connection.Close();
//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Database SELECT - Get a producto en venta
//        /// </summary>
//        /// <param name="codigo_venta"></param>
//        /// <returns></returns>
//        public Productos_en_venta GetProducto_en_venta(int codigo_venta, int codigo_producto)
//        {
//            try
//            {
//                if (produvenList == null)
//                {
//                    produvenList = GetProductos_en_ventas();
//                }
//                // enumerate through all employee list
//                // and select the concerned employee
//                foreach (Productos_en_venta produven in produvenList)
//                {
//                    if (produven.Codigo_venta == codigo_venta && produven.Codigo_producto == codigo_producto)
//                    {
//                        return produven;
//                    }
//                }
//                return null;
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Method - Get list of all productos en venta
//        /// </summary>
//        /// <returns>Productos_en_venta</returns>
//        public List<Productos_en_venta> GetProductos_en_ventas()
//        {
//            try
//            {
//                using (conn)
//                {
//                    produvenList = new List<Productos_en_venta>();

//                    conn = new SqlConnection(connString);

//                    string sqlSelectString = "SELECT * FROM Productos_en_venta";
//                    command = new SqlCommand(sqlSelectString, conn);
//                    command.Connection.Open();

//                    SqlDataReader reader = command.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        Productos_en_venta produven = new Productos_en_venta();
//                        string _Codigo_producto_temp = reader[0].ToString();
//                        if (_Codigo_producto_temp == "")
//                        {
//                            produven.Codigo_producto = 0;
//                        }
//                        else
//                        {
//                            produven.Codigo_producto = int.Parse(_Codigo_producto_temp);
//                        }
//                        string _Precio_individual_temp = reader[1].ToString();
//                        produven.Precio_individual = double.Parse(_Precio_individual_temp);
//                        string _Cantidad_temp = reader[2].ToString();
//                        produven.Cantidad = int.Parse(_Cantidad_temp);
//                        string _Codigo_venta_temp = reader[3].ToString();
//                        produven.Codigo_venta = int.Parse(_Codigo_venta_temp);
//                        string _Precio_total_temp = reader[4].ToString();
//                        if (_Precio_total_temp == "")
//                        {
//                            produven.Precio_total = 0;
//                        }
//                        else
//                        {
//                            produven.Precio_total = double.Parse(_Precio_total_temp);
//                        }
//                        produvenList.Add(produven);

//                    }
//                    command.Connection.Close();
//                    return produvenList;
//                }

//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        #endregion
//        #region Operaciones sobre proveedores
//        /// <summary>
//        /// Database INSERT - Add a Proveedor
//        /// </summary>
//        /// <param name="prove"></param>
//        public bool AddProveedor(Proveedor prove)
//        {
//            try
//            {
//                using (conn)
//                {
//                    //using parametirized query
//                    string sqlInserString =
//                    "INSERT INTO Proveedor (Cedula, Tipo, Nombre) VALUES (@cedula, @tipo, @nombre)";

//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();
//                    command.CommandText = sqlInserString;

//                    SqlParameter Cedulaparam = new SqlParameter("@cedula", prove.Cedula);
//                    SqlParameter Tipoparam = new SqlParameter("@tipo", prove.Tipo);
//                    SqlParameter Nombreparam = new SqlParameter("@nombre", prove.Nombre);

//                    command.Parameters.AddRange(new SqlParameter[] { Cedulaparam, Tipoparam, Nombreparam });
//                    command.ExecuteNonQuery();
//                    command.Connection.Close();
//                    return true;

//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Database UPDATE - Update a Proveedor
//        /// </summary>
//        /// <param name="prove"></param>
//        public bool UpdateProveedor(Proveedor prove)
//        {
//            try
//            {
//                using (conn)
//                {
//                    string sqlUpdateString =
//                    "UPDATE Proveedor SET Tipo=@tipo, Nombre=@nombre WHERE Cedula=@cedula ";
//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();
//                    command.CommandText = sqlUpdateString;

//                    SqlParameter Cedulaparam = new SqlParameter("@cedula", prove.Cedula.ToString());
//                    SqlParameter Tipoparam = new SqlParameter("@tipo", prove.Tipo);
//                    SqlParameter Nombreparam = new SqlParameter("@nombre", prove.Nombre);

//                    command.Parameters.AddRange(new SqlParameter[] { Cedulaparam, Tipoparam, Nombreparam });
//                    command.ExecuteNonQuery();
//                    #region  Revision de que se hizo bien
//                    string buscarProveedor = "SELECT * FROM Proveedor WHERE Cedula=@cedula";
//                    string proveedorEncontrado = "";
//                    command.CommandText = buscarProveedor;
//                    SqlDataReader reader = command.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        proveedorEncontrado = reader[0].ToString();
//                    }
//                    command.Connection.Close();
//                    if (proveedorEncontrado != "")
//                    {
//                        return true;
//                    }
//                    else
//                    {
//                        return false;
//                    }
//                    #endregion
//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Database DELETE - Delete a Proveeodr
//        /// </summary>
//        /// <param name="cedula"></param>
//        public bool DeleteProveedor(int cedula)
//        {
//            try
//            {
//                using (conn)
//                {
//                    string sqlDeleteString =
//                    "DELETE FROM Proveedor WHERE Cedula=@cedula ";

//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();
                    

//                    SqlParameter cedulaparam = new SqlParameter("@cedula", cedula);
//                    command.Parameters.Add(cedulaparam);
//                    #region Borrar prove de Compra
//                    string sqlBorrarProveCompra = "UPDATE Compra SET Cedula_proveedor=NULL WHERE Cedula_proveedor=@cedula";
//                    command.CommandText = sqlBorrarProveCompra;
//                    command.ExecuteNonQuery();
//                    #endregion
//                    #region Borrar prove de Producto
//                    string sqlBorrarProveProducto = "UPDATE Producto SET Cedula_proveedor=NULL WHERE Cedula_proveedor=@cedula";
//                    command.CommandText = sqlBorrarProveProducto;
//                    command.ExecuteNonQuery();
//                    #endregion

//                    command.CommandText = sqlDeleteString;
//                    command.ExecuteNonQuery();
//                    command.Connection.Close();
//                    return true;
//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Database SELECT - Get a Proveedor
//        /// </summary>
//        /// <param name="cedula"></param>
//        /// <returns></returns>
//        public Proveedor GetProveedor(int cedula)
//        {
//            try
//            {
//                if (proveList == null)
//                {
//                    proveList = GetProveedores();
//                }
//                // enumerate through all employee list
//                // and select the concerned employee
//                foreach (Proveedor prove in proveList)
//                {
//                    if (prove.Cedula == cedula)
//                    {
//                        return prove;
//                    }
//                }
//                return null;
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Method - Get list of all Proveedores
//        /// </summary>
//        /// <returns>Proveedor</returns>
//        public List<Proveedor> GetProveedores()
//        {
//            try
//            {
//                using (conn)
//                {
//                    proveList = new List<Proveedor>();

//                    conn = new SqlConnection(connString);

//                    string sqlSelectString = "SELECT * FROM Proveedor";
//                    command = new SqlCommand(sqlSelectString, conn);
//                    command.Connection.Open();

//                    SqlDataReader reader = command.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        Proveedor prove = new Proveedor();
//                        string _Cedula_temp = reader[0].ToString();
//                        prove.Cedula = int.Parse(_Cedula_temp);
//                        prove.Tipo = reader[1].ToString();
//                        prove.Nombre = reader[2].ToString();
//                        proveList.Add(prove);
//                    }
//                    command.Connection.Close();
//                    return proveList;
//                }

//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        #endregion
//        /*
//        #region Operaciones sobre roles
//        /// <summary>
//        /// Database INSERT - Add a Rol
//        /// </summary>
//        /// <param name="rol"></param>
//        public bool AddRol(Rol rol)
//        {
//            try
//            {
//                using (conn)
//                {
//                    //using parametirized query
//                    string sqlInserString = "";

//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();
                    

//                    SqlParameter Nombreparam = new SqlParameter("@nombre", rol.Nombre);
//                    SqlParameter Descripcionparam = new SqlParameter("@descripcion", rol.Descripcion);
//                    command.Parameters.AddRange(new SqlParameter[] { Nombreparam, Descripcionparam });
//                    sqlInserString =
//                    "INSERT INTO Rol (Nombre, Descripcion) VALUES (@nombre, @descripcion)";
//                    command.CommandText = sqlInserString;
//                    command.ExecuteNonQuery();
//                    command.Connection.Close();
//                    return true;
//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Database UPDATE - Update a Rol
//        /// </summary>
//        /// <param name="rol"></param>
//        public bool UpdateRol(Rol rol)
//        {
//            try
//            {
//                using (conn)
//                {
//                    string sqlUpdateString =
//                    "UPDATE Rol SET Descripcion=@descripcion WHERE Nombre=@nombre";
//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();
//                    command.CommandText = sqlUpdateString;

//                    SqlParameter Nombreparam = new SqlParameter("@nombre", rol.Nombre);
//                    SqlParameter Descripcionparam = new SqlParameter("@descripcion", rol.Descripcion);

//                    command.Parameters.AddRange(new SqlParameter[] { Nombreparam, Descripcionparam});
//                    command.ExecuteNonQuery();
//                    #region  Revision de que se hizo bien
//                    string buscarRol = "SELECT * FROM Rol WHERE Nombre=@nombre";
//                    string rolEncontrado = "";
//                    command.CommandText = buscarRol;
//                    SqlDataReader reader = command.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        rolEncontrado = reader[0].ToString();
//                    }
//                    command.Connection.Close();
//                    if (rolEncontrado != "")
//                    {
//                        return true;
//                    }
//                    else
//                    {
//                        return false;
//                    }
//                    #endregion
//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Database DELETE - Delete a Rol
//        /// </summary>
//        /// <param name="cedula_empleado"></param>
//        public bool DeleteRol(string nombre)
//        {
//            try
//            {
//                using (conn)
//                {
//                    string sqlDeleteString =
//                    "DELETE FROM Rol WHERE Nombre=@nombre ";

//                    conn = new SqlConnection(connString);

//                    command = new SqlCommand();
//                    command.Connection = conn;
//                    command.Connection.Open();

//                    SqlParameter nombreparam = new SqlParameter("@nombre", nombre);
//                    command.Parameters.Add(nombreparam);

//                    command.CommandText = sqlDeleteString;
//                    command.ExecuteNonQuery();
//                    command.Connection.Close();
//                    return true;
//                }
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Database SELECT - Get an Rol
//        /// </summary>
//        /// <param name="nombre"></param>
//        /// <returns></returns>
//        public Rol GetRol(string nombre)
//        {
//            try
//            {
//                if (rolList == null)
//                {
//                    rolList = GetRoles();
//                }
//                // enumerate through all employee list
//                // and select the concerned employee
//                foreach (Rol rol in rolList)
//                {
//                    if (rol.Nombre == nombre)
//                    {
//                        return rol;
//                    }
//                }
//                return null;
//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        /// <summary>
//        /// Method - Get list of all Roles
//        /// </summary>
//        /// <returns>Rol</returns>
//        public List<Rol> GetRoles()
//        {
//            try
//            {
//                using (conn)
//                {
//                    rolList = new List<Rol>();

//                    conn = new SqlConnection(connString);

//                    string sqlSelectString = "SELECT * FROM Rol";
//                    command = new SqlCommand(sqlSelectString, conn);
//                    command.Connection.Open();

//                    SqlDataReader reader = command.ExecuteReader();
//                    while (reader.Read())
//                    {
//                        Rol rol = new Rol();
//                        rol.Nombre = reader[0].ToString();
//                        rol.Descripcion = reader[1].ToString();
//                        rolList.Add(rol);
//                    }
//                    command.Connection.Close();
//                    return rolList;
//                }

//            }
//            catch (Exception ex)
//            {
//                err.ErrorMessage = ex.Message.ToString();
//                throw;
//            }
//        }
//        #endregion
//        */
//        /// <summary>
//        /// Get Exception if any
//        /// </summary>
//        /// <returns> Error Message</returns>
//        public string GetException()
//        {
//            return err.ErrorMessage.ToString();
//        }
//    }
//}
