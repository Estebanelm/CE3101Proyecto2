using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BancaTec;
using System.Net;

namespace RestWebService
{
    public class Service : IHttpHandler
    {
        #region Private Members

        //Estos miembros están presentes para evitar repetición de declaracione dentro de las funciones
        private BancaTec.Asesor ase;
        private BancaTec.Cliente clie;
        private BancaTec.Cuenta cuen;
        private BancaTec.Prestamo prest;
        private BancaTec.Tarjeta tarje;
        private BancaTec.Rol rol;
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
        private void READ(HttpContext context, string request_instance)
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
                        List<BancaTec.Asesor> lista_asesores = Asesor.GetAsesores();
                        if (lista_asesores.Count == 0)
                        {
                            WriteResponse("No se encontraron Asesores");
                        }
                        else
                        {
                            string serializedList = Serialize(lista_asesores);
                            context.Response.ContentType = "text/xml";
                            WriteResponse(serializedList);
                        }
                    }
                    else //si hay parámetro cedula, obtener solo 1 empleado
                    {
                        string _cedula = _cedula_temp;
                        ase = Asesor.GetAsesor(_cedula);
                        if (ase == null)
                        {
                            context.Response.Write("No Asesor Found: " + context.Request["cedula"]);
                        }
                        else
                        {
                            string serializedAsesor = Serialize(ase);
                            context.Response.ContentType = "text/xml";
                            WriteResponse(serializedAsesor);
                        }
                    }
                }
                #endregion
                #region Cliente
                else if (request_instance == "cliente")
                {
                    string cedula_temp = context.Request["cedula"];
                    if (cedula_temp == null)
                    {
                        List<BancaTec.Cliente> lista_clientes = Cliente.GetClientes();
                        if (lista_clientes.Count == 0)
                        {
                            WriteResponse("No se encontraron clientes");
                        }
                        else
                        {
                            string serializedList = Serialize(lista_clientes);
                            context.Response.ContentType = "text/xml";
                            WriteResponse(serializedList);
                        }

                    }
                    else
                    {
                        string _cedula = cedula_temp;
                        clie = Cliente.GetCliente(_cedula);
                        if (clie == null)
                        {
                            context.Response.Write("No Cliente Found: " + context.Request["cedula"]);
                        }
                        else
                        {
                            string serializedCliente = Serialize(clie);
                            context.Response.ContentType = "text/xml";
                            WriteResponse(serializedCliente);
                        }
                    }
                }
                #endregion
                #region Cuenta
                else if (request_instance == "cuenta")
                {
                    string num_temp = context.Request["numcuenta"];
                    string cedCliente_temp = context.Request["cedcliente"];
                    if (num_temp == null)
                    {
                        List<BancaTec.Cuenta> lista_cuentas = new List<Cuenta>();
                        if (cedCliente_temp == null)
                        {
                            lista_cuentas = Cuenta.GetCuentas();
                        }
                        else
                        {
                            lista_cuentas = Cuenta.GetCuentas(cedCliente_temp);
                        }
                        if (lista_cuentas.Count == 0)
                        {
                            WriteResponse("No se encontraron cuentas");
                        }
                        else
                        {
                            string serializedList = Serialize(lista_cuentas);
                            context.Response.ContentType = "text/xml";
                            WriteResponse(serializedList);
                        }
                    }
                    else
                    {
                        int _num = int.Parse(num_temp);

                        //HTTP Request Type - GET"
                        //Performing Operation - READ"
                        cuen = Cuenta.GetCuenta(_num);
                        if (cuen == null)
                        {
                            context.Response.Write("No Cuenta Found: " + num_temp);
                        }
                        else
                        {
                            string serializedCuenta = Serialize(cuen);
                            context.Response.ContentType = "text/xml";
                            WriteResponse(serializedCuenta);
                        }
                    }
                }
                #endregion
                #region Pago
                else if (request_instance == "pago")
                {
                    string num_prestamo_temp = context.Request["numprestamo"];
                    string ced_cliente_temp = context.Request["cedcliente"];
                    List<BancaTec.Pago> lista_pagos = new List<BancaTec.Pago>();
                    if (num_prestamo_temp == null && ced_cliente_temp != null)
                    {
                        lista_pagos = Pago.GetPagos(ced_cliente_temp, "cliente");
                    }
                    else if (num_prestamo_temp != null)
                    {
                        lista_pagos = Pago.GetPagos(num_prestamo_temp, "prestamo");
                    }
                    else
                    {
                        lista_pagos = Pago.GetPagos();
                    }
                    if (lista_pagos.Count == 0)
                    {
                        context.Response.Write("No Pagos Found");
                    }
                    else
                    {
                        string serializedList = Serialize(lista_pagos);
                        context.Response.ContentType = "text/xml";
                        WriteResponse(serializedList);
                    }
                }
                #endregion
                #region Prestamo
                else if (request_instance == "prestamo")
                {
                    string num_temp = context.Request["numero"];
                    if (num_temp == null)
                    {
                        List<BancaTec.Prestamo> lista_prestamos = Prestamo.GetPrestamos();
                        if (lista_prestamos.Count == 0)
                        {
                            WriteResponse("No se encontraron Prestamos");
                        }
                        else
                        {
                            string serializedList = Serialize(lista_prestamos);
                            context.Response.ContentType = "text/xml";
                            WriteResponse(serializedList);
                        }

                    }
                    else
                    {
                        int _num = int.Parse(num_temp);
                        prest = Prestamo.GetPrestamo(_num);
                        if (prest == null)
                        {
                            context.Response.Write(_num + "No Cuenta Found" + num_temp);
                        }
                        else
                        {
                            string serializedPrestamo = Serialize(prest);
                            context.Response.ContentType = "text/xml";
                            WriteResponse(serializedPrestamo);
                        }
                    }
                }
                #endregion
                #region Tarjeta
                else if (request_instance == "tarjeta")
                {
                    string _numtemp = context.Request["numero"];
                    if (_numtemp == null)
                    {
                        List<BancaTec.Tarjeta> lista_tarjetas = Tarjeta.GetTarjetas();
                        if (lista_tarjetas.Count == 0)
                        {
                            WriteResponse("No se encontraron tarjetas");
                        }
                        else
                        {
                            string serializedList = Serialize(lista_tarjetas);
                            context.Response.ContentType = "text/xml";
                            WriteResponse(serializedList);
                        }
                    }
                    else
                    {
                        int _num = int.Parse(_numtemp);
                        //HTTP Request Type - GET"
                        //Performing Operation - READ"
                        tarje = Tarjeta.GetTarjeta(_num);
                        if (tarje == null)
                        {
                            context.Response.Write(_num + "No Tarjeta Found" + _numtemp);
                        }
                        else
                        {
                            string serializedTarjeta = Serialize(tarje);
                            context.Response.ContentType = "text/xml";
                            WriteResponse(serializedTarjeta);
                        }
                    }
                }
                #endregion
                #region Rol
                else if (request_instance == "rol")
                {
                    string nombretemp = context.Request["nombre"];
                    if (nombretemp == null)
                    {
                        List<BancaTec.Rol> lista_roles = Rol.GetRoles();
                        if (lista_roles.Count == 0)
                        {
                            WriteResponse("No se encontraron roles");
                        }
                        else
                        {
                            string serializedList = Serialize(lista_roles);
                            context.Response.ContentType = "text/xml";
                            WriteResponse(serializedList);
                        }

                    }
                    else
                    {
                        string nombre = nombretemp;

                        //HTTP Request Type - GET"
                        //Performing Operation - READ"
                        rol = Rol.GetRol(nombre);
                        if (rol == null)
                        {
                            context.Response.Write("No Rol Found");
                        }
                        else
                        {
                            string serializedRol = Serialize(rol);
                            context.Response.ContentType = "text/xml";
                            WriteResponse(serializedRol);
                        }
                    }
                }
                #endregion
                #region Compra
                else if (request_instance == "compra")
                {
                    string numtarjetemp = context.Request["numtarjeta"];
                    if (numtarjetemp == null)
                    {
                        List<BancaTec.Compra> lista_compras = Compra.GetCompras();
                        if (lista_compras.Count == 0)
                        {
                            WriteResponse("No se encontraron compras");
                        }
                        else
                        {
                            string serializedList = Serialize(lista_compras);
                            context.Response.ContentType = "text/xml";
                            WriteResponse(serializedList);
                        }
                    }
                    else
                    {
                        int numtarjeta = int.Parse(numtarjetemp);
                        //HTTP Request Type - GET"
                        //Performing Operation - READ"
                        List<BancaTec.Compra> compra = Compra.GetCompras(numtarjeta);
                        if (compra.Count == 0)
                        {
                            context.Response.Write(numtarjeta + "No Compras Found" + numtarjetemp);
                        }
                        else
                        {
                            string serializedRol = Serialize(compra);
                            context.Response.ContentType = "text/xml";
                            WriteResponse(serializedRol);
                        }
                    }
                }
                #endregion
                #region Empleado
                else if (request_instance == "empleado")
                {
                    string cedulatemp = context.Request["cedula"];
                    if (cedulatemp == null)
                    {
                        List<BancaTec.Empleado> lista_empleados = Empleado.GetEmpleados();
                        if (lista_empleados.Count == 0)
                        {
                            WriteResponse("No se encontraron empleados");
                        }
                        else
                        {
                            string serializedList = Serialize(lista_empleados);
                            context.Response.ContentType = "text/xml";
                            WriteResponse(serializedList);
                        }
                    }
                    else
                    {
                        string cedula = cedulatemp;

                        //HTTP Request Type - GET"
                        //Performing Operation - READ"
                        BancaTec.Empleado empleado = Empleado.GetEmpleado(cedula);
                        if (empleado == null)
                        {
                            context.Response.Write(cedula + "No Rol Found" + cedulatemp);
                        }
                        else
                        {
                            EmpleadoRol emplerol = EmpleadoRol.GetEmpleadoRol(cedula);
                            string serializedemplerol = Serialize(emplerol);
                            string serializedRol = Serialize(empleado);
                            context.Response.ContentType = "text/xml";
                            WriteResponse(serializedRol+serializedemplerol);
                        }
                    }
                }
                #endregion
                #region Movimiento
                else if (request_instance == "movimiento")
                {
                    string numcuentatemp = context.Request["numcuenta"];
                    if (numcuentatemp == null)
                    {
                        List<BancaTec.Movimiento> lista_movim = Movimiento.GetMovimientos();
                        if (lista_movim.Count == 0)
                        {
                            WriteResponse("No se encontraron movimientos");
                        }
                        else
                        {
                            string serializedList = Serialize(lista_movim);
                            context.Response.ContentType = "text/xml";
                            WriteResponse(serializedList);
                        }
                    }
                    else
                    {
                        int numcuenta = int.Parse(numcuentatemp);
                        List<BancaTec.Movimiento> movimiento = Movimiento.GetMovimientos(numcuenta);
                        if (movimiento.Count == 0)
                        {
                            context.Response.Write(numcuenta + "No Movimientos Found" + numcuentatemp);
                        }
                        else
                        {
                            string serializedRol = Serialize(movimiento);
                            context.Response.ContentType = "text/xml";
                            WriteResponse(serializedRol);
                        }
                    }
                }
                #endregion
                #region Transferencia
                else if (request_instance == "transferencia")
                {
                    string numcuentaemitemp = context.Request["cuenta"];
                    if (numcuentaemitemp == null)
                    {
                        WriteResponse("Faltan detalles");
                    }
                    else
                    {
                        int numcuentaemi = int.Parse(numcuentaemitemp);
                        //HTTP Request Type - GET"
                        //Performing Operation - READ"
                        List<BancaTec.Transferencia> transferencia = Transferencia.GetTransferencias(numcuentaemi);
                        if (transferencia.Count == 0)
                        {
                            context.Response.Write("No se encontraron transferencias");
                        }
                        else
                        {
                            string serializedRol = Serialize(transferencia);
                            context.Response.ContentType = "text/xml";
                            WriteResponse(serializedRol);
                        }
                    }
                }
                #endregion

                #region Reporte Comisiones
                else if (request_instance == "comisiones")
                {
                    List<Operations.Comision> listaComisiones = operations.ReporteComisiones();
                    string serializedRol = Serialize(listaComisiones);
                    context.Response.ContentType = "text/xml";
                    WriteResponse(serializedRol);
                }
                #endregion
                #region Mora sin fecha
                else if (request_instance == "mora")
                {
                    string cedula = context.Request["cedula"];
                    if (cedula == null)
                    {
                        WriteResponse("Datos incorrectos, ingrese parámetro cedula");
                    }
                    else
                    {
                        List<Operations.Mora> listaMoras = operations.ReporteDeMora(cedula);
                        string serializedList = Serialize(listaMoras);
                        WriteResponse(serializedList);
                    }
                }
                #endregion
                #region Mora con fecha
                else if (request_instance == "morafecha")
                {
                    string cedula = context.Request["cedula"];
                    if (cedula == null)
                    {
                        WriteResponse("Datos incorrectos, ingrese parámetro cedula");
                    }
                    else
                    {
                        List<Operations.MoraFechas> listaMoras = operations.ReporteDeMoraFechas(cedula);
                        string serializedList = Serialize(listaMoras);
                        WriteResponse(serializedList);
                    }
                }
                #endregion

                #region L3M
                else if (request_instance == "l3m")
                {
                    fromL3M(context);
                }
                #endregion
            }
            catch (Exception ex)
            {
                WriteResponse("Error al obtener instancia");
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
                    BancaTec.Asesor ase = new BancaTec.Asesor
                    {
                        Cedula = context.Request["cedula"],
                        FechaNac = DateTime.Parse(context.Request["fechanac"]),
                        Nombre = context.Request["nombre"],
                        SegNombre = context.Request["segnombre"],
                        PriApellido = context.Request["priapellido"],
                        SegApellido = context.Request["segapellido"],
                        MetaColones = decimal.Parse(context.Request["metacolones"]),
                        MetaDolares = decimal.Parse(context.Request["metadolares"])
                    };
                    Asesor.AddAsesor(ase);
                    WriteResponse("Asesor creado correctamente");
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
                        Ingreso = decimal.Parse(context.Request["ingreso"]),
                        Contrasena = context.Request["contrasena"],
                        Moneda = context.Request["moneda"]
                    };
                    Cliente.AddCliente(clie);
                    WriteResponse("Cliente creado correctamente");
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
                        CedCliente = context.Request["cedcliente"],
                        Saldo = decimal.Parse(context.Request["saldo"])
                    };
                    Cuenta.AddCuenta(cuen);
                    WriteResponse("Cuenta creada correctamente");
                }
                #endregion
                #region Pago
                else if (request_instance == "pago")
                {
                    BancaTec.Pago pag = new BancaTec.Pago
                    {
                        Monto = decimal.Parse(context.Request["monto"]),
                        NumPrestamo = int.Parse(context.Request["numprestamo"]),
                        Fecha = DateTime.Parse(context.Request["fecha"]),
                        CedCliente = context.Request["cedcliente"],
                        PagosRestantes = int.Parse(context.Request["pagosrestantes"])
                    };
                    Pago.AddPago(pag);
                    WriteResponse("Pago creado correctamente");
                }
                #endregion
                #region Prestamo
                else if (request_instance == "prestamo")
                {
                    BancaTec.Prestamo pres = new BancaTec.Prestamo
                    {
                        Interes = double.Parse(context.Request["interes"]),
                        SaldoOrig = decimal.Parse(context.Request["saldoorig"]),
                        SaldoActual = decimal.Parse(context.Request["saldoorig"]),
                        CedCliente = context.Request["cedcliente"],
                        CedAsesor = context.Request["cedasesor"],
                        Moneda = context.Request["moneda"]
                    };
                    Prestamo.AddPrestamo(pres);
                    WriteResponse("Prestamo creado correctamente");
                }
                #endregion
                #region Tarjeta
                else if (request_instance == "tarjeta")
                {
                    Random getrandom = new Random();
                    int random = getrandom.Next(0,999);
                    BancaTec.Tarjeta tarj = new BancaTec.Tarjeta
                    {
                        CodigoSeg = context.Request["codigoseg"].ToString() == "" ? random.ToString() : context.Request["codigoseg"],
                        FechaExp = DateTime.Parse(context.Request["fechaexp"]),
                        SaldoActual = context.Request["tipo"] == "credito" ? decimal.Parse(context.Request["saldo"]) : 0,
                        SaldoOrig = context.Request["tipo"] == "credito" ? decimal.Parse(context.Request["saldo"]) : 0,
                        Tipo = context.Request["tipo"],
                        NumCuenta = int.Parse(context.Request["numcuenta"])
                    };
                    Tarjeta.AddTarjeta(tarj);
                    WriteResponse("Tarjeta creada correctamente");
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
                    Rol.AddRol(rol);
                    WriteResponse("Rol creado correctamente");
                }
                #endregion
                #region Compra
                else if (request_instance == "compra")
                {
                    BancaTec.Compra compr = new BancaTec.Compra
                    {
                        NumTarjeta = int.Parse(context.Request["numtarjeta"]),
                        Comercio = context.Request["comercio"],
                        Monto = decimal.Parse(context.Request["monto"]),
                        Moneda = context.Request["moneda"],
                        Fecha = DateTime.Now
                    };
                    Compra.AddCompra(compr);
                    WriteResponse("Compra creada correctamente");
                }
                #endregion
                #region Empleado
                else if (request_instance == "empleado")
                {
                    Rol rol = Rol.GetRol(context.Request["rol"]);
                    if (rol == null)
                    {
                        WriteResponse("Rol indicado no existe o fue ingresado incorrectamente");
                    }
                    else
                    {
                        BancaTec.Empleado emple = new BancaTec.Empleado
                        {
                            Cedula = context.Request["cedula"],
                            Sucursal = context.Request["sucursal"].ToString() == "" ? null : context.Request["sucursal"],
                            Nombre = context.Request["nombre"],
                            SegNombre = context.Request["segnombre"],
                            PriApellido = context.Request["priapellido"],
                            SegApellido = context.Request["segapellido"],
                            Contrasena = context.Request["contrasena"]
                        };
                        Empleado.AddEmpleado(emple);
                        EmpleadoRol emperol = new EmpleadoRol
                        {
                            CedulaEmpledo = emple.Cedula,
                            NombreRol = rol.Nombre
                        };
                        EmpleadoRol.AddEmpleadoRol(emperol);
                        WriteResponse("Empleado agregado correctamente");
                    }
                }
                #endregion
                #region Movimiento
                else if (request_instance == "movimiento")
                {
                    BancaTec.Movimiento movi = new BancaTec.Movimiento
                    {
                        Tipo = context.Request["tipo"],
                        Fecha = DateTime.Parse(context.Request["fecha"]),
                        Moneda = context.Request["moneda"],
                        Monto = decimal.Parse(context.Request["monto"]),
                        NumCuenta = int.Parse(context.Request["numcuenta"])
                    };
                    Movimiento.AddMovimiento(movi);
                    WriteResponse("Movimiento creado correctamente");
                }
                #endregion
                #region Transferencialegacy
                else if (request_instance == "transferenciavieja")
                {
                    BancaTec.Transferencia transf = new BancaTec.Transferencia
                    {
                        Monto = decimal.Parse(context.Request["monto"]),
                        Fecha = DateTime.Parse(context.Request["fecha"]),
                        CuentaEmisora = int.Parse(context.Request["cuentaemisora"]),
                        CuentaReceptora = int.Parse(context.Request["cuentareceptora"]),
                        Moneda = context.Request["moneda"]
                    };
                    Transferencia.AddTransferencia(transf);
                    WriteResponse("Transferencia creada correctamente");
                }
                #endregion

                #region Transferencia
                else if (request_instance == "transferencia")
                {
                    string montotemp = context.Request["monto"];
                    string emisoratemp = context.Request["cuentaemisora"];
                    string receptoratemp = context.Request["cuentareceptora"];
                    string moneda = context.Request["moneda"];
                    string respuesta = operations.RealizarTransferencia(decimal.Parse(montotemp), int.Parse(emisoratemp), int.Parse(receptoratemp), moneda);
                    if (respuesta.Equals("ok"))
                    {
                        WriteResponse("Transferencia realizada correctamente");
                    }
                    else
                    {
                        WriteResponse(respuesta);
                    }
                }
                #endregion
                #region Calendario de Pagos
                else if (request_instance == "calendario")
                {
                    string numPrestamotemp = context.Request["numprestamo"];
                    string mesestemp = context.Request["meses"];
                    if (numPrestamotemp == null || mesestemp == null)
                    {
                        WriteResponse("Datos incorrectos o mal ingresados");
                    }
                    else
                    {
                        int numPrestamo = int.Parse(numPrestamotemp);
                        int meses = int.Parse(mesestemp);
                        string generado = operations.GenerarCalendarioPagos(numPrestamo, meses);
                        if (generado.Equals("ok"))
                        {
                            WriteResponse("Operacion completada");
                        }
                        else
                        {
                            WriteResponse(generado);
                        }
                    }
                }
                #endregion

                #region Pago Tarjeta
                else if (request_instance == "l3m")
                {
                    fromL3M(context);
                }
                #endregion
            }
            catch (Exception ex)
            {

                WriteResponse("Error al crear instancia");
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
                        SegApellido = context.Request["segapellido"],
                        MetaColones = decimal.Parse(context.Request["metacolones"]),
                        MetaDolares = decimal.Parse(context.Request["metadolares"])
                    };
                    Asesor.UpdateAsesor(ase);
                    WriteResponse("Asesor modificado correctamente");
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
                        Ingreso = decimal.Parse(context.Request["ingreso"]),
                        Contrasena = context.Request["contrasena"],
                        Moneda = context.Request["moneda"]
                    };
                    Cliente.UpdateCliente(clie);
                    WriteResponse("Cliente modificado correctamente");
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
                        CedCliente = context.Request["cedcliente"],
                        NumCuenta = int.Parse(context.Request["numcuenta"]),
                        Saldo = decimal.Parse(context.Request["saldo"])
                    };
                    Cuenta.UpdateCuenta(cuen);
                    WriteResponse("Cuenta modificada correctamente");
                }
                #endregion
                #region Pago
                else if (request_instance == "pago")
                {
                    BancaTec.Pago pag = new BancaTec.Pago
                    {
                        Monto = decimal.Parse(context.Request["monto"]),
                        NumPrestamo = int.Parse(context.Request["numprestamo"]),
                        Fecha = DateTime.Parse(context.Request["fecha"]),
                        CedCliente = context.Request["cedcliente"],
                        PagosRestantes = int.Parse(context.Request["pagosrestantes"]),
                        Estado = context.Request["estado"]
                    };
                    Pago.UpdatePago(pag);
                    WriteResponse("Pago modificado correctamente");
                }
                #endregion
                #region Prestamo
                else if (request_instance == "prestamo")
                {
                    BancaTec.Prestamo pres = new BancaTec.Prestamo
                    {
                        Interes = double.Parse(context.Request["interes"]),
                        SaldoOrig = decimal.Parse(context.Request["saldoorig"]),
                        SaldoActual = decimal.Parse(context.Request["saldoactual"]),
                        CedCliente = context.Request["cedcliente"],
                        CedAsesor = context.Request["cedasesor"],
                        Moneda = context.Request["moneda"]
                    };
                    Prestamo.UpdatePrestamo(pres);
                    WriteResponse("Prestamo modificado correctamente");
                }
                #endregion
                #region Tarjeta
                else if (request_instance == "tarjeta")
                {
                    BancaTec.Tarjeta tarj = new BancaTec.Tarjeta
                    {
                        CodigoSeg = context.Request["codigoseg"],
                        FechaExp = DateTime.Parse(context.Request["fechaexp"]),
                        SaldoActual = context.Request["tipo"] == "credito" ? decimal.Parse(context.Request["saldoActual"]) : 0,
                        SaldoOrig = context.Request["tipo"] == "credito" ? decimal.Parse(context.Request["saldoOrig"]) : 0,
                        Tipo = context.Request["tipo"],
                        NumCuenta = int.Parse(context.Request["numcuenta"]),
                        Numero = int.Parse(context.Request["numero"])
                    };
                    Tarjeta.UpdateTarjeta(tarj);
                    WriteResponse("Tarjeta modificada correctamente");
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
                    Rol.UpdateRol(rol);
                    WriteResponse("Rol modificado correctamente");
                }
                #endregion
                #region Compra
                else if (request_instance == "compra")
                {
                    BancaTec.Compra compr = new BancaTec.Compra
                    {
                        NumTarjeta = int.Parse(context.Request["numtarjeta"]),
                        Comercio = context.Request["comercio"],
                        Monto = decimal.Parse(context.Request["monto"]),
                        Moneda = context.Request["moneda"],
                        Id = int.Parse(context.Request["id"])
                    };
                    Compra.UpdateCompra(compr);
                    WriteResponse("Compra modificada correctamente");
                }
                #endregion
                #region Empleado
                else if (request_instance == "empleado")
                {
                    BancaTec.Empleado emple = new BancaTec.Empleado
                    {
                        Cedula = context.Request["cedula"],
                        Sucursal = context.Request["sucursal"] == "" ? null : context.Request["sucursal"],
                        Nombre = context.Request["nombre"],
                        SegNombre = context.Request["segnombre"],
                        PriApellido = context.Request["priapellido"],
                        SegApellido = context.Request["segapellido"],
                        Contrasena = context.Request["contrasena"]
                    };
                    Empleado.UpdateEmpleado(emple);
                    WriteResponse("Empleado modificado correctamente");
                }
                #endregion
                #region Movimiento
                else if (request_instance == "movimiento")
                {
                    BancaTec.Movimiento movi = new BancaTec.Movimiento
                    {
                        Tipo = context.Request["tipo"],
                        Fecha = DateTime.Parse(context.Request["fecha"]),
                        Moneda = context.Request["moneda"],
                        Monto = decimal.Parse(context.Request["monto"]),
                        NumCuenta = int.Parse(context.Request["numcuenta"]),
                        Id = int.Parse(context.Request["id"])
                    };
                    Movimiento.UpdateMovimiento(movi);
                    WriteResponse("Movimiento modificado correctamente");
                }
                #endregion
                #region Transferencia
                else if (request_instance == "transferencia")
                {
                    BancaTec.Transferencia transf = new BancaTec.Transferencia
                    {
                        Monto = decimal.Parse(context.Request["monto"]),
                        Fecha = DateTime.Parse(context.Request["fecha"]),
                        CuentaEmisora = int.Parse(context.Request["cuentaemisora"]),
                        CuentaReceptora = int.Parse(context.Request["cuentareceptora"]),
                        Moneda = context.Request["moneda"],
                        Id = int.Parse(context.Request["id"])
                    };
                    Transferencia.UpdateTransferencia(transf);
                    WriteResponse("Transferencia modificada correctamente");
                }
                #endregion
            }
            catch (Exception ex)
            {

                WriteResponse("Error al modificar instancia");
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
                #region Asesor
                if (request_instance == "asesor")
                {
                    string _cedula_temp = context.Request["cedula"];
                    Asesor.DeleteAsesor(_cedula_temp);
                    WriteResponse("Asesor eliminado");
                }
                #endregion
                #region Cliente
                if (request_instance == "cliente")
                {
                    string _cedula_temp = context.Request["cedula"];
                    Cliente.DeleteCliente(_cedula_temp);
                    WriteResponse("Cliente eliminado");
                }
                #endregion
                #region Cuenta
                if (request_instance == "cuenta")
                {
                    string _num_temp = context.Request["numcuenta"];
                    int _num = int.Parse(_num_temp);
                    Cuenta.DeleteCuenta(_num);
                    WriteResponse("Cuenta eliminado");
                }
                #endregion
                #region Pago
                if (request_instance == "pago")
                {
                    string fecha_temp = context.Request["fecha"];
                    string _numprest_temp = context.Request["numprestamo"];
                    DateTime fecha = DateTime.Parse(fecha_temp);
                    int _numprest = int.Parse(_numprest_temp);
                    Pago.DeletePago(_numprest, fecha);
                    WriteResponse("Pago eliminado");
                }
                #endregion
                #region Prestamo
                if (request_instance == "prestamo")
                {
                    string _numero_temp = context.Request["numero"];
                    int _numero = int.Parse(_numero_temp);
                    Prestamo.DeletePrestamo(_numero);
                    WriteResponse("Prestamo eliminado");
                }
                #endregion
                #region Tarjeta
                if (request_instance == "tarjeta")
                {
                    string _numero_temp = context.Request["numero"];
                    int _numero = int.Parse(_numero_temp);
                    Tarjeta.DeleteTarjeta(_numero);
                    WriteResponse("Tarjeta eliminada");
                }
                #endregion
                #region Rol
                if (request_instance == "rol")
                {
                    string _nombre_temp = context.Request["nombre"];
                    Rol.DeleteRol(_nombre_temp);
                    WriteResponse("Rol eliminado");
                }
                #endregion
                #region Compra
                if (request_instance == "compra")
                {
                    string numtarjetatemp = context.Request["id"]; ;
                    int numtarjeta = int.Parse(numtarjetatemp);
                    Compra.DeleteCompra(numtarjeta);
                    WriteResponse("Compra eliminado");
                }
                #endregion
                #region Empleado
                if (request_instance == "empleado")
                {
                    string cedula_temp = context.Request["cedula"];
                    Empleado.DeleteEmpleado(cedula_temp);
                    WriteResponse("Empleado eliminado");
                }
                #endregion
                #region Movimiento
                if (request_instance == "movimiento")
                {
                    string _numero_temp = context.Request["id"];
                    int _numero = int.Parse(_numero_temp);
                    Movimiento.DeleteMovimiento(_numero);
                    WriteResponse("Movimiento eliminado");
                }
                #endregion
                #region Transferencia
                if (request_instance == "transferencia")
                {
                    string cuentatemp = context.Request["id"];
                    int cuenta = int.Parse(cuentatemp);
                    Transferencia.DeleteTransferencia(cuenta);
                    WriteResponse("Transferencia eliminada");
                }
                #endregion
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("No se encontro instancia"))
                {
                    WriteResponse(ex.Message);
                }
                else
                {
                    WriteResponse("Error al eliminar instancia");
                }
                errHandler.ErrorMessage = operations.GetException();
                errHandler.ErrorMessage = ex.Message.ToString();
            }
        }

        #endregion

        #region Utility Functions

        private void fromL3M(HttpContext context)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement el = (XmlElement)doc.AppendChild(doc.CreateElement("respuesta"));
            string numerotarjeta = context.Request["numerotarjeta"];
            string numeroseguridad = context.Request["numeroseguridad"];
            string fechaexpiracion = context.Request["fechaexpiracion"];
            string monto = context.Request["monto"];
            string comercio = context.Request["comercio"];

            Tarjeta tarje = Tarjeta.GetTarjeta(int.Parse(numerotarjeta));
            if (tarje == null)
            {
                el.SetAttribute("mensaje", "Numero de tarjeta desconocido");
                Console.WriteLine(doc.OuterXml);
                WriteResponse(doc.OuterXml);
            }
            else if (tarje.CodigoSeg != numeroseguridad)
            {
                el.SetAttribute("mensaje", "Codigo de seguridad incorrecto");
                Console.WriteLine(doc.OuterXml);
                WriteResponse(doc.OuterXml);
            }
            else if (tarje.FechaExp != DateTime.Parse(fechaexpiracion))
            {
                el.SetAttribute("mensaje", "Fecha de expiracion incorrecta");
                Console.WriteLine(doc.OuterXml);
                WriteResponse(doc.OuterXml);
            }
            else
            {
                Cuenta cuenta = Cuenta.GetCuenta(tarje.NumCuenta);
                if (tarje.Tipo.Equals("Credito"))
                {
                    if (decimal.Parse(monto) > tarje.SaldoActual)
                    {
                        el.SetAttribute("mensaje", "No tiene suficiente crédito");
                        Console.WriteLine(doc.OuterXml);
                        WriteResponse(doc.OuterXml);
                    }
                    else
                    {
                        Compra comp = new Compra
                        {
                            NumTarjeta = int.Parse(numerotarjeta),
                            Comercio = comercio,
                            Monto = decimal.Parse(monto),
                            Moneda = cuenta.Moneda,
                            Fecha = DateTime.Now
                        };
                        Compra.AddCompra(comp);
                        el.SetAttribute("mensaje", "ok");
                        Console.WriteLine(doc.OuterXml);
                        WriteResponse(doc.OuterXml);
                    }
                }
                else
                {
                    if (decimal.Parse(monto) > cuenta.Saldo)
                    {
                        el.SetAttribute("mensaje", "No tiene suficiente crédito");
                        Console.WriteLine(doc.OuterXml);
                        WriteResponse(doc.OuterXml);
                    }
                    else
                    {
                        Compra comp = new Compra
                        {
                            NumTarjeta = int.Parse(numerotarjeta),
                            Comercio = comercio,
                            Monto = decimal.Parse(monto),
                            Moneda = cuenta.Moneda,
                            Fecha = DateTime.Now
                        };
                        Compra.AddCompra(comp);
                        el.SetAttribute("mensaje", "ok");
                        Console.WriteLine(doc.OuterXml);
                        WriteResponse(doc.OuterXml);
                    }
                }
            }
        }

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
