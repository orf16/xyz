using OfficeOpenXml;
using SG_SST.EntidadesDominio.Ausentismo;
using SG_SST.Interfaces.Ausentismo;
using SG_SST.InterfazManager.Ausentismo;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;


namespace SG_SST.Logica.Ausentismo
{
    public class LNReportes
    {
        private static IReportes reportesMG = IMAusentismo.Reportes();
        private static IAusencia ausenMg = IMAusentismo.Ausencia();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="anio"></param>
        /// <returns></returns>
        public List<EDReportes> ConsultarPorContingencia(EDReportes edReporte)
        {
            List<EDReportesGenerados> respuestas = new List<EDReportesGenerados>();
            var reporteDefinitivo = new List<EDReportes>();
            var edrespuestas = reportesMG.ReporteContingencia(edReporte);

            int diasHablides = ausenMg.ObtenerDiasLaborablesEmpresa(edReporte.nitEmpresa);
            if (diasHablides == 1)
                diasHablides = 5;
            else if (diasHablides == 2)
                diasHablides = 6;

            foreach (var edrespuesta in edrespuestas)
            {
                respuestas.AddRange(ObtenerDiasCadaMes(edrespuesta, diasHablides));
            }

            reporteDefinitivo = GenerarInformacionReportes(respuestas).ToList();
            return reporteDefinitivo;            
        }

        private IEnumerable<EDReportesGenerados> ObtenerDiasCadaMes(EDDatosReportes edrespuesta, int diasHablides)
        {
            List<EDReportesGenerados> datosRespuestas = new List<EDReportesGenerados>();
            int mesInicial = edrespuesta.FechaInicio.Month;
            int mesFinal = edrespuesta.FechaFin.Month;
            int dias = 0;

            DateTime tempFinal = new DateTime();
            DateTime tempInicial = new DateTime();

            LNAusencia use = new LNAusencia();
            if (mesInicial > mesFinal)
                mesFinal = 12;
            if (mesInicial == mesFinal)
            {                
                dias = use.CalcularDiasLaborales(edrespuesta.FechaInicio, edrespuesta.FechaFin, diasHablides, edrespuesta.idContigencia);

                EDReportesGenerados datoRespuesta = new EDReportesGenerados()
                {
                    Contingencia = edrespuesta.Descripcion,
                    Mes = mesInicial,
                    Evento = "",
                    Total = dias
                };
                datosRespuestas.Add(datoRespuesta);
            }
            else
            {
                int auxmes = 0;
                int contador = 0;               
                for(int i = mesInicial; i <= mesFinal; i++)
                {
                    if (i == 12)
                        auxmes = 12;
                    else
                        auxmes = i + 1;
                    if (i == mesFinal)
                    {
                        tempInicial = Convert.ToDateTime(string.Format("{0}/{1}/{2}", 1, i, edrespuesta.FechaInicio.Year));
                        dias = use.CalcularDiasLaborales(tempInicial, edrespuesta.FechaFin, diasHablides, edrespuesta.idContigencia);
                    }
                    else
                    {                        
                        if (contador == 0)
                        {        
                            tempFinal = Convert.ToDateTime(string.Format("{0}/{1}/{2}", 1, auxmes, edrespuesta.FechaInicio.Year)).AddDays(-1);
                            dias = use.CalcularDiasLaborales(edrespuesta.FechaInicio, tempFinal, diasHablides, edrespuesta.idContigencia);
                        }
                        else
                        {                            
                            tempFinal = Convert.ToDateTime(string.Format("{0}/{1}/{2}", 1, auxmes, edrespuesta.FechaInicio.Year)).AddDays(-1);
                            tempInicial = Convert.ToDateTime(string.Format("{0}/{1}/{2}", 1, i, edrespuesta.FechaInicio.Year));
                            dias = use.CalcularDiasLaborales(tempInicial, tempFinal, diasHablides, edrespuesta.idContigencia);
                        }                        
                        contador++;
                    }

                    EDReportesGenerados datoRespuesta = new EDReportesGenerados()
                    {
                        Contingencia = edrespuesta.Descripcion,
                        Mes = i,
                        Evento = "",
                        Total = dias
                    };
                    datosRespuestas.Add(datoRespuesta);
                }
            }

            return datosRespuestas;
        }

