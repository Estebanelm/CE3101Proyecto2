using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BancaTec;

//namespace RestWebService
//{
//    public class Service:IHttpHandler
//    {
//        #region Private Members

//        //Estos miembros están presentes para evitar repetición de declaracione dentro de las funciones
//        private BancaTec.Asesor ase;
//        private BancaTec.Cliente clie;
//        private BancaTec.Cuenta cuen;
//        private BancaTec.Prestamo prest;
//        private BancaTec.Tarjeta tarje;
//        private BancaTec.Rol rol;
//        private Operations.Operations operations;
//        private string connString; //string con los parámetros de conexión hacia la base de datos
//        private ErrorHandler.ErrorHandler errHandler;

//        #endregion

//        #region Handler
//        bool IHttpHandler.IsReusable
//        {
//            get { throw new NotImplementedException(); }
//        }

//        //Método que procesa los request. Este método debe de existir para poder recibir
//        //las solicitudes desde los clientes.
//        void IHttpHandler.ProcessRequest(HttpContext context)
//        {
//            try
//            {                
//                string url = Convert.ToString(context.Request.Url);
//                string request_instance = url.Split('/').Last<String>().Split('?')[0]; //instance de la solicitud, ya sea empleado, sucursal, etc.
//                connString = Properties.Settings.Default.ConnectionString;
//                operations = new Operations.Operations(connString); //estas variables se deben inicializar acá para que se realice
//                errHandler = new ErrorHandler.ErrorHandler();       //cada vez que el cliente hace una solicitud

//                //Handling CRUD

//                switch (context.Request.HttpMethod)
//                {
//                    case "GET":
//                        {
//                            string isDelete = context.Request["Delete"]; //determina si existe el parámetro delete
//                            if (isDelete == null)                        // ya que no hay soporte a delete en chrome
//                            {
//                                READ(context, request_instance);    //si no es delete, haga el read
//                                break;
//                            }
//                            else if (isDelete == "1")                //si es delete, hace el borrado
//                            {
//                                DELETE(context, request_instance);
//                                break;
//                            }
//                            break;
//                        }
//                    case "POST":
//                        {
//                            //Perform CREATE Operation
//                            string isPut = context.Request["Put"];//determina si existe el parámetro put
//                            if (isPut == null)                    //debido a la falta de soporte por parte de Chrome
//                            {
//                                CREATE(context, request_instance); //hacer create porque es post
//                                break;
//                            }
//                            else if (isPut == "1")
//                            {
//                                UPDATE(context, request_instance); //hacer update porque es put
//                                break;
//                            }
//                            break;
//                        }
//                        //Casos legacy, ya que el soporte en chrome no existe para delete y put.
//                        //Estos metodos funcionan en Internet Explorer.
//                    //case "PUT":
//                        //Perform UPDATE Operation
//                        //UPDATE(context, request_instance);
//                        //break;
//                   // case "DELETE":
//                        //Perform DELETE Operation
//                        //DELETE(context, request_instance);
//                        //break;
//                    default:
//                        break;
//                }
                
//            }
//            catch (Exception ex)
//            {
                
//                errHandler.ErrorMessage = ex.Message.ToString();
//                context.Response.Write(errHandler.ErrorMessage);                
//            }
//        }

//        #endregion Handler

//        #region CRUD Functions
//        /// <summary>
//        /// GET Operation
//        /// Todos las regiones marcan un if que va a permitir determinar cuál operación hacer dentro de la base de datos.
//        /// De esta manera, todas tienen una funcionalidad casi igual y el código es muy parecido entre ellos, solamente
//        /// elige entre el método a llamar en la clase Operations.
//        /// </summary>
//        /// <param name="context"></param>
//        /// <param name="request_instance"></param>
//        private void READ( HttpContext context, string request_instance)
//        {
//            //HTTP Request - //http://server.com/virtual directory/empleado?id={id}
//            //http://localhost/RestWebService/empleado
//            //Formato de la forma de hacer el request
//            try
//            {
//                #region Asesor
//                if (request_instance == "asesor")
//                {
//                    string _cedula_temp = context.Request["cedula"]; //obtiene el valor del parámetro cedula
//                    if (_cedula_temp == null) //si no hay parámetro, obtener todos los empleados
//                    {
//                        List<BancaTec.Asesor> lista_asesores = Asesor.GetAsesores();
//                        string serializedList = Serialize(lista_asesores);
//                        context.Response.ContentType = "text/xml";
//                        WriteResponse(serializedList);
//                    }
//                    else //si hay parámetro cedula, obtener solo 1 empleado
//                    {
//                        string _cedula = _cedula_temp;
//                        ase = Asesor.GetAsesor(_cedula);
//                        if (ase == null)
//                            context.Response.Write("No Asesor Found: " + context.Request["cedula"]);

