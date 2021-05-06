///---------------------------------------------------------------------------------------------------------------------------------------------------------------
///   Namespace:        pide_api
///   Objeto:           WebServicesController
///   Descripcion:      Controlador sobre las operaciones de verificación a los servicios web
///   Autor:            
///---------------------------------------------------------------------------------------------------------------------------------------------------------------
///   Historia de modificaciones:
///   Requerimiento:         Autor:            Fecha:          Descripcion:
///   REQ2086 - RSIS002      cvalenciap		   10/01/2018      Adecuación del método VerificarWsDni a la nueva estructura del servicio web de la RENIEC
///---------------------------------------------------------------------------------------------------------------------------------------------------------------
using Newtonsoft.Json.Linq;
using pide_api.Models;
using pide_api.Services;
using pide_api.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace pide_api.Controllers
{
    [RoutePrefix("api/webservices")]
    public class WebservicesController : ApiController
    {
        

        // POST api/webservices/verificarWsRuc
        [Route("verificarWsRuc")]
        [HttpPost]
        public HttpResponseMessage VerificarWsRuc(JObject data)
        {
            try
            {
                Debug.Print("----- START ws verificarWsRuc ----");
                var ruc = "20543751589";

                var valid = true;
                var mensaje = ErrorMessagesUtil.SERVICIO_HABILITADO;

                ConsultaService service = new ConsultaService();
                service.VerificarConsultarRuc(ruc, out valid);

                if (!valid)
                {
                    mensaje = ErrorMessagesUtil.SERVICIO_INHABILITADO;
                }

                RestDataResponse response = new RestDataResponse(valid, RestDataResponse.STATUS_OK, mensaje);

                Debug.Print("----- END ws verificarWsRuc ----");
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                Debug.Print(e.StackTrace.ToString());
                var mensaje = ErrorMessagesUtil.SERVICIO_HABILITADO;
                bool notService = ErrorMessagesUtil.ErrorConexionServicio(e) == 3;
                if (notService)
                {
                    mensaje = ErrorMessagesUtil.SERVICIO_INHABILITADO;
                }
                RestDataResponse response = new RestDataResponse(!notService, RestDataResponse.STATUS_OK, mensaje);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }

        }

        // POST api/webservices/verificarWsTitularidad
        [Route("verificarWsTitularidad")]
        [HttpPost]
        public HttpResponseMessage VerificarWsTitularidad(JObject data)
        {
            try
            {
                Debug.Print("----- START ws verificarWsTitularidad ----");
                var tipo = "N";
                var nombre = "MARKO";
                var apPaterno = "PACHECO";
                var apMaterno = "GALVEZ";
                var razonSocial = "";

                var valid = true;
                var mensaje = ErrorMessagesUtil.SERVICIO_HABILITADO;

                ConsultaService service = new ConsultaService();
                service.VerificarConsultarTitularidad(tipo, nombre, apPaterno, apMaterno, razonSocial, out valid);

                if (!valid)
                {
                    mensaje = ErrorMessagesUtil.SERVICIO_INHABILITADO;
                }

                RestDataResponse response = new RestDataResponse(valid, RestDataResponse.STATUS_OK, mensaje);

                Debug.Print("----- END ws verificarWsTitularidad ----");
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                Debug.Print(e.StackTrace.ToString());
                Debug.Print(e.GetType().ToString());
                Debug.Print("Tipo de Error: " + ErrorMessagesUtil.ErrorConexionServicio(e));

                var mensaje = ErrorMessagesUtil.SERVICIO_HABILITADO;
                bool notService = ErrorMessagesUtil.ErrorConexionServicio(e) == 3;
                if (notService)
                {
                    mensaje = ErrorMessagesUtil.SERVICIO_INHABILITADO;
                }
                RestDataResponse response = new RestDataResponse(!notService, RestDataResponse.STATUS_OK, mensaje);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        // POST api/webservices/verificarWsDetalleTitularidad
        [Route("verificarWsDetalleTitularidad")]
        [HttpPost]
        public HttpResponseMessage VerificarWsDetalleTitularidad(JObject data)
        {
            try
            {
                Debug.Print("----- START ws verificarWsDetalleTitularidad ----");
                var zona = "09";
                var oficina = "01";
                var partida = "00028694";
                var registro = "22000";

                var valid = true;
                var mensaje = ErrorMessagesUtil.SERVICIO_HABILITADO;

                ConsultaService service = new ConsultaService();
                service.VerificarConsultarDetalleTitularidad(zona, oficina, partida, registro, out valid);

                if (!valid)
                {
                    mensaje = ErrorMessagesUtil.SERVICIO_INHABILITADO;
                }

                RestDataResponse response = new RestDataResponse(valid, RestDataResponse.STATUS_OK, mensaje);

                Debug.Print("----- END ws verificarWsDetalleTitularidad ----");
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                Debug.Print(e.StackTrace.ToString());
                var mensaje = ErrorMessagesUtil.SERVICIO_HABILITADO;
                bool notService = ErrorMessagesUtil.ErrorConexionServicio(e) == 3;
                if (notService)
                {
                    mensaje = ErrorMessagesUtil.SERVICIO_INHABILITADO;
                }
                RestDataResponse response = new RestDataResponse(!notService, RestDataResponse.STATUS_OK, mensaje);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        // POST api/webservices/verificarWsVigencia
        [Route("verificarWsVigencia")]
        [HttpPost]
        public HttpResponseMessage VerificarWsVigencia(JObject data)
        {
            try
            {
                Debug.Print("----- START ws verificarWsVigencia ----");
                var transaccion = "27040";
                var idImg = "8058884";
                var tipo = "FOLIO";
                var totalPag = "22";
                var pagRef = "21";
                var pag = "1";

                var valid = true;
                var mensaje = ErrorMessagesUtil.SERVICIO_HABILITADO;

                ConsultaService service = new ConsultaService();
                service.VerificarConsultarImagenVigenciaPoder(transaccion, idImg, tipo, totalPag, pagRef, pag, out valid);

                if (!valid)
                {
                    mensaje = ErrorMessagesUtil.SERVICIO_INHABILITADO;
                }

                RestDataResponse response = new RestDataResponse(valid, RestDataResponse.STATUS_OK, mensaje);

                Debug.Print("----- END ws verificarWsVigencia ----");
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                Debug.Print(e.StackTrace.ToString());
                var mensaje = ErrorMessagesUtil.SERVICIO_HABILITADO;
                bool notService = ErrorMessagesUtil.ErrorConexionServicio(e) == 3;
                if (notService)
                {
                    mensaje = ErrorMessagesUtil.SERVICIO_INHABILITADO;
                }
                RestDataResponse response = new RestDataResponse(!notService, RestDataResponse.STATUS_OK, mensaje);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }

        //REQ2086 - RSIS002 - INICIO
        // POST api/webservices/verificaWsDni
        [Route("verificarWsDni")]
        [HttpPost]
        public HttpResponseMessage VerificaWsDni(JObject data)
        {
            try
            {
                Debug.Print("----- START ws verificarWsDni ----");
                var dni = "06836030";
               
                var valid = true;
                var mensaje = ErrorMessagesUtil.SERVICIO_HABILITADO;

                ConsultaService service = new ConsultaService();
                service.VerificarConsultarDni(dni, out valid);

                if (!valid)
                {
                    mensaje = ErrorMessagesUtil.SERVICIO_INHABILITADO;
                }

                RestDataResponse response = new RestDataResponse(valid, RestDataResponse.STATUS_OK, mensaje);

                Debug.Print("----- END ws verificarWsDni ----");
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                Debug.Print(e.StackTrace.ToString());
                Debug.Print(e.GetType().ToString());
                Debug.Print("Tipo de Error: " + ErrorMessagesUtil.ErrorConexionServicio(e));

                var mensaje = ErrorMessagesUtil.SERVICIO_HABILITADO;
                bool notService = ErrorMessagesUtil.ErrorConexionServicio(e) == 3;
                if (notService)
                {
                    mensaje = ErrorMessagesUtil.SERVICIO_INHABILITADO;
                }
                RestDataResponse response = new RestDataResponse(!notService, RestDataResponse.STATUS_OK, mensaje);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }   
        }
		//REQ2086 - RSIS002 - FIN
    }
}
