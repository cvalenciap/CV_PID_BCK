using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pide_api.Models
{
    public class RestDataResponse
    {
        public static int STATUS_OK = 1;
        public static int STATUS_ERROR = 0;

        public Object data { get; set; }
        public int status { get; set; }
        public String message { get; set; }

        public RestDataResponse(Object data, int status, String mensaje)
        {
            this.data = data;
            this.status = status;
            this.message = mensaje;
        }
    }
}