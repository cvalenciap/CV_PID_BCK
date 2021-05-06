using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Net.Http;
using System.Net;
using CERBERO.Cliente.Servicio;
using CERBERO.Cliente.Contratos;

namespace CERBERO.Cliente
{
    public class ClienteCerbero
    {
        private const int RESPUESTA_OK = 1;

        public int ValidarVersion(string CodigoSistema, string Version, int TipoInformacion, out int strCodigoRespuesta, out string strMensaje)
        {
            ClienteServicio objeto = new ClienteServicio();
            RespuestaValidarVersion respuesta = objeto.ValidarVersion(CodigoSistema, Version, TipoInformacion);

            strCodigoRespuesta = respuesta.CodigoRespuesta;
            strMensaje = respuesta.Mensaje;

            return (respuesta.CodigoRespuesta == ClienteCerbero.RESPUESTA_OK) ? 1 : 0;
        }

        public int AutenticarUsuario(string strCodigoSistema, string strUsuario, string strPassword, int intTipoUsuario, out int strCodigoRespuesta, out string strMensaje, out string strOpciones)
        {
            ClienteServicio objeto = new ClienteServicio();
            RespuestaAutenticarUsuario respuesta = objeto.AutenticarUsuario(strCodigoSistema, strUsuario, strPassword, intTipoUsuario);

            strCodigoRespuesta = respuesta.CodigoRespuesta;
            strMensaje = respuesta.Mensaje;
            strOpciones = (strCodigoRespuesta == 1) ? respuesta.Objeto.Opciones : "";

            return (respuesta.CodigoRespuesta == ClienteCerbero.RESPUESTA_OK) ? 1 : 0;
        }


    }
}
