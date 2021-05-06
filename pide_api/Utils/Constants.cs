///---------------------------------------------------------------------------------------------------------------------------------------------------------------
///   Namespace:        pide_api
///   Objeto:           Constants
///   Descripcion:      Clase que contiene parámetros constantes
///   Autor:            
///---------------------------------------------------------------------------------------------------------------------------------------------------------------
///   Historia de modificaciones:
///   Requerimiento:         Autor:            Fecha:          Descripcion:
///   REQ2086                cvalenciap		   10/01/2018      Agregación de parámetros constantes utilizados por el requerimiento
///---------------------------------------------------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pide_api.Utils
{
    public class Constants
    {
        //REQ2086 - RSIS002 - INICIO
		public static String SOAP_USER_REQUEST_DNI = "40898174";
        public static String SOAP_PASS_REQUEST_DNI = "40898174";
		//REQ2086 - RSIS002 - FIN
        public static String SOAP_COD_USER = "N00003";
        public static String SOAP_COD_TRANSAC = "5";
        public static String SOAP_COD_ENTIDAD = "03";

        public static String CERBERO_SYSTEM_CODE = "PIDE";
        public static String CERBERO_VERSION = "1.0";
        public static int CERBERO_TYPE_INFORMATION = 0;

        public static byte CONSULTA_DNI_ID = 1;
        public static byte CONSULTA_RUC_ID = 2;
        public static byte CONSULTA_TITULARIDAD_ID = 3;
        public static byte CONSULTA_ASIENTOS_ID = 4;
        public static byte CONSULTA_VIGENCIA_ID = 5;

        //REQ2086 - RSIS001 - INICIO
        public static String SOAP_USER_REQUEST_RUC = "20168999926";
        public static byte ACTUALIZAR_CRED = 6;
		//REQ2086 - RSIS001 - FIN

    }
}