        /// <summary>
        /// Genera el archivo excel para el reporte Días Ausentismo por contingencia
        /// </summary>
        /// <param name="edReporte"></param>
        /// <returns></returns>
        public byte[] ConsultarPorContingenciaExcel(EDReportes edReporte)
        {
            List<EDReportes> DatosReporte = ConsultarPorContingencia(edReporte);
                      

            ExcelPackage excel = new ExcelPackage();

            excel.Workbook.Worksheets.Add("Días Ausentismo x contingencia");

            ExcelWorksheet hoja = excel.Workbook.Worksheets[1];

            hoja.Cells["A1:N1"].Merge = true;
            hoja.Cells["A1"].Value = "Días Ausentismo por contingencia ";
            hoja.Cells["A1"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["A1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["A1"].Style.Font.Bold = true;
            hoja.Cells["A1"].Style.WrapText = true;

            hoja.Cells["A2"].Value = "Tipo Contingencia";
            hoja.Cells["A2"].Style.Font.Bold = true;
            hoja.Cells["B2"].Value = "Enero";
            hoja.Cells["B2"].Style.Font.Bold = true;
            hoja.Cells["C2"].Value = "Febrero";
            hoja.Cells["C2"].Style.Font.Bold = true;
            hoja.Cells["D2"].Value = "Marzo";
            hoja.Cells["D2"].Style.Font.Bold = true;
            hoja.Cells["E2"].Value = "Abril";
            hoja.Cells["E2"].Style.Font.Bold = true;
            hoja.Cells["F2"].Value = "Mayo";
            hoja.Cells["F2"].Style.Font.Bold = true;
            hoja.Cells["G2"].Value = "Junio";
            hoja.Cells["G2"].Style.Font.Bold = true;
            hoja.Cells["H2"].Value = "Julio";
            hoja.Cells["H2"].Style.Font.Bold = true;
            hoja.Cells["I2"].Value = "Agosto";
            hoja.Cells["I2"].Style.Font.Bold = true;
            hoja.Cells["J2"].Value = "Septiembre";
            hoja.Cells["J2"].Style.Font.Bold = true;
            hoja.Cells["K2"].Value = "Octubre";
            hoja.Cells["K2"].Style.Font.Bold = true;
            hoja.Cells["L2"].Value = "Noviembre";
            hoja.Cells["L2"].Style.Font.Bold = true;
            hoja.Cells["M2"].Value = "Diciembre";
            hoja.Cells["M2"].Style.Font.Bold = true;
            hoja.Cells["N2"].Value = "Total";
            hoja.Cells["N2"].Style.Font.Bold = true;

            int nunInicial = 3;

            foreach (var dato in DatosReporte)
            {
                hoja.Cells[string.Format("A{0}", nunInicial)].Value = dato.CONTINGENCIA;
                hoja.Cells[string.Format("A{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].Value = dato.Ene;
                hoja.Cells[string.Format("B{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("C{0}", nunInicial)].Value = dato.Feb;
                hoja.Cells[string.Format("C{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("C{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("D{0}", nunInicial)].Value = dato.Mar;
                hoja.Cells[string.Format("D{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("D{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("E{0}", nunInicial)].Value = dato.Abr;
                hoja.Cells[string.Format("E{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("E{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("F{0}", nunInicial)].Value = dato.May;
                hoja.Cells[string.Format("F{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("F{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("G{0}", nunInicial)].Value = dato.Jun;
                hoja.Cells[string.Format("G{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("G{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("H{0}", nunInicial)].Value = dato.Jul;
                hoja.Cells[string.Format("H{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("H{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("I{0}", nunInicial)].Value = dato.Ago;
                hoja.Cells[string.Format("I{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("I{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("J{0}", nunInicial)].Value = dato.Sep;
                hoja.Cells[string.Format("J{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("J{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("K{0}", nunInicial)].Value = dato.Oct;
                hoja.Cells[string.Format("K{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("K{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("L{0}", nunInicial)].Value = dato.Nov;
                hoja.Cells[string.Format("L{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("L{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("M{0}", nunInicial)].Value = dato.Dic;
                hoja.Cells[string.Format("M{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("M{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("N{0}", nunInicial)].Value = dato.Total;
                hoja.Cells[string.Format("N{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("N{0}", nunInicial)].OfType<decimal>();

                nunInicial++;
            }

            hoja.Column(1).Width = 40;            

            foreach (var cel in hoja.Cells[string.Format("A1:N{0}", DatosReporte.Count + 2)])
            {
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            }

            //AGREGAMOS LA GRAFICA
            var chart = hoja.Drawings.AddChart("Días Ausentismo por contingencia", OfficeOpenXml.Drawing.Chart.eChartType.ColumnClustered3D);
            chart.Title.Text = "Días Ausentismo por contingencia";
            chart.SetPosition(1, 0, 15, 0);
            chart.SetSize(600, 400); // Tamaño de la gráfica
            chart.Legend.Remove(); // Si desea eliminar la leyenda
            
            // Define donde está la información de la gráfica.
            var serie = chart.Series.Add(hoja.Cells[string.Format("N3:N{0}", DatosReporte.Count + 2)], hoja.Cells[string.Format("A3:A{0}", DatosReporte.Count + 2)]);

            //FileInfo fileInfo = new FileInfo(@"D:\Archivos\Actividades.xlsx");
            //excel.SaveAs(fileInfo);
            return excel.GetAsByteArray();
        }


        public List<EDReportes> ConsultarPorDepartamento(EDReportes edReporte)
        {
            List<EDReportesGenerados> respuestas = new List<EDReportesGenerados>();
            var reporteDefinitivo = new List<EDReportes>();
            var edrespuestas = reportesMG.ReporteDepartamento(edReporte);

            int diasHablides = ausenMg.ObtenerDiasLaborablesEmpresa(edReporte.nitEmpresa);
            if (diasHablides == 1)
                diasHablides = 5;
            else if (diasHablides == 2)
                diasHablides = 6;

            foreach (var edrespuesta in edrespuestas)
            {
                respuestas.AddRange(ObtenerDiasCadaMesEvento(edrespuesta, diasHablides));
            }

            reporteDefinitivo = GenerarInformacionReportes(respuestas).ToList();
            return reporteDefinitivo;
        }

        private IEnumerable<EDReportesGenerados> ObtenerDiasCadaMesEvento(EDDatosReportes edrespuesta, int diasHablides)
        {
            List<EDReportesGenerados> datosRespuestas = new List<EDReportesGenerados>();
            int mesInicial = edrespuesta.FechaInicio.Month;
            int mesFinal = edrespuesta.FechaFin.Month;
            int dias = 0;

            DateTime tempFinal = new DateTime();
            DateTime tempInicial = new DateTime();

            LNAusencia use = new LNAusencia();
            if (mesInicial > mesFinal)
                mesFinal = 12;
            if (mesInicial == mesFinal)
            {
                dias = use.CalcularDiasLaborales(edrespuesta.FechaInicio, edrespuesta.FechaFin, diasHablides, edrespuesta.idContigencia);

                EDReportesGenerados datoRespuesta = new EDReportesGenerados()
                {
                    Contingencia = "",
                    Mes = mesInicial,
                    Evento = edrespuesta.Descripcion,
                    Total = dias
                };
                datosRespuestas.Add(datoRespuesta);
            }
            else
            {
                int auxmes = 0;
                int contador = 0;
                for (int i = mesInicial; i <= mesFinal; i++)
                {
                    if (i == 12)
                        auxmes = 12;
                    else
                        auxmes = i + 1;
                    if (i == mesFinal)
                    {
                        tempInicial = Convert.ToDateTime(string.Format("{0}/{1}/{2}", 1, i, edrespuesta.FechaInicio.Year));
                        dias = use.CalcularDiasLaborales(tempInicial, edrespuesta.FechaFin, diasHablides, edrespuesta.idContigencia);
                    }
                    else
                    {
                        if (contador == 0)
                        {
                            tempFinal = Convert.ToDateTime(string.Format("{0}/{1}/{2}", 1, auxmes, edrespuesta.FechaInicio.Year)).AddDays(-1);
                            dias = use.CalcularDiasLaborales(edrespuesta.FechaInicio, tempFinal, diasHablides, edrespuesta.idContigencia);
                        }
                        else
                        {
                            tempFinal = Convert.ToDateTime(string.Format("{0}/{1}/{2}", 1, auxmes, edrespuesta.FechaInicio.Year)).AddDays(-1);
                            tempInicial = Convert.ToDateTime(string.Format("{0}/{1}/{2}", 1, i, edrespuesta.FechaInicio.Year));
                            dias = use.CalcularDiasLaborales(tempInicial, tempFinal, diasHablides, edrespuesta.idContigencia);
                        }
                        contador++;
                    }

                    EDReportesGenerados datoRespuesta = new EDReportesGenerados()
                    {
                        Contingencia = "",
                        Mes = i,
                        Evento = edrespuesta.Descripcion,
                        Total = dias
                    };
                    datosRespuestas.Add(datoRespuesta);
                }
            }

            return datosRespuestas;
        }

        /// <summary>
        /// Genera el archivo excel para el informe: Dias ausentismo por Departamentos
        /// </summary>
        /// <param name="edReporte"></param>
        /// <returns></returns>
        public byte[] ConsultarPorDepartamentoExcel(EDReportes edReporte)
        {
            List<EDReportes> DatosReporte = ConsultarPorDepartamento(edReporte);

            ExcelPackage excel = new ExcelPackage();
            excel.Workbook.Worksheets.Add("Dias ausentismo por Dpto");                                           

            ExcelWorksheet hoja = excel.Workbook.Worksheets[1];

            hoja.Cells["A1:N1"].Merge = true;
            hoja.Cells["A1"].Value = "Dias ausentismo por Departamentos ";
            hoja.Cells["A1"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["A1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["A1"].Style.Font.Bold = true;
            hoja.Cells["A1"].Style.WrapText = true;

            hoja.Cells["A2"].Value = "Departamento";
            hoja.Cells["A2"].Style.Font.Bold = true;
            hoja.Cells["B2"].Value = "Enero";
            hoja.Cells["B2"].Style.Font.Bold = true;
            hoja.Cells["C2"].Value = "Febrero";
            hoja.Cells["C2"].Style.Font.Bold = true;
            hoja.Cells["D2"].Value = "Marzo";
            hoja.Cells["D2"].Style.Font.Bold = true;
            hoja.Cells["E2"].Value = "Abril";
            hoja.Cells["E2"].Style.Font.Bold = true;
            hoja.Cells["F2"].Value = "Mayo";
            hoja.Cells["F2"].Style.Font.Bold = true;
            hoja.Cells["G2"].Value = "Junio";
            hoja.Cells["G2"].Style.Font.Bold = true;
            hoja.Cells["H2"].Value = "Julio";
            hoja.Cells["H2"].Style.Font.Bold = true;
            hoja.Cells["I2"].Value = "Agosto";
            hoja.Cells["I2"].Style.Font.Bold = true;
            hoja.Cells["J2"].Value = "Septiembre";
            hoja.Cells["J2"].Style.Font.Bold = true;
            hoja.Cells["K2"].Value = "Octubre";
            hoja.Cells["K2"].Style.Font.Bold = true;
            hoja.Cells["L2"].Value = "Noviembre";
            hoja.Cells["L2"].Style.Font.Bold = true;
            hoja.Cells["M2"].Value = "Diciembre";
            hoja.Cells["M2"].Style.Font.Bold = true;
            hoja.Cells["N2"].Value = "Total";
            hoja.Cells["N2"].Style.Font.Bold = true;

            int nunInicial = 3;

            foreach (var dato in DatosReporte)
            {
                hoja.Cells[string.Format("A{0}", nunInicial)].Value = dato.Evento;
                hoja.Cells[string.Format("A{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].Value = dato.Ene;
                hoja.Cells[string.Format("B{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("C{0}", nunInicial)].Value = dato.Feb;
                hoja.Cells[string.Format("C{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("C{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("D{0}", nunInicial)].Value = dato.Mar;
                hoja.Cells[string.Format("D{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("D{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("E{0}", nunInicial)].Value = dato.Abr;
                hoja.Cells[string.Format("E{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("E{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("F{0}", nunInicial)].Value = dato.May;
                hoja.Cells[string.Format("F{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("F{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("G{0}", nunInicial)].Value = dato.Jun;
                hoja.Cells[string.Format("G{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("G{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("H{0}", nunInicial)].Value = dato.Jul;
                hoja.Cells[string.Format("H{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("H{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("I{0}", nunInicial)].Value = dato.Ago;
                hoja.Cells[string.Format("I{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("I{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("J{0}", nunInicial)].Value = dato.Sep;
                hoja.Cells[string.Format("J{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("J{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("K{0}", nunInicial)].Value = dato.Oct;
                hoja.Cells[string.Format("K{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("K{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("L{0}", nunInicial)].Value = dato.Nov;
                hoja.Cells[string.Format("L{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("L{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("M{0}", nunInicial)].Value = dato.Dic;
                hoja.Cells[string.Format("M{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("M{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("N{0}", nunInicial)].Value = dato.Total;
                hoja.Cells[string.Format("N{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("N{0}", nunInicial)].OfType<decimal>();

                nunInicial++;
            }

            hoja.Column(1).Width = 40;

            foreach (var cel in hoja.Cells[string.Format("A1:N{0}", DatosReporte.Count + 2)])
            {
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            }

            //AGREGAMOS LA GRAFICA
            var chart = hoja.Drawings.AddChart("Dias ausentismo por Departamentos", OfficeOpenXml.Drawing.Chart.eChartType.ColumnClustered3D);
            chart.Title.Text = "Dias ausentismo por Departamentos";
            chart.SetPosition(1, 0, 15, 0);
            chart.SetSize(600, 400); // Tamaño de la gráfica
            chart.Legend.Remove(); // Si desea eliminar la leyenda

            // Define donde está la información de la gráfica.
            var serie = chart.Series.Add(hoja.Cells[string.Format("N3:N{0}", DatosReporte.Count + 2)], hoja.Cells[string.Format("A3:A{0}", DatosReporte.Count + 2)]);

            //FileInfo fileInfo = new FileInfo(@"D:\Archivos\Actividades.xlsx");
            //excel.SaveAs(fileInfo);
            return excel.GetAsByteArray();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="anio"></param>
        /// <returns></returns>
        public List<EDReportes> ConsultarPorEventos(EDReportes edReporte)
        {
            var reporteDefinitivo = new List<EDReportes>();
            var respuesta = reportesMG.ReporteEvento(edReporte);
            reporteDefinitivo = GenerarInformacionReportes(respuesta).ToList();
            return reporteDefinitivo;
        }

        /// <summary>
        /// genera el excel para el informe: Número de eventos por contingencia
        /// </summary>
        /// <param name="edReporte"></param>
        /// <returns></returns>
        public byte[] ConsultarPorEventosExcel(EDReportes edReporte)
        {
            List<EDReportes> DatosReporte = ConsultarPorEventos(edReporte);

            ExcelPackage excel = new ExcelPackage();
            excel.Workbook.Worksheets.Add("Número eventos x contingencia");                                           

            ExcelWorksheet hoja = excel.Workbook.Worksheets[1];

            hoja.Cells["A1:N1"].Merge = true;
            hoja.Cells["A1"].Value = "Número de eventos por contingencia ";
            hoja.Cells["A1"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["A1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["A1"].Style.Font.Bold = true;
            hoja.Cells["A1"].Style.WrapText = true;

            hoja.Cells["A2"].Value = "Contingencia";
            hoja.Cells["A2"].Style.Font.Bold = true;
            hoja.Cells["B2"].Value = "Enero";
            hoja.Cells["B2"].Style.Font.Bold = true;
            hoja.Cells["C2"].Value = "Febrero";
            hoja.Cells["C2"].Style.Font.Bold = true;
            hoja.Cells["D2"].Value = "Marzo";
            hoja.Cells["D2"].Style.Font.Bold = true;
            hoja.Cells["E2"].Value = "Abril";
            hoja.Cells["E2"].Style.Font.Bold = true;
            hoja.Cells["F2"].Value = "Mayo";
            hoja.Cells["F2"].Style.Font.Bold = true;
            hoja.Cells["G2"].Value = "Junio";
            hoja.Cells["G2"].Style.Font.Bold = true;
            hoja.Cells["H2"].Value = "Julio";
            hoja.Cells["H2"].Style.Font.Bold = true;
            hoja.Cells["I2"].Value = "Agosto";
            hoja.Cells["I2"].Style.Font.Bold = true;
            hoja.Cells["J2"].Value = "Septiembre";
            hoja.Cells["J2"].Style.Font.Bold = true;
            hoja.Cells["K2"].Value = "Octubre";
            hoja.Cells["K2"].Style.Font.Bold = true;
            hoja.Cells["L2"].Value = "Noviembre";
            hoja.Cells["L2"].Style.Font.Bold = true;
            hoja.Cells["M2"].Value = "Diciembre";
            hoja.Cells["M2"].Style.Font.Bold = true;
            hoja.Cells["N2"].Value = "Total";
            hoja.Cells["N2"].Style.Font.Bold = true;

            int nunInicial = 3;

            foreach (var dato in DatosReporte)
            {
                hoja.Cells[string.Format("A{0}", nunInicial)].Value = dato.CONTINGENCIA;
                hoja.Cells[string.Format("A{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].Value = dato.Ene;
                hoja.Cells[string.Format("B{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("C{0}", nunInicial)].Value = dato.Feb;
                hoja.Cells[string.Format("C{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("C{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("D{0}", nunInicial)].Value = dato.Mar;
                hoja.Cells[string.Format("D{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("D{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("E{0}", nunInicial)].Value = dato.Abr;
                hoja.Cells[string.Format("E{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("E{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("F{0}", nunInicial)].Value = dato.May;
                hoja.Cells[string.Format("F{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("F{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("G{0}", nunInicial)].Value = dato.Jun;
                hoja.Cells[string.Format("G{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("G{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("H{0}", nunInicial)].Value = dato.Jul;
                hoja.Cells[string.Format("H{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("H{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("I{0}", nunInicial)].Value = dato.Ago;
                hoja.Cells[string.Format("I{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("I{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("J{0}", nunInicial)].Value = dato.Sep;
                hoja.Cells[string.Format("J{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("J{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("K{0}", nunInicial)].Value = dato.Oct;
                hoja.Cells[string.Format("K{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("K{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("L{0}", nunInicial)].Value = dato.Nov;
                hoja.Cells[string.Format("L{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("L{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("M{0}", nunInicial)].Value = dato.Dic;
                hoja.Cells[string.Format("M{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("M{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("N{0}", nunInicial)].Value = dato.Total;
                hoja.Cells[string.Format("N{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("N{0}", nunInicial)].OfType<decimal>();

                nunInicial++;
            }

            hoja.Column(1).Width = 40;

            foreach (var cel in hoja.Cells[string.Format("A1:N{0}", DatosReporte.Count + 2)])
            {
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            }

            //AGREGAMOS LA GRAFICA
            var chart = hoja.Drawings.AddChart("Número de eventos por contingencia", OfficeOpenXml.Drawing.Chart.eChartType.ColumnClustered3D);
            chart.Title.Text = "Número de eventos por contingencia";
            chart.SetPosition(1, 0, 15, 0);
            chart.SetSize(600, 400); // Tamaño de la gráfica
            chart.Legend.Remove(); // Si desea eliminar la leyenda

            // Define donde está la información de la gráfica.
            var serie = chart.Series.Add(hoja.Cells[string.Format("N3:N{0}", DatosReporte.Count + 2)], hoja.Cells[string.Format("A3:A{0}", DatosReporte.Count + 2)]);

            //FileInfo fileInfo = new FileInfo(@"D:\Archivos\Actividades.xlsx");
            //excel.SaveAs(fileInfo);
            return excel.GetAsByteArray();
        }
        

        public List<EDReportes> ConsultarPorEnfermedades(EDReportes edReporte)
        {
            List<EDReportesGenerados> respuestas = new List<EDReportesGenerados>();
            var reporteDefinitivo = new List<EDReportes>();
            var edrespuestas = reportesMG.ReporteDiasAusentismoEnfermedades(edReporte);

            int diasHablides = ausenMg.ObtenerDiasLaborablesEmpresa(edReporte.nitEmpresa);
            if (diasHablides == 1)
                diasHablides = 5;
            else if (diasHablides == 2)
                diasHablides = 6;

            foreach (var edrespuesta in edrespuestas)
            {
                respuestas.AddRange(ObtenerDiasCadaMesEvento(edrespuesta, diasHablides));
            }
            reporteDefinitivo = GenerarInformacionReportes(respuestas).ToList();
            return reporteDefinitivo;
        }

        /// <summary>
        /// Genera el archivo excel para el informe: Dias ausentismo por capitulos de enfermedades CIE10
        /// </summary>
        /// <param name="edReporte"></param>
        /// <returns></returns>
        public byte[] ConsultarPorEnfermedadesExcel(EDReportes edReporte)
        {
            List<EDReportes> DatosReporte = ConsultarPorEnfermedades(edReporte);

            ExcelPackage excel = new ExcelPackage();
            excel.Workbook.Worksheets.Add("Dias ausent x enferme CIE10");                                           

            ExcelWorksheet hoja = excel.Workbook.Worksheets[1];

            hoja.Cells["A1:N1"].Merge = true;
            hoja.Cells["A1"].Value = "Dias ausentismo por capitulos de enfermedades CIE10 ";
            hoja.Cells["A1"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["A1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["A1"].Style.Font.Bold = true;
            hoja.Cells["A1"].Style.WrapText = true;

            hoja.Cells["A2"].Value = "Enfermedad";
            hoja.Cells["A2"].Style.Font.Bold = true;
            hoja.Cells["B2"].Value = "Enero";
            hoja.Cells["B2"].Style.Font.Bold = true;
            hoja.Cells["C2"].Value = "Febrero";
            hoja.Cells["C2"].Style.Font.Bold = true;
            hoja.Cells["D2"].Value = "Marzo";
            hoja.Cells["D2"].Style.Font.Bold = true;
            hoja.Cells["E2"].Value = "Abril";
            hoja.Cells["E2"].Style.Font.Bold = true;
            hoja.Cells["F2"].Value = "Mayo";
            hoja.Cells["F2"].Style.Font.Bold = true;
            hoja.Cells["G2"].Value = "Junio";
            hoja.Cells["G2"].Style.Font.Bold = true;
            hoja.Cells["H2"].Value = "Julio";
            hoja.Cells["H2"].Style.Font.Bold = true;
            hoja.Cells["I2"].Value = "Agosto";
            hoja.Cells["I2"].Style.Font.Bold = true;
            hoja.Cells["J2"].Value = "Septiembre";
            hoja.Cells["J2"].Style.Font.Bold = true;
            hoja.Cells["K2"].Value = "Octubre";
            hoja.Cells["K2"].Style.Font.Bold = true;
            hoja.Cells["L2"].Value = "Noviembre";
            hoja.Cells["L2"].Style.Font.Bold = true;
            hoja.Cells["M2"].Value = "Diciembre";
            hoja.Cells["M2"].Style.Font.Bold = true;
            hoja.Cells["N2"].Value = "Total";
            hoja.Cells["N2"].Style.Font.Bold = true;

            int nunInicial = 3;

            foreach (var dato in DatosReporte)
            {
                hoja.Cells[string.Format("A{0}", nunInicial)].Value = dato.Evento;
                hoja.Cells[string.Format("A{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].Value = dato.Ene;
                hoja.Cells[string.Format("B{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("C{0}", nunInicial)].Value = dato.Feb;
                hoja.Cells[string.Format("C{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("C{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("D{0}", nunInicial)].Value = dato.Mar;
                hoja.Cells[string.Format("D{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("D{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("E{0}", nunInicial)].Value = dato.Abr;
                hoja.Cells[string.Format("E{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("E{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("F{0}", nunInicial)].Value = dato.May;
                hoja.Cells[string.Format("F{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("F{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("G{0}", nunInicial)].Value = dato.Jun;
                hoja.Cells[string.Format("G{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("G{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("H{0}", nunInicial)].Value = dato.Jul;
                hoja.Cells[string.Format("H{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("H{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("I{0}", nunInicial)].Value = dato.Ago;
                hoja.Cells[string.Format("I{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("I{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("J{0}", nunInicial)].Value = dato.Sep;
                hoja.Cells[string.Format("J{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("J{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("K{0}", nunInicial)].Value = dato.Oct;
                hoja.Cells[string.Format("K{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("K{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("L{0}", nunInicial)].Value = dato.Nov;
                hoja.Cells[string.Format("L{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("L{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("M{0}", nunInicial)].Value = dato.Dic;
                hoja.Cells[string.Format("M{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("M{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("N{0}", nunInicial)].Value = dato.Total;
                hoja.Cells[string.Format("N{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("N{0}", nunInicial)].OfType<decimal>();

                nunInicial++;
            }

            hoja.Column(1).Width = 40;

            foreach (var cel in hoja.Cells[string.Format("A1:N{0}", DatosReporte.Count + 2)])
            {
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            }

            //AGREGAMOS LA GRAFICA
            var chart = hoja.Drawings.AddChart("Dias ausentismo por capitulos de enfermedades CIE10", OfficeOpenXml.Drawing.Chart.eChartType.ColumnClustered3D);
            chart.Title.Text = "Dias ausentismo por capitulos de enfermedades CIE10";
            chart.SetPosition(1, 0, 15, 0);
            chart.SetSize(600, 400); // Tamaño de la gráfica
            chart.Legend.Remove(); // Si desea eliminar la leyenda

            // Define donde está la información de la gráfica.
            var serie = chart.Series.Add(hoja.Cells[string.Format("N3:N{0}", DatosReporte.Count + 2)], hoja.Cells[string.Format("A3:A{0}", DatosReporte.Count + 2)]);

            //FileInfo fileInfo = new FileInfo(@"D:\Archivos\Actividades.xlsx");
            //excel.SaveAs(fileInfo);
            return excel.GetAsByteArray();
        }
        

        public List<EDReportes> ConsultarPorEps(EDReportes edReporte)
        {
            var reporteDefinitivo = new List<EDReportes>();
            var respuesta = reportesMG.ReporteDiasEps(edReporte);
            reporteDefinitivo = GenerarInformacionReportes(respuesta).ToList();
            return reporteDefinitivo;
        }

        /// <summary>
        /// Genera el archivo excel para el informe: Dias por eps
        /// </summary>
        /// <param name="edReporte"></param>
        /// <returns></returns>
        public byte[] ConsultarPorEpsExcel(EDReportes edReporte)
        {
            List<EDReportes> DatosReporte = ConsultarPorEps(edReporte);

            ExcelPackage excel = new ExcelPackage();
            excel.Workbook.Worksheets.Add("Ausentismos por EPS");

            ExcelWorksheet hoja = excel.Workbook.Worksheets[1];

            hoja.Cells["A1:N1"].Merge = true;
            hoja.Cells["A1"].Value = "Ausentismos por EPS ";
            hoja.Cells["A1"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["A1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["A1"].Style.Font.Bold = true;
            hoja.Cells["A1"].Style.WrapText = true;

            hoja.Cells["A2"].Value = "EPS";
            hoja.Cells["A2"].Style.Font.Bold = true;
            hoja.Cells["B2"].Value = "Enero";
            hoja.Cells["B2"].Style.Font.Bold = true;
            hoja.Cells["C2"].Value = "Febrero";
            hoja.Cells["C2"].Style.Font.Bold = true;
            hoja.Cells["D2"].Value = "Marzo";
            hoja.Cells["D2"].Style.Font.Bold = true;
            hoja.Cells["E2"].Value = "Abril";
            hoja.Cells["E2"].Style.Font.Bold = true;
            hoja.Cells["F2"].Value = "Mayo";
            hoja.Cells["F2"].Style.Font.Bold = true;
            hoja.Cells["G2"].Value = "Junio";
            hoja.Cells["G2"].Style.Font.Bold = true;
            hoja.Cells["H2"].Value = "Julio";
            hoja.Cells["H2"].Style.Font.Bold = true;
            hoja.Cells["I2"].Value = "Agosto";
            hoja.Cells["I2"].Style.Font.Bold = true;
            hoja.Cells["J2"].Value = "Septiembre";
            hoja.Cells["J2"].Style.Font.Bold = true;
            hoja.Cells["K2"].Value = "Octubre";
            hoja.Cells["K2"].Style.Font.Bold = true;
            hoja.Cells["L2"].Value = "Noviembre";
            hoja.Cells["L2"].Style.Font.Bold = true;
            hoja.Cells["M2"].Value = "Diciembre";
            hoja.Cells["M2"].Style.Font.Bold = true;
            hoja.Cells["N2"].Value = "Total";
            hoja.Cells["N2"].Style.Font.Bold = true;

            int nunInicial = 3;

            foreach (var dato in DatosReporte)
            {
                hoja.Cells[string.Format("A{0}", nunInicial)].Value = dato.Evento;
                hoja.Cells[string.Format("A{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].Value = dato.Ene;
                hoja.Cells[string.Format("B{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("C{0}", nunInicial)].Value = dato.Feb;
                hoja.Cells[string.Format("C{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("C{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("D{0}", nunInicial)].Value = dato.Mar;
                hoja.Cells[string.Format("D{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("D{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("E{0}", nunInicial)].Value = dato.Abr;
                hoja.Cells[string.Format("E{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("E{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("F{0}", nunInicial)].Value = dato.May;
                hoja.Cells[string.Format("F{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("F{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("G{0}", nunInicial)].Value = dato.Jun;
                hoja.Cells[string.Format("G{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("G{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("H{0}", nunInicial)].Value = dato.Jul;
                hoja.Cells[string.Format("H{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("H{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("I{0}", nunInicial)].Value = dato.Ago;
                hoja.Cells[string.Format("I{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("I{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("J{0}", nunInicial)].Value = dato.Sep;
                hoja.Cells[string.Format("J{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("J{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("K{0}", nunInicial)].Value = dato.Oct;
                hoja.Cells[string.Format("K{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("K{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("L{0}", nunInicial)].Value = dato.Nov;
                hoja.Cells[string.Format("L{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("L{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("M{0}", nunInicial)].Value = dato.Dic;
                hoja.Cells[string.Format("M{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("M{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("N{0}", nunInicial)].Value = dato.Total;
                hoja.Cells[string.Format("N{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("N{0}", nunInicial)].OfType<decimal>();

                nunInicial++;
            }

            hoja.Column(1).Width = 40;

            foreach (var cel in hoja.Cells[string.Format("A1:N{0}", DatosReporte.Count + 2)])
            {
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            }

            //AGREGAMOS LA GRAFICA
            var chart = hoja.Drawings.AddChart("Ausentismos por EPS", OfficeOpenXml.Drawing.Chart.eChartType.ColumnClustered3D);
            chart.Title.Text = "Ausentismos por EPS";
            chart.SetPosition(1, 0, 15, 0);
            chart.SetSize(600, 400); // Tamaño de la gráfica
            chart.Legend.Remove(); // Si desea eliminar la leyenda

            // Define donde está la información de la gráfica.
            var serie = chart.Series.Add(hoja.Cells[string.Format("N3:N{0}", DatosReporte.Count + 2)], hoja.Cells[string.Format("A3:A{0}", DatosReporte.Count + 2)]);

            //FileInfo fileInfo = new FileInfo(@"D:\Archivos\Actividades.xlsx");
            //excel.SaveAs(fileInfo);
            return excel.GetAsByteArray();
        }

        /// <summary>
        /// Se consultan las ausencias por tipo de vinculación.
        /// </summary>
        /// <param name="edReporte"></param>
        /// <returns></returns>
        public List<EDReportes> ConsultarAusenciasPorVinculacion(EDReportes edReporte)
        {
            var reporteDefinitivo = new List<EDReportes>();
            var respuesta = reportesMG.ReporteVincualcion(edReporte);
            reporteDefinitivo = GenerarInformacionReportes(respuesta).ToList();
            return reporteDefinitivo;
        }

        /// <summary>
        /// Genera el archivo excel para el informe: Dias ausentismo por capitulos de enfermedades CIE10
        /// </summary>
        /// <param name="edReporte"></param>
        /// <returns></returns>
        public byte[] ConsultarAusenciasPorVinculacionExcel(EDReportes edReporte)
        {
            List<EDReportes> DatosReporte = ConsultarAusenciasPorVinculacion(edReporte);

            ExcelPackage excel = new ExcelPackage();
            excel.Workbook.Worksheets.Add("Ausentismos x ocupacion CIUO");

            ExcelWorksheet hoja = excel.Workbook.Worksheets[1];

            hoja.Cells["A1:N1"].Merge = true;
            hoja.Cells["A1"].Value = "Ausentismos por ocupacion CIUO ";
            hoja.Cells["A1"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["A1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["A1"].Style.Font.Bold = true;
            hoja.Cells["A1"].Style.WrapText = true;

            hoja.Cells["A2"].Value = "Ocupacion";
            hoja.Cells["A2"].Style.Font.Bold = true;
            hoja.Cells["B2"].Value = "Enero";
            hoja.Cells["B2"].Style.Font.Bold = true;
            hoja.Cells["C2"].Value = "Febrero";
            hoja.Cells["C2"].Style.Font.Bold = true;
            hoja.Cells["D2"].Value = "Marzo";
            hoja.Cells["D2"].Style.Font.Bold = true;
            hoja.Cells["E2"].Value = "Abril";
            hoja.Cells["E2"].Style.Font.Bold = true;
            hoja.Cells["F2"].Value = "Mayo";
            hoja.Cells["F2"].Style.Font.Bold = true;
            hoja.Cells["G2"].Value = "Junio";
            hoja.Cells["G2"].Style.Font.Bold = true;
            hoja.Cells["H2"].Value = "Julio";
            hoja.Cells["H2"].Style.Font.Bold = true;
            hoja.Cells["I2"].Value = "Agosto";
            hoja.Cells["I2"].Style.Font.Bold = true;
            hoja.Cells["J2"].Value = "Septiembre";
            hoja.Cells["J2"].Style.Font.Bold = true;
            hoja.Cells["K2"].Value = "Octubre";
            hoja.Cells["K2"].Style.Font.Bold = true;
            hoja.Cells["L2"].Value = "Noviembre";
            hoja.Cells["L2"].Style.Font.Bold = true;
            hoja.Cells["M2"].Value = "Diciembre";
            hoja.Cells["M2"].Style.Font.Bold = true;
            hoja.Cells["N2"].Value = "Total";
            hoja.Cells["N2"].Style.Font.Bold = true;

            int nunInicial = 3;

            foreach (var dato in DatosReporte)
            {
                hoja.Cells[string.Format("A{0}", nunInicial)].Value = dato.Evento;
                hoja.Cells[string.Format("A{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].Value = dato.Ene;
                hoja.Cells[string.Format("B{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("C{0}", nunInicial)].Value = dato.Feb;
                hoja.Cells[string.Format("C{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("C{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("D{0}", nunInicial)].Value = dato.Mar;
                hoja.Cells[string.Format("D{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("D{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("E{0}", nunInicial)].Value = dato.Abr;
                hoja.Cells[string.Format("E{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("E{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("F{0}", nunInicial)].Value = dato.May;
                hoja.Cells[string.Format("F{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("F{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("G{0}", nunInicial)].Value = dato.Jun;
                hoja.Cells[string.Format("G{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("G{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("H{0}", nunInicial)].Value = dato.Jul;
                hoja.Cells[string.Format("H{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("H{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("I{0}", nunInicial)].Value = dato.Ago;
                hoja.Cells[string.Format("I{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("I{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("J{0}", nunInicial)].Value = dato.Sep;
                hoja.Cells[string.Format("J{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("J{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("K{0}", nunInicial)].Value = dato.Oct;
                hoja.Cells[string.Format("K{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("K{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("L{0}", nunInicial)].Value = dato.Nov;
                hoja.Cells[string.Format("L{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("L{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("M{0}", nunInicial)].Value = dato.Dic;
                hoja.Cells[string.Format("M{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("M{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("N{0}", nunInicial)].Value = dato.Total;
                hoja.Cells[string.Format("N{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("N{0}", nunInicial)].OfType<decimal>();

                nunInicial++;
            }

            hoja.Column(1).Width = 40;

            foreach (var cel in hoja.Cells[string.Format("A1:N{0}", DatosReporte.Count + 2)])
            {
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            }

            //AGREGAMOS LA GRAFICA
            var chart = hoja.Drawings.AddChart("Ausentismos por ocupacion CIUO", OfficeOpenXml.Drawing.Chart.eChartType.ColumnClustered3D);
            chart.Title.Text = "Ausentismos por ocupacion CIUO";
            chart.SetPosition(1, 0, 15, 0);
            chart.SetSize(600, 400); // Tamaño de la gráfica
            chart.Legend.Remove(); // Si desea eliminar la leyenda

            // Define donde está la información de la gráfica.
            var serie = chart.Series.Add(hoja.Cells[string.Format("N3:N{0}", DatosReporte.Count + 2)], hoja.Cells[string.Format("A3:A{0}", DatosReporte.Count + 2)]);

            //FileInfo fileInfo = new FileInfo(@"D:\Archivos\Actividades.xlsx");
            //excel.SaveAs(fileInfo);
            return excel.GetAsByteArray();
        }

        public List<EDReportes> ConsultarPorSede(EDReportes edReporte)
        {
            List<EDReportesGenerados> respuestas = new List<EDReportesGenerados>();
            var reporteDefinitivo = new List<EDReportes>();
            var edrespuestas = reportesMG.ReporteSede(edReporte);

            int diasHablides = ausenMg.ObtenerDiasLaborablesEmpresa(edReporte.nitEmpresa);
            if (diasHablides == 1)
                diasHablides = 5;
            else if (diasHablides == 2)
                diasHablides = 6;

            foreach (var edrespuesta in edrespuestas)
            {
                respuestas.AddRange(ObtenerDiasCadaMesEvento(edrespuesta, diasHablides));
            }
            reporteDefinitivo = GenerarInformacionReportes(respuestas).ToList();
            return reporteDefinitivo;
        }

        /// <summary>
        /// Genera el excel para el reporte: Dias de ausentismo por Sede
        /// </summary>
        /// <param name="edReporte"></param>
        /// <returns></returns>
        public byte[] ConsultarPorSedeExcel(EDReportes edReporte)
        {
            List<EDReportes> DatosReporte = ConsultarPorSede(edReporte);

            ExcelPackage excel = new ExcelPackage();
            excel.Workbook.Worksheets.Add("Dias de ausentismo por Sede");

            ExcelWorksheet hoja = excel.Workbook.Worksheets[1];

            hoja.Cells["A1:N1"].Merge = true;
            hoja.Cells["A1"].Value = "Dias de ausentismo por Sede ";
            hoja.Cells["A1"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["A1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["A1"].Style.Font.Bold = true;
            hoja.Cells["A1"].Style.WrapText = true;

            hoja.Cells["A2"].Value = "Sede";
            hoja.Cells["A2"].Style.Font.Bold = true;
            hoja.Cells["B2"].Value = "Enero";
            hoja.Cells["B2"].Style.Font.Bold = true;
            hoja.Cells["C2"].Value = "Febrero";
            hoja.Cells["C2"].Style.Font.Bold = true;
            hoja.Cells["D2"].Value = "Marzo";
            hoja.Cells["D2"].Style.Font.Bold = true;
            hoja.Cells["E2"].Value = "Abril";
            hoja.Cells["E2"].Style.Font.Bold = true;
            hoja.Cells["F2"].Value = "Mayo";
            hoja.Cells["F2"].Style.Font.Bold = true;
            hoja.Cells["G2"].Value = "Junio";
            hoja.Cells["G2"].Style.Font.Bold = true;
            hoja.Cells["H2"].Value = "Julio";
            hoja.Cells["H2"].Style.Font.Bold = true;
            hoja.Cells["I2"].Value = "Agosto";
            hoja.Cells["I2"].Style.Font.Bold = true;
            hoja.Cells["J2"].Value = "Septiembre";
            hoja.Cells["J2"].Style.Font.Bold = true;
            hoja.Cells["K2"].Value = "Octubre";
            hoja.Cells["K2"].Style.Font.Bold = true;
            hoja.Cells["L2"].Value = "Noviembre";
            hoja.Cells["L2"].Style.Font.Bold = true;
            hoja.Cells["M2"].Value = "Diciembre";
            hoja.Cells["M2"].Style.Font.Bold = true;
            hoja.Cells["N2"].Value = "Total";
            hoja.Cells["N2"].Style.Font.Bold = true;

            int nunInicial = 3;

            foreach (var dato in DatosReporte)
            {
                hoja.Cells[string.Format("A{0}", nunInicial)].Value = dato.Evento;
                hoja.Cells[string.Format("A{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].Value = dato.Ene;
                hoja.Cells[string.Format("B{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("C{0}", nunInicial)].Value = dato.Feb;
                hoja.Cells[string.Format("C{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("C{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("D{0}", nunInicial)].Value = dato.Mar;
                hoja.Cells[string.Format("D{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("D{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("E{0}", nunInicial)].Value = dato.Abr;
                hoja.Cells[string.Format("E{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("E{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("F{0}", nunInicial)].Value = dato.May;
                hoja.Cells[string.Format("F{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("F{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("G{0}", nunInicial)].Value = dato.Jun;
                hoja.Cells[string.Format("G{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("G{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("H{0}", nunInicial)].Value = dato.Jul;
                hoja.Cells[string.Format("H{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("H{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("I{0}", nunInicial)].Value = dato.Ago;
                hoja.Cells[string.Format("I{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("I{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("J{0}", nunInicial)].Value = dato.Sep;
                hoja.Cells[string.Format("J{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("J{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("K{0}", nunInicial)].Value = dato.Oct;
                hoja.Cells[string.Format("K{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("K{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("L{0}", nunInicial)].Value = dato.Nov;
                hoja.Cells[string.Format("L{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("L{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("M{0}", nunInicial)].Value = dato.Dic;
                hoja.Cells[string.Format("M{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("M{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("N{0}", nunInicial)].Value = dato.Total;
                hoja.Cells[string.Format("N{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("N{0}", nunInicial)].OfType<decimal>();

                nunInicial++;
            }

            hoja.Column(1).Width = 40;

            foreach (var cel in hoja.Cells[string.Format("A1:N{0}", DatosReporte.Count + 2)])
            {
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            }

            //AGREGAMOS LA GRAFICA
            var chart = hoja.Drawings.AddChart("Dias de ausentismo por Sede", OfficeOpenXml.Drawing.Chart.eChartType.ColumnClustered3D);
            chart.Title.Text = "Dias de ausentismo por Sede";
            chart.SetPosition(1, 0, 15, 0);
            chart.SetSize(600, 400); // Tamaño de la gráfica
            chart.Legend.Remove(); // Si desea eliminar la leyenda

            // Define donde está la información de la gráfica.
            var serie = chart.Series.Add(hoja.Cells[string.Format("N3:N{0}", DatosReporte.Count + 2)], hoja.Cells[string.Format("A3:A{0}", DatosReporte.Count + 2)]);

            //FileInfo fileInfo = new FileInfo(@"D:\Archivos\Actividades.xlsx");
            //excel.SaveAs(fileInfo);
            return excel.GetAsByteArray();
        }

        public List<EDReportes> ConsultarPorSexo(EDReportes edReporte)
        {
            var reporteDefinitivo = new List<EDReportes>();
            var respuesta = reportesMG.ReporteSexo(edReporte);
            reporteDefinitivo = GenerarInformacionReportes(respuesta).ToList();
            return reporteDefinitivo;
        }

        /// <summary>
        /// Genera el excel para el reporte: Ausentismos por sexo
        /// </summary>
        /// <param name="edReporte"></param>
        /// <returns></returns>
        public byte[] ConsultarPorSexoExcel(EDReportes edReporte)
        {
            List<EDReportes> DatosReporte = ConsultarPorSexo(edReporte);

            ExcelPackage excel = new ExcelPackage();
            excel.Workbook.Worksheets.Add("Ausentismos por sexo");

            ExcelWorksheet hoja = excel.Workbook.Worksheets[1];

            hoja.Cells["A1:N1"].Merge = true;
            hoja.Cells["A1"].Value = "Ausentismos por sexo ";
            hoja.Cells["A1"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["A1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["A1"].Style.Font.Bold = true;
            hoja.Cells["A1"].Style.WrapText = true;

            hoja.Cells["A2"].Value = "Genero";
            hoja.Cells["A2"].Style.Font.Bold = true;
            hoja.Cells["B2"].Value = "Enero";
            hoja.Cells["B2"].Style.Font.Bold = true;
            hoja.Cells["C2"].Value = "Febrero";
            hoja.Cells["C2"].Style.Font.Bold = true;
            hoja.Cells["D2"].Value = "Marzo";
            hoja.Cells["D2"].Style.Font.Bold = true;
            hoja.Cells["E2"].Value = "Abril";
            hoja.Cells["E2"].Style.Font.Bold = true;
            hoja.Cells["F2"].Value = "Mayo";
            hoja.Cells["F2"].Style.Font.Bold = true;
            hoja.Cells["G2"].Value = "Junio";
            hoja.Cells["G2"].Style.Font.Bold = true;
            hoja.Cells["H2"].Value = "Julio";
            hoja.Cells["H2"].Style.Font.Bold = true;
            hoja.Cells["I2"].Value = "Agosto";
            hoja.Cells["I2"].Style.Font.Bold = true;
            hoja.Cells["J2"].Value = "Septiembre";
            hoja.Cells["J2"].Style.Font.Bold = true;
            hoja.Cells["K2"].Value = "Octubre";
            hoja.Cells["K2"].Style.Font.Bold = true;
            hoja.Cells["L2"].Value = "Noviembre";
            hoja.Cells["L2"].Style.Font.Bold = true;
            hoja.Cells["M2"].Value = "Diciembre";
            hoja.Cells["M2"].Style.Font.Bold = true;
            hoja.Cells["N2"].Value = "Total";
            hoja.Cells["N2"].Style.Font.Bold = true;

            int nunInicial = 3;

            foreach (var dato in DatosReporte)
            {
                hoja.Cells[string.Format("A{0}", nunInicial)].Value = dato.Evento.Equals("M") ? "Masculino" : "Femenino";
                hoja.Cells[string.Format("A{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].Value = dato.Ene;
                hoja.Cells[string.Format("B{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("C{0}", nunInicial)].Value = dato.Feb;
                hoja.Cells[string.Format("C{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("C{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("D{0}", nunInicial)].Value = dato.Mar;
                hoja.Cells[string.Format("D{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("D{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("E{0}", nunInicial)].Value = dato.Abr;
                hoja.Cells[string.Format("E{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("E{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("F{0}", nunInicial)].Value = dato.May;
                hoja.Cells[string.Format("F{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("F{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("G{0}", nunInicial)].Value = dato.Jun;
                hoja.Cells[string.Format("G{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("G{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("H{0}", nunInicial)].Value = dato.Jul;
                hoja.Cells[string.Format("H{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("H{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("I{0}", nunInicial)].Value = dato.Ago;
                hoja.Cells[string.Format("I{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("I{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("J{0}", nunInicial)].Value = dato.Sep;
                hoja.Cells[string.Format("J{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("J{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("K{0}", nunInicial)].Value = dato.Oct;
                hoja.Cells[string.Format("K{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("K{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("L{0}", nunInicial)].Value = dato.Nov;
                hoja.Cells[string.Format("L{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("L{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("M{0}", nunInicial)].Value = dato.Dic;
                hoja.Cells[string.Format("M{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("M{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("N{0}", nunInicial)].Value = dato.Total;
                hoja.Cells[string.Format("N{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("N{0}", nunInicial)].OfType<decimal>();

                nunInicial++;
            }

            hoja.Column(1).Width = 40;

            foreach (var cel in hoja.Cells[string.Format("A1:N{0}", DatosReporte.Count + 2)])
            {
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            }

            //AGREGAMOS LA GRAFICA
            var chart = hoja.Drawings.AddChart("Ausentismos por sexo", OfficeOpenXml.Drawing.Chart.eChartType.ColumnClustered3D);
            chart.Title.Text = "Ausentismos por sexo";
            chart.SetPosition(1, 0, 15, 0);
            chart.SetSize(600, 400); // Tamaño de la gráfica
            chart.Legend.Remove(); // Si desea eliminar la leyenda

            // Define donde está la información de la gráfica.
            var serie = chart.Series.Add(hoja.Cells[string.Format("N3:N{0}", DatosReporte.Count + 2)], hoja.Cells[string.Format("A3:A{0}", DatosReporte.Count + 2)]);

            //FileInfo fileInfo = new FileInfo(@"D:\Archivos\Actividades.xlsx");
            //excel.SaveAs(fileInfo);
            return excel.GetAsByteArray();
        }


        public List<EDReportes> ConsultarPorCosto(EDReportes edReporte)
        {
            var reporteDefinitivo = new List<EDReportes>();
            var respuesta = reportesMG.ReporteCostoContingencia(edReporte);
            reporteDefinitivo = GenerarInformacionReportes(respuesta).ToList();
            return reporteDefinitivo;
        }

        /// <summary>
        /// Genera excel del reporte:Promedio de Costos por contingencias
        /// </summary>
        /// <param name="edReporte"></param>
        /// <returns></returns>
        public byte[] ConsultarPorCostoExcel(EDReportes edReporte)
        {
            List<EDReportes> DatosReporte = ConsultarPorCosto(edReporte);

            ExcelPackage excel = new ExcelPackage();
            excel.Workbook.Worksheets.Add("Promedio x Costos x contingen");

            ExcelWorksheet hoja = excel.Workbook.Worksheets[1];

            hoja.Cells["A1:N1"].Merge = true;
            hoja.Cells["A1"].Value = "Promedio de Costos por contingencias ";
            hoja.Cells["A1"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["A1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["A1"].Style.Font.Bold = true;
            hoja.Cells["A1"].Style.WrapText = true;

            hoja.Cells["A2"].Value = "Descripcion";
            hoja.Cells["A2"].Style.Font.Bold = true;
            hoja.Cells["B2"].Value = "Enero";
            hoja.Cells["B2"].Style.Font.Bold = true;
            hoja.Cells["C2"].Value = "Febrero";
            hoja.Cells["C2"].Style.Font.Bold = true;
            hoja.Cells["D2"].Value = "Marzo";
            hoja.Cells["D2"].Style.Font.Bold = true;
            hoja.Cells["E2"].Value = "Abril";
            hoja.Cells["E2"].Style.Font.Bold = true;
            hoja.Cells["F2"].Value = "Mayo";
            hoja.Cells["F2"].Style.Font.Bold = true;
            hoja.Cells["G2"].Value = "Junio";
            hoja.Cells["G2"].Style.Font.Bold = true;
            hoja.Cells["H2"].Value = "Julio";
            hoja.Cells["H2"].Style.Font.Bold = true;
            hoja.Cells["I2"].Value = "Agosto";
            hoja.Cells["I2"].Style.Font.Bold = true;
            hoja.Cells["J2"].Value = "Septiembre";
            hoja.Cells["J2"].Style.Font.Bold = true;
            hoja.Cells["K2"].Value = "Octubre";
            hoja.Cells["K2"].Style.Font.Bold = true;
            hoja.Cells["L2"].Value = "Noviembre";
            hoja.Cells["L2"].Style.Font.Bold = true;
            hoja.Cells["M2"].Value = "Diciembre";
            hoja.Cells["M2"].Style.Font.Bold = true;
            hoja.Cells["N2"].Value = "Total";
            hoja.Cells["N2"].Style.Font.Bold = true;

            int nunInicial = 3;

            foreach (var dato in DatosReporte)
            {
                hoja.Cells[string.Format("A{0}", nunInicial)].Value = dato.CONTINGENCIA;
                hoja.Cells[string.Format("A{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].Value = dato.Ene;
                hoja.Cells[string.Format("B{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("C{0}", nunInicial)].Value = dato.Feb;
                hoja.Cells[string.Format("C{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("C{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("D{0}", nunInicial)].Value = dato.Mar;
                hoja.Cells[string.Format("D{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("D{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("E{0}", nunInicial)].Value = dato.Abr;
                hoja.Cells[string.Format("E{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("E{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("F{0}", nunInicial)].Value = dato.May;
                hoja.Cells[string.Format("F{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("F{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("G{0}", nunInicial)].Value = dato.Jun;
                hoja.Cells[string.Format("G{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("G{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("H{0}", nunInicial)].Value = dato.Jul;
                hoja.Cells[string.Format("H{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("H{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("I{0}", nunInicial)].Value = dato.Ago;
                hoja.Cells[string.Format("I{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("I{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("J{0}", nunInicial)].Value = dato.Sep;
                hoja.Cells[string.Format("J{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("J{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("K{0}", nunInicial)].Value = dato.Oct;
                hoja.Cells[string.Format("K{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("K{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("L{0}", nunInicial)].Value = dato.Nov;
                hoja.Cells[string.Format("L{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("L{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("M{0}", nunInicial)].Value = dato.Dic;
                hoja.Cells[string.Format("M{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("M{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("N{0}", nunInicial)].Value = dato.Total;
                hoja.Cells[string.Format("N{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("N{0}", nunInicial)].OfType<decimal>();

                nunInicial++;
            }

            hoja.Column(1).Width = 40;

            foreach (var cel in hoja.Cells[string.Format("A1:N{0}", DatosReporte.Count + 2)])
            {
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            }

            //AGREGAMOS LA GRAFICA
            var chart = hoja.Drawings.AddChart("Promedio de Costos por contingencias", OfficeOpenXml.Drawing.Chart.eChartType.ColumnClustered3D);
            chart.Title.Text = "Promedio de Costos por contingencias";
            chart.SetPosition(1, 0, 15, 0);
            chart.SetSize(600, 400); // Tamaño de la gráfica
            chart.Legend.Remove(); // Si desea eliminar la leyenda

            // Define donde está la información de la gráfica.
            var serie = chart.Series.Add(hoja.Cells[string.Format("N3:N{0}", DatosReporte.Count + 2)], hoja.Cells[string.Format("A3:A{0}", DatosReporte.Count + 2)]);

            //FileInfo fileInfo = new FileInfo(@"D:\Archivos\Actividades.xlsx");
            //excel.SaveAs(fileInfo);
            return excel.GetAsByteArray();
        }

        public List<EDReportes> ReporteCantidadAusenciasPorOcupacion(EDReportes edReporte)
        {
            var reporteDefinitivo = new List<EDReportes>();
            var respuesta = reportesMG.ReporteCantidadAusenciasPorOcupacion(edReporte);
            reporteDefinitivo = GenerarInformacionReportes(respuesta).ToList();
            return reporteDefinitivo;
        }

        /// <summary>
        /// Genera excel para el reporte: Ausentismos por tipo vinculacion
        /// </summary>
        /// <param name="edReporte"></param>
        /// <returns></returns>
        public byte[] ReporteCantidadAusenciasPorOcupacionExcel(EDReportes edReporte)
        {
            List<EDReportes> DatosReporte = ReporteCantidadAusenciasPorOcupacion(edReporte);

            ExcelPackage excel = new ExcelPackage();
            excel.Workbook.Worksheets.Add("Ausentismos x tipo vinculacion");

            ExcelWorksheet hoja = excel.Workbook.Worksheets[1];

            hoja.Cells["A1:N1"].Merge = true;
            hoja.Cells["A1"].Value = "Ausentismos por tipo vinculacion ";
            hoja.Cells["A1"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["A1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["A1"].Style.Font.Bold = true;
            hoja.Cells["A1"].Style.WrapText = true;

            hoja.Cells["A2"].Value = "Tipo Vinculacion";
            hoja.Cells["A2"].Style.Font.Bold = true;
            hoja.Cells["B2"].Value = "Enero";
            hoja.Cells["B2"].Style.Font.Bold = true;
            hoja.Cells["C2"].Value = "Febrero";
            hoja.Cells["C2"].Style.Font.Bold = true;
            hoja.Cells["D2"].Value = "Marzo";
            hoja.Cells["D2"].Style.Font.Bold = true;
            hoja.Cells["E2"].Value = "Abril";
            hoja.Cells["E2"].Style.Font.Bold = true;
            hoja.Cells["F2"].Value = "Mayo";
            hoja.Cells["F2"].Style.Font.Bold = true;
            hoja.Cells["G2"].Value = "Junio";
            hoja.Cells["G2"].Style.Font.Bold = true;
            hoja.Cells["H2"].Value = "Julio";
            hoja.Cells["H2"].Style.Font.Bold = true;
            hoja.Cells["I2"].Value = "Agosto";
            hoja.Cells["I2"].Style.Font.Bold = true;
            hoja.Cells["J2"].Value = "Septiembre";
            hoja.Cells["J2"].Style.Font.Bold = true;
            hoja.Cells["K2"].Value = "Octubre";
            hoja.Cells["K2"].Style.Font.Bold = true;
            hoja.Cells["L2"].Value = "Noviembre";
            hoja.Cells["L2"].Style.Font.Bold = true;
            hoja.Cells["M2"].Value = "Diciembre";
            hoja.Cells["M2"].Style.Font.Bold = true;
            hoja.Cells["N2"].Value = "Total";
            hoja.Cells["N2"].Style.Font.Bold = true;

            int nunInicial = 3;

            foreach (var dato in DatosReporte)
            {
                hoja.Cells[string.Format("A{0}", nunInicial)].Value = dato.Evento;
                hoja.Cells[string.Format("A{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].Value = dato.Ene;
                hoja.Cells[string.Format("B{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("C{0}", nunInicial)].Value = dato.Feb;
                hoja.Cells[string.Format("C{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("C{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("D{0}", nunInicial)].Value = dato.Mar;
                hoja.Cells[string.Format("D{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("D{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("E{0}", nunInicial)].Value = dato.Abr;
                hoja.Cells[string.Format("E{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("E{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("F{0}", nunInicial)].Value = dato.May;
                hoja.Cells[string.Format("F{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("F{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("G{0}", nunInicial)].Value = dato.Jun;
                hoja.Cells[string.Format("G{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("G{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("H{0}", nunInicial)].Value = dato.Jul;
                hoja.Cells[string.Format("H{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("H{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("I{0}", nunInicial)].Value = dato.Ago;
                hoja.Cells[string.Format("I{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("I{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("J{0}", nunInicial)].Value = dato.Sep;
                hoja.Cells[string.Format("J{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("J{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("K{0}", nunInicial)].Value = dato.Oct;
                hoja.Cells[string.Format("K{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("K{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("L{0}", nunInicial)].Value = dato.Nov;
                hoja.Cells[string.Format("L{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("L{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("M{0}", nunInicial)].Value = dato.Dic;
                hoja.Cells[string.Format("M{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("M{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("N{0}", nunInicial)].Value = dato.Total;
                hoja.Cells[string.Format("N{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("N{0}", nunInicial)].OfType<decimal>();

                nunInicial++;
            }

            hoja.Column(1).Width = 40;

            foreach (var cel in hoja.Cells[string.Format("A1:N{0}", DatosReporte.Count + 2)])
            {
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            }

            //AGREGAMOS LA GRAFICA
            var chart = hoja.Drawings.AddChart("Ausentismos por tipo vinculacion", OfficeOpenXml.Drawing.Chart.eChartType.ColumnClustered3D);
            chart.Title.Text = "Ausentismos por tipo vinculacion";
            chart.SetPosition(1, 0, 15, 0);
            chart.SetSize(600, 400); // Tamaño de la gráfica
            chart.Legend.Remove(); // Si desea eliminar la leyenda

            // Define donde está la información de la gráfica.
            var serie = chart.Series.Add(hoja.Cells[string.Format("N3:N{0}", DatosReporte.Count + 2)], hoja.Cells[string.Format("A3:A{0}", DatosReporte.Count + 2)]);

            //FileInfo fileInfo = new FileInfo(@"D:\Archivos\Actividades.xlsx");
            //excel.SaveAs(fileInfo);
            return excel.GetAsByteArray();
        }

        public List<EDReportes> ReporteDiasAusentismoPorProceso(EDReportes edReporte)
        {
            List<EDReportesGenerados> respuestas = new List<EDReportesGenerados>();
            var reporteDefinitivo = new List<EDReportes>();
            var edrespuestas = reportesMG.ReporteDiasAusentismoPorProceso(edReporte);

            int diasHablides = ausenMg.ObtenerDiasLaborablesEmpresa(edReporte.nitEmpresa);
            if (diasHablides == 1)
                diasHablides = 5;
            else if (diasHablides == 2)
                diasHablides = 6;

            foreach (var edrespuesta in edrespuestas)
            {
                respuestas.AddRange(ObtenerDiasCadaMesEvento(edrespuesta, diasHablides));
            }
            reporteDefinitivo = GenerarInformacionReportes(respuestas).ToList();
            return reporteDefinitivo;
        }

        /// <summary>
        /// Genera excel para el reporte: Dias de ausentismo por proceso
        /// </summary>
        /// <param name="edReporte"></param>
        /// <returns></returns>
        public byte[] ReporteDiasAusentismoPorProcesoExcel(EDReportes edReporte)
        {
            List<EDReportes> DatosReporte = ReporteDiasAusentismoPorProceso(edReporte);

            ExcelPackage excel = new ExcelPackage();
            excel.Workbook.Worksheets.Add("Dias de ausentismo por proceso");

            ExcelWorksheet hoja = excel.Workbook.Worksheets[1];

            hoja.Cells["A1:N1"].Merge = true;
            hoja.Cells["A1"].Value = "Dias de ausentismo por proceso ";
            hoja.Cells["A1"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["A1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["A1"].Style.Font.Bold = true;
            hoja.Cells["A1"].Style.WrapText = true;

            hoja.Cells["A2"].Value = "Proceso";
            hoja.Cells["A2"].Style.Font.Bold = true;
            hoja.Cells["B2"].Value = "Enero";
            hoja.Cells["B2"].Style.Font.Bold = true;
            hoja.Cells["C2"].Value = "Febrero";
            hoja.Cells["C2"].Style.Font.Bold = true;
            hoja.Cells["D2"].Value = "Marzo";
            hoja.Cells["D2"].Style.Font.Bold = true;
            hoja.Cells["E2"].Value = "Abril";
            hoja.Cells["E2"].Style.Font.Bold = true;
            hoja.Cells["F2"].Value = "Mayo";
            hoja.Cells["F2"].Style.Font.Bold = true;
            hoja.Cells["G2"].Value = "Junio";
            hoja.Cells["G2"].Style.Font.Bold = true;
            hoja.Cells["H2"].Value = "Julio";
            hoja.Cells["H2"].Style.Font.Bold = true;
            hoja.Cells["I2"].Value = "Agosto";
            hoja.Cells["I2"].Style.Font.Bold = true;
            hoja.Cells["J2"].Value = "Septiembre";
            hoja.Cells["J2"].Style.Font.Bold = true;
            hoja.Cells["K2"].Value = "Octubre";
            hoja.Cells["K2"].Style.Font.Bold = true;
            hoja.Cells["L2"].Value = "Noviembre";
            hoja.Cells["L2"].Style.Font.Bold = true;
            hoja.Cells["M2"].Value = "Diciembre";
            hoja.Cells["M2"].Style.Font.Bold = true;
            hoja.Cells["N2"].Value = "Total";
            hoja.Cells["N2"].Style.Font.Bold = true;

            int nunInicial = 3;

            foreach (var dato in DatosReporte)
            {
                hoja.Cells[string.Format("A{0}", nunInicial)].Value = dato.Evento;
                hoja.Cells[string.Format("A{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].Value = dato.Ene;
                hoja.Cells[string.Format("B{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("C{0}", nunInicial)].Value = dato.Feb;
                hoja.Cells[string.Format("C{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("C{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("D{0}", nunInicial)].Value = dato.Mar;
                hoja.Cells[string.Format("D{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("D{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("E{0}", nunInicial)].Value = dato.Abr;
                hoja.Cells[string.Format("E{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("E{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("F{0}", nunInicial)].Value = dato.May;
                hoja.Cells[string.Format("F{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("F{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("G{0}", nunInicial)].Value = dato.Jun;
                hoja.Cells[string.Format("G{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("G{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("H{0}", nunInicial)].Value = dato.Jul;
                hoja.Cells[string.Format("H{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("H{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("I{0}", nunInicial)].Value = dato.Ago;
                hoja.Cells[string.Format("I{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("I{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("J{0}", nunInicial)].Value = dato.Sep;
                hoja.Cells[string.Format("J{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("J{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("K{0}", nunInicial)].Value = dato.Oct;
                hoja.Cells[string.Format("K{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("K{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("L{0}", nunInicial)].Value = dato.Nov;
                hoja.Cells[string.Format("L{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("L{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("M{0}", nunInicial)].Value = dato.Dic;
                hoja.Cells[string.Format("M{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("M{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("N{0}", nunInicial)].Value = dato.Total;
                hoja.Cells[string.Format("N{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("N{0}", nunInicial)].OfType<decimal>();

                nunInicial++;
            }

            hoja.Column(1).Width = 40;

            foreach (var cel in hoja.Cells[string.Format("A1:N{0}", DatosReporte.Count + 2)])
            {
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            }

            //AGREGAMOS LA GRAFICA
            var chart = hoja.Drawings.AddChart("Dias de ausentismo por proceso", OfficeOpenXml.Drawing.Chart.eChartType.ColumnClustered3D);
            chart.Title.Text = "Dias de ausentismo por proceso";
            chart.SetPosition(1, 0, 15, 0);
            chart.SetSize(600, 400); // Tamaño de la gráfica
            chart.Legend.Remove(); // Si desea eliminar la leyenda

            // Define donde está la información de la gráfica.
            var serie = chart.Series.Add(hoja.Cells[string.Format("N3:N{0}", DatosReporte.Count + 2)], hoja.Cells[string.Format("A3:A{0}", DatosReporte.Count + 2)]);

            //FileInfo fileInfo = new FileInfo(@"D:\Archivos\Actividades.xlsx");
            //excel.SaveAs(fileInfo);
            return excel.GetAsByteArray();
        }

        public List<EDReportes> ReporteCantiEventPorEnfermedades(EDReportes edReporte)
        {
            var reporteDefinitivo = new List<EDReportes>();
            var respuesta = reportesMG.ReporteCantiEventPorEnfermedades(edReporte);
            reporteDefinitivo = GenerarInformacionReportes(respuesta).ToList();
            return reporteDefinitivo;
        }

        /// <summary>
        /// Genera excel para el reporte: Número de eventos por capitulos de enfermedades CIE10
        /// </summary>
        /// <param name="edReporte"></param>
        /// <returns></returns>
        public byte[] ReporteCantiEventPorEnfermedadesExcel(EDReportes edReporte)
        {
            List<EDReportes> DatosReporte = ReporteCantiEventPorEnfermedades(edReporte);

            ExcelPackage excel = new ExcelPackage();
            excel.Workbook.Worksheets.Add("Num eventos x enferme CIE10");

            ExcelWorksheet hoja = excel.Workbook.Worksheets[1];

            hoja.Cells["A1:N1"].Merge = true;
            hoja.Cells["A1"].Value = "Número de eventos por capitulos de enfermedades CIE10 ";
            hoja.Cells["A1"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["A1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["A1"].Style.Font.Bold = true;
            hoja.Cells["A1"].Style.WrapText = true;

            hoja.Cells["A2"].Value = "Contingencia";
            hoja.Cells["A2"].Style.Font.Bold = true;
            hoja.Cells["B2"].Value = "Enero";
            hoja.Cells["B2"].Style.Font.Bold = true;
            hoja.Cells["C2"].Value = "Febrero";
            hoja.Cells["C2"].Style.Font.Bold = true;
            hoja.Cells["D2"].Value = "Marzo";
            hoja.Cells["D2"].Style.Font.Bold = true;
            hoja.Cells["E2"].Value = "Abril";
            hoja.Cells["E2"].Style.Font.Bold = true;
            hoja.Cells["F2"].Value = "Mayo";
            hoja.Cells["F2"].Style.Font.Bold = true;
            hoja.Cells["G2"].Value = "Junio";
            hoja.Cells["G2"].Style.Font.Bold = true;
            hoja.Cells["H2"].Value = "Julio";
            hoja.Cells["H2"].Style.Font.Bold = true;
            hoja.Cells["I2"].Value = "Agosto";
            hoja.Cells["I2"].Style.Font.Bold = true;
            hoja.Cells["J2"].Value = "Septiembre";
            hoja.Cells["J2"].Style.Font.Bold = true;
            hoja.Cells["K2"].Value = "Octubre";
            hoja.Cells["K2"].Style.Font.Bold = true;
            hoja.Cells["L2"].Value = "Noviembre";
            hoja.Cells["L2"].Style.Font.Bold = true;
            hoja.Cells["M2"].Value = "Diciembre";
            hoja.Cells["M2"].Style.Font.Bold = true;
            hoja.Cells["N2"].Value = "Total";
            hoja.Cells["N2"].Style.Font.Bold = true;

            int nunInicial = 3;

            foreach (var dato in DatosReporte)
            {
                hoja.Cells[string.Format("A{0}", nunInicial)].Value = dato.Evento;
                hoja.Cells[string.Format("A{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].Value = dato.Ene;
                hoja.Cells[string.Format("B{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("C{0}", nunInicial)].Value = dato.Feb;
                hoja.Cells[string.Format("C{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("C{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("D{0}", nunInicial)].Value = dato.Mar;
                hoja.Cells[string.Format("D{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("D{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("E{0}", nunInicial)].Value = dato.Abr;
                hoja.Cells[string.Format("E{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("E{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("F{0}", nunInicial)].Value = dato.May;
                hoja.Cells[string.Format("F{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("F{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("G{0}", nunInicial)].Value = dato.Jun;
                hoja.Cells[string.Format("G{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("G{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("H{0}", nunInicial)].Value = dato.Jul;
                hoja.Cells[string.Format("H{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("H{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("I{0}", nunInicial)].Value = dato.Ago;
                hoja.Cells[string.Format("I{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("I{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("J{0}", nunInicial)].Value = dato.Sep;
                hoja.Cells[string.Format("J{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("J{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("K{0}", nunInicial)].Value = dato.Oct;
                hoja.Cells[string.Format("K{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("K{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("L{0}", nunInicial)].Value = dato.Nov;
                hoja.Cells[string.Format("L{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("L{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("M{0}", nunInicial)].Value = dato.Dic;
                hoja.Cells[string.Format("M{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("M{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("N{0}", nunInicial)].Value = dato.Total;
                hoja.Cells[string.Format("N{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("N{0}", nunInicial)].OfType<decimal>();

                nunInicial++;
            }

            hoja.Column(1).Width = 40;

            foreach (var cel in hoja.Cells[string.Format("A1:N{0}", DatosReporte.Count + 2)])
            {
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            }

            //AGREGAMOS LA GRAFICA
            var chart = hoja.Drawings.AddChart("Número de eventos por capitulos de enfermedades CIE10", OfficeOpenXml.Drawing.Chart.eChartType.ColumnClustered3D);
            chart.Title.Text = "Número de eventos por capitulos de enfermedades CIE10";
            chart.SetPosition(1, 0, 15, 0);
            chart.SetSize(600, 400); // Tamaño de la gráfica
            chart.Legend.Remove(); // Si desea eliminar la leyenda

            // Define donde está la información de la gráfica.
            var serie = chart.Series.Add(hoja.Cells[string.Format("N3:N{0}", DatosReporte.Count + 2)], hoja.Cells[string.Format("A3:A{0}", DatosReporte.Count + 2)]);

            //FileInfo fileInfo = new FileInfo(@"D:\Archivos\Actividades.xlsx");
            //excel.SaveAs(fileInfo);
            return excel.GetAsByteArray();
        }

        public List<EDReportes> ReporteCantidadAusenGrupEtarios(EDReportes edReporte)
        {
            var reporteDefinitivo = new List<EDReportes>();
            var respuesta = reportesMG.ReporteCantidadAusenGrupEtarios(edReporte);
            reporteDefinitivo = GenerarInformacionReportes(respuesta).ToList();
            return reporteDefinitivo;
        }


        /// <summary>
        /// Genera excel para el reporte: Dias de ausentismo por proceso
        /// </summary>
        /// <param name="edReporte"></param>
        /// <returns></returns>
        public byte[] ReporteCantidadAusenGrupEtariosExcel(EDReportes edReporte)
        {
            List<EDReportes> DatosReporte = ReporteCantidadAusenGrupEtarios(edReporte);

            ExcelPackage excel = new ExcelPackage();
            excel.Workbook.Worksheets.Add("Ausentismos por Grupos Etarios");

            ExcelWorksheet hoja = excel.Workbook.Worksheets[1];

            hoja.Cells["A1:N1"].Merge = true;
            hoja.Cells["A1"].Value = "Ausentismos por Grupos Etarios ";
            hoja.Cells["A1"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["A1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["A1"].Style.Font.Bold = true;
            hoja.Cells["A1"].Style.WrapText = true;

            hoja.Cells["A2"].Value = "Proceso";
            hoja.Cells["A2"].Style.Font.Bold = true;
            hoja.Cells["B2"].Value = "Enero";
            hoja.Cells["B2"].Style.Font.Bold = true;
            hoja.Cells["C2"].Value = "Febrero";
            hoja.Cells["C2"].Style.Font.Bold = true;
            hoja.Cells["D2"].Value = "Marzo";
            hoja.Cells["D2"].Style.Font.Bold = true;
            hoja.Cells["E2"].Value = "Abril";
            hoja.Cells["E2"].Style.Font.Bold = true;
            hoja.Cells["F2"].Value = "Mayo";
            hoja.Cells["F2"].Style.Font.Bold = true;
            hoja.Cells["G2"].Value = "Junio";
            hoja.Cells["G2"].Style.Font.Bold = true;
            hoja.Cells["H2"].Value = "Julio";
            hoja.Cells["H2"].Style.Font.Bold = true;
            hoja.Cells["I2"].Value = "Agosto";
            hoja.Cells["I2"].Style.Font.Bold = true;
            hoja.Cells["J2"].Value = "Septiembre";
            hoja.Cells["J2"].Style.Font.Bold = true;
            hoja.Cells["K2"].Value = "Octubre";
            hoja.Cells["K2"].Style.Font.Bold = true;
            hoja.Cells["L2"].Value = "Noviembre";
            hoja.Cells["L2"].Style.Font.Bold = true;
            hoja.Cells["M2"].Value = "Diciembre";
            hoja.Cells["M2"].Style.Font.Bold = true;
            hoja.Cells["N2"].Value = "Total";
            hoja.Cells["N2"].Style.Font.Bold = true;

            int nunInicial = 3;

            foreach (var dato in DatosReporte)
            {
                hoja.Cells[string.Format("A{0}", nunInicial)].Value = dato.Evento;
                hoja.Cells[string.Format("A{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].Value = dato.Ene;
                hoja.Cells[string.Format("B{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("C{0}", nunInicial)].Value = dato.Feb;
                hoja.Cells[string.Format("C{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("C{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("D{0}", nunInicial)].Value = dato.Mar;
                hoja.Cells[string.Format("D{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("D{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("E{0}", nunInicial)].Value = dato.Abr;
                hoja.Cells[string.Format("E{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("E{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("F{0}", nunInicial)].Value = dato.May;
                hoja.Cells[string.Format("F{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("F{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("G{0}", nunInicial)].Value = dato.Jun;
                hoja.Cells[string.Format("G{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("G{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("H{0}", nunInicial)].Value = dato.Jul;
                hoja.Cells[string.Format("H{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("H{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("I{0}", nunInicial)].Value = dato.Ago;
                hoja.Cells[string.Format("I{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("I{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("J{0}", nunInicial)].Value = dato.Sep;
                hoja.Cells[string.Format("J{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("J{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("K{0}", nunInicial)].Value = dato.Oct;
                hoja.Cells[string.Format("K{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("K{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("L{0}", nunInicial)].Value = dato.Nov;
                hoja.Cells[string.Format("L{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("L{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("M{0}", nunInicial)].Value = dato.Dic;
                hoja.Cells[string.Format("M{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("M{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("N{0}", nunInicial)].Value = dato.Total;
                hoja.Cells[string.Format("N{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("N{0}", nunInicial)].OfType<decimal>();

                nunInicial++;
            }

            hoja.Column(1).Width = 40;

            foreach (var cel in hoja.Cells[string.Format("A1:N{0}", DatosReporte.Count + 2)])
            {
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            }

            //AGREGAMOS LA GRAFICA
            var chart = hoja.Drawings.AddChart("Ausentismos por Grupos Etarios", OfficeOpenXml.Drawing.Chart.eChartType.ColumnClustered3D);
            chart.Title.Text = "Ausentismos por Grupos Etarios";
            chart.SetPosition(1, 0, 15, 0);
            chart.SetSize(600, 400); // Tamaño de la gráfica
            chart.Legend.Remove(); // Si desea eliminar la leyenda

            // Define donde está la información de la gráfica.
            var serie = chart.Series.Add(hoja.Cells[string.Format("N3:N{0}", DatosReporte.Count + 2)], hoja.Cells[string.Format("A3:A{0}", DatosReporte.Count + 2)]);

            //FileInfo fileInfo = new FileInfo(@"D:\Archivos\Actividades.xlsx");
            //excel.SaveAs(fileInfo);
            return excel.GetAsByteArray();
        }

        /// <summary>
        /// Retorna la informacion para generar los resportes en la aplicacion
        /// </summary>
        /// <param name="reportes"></param>
        /// <returns></returns>
        private IEnumerable<EDReportes> GenerarInformacionReportes(List<EDReportesGenerados> reportes)
        {
            var reporteDefinitivo = new List<EDReportes>();
            if (reportes != null && reportes.Count > 0)
            {
                var reporte = reportes.Select(r => new EDReportes()
                {
                    CONTINGENCIA = r.Contingencia,
                    Evento = r.Evento,
                    Ene = r.Mes != 1 ? 0 : r.Total,
                    Feb = r.Mes != 2 ? 0 : r.Total,
                    Mar = r.Mes != 3 ? 0 : r.Total,
                    Abr = r.Mes != 4 ? 0 : r.Total,
                    May = r.Mes != 5 ? 0 : r.Total,
                    Jun = r.Mes != 6 ? 0 : r.Total,
                    Jul = r.Mes != 7 ? 0 : r.Total,
                    Ago = r.Mes != 8 ? 0 : r.Total,
                    Sep = r.Mes != 9 ? 0 : r.Total,
                    Oct = r.Mes != 10 ? 0 : r.Total,
                    Nov = r.Mes != 11 ? 0 : r.Total,
                    Dic = r.Mes != 12 ? 0 : r.Total,
                    Total = reportes.Where(d => d.Evento.Equals(r.Evento) && d.Contingencia.Equals(r.Contingencia))
                                        .Sum(t => t.Total)
                }).ToList();

                reporteDefinitivo = ((from c in reporte
                                      group c by
                                      new
                                      {
                                          c.CONTINGENCIA,
                                          c.Evento
                                      } into grupo
                                      where grupo.ToList().Count > 1
                                      select new EDReportes()
                                      {
                                          CONTINGENCIA = grupo.Key.CONTINGENCIA,
                                          Evento = grupo.Key.Evento,
                                          Ene = grupo.Sum(m => m.Ene),
                                          Feb = grupo.Sum(m => m.Feb),
                                          Mar = grupo.Sum(m => m.Mar),
                                          Abr = grupo.Sum(m => m.Abr),
                                          May = grupo.Sum(m => m.May),
                                          Jun = grupo.Sum(m => m.Jun),
                                          Jul = grupo.Sum(m => m.Jul),
                                          Ago = grupo.Sum(m => m.Ago),
                                          Sep = grupo.Sum(m => m.Sep),
                                          Oct = grupo.Sum(m => m.Oct),
                                          Nov = grupo.Sum(m => m.Nov),
                                          Dic = grupo.Sum(m => m.Dic),
                                          Total = grupo.Select(t => t.Total).First()
                                      }).Union(
                                    from c in reporte
                                    group c by
                                    new
                                    {
                                        c.CONTINGENCIA,
                                        c.Evento
                                    } into grupo
                                    where grupo.ToList().Count == 1
                                    select new EDReportes()
                                    {
                                        CONTINGENCIA = grupo.Key.CONTINGENCIA,
                                        Evento = grupo.Key.Evento,
                                        Ene = grupo.Select(m => m.Ene).FirstOrDefault(),
                                        Feb = grupo.Select(m => m.Feb).FirstOrDefault(),
                                        Mar = grupo.Select(m => m.Mar).FirstOrDefault(),
                                        Abr = grupo.Select(m => m.Abr).FirstOrDefault(),
                                        May = grupo.Select(m => m.May).FirstOrDefault(),
                                        Jun = grupo.Select(m => m.Jun).FirstOrDefault(),
                                        Jul = grupo.Select(m => m.Jul).FirstOrDefault(),
                                        Ago = grupo.Select(m => m.Ago).FirstOrDefault(),
                                        Sep = grupo.Select(m => m.Sep).FirstOrDefault(),
                                        Oct = grupo.Select(m => m.Oct).FirstOrDefault(),
                                        Nov = grupo.Select(m => m.Nov).FirstOrDefault(),
                                        Dic = grupo.Select(m => m.Dic).FirstOrDefault(),
                                        Total = grupo.Select(t => t.Total).First()
                                    }).OrderBy(c => c.CONTINGENCIA)).ToList();
            }
            return reporteDefinitivo;
        }

        public int ObtenerAnoInicioEmpresa(string Nit)
        {
            return reportesMG.Obteneranoinicioempresa(Nit);
        }
    }
}
