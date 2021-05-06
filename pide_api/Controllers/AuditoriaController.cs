using Newtonsoft.Json.Linq;
using pide_api.Models;
using pide_api.Utils;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace pide_api.Controllers
{
    [RoutePrefix("api/auditoria")]
    public class AuditoriaController : ApiController
    {
        /// <summary>
        /// Cargar Lista de Consultas disponibles
        /// </summary>
        /// <returns>Lista de Objeto Consulta</returns> 
        // GET api/auditoria/consultas
        [Route("consultas")]
        [HttpGet]
        public HttpResponseMessage ListarConsultas()
        {
            try
            {

                Debug.Print("----- START ws ListarConsultas ----");
                var results = CRUDConsultaDbUtil.ObtenerConsultas();

                if (results != null)
                {
                    RestDataResponse response = new RestDataResponse(results, RestDataResponse.STATUS_OK, "");

                    Debug.Print("----- END ws ListarConsultas ----");
                    return this.Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessagesUtil.cannotGetInfo());
                }
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                Debug.Print(e.StackTrace.ToString());
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessagesUtil.error());
            }
        }

        /// <summary>
        /// Obtener registro de consultas en un periodo de tiempo
        /// </summary>
        /// <returns>Lista de consultas regisradas en el log</returns> 
        // POST api/auditoria/registroConsultas
        [AuthorizeUser]
        [Route("registroConsultas")]
        [HttpPost]
        public HttpResponseMessage RegistroConsultas(JObject data)
        {
            try
            {
                Debug.Print("----- START ws RegistroConsultas ----");
                var startDate = data.GetValue("startDate").Value<string>();
                var endDate = data.GetValue("endDate").Value<string>();

                Debug.Print(startDate);
                Debug.Print(endDate);

                var results = CRUDConsultaDbUtil.FindConsultaInRange(startDate, endDate);

                if (results != null)
                {
                    RestDataResponse response = new RestDataResponse(results, RestDataResponse.STATUS_OK, "");

                    Debug.Print("----- END ws RegistroConsultas ----");
                    return this.Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessagesUtil.cannotGetInfo());
                }
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                Debug.Print(e.StackTrace.ToString());
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessagesUtil.error());
            }
        }

        /// <summary>
        /// Obtener cuantas consultas se han dado un mes determinando
        /// </summary>
        /// <returns>Cantidad de consultas realizadas para cada tipo</returns> 
        // POST api/auditoria/resumenConsultas
        [AuthorizeUser]
        [Route("resumenConsultas")]
        [HttpPost]
        public HttpResponseMessage ResumenConsultas(JObject data)
        {
            try
            {
                Debug.Print("----- START ws ResumenConsultas ----");
                var date = data.GetValue("date").Value<string>();

                Debug.Print(date);

                var fechaResumen = DateUtil.ParseDateFromString(date, DateUtil.FORMAT_DATETIME_1);
                var month = fechaResumen.Month;
                var year = fechaResumen.Year;

                var results = CRUDConsultaDbUtil.FindConsultaInMothYear(month, year);

                if (results != null)
                {
                    RestDataResponse response = new RestDataResponse(results, RestDataResponse.STATUS_OK, "");

                    Debug.Print("----- END ws ResumenConsultas ----");
                    return this.Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessagesUtil.cannotGetInfo());
                }
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                Debug.Print(e.StackTrace.ToString());
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessagesUtil.error());
            }
        }
    }
}
