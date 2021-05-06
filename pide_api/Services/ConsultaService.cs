///---------------------------------------------------------------------------------------------------------------------------------------------------------------
///   Namespace:        pide_api
///   Objeto:           ConsultasService
///   Descripcion:      Clase que realiza las operaciones de consumo de los servicios web
///   Autor:            
///---------------------------------------------------------------------------------------------------------------------------------------------------------------
///   Historia de modificaciones:
///   Requerimiento:         Autor:            Fecha:          Descripcion:
///   REQ2086                cvalenciap		   10/01/2018      Adecuación del método VerificarConsultarDni a la nueva estructura del servicio web de la RENIEC
///															   Adecuación del método ConsultarDni a la nueva estructura del servicio web de la RENIEC
///															   Creación del método ActualizarCredenciales que consume el servicio web de la RENIEC
///---------------------------------------------------------------------------------------------------------------------------------------------------------------
using pide_api.Models;
using pide_api.ServiceReference;
using pide_api.pe.gob.pide.ws;
using pide_api.pe.gob.pide.ws2;
using pide_api.pe.gob.pide.ws3;
using pide_api.Utils;
using System.Collections.Generic;
using System.Diagnostics;
using System;
//REQ2086 - INICIO
using pide_api.Pruebawebserv;
using System.Configuration;
using pide_api.Models.Results;
using CERBERO.Util;
//REQ2086 - FIN

namespace pide_api.Services
{
    public class ConsultaService
    {
        

        public IList<ConsultaRucResult> ConsultarPorRuc(string ruc, out string mensaje, out int codigoError)
        {
            IList<ConsultaRucResult> resultados = new List<ConsultaRucResult>();

            /* Conexion a SOAP */
            var soapDataClient = new ConsultaRucSoapBindingImplService_vs0();

            // Obtener datos de Ruc (getDatosPrincipales)
            var datosPrincipalesResultSearch = soapDataClient.getDatosPrincipales(ruc);

            // Obtener datos de Ruc (getDatosSecundarios)
            var datosSecundariosResultSearch = soapDataClient.getDatosSecundarios(ruc);

            // Obtener datos de Ruc (getDomicilioLegal)
            var domicilioLegalResultSearch = soapDataClient.getDomicilioLegal(ruc);

            var rawXml = ConsultaRucService.getRepLegalesXml(ruc);

            var repLegaleslResultSearch = BeanParserUtil.ConvertRsoFromXml(rawXml);

            if (datosSecundariosResultSearch.dds_numruc != null && datosSecundariosResultSearch.dds_numruc != "")
            {
                codigoError = 1;
                mensaje = ErrorMessagesUtil.OBTUVO_RESULTADO_BUSQUEDA;
                var result = XmlUtil.parseGetByRuc(datosPrincipalesResultSearch, datosSecundariosResultSearch, repLegaleslResultSearch, domicilioLegalResultSearch);
                resultados.Add(result);
            }
            else
            {
                codigoError = 1;
                mensaje = ErrorMessagesUtil.NO_OBTUVO_RESULTADO_BUSQUEDA;
            }

            return resultados;
        }

        public IList<ConsultaTitularidadResult> ConsultarTitularidad(string type, string name, string lastName, string motherLastName, string legalName, out string mensaje, out int codigoError)
        {
            IList<ConsultaTitularidadResult> resultados = new List<ConsultaTitularidadResult>();

            /* Conexion a SOAP */
            var soapDataClient = new PIDEWSServiceClient();

            // Obtener lista de titulos de bienes
            var buscarTitularidadesResultsSearch = soapDataClient.buscarTitularidad(type, lastName, motherLastName, name, legalName);

            if (buscarTitularidadesResultsSearch.Length > 0)
            {
                codigoError = 1;
                mensaje = ErrorMessagesUtil.OBTUVO_RESULTADO_BUSQUEDA;
                resultados = XmlUtil.parseGetTitularidadArray(buscarTitularidadesResultsSearch);
            }
            else
            {
                codigoError = 1;
                mensaje = ErrorMessagesUtil.NO_OBTUVO_RESULTADO_BUSQUEDA;
            }

            return resultados;
        }

