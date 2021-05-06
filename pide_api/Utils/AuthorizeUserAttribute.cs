using System;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Net.Http;
using System.Net;
using System.Web.Http;

namespace pide_api.Utils
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var tokenValue = actionContext.Request.Headers.FirstOrDefault(h => h.Key.Equals("Authorization")).Value;
            String token = null;

            if (tokenValue != null)
            {
                token = tokenValue.First();
            }

            if (token == null)
            {
                HandleUnauthorizedRequest(actionContext);
            }
            else
            {
                if (!ExistsToken(token))
                {
                    HandleUnauthorizedRequest(actionContext);
                }
            }

        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            HttpContext.Current.Response.AddHeader("AuthenticationStatus", "NotAuthorized");
            actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "You are unauthorized to perform this action");
            return;
        }

        public bool ExistsToken(String token)
        {
            var tokens = CRUDConsultaDbUtil.BuscarToken(token);

            if(tokens.Count != 0)
            {
                if (tokens[0].DTOK_FECHA_EXPIRACION > DateTime.Now)
                {
                    CRUDConsultaDbUtil.UpdateToken(tokens[0]);
                    return true;
                }
                else
                {
                    CRUDConsultaDbUtil.DeleteToken(tokens[0]);
                    return false;
                }
            }

            return false;
        }

    }
}