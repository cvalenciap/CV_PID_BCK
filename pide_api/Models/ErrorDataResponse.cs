using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pide_api.Models
{
    public class ErrorDataResponse
    {
        public String message { get; set; }
        public String stacktracker { get; set; }

        public ErrorDataResponse(String mensaje, String stacktracker)
        {
            this.message = mensaje;
            this.stacktracker = stacktracker;
        }
    }
}