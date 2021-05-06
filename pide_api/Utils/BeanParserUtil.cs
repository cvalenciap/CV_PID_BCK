using pide_api.pe.gob.pide.ws;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Xml;

namespace pide_api.Utils
{
    public class BeanParserUtil
    {
        public static BeanDdp ConvertDdpFromXml(XmlNode[] nodes)
        {
            var ddp = new BeanDdp();

            Dictionary<string, string> map = new Dictionary<string, string>();

            MapNodes(nodes, map);

            ddp.cod_dep = map["cod_dep"];
            ddp.cod_dist = map["cod_dist"];
            ddp.cod_prov = map["cod_prov"];
            ddp.ddp_ciiu = map["ddp_ciiu"];
            ddp.ddp_doble = map["ddp_doble"];
            ddp.ddp_estado = map["ddp_estado"];
            ddp.ddp_fecact = map["ddp_fecact"];
            ddp.ddp_fecalt = map["ddp_fecalt"];
            ddp.ddp_fecbaj = map["ddp_fecbaj"];
            ddp.ddp_flag22 = map["ddp_flag22"];
            ddp.ddp_identi = map["ddp_identi"];
            ddp.ddp_inter1 = map["ddp_inter1"];
            ddp.ddp_lllttt = map["ddp_lllttt"];
            ddp.ddp_mclase = map["ddp_mclase"];
            ddp.ddp_nombre = map["ddp_nombre"];
            ddp.ddp_nomvia = map["ddp_nomvia"];
            ddp.ddp_nomzon = map["ddp_nomzon"];
            ddp.ddp_numer1 = map["ddp_numer1"];
            ddp.ddp_numreg = map["ddp_numreg"];
            ddp.ddp_numruc = map["ddp_numruc"];
            ddp.ddp_reacti = map["ddp_reacti"];
            ddp.ddp_refer1 = map["ddp_refer1"];
            ddp.ddp_secuen = Int32.Parse(map["ddp_secuen"]);
            ddp.ddp_tamano = map["ddp_tamano"];
            ddp.ddp_tipvia = map["ddp_tipvia"];
            ddp.ddp_tipzon = map["ddp_tipzon"];
            ddp.ddp_tpoemp = map["ddp_tpoemp"];
            ddp.ddp_ubigeo = map["ddp_ubigeo"];
            ddp.ddp_userna = map["ddp_userna"];
            ddp.desc_ciiu = map["desc_ciiu"];
            ddp.desc_dep = map["desc_dep"];
            ddp.desc_dist = map["desc_dist"];
            ddp.desc_estado = map["desc_estado"];
            ddp.desc_flag22 = map["desc_flag22"];
            ddp.desc_identi = map["desc_identi"];
            ddp.desc_numreg = map["desc_numreg"];
            ddp.desc_prov = map["desc_prov"];
            ddp.desc_tamano = map["desc_tamano"];
            ddp.desc_tipvia = map["desc_tipvia"];
            ddp.desc_tipzon = map["desc_tipzon"];
            ddp.desc_tpoemp = map["desc_tpoemp"];
            ddp.esActivo = Boolean.Parse(map["esActivo"]);
            ddp.esHabido = Boolean.Parse(map["esHabido"]);

            return ddp;
        }