//                        string serializedAsesor = Serialize(ase);
//                        context.Response.ContentType = "text/xml";
//                        WriteResponse(serializedAsesor);
//                    }
//                }
//                #endregion

//                #region Cliente
//                else if (request_instance == "cliente")
//                {
//                    string cedula_temp = context.Request["cedula"];
//                    if (cedula_temp == null)
//                    {
//                        List<BancaTec.Cliente> lista_clientes = operations.GetClientes();
//                        string serializedList = Serialize(lista_clientes);
//                        context.Response.ContentType = "text/xml";
//                        WriteResponse(serializedList);

//                    }
//                    else
//                    {
//                        string _cedula = cedula_temp;
//                        clie = operations.GetSucursal(_cedula);
//                        if (clie == null)
//                            context.Response.Write(_cedula + "No Cliente Found" + context.Request["cedula"]);

//                        string serializedCliente = Serialize(clie);
//                        context.Response.ContentType = "text/xml";
//                        WriteResponse(serializedCliente);
//                    }
//                }
//                #endregion
//                #region Cuenta
//                else if (request_instance == "cuenta")
//                {
//                    string num_temp = context.Request["numcuenta"];
//                    if (num_temp == null)
//                    {
//                        List<BancaTec.Cuenta> lista_cuentas= operations.GetCuentas();
//                        string serializedList = Serialize(lista_cuentas);
//                        context.Response.ContentType = "text/xml";
//                        WriteResponse(serializedList);

//                    }
//                    else
//                    {
//                        int _num = int.Parse(num_temp);

//                        //HTTP Request Type - GET"
//                        //Performing Operation - READ"
//                        cuen = operations.GetCuenta(_num);
//                        if (cuen == null)
//                            context.Response.Write(_num + "No Cuenta Found" + num_temp);

//                        string serializedCuenta = Serialize(cuen);
//                        context.Response.ContentType = "text/xml";
//                        WriteResponse(serializedCuenta);
//                    }
//                }
//                #endregion
//                #region Pago
//                else if (request_instance == "cuenta")
//                {
//                    string num_prestamo_temp = context.Request["numprestamo"];
//                    string ced_cliente_temp = context.Request["cedcliente"];
//                    List<BancaTec.Pago> lista_pagos = new List<BancaTec.Pago>();
//                    if (num_prestamo_temp == null && ced_cliente_temp != null)
//                    {
//                        int ced_cliente = int.Parse(ced_cliente_temp);
//                        lista_pagos = operations.GetPagos(ced_cliente, "cliente");
//                    }
//                    else if (num_prestamo_temp !=null)
//                    {
//                        int num_prestamo = int.Parse(num_prestamo_temp);
//                        lista_pagos = operations.GetPagos(num_prestamo, "prestamo");
//                    }
//                    else
//                    {
//                        lista_pagos = operations.GetPagos();
//                    }
//                    if (lista_pagos == null)
//                        context.Response.Write("No Pagos Found");
//                    string serializedList = Serialize(lista_pagos);
//                    context.Response.ContentType = "text/xml";
//                    WriteResponse(serializedList);
//                }
//                #endregion
//                #region Prestamo
//                else if (request_instance == "prestamo")
//                {
//                    string num_temp = context.Request["numero"];
//                    if (num_temp == null)
//                    {
//                        List<BancaTec.Prestamo> lista_prestamos = operations.GetPrestamos();
//                        string serializedList = Serialize(lista_prestamos);
//                        context.Response.ContentType = "text/xml";
//                        WriteResponse(serializedList);

//                    }
//                    else
//                    {
//                        int _num = int.Parse(num_temp);

//                        //HTTP Request Type - GET"
//                        //Performing Operation - READ"
//                        prest = operations.GetPrestamo(_num);
//                        if (prest == null)
//                            context.Response.Write(_num + "No Cuenta Found" + num_temp);

