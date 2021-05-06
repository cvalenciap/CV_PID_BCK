using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pide_api.Utils
{
    public class ErrorCodeValidationUtil
    {
        public static int getErrorCode(String xmlDocument)
        {
            int errorCode;
            bool isNumeric = int.TryParse(xmlDocument, out errorCode);
            if (isNumeric)
            {
                return errorCode;
            }
            return 0;
        }
    }
}