using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pide_api.Models
{
    public class ConsultaDetalleTitularidadResult
    {
        public String transaccion { get; set; }
        public String total_pag { get; set; }
        public List<ConsultaAsientoTitularidadResult> asientos { get; set; }

        public ConsultaDetalleTitularidadResult()
        {

        }
    }
}