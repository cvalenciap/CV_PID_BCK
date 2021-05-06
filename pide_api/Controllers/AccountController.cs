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
using System.Text;
using System.Web.Http;

namespace pide_api.Controllers
{
    [RoutePrefix("api/autenticacion")]
    public class AccountController : ApiController
    {
        /// <summary>
        /// Autentificacion del usuario por Medio de Cerbero
        /// </summary>
        /// <returns> Un objeto Usuario</returns> 
        // POST api/autenticacion/login
        [Route("login")]
        [HttpPost]
        public HttpResponseMessage Login(JObject data)
        {
            try
            {
                Debug.Print("----- START ws login ----");
                var user = data.GetValue("user").Value<string>();
                var pass = data.GetValue("pass").Value<string>();

                Debug.Print("user: " + user);
                Debug.Print("pass: " + pass);

                var resultValidate = CerberoService.ValidarVersion(Constants.CERBERO_SYSTEM_CODE, Constants.CERBERO_VERSION, Constants.CERBERO_TYPE_INFORMATION);

                if(resultValidate.respuesta == 1)
                {
                    var resultAuntenticar = CerberoService.AutenticarUsuario(Constants.CERBERO_SYSTEM_CODE, user, pass, 0);

                    if (resultAuntenticar.respuesta == 1)
                    {
                        var tokenString = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}{1}{2}", user, DateTime.Now.Millisecond, DateTime.Now.ToShortTimeString())));
                        CRUDConsultaDbUtil.GuardarToken(user, tokenString);

                        var opciones = resultAuntenticar.opciones.Split('|').ToList();
                        var usuario = new Usuario(user, tokenString, opciones);

                        RestDataResponse response = new RestDataResponse(usuario, RestDataResponse.STATUS_OK, "Usuario logueado correctamente");

                        new LogUtil(user + " se logueo el " + DateTime.Now.ToLongTimeString() + " " +  DateTime.Now.ToLongDateString());
                        Debug.Print("----- END ws login ----");
                        return this.Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        Debug.Print("----- END ws login (No se pudo loguear en cerbero) ----");
                        return this.Request.CreateResponse(HttpStatusCode.Unauthorized, ErrorMessagesUtil.cannotLoginCerbero());
                    }
                }
                else
                {
                    Debug.Print("----- END ws login (No se pudo conectar a cerbero) ----");
                    return this.Request.CreateResponse(HttpStatusCode.Unauthorized, ErrorMessagesUtil.cannotConnectCerbero());
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                Debug.Print(ex.StackTrace.ToString());
                new LogUtil(ex);
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessagesUtil.error());
            }
        }

        // POST api/autenticacion/recoveryPass
        [Route("recoveryPass")]
        [HttpPost]
        public HttpResponseMessage RecoveryPass(JObject data)
        {
            try
            {
                if (data == null) return this.Request.CreateResponse(HttpStatusCode.BadRequest, ErrorMessagesUtil.formatoIncorrecto());

                var email = data.GetValue("email");

                // Cambiar contraseña por medio de cerbero

                IList<String> acc = new List<String>();

                if (acc.Count == 0)
                {
                    return this.Request.CreateResponse(HttpStatusCode.Unauthorized, ErrorMessagesUtil.userNoExits());
                }
                else
                {
                    RestDataResponse response = new RestDataResponse(null, RestDataResponse.STATUS_OK, "Contraseña recuperada");
                    return this.Request.CreateResponse(HttpStatusCode.OK, response);
                }

            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                Debug.Print(e.StackTrace.ToString());
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessagesUtil.error());
            }
        }

        // POST api/account/logout
        [AuthorizeUser]
        [Route("logout")]
        [HttpPost]
        public HttpResponseMessage Logout()
        {
            String token = GetTokenHeader(Request);
            var results = CRUDConsultaDbUtil.BuscarToken(token);

            if (results.Count != 0)
            {
                CRUDConsultaDbUtil.DeleteToken(results.First());
                RestDataResponse response = new RestDataResponse(null, RestDataResponse.STATUS_OK, "Logout");

                return this.Request.CreateResponse(HttpStatusCode.OK, response);
            } else
            {
                return this.Request.CreateResponse(HttpStatusCode.Unauthorized, ErrorMessagesUtil.userNoExits());
            }
        }

        private HttpResponseMessage HttpResponseMessage<T1>(String responseMessage, HttpStatusCode httpStatusCode)
        {
            throw new HttpResponseException(httpStatusCode);
        }

        public static string GetTokenHeader(HttpRequestMessage Request)
        {
            var tokenValue = Request.Headers.FirstOrDefault(h => h.Key.Equals("Authorization")).Value;
            if (tokenValue != null)
            {
                return tokenValue.First();
            }
            return "";
        }
    }
}