//                        string serializedPrestamo = Serialize(prest);
//                        context.Response.ContentType = "text/xml";
//                        WriteResponse(serializedPrestamo);
//                    }
//                }
//                #endregion
//                #region Tarjeta
//                else if (request_instance == "tarjeta")
//                {
//                    string _numtemp = context.Request["numero"];
//                    if (_numtemp == null)
//                    {
//                        List<BancaTec.Tarjeta> lista_tarjetas = operations.GetTarjetas();
//                        string serializedList = Serialize(lista_tarjetas);
//                        context.Response.ContentType = "text/xml";
//                        WriteResponse(serializedList);

//                    }
//                    else
//                    {
//                        int _num = int.Parse(_numtemp);
//                        //HTTP Request Type - GET"
//                        //Performing Operation - READ"
//                        tarje = operations.GetTarjeta(_num);
//                        if (tarje == null)
//                            context.Response.Write(_num + "No Tarjeta Found" + _numtemp);

//                        string serializedTarjeta = Serialize(tarje);
//                        context.Response.ContentType = "text/xml";
//                        WriteResponse(serializedTarjeta);
//                    }
//                }
//                #endregion
//                #region Rol
//                else if (request_instance == "rol")
//                {
//                    string nombretemp = context.Request["nombre"];
//                    if (nombretemp == null)
//                    {
//                        List<BancaTec.Rol> lista_roles = operations.GetRoles();
//                        string serializedList = Serialize(lista_roles);
//                        context.Response.ContentType = "text/xml";
//                        WriteResponse(serializedList);

//                    }
//                    else
//                    {
//                        string nombre = nombretemp;

//                        //HTTP Request Type - GET"
//                        //Performing Operation - READ"
//                        rol = operations.GetRol(nombre);
//                        if (rol == null)
//                            context.Response.Write(nombre + "No Rol Found" + nombretemp);

//                        string serializedRol = Serialize(rol);
//                        context.Response.ContentType = "text/xml";
//                        WriteResponse(serializedRol);
//                    }
//                }
//                #endregion
//				#region Compra
//				else if (request_instance == "compra")
//                {
//                    string numtarjetemp = context.Request["numtarjeta"];
//                    if (numtarjetemp == null)
//                    {
//                        List<BancaTec.Compra> lista_compras = operations.GetCompras();
//                        string serializedList = Serialize(lista_compras);
//                        context.Response.ContentType = "text/xml";
//                        WriteResponse(serializedList);

//                    }
//                    else
//                    {
//                        int numtarjeta = int.Parse(numtarjetemp);

//                        //HTTP Request Type - GET"
//                        //Performing Operation - READ"
//                        List<BancaTec.Compra> compra = operations.GetCompras(numtarjeta);
//                        if (compra.Count == 0)
//                            context.Response.Write(numtarjeta + "No Compras Found" + numtarjetemp);

//                        string serializedRol = Serialize(compra);
//                        context.Response.ContentType = "text/xml";
//                        WriteResponse(serializedRol);
//                    }
//                }
//                #endregion
//                #region Empleado
//                else if (request_instance == "empleado")
//                {
//                    string cedulatemp = context.Request["cedula"];
//                    if (cedulatemp == null)
//                    {
//                        List<BancaTec.Empleado> lista_empleados = operations.GetEmpleados();
//                        string serializedList = Serialize(lista_empleados);
//                        context.Response.ContentType = "text/xml";
//                        WriteResponse(serializedList);

//                    }
//                    else
//                    {
//                        string cedula = cedulatemp;

//                        //HTTP Request Type - GET"
//                        //Performing Operation - READ"
//                        BancaTec.Empleado empleado = operations.GetEmpleado(cedula);
//                        if (empleado == null)
//                            context.Response.Write(cedula + "No Rol Found" + cedulatemp);

//                        string serializedRol = Serialize(empleado);
//                        context.Response.ContentType = "text/xml";
//                        WriteResponse(serializedRol);
//                    }
//                }
//                #endregion
//                #region Movimiento
//                else if (request_instance == "movimiento")
//                {
//                    string numcuentatemp = context.Request["numcuenta"];
//                    if (numcuentatemp == null)
//                    {
//                        List<BancaTec.Movimiento> lista_movim = operations.GetMovimientos();
//                        string serializedList = Serialize(lista_movim);
//                        context.Response.ContentType = "text/xml";
//                        WriteResponse(serializedList);

//                    }
//                    else
//                    {
//                        int numcuenta = int.Parse(numcuentatemp);

