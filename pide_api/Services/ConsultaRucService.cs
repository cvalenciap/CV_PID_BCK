using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

namespace pide_api.Utils
{
    public class ConsultaRucService
    {
        public static string getRepLegalesXml(string numRuc)
        {
            StringBuilder xml = new StringBuilder();
            xml.Append(@"<?xml version=""1.0"" encoding=""utf-8""?>");
            xml.Append(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" ");
            xml.Append(@"xmlns:ser=""http://service.consultaruc.registro.servicio2.sunat.gob.pe"">");
            xml.Append("<soapenv:Header/>");
            xml.Append("<soapenv:Body>");
            xml.Append("<ser:getRepLegales>");
            xml.Append("<numruc>"+ numRuc + "</numruc> ");
            xml.Append("</ser:getRepLegales>");
            xml.Append("</soapenv:Body>");
            xml.Append("</soapenv:Envelope>");

            return getRepLegalesData(xml.ToString(), "http://ws.pide.gob.pe/ConsultaRuc?wsdl");
        }

        public static string getRepLegalesData(string xml, string address)
        {
            string result = "";
            
            HttpWebRequest request = CreateWebRequest(address,"");
            XmlDocument soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(xml);

            using (Stream stream = request.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            using (WebResponse response = request.GetResponse()) // Error occurs here
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    string soapResult = rd.ReadToEnd();
                    Console.WriteLine(soapResult);
                    result = soapResult;
                }
            }
            return result;
        }

        public static HttpWebRequest CreateWebRequest(string url, string soapAction)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }
    }
}