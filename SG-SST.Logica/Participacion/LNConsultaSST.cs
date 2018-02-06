using SG_SST.EntidadesDominio.Participacion;
using SG_SST.Interfaces.Participacion;
using SG_SST.InterfazManager.Participacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace SG_SST.Logica.Participacion
{
    public class LNConsultaSST
    {
        private static IConsulta Consultas = IMConsultaSST.Consulta();

        public EDConsultaSST GrabarConsultaSST(EDConsultaSST consulta)
        {
            return Consultas.GrabarConsultaSST(consulta);
        }

        public List<EDConsultaSST> ObtenerConsultasSST(int idEmpresa)
        {
            return Consultas.ObtenerConsultasSST(idEmpresa);
        }

        public EDConsultaSST ObtenerUnaConsultaSST(int idConsulta)
        {
            return Consultas.ObtenerUnaConsultaSST(idConsulta);
        }
        public bool EditarGestionConsulta(EDConsultaTrazabilidad NuevoAdmonCTZB)
        {
            return Consultas.EditarGestionConsulta(NuevoAdmonCTZB);
        }

        public bool EliminarEvidenciaConsulta(int id, string ruta, int dato)
        {
            return Consultas.EliminarEvidenciaConsulta(id, ruta, dato);
        }

        public List<EDConsultaSST> ConsultarConsultasSST(EDConsultarConsultasSST consultar)
        {
            return Consultas.ConsultarConsultasSST(consultar);
        }

        public byte[] DescargarConsultaSST(EDConsultarConsultasSST consultar)
        {
            ExcelPackage excel = new ExcelPackage();
            excel.Workbook.Worksheets.Add("ConsultaSST");
            ExcelWorksheet hoja1 = excel.Workbook.Worksheets[1];

            hoja1.Cells["A1"].Value = "Fecha de la Consulta";
            hoja1.Cells["B1"].Value = "Tipo de Consulta SST";
            hoja1.Cells["C1"].Value = "Descripción";
            hoja1.Cells["D1"].Value = "Fecha de la Gestión";
            hoja1.Cells["E1"].Value = "Observaciones de la Gestión";

            int col = 1;
            foreach (var cel in hoja1.Cells["A1:E1"])
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
            List<EDConsultaSST> consultas = Consultas.ConsultarConsultasSST(consultar);
            int nunInicial = 2;
            foreach (var consulta in consultas)
            {
                hoja1.Cells[string.Format("A{0}", nunInicial)].Value = consulta.FechaConsultaED.ToString("dd/MM/yyyy");
                hoja1.Cells[string.Format("B{0}", nunInicial)].Value = consulta.TipoConsultaED;
                hoja1.Cells[string.Format("C{0}", nunInicial)].Value = consulta.DescripcionConsultaED;
                if (consulta.FechaRevisionED.ToString("dd/MM/yyyy") != "01/01/1900")
                {
                    //nueva.FechaRevisionED = consulta.FechaRevisionED;
                    hoja1.Cells[string.Format("D{0}", nunInicial)].Value = consulta.FechaRevisionED.ToString("dd/MM/yyyy");
                }
                else
                {
                    //string dateString = null;
                    //nueva.FechaRevisionED = Convert.ToDateTime(dateString);
                    hoja1.Cells[string.Format("D{0}", nunInicial)].Value = "";
                }

                hoja1.Cells[string.Format("E{0}", nunInicial)].Value = consulta.ObservacionesED;
                nunInicial++;
            }
            return excel.GetAsByteArray();
        }

        public byte[] DescargarConsultaSSTSinFiltro(int idEmpresa)
        {
            ExcelPackage excel = new ExcelPackage();
            excel.Workbook.Worksheets.Add("ConsultaSST");
            ExcelWorksheet hoja1 = excel.Workbook.Worksheets[1];

            hoja1.Cells["A1"].Value = "Fecha de la Consulta";
            hoja1.Cells["B1"].Value = "Tipo de Consulta SST";
            hoja1.Cells["C1"].Value = "Descripción";
            hoja1.Cells["D1"].Value = "Fecha de la Gestión";
            hoja1.Cells["E1"].Value = "Observaciones de la Gestión";

            int col = 1;
            foreach (var cel in hoja1.Cells["A1:E1"])
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
            List<EDConsultaSST> consultas = Consultas.ObtenerConsultasSST(idEmpresa);
            int nunInicial = 2;
            foreach (var consulta in consultas)
            {
                hoja1.Cells[string.Format("A{0}", nunInicial)].Value = consulta.FechaConsultaED.ToString("dd/MM/yyyy");
                hoja1.Cells[string.Format("B{0}", nunInicial)].Value = consulta.TipoConsultaED;
                hoja1.Cells[string.Format("C{0}", nunInicial)].Value = consulta.DescripcionConsultaED;
                if (consulta.FechaRevisionED.ToString("dd/MM/yyyy") != "01/01/1900")
                {
                    //nueva.FechaRevisionED = consulta.FechaRevisionED;
                    hoja1.Cells[string.Format("D{0}", nunInicial)].Value = consulta.FechaRevisionED.ToString("dd/MM/yyyy");
                }
                else
                {
                    //string dateString = null;
                    //nueva.FechaRevisionED = Convert.ToDateTime(dateString);
                    hoja1.Cells[string.Format("D{0}", nunInicial)].Value = "";
                }

                hoja1.Cells[string.Format("E{0}", nunInicial)].Value = consulta.ObservacionesED;
                nunInicial++;
            }
            return excel.GetAsByteArray();
        }
    }
}
