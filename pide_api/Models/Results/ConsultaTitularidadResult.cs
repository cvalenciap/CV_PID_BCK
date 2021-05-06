using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pide_api.Models
{
    public class ConsultaTitularidadResult
    {
        public String registro { get; set; }
        public String nro_partida { get; set; }
        public String estado { get; set; }
        public String zona { get; set; }
        public String oficina { get; set; }
        public String tipo_doc { get; set; }
        public String nro_doc { get; set; }
        public String nro_placa { get; set; }

        public ConsultaTitularidadResult()
        {

        }
    }
}