using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pide_api.Models
{
    public class AuditoriaResumen
    {
        public int cantidad_consultas { get; set; }
        public string tipo_consulta { get; set; }

        public AuditoriaResumen(int cantidad, string nombre)
        {
            this.cantidad_consultas = cantidad;
            this.tipo_consulta = nombre;
        }
    }
}