//                        //HTTP Request Type - GET"
//                        //Performing Operation - READ"
//                        List<BancaTec.Movimiento> movimiento = operations.GetMovimientos(numcuenta);
//                        if (movimiento.Count == 0)
//                            context.Response.Write(numcuenta + "No Movimientos Found" + numcuentatemp);

//                        string serializedRol = Serialize(movimiento);
//                        context.Response.ContentType = "text/xml";
//                        WriteResponse(serializedRol);
//                    }
//                }
//                #endregion
//                #region Transferencia
//                else if (request_instance == "transferencia")
//                {
//                    string numcuentaemitemp = context.Request["cuenta"];
//                    if (numcuentaemitemp == null)
//                    {
//                        WriteResponse("Faltan detalles");
//                    }
//                    else
//                    {
//                        int numcuentaemi = int.Parse(numcuentaemitemp);
//                        //HTTP Request Type - GET"
//                        //Performing Operation - READ"
//                        List<BancaTec.Transferencia> transferencia = operations.GetTransferencias(numcuentaemitemp);
//                        if (transferencia.Count == 0)
//                            context.Response.Write(numcuentaemi + "No Transferencias Found" + numcuentaemitemp);

//                        string serializedRol = Serialize(transferencia);
//                        context.Response.ContentType = "text/xml";
//                        WriteResponse(serializedRol);
//                    }
//                }
//                #endregion
//            }
//            catch (Exception ex)
//            {
//                WriteResponse(ex.Message.ToString());
//                errHandler.ErrorMessage = operations.GetException();
//                errHandler.ErrorMessage = ex.Message.ToString();                
//            }            
//        }
//        /// <summary>
//        /// POST Operation
//        /// Esta función consiste de los if que determinan cuál método llamar en la clase Operations.
//        /// Solamente se evaluará lo que el cliente solicita y se procede a llamar la función adecuada.
//        /// </summary>
//        /// <param name="context"></param>
//        /// <param name="request_instance"></param>
//        private void CREATE(HttpContext context, string request_instance)
//        {
//            try
//            {
//                #region Asesor
//                if (request_instance == "asesor")
//                {
//                    BancaTec.Asesor ase = new BancaTec.Asesor {
//                        Cedula = context.Request["cedula"],
//                        FechaNac = DateTime.Parse(context.Request["fechanac"]),
//                        Nombre = context.Request["nombre"],
//                        SegNombre = context.Request["segnombre"],
//                        PriApellido = context.Request["priapellido"],
//                        SegApellido = context.Request["segapellido"],
//                        MetaColones = decimal.Parse(context.Request["metacolones"]),
//                        MetaDolares = decimal.Parse(context.Request["metadolares"])};
//                    Asesor.AddAsesor(ase);
//                }
//                #endregion

