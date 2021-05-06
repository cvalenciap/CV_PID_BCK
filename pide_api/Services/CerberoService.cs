using CERBERO.Cliente;
using pide_api.Models.Results;
using System.Diagnostics;

namespace pide_api.Services
{
    public class CerberoService
    {
        public static CerberoResult ValidarVersion(string CodigoSistema, string Version, int TipoInformacion)
        {
            int Respuesta;
            int CodigoRespuesta;
            string Mensaje;
            ClienteCerbero Cliente = new ClienteCerbero();
            Respuesta = Cliente.ValidarVersion(CodigoSistema, Version, TipoInformacion, out CodigoRespuesta, out Mensaje);
            Debug.Print("Codigo respuesta: " + CodigoRespuesta + " | Mensaje: " + Mensaje + " | Respuesta: " + Respuesta);
            return new CerberoResult(CodigoRespuesta, Mensaje, Respuesta, "");
        }

        public static CerberoResult AutenticarUsuario(string CodigoSistema, string Usuario, string Password, int TipoUsuario)
        {
            int Respuesta;
            int CodigoRespuesta;
            string Mensaje;
            string Opciones;

            ClienteCerbero Cliente = new ClienteCerbero();
            Respuesta = Cliente.AutenticarUsuario(CodigoSistema, Usuario, Password, TipoUsuario, out CodigoRespuesta, out Mensaje, out Opciones);
            Debug.Print("Codigo respuesta: " + CodigoRespuesta + " | Mensaje: " + Mensaje + " | Respuesta: " + Respuesta + " | Opciones: " + Opciones);
            return new CerberoResult(CodigoRespuesta, Mensaje, Respuesta, Opciones);
        }
    }
}