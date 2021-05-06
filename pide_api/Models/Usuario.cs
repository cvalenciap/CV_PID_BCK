using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pide_api.Models
{
    public class Usuario
    {
        /// <summary>
        /// Nombre de usuario
        /// </summary>
        public String username { get; set; }
        /// <summary>
        /// Token de usuario autentificado
        /// </summary>
        public String authToken { get; set; }
        /// <summary>
        /// Opciones disponibles de usuario
        /// </summary>
        public List<String> opciones { get; set; }

        public Usuario(String username, String authToken, List<String> opciones)
        {
            this.username = username;
            this.authToken = authToken;
            this.opciones = opciones;
        }
    }
}