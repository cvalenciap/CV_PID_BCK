///-------------------------------------------------------------------------------------
///   Namespace:        CERBERO.Util
///   Objeto:           Encriptacion
///   Descripcion:      Métodos para encriptar y desencriptar datos utilizados por Cerbero
///   Autor:            Andrea Sánchez
///-------------------------------------------------------------------------------------
///   Historia de modificaciones:
///   Requerimiento:    Autor:            Fecha:        Descripcion:
///-------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace CERBERO.Util
{
    /// <summary>
    /// Métodos para encriptar y desencriptar datos utilizados por Cerbero
    /// </summary>
    public abstract class Encriptacion
    {
        private static string strPatron_busqueda = "mnbvcxzñlkjhgfdsapoiuytrewq6574839201MNBVCXZÑLKJHGFDSAPOIUYTREWQ";
        private static string strPatron_encripta = "0129786345QAZWSXCDEVFRTGBNHYUJMKIÑOPLqawspñoledrfiktgujhyzmxncbv";

        /// <summary>
        /// Devuelve el hash MD5 de una cadena
        /// </summary>
        /// <param name="strCadena">cadena de texto a convertir</param>
        /// <returns>hash MD5 o Excepción en caso la cadena esté vacía</returns>
        public static string MD5(string strCadena)
        {
            if (strCadena.Equals(string.Empty)) throw new ArgumentOutOfRangeException();

            using (MD5 md5Hash = System.Security.Cryptography.MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(strCadena));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sb.Append(data[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// Desencripta una cadena
        /// </summary>
        /// <param name="strCadena">cadena a desencriptar</param>
        /// <returns>cadena desencriptada</returns>
        public static string DesEncriptarCadena(string strCadena)
        {
            if (strCadena.Equals(string.Empty)) throw new ArgumentOutOfRangeException();

            string str2 = string.Empty;
            int num2 = (strCadena.Length - 1);
            for (int i = 0; i <= num2; i++)
            {
                str2 = str2 + Encriptacion.DesEncriptarCaracter(strCadena.Substring(i, 1), strCadena.Length, i);
            } 
            return str2;
        }

        private static string DesEncriptarCaracter(string strCaracter, int intVariable, int inta_indice)
        {
            int num;
            if (strPatron_encripta.IndexOf(strCaracter) == -1) 
            {
                return strCaracter;
            }
            if (((strPatron_encripta.IndexOf(strCaracter) - intVariable) - inta_indice) > 0)
            {
                num = (((strPatron_encripta.IndexOf(strCaracter) - intVariable) - inta_indice) % strPatron_encripta.Length);
            }
            else 
            {
                num = (strPatron_busqueda.Length + (((strPatron_encripta.IndexOf(strCaracter) - intVariable) - inta_indice) % strPatron_encripta.Length));
            }
            num = (num % strPatron_encripta.Length);
            return strPatron_busqueda.Substring(num, 1);
        }

        /// <summary>
        /// Encripta una cadena
        /// </summary>
        /// <param name="strCadena">cadena a encriptar</param>
        /// <returns>cadena encriptada</returns>
        public static string EncriptarCadena(string strCadena) 
        {
            if (strCadena.Equals(string.Empty)) throw new ArgumentOutOfRangeException();

            string str2 = string.Empty;
            int num2 = (strCadena.Length - 1);
            for (int i = 0; i <= num2; i++)
            {
                str2 = str2 + Encriptacion.EncriptarCaracter(strCadena.Substring(i, 1), strCadena.Length, i);
            }
            return str2;
        }

        private static string EncriptarCaracter(string strCaracter, int intVariable, int inta_indice) 
        {
            if (strPatron_busqueda.IndexOf(strCaracter) != -1)
            {
                int startIndex = (((Encriptacion.strPatron_busqueda.IndexOf(strCaracter) + intVariable) + inta_indice) % strPatron_busqueda.Length);
                return strPatron_encripta.Substring(startIndex, 1);
            }
            return strCaracter;
        }
    }
}
