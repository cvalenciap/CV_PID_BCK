///---------------------------------------------------------------------------------------------------------------------------------------------------------------
///   Namespace:        pide_api
///   Objeto:           ExecSPUtil
///   Descripcion:      Utilitario para la ejecución de los procedimientos almacenados utilizados en las oepraciones de consumo de servicio web
///   Autor:            Carlos Valencia
///---------------------------------------------------------------------------------------------------------------------------------------------------------------
///   Historia de modificaciones:
///   Requerimiento:         Autor:            Fecha:          Descripcion:
///---------------------------------------------------------------------------------------------------------------------------------------------------------------
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using pide_api.Database;
using pide_api.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace pide_api.Utils
{
    public class ExecSPUtil
    {
        private OracleConnection conn;
        private string Package = "PKG_SERVICE_RENIEC";
        public const int RESULTADO_OK = 1;
        public const int RESULTADO_ERROR = -1;
        public string cadenaConexion;
        //Constructor obtiene cadena de conexión a base de datos
        public ExecSPUtil()
        {
            cadenaConexion = ConfigurationManager.AppSettings["connBD"];
            try
            {
                //conn.Open();
            }
            catch (OracleException ex)
            {
                String errorMessage = "Code: " + ex.ErrorCode + "\n" + "Message: " + ex.Message;
                Console.WriteLine("Excepción en conexión a Base de Datos");
            }
        }
        
        //Método ejecuta el procedimiento almacenado que obtiene datos de BD
        public string[] EjecGetCred(string username)
        {
            string[] arrayResult = new string[4];

            try
            {
                conn = new OracleConnection(cadenaConexion);
                conn.Open();
                OracleCommand cmd;
                cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = Package + ".SP_GET_CREDE_RENIEC";
                cmd.CommandType = CommandType.StoredProcedure;
                //Asignar usuario como parametro de entrada
                OracleParameter in_username = new OracleParameter("V_USERNAME", OracleDbType.Varchar2);
                in_username.Value = username;
                cmd.Parameters.Add(in_username);
                //devuelve valor del cursor
                cmd.Parameters.Add("REFCURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                OracleDataReader curReader = default(OracleDataReader);
                //Ejecución de procedimiento almacenado
                cmd.ExecuteNonQuery();

                curReader = ((OracleRefCursor)cmd.Parameters["REFCURSOR"].Value).GetDataReader();
                //Almacena resultados del cursor en arreglo
                while (curReader.Read())
                {
                    for (int i = 0; i <= curReader.FieldCount - 1; i++)
                    {
                        arrayResult[i] = Convert.ToString(curReader[i]);
                    }
                }
                curReader.Close();
                CerrarConexion(cmd);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Exception: {0}", ex.ToString());
            }
            return arrayResult;
        }

        //Método ejecuta el procedimiento almacenado que almacena las credenciales nuevas en BD
        public string EjectActCred(string username, string credAct, string credNew)
        {
            string sp_value = null;
            try
            {
                conn = new OracleConnection(cadenaConexion);
                conn.Open();
                OracleCommand cmd;
                cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = Package + ".SP_ACT_CREDE_RENIEC";
                cmd.CommandType = CommandType.StoredProcedure;
                //Asignar usuario como parámetro de entrada
                OracleParameter in_username = new OracleParameter("V_USERNAME", OracleDbType.Varchar2);
                in_username.Value = username;
                cmd.Parameters.Add(in_username);
                //Asignar credencial antigua como parámetro de entrada
                OracleParameter in_cred_ant = new OracleParameter("V_CRED_ANT", OracleDbType.Varchar2);
                in_cred_ant.Value = credAct;
                cmd.Parameters.Add(in_cred_ant);
                //Asignar credencial nueva como parámetro de entrada
                OracleParameter in_cred_new = new OracleParameter("V_CRED_NEW", OracleDbType.Varchar2);
                in_cred_new.Value = credNew;
                cmd.Parameters.Add(in_cred_new);
                //Almacena resultado de ejecución de procedimiento almacenado
                cmd.Parameters.Add("V_RESULTADO", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                sp_value = cmd.Parameters["V_RESULTADO"].Value.ToString();

                CerrarConexion(cmd);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Exception: {0}", ex.ToString());
            }

            return sp_value;
        }
        //Método que cierra la conexión a la BD
        public void CerrarConexion(OracleCommand cmd)
        {
            conn.Close();
            conn.Dispose();
            cmd.Dispose();
        }

    }
}