        public static BeanDds ConvertDdsFromXml(XmlNode[] nodes)
        {
            var dds = new BeanDds();

            Dictionary<string, string> map = new Dictionary<string, string>();

            MapNodes(nodes, map);

            dds.dds_aparta = map["dds_aparta"];
            dds.dds_asient = map["dds_asient"];
            dds.dds_califi = map["dds_califi"];
            dds.dds_centro = map["dds_centro"];
            dds.dds_cierre = map["dds_cierre"];
            dds.dds_comext = map["dds_comext"];
            dds.dds_consti = map["dds_consti"];
            dds.dds_contab = map["dds_contab"];
            dds.dds_docide = map["dds_docide"];
            dds.dds_domici = map["dds_domici"];
            dds.dds_ejempl = map["dds_ejempl"];
            dds.dds_factur = map["dds_factur"];
            dds.dds_fecact = map["dds_fecact"];
            dds.dds_fecnac = map["dds_fecnac"];
            dds.dds_fecven = map["dds_fecven"];
            dds.dds_ficha = map["dds_ficha"];
            dds.dds_inicio = map["dds_inicio"];
            dds.dds_licenc = map["dds_licenc"];
            dds.dds_motbaj = map["dds_motbaj"];
            dds.dds_motemi = map["dds_motemi"];
            dds.dds_nacion = map["dds_nacion"];
            dds.dds_nfolio = map["dds_nfolio"];
            dds.dds_nomcom = map["dds_nomcom"];
            dds.dds_nrodoc = map["dds_nrodoc"];
            dds.dds_numfax = map["dds_numfax"];
            dds.dds_numruc = map["dds_numruc"];
            dds.dds_orient = map["dds_orient"];
            dds.dds_paispa = map["dds_paispa"];
            dds.dds_pasapo = map["dds_pasapo"];
            dds.dds_patron = map["dds_patron"];
            dds.dds_sexo = map["dds_sexo"];
            dds.dds_telef1 = map["dds_telef1"];
            dds.dds_telef2 = map["dds_telef2"];
            dds.dds_telef3 = map["dds_telef3"];
            dds.dds_userna = map["dds_userna"];
            dds.declara = map["declara"];
            dds.desc_cierre = map["desc_cierre"];
            dds.desc_comext = map["desc_comext"];
            dds.desc_contab = map["desc_contab"];
            dds.desc_docide = map["desc_docide"];
            dds.desc_domici = map["desc_domici"];
            dds.desc_factur = map["desc_factur"];
            dds.desc_motbaj = map["desc_motbaj"];
            dds.desc_nacion = map["desc_nacion"];
            dds.desc_orient = map["desc_orient"];
            dds.desc_sexo = map["desc_sexo"];

            return dds;
        }

        public static BeanRso[] ConvertRsoFromXml(String rawXml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(rawXml);

            List<XmlElement> items = new List<XmlElement>();
            Dictionary<string, XmlElement> multiRefs = new Dictionary<string, XmlElement>();

            InspectNodes(doc.DocumentElement, items, multiRefs);

            BeanRso[] rsoArray = new BeanRso[multiRefs.Count];

            for (int i = 0; i < multiRefs.Count; i++)
            {
                var key = "id" + i;
                var nodes = multiRefs[key].ChildNodes;

                var rso = new BeanRso();
                Dictionary<string, string> map = new Dictionary<string, string>();

                MapNodes(nodes, map);

                rso.cod_cargo = map["cod_cargo"];
                rso.cod_depar = map["cod_depar"];
                rso.desc_docide = map["desc_docide"];
                rso.num_ord_suce = Int16.Parse(map["num_ord_suce"]);
                rso.rso_cargoo = map["rso_cargoo"];
                rso.rso_docide = map["rso_docide"];
                rso.rso_fecact = map["rso_fecact"];
                rso.rso_fecnac = map["rso_fecnac"];
                rso.rso_nombre = map["rso_nombre"];
                rso.rso_nrodoc = map["rso_nrodoc"];
                rso.rso_numruc = map["rso_numruc"];
                rso.rso_userna = map["rso_userna"];
                rso.rso_vdesde = map["rso_vdesde"];

                rsoArray[i] = rso;
            }

            return rsoArray;
        }

        private static void InspectNodes(XmlElement element, List<XmlElement> items, Dictionary<string, XmlElement> multiRefs)
        {
            string val = element.GetAttribute("href");
            if (val != null && val.StartsWith("#id"))
                items.Add(element);
            else if (element.Name == "multiRef")
                multiRefs[element.GetAttribute("id")] = element;

            foreach (XmlNode node in element.ChildNodes)
            {
                var child = node as XmlElement;
                if (child != null)
                    InspectNodes(child, items, multiRefs);
            }

        }

        private static void FixNodes(List<XmlElement> items, Dictionary<string, XmlElement> multiRefs)
        {
            // Reverse order so populate the id refs into one single element. This is only a solution in relation to the WSDL definition.
            for (int x = items.Count - 1; x >= 0; x--)
            {
                XmlElement element = items[x];

                string href = element.GetAttribute("href");
                if (String.IsNullOrEmpty(href))
                    continue;

                if (href.StartsWith("#"))
                    href = href.Remove(0, 1);

                XmlElement multiRef = multiRefs[href];

                if (multiRef == null)
                    continue;

                element.RemoveAttribute("href");
                element.InnerXml = multiRef.InnerXml;

                multiRef.ParentNode.RemoveChild(multiRef as XmlNode);
            }
        }

        private static void MapNodes(XmlNodeList nodes, Dictionary<string, string> map)
        {
            foreach (XmlNode node in nodes)
            {
                map[node.Name] = node.InnerText.Trim();
            }
        }

        private static void MapNodes(XmlNode[] nodes, Dictionary<string, string> map)
        {
            foreach (XmlNode node in nodes)
            {
                map[node.Name] = node.InnerText.Trim();
            }
        }
    }
}