//                #region Cliente
//                else if (request_instance == "cliente")
//                {
//                    BancaTec.Cliente clie = new BancaTec.Cliente
//                    {
//                        Nombre = context.Request["nombre"],
//                        SegundoNombre = context.Request["segundonombre"],
//                        PriApellido = context.Request["priapellido"],
//                        SegApellido = context.Request["segapellido"],
//                        Cedula = context.Request["cedula"],
//                        Tipo = context.Request["tipo"],
//                        Direccion = context.Request["direccion"],
//                        Telefono = context.Request["telefono"],
//                        Ingreso = decimal.Parse(context.Request["ingreso"]),
//                        Contrasena = context.Request["contrasena"]
//                    };
//                    operations.AddCliente(clie);
//                }
//                #endregion
//                #region Cuenta
//                else if (request_instance == "cuenta")
//                {
//                    BancaTec.Cuenta cuen = new BancaTec.Cuenta
//                    {
//                        Tipo = context.Request["tipo"],
//                        Moneda = context.Request["moneda"],
//                        Descripcion = context.Request["descripcion"],
//                        CedCliente = context.Request["cedcliente"],
//                        NumCuenta = int.Parse(context.Request["numcuenta"]),
//                        Saldo = decimal.Parse(context.Request["saldo"])
//                    };
//                    operations.AddCuenta(cuen);
//                }
//                #endregion
//                #region Pago
//                else if (request_instance == "pago")
//                {
//                    BancaTec.Pago pag = new BancaTec.Pago
//                    {
//                        Monto = decimal.Parse(context.Request["monto"]),
//                        NumPrestamo = int.Parse(context.Request["numprestamo"]),
//                        Fecha = DateTime.Parse(context.Request["fecha"]),
//                        CedCliente = context.Request["cedcliente"],
//                        PagosRestantes = int.Parse(context.Request["pagosrestantes"])
//                    };
//                    operations.AddPago(pag);
//                }
//                #endregion
//                #region Prestamo
//                else if (request_instance == "prestamo")
//                {
//                    BancaTec.Prestamo pres = new BancaTec.Prestamo
//                    {
//                        Interes = double.Parse(context.Request["interes"]),
//                        SaldoOrig = decimal.Parse(context.Request["saldoorig"]),
//                        SaldoActual = decimal.Parse(context.Request["saldoactual"]),
//                        CedCliente = context.Request["cedcliente"],
//                        CedAsesor = context.Request["cedasesor"],
//                        Moneda = context.Request["moneda"]
//                    };
//                    operations.AddPrestamo(pres);
//                }
//                #endregion
//                #region Tarjeta
//                else if (request_instance == "tarjeta")
//                {
//                    BancaTec.Tarjeta tarj = new BancaTec.Tarjeta
//                    {
//                        CodigoSeg = context.Request["codigoseg"],
//                        FechaExp = DateTime.Parse(context.Request["fechaexp"]),
//                        Saldo = context.Request["tipo"] == "credito" ? decimal.Parse(context.Request["saldo"]) : 0,
//                        Tipo = context.Request["tipo"],
//                        NumCuenta = int.Parse(context.Request["numcuenta"]),
//                        Numero = int.Parse(context.Request["numero"])
//                    };
//                    operations.AddTarjeta(tarj);
//                }
//                #endregion
//                #region Rol
//                else if (request_instance == "rol")
//                {
//                    BancaTec.Rol rol = new BancaTec.Rol
//                    {
//                        Nombre = context.Request["nombre"],
//                        Descripcion = context.Request["descripcion"]
//                    };
//                    operations.AddRol(rol);
//                }
//                #endregion
//                #region Compra
//                else if (request_instance == "compra")
//                {
//                    BancaTec.Compra compr = new BancaTec.Compra
//                    {
//                        NumTarjeta = int.Parse(context.Request["numtarjeta"]),
//                        Comercio = context.Request["comercio"],
//                        Monto = decimal.Parse(context.Request["monto"]),
//                        Moneda = context.Request["moneda"]
//                    };
//                    operations.AddCompra(compr);
//                }
//                #endregion
//                #region Empleado
//                else if (request_instance == "empleado")
//                {
//                    BancaTec.Empleado emple = new BancaTec.Empleado
//                    {
//                        Cedula = context.Request["cedula"],
//                        Nombre = context.Request["nombre"],
//                        SegNombre = context.Request["segnombre"],
//                        PriApellido = context.Request["priapellido"],
//                        SegApellido = context.Request["segapellido"],
//                        Contrasena = context.Request["contrasena"]
//                    };
//                    operations.AddEmpleado(emple);
//                }
//                #endregion
//                #region Movimiento
//                else if (request_instance == "movimiento")
//                {
//                    BancaTec.Movimiento movi = new BancaTec.Movimiento
//                    {
//                        Tipo = context.Request["tipo"],
//                        Fecha = DateTime.Parse(context.Request["fecha"]),
//                        Moneda = context.Request["moneda"],
//                        Monto = decimal.Parse(context.Request["monto"]),
//                        NumCuenta = int.Parse(context.Request["numcuenta"])
//                    };
//                    operations.AddMovimiento(movi);
//                }
//                #endregion
//                #region Transferencia
//                else if (request_instance == "transferencia")
//                {
//                    BancaTec.Transferencia transf = new BancaTec.Transferencia
//                    {
//                        Monto = decimal.Parse(context.Request["monto"]),
//                        Fecha = DateTime.Parse(context.Request["fecha"]),
//                        CuentaEmisora = int.Parse(context.Request["cuentaemisora"]),
//                        CuentaReceptora = int.Parse(context.Request["cuentareceptora"]),
//                        Moneda = context.Request["moneda"]
//                    };
//                    operations.AddTransferencia(transf);
//                }
//                #endregion
//            }
//            catch (Exception ex)
//            {