        public IList<ConsultaDetalleTitularidadResult> ConsultarDetalleTitularidad(string zona, string oficina, string partida, string registro, out string mensaje, out int codigoError)
        {
            IList<ConsultaDetalleTitularidadResult> resultados = new List<ConsultaDetalleTitularidadResult>();

            /* Conexion a SOAP */
            var soapDataClient = new PIDEWSServiceClient();

            // Obtener detalle de titularidad
            var detalleTitularidadResultsSearch = soapDataClient.listarAsientos(zona, oficina, partida, registro);

            if (detalleTitularidadResultsSearch != null)
            {
                if (detalleTitularidadResultsSearch.transaccion != 0)
                {
                    codigoError = 1;
                    mensaje = ErrorMessagesUtil.OBTUVO_RESULTADO_BUSQUEDA;
                    var result = XmlUtil.parseGetDetalleTitularidad(detalleTitularidadResultsSearch);
                    resultados.Add(result);
                }
                else
                {
                    codigoError = 1;
                    mensaje = ErrorMessagesUtil.NO_OBTUVO_RESULTADO_BUSQUEDA;
                }
            }
            else
            {
                codigoError = 1;
                mensaje = ErrorMessagesUtil.NO_OBTUVO_RESULTADO_BUSQUEDA;
            }

            return resultados;
        }

        public string ConsultarImagenVigenciaPoder(string transaccion, string idImg, string tipo, string nroTotalPag, string nroPagRef, string pagina, out string mensaje, out int codigoError)
        {
            string result = "";

            /*Conexion a SOAP */
            var soapDataClient = new PIDEWSServiceClient();

            long trans, idImagen;
            trans = long.Parse(transaccion);
            idImagen = long.Parse(idImg);

            // Obtener imagen de poder
            var imageVigenciaPoderResultSearch = soapDataClient.verAsiento(trans, idImagen, tipo, nroTotalPag, nroPagRef, pagina);

            if (imageVigenciaPoderResultSearch != null)
            {
                codigoError = 1;
                mensaje = ErrorMessagesUtil.OBTUVO_RESULTADO_BUSQUEDA;
                result = XmlUtil.parseGetVigenciaPoder(imageVigenciaPoderResultSearch);
            }
            else
            {
                codigoError = 1;
                mensaje = ErrorMessagesUtil.NO_OBTUVO_RESULTADO_BUSQUEDA;
            }

            return result;
        }

        public oficinaRegistralBean[] ConsultarOficina()
        {
            /* Conexion a SOAP */
            var soapDataClient = new PIDEWSServiceClient();

            return soapDataClient.getOficinas();
        }

        

        public void VerificarConsultarRuc(string ruc, out bool valid)
        {
            try
            {
                //var soapAuthenticationClient = new WSAuthenticationService();
                //var resultTicket = soapAuthenticationClient.getTicket(Constants.SOAP_USER_REQUEST_DNI, Constants.SOAP_PASS_REQUEST_DNI);
                var results = ConsultaRucService.getRepLegalesXml(ruc);
                valid = true;
            }
            catch (Exception e)
            {
                valid = false;
                Debug.Print(e.StackTrace);
            }
        }

        public void VerificarConsultarTitularidad(string type,
            string name, string lastName, string motherLastName, string legalName,
            out bool valid)
        {
            try
            {
                //var soapAuthenticationClient = new WSAuthenticationService();
                //var resultTicket = soapAuthenticationClient.getTicket(Constants.SOAP_USER_REQUEST_DNI, Constants.SOAP_PASS_REQUEST_DNI);
                var soapDataClient = new PIDEWSServiceClient();
                var titularidad = soapDataClient.buscarTitularidad(type, lastName, motherLastName, name, legalName);

                if (titularidad.Length > 0)
                {
                    var first = titularidad[0];
                }
                valid = true;
            }
            catch (Exception e)
            {
                valid = false;
                Debug.Print(e.StackTrace);
            }
        }

