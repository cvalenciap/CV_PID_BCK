///---------------------------------------------------------------------------------------------------------------------------------------------------------------
///   Namespace:        pide_api
///   Objeto:           XmlUtil
///   Descripcion:      Utilitario que realiza conversión de resultados del consumo de servicio web
///   Autor:            
///---------------------------------------------------------------------------------------------------------------------------------------------------------------
///   Historia de modificaciones:
///   Requerimiento:         Autor:            Fecha:          Descripcion:
///   REQ2086                cvalenciap		   10/01/2018      Adecuación de método parseGetDni para el uso de datos de la operación consultar dni
///															   Creación de método parseGetDni para uso de datos de la operación actualizar credenciales								
///---------------------------------------------------------------------------------------------------------------------------------------------------------------
using pide_api.Models;
//REQ2086 - INICIO
using pide_api.Models.Results;
using pide_api.Pruebawebserv;
//REQ2086 - FIN
using pide_api.pe.gob.pide.ws;
using pide_api.ServiceReference;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;

namespace pide_api.Utils
{
    public class XmlUtil
    {
        

        public static ConsultaRucResult parseGetByRuc(BeanDdp datosPrincipales,
            BeanDds datosSecundarios, BeanRso[] repLegales, string domicilioLegal)
        {
            var result = new ConsultaRucResult();
            result.telefono = "";

            if (datosPrincipales != null)
            {
                result.razon_social = datosPrincipales.ddp_nombre;
                result.estado = datosPrincipales.desc_estado;
                result.condicion = datosPrincipales.desc_flag22;
            }

            if (datosSecundarios != null)
            {
                result.ruc = datosSecundarios.dds_numruc;
                if (!datosSecundarios.dds_telef1.Equals("-"))
                {
                    result.telefono = datosSecundarios.dds_telef1;
                }
                if (!datosSecundarios.dds_telef2.Equals("-"))
                {
                    if (!result.telefono.Equals(""))
                    {
                        result.telefono += ", ";
                    }
                    result.telefono += datosSecundarios.dds_telef2;
                }
                if (!datosSecundarios.dds_telef3.Equals("-"))
                {
                    if (result.telefono.Equals(""))
                    {
                        result.telefono += ", ";
                    }
                    result.telefono += datosSecundarios.dds_telef3;
                }
            }

            result.representate_legal = "";

            if (repLegales != null)
            {
                foreach (var repLegal in repLegales)
                {
                    var nameRepLegal = repLegal.rso_nombre;
                    if (!result.representate_legal.Equals(""))
                    {
                        result.representate_legal += ", ";
                    }
                    result.representate_legal += nameRepLegal;
                }
            }

            if (domicilioLegal != null)
            {
                result.direccion = domicilioLegal;
            }

            if (result.telefono.Equals(""))
            {
                result.telefono = "NO REGISTRADO";
            }

            if (result.representate_legal.Equals(""))
            {
                result.representate_legal = "NO POSEE";
            }

            result.otro = "POR DEFINIR";

            return result;
        }
        
        public static ConsultaTitularidadResult parseGetTitularidad(resultadoTitularidad respuestaTitularidad)
        {
            var result = new ConsultaTitularidadResult();

            result.registro = respuestaTitularidad.registro;
            result.nro_partida = respuestaTitularidad.numeroPartida;
            result.estado = respuestaTitularidad.estado;
            result.zona = respuestaTitularidad.zona;
            result.oficina = respuestaTitularidad.oficina;
            result.tipo_doc = respuestaTitularidad.tipoDocumento;
            result.nro_doc = respuestaTitularidad.numeroDocumento;
            result.nro_placa = respuestaTitularidad.numeroPlaca;

            return result;
        }

        public static IList<ConsultaTitularidadResult> parseGetTitularidadArray(resultadoTitularidad[] respuestaTitularidad)
        {
            IList<ConsultaTitularidadResult> results = new List<ConsultaTitularidadResult>();

            foreach (var titularidad in respuestaTitularidad)
            {
                var result = parseGetTitularidad(titularidad);
                results.Add(result);
            }

            return results;
        }

        public static ConsultaDetalleTitularidadResult parseGetDetalleTitularidad(respuestaPartidaBean detalleTitularidad)
        {
            var result = new ConsultaDetalleTitularidadResult();

            result.transaccion = detalleTitularidad.transaccion.ToString();
            result.total_pag = detalleTitularidad.nroTotalPag;

            result.asientos = new List<ConsultaAsientoTitularidadResult>();

            if (detalleTitularidad.listAsientos != null)
            {
                foreach (var asiento in detalleTitularidad.listAsientos)
                {
                    foreach (var pag in asiento.listPag)
                    {
                        var asientoData = new ConsultaAsientoTitularidadResult();
                        asientoData.id_img = asiento.idImgAsiento.ToString();
                        asientoData.tipo = asiento.tipo;
                        asientoData.nro_pag = pag.pagina;
                        asientoData.pag_ref = pag.nroPagRef;
                        result.asientos.Add(asientoData);
                    }
                }
            }

            if (detalleTitularidad.listFichas != null) {
                foreach (var ficha in detalleTitularidad.listFichas)
                {
                    foreach (var pag in ficha.listPag)
                    {
                        var asientoData = new ConsultaAsientoTitularidadResult();
                        asientoData.id_img = ficha.idImgFicha.ToString();
                        asientoData.tipo = ficha.tipo;
                        asientoData.nro_pag = pag.pagina;
                        asientoData.pag_ref = pag.nroPagRef;
                        result.asientos.Add(asientoData);
                    }
                }
            }

            if (detalleTitularidad.listFolios != null)
            {
                foreach (var folio in detalleTitularidad.listFolios)
                {
                    var asientoData = new ConsultaAsientoTitularidadResult();
                    asientoData.id_img = folio.idImgFolio.ToString();
                    asientoData.tipo = folio.tipo;
                    asientoData.nro_pag = folio.pagina;
                    asientoData.pag_ref = folio.nroPagRef;
                    result.asientos.Add(asientoData);
                }
            }

            result.asientos.Sort((x, y) => x.pag_ref.CompareTo(y.pag_ref));

            return result;
        }

        public static string parseGetVigenciaPoder(byte[] vigenciaPoderData)
        {
            return Convert.ToBase64String(vigenciaPoderData);
        }

        //REQ2086 - RSIS002 - INICIO
        public static ConsultarDniResult parseGetDni(datosPersona respuestaDatosPersona)
        {
            var result = new ConsultarDniResult();

            result.apPaterno = respuestaDatosPersona.apPrimer;
            result.apMaterno = respuestaDatosPersona.apSegundo;
            result.preNombres = respuestaDatosPersona.prenombres;
            result.estCivil = respuestaDatosPersona.estadoCivil;
            result.foto = Convert.ToBase64String(respuestaDatosPersona.foto);
            result.ubigeo = respuestaDatosPersona.ubigeo;
            result.direccion = respuestaDatosPersona.direccion;
            result.restriccion = respuestaDatosPersona.restriccion;

            return result;
        }
		//REQ2086 - RSIS002 - FIN
		
		//REQ2086 - RSIS001 - INICIO
        public static ActCredResult parseActCred(string respuestaCod, string respuestaDesc) {
            var result = new ActCredResult();

            result.codResultado = respuestaCod;
            result.desResultado = respuestaDesc;

            return result;
        }
		//REQ2086 - RSIS001 - FIN
    }
}