//                WriteResponse(ex.Message.ToString());
//                errHandler.ErrorMessage = operations.GetException();
//                errHandler.ErrorMessage = ex.Message.ToString();                
//            }
//        }
//        /// <summary>
//        /// PUT Operation
//        /// Al igual que en post, solamente se va a determinar el método a llamar en la clase Operations.
//        /// </summary>
//        /// <param name="context"></param>
//        /// <param name="request_instance"></param>
//        private void UPDATE(HttpContext context, string request_instance)
//        {           
//            try
//            {
//                #region Asesor
//                if (request_instance == "asesor")
//                {
//                    BancaTec.Asesor ase = new BancaTec.Asesor
//                    {
//                        Cedula = context.Request["cedula"],
//                        FechaNac = DateTime.Parse(context.Request["fechanac"]),
//                        Nombre = context.Request["nombre"],
//                        SegNombre = context.Request["segnombre"],
//                        PriApellido = context.Request["priapellido"],
//                        SegApellido = context.Request["segapellido"],
//                        MetaColones = decimal.Parse(context.Request["metacolones"]),
//                        MetaDolares = decimal.Parse(context.Request["metadolares"])
//                    };
//                    operations.UpdateAsesor(ase);
//                    context.Response.Write("Asesor Updated Sucessfully");
//                    WriteResponse("ok");
//                }
//                #endregion
//                #region Cliente
//                if (request_instance == "cliente")
//                {
//                    BancaTec.Cliente clie = new BancaTec.Cliente
//                    {
//                        Nombre = context.Request["nombre"],
//                        SegundoNombre = context.Request["segundonombre"],
//                        PriApellido = context.Request["priapellido"],
//                        SegApellido = context.Request["segapellido"],
//                        Cedula = context.Request["cedula"],
//                        Tipo = context.Request["tipo"],
//                        Direccion = context.Request["direccion"],
//                        Telefono = context.Request["telefono"],
//                        Ingreso = decimal.Parse(context.Request["ingreso"]),
//                        Contrasena = context.Request["contrasena"]
//                    };
//                    operations.UpdateSucursal(clie);
//                    WriteResponse("ok");
//                }
//                #endregion
//                #region Cuenta
//                else if (request_instance == "cuenta")
//                {
//                    BancaTec.Cuenta cuen = new BancaTec.Cuenta
//                    {
//                        Tipo = context.Request["tipo"],
//                        Moneda = context.Request["moneda"],
//                        Descripcion = context.Request["descripcion"],
//                        CedCliente = context.Request["cedcliente"],
//                        NumCuenta = int.Parse(context.Request["numcuenta"]),
//                        Saldo = decimal.Parse(context.Request["saldo"])
//                    };
//                    operations.UpdateCuenta(cuen);
//                    WriteResponse("ok");
//                }
//                #endregion
//                #region Pago
//                else if (request_instance == "pago")
//                {
//                    BancaTec.Pago pag = new BancaTec.Pago
//                    {
//                        Monto = decimal.Parse(context.Request["monto"]),
//                        NumPrestamo = int.Parse(context.Request["numprestamo"]),
//                        Fecha = DateTime.Parse(context.Request["fecha"]),
//                        CedCliente = context.Request["cedcliente"],
//                        PagosRestantes = int.Parse(context.Request["pagosrestantes"])
//                    };
//                    operations.UpdatePago(pag);
//                    WriteResponse("ok");
//                }
//                #endregion
//                #region Prestamo
//                else if (request_instance == "prestamo")
//                {
//                    BancaTec.Prestamo pres = new BancaTec.Prestamo
//                    {
//                        Interes = double.Parse(context.Request["interes"]),
//                        SaldoOrig = decimal.Parse(context.Request["saldoorig"]),
//                        SaldoActual = decimal.Parse(context.Request["saldoactual"]),
//                        CedCliente = context.Request["cedcliente"],
//                        CedAsesor = context.Request["cedasesor"],
//                        Moneda = context.Request["moneda"]
//                    };
//                    operations.UpdatePrestamo(pres);
//                    WriteResponse("ok");
//                }
//                #endregion
//                #region Tarjeta
//                else if (request_instance == "tarjeta")
//                {
//                    BancaTec.Tarjeta tarj = new BancaTec.Tarjeta
//                    {
//                        CodigoSeg = context.Request["codigoseg"],
//                        FechaExp = DateTime.Parse(context.Request["fechaexp"]),
//                        Saldo = context.Request["tipo"] == "credito" ? decimal.Parse(context.Request["saldo"]) : 0,
//                        Tipo = context.Request["tipo"],
//                        NumCuenta = int.Parse(context.Request["numcuenta"]),
//                        Numero = int.Parse(context.Request["numero"])
//                    };
//                    operations.UpdateTarjeta(tarj);
//                    WriteResponse("ok");
//                }
//                #endregion
//                #region Rol
//                else if (request_instance == "rol")
//                {
//                    BancaTec.Rol rol = new BancaTec.Rol
//                    {
//                        Nombre = context.Request["nombre"],
//                        Descripcion = context.Request["descripcion"]
//                    };
//                    operations.UpdateRol(rol);
//                    WriteResponse("ok");
//                }
//                #endregion
//                #region Compra
//                else if (request_instance == "compra")
//                {
//                    BancaTec.Compra compr = new BancaTec.Compra
//                    {
//                        NumTarjeta = int.Parse(context.Request["numtarjeta"]),
//                        Comercio = context.Request["comercio"],
//                        Monto = decimal.Parse(context.Request["monto"]),
//                        Moneda = context.Request["moneda"]
//                    };
//                    operations.UpdateCompra(compr);
//                    WriteResponse("ok");
//                }
//                #endregion
//                #region Empleado
//                else if (request_instance == "empleado")
//                {
//                    BancaTec.Empleado emple = new BancaTec.Empleado
//                    {
//                        Cedula = context.Request["cedula"],
//                        Nombre = context.Request["nombre"],
//                        SegNombre = context.Request["segnombre"],
//                        PriApellido = context.Request["priapellido"],
//                        SegApellido = context.Request["segapellido"],
//                        Contrasena = context.Request["contrasena"]
//                    };
//                    operations.UpdateEmpleado(emple);
//                    WriteResponse("ok");
//                }
//                #endregion
//                #region Movimiento
//                else if (request_instance == "movimiento")
//                {
//                    BancaTec.Movimiento movi = new BancaTec.Movimiento
//                    {
//                        Tipo = context.Request["tipo"],
//                        Fecha = DateTime.Parse(context.Request["fecha"]),
//                        Moneda = context.Request["moneda"],
//                        Monto = decimal.Parse(context.Request["monto"]),
//                        NumCuenta = int.Parse(context.Request["numcuenta"])
//                    };
//                    operations.UpdateMovimiento(movi);
//                    WriteResponse("ok");
//                }
//                #endregion
//                #region Transferencia
//                else if (request_instance == "transferencia")
//                {
//                    BancaTec.Transferencia transf = new BancaTec.Transferencia
//                    {
//                        Monto = decimal.Parse(context.Request["monto"]),
//                        Fecha = DateTime.Parse(context.Request["fecha"]),
//                        CuentaEmisora = int.Parse(context.Request["cuentaemisora"]),
//                        CuentaReceptora = int.Parse(context.Request["cuentareceptora"]),
//                        Moneda = context.Request["moneda"]
//                    };
//                    operations.UpdateTransferencia(transf);
//                    WriteResponse("ok");
//                }
//                #endregion
//            }
//            catch (Exception ex)
//            {

