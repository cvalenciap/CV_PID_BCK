using Microsoft.Win32;
using System.Configuration;
using System.Diagnostics;

namespace pide_api.Utils
{
    public class ConnectionUtil
    {
        public static string ObtenerCadenaConexion()
        {
            RegistryKey objRegistry64;
            RegistryKey regKey;
            string strCadena;


            if (ConfigurationManager.AppSettings["connBDTipo"].Equals("1"))
            {
                objRegistry64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                regKey = objRegistry64.OpenSubKey(@"SOFTWARE\PIDE\", false);
                strCadena = regKey.GetValue(ConfigurationManager.AppSettings["keyConnBDRegedit"]).ToString();
            }
            else
            {
                strCadena = ConfigurationManager.AppSettings["connBD"];
            }

            return GenerarCadenaFormato(strCadena);
        }

        private static string GenerarCadenaFormato(string cadena)
        {
            return "metadata=res://*/Database.PideDatabase.csdl|res://*/Database.PideDatabase.ssdl|res://*/Database.PideDatabase.msl;provider=Oracle.ManagedDataAccess.Client;provider connection string=\""
                + cadena + "\"";
        }

        public static string ObtenerRutaLogs()
        {
            RegistryKey objRegistry64;
            RegistryKey regKey;
            string strCadena;


            if (ConfigurationManager.AppSettings["connPathLogsTipo"].Equals("1"))
            {
                objRegistry64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                regKey = objRegistry64.OpenSubKey(@"SOFTWARE\PIDE\", false);
                strCadena = regKey.GetValue(ConfigurationManager.AppSettings["keyPathLogsRegedit"]).ToString();
            }
            else
            {
                strCadena = ConfigurationManager.AppSettings["pathLogs"];
            }

            return strCadena;
        }
    }
}