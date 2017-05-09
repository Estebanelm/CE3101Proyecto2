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
        private BancaTec.Asesor ase;
        private BancaTec.Cliente clie;
        private BancaTec.Cuenta cuen;
        private BancaTec.Prestamo prest;
        private BancaTec.Tarjeta tarje;
        private BancaTec.Producto produ;
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
                #region Asesor
                if (request_instance == "asesor")
                {
                    string _cedula_temp = context.Request["cedula"]; //obtiene el valor del parámetro cedula
                    if (_cedula_temp == null) //si no hay parámetro, obtener todos los empleados
                    {
                        List<BancaTec.Asesor> lista_asesores = operations.GetAsesores();
                        string serializedList = Serialize(lista_asesores);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedList);
                    }
                    else //si hay parámetro cedula, obtener solo 1 empleado
                    {
                        string _cedula = _cedula_temp;
                        ase = operations.GetAsesor(_cedula);
                        if (ase == null)
                            context.Response.Write("No Asesor Found: " + context.Request["cedula"]);

                        string serializedAsesor = Serialize(ase);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedAsesor);
                    }
                }
                #endregion
                #region Cliente
                else if (request_instance == "cliente")
                {
                    string cedula_temp = context.Request["cedula"];
                    if (cedula_temp == null)
                    {
                        List<BancaTec.Cliente> lista_clientes = operations.GetClientes();
                        string serializedList = Serialize(lista_clientes);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedList);

                    }
                    else
                    {
                        string _cedula = cedula_temp;
                        clie = operations.GetSucursal(_cedula);
                        if (clie == null)
                            context.Response.Write(_cedula + "No Cliente Found" + context.Request["cedula"]);

                        string serializedCliente = Serialize(clie);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedCliente);
                    }
                }
                #endregion
                #region Cuenta
                else if (request_instance == "cuenta")
                {
                    string num_temp = context.Request["numcuenta"];
                    if (num_temp == null)
                    {
                        List<BancaTec.Cuenta> lista_cuentas= operations.GetCuentas();
                        string serializedList = Serialize(lista_cuentas);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedList);

                    }
                    else
                    {
                        int _num = int.Parse(num_temp);

                        //HTTP Request Type - GET"
                        //Performing Operation - READ"
                        cuen = operations.GetCuenta(_num);
                        if (cuen == null)
                            context.Response.Write(_num + "No Cuenta Found" + num_temp);

                        string serializedCuenta = Serialize(cuen);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedCuenta);
                    }
                }
                #endregion
                #region Pago
                else if (request_instance == "cuenta")
                {
                    string num_prestamo_temp = context.Request["numprestamo"];
                    string ced_cliente_temp = context.Request["cedcliente"];
                    List<BancaTec.Pago> lista_pagos = new List<BancaTec.Pago>();
                    if (num_prestamo_temp == null && ced_cliente_temp != null)
                    {
                        int ced_cliente = int.Parse(ced_cliente_temp);
                        lista_pagos = operations.GetPagos(ced_cliente, "cliente");
                    }
                    else if (num_prestamo_temp !=null)
                    {
                        int num_prestamo = int.Parse(num_prestamo_temp);
                        lista_pagos = operations.GetPagos(num_prestamo, "prestamo");
                    }
                    else
                    {
                        lista_pagos = operations.GetPagos();
                    }
                    if (lista_pagos == null)
                        context.Response.Write("No Pagos Found");
                    string serializedList = Serialize(lista_pagos);
                    context.Response.ContentType = "text/xml";
                    WriteResponse(serializedList);
                }
                #endregion
                #region Prestamo
                else if (request_instance == "prestamo")
                {
                    string num_temp = context.Request["numero"];
                    if (num_temp == null)
                    {
                        List<BancaTec.Prestamo> lista_prestamos = operations.GetPrestamos();
                        string serializedList = Serialize(lista_prestamos);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedList);

                    }
                    else
                    {
                        int _num = int.Parse(num_temp);

                        //HTTP Request Type - GET"
                        //Performing Operation - READ"
                        prest = operations.GetPrestamo(_num);
                        if (prest == null)
                            context.Response.Write(_num + "No Cuenta Found" + num_temp);

                        string serializedPrestamo = Serialize(prest);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedPrestamo);
                    }
                }
                #endregion
                #region Tarjeta
                else if (request_instance == "tarjeta")
                {
                    string _numtemp = context.Request["numero"];
                    if (_numtemp == null)
                    {
                        List<BancaTec.Tarjeta> lista_tarjetas = operations.GetTarjetas();
                        string serializedList = Serialize(lista_tarjetas);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedList);

                    }
                    else
                    {
                        int _num = int.Parse(_numtemp);
                        //HTTP Request Type - GET"
                        //Performing Operation - READ"
                        tarje = operations.GetTarjeta(_num);
                        if (tarje == null)
                            context.Response.Write(_num + "No Tarjeta Found" + _numtemp);

                        string serializedTarjeta = Serialize(tarje);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedTarjeta);
                    }
                }
                #endregion
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
                            context.Response.Write(nombre + "No Rol Found" + nombretemp);

                        string serializedRol = Serialize(rol);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedRol);
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
                #region Asesor
                if (request_instance == "asesor")
                {
                    BancaTec.Asesor ase = new BancaTec.Asesor {
                        Cedula = context.Request["cedula"],
                        FechaNac = DateTime.Parse(context.Request["fechanac"]),
                        Nombre = context.Request["nombre"],
                        SegNombre = context.Request["segnombre"],
                        PriApellido = context.Request["priapellido"],};
                    operations.AddAsesor(ase);
                }
                #endregion
                #region Cliente
                else if (request_instance == "cliente")
                {
                    BancaTec.Cliente clie = new BancaTec.Cliente
                    {
                        Nombre = context.Request["nombre"],
                        SegundoNombre = context.Request["segundonombre"],
                        PriApellido = context.Request["priapellido"],
                        SegApellido = context.Request["segapellido"],
                        Cedula = context.Request["cedula"],
                        Tipo = context.Request["tipo"],
                        Direccion = context.Request["direccion"],
                        Telefono = context.Request["telefono"],
                        Ingreso = int.Parse(context.Request["ingreso"])
                    };
                    operations.AddCliente(clie);
                }
                #endregion
                #region Cuenta
                else if (request_instance == "cuenta")
                {
                    BancaTec.Cuenta cuen = new BancaTec.Cuenta
                    {
                        Tipo= context.Request["tipo"],
                        Moneda = context.Request["moneda"],
                        Descripcion = context.Request["descripcion"],
                        CedCliente = context.Request["cedcliente"]
                    };
                    operations.AddCuenta(cuen);
                }
                #endregion
                #region Pago
                else if (request_instance == "pago")
                {
                    BancaTec.Pago pag = new BancaTec.Pago
                    {
                        Monto = long.Parse(context.Request["monto"]),
                        NumPrestamo = int.Parse(context.Request["numprestamo"]),
                        Fecha = DateTime.Parse(context.Request["fecha"]),
                        Tipo = context.Request["tipo"],
                        CedCliente = context.Request["cedcliente"]
                    };
                    operations.AddPago(pag);
                }
                #endregion
                #region Prestamo
                else if (request_instance == "prestamo")
                {
                    BancaTec.Prestamo pres = new BancaTec.Prestamo
                    {
                        Interes = double.Parse(context.Request["interes"]),
                        SaldoOrig = long.Parse(context.Request["saldoorig"]),
                        SaldoActual = long.Parse(context.Request["saldoactual"]),
                        CedCliente = context.Request["cedcliente"],
                        CedAsesor = context.Request["cedasesor"],
                        Numero = int.Parse(context.Request["numero"])
                    };
                    operations.AddPrestamo(pres);
                }
                #endregion
                #region Tarjeta
                else if (request_instance == "tarjeta")
                {
                    BancaTec.Tarjeta tarj = new BancaTec.Tarjeta
                    {
                        CodigoSeg = context.Request["codigoseg"],
                        FechaExp = DateTime.Parse(context.Request["fechaexp"]),
                        Saldo = long.Parse(context.Request["saldo"]),
                        Tipo = context.Request["tipo"],
                        NumCuenta = int.Parse(context.Request["numcuenta"]),
                        Numero = int.Parse(context.Request["numero"])
                    };
                    operations.AddTarjeta(tarj);
                }
                #endregion
                #region Rol
                else if (request_instance == "rol")
                {
                    BancaTec.Rol rol = new BancaTec.Rol
                    {
                        Nombre = context.Request["nombre"],
                        Descripcion = context.Request["descripcion"]
                    };
                    operations.AddRol(rol);
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
                #region Asesor
                if (request_instance == "asesor")
                {
                    BancaTec.Asesor ase = new BancaTec.Asesor
                    {
                        Cedula = context.Request["cedula"],
                        FechaNac = DateTime.Parse(context.Request["fechanac"]),
                        Nombre = context.Request["nombre"],
                        SegNombre = context.Request["segnombre"],
                        PriApellido = context.Request["priapellido"],
                    };
                    operations.UpdateAsesor(ase);
                    context.Response.Write("Asesor Updated Sucessfully");
                    WriteResponse("ok");
                }
                #endregion
                #region Cliente
                if (request_instance == "cliente")
                {
                    BancaTec.Cliente clie = new BancaTec.Cliente
                    {
                        Nombre = context.Request["nombre"],
                        SegundoNombre = context.Request["segundonombre"],
                        PriApellido = context.Request["priapellido"],
                        SegApellido = context.Request["segapellido"],
                        Cedula = context.Request["cedula"],
                        Tipo = context.Request["tipo"],
                        Direccion = context.Request["direccion"],
                        Telefono = context.Request["telefono"],
                        Ingreso = int.Parse(context.Request["ingreso"])
                    };
                    operations.UpdateSucursal(clie);
                    WriteResponse("ok");
                }
                #endregion
                #region Cuenta
                else if (request_instance == "cuenta")
                {
                    BancaTec.Cuenta cuen = new BancaTec.Cuenta
                    {
                        Tipo = context.Request["tipo"],
                        Moneda = context.Request["moneda"],
                        Descripcion = context.Request["descripcion"],
                        CedCliente = context.Request["cedcliente"]
                    };
                    operations.UpdateCuenta(cuen);
                    WriteResponse("ok");
                }
                #endregion
                #region Pago
                else if (request_instance == "pago")
                {
                    BancaTec.Pago pag = new BancaTec.Pago
                    {
                        Monto = long.Parse(context.Request["monto"]),
                        NumPrestamo = int.Parse(context.Request["numprestamo"]),
                        Fecha = DateTime.Parse(context.Request["fecha"]),
                        Tipo = context.Request["tipo"],
                        CedCliente = context.Request["cedcliente"]
                    };
                    operations.UpdatePago(pag);
                    WriteResponse("ok");
                }
                #endregion
                #region Prestamo
                else if (request_instance == "prestamo")
                {
                    BancaTec.Prestamo pres = new BancaTec.Prestamo
                    {
                        Interes = double.Parse(context.Request["interes"]),
                        SaldoOrig = long.Parse(context.Request["saldoorig"]),
                        SaldoActual = long.Parse(context.Request["saldoactual"]),
                        CedCliente = context.Request["cedcliente"],
                        CedAsesor = context.Request["cedasesor"],
                        Numero = int.Parse(context.Request["numero"])
                    };
                    operations.UpdatePrestamo(pres);
                    WriteResponse("ok");
                }
                #endregion
                #region Tarjeta
                else if (request_instance == "tarjeta")
                {
                    BancaTec.Tarjeta tarj = new BancaTec.Tarjeta
                    {
                        CodigoSeg = context.Request["codigoseg"],
                        FechaExp = DateTime.Parse(context.Request["fechaexp"]),
                        Saldo = long.Parse(context.Request["saldo"]),
                        Tipo = context.Request["tipo"],
                        NumCuenta = int.Parse(context.Request["numcuenta"]),
                        Numero = int.Parse(context.Request["numero"])
                    };
                    operations.UpdateTarjeta(tarj);
                    WriteResponse("ok");
                }
                #endregion
                #region Rol
                else if (request_instance == "rol")
                {
                    BancaTec.Rol rol = new BancaTec.Rol
                    {
                        Nombre = context.Request["nombre"],
                        Descripcion = context.Request["descripcion"]
                    };
                    operations.UpdateRol(rol);
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
