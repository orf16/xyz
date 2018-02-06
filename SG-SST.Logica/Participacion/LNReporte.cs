using SG_SST.EntidadesDominio.Participacion;
using SG_SST.Interfaces.Participacion;
using SG_SST.InterfazManager.Participacion;
using OfficeOpenXml;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.Audotoria;
using System;
using System.Globalization;
using System.IO;
using SG_SST.Models;
using System.Configuration;
using System.Web;
using System.Net;
using RestSharp;
using System.Web.Http;
using System.Drawing;



namespace SG_SST.Logica.Participacion
{
    public class LNReporte
    {

        string cargo = "";
        string nombre = "";
        string afiliadoempresaactivo = ConfigurationSettings.AppSettings["afiliadoempresaactivo"];

        private static IReporte Reportes = IMReporte.Reporte();

        public EDReporte GuardarReporte(EDReporte Reporte)
        {
            return Reportes.GuardarReporte(Reporte);
        }

        public EDReporte GuardarReporteEditado(EDReporte Reporte)
        {
            return Reportes.GuardarReporteEditado(Reporte);
        }

        public List<EDReporte> ObtenerReportesPorEmpresa(string nit)
        {
            return Reportes.ObtenerReportesPorEmpresa(nit);
        }

        public List<EDReporte> ObteneReportesPorBusqueda(EDReporte reporte)
        {
            return Reportes.ObteneReportesPorBusqueda(reporte);
        }

        public List<EDActividadesActosInseguros> ObtenerActividadesPorBusqueda(EDReporte reporte)
        {
            return Reportes.ObtenerActividadesPorBusqueda(reporte);
        }

        public List<EDReporte> ObtenerReporteCondicionesInsegurasPorID(int id)
        {
            return Reportes.ObtenerReporteCondicionesInsegurasPorID(id);
        }

        public List<EDActividadesActosInseguros> ObtenerActividadesCondicionesInsegurasPorID(int id)
        {
            return Reportes.ObtenerActividadesCondicionesInsegurasPorID(id);
        }

        public List<EDImagenesReportes> ObtenerImagenesCondicionesInsegurasPorID(int id)
        {
            return Reportes.ObtenerImagenesCondicionesInsegurasPorID(id);
        }

        //eliminar imagen

        public bool ELiminarImagenReporte(int idImagen)
        {
            return Reportes.ELiminarImagenReporte(idImagen);
        }

        public EDImagenesReportes ObtenerImagen(int idImagen)
        {
            return Reportes.ObtenerImagen(idImagen);
        }

        //app
        public List<EDTipoReporte> ConsultarTipoReporte()
        {
            return Reportes.ConsultarTipoReporte();
        }



        public EDReporteApp GuardarReporteAPP(EDReporteApp reporte)

        {

            return Reportes.GuardarReporteAPP(reporte);

        }


