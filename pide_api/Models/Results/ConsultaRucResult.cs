using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pide_api.Models
{
    public class ConsultaRucResult
    {
        public String razon_social { get; set; }
        public String ruc { get; set; }
        public String direccion { get; set; }
        public String estado { get; set; }
        public String condicion { get; set; }
        public String telefono { get; set; }
        public String representate_legal { get; set; }
        public String otro { get; set; }

        public ConsultaRucResult()
        {

        }
    }
}