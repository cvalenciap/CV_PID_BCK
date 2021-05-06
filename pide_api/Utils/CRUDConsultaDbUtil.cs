using pide_api.Database;
using pide_api.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace pide_api.Utils
{
    public class CRUDConsultaDbUtil
    {
        private static PideDatabase ObtenerBaseDatos()
        {
            string cadenaConexion = ConnectionUtil.ObtenerCadenaConexion();
            return new PideDatabase(cadenaConexion);
        }

        public static void SaveConsulta(string token, byte consultaId, string parameters, string error, string comentarioError)
        {
            Debug.Print("Insertar en BD");
            PideDatabase db = ObtenerBaseDatos();

            var id = db.Database.SqlQuery<int>("SELECT SEQPIDE_CREA_CONSULTALOG_ID.NEXTVAL FROM DUAL").First();

            TBLPIDE_CONSULTA_LOG consultaLog = new TBLPIDE_CONSULTA_LOG();
            consultaLog.CCONSLOG_CONSULTA_LOG_ID = id;
            consultaLog.SCONSLOG_USUARIO = GetUserFromToken(token);
            consultaLog.DCONSLOG_FECHA_HORA = DateTime.Now;
            consultaLog.CCCONSLOG_ERROR = error;
            consultaLog.SCONSLOG_COMENTARIO_ERROR = comentarioError;
            consultaLog.CCONS_CONSULTA_ID = consultaId;
            consultaLog.SCONSLOG_PARAMETROS_REQUEST = parameters;

            db.TBLPIDE_CONSULTA_LOG.Add(consultaLog);
            db.SaveChanges();
            Debug.Print("FIN De insercion en BD");
        }

        public static void GuardarToken(string email, string authToken)
        {
            PideDatabase db = ObtenerBaseDatos();
            var id = db.Database.SqlQuery<int>("SELECT SEQPIDE_CREA_TOKEN_ID.NEXTVAL FROM DUAL").First();

            TBLPIDE_TOKEN token = new TBLPIDE_TOKEN
            {
                CTOK_TOKEN_ID = id,
                STOK_USUARIO_ID = email,
                STOK_AUTH_TOKEN = authToken
            };
            DateTime now = DateTime.Now;
            token.DTOK_FECHA_CREACION = now;
            token.DTOK_FECHA_EXPIRACION = now.AddHours(3);

            db.TBLPIDE_TOKEN.Add(token);
            db.SaveChanges();
        }

        private static string GetUserFromToken(string token)
        {
            PideDatabase db = ObtenerBaseDatos();
            var result = db.TBLPIDE_TOKEN.SingleOrDefault(t => t.STOK_AUTH_TOKEN == token);
            if(result == null)
            {
                return "USUARIO NO IDENTIFICADO";
            }

            return result.STOK_USUARIO_ID;
        }

        public static List<TBLPIDE_TOKEN> BuscarToken(string token)
        {
            PideDatabase db = ObtenerBaseDatos();
            return db.TBLPIDE_TOKEN.Where(t => t.STOK_AUTH_TOKEN == token).ToList();
        }

        public static void UpdateToken(TBLPIDE_TOKEN token)
        {
            PideDatabase db = ObtenerBaseDatos();
            var result = db.TBLPIDE_TOKEN.SingleOrDefault(t => t.STOK_AUTH_TOKEN == token.STOK_AUTH_TOKEN);
            if (result != null)
            {
                result.DTOK_FECHA_EXPIRACION = DateTime.Now.AddHours(3);
                db.SaveChanges();
            }
        }

        public static void DeleteToken(TBLPIDE_TOKEN token)
        {
            PideDatabase db = ObtenerBaseDatos();
            db.TBLPIDE_TOKEN.Attach(token);
            db.TBLPIDE_TOKEN.Remove(token);
            db.SaveChanges();
        }

        public static List<TBLPIDE_CONSULTA> ObtenerConsultas()
        {
            PideDatabase db = ObtenerBaseDatos();
            return db.TBLPIDE_CONSULTA.ToList();
        }

        public static IList<AuditoriaResumen> FindConsultaInMothYear(int month, int year)
        {
            IList<AuditoriaResumen> resultados = new List<AuditoriaResumen>();
            PideDatabase db = ObtenerBaseDatos();

            var consultas = db.TBLPIDE_CONSULTA;

            foreach (var consulta in consultas)
            {
                var cantidad = db.TBLPIDE_CONSULTA_LOG
                    .Where(cl => cl.CCONS_CONSULTA_ID == consulta.CCONS_CONSULTA_ID)
                    .Where(cl => cl.DCONSLOG_FECHA_HORA.Month == month)
                    .Where(cl => cl.DCONSLOG_FECHA_HORA.Year == year)
                    .Select(cl => cl.CCONSLOG_CONSULTA_LOG_ID).Count();

                var resultado = new AuditoriaResumen(cantidad, consulta.SCONS_NOMBRE);
                resultados.Add(resultado);
            }

            return resultados;
        }

        public static IList<AuditoriaRegistro> FindConsultaInRange(string startDate, string endDate)
        {
            IList<AuditoriaRegistro> resultados = new List<AuditoriaRegistro>();
            PideDatabase db = ObtenerBaseDatos();

            var fechaInicioTmp = DateUtil.ParseDateFromString(startDate, DateUtil.FORMAT_DATETIME_1);
            var fechaInicio = new DateTime(fechaInicioTmp.Year, fechaInicioTmp.Month, fechaInicioTmp.Day, 0, 0 ,0);
            var fechaFinTmp = DateUtil.ParseDateFromString(endDate, DateUtil.FORMAT_DATETIME_1).AddDays(1);
            var fechaFin = new DateTime(fechaFinTmp.Year, fechaFinTmp.Month, fechaFinTmp.Day, 23, 59, 59);

            var query = db.TBLPIDE_CONSULTA_LOG
                .Where(cl => cl.DCONSLOG_FECHA_HORA >= fechaInicio && cl.DCONSLOG_FECHA_HORA <= fechaFin)
                .Select(cl => new {
                    usuario = cl.SCONSLOG_USUARIO,
                    fecha_hora = cl.DCONSLOG_FECHA_HORA,
                    tipo_consulta = cl.TBLPIDE_CONSULTA.SCONS_NOMBRE,
                    criterio_busqueda = cl.SCONSLOG_PARAMETROS_REQUEST,
                    codigo_error = cl.CCCONSLOG_ERROR,
                    mensaje_error = cl.SCONSLOG_COMENTARIO_ERROR
                })
                .OrderByDescending(x => x.fecha_hora);

            foreach(var result in query.ToList())
            {
                var resultado = new AuditoriaRegistro(result.usuario, 
                    DateUtil.ParseDateToStringWithFormat(result.fecha_hora, DateUtil.FORMAT_DATETIME_2), 
                    result.tipo_consulta, 
                    result.criterio_busqueda,
                    result.codigo_error,
                    result.mensaje_error);
                resultados.Add(resultado);
            }

            return resultados;
        }

    }
}