//                WriteResponse(ex.Message.ToString());
//                errHandler.ErrorMessage = operations.GetException();
//                errHandler.ErrorMessage = ex.Message.ToString();                
//            }
//        }
//        /// <summary>
//        /// DELETE Operation
//        /// Tiene la misma función que el método Get, por lo que funciona de la misma forma
//        /// </summary>
//        /// <param name="context"></param>
//        private void DELETE(HttpContext context, string request_instance)
//        {
//            try
//            {
//                #region Asesor
//                if (request_instance == "asesor")
//                {
//                    string _cedula_temp = context.Request["cedula"];
//                    operations.DeleteAsesor(_cedula_temp);
//                    WriteResponse("ok");
//                }
//                #endregion
//                #region Cliente
//                if (request_instance == "cliente")
//                {
//                    string _cedula_temp = context.Request["cedula"];
//                    operations.DeleteCliente(_cedula_temp);
//                    WriteResponse("ok");
//                }
//                #endregion
//                #region Cuenta
//                if (request_instance == "cuenta")
//                {
//                    string _num_temp = context.Request["numcuenta"];
//                    int _num = int.Parse(_num_temp);
//                    operations.DeleteCuenta(_num);
//                    WriteResponse("ok");
//                }
//                #endregion
//                #region Pago
//                if (request_instance == "pago")
//                {
//                    string _cedcliente_temp = context.Request["cedcliente"];
//                    string _numprest_temp = context.Request["numprestamo"];
//                    int _numprest = int.Parse(_numprest_temp);
//                    operations.DeletePago(_cedcliente_temp, _numprest);
//                    WriteResponse("ok");
//                }
//                #endregion
//                #region Prestamo
//                if (request_instance == "prestamo")
//                {
//                    string _numero_temp = context.Request["numero"];
//                    int _numero = int.Parse(_numero_temp);
//                    operations.DeletePrestamo(_numero);
//                    WriteResponse("ok");
//                }
//                #endregion
//                #region Tarjeta
//                if (request_instance == "tarjeta")
//                {
//                    string _numero_temp = context.Request["numero"];
//                    int _numero = int.Parse(_numero_temp);
//                    operations.DeleteTarjeta(_numero);
//                    WriteResponse("ok");
//                }
//                #endregion
//                #region Rol
//                if (request_instance == "rol")
//                {
//                    string _nombre_temp = context.Request["nombre"];
//                    operations.DeleteRol(_nombre_temp);
//                    WriteResponse("ok");
//                }
//                #endregion
//                #region Compra
//                if (request_instance == "compra")
//                {
//                    string numtarjetatemp = context.Request["numtarjeta"];;
//                    int numtarjeta = int.Parse(numtarjetatemp);
//                    operations.DeleteCompra(numtarjeta);
//                    WriteResponse("ok");
//                }
//                #endregion
//                #region Empleado
//                if (request_instance == "empleado")
//                {
//                    string cedula_temp = context.Request["cedula"];
//                    operations.DeleteEmpleado(cedula_temp);
//                    WriteResponse("ok");
//                }
//                #endregion
//                #region Movimiento
//                if (request_instance == "movimiento")
//                {
//                    string _numero_temp = context.Request["numcuenta"];
//                    int _numero = int.Parse(_numero_temp);
//                    operations.DeleteMovimiento(_numero);
//                    WriteResponse("ok");
//                }
//                #endregion
//                #region Transferencia
//                if (request_instance == "transferencia")
//                {
//                    string cuentatemp = context.Request["cuenta"];
//                    int cuenta = int.Parse(cuentatemp);
//                    operations.DeleteTransferencia(cuenta);
//                    WriteResponse("ok");
//                }
//                #endregion
//            }
//            catch (Exception ex)
//            {
                
