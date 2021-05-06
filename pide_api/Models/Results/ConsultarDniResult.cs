///---------------------------------------------------------------------------------------------------------------------------------------------------------------
///   Namespace:        pide_api
///   Objeto:           ConsultarDniResult
///   Descripcion:      Nuevo modelo sobre los resultados de la operación consultar DNI
///   Autor:            Carlos Valencia
///---------------------------------------------------------------------------------------------------------------------------------------------------------------
///   Historia de modificaciones:
///   Requerimiento:         Autor:            Fecha:          Descripcion:
///---------------------------------------------------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pide_api.Models
{
    public class ConsultarDniResult
    {
        public String apPaterno { get; set; }
        public String apMaterno { get; set; }
        public String preNombres { get; set; }
        public String estCivil { get; set; }
        public String foto { get; set;  }
        public String ubigeo { get; set; }
        public String direccion { get; set; }
        public String restriccion { get; set; }

        public ConsultarDniResult() {
        }
    }
}