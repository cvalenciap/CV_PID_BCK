///---------------------------------------------------------------------------------------------------------------------------------------------------------------
///   Namespace:        pide_api
///   Objeto:           ActCredResult
///   Descripcion:      Modelo sobre los resultados de la operación actualización de credenciales
///   Autor:            Carlos Valencia
///---------------------------------------------------------------------------------------------------------------------------------------------------------------
///   Historia de modificaciones:
///   Requerimiento:         Autor:            Fecha:          Descripcion:
///---------------------------------------------------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pide_api.Models.Results
{
    public class ActCredResult
    {
        public String codResultado { get; set; }
        public String desResultado { get; set; } 

        public ActCredResult()
        {

        }
    }
}