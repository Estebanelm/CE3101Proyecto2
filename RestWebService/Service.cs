using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestWebService
{
    public class Service:IHttpHandler
    {
        #region Private Members

        //Estos miembros están presentes para evitar repetición de declaracione dentro de las funciones
        private BancaTec.Empleado emp;
        private BancaTec.Sucursal suc;
        private BancaTec.Categoria cat;
        private BancaTec.Compra com;
        private BancaTec.Venta ven;
        private BancaTec.Producto produ;
        private BancaTec.Productos_en_compra producom;
        private BancaTec.Productos_en_venta produven;
        private BancaTec.Proveedor prove;
        //private BancaTec.Rol rol;
        private Operations.Operations operations;
        private string connString; //string con los parámetros de conexión hacia la base de datos
        private ErrorHandler.ErrorHandler errHandler;

        #endregion

        #region Handler
        bool IHttpHandler.IsReusable
        {
            get { throw new NotImplementedException(); }
        }

        //Método que procesa los request. Este método debe de existir para poder recibir
        //las solicitudes desde los clientes.
        void IHttpHandler.ProcessRequest(HttpContext context)
        {
            try
            {                
                string url = Convert.ToString(context.Request.Url);
                string request_instance = url.Split('/').Last<String>().Split('?')[0]; //instance de la solicitud, ya sea empleado, sucursal, etc.
                connString = Properties.Settings.Default.ConnectionString;
                operations = new Operations.Operations(connString); //estas variables se deben inicializar acá para que se realice
                errHandler = new ErrorHandler.ErrorHandler();       //cada vez que el cliente hace una solicitud

                //Handling CRUD

                switch (context.Request.HttpMethod)
                {
                    case "GET":
                        {
                            string isDelete = context.Request["Delete"]; //determina si existe el parámetro delete
                            if (isDelete == null)                        // ya que no hay soporte a delete en chrome
                            {
                                READ(context, request_instance);    //si no es delete, haga el read
                                break;
                            }
                            else if (isDelete == "1")                //si es delete, hace el borrado
                            {
                                DELETE(context, request_instance);
                                break;
                            }
                            break;
                        }
                    case "POST":
                        {
                            //Perform CREATE Operation
                            string isPut = context.Request["Put"];//determina si existe el parámetro put
                            if (isPut == null)                    //debido a la falta de soporte por parte de Chrome
                            {
                                CREATE(context, request_instance); //hacer create porque es post
                                break;
                            }
                            else if (isPut == "1")
                            {
                                UPDATE(context, request_instance); //hacer update porque es put
                                break;
                            }
                            break;
                        }
                        //Casos legacy, ya que el soporte en chrome no existe para delete y put.
                        //Estos metodos funcionan en Internet Explorer.
                    //case "PUT":
                        //Perform UPDATE Operation
                        //UPDATE(context, request_instance);
                        //break;
                   // case "DELETE":
                        //Perform DELETE Operation
                        //DELETE(context, request_instance);
                        //break;
                    default:
                        break;
                }
                
            }
            catch (Exception ex)
            {
                
                errHandler.ErrorMessage = ex.Message.ToString();
                context.Response.Write(errHandler.ErrorMessage);                
            }
        }

        #endregion Handler

        #region CRUD Functions
        /// <summary>
        /// GET Operation
        /// Todos las regiones marcan un if que va a permitir determinar cuál operación hacer dentro de la base de datos.
        /// De esta manera, todas tienen una funcionalidad casi igual y el código es muy parecido entre ellos, solamente
        /// elige entre el método a llamar en la clase Operations.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="request_instance"></param>
        private void READ( HttpContext context, string request_instance)
        {
            //HTTP Request - //http://server.com/virtual directory/empleado?id={id}
            //http://localhost/RestWebService/empleado
            //Formato de la forma de hacer el request
            try
            {
                #region Empleado
                if (request_instance == "empleado")
                {
                    string _cedula_temp = context.Request["cedula"]; //obtiene el valor del parámetro cedula
                    if (_cedula_temp == null) //si no hay parámetro, obtener todos los empleados
                    {
                        List<BancaTec.Empleado> lista_empleados = operations.GetEmpleados();
                        string serializedList = Serialize(lista_empleados);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedList);

                    }
                    else //si hay parámetro cedula, obtener solo 1 empleado
                    {
                        int _cedula = int.Parse(_cedula_temp);
                        emp = operations.GetEmpleado(_cedula);
                        if (emp == null)
                            context.Response.Write("No Empleado Found" + context.Request["cedula"]);

                        string serializedEmpleado = Serialize(emp);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedEmpleado);
                    }
                }
                #endregion
                #region Sucursal
                else if (request_instance == "sucursal")
                {
                    string _codigo_temp = context.Request["codigo"];
                    if (_codigo_temp == null)
                    {
                        List<BancaTec.Sucursal> lista_sucursales = operations.GetSucursales();
                        string serializedList = Serialize(lista_sucursales);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedList);

                    }
                    else
                    {
                        string _codigo = _codigo_temp;
                        suc = operations.GetSucursal(_codigo);
                        if (suc == null)
                            context.Response.Write(_codigo + "No Sucursal Found" + context.Request["codigo"]);

                        string serializedSucursal = Serialize(suc);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedSucursal);
                    }
                }
                #endregion
                #region Categoria
                else if (request_instance == "categoria")
                {
                    string _id_temp = context.Request["id"];
                    if (_id_temp == null)
                    {
                        List<BancaTec.Categoria> lista_categorias= operations.GetCategorias();
                        string serializedList = Serialize(lista_categorias);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedList);

                    }
                    else
                    {
                        int _id = int.Parse(_id_temp);

                        //HTTP Request Type - GET"
                        //Performing Operation - READ"
                        cat = operations.GetCategoria(_id);
                        if (cat == null)
                            context.Response.Write(_id + "No Categoria Found" + _id_temp);

                        string serializedCategoria = Serialize(cat);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedCategoria);
                    }
                }
                #endregion
                #region Compra
                else if (request_instance == "compra")
                {
                    string _codigotemp = context.Request["codigo"];
                    string _fecha_inicial = context.Request["fecha_inicial"];
                    string _fecha_final = context.Request["fecha_final"];
                    string _codigo_sucursal = context.Request["codigo_sucursal"];
                    if (_codigotemp == null)
                    {
                        if (_codigo_sucursal == null)
                        {
                            if (_fecha_inicial != null && _fecha_final != null)
                            {
                                List<Operations.Gasto> lista_compras = operations.GetGastos("", _fecha_inicial, _fecha_final);
                                string serializedList = Serialize(lista_compras);
                                context.Response.ContentType = "text/xml";
                                WriteResponse(serializedList);
                            }
                            else
                            {
                                List<BancaTec.Compra> lista_compras = operations.GetCompras();
                                string serializedList = Serialize(lista_compras);
                                context.Response.ContentType = "text/xml";
                                WriteResponse(serializedList);
                            }
                        }
                        else
                        {
                            if (_fecha_inicial != null && _fecha_final != null)
                            {
                                List<Operations.Gasto> lista_compras = operations.GetGastos(_codigo_sucursal, _fecha_inicial, _fecha_final);
                                string serializedList = Serialize(lista_compras);
                                context.Response.ContentType = "text/xml";
                                WriteResponse(serializedList);
                            }
                            else
                            {
                                List<BancaTec.Compra> lista_compras = operations.GetCompras();
                                string serializedList = Serialize(lista_compras);
                                context.Response.ContentType = "text/xml";
                                WriteResponse(serializedList);
                            }
                        }
                        

                    }
                    else
                    {
                        int _codigo = int.Parse(_codigotemp);

                        //HTTP Request Type - GET"
                        //Performing Operation - READ"
                        com = operations.GetCompra(_codigo);
                        if (com == null)
                            context.Response.Write(_codigo + "No Compra Found" + _codigotemp);

                        string serializedCompra = Serialize(com);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedCompra);
                    }
                }
                #endregion
                #region Horas
                else if (request_instance == "horas")
                {
                    string _horastemp = context.Request["id_semana"];
                    if (_horastemp == null)
                    {
                        List<BancaTec.Horas> lista_horas = operations.GetHorases();
                        string serializedList = Serialize(lista_horas);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedList);

                    }
                    else
                    {
                        string _horas = _horastemp;
                        List<BancaTec.Horas> listaHorasSemana = new List<BancaTec.Horas>();
                        //HTTP Request Type - GET"
                        //Performing Operation - READ"
                        listaHorasSemana = operations.GetHoras(_horas);
                        if (listaHorasSemana.Count == 0)
                            context.Response.Write(_horas + "No Horas Found" + _horastemp);

                        string serializedHoras = Serialize(listaHorasSemana);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedHoras);
                    }
                }
                #endregion
                #region Producto
                else if (request_instance == "producto")
                {
                    string _codigo_barrastemp = context.Request["codigo_barras"];
                    string _codigo_sucursaltemp = context.Request["codigo_sucursal"];
                    string _todos = context.Request["Todos"];
                    if (_codigo_barrastemp == null)
                    {
                        if (_codigo_sucursaltemp == null)
                        {
                            if (_todos == null)
                            {
                                List<BancaTec.Producto> lista_productos = operations.GetProductos();
                                string serializedList = Serialize(lista_productos);
                                context.Response.ContentType = "text/xml";
                                WriteResponse(serializedList);
                            }
                            else
                            {
                                List<Operations.ReporteProductos> lista_productos = operations.GetProductosTodos();
                                string serializedList = Serialize(lista_productos);
                                context.Response.ContentType = "text/xml";
                                WriteResponse(serializedList);
                            }
                        }
                        else
                        {
                            List<Operations.ReporteProductosSucursal> lista_productos = operations.GetProductosporSucursal(_codigo_sucursaltemp);
                            string serializedList = Serialize(lista_productos);
                            context.Response.ContentType = "text/xml";
                            WriteResponse(serializedList);
                        }
                    }
                    else
                    {
                        int _codigo_barras = int.Parse(_codigo_barrastemp);

                        //HTTP Request Type - GET"
                        //Performing Operation - READ"
                        produ = operations.GetProducto(_codigo_barras, _codigo_sucursaltemp);
                        if (produ == null)
                            context.Response.Write(_codigo_barras + "No Producto Found" + _codigo_barrastemp);

                        string serializedProducto = Serialize(produ);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedProducto);
                    }
                }
                #endregion
                #region Productos_en_compra
                else if (request_instance == "productos_en_compra")
                {
                    string _codigo_compratemp = context.Request["codigo_compra"];
                    string _codigo_productotemp = context.Request["codigo_producto"];
                    if (_codigo_compratemp == null)
                    {
                        List<BancaTec.Productos_en_compra> lista_productos_en_compra = operations.GetProductos_en_compras();
                        string serializedList = Serialize(lista_productos_en_compra);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedList);

                    }
                    else
                    {
                        int _codigo_compra = int.Parse(_codigo_compratemp);
                        int _codigo_producto = int.Parse(_codigo_productotemp);

                        //HTTP Request Type - GET"
                        //Performing Operation - READ"
                        producom = operations.GetProducto_en_compra(_codigo_compra, _codigo_producto);
                        if (producom == null)
                            context.Response.Write(_codigo_compra + "No Producto Found" + _codigo_compratemp);

                        string serializedProductos_en_compra = Serialize(producom);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedProductos_en_compra);
                    }
                }
                #endregion
                #region Productos_en_venta
                else if (request_instance == "productos_en_venta")
                {
                    string _codigo_ventatemp = context.Request["codigo_venta"];
                    string _codigo_productotemp = context.Request["codigo_producto"];
                    if (_codigo_ventatemp == null)
                    {
                        List<BancaTec.Productos_en_venta> lista_productos_en_venta = operations.GetProductos_en_ventas();
                        string serializedList = Serialize(lista_productos_en_venta);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedList);

                    }
                    else
                    {
                        int _codigo_venta = int.Parse(_codigo_ventatemp);
                        int _codigo_producto = int.Parse(_codigo_productotemp);

                        //HTTP Request Type - GET"
                        //Performing Operation - READ"
                        produven = operations.GetProducto_en_venta(_codigo_venta, _codigo_producto);
                        if (produven == null)
                            context.Response.Write(_codigo_venta + "No Producto Found" + _codigo_ventatemp);

                        string serializedProductos_en_venta = Serialize(produven);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedProductos_en_venta);
                    }
                }
                #endregion
                #region Proveedor
                else if (request_instance == "proveedor")
                {
                    string _cedulatemp = context.Request["cedula"];
                    if (_cedulatemp == null)
                    {
                        List<BancaTec.Proveedor> lista_proveedores = operations.GetProveedores();
                        string serializedList = Serialize(lista_proveedores);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedList);

                    }
                    else
                    {
                        int _cedula = int.Parse(_cedulatemp);

                        //HTTP Request Type - GET"
                        //Performing Operation - READ"
                        prove = operations.GetProveedor(_cedula);
                        if (prove == null)
                            context.Response.Write(_cedula + "No Producto Found" + _cedulatemp);

                        string serializedProveedor = Serialize(prove);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedProveedor);
                    }
                }
                #endregion
                /*
                #region Rol
                else if (request_instance == "rol")
                {
                    string nombretemp = context.Request["nombre"];
                    if (nombretemp == null)
                    {
                        List<BancaTec.Rol> lista_roles = operations.GetRoles();
                        string serializedList = Serialize(lista_roles);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedList);

                    }
                    else
                    {
                        string nombre = nombretemp;

                        //HTTP Request Type - GET"
                        //Performing Operation - READ"
                        rol = operations.GetRol(nombre);
                        if (rol == null)
                            context.Response.Write(nombre + "No Producto Found" + nombretemp);

                        string serializedRol = Serialize(rol);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedRol);
                    }
                }
                #endregion
    */
                #region Venta
                else if (request_instance == "venta")
                {
                    string _codigotemp = context.Request["codigo"];
                    if (_codigotemp == null)
                    {
                        List<BancaTec.Venta> lista_ventas = operations.GetVentas();
                        string serializedList = Serialize(lista_ventas);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedList);

                    }
                    else
                    {
                        int _codigo = int.Parse(_codigotemp);

                        //HTTP Request Type - GET"
                        //Performing Operation - READ"
                        ven = operations.GetVenta(_codigo);
                        if (ven == null)
                            context.Response.Write(_codigo + "No Producto Found" + _codigotemp);

                        string serializedVenta = Serialize(ven);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedVenta);
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                WriteResponse(ex.Message.ToString());
                errHandler.ErrorMessage = operations.GetException();
                errHandler.ErrorMessage = ex.Message.ToString();                
            }            
        }
        /// <summary>
        /// POST Operation
        /// Esta función consiste de los if que determinan cuál método llamar en la clase Operations.
        /// Solamente se evaluará lo que el cliente solicita y se procede a llamar la función adecuada.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="request_instance"></param>
        private void CREATE(HttpContext context, string request_instance)
        {
            try
            {
                #region Empleado
                if (request_instance == "empleado")
                {
                    BancaTec.Empleado emp = new BancaTec.Empleado(context);
                    operations.AddEmpleado(emp);
                }
                #endregion
                #region Sucursal
                else if (request_instance == "sucursal")
                {
                    BancaTec.Sucursal suc = new BancaTec.Sucursal(context);
                    //L3MDB.Empleado emp = Deserialize(PostData);                
                    // Insert data in database
                    operations.AddSucursal(suc);
                }
                #endregion
                #region Categoria
                else if (request_instance == "categoria")
                {
                    BancaTec.Categoria cat = new BancaTec.Categoria(context);
                    //L3MDB.Empleado emp = Deserialize(PostData);                
                    // Insert data in database
                    operations.AddCategoria(cat);
                }
                #endregion
                #region Compra
                else if (request_instance == "compra")
                {
                    if (context.Request["productos"] == null || context.Request["cantidad"] == null)
                    {
                        BancaTec.Compra com = new BancaTec.Compra(context);
                        //L3MDB.Empleado emp = Deserialize(PostData);                
                        // Insert data in database
                        operations.AddCompra(com);
                    }
                    else
                    {
                        BancaTec.Compra com = new BancaTec.Compra(context);
                        operations.AddCompraProductos(com, context);
                    }
                }
                #endregion
                #region Horas
                else if (request_instance == "horas")
                {
                    BancaTec.Horas hor = new BancaTec.Horas(context);
                    //L3MDB.Empleado emp = Deserialize(PostData);                
                    // Insert data in database
                    operations.AddHoras(hor);
                }
                #endregion
                #region Producto
                else if (request_instance == "producto")
                {
                    BancaTec.Producto produ = new BancaTec.Producto(context);
                    //L3MDB.Empleado emp = Deserialize(PostData);                
                    // Insert data in database
                    operations.AddProducto(produ);
                }
                #endregion
                #region Productos_en_compra
                else if (request_instance == "productos_en_compra")
                {
                    BancaTec.Productos_en_compra producom = new BancaTec.Productos_en_compra(context);
                    //L3MDB.Empleado emp = Deserialize(PostData);                
                    // Insert data in database
                    operations.AddProductocompra(producom);
                }
                #endregion
                #region Productos_en_venta
                else if (request_instance == "productos_en_venta")
                {
                    BancaTec.Productos_en_venta produven = new BancaTec.Productos_en_venta(context);
                    //L3MDB.Empleado emp = Deserialize(PostData);                
                    // Insert data in database
                    operations.AddProductoventa(produven);
                }
                #endregion
                #region Proveedor
                else if (request_instance == "proveedor")
                {
                    BancaTec.Proveedor prove = new BancaTec.Proveedor(context);
                    //L3MDB.Empleado emp = Deserialize(PostData);                
                    // Insert data in database
                    operations.AddProveedor(prove);
                }
                #endregion
                /*
                #region Rol
                else if (request_instance == "rol")
                {
                    BancaTec.Rol rol = new BancaTec.Rol(context);
                    //L3MDB.Empleado emp = Deserialize(PostData);                
                    // Insert data in database
                    operations.AddRol(rol);
                }
                #endregion
    */
                #region Venta
                else if (request_instance == "venta")
                {
                    if (context.Request["productos"] == null || context.Request["cantidad"] == null)
                    {
                        BancaTec.Venta ven = new BancaTec.Venta(context);
                        //L3MDB.Empleado emp = Deserialize(PostData);                
                        // Insert data in database
                        operations.AddVenta(ven);
                    }
                    else
                    {
                        operations.AddVentaProductos(context);
                    }
                }
                #endregion               
            }
            catch (Exception ex)
            {

                WriteResponse(ex.Message.ToString());
                errHandler.ErrorMessage = operations.GetException();
                errHandler.ErrorMessage = ex.Message.ToString();                
            }
        }
        /// <summary>
        /// PUT Operation
        /// Al igual que en post, solamente se va a determinar el método a llamar en la clase Operations.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="request_instance"></param>
        private void UPDATE(HttpContext context, string request_instance)
        {           
            try
            {
                #region Empleado
                if (request_instance == "empleado")
                {
                    BancaTec.Empleado emp = new BancaTec.Empleado(context);
                    operations.UpdateEmpleado(emp);
                    context.Response.Write("Employee Updtated Sucessfully");
                    WriteResponse("oka");
                }
                #endregion
                #region Sucursal
                if (request_instance == "sucursal")
                {
                    BancaTec.Sucursal suc = new BancaTec.Sucursal(context);
                    operations.UpdateSucursal(suc);
                    WriteResponse("ok");
                }
                #endregion
                #region Categoria
                if (request_instance == "categoria")
                {
                    BancaTec.Categoria cat = new BancaTec.Categoria(context);
                    operations.UpdateCategoria(cat);
                    WriteResponse("ok");
                }
                #endregion
                #region Compra
                if (request_instance == "compra")
                {
                    BancaTec.Compra com = new BancaTec.Compra(context);
                    operations.UpdateCompra(com);
                    WriteResponse("ok");
                }
                #endregion
                #region Horas
                if (request_instance == "horas")
                {
                    BancaTec.Horas hor = new BancaTec.Horas(context);
                    operations.UpdateHoras(hor);
                    WriteResponse("ok");
                }
                #endregion
                #region Producto
                if (request_instance == "producto")
                {
                    BancaTec.Producto produ = new BancaTec.Producto(context);
                    operations.UpdateProducto(produ);
                    WriteResponse("ok");
                }
                #endregion
                #region Productos_en_compra
                if (request_instance == "productos_en_compra")
                {
                    string listaproductosconComas = context.Request["Productos"];
                    string listacantidadesconComas = context.Request["Cantidad"];
                    if (listaproductosconComas == null)
                    {
                        BancaTec.Productos_en_compra producom = new BancaTec.Productos_en_compra(context);
                        operations.UpdateProductocompra(producom);
                    }
                    else
                    {
                        string codigo_compra_temp = context.Request["codigo_compra"];
                        int codigo_compra = int.Parse(codigo_compra_temp);
                        string[] listaProductosSeparados = listaproductosconComas.Split(',');
                        string[] listaCantidadesSeparadas = listacantidadesconComas.Split(',');
                        for (int i = 0; i < listaProductosSeparados.Length; i++)
                        {
                            BancaTec.Productos_en_compra produCompModificar = new BancaTec.Productos_en_compra();
                            int codigo_producto = int.Parse(listaProductosSeparados[i]);
                            int cantidad = int.Parse(listaCantidadesSeparadas[i]);
                            produCompModificar.Cantidad = cantidad;
                            produCompModificar.Codigo_compra = codigo_compra;
                            produCompModificar.Codigo_producto = codigo_producto;
                            operations.UpdateProductocompra(produCompModificar);
                        }
                    }
                    
                    WriteResponse("ok");
                }
                #endregion
                #region Productos_en_venta
                if (request_instance == "productos_en_venta")
                {
                    BancaTec.Productos_en_venta produven = new BancaTec.Productos_en_venta(context);
                    operations.UpdateProductoventa(produven);
                    WriteResponse("ok");
                }
                #endregion
                #region Proveedor
                if (request_instance == "proveedor")
                {
                    BancaTec.Proveedor prove = new BancaTec.Proveedor(context);
                    operations.UpdateProveedor(prove);
                    WriteResponse("ok");
                }
                #endregion
                /*
                #region Rol
                if (request_instance == "rol")
                {
                    BancaTec.Rol rol = new BancaTec.Rol(context);
                    operations.UpdateRol(rol);
                    WriteResponse("ok");
                }
                #endregion
    */
                #region Venta
                if (request_instance == "venta")
                {
                    BancaTec.Venta ven = new BancaTec.Venta(context);
                    operations.UpdateVenta(ven);
                    WriteResponse("ok");
                }
                #endregion
            }
            catch (Exception ex)
            {

                WriteResponse(ex.Message.ToString());
                errHandler.ErrorMessage = operations.GetException();
                errHandler.ErrorMessage = ex.Message.ToString();                
            }
        }
        /// <summary>
        /// DELETE Operation
        /// Tiene la misma función que el método Get, por lo que funciona de la misma forma
        /// </summary>
        /// <param name="context"></param>
        private void DELETE(HttpContext context, string request_instance)
        {
            try
            {
                #region Empleado
                if (request_instance == "empleado")
                {
                    string _cedula_temp = context.Request["cedula"];
                    int _cedula = int.Parse(_cedula_temp);
                    operations.DeleteEmpleado(_cedula);
                    WriteResponse("ok");
                }
                #endregion
                #region Sucursal
                if (request_instance == "sucursal")
                {
                    string _codigo_temp = context.Request["codigo"];
                    operations.DeleteSucursal(_codigo_temp);
                    WriteResponse("ok");
                }
                #endregion
                #region Categoria
                if (request_instance == "categoria")
                {
                    string _id_temp = context.Request["id"];
                    int _id = int.Parse(_id_temp);
                    operations.DeleteCategoria(_id);
                    WriteResponse("ok");
                }
                #endregion
                #region Compra
                if (request_instance == "compra")
                {
                    string _codigo_temp = context.Request["codigo"];
                    int _codigo = int.Parse(_codigo_temp);
                    operations.DeleteCompra(_codigo);
                    WriteResponse("ok");
                }
                #endregion
                #region Horas
                if (request_instance == "horas")
                {
                    string _id_semana = context.Request["id_semana"];
                    string _cedempleado_temp = context.Request["ced_empleado"];
                    int _cedempleado = int.Parse(_cedempleado_temp);
                    operations.DeleteHoras(_id_semana, _cedempleado);
                    WriteResponse("ok");
                }
                #endregion
                #region Producto
                if (request_instance == "producto")
                {
                    string _codigo_barras_temp = context.Request["codigo_barras"];
                    int _codigo_barras = int.Parse(_codigo_barras_temp);
                    string _codigo_sucursal = context.Request["codigo_sucursal"];
                    operations.DeleteProducto(_codigo_barras, _codigo_sucursal);
                    WriteResponse("ok");
                }
                #endregion
                #region Productos_en_compra
                if (request_instance == "productos_en_compra")
                {
                    string _codigo_compra_temp = context.Request["codigo_compra"];
                    string _codigo_productotemp = context.Request["codigo_producto"];
                    int _codigo_compra = int.Parse(_codigo_compra_temp);
                    int _codigo_producto = int.Parse(_codigo_productotemp);
                    operations.DeleteProductocompra(_codigo_compra, _codigo_producto);
                    WriteResponse("ok");
                }
                #endregion
                #region Productos_en_venta
                if (request_instance == "productos_en_venta")
                {
                    string _codigo_venta_temp = context.Request["codigo_venta"];
                    string _codigo_productotemp = context.Request["codigo_producto"];
                    int _codigo_venta = int.Parse(_codigo_venta_temp);
                    int _codigo_producto = int.Parse(_codigo_productotemp);
                    operations.DeleteProductoventa(_codigo_venta, _codigo_producto);
                    WriteResponse("ok");
                }
                #endregion
                #region Proveedor
                if (request_instance == "proveedor")
                {
                    string _cedula_temp = context.Request["cedula"];
                    int _cedula = int.Parse(_cedula_temp);
                    operations.DeleteProveedor(_cedula);
                    WriteResponse("ok");
                }
                #endregion
                /*
                #region Rol
                if (request_instance == "rol")
                {
                    string nombre = context.Request["nombre"];
                    operations.DeleteRol(nombre);
                    WriteResponse("ok");
                }
                #endregion
    */
                #region Venta
                if (request_instance == "venta")
                {
                    string _codigo_temp = context.Request["codigo"];
                    int _codigo = int.Parse(_codigo_temp);
                    operations.DeleteVenta(_codigo);
                    WriteResponse("ok");
                }
                #endregion
            }
            catch (Exception ex)
            {
                
                WriteResponse(ex.Message.ToString());
                errHandler.ErrorMessage = operations.GetException();
                errHandler.ErrorMessage = ex.Message.ToString();                
            }
        }

        #endregion

        #region Utility Functions
        /// <summary>
        /// Devuelve un mensaje al cliente
        /// </summary>
        /// <param name="strMessage"></param>
        private static void WriteResponse(string strMessage)
        {
            HttpContext.Current.Response.Write(strMessage);            
        }

        /// <summary>
        /// Convierte una clase en un XML
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private String Serialize<T>(T obj)
        {
            try
            {
                String XmlizedString = null;
                XmlSerializer xs = new XmlSerializer(typeof(T));
                //create an instance of the MemoryStream class since we intend to keep the XML string 
                //in memory instead of saving it to a file.
                MemoryStream memoryStream = new MemoryStream();
                //XmlTextWriter - fast, non-cached, forward-only way of generating streams or files 
                //containing XML data
                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                //Serialize emp in the xmlTextWriter
                xs.Serialize(xmlTextWriter, obj);
                //Get the BaseStream of the xmlTextWriter in the Memory Stream
                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                //Convert to array
                XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
                return XmlizedString;
            }
            catch (Exception ex)
            {
                errHandler.ErrorMessage = ex.Message.ToString();
                throw;
            }           

        }

        /// <summary>
        /// To convert a Byte Array of Unicode values (UTF-8 encoded) to a complete String.
        /// </summary>
        /// <param name="characters">Unicode Byte Array to be converted to String</param>
        /// <returns>String converted from Unicode Byte Array</returns>
        private String UTF8ByteArrayToString(Byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            String constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        #endregion
    }
}
