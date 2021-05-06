using Microsoft.Win32;
using System.Configuration;

namespace CERBERO.Cliente
{
    class UtilConexion
    {
        public static string ObtenerCadenaConexionCerbero()
        {
            RegistryKey objRegistry64;
            RegistryKey regKey;
            string strCadena;


            if (ConfigurationManager.AppSettings["connCerberoTipo"].Equals("1"))
            {
                objRegistry64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                regKey = objRegistry64.OpenSubKey(@"SOFTWARE\PIDE\", false);
                strCadena = regKey.GetValue(ConfigurationManager.AppSettings["keyConnCerberoRegedit"]).ToString();
            }
            else
            {
                strCadena = ConfigurationManager.AppSettings["connCerbero"];
            }

            return strCadena;
        }
    }
}
