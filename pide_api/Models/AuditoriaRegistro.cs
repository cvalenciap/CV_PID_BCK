using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pide_api.Models
{
    public class AuditoriaRegistro
    {  
        public string usuario { get; set; }
        public string fecha_hora { get; set; }
        public string tipo_consulta { get; set; }
        public string criterio_busqueda { get; set; }
        public string codigo_error { get; set; }
        public string mensaje_error { get; set; }

        public AuditoriaRegistro(string usuario, string fechaHora, string tipoConsulta, 
            string criterioBusqueda, string codigo_error, string mensaje_error)
        {
            this.usuario = usuario;
            this.fecha_hora = fechaHora;
            this.tipo_consulta = tipoConsulta;
            this.criterio_busqueda = criterioBusqueda;
            this.codigo_error = codigo_error;
            this.mensaje_error = mensaje_error;
        }
    }
}