//                WriteResponse(ex.Message.ToString());
//                errHandler.ErrorMessage = operations.GetException();
//                errHandler.ErrorMessage = ex.Message.ToString();                
//            }
//        }

//        #endregion

//        #region Utility Functions
//        /// <summary>
//        /// Devuelve un mensaje al cliente
//        /// </summary>
//        /// <param name="strMessage"></param>
//        private static void WriteResponse(string strMessage)
//        {
//            HttpContext.Current.Response.Write(strMessage);            
//        }

//        /// <summary>
//        /// Convierte una clase en un XML
//        /// </summary>
//        /// <param name="obj"></param>
//        /// <returns></returns>
//        private String Serialize<T>(T obj)
//        {
//            try
//            {
//                String XmlizedString = null;
//                XmlSerializer xs = new XmlSerializer(typeof(T));
//                //create an instance of the MemoryStream class since we intend to keep the XML string 
//                //in memory instead of saving it to a file.
//                MemoryStream memoryStream = new MemoryStream();
//                //XmlTextWriter - fast, non-cached, forward-only way of generating streams or files 
//                //containing XML data
//                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
//                //Serialize emp in the xmlTextWriter
//                xs.Serialize(xmlTextWriter, obj);
//                //Get the BaseStream of the xmlTextWriter in the Memory Stream
//                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
//                //Convert to array
//                XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
//                return XmlizedString;
//            }
//            catch (Exception ex)
//            {
//                errHandler.ErrorMessage = ex.Message.ToString();
//                throw;
//            }           

//        }

//        /// <summary>
//        /// To convert a Byte Array of Unicode values (UTF-8 encoded) to a complete String.
//        /// </summary>
//        /// <param name="characters">Unicode Byte Array to be converted to String</param>
//        /// <returns>String converted from Unicode Byte Array</returns>
//        private String UTF8ByteArrayToString(Byte[] characters)
//        {
//            UTF8Encoding encoding = new UTF8Encoding();
//            String constructedString = encoding.GetString(characters);
//            return (constructedString);
//        }

//        #endregion
//    }
//}