        public byte[] ObtenerRepExcelPorReporte(int id)
        {
          
            ExcelPackage excel = new ExcelPackage();

            excel.Workbook.Worksheets.Add("Reporte de condiciones actos inseguros");
            ExcelWorksheet hoja1 = excel.Workbook.Worksheets[1];

            hoja1.Cells["A1"].Value = "Razón social de la empresa";
            hoja1.Cells["B1"].Value = "Nit Empresa";
            hoja1.Cells["C1"].Value = "Fecha de reporte";
            hoja1.Cells["D1"].Value = "Sede";
            hoja1.Cells["E1"].Value = "Tipo";
            hoja1.Cells["F1"].Value = "Proceso";
            hoja1.Cells["G1"].Value = "Área/lugar";
            hoja1.Cells["H1"].Value = "Fecha del evento";
            hoja1.Cells["I1"].Value = "Documento";
            hoja1.Cells["J1"].Value = "Nombre";
            hoja1.Cells["K1"].Value = "Cargo";
            hoja1.Cells["L1"].Value = "Descripción";
            hoja1.Cells["M1"].Value = "Causa";
            hoja1.Cells["N1"].Value = "Sugerencias";
            hoja1.Cells["O1"].Value = "Origen";


            int col = 1;
            foreach (var cel in hoja1.Cells["A1:O1"])
            {
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                cel.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                cel.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                cel.Style.Font.Bold = true;
                cel.Style.WrapText = true;
                hoja1.Column(col).Width = 25;
                col++;
            }

            List<EDReporte> reportes = Reportes.ObtenerReporteCondicionesInsegurasPorID(id);


            int nunInicial = 2;
            foreach (var rep in reportes)
            {
           
                
                ValidarDocumento(rep.CedulaQuienReporta);
                hoja1.Cells[string.Format("A{0}", nunInicial)].Value = rep.RazonSocialEmpresa;
                hoja1.Cells[string.Format("B{0}", nunInicial)].Value = rep.nitEmpresa;
                hoja1.Cells[string.Format("C{0}", nunInicial)].Value = rep.fechaSistena.ToString(("dd-MM-yyyy"));
                hoja1.Cells[string.Format("D{0}", nunInicial)].Value = rep.sede;
                hoja1.Cells[string.Format("E{0}", nunInicial)].Value = rep.tipo;

                if (rep.nombreProceso != null)
                {
                    hoja1.Cells[string.Format("F{0}", nunInicial)].Value = rep.nombreProceso;
                }
                else
                {
                    hoja1.Cells[string.Format("F{0}", nunInicial)].Value = "NA";
                }
               
                hoja1.Cells[string.Format("G{0}", nunInicial)].Value = rep.AreaLugar;
                hoja1.Cells[string.Format("H{0}", nunInicial)].Value = rep.FechaOcurrencia.ToString(("dd-MM-yyyy"));
                hoja1.Cells[string.Format("I{0}", nunInicial)].Value = rep.CedulaQuienReporta;
                hoja1.Cells[string.Format("J{0}", nunInicial)].Value = nombre;
                hoja1.Cells[string.Format("K{0}", nunInicial)].Value = cargo;
                hoja1.Cells[string.Format("L{0}", nunInicial)].Value = rep.DescripcionReporte;
                hoja1.Cells[string.Format("M{0}", nunInicial)].Value = rep.CausaReporte;
                hoja1.Cells[string.Format("N{0}", nunInicial)].Value = rep.SugerenciasReporte;

                if (rep.medioAcceso == false)
                {
                    hoja1.Cells[string.Format("O{0}", nunInicial)].Value = "Alissta WEB";
                }
                else
                {
                    hoja1.Cells[string.Format("O{0}", nunInicial)].Value = "Alissta APP";
                }

                nunInicial++;
            }
            hoja1.Cells.AutoFitColumns();
            //Cargar datos de hoja dos tipo parametros
            excel.Workbook.Worksheets.Add("Actividades");
            ExcelWorksheet hoja2 = excel.Workbook.Worksheets["Actividades"];



            hoja2.Cells["A1"].Value = "Nombre de la Actividad";
            hoja2.Cells["B1"].Value = "Responsable de la Actividad";
            hoja2.Cells["C1"].Value = "Fecha de ejecución ";



            List<EDActividadesActosInseguros> actividades = Reportes.ObtenerActividadesCondicionesInsegurasPorID(id);


            nunInicial = 2;

            foreach (var act in actividades)
            {
             
                hoja2.Cells[string.Format("A{0}", nunInicial)].Value = act.nombreActividad;
                hoja2.Cells[string.Format("B{0}", nunInicial)].Value = act.RespActividad;
                hoja2.Cells[string.Format("C{0}", nunInicial)].Value = act.FecEjecucion.ToString(("dd-MM-yyyy"));



                nunInicial++;
            }





          
            hoja2.Cells.AutoFitColumns();

            foreach (var cel in hoja2.Cells["A1:AC1"])
            {
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                cel.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                cel.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                cel.Style.Font.Bold = true;
                cel.Style.WrapText = true;
                hoja1.Column(col).Width = 25;
                col++;
            }

            return excel.GetAsByteArray();

        }


        public byte[] ObtenerReporteExcel(List<EDReporte> resultReporte, List<EDActividadesActosInseguros> resultActividades)
        {
            ExcelPackage excel = new ExcelPackage();

            excel.Workbook.Worksheets.Add("Reporte de condiciones actos inseguros");
            ExcelWorksheet hoja1 = excel.Workbook.Worksheets[1];

            hoja1.Cells["A1"].Value = "Razón social de la empresa";
            hoja1.Cells["B1"].Value = "Nit Empresa";
            hoja1.Cells["C1"].Value = "ID Reporte";
            hoja1.Cells["D1"].Value = "Fecha de reporte";
            hoja1.Cells["E1"].Value = "Sede";
            hoja1.Cells["F1"].Value = "Tipo";
            hoja1.Cells["G1"].Value = "Proceso";
            hoja1.Cells["H1"].Value = "Área/lugar";
            hoja1.Cells["I1"].Value = "Fecha del evento";
            hoja1.Cells["J1"].Value = "Documento";
            hoja1.Cells["K1"].Value = "Nombre";
            hoja1.Cells["L1"].Value = "Cargo";
            hoja1.Cells["M1"].Value = "Descripción";
            hoja1.Cells["N1"].Value = "Causa";
            hoja1.Cells["O1"].Value = "Sugerencias";
            hoja1.Cells["P1"].Value = "Origen";


            int col = 1;
            foreach (var cel in hoja1.Cells["A1:P1"])
            {
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                cel.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                cel.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                cel.Style.Font.Bold = true;
                cel.Style.WrapText = true;
                hoja1.Column(col).Width = 25;
                col++;
            }

            int nunInicial = 2;
            foreach (var rep in resultReporte)
            {
                ValidarDocumento(rep.CedulaQuienReporta);
                hoja1.Cells[string.Format("A{0}", nunInicial)].Value = rep.RazonSocialEmpresa;
                hoja1.Cells[string.Format("B{0}", nunInicial)].Value = rep.nitEmpresa;
                hoja1.Cells[string.Format("C{0}", nunInicial)].Value = rep.IdReportes;
                hoja1.Cells[string.Format("D{0}", nunInicial)].Value = rep.fechaSistena.ToString(("dd-MM-yyyy"));
                hoja1.Cells[string.Format("E{0}", nunInicial)].Value = rep.sede;
                hoja1.Cells[string.Format("F{0}", nunInicial)].Value = rep.tipo;

                if (rep.nombreProceso != null)
                {
                    hoja1.Cells[string.Format("G{0}", nunInicial)].Value = rep.nombreProceso;
                }
                else
                {
                    hoja1.Cells[string.Format("G{0}", nunInicial)].Value ="NA";
                }
                hoja1.Cells[string.Format("H{0}", nunInicial)].Value = rep.AreaLugar;
                hoja1.Cells[string.Format("I{0}", nunInicial)].Value = rep.FechaOcurrencia.ToString(("dd-MM-yyyy"));
                hoja1.Cells[string.Format("J{0}", nunInicial)].Value = rep.CedulaQuienReporta;
                hoja1.Cells[string.Format("K{0}", nunInicial)].Value = nombre;
                hoja1.Cells[string.Format("L{0}", nunInicial)].Value = cargo;
                hoja1.Cells[string.Format("M{0}", nunInicial)].Value = rep.DescripcionReporte;
                hoja1.Cells[string.Format("N{0}", nunInicial)].Value = rep.CausaReporte;
                hoja1.Cells[string.Format("O{0}", nunInicial)].Value = rep.SugerenciasReporte;

                if (rep.medioAcceso == false)
                {
                    hoja1.Cells[string.Format("P{0}", nunInicial)].Value = "Alissta WEB";
                }
                else
                {
                    hoja1.Cells[string.Format("P{0}", nunInicial)].Value = "Alissta APP";
                }

                nunInicial++;
            }

            hoja1.Cells.AutoFitColumns();

            //Cargar datos de hoja dos tipo parametros
            excel.Workbook.Worksheets.Add("Actividades");
            ExcelWorksheet hoja2 = excel.Workbook.Worksheets["Actividades"];



       

            hoja2.Cells["A1"].Value = "ID Reporte";
            hoja2.Cells["B1"].Value = "Nombre de la Actividad";
            hoja2.Cells["C1"].Value = "Responsable de la Actividad";
            hoja2.Cells["D1"].Value = "Fecha de ejecución ";




            nunInicial = 2;

            foreach (var act in resultActividades)
            {
                hoja2.Cells[string.Format("A{0}", nunInicial)].Value = act.FKReportes;
                hoja2.Cells[string.Format("B{0}", nunInicial)].Value = act.nombreActividad;
                hoja2.Cells[string.Format("C{0}", nunInicial)].Value = act.RespActividad;
                hoja2.Cells[string.Format("D{0}", nunInicial)].Value = act.FecEjecucion.ToString(("dd-MM-yyyy"));



                nunInicial++;
            }

            hoja2.Cells.AutoFitColumns();

            foreach (var cel in hoja2.Cells["A1:AD1"])
            {
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                cel.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                cel.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                cel.Style.Font.Bold = true;
                cel.Style.WrapText = true;
                hoja1.Column(col).Width = 25;
                col++;
            }
            return excel.GetAsByteArray();

        }


        public void ValidarDocumento(int documento)
        {


            cargo = "";
            nombre = "";

            try
            {
                var cliente = new RestSharp.RestClient(afiliadoempresaactivo);
                var request = new RestRequest("wssst/afiliado?", RestSharp.Method.GET);
                request.RequestFormat = DataFormat.Xml;
                request.Parameters.Clear();
                request.AddParameter("tpDoc", "cc");
                request.AddParameter("doc", documento);
                request.AddParameter("color", "orange");
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");


                //se omite la validación de certificado de SSL
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                IRestResponse<List<EDReporteWS>> response = cliente.Execute<List<EDReporteWS>>(request);
                var result = response.Content;
                var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EDReporteWS>>(result);

                cargo = respuesta[0].ocupacion;
                nombre = respuesta[0].nombre1 + " " + respuesta[0].nombre2 + " " + respuesta[0].apellido1 + " " + respuesta[0].apellido2;


            }
            catch (Exception e)
            {

            }
        }

   
    }
}