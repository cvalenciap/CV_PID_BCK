using System;
using System.Diagnostics;
using System.IO;

namespace pide_api.Utils
{
    public class LogUtil
    {
        private string m_exePath = string.Empty;

        public LogUtil(string logMessage)
        {
            LogWrite(logMessage);
        }

        public LogUtil(Exception ex)
        {
            var Excep = "Exception -> " + ex.Message + ": " + ex.StackTrace.ToString();
            LogWrite(Excep);

            LogInnerException(ex);
        }

        public void LogInnerException(Exception ex)
        {
            if (ex.InnerException != null)
            {
                var InnerExcp = "Inner Exception -> " + ex.InnerException.Message + ": " + ex.InnerException.StackTrace.ToString();
                LogWrite(InnerExcp);

                LogInnerException(ex.InnerException);
            }
        }

        public void LogWrite(string logMessage)
        {
            m_exePath = ConnectionUtil.ObtenerRutaLogs();

            Directory.CreateDirectory(m_exePath);

            Debug.Print(m_exePath);
            try
            {
                using (StreamWriter w = File.AppendText(m_exePath + "\\" + "PideLog.txt"))
                {
                    Log(logMessage, w);
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                Debug.Print(ex.StackTrace.ToString());
            }
        }

        public void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :");
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
                var innerExcp = ex.InnerException.Message + ": " + ex.InnerException.StackTrace.ToString();
                var Excep = ex.Message + ": " + ex.StackTrace.ToString();
                var error = "Excepcion -> " + Excep + " | Inner Exception -> " + innerExcp;
                Log(error, txtWriter);
            }
        }
    }
}