        public void VerificarConsultarDetalleTitularidad(string zona,
            string oficina, string partida, string registro, out bool valid)
        {
            try
            {
                //var soapAuthenticationClient = new WSAuthenticationService();
                //var resultTicket = soapAuthenticationClient.getTicket(Constants.SOAP_USER_REQUEST_DNI, Constants.SOAP_PASS_REQUEST_DNI);
                var soapDataClient = new PIDEWSServiceClient();
                var listaAsientos = soapDataClient.listarAsientos(zona, oficina, partida, registro);
                valid = true;
            }
            catch (Exception e)
            {
                valid = false;
                Debug.Print(e.StackTrace);
            }
        }

        public void VerificarConsultarImagenVigenciaPoder(string transaccion,
            string idImg, string tipo, string nroTotalPag,
            string nroPagRef, string pagina, out bool valid)
        {
            try
            {
                //var soapAuthenticationClient = new WSAuthenticationService();
                //var resultTicket = soapAuthenticationClient.getTicket(Constants.SOAP_USER_REQUEST_DNI, Constants.SOAP_PASS_REQUEST_DNI);
                var soapDataClient = new PIDEWSServiceClient();

                long trans, idImagen;
                trans = long.Parse(transaccion);
                idImagen = long.Parse(idImg);
                var result = soapDataClient.verAsiento(trans, idImagen, tipo, nroTotalPag, nroPagRef, pagina);
                valid = true;
            }
            catch (Exception e)
            {
                valid = false;
                Debug.Print(e.StackTrace);
            }
        }

        //REQ2086 - RSIS002 - INICIO
        public IList<ConsultarDniResult> ConsultarDni(string dniConsult, string user, out string mensaje, out int codigoError)
        {
            //Resultado como lista de objetos
            IList<ConsultarDniResult> resultados = new List<ConsultarDniResult>();
            /*Conexión a SOAP*/
            var soapDataClient = new ReniecConsultaDniPortTypeClient();
            //Instanciación de petición de consumo de servicio web
            var aux = new peticionConsulta();
            var utilSP = new ExecSPUtil();
            //Obtener valores de base de datos
            var resultArray = utilSP.EjecGetCred(user);

            //En caso no encontrar el usuario 
            if (resultArray[0] == null && resultArray[1] == null && resultArray[2] == null && resultArray[3] == null)
            {
                codigoError = 1;
                mensaje = ErrorMessagesUtil.USUARIO_NO_REGISTRADO;
            }
            else
            {
                aux.nuDniConsulta = dniConsult;
                aux.nuDniUsuario = resultArray[2];
                aux.nuRucUsuario = ConfigurationManager.AppSettings["nuRucReniecService"];
                //Desencriptación
                if (resultArray[3] != null && resultArray[3] != "")
                {
                    aux.password = Encriptacion.DesEncriptarCadena(resultArray[3]);
                }
                else
                {
                    aux.password = resultArray[2];
                }
                //Consumo de servicio web
                var consultaDniResultSearch = soapDataClient.consultar(aux);

                if (consultaDniResultSearch != null)
                {
                    var valCod = consultaDniResultSearch.coResultado;
                    if (valCod == "0000")
                    {
                        codigoError = 0;
                        mensaje = consultaDniResultSearch.deResultado;
                        var result = XmlUtil.parseGetDni(consultaDniResultSearch.datosPersona);
                        resultados.Add(result);
                    }
                    else
                    {
                        codigoError = 1;
                        mensaje = consultaDniResultSearch.deResultado;//Resultado de consumo de servicio no satisfactorio
                    }
                }
                else
                {
                    codigoError = 0;
                    mensaje = ErrorMessagesUtil.NO_OBTUVO_RESULTADO_BUSQUEDA;//No se efectuo el consumo de servicio 
                }
            }
            return resultados;
        }

        public void VerificarConsultarDni(string dniConsult, out bool valid)
        {
            try
            {
                /*Conexión a SOAP*/
                var soapDataClient = new ReniecConsultaDniPortTypeClient();
                var aux = new peticionConsulta();
                aux.nuDniConsulta = dniConsult;
                aux.nuDniUsuario = Constants.SOAP_USER_REQUEST_DNI;

                string nuRucReniecService = ConfigurationManager.AppSettings["nuRucReniecService"];
                aux.nuRucUsuario = nuRucReniecService;
                aux.password = Constants.SOAP_PASS_REQUEST_DNI;

                var consultaDniResultSearch = soapDataClient.consultar(aux);

                if (consultaDniResultSearch != null)
                {
                    var datos = consultaDniResultSearch.datosPersona;
                    var codMensaje = consultaDniResultSearch.coResultado;
                    var desMensaje = consultaDniResultSearch.deResultado;
                }
                valid = true;
            }
            catch (Exception e)
            {
                valid = false;
                Debug.Print(e.StackTrace);
            }
        }
		//REQ2086 - RSIS002 - FIN

		//REQ2086 - RSIS001 - INICIO
		public IList<ActCredResult> ActualizarCredenciales(string user, string credAnt, string credNew, out string mensaje, out int codigoError)
        {
            //Resultado como lista de objetos 
            IList<ActCredResult> resultados = new List<ActCredResult>();
            /*Conexión a SOAP*/
            var soapDataClient = new ReniecConsultaDniPortTypeClient();
            //Instanciacion de petición de consumo de servicio web
            var aux = new peticionActualizarCredencial();
            var utilSP = new ExecSPUtil();
            //Obtener valores de base de datos
            var resultArray = utilSP.EjecGetCred(user);
            string credNoEncript;
            
            //Usuario no encontrado en BD 
            if (resultArray[0] == null && resultArray[1] == null && resultArray[2] == null && resultArray[3] == null)
            {
                codigoError = 1;
                mensaje = ErrorMessagesUtil.USUARIO_NO_REGISTRADO;
            }
            else
            {
                //Desencriptación
                if (resultArray[3] != null && resultArray[3] != "")
                {
                    credNoEncript = Encriptacion.DesEncriptarCadena(resultArray[3]);
                }
                else {
                    credNoEncript = resultArray[2];
                }
                //Ejecución 
                if (credNoEncript == credAnt)
                {
                    aux.credencialAnterior = credAnt;
                    aux.credencialNueva = credNew;
                    aux.nuDni = resultArray[2];
                    aux.nuRuc = ConfigurationManager.AppSettings["nuRucReniecService"];
                    //Consumo de servicio web
                    var actualizarCred = soapDataClient.actualizarcredencial(aux);

                    if (actualizarCred != null)
                    {
                        if (actualizarCred.coResultado == "0000")
                        {
                            //Encriptación
                            var credEncript = Encriptacion.EncriptarCadena(credNew);
                            var credAntEncript = Encriptacion.EncriptarCadena(credAnt);
                            string resultAct = utilSP.EjectActCred(user, credAntEncript, credEncript);
                            if (resultAct.Equals("Y"))
                            {
                                codigoError = 0;
                                mensaje = actualizarCred.deResultado;
                                var result = XmlUtil.parseActCred(actualizarCred.coResultado, mensaje);
                                resultados.Add(result);
                            }
                            else
                            {
                                codigoError = 1;
                                mensaje = ErrorMessagesUtil.NO_ACTUALIZO_BD;//Error critico: se realiza consumo pero no procedimiento almacenado
                            }
                        }
                        else {
                            codigoError = 1;
                            mensaje = actualizarCred.deResultado;//Resultado de consumo de servicio no satisfactorio
                        }
                    }
                    else
                    {
                        codigoError = 1;
                        mensaje = ErrorMessagesUtil.NO_OBTUVO_RESULTADO_ACTUALIZACION;//No se efectuo el consumo del servicio 
                    }
                }
                else
                {
                    codigoError = 1;
                    mensaje = ErrorMessagesUtil.CREDENCIALES_INCONSISTENTES;//Credenciales incorrectas
                }
            }
            return resultados;
        }
		//REQ2086 - RSIS001 - FIN
    }
}