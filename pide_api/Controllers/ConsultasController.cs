///---------------------------------------------------------------------------------------------------------------------------------------------------------------
///   Namespace:        pide_api
///   Objeto:           ConsultasController
///   Descripcion:      Controlador sobre las operaciones del módulo consultas
///   Autor:            
///---------------------------------------------------------------------------------------------------------------------------------------------------------------
///   Historia de modificaciones:
///   Requerimiento:         Autor:            Fecha:          Descripcion:
///   REQ2086                cvalenciap		   10/01/2018      Adecuación del método getByDni a las caracteristicas de la nueva estructura de la consulta de DNi
///  														   Creación del método actNewCred para la respuesta a la operación actualizar credenciales
///---------------------------------------------------------------------------------------------------------------------------------------------------------------
using Newtonsoft.Json.Linq;
using pide_api.Models;
using pide_api.Services;
using pide_api.Utils;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace pide_api.Controllers
{
    [RoutePrefix("api/consultas")]
    public class ConsultasController : ApiController
    {
        

        /// <summary>
        /// Obtener informacion de un persona juridica por medio de su RUC
        /// </summary>
        /// <returns>Objeto de Resultado para Consulta RUC</returns> 
        // POST api/consultas/getByRuc
        [AuthorizeUser]
        [Route("getByRuc")]
        [HttpPost]
        public HttpResponseMessage GetByRuc(JObject data)
        {
            String token = AccountController.GetTokenHeader(Request);
            string mensaje;
            int codigoError;

            try
            {
                Debug.Print("----- START ws getByRuc ----");
                var ruc = data.GetValue("ruc").Value<string>();

                ConsultaService service = new ConsultaService();
                var results = service.ConsultarPorRuc(ruc, out mensaje, out codigoError);
                RestDataResponse response = new RestDataResponse(results, RestDataResponse.STATUS_OK, mensaje);

                Debug.Print("----- END ws getByRuc ----");
                CRUDConsultaDbUtil.SaveConsulta(token, Constants.CONSULTA_RUC_ID, data.ToString(), codigoError.ToString(), mensaje);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                Debug.Print(e.StackTrace.ToString());
                ErrorDataResponse error = new ErrorDataResponse(e.Message, e.StackTrace.ToString());
                CRUDConsultaDbUtil.SaveConsulta(token, Constants.CONSULTA_RUC_ID, data.ToString(), RestDataResponse.STATUS_ERROR.ToString(), "Error: " + error);
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessagesUtil.error());
            }
            
        }

        /// <summary>
        /// Obtener lista de titulos de propiedas para una persona natural o juricia
        /// </summary>
        /// <returns>Lista de titulos de propieda</returns> 
        // POST api/consultas/geTitularidad
        [AuthorizeUser]
        [Route("getTitularidad")]
        [HttpPost]
        public HttpResponseMessage GeTitularidad(JObject data)
        {
            String token = AccountController.GetTokenHeader(Request);
            string mensaje;
            int codigoError;

            try
            {
                Debug.Print("----- START ws geTitularidad ----");
                var tipo = data.GetValue("tipo").Value<string>().ToUpper();
                var nombre = data.GetValue("nombre").Value<string>().ToUpper();
                var apPaterno = data.GetValue("apPaterno").Value<string>().ToUpper();
                var apMaterno = data.GetValue("apMaterno").Value<string>().ToUpper();
                var razonSocial = data.GetValue("razonSocial").Value<string>().ToUpper();

                ConsultaService service = new ConsultaService();
                var results = service.ConsultarTitularidad(tipo, nombre, apPaterno, apMaterno, razonSocial, out mensaje, out codigoError);
                RestDataResponse response = new RestDataResponse(results, RestDataResponse.STATUS_OK, mensaje);

                Debug.Print("----- END ws geTitularidad ----");
                CRUDConsultaDbUtil.SaveConsulta(token, Constants.CONSULTA_TITULARIDAD_ID, data.ToString(), codigoError.ToString(), mensaje);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                Debug.Print(e.StackTrace.ToString());
                ErrorDataResponse error = new ErrorDataResponse(e.Message, e.StackTrace.ToString());
                CRUDConsultaDbUtil.SaveConsulta(token, Constants.CONSULTA_TITULARIDAD_ID, data.ToString(), RestDataResponse.STATUS_ERROR.ToString(), "Error: " + error);
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessagesUtil.error());
            }
        }

        /// <summary>
        /// Obtener Lista de Asientos para una titularidad determinada
        /// </summary>
        /// <returns>Lista de Asientos</returns> 
        // POST api/consultas/getAsientosTitularidad
        [AuthorizeUser]
        [Route("getAsientosTitularidad")]
        [HttpPost]
        public HttpResponseMessage GetAsientosTitularidad(JObject data)
        {
            String token = AccountController.GetTokenHeader(Request);
            string mensaje;
            int codigoError;

            try
            {
                Debug.Print("----- START ws getAsientosTitularidad ----");
                var zona = data.GetValue("zona").Value<string>();
                var oficina = data.GetValue("oficina").Value<string>();
                var partida = data.GetValue("partida").Value<string>();
                var registro = data.GetValue("registro").Value<string>();

                ConsultaService service = new ConsultaService();
                var results = service.ConsultarDetalleTitularidad(zona, oficina, partida, registro, out mensaje, out codigoError);
                RestDataResponse response = new RestDataResponse(results, RestDataResponse.STATUS_OK, mensaje);

                Debug.Print("----- END ws getAsientosTitularidad ----");
                CRUDConsultaDbUtil.SaveConsulta(token, Constants.CONSULTA_ASIENTOS_ID, data.ToString(), codigoError.ToString(), mensaje);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                Debug.Print(e.StackTrace.ToString());
                ErrorDataResponse error = new ErrorDataResponse(e.Message, e.StackTrace.ToString());
                CRUDConsultaDbUtil.SaveConsulta(token, Constants.CONSULTA_ASIENTOS_ID, data.ToString(), RestDataResponse.STATUS_ERROR.ToString(), "Error: " + error);
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessagesUtil.error());
            }
        }

        /// <summary>
        /// Obtener las oficinas
        /// </summary>
        /// <returns>Data de Imagen de Vigencia</returns> 
        // POST api/consultas/getOficinas
        [AuthorizeUser]
        [Route("getOficinas")]
        [HttpPost]
        public HttpResponseMessage GetOficinas()
        {
            String token = AccountController.GetTokenHeader(Request);
 
            try
            {
                ConsultaService service = new ConsultaService();
                var results = service.ConsultarOficina();

                RestDataResponse response = new RestDataResponse(results, RestDataResponse.STATUS_OK, "");
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                Debug.Print(e.StackTrace.ToString());
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessagesUtil.error());
            }
        }

        /// <summary>
        /// Obtener data de imagen de vigencia para un asiento determinado
        /// </summary>
        /// <returns>Data de Imagen de Vigencia</returns> 
        // POST api/consultas/getVigencia
        [AuthorizeUser]
        [Route("getVigencia")]
        [HttpPost]
        public HttpResponseMessage GetVigencia(JObject data)
        {
            String token = AccountController.GetTokenHeader(Request);
            string mensaje;
            int codigoError;

            try
            {
                Debug.Print("----- START ws getVigencia ----");
                var transaccion = data.GetValue("transaccion").Value<string>();
                var idImg = data.GetValue("id_img").Value<string>();
                var tipo = data.GetValue("tipo").Value<string>();
                var totalPag = data.GetValue("total_pag").Value<string>();
                var pagRef = data.GetValue("pag_ref").Value<string>();
                var pag = data.GetValue("nro_pag").Value<string>();

                ConsultaService service = new ConsultaService();
                var results = service.ConsultarImagenVigenciaPoder(transaccion, idImg, tipo, totalPag, pagRef, pag, out mensaje, out codigoError);

                RestDataResponse response = new RestDataResponse(results, RestDataResponse.STATUS_OK, mensaje);

                Debug.Print("----- END ws getVigencia ----");
                CRUDConsultaDbUtil.SaveConsulta(token, Constants.CONSULTA_VIGENCIA_ID, data.ToString(), codigoError.ToString(), mensaje);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                Debug.Print(e.StackTrace.ToString());
                ErrorDataResponse error = new ErrorDataResponse(e.Message, e.StackTrace.ToString());
                CRUDConsultaDbUtil.SaveConsulta(token, Constants.CONSULTA_VIGENCIA_ID, data.ToString(), RestDataResponse.STATUS_ERROR.ToString(), "Error: " + error);
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessagesUtil.error());
            }
        }

        //REQ2086 - RSIS002 - INICIO

        // POST api/consultas/getByDni
        [AuthorizeUser]
        [Route("getByDni")]
        [HttpPost]
        public HttpResponseMessage GetByDni(JObject data)
        {
            String token = AccountController.GetTokenHeader(Request);
            string mensaje;
            int codigoError;

            try
            {
                Debug.Print("----- START ws getByDni ----");
                var dni = data.GetValue("dni").Value<string>();
                var user = data.GetValue("user").Value<string>();

                ConsultaService service = new ConsultaService();
                var results = service.ConsultarDni(dni, user, out mensaje, out codigoError);

                RestDataResponse response = new RestDataResponse(results, RestDataResponse.STATUS_OK, mensaje);

                Debug.Print("----- END ws getByDni ----");
                CRUDConsultaDbUtil.SaveConsulta(token, Constants.CONSULTA_DNI_ID, data.ToString(), codigoError.ToString(), mensaje);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                Debug.Print(e.StackTrace.ToString());
                CRUDConsultaDbUtil.SaveConsulta(token, Constants.CONSULTA_DNI_ID, data.ToString(), RestDataResponse.STATUS_ERROR.ToString(), "Error: " + e.Message);
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessagesUtil.error());
            }
        }
		//REQ2086 - RSIS002 - INICIO

        //REQ2086 - RSIS001 - INICIO
		// POST api/consultas/actCred
        [AuthorizeUser]
        [Route("actNewCred")]
        [HttpPost]
        public HttpResponseMessage ActCred(JObject data)
        {
            String token = AccountController.GetTokenHeader(Request);
            string mensaje;
            int codigoError;

            try
            {
                Debug.Print("----- START ws ActCred ----");

                var user = data.GetValue("user").Value<string>();
                var credAnt = data.GetValue("cred_act").Value<string>();
                var credNew = data.GetValue("cred_new").Value<string>();
                
                ConsultaService service = new ConsultaService();
                var results = service.ActualizarCredenciales(user, credAnt, credNew, out mensaje, out codigoError);

                RestDataResponse response = new RestDataResponse(results, RestDataResponse.STATUS_OK, mensaje);

                Debug.Print("----- END ws ActCred ----");
                CRUDConsultaDbUtil.SaveConsulta(token, Constants.ACTUALIZAR_CRED, data.ToString(), codigoError.ToString(), mensaje);
                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                Debug.Print(e.StackTrace.ToString());
                CRUDConsultaDbUtil.SaveConsulta(token, Constants.ACTUALIZAR_CRED, data.ToString(), RestDataResponse.STATUS_ERROR.ToString(), "Error: " + e.Message);
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessagesUtil.error());
            }
        }
		//REQ2086 - RSIS001 - FIN

    }
}
