using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace pide_api.Utils
{
    public class DateUtil
    {

        public static string FORMAT_DATETIME_1 = "MM/dd/yyyy HH:mm:ss";
        public static string FORMAT_DATETIME_2 = "dd/MM/yyyy hh:mm tt";
        public static string FORMAT_DATETIME_3 = "yyyyMMdd";
        public static string FORMAT_DATETIME_4 = "dd/MM/yyyy";

        public static DateTime ParseDateFromString(string dateTime, string format)
        {
            return DateTime.ParseExact(dateTime, format, CultureInfo.InvariantCulture);
        }

        public static string ParseDateToStringWithFormat(DateTime dateTime, string format)
        {
            return dateTime.ToString(format, CultureInfo.InvariantCulture);
        }

        public static string ParseStringToStringWithFormat(string date, string formatBase, string formatNew)
        {
            return ParseDateToStringWithFormat(ParseDateFromString(date, formatBase), formatNew);
        }
    }
}