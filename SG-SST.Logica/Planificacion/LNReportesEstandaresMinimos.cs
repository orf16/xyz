using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Interfaces.Planificacion;
using SG_SST.InterfazManager.Planificacion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Logica.Planificacion
{
    public class LNReportesEstandaresMinimos
    {
        private static IReportesEstandaresMinimos resm = IMEvaluaccion.Reportes();
        private static IEvaluacionEstandMinimos esm = IMEvaluaccion.EstandaresMinimos();

        /// <summary>
        /// Genera los datos para la grafica de informe parcial de cantidad de respuestas dadas
        /// por ciclo
        /// </summary>
        /// <param name="Nit"></param>
        /// <returns></returns>
        public List<EDCiclo> ObtenerPorcentajeDeRespuestas(string Nit)
        {
            List<EDCiclo> Ciclos = esm.ObtenerCiclos();
            foreach (EDCiclo ciclo in Ciclos)
            {
                int preguntasRealizadas = resm.ObtenerCantidadPreguntasAlmomento(ciclo.Id_Ciclo, Nit);
                int TotalPreguntas = esm.ObtenerCantidadCriteriosPorEstandar(ciclo.Id_Ciclo);
                if (preguntasRealizadas > 0)
                    ciclo.PorcenRespondido = (preguntasRealizadas * 100) / TotalPreguntas;
                else
                    ciclo.PorcenRespondido = 0;
            }
            return Ciclos;
        }

        /// <summary>
        /// Genera el excel para la grafica de informe parcial de cantidad de respuestas dadas
        /// por ciclo
        /// </summary>
        /// <param name="Nit"></param>
        /// <returns></returns>
        public byte [] ObtenerExcelPorcentajeDeRespuestas(string Nit)
        {
            List<EDCiclo> Ciclos = esm.ObtenerCiclos();
            foreach (EDCiclo ciclo in Ciclos)
            {
                int preguntasRealizadas = resm.ObtenerCantidadPreguntasAlmomento(ciclo.Id_Ciclo, Nit);
                int TotalPreguntas = esm.ObtenerCantidadCriteriosPorEstandar(ciclo.Id_Ciclo);
                if (preguntasRealizadas > 0)
                    ciclo.PorcenRespondido = (preguntasRealizadas * 100) / TotalPreguntas;
                else
                    ciclo.PorcenRespondido = 0;
            }

            ExcelPackage excel = new ExcelPackage();

            excel.Workbook.Worksheets.Add("Cantidad de respuestas");

            ExcelWorksheet hoja = excel.Workbook.Worksheets[1];

            hoja.Cells["A1:B1"].Merge = true;
            hoja.Cells["A1"].Value = "PORCENTAJE DE RESPUESTAS POR CICLO";
            hoja.Cells["A1"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["A1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["A1"].Style.Font.Bold = true;
            hoja.Cells["A1"].Style.WrapText = true;

            hoja.Cells["A2"].Value = "CICLO";
            hoja.Cells["A2"].Style.Font.Bold = true;
            hoja.Cells["B2"].Value = "% RESPONDIDO";
            hoja.Cells["B2"].Style.Font.Bold = true;
            int nunInicial = 3;

            foreach (var ciclo in Ciclos)
            {
                hoja.Cells[string.Format("A{0}", nunInicial)].Value = ciclo.Nombre;
                hoja.Cells[string.Format("A{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].Value = ciclo.PorcenRespondido;
                hoja.Cells[string.Format("B{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].OfType<decimal>();                   

                nunInicial++;
            }

            hoja.Row(1).Height = 40;           
            hoja.Column(1).Width = 50;
            hoja.Column(2).Width = 25;
            

            foreach (var cel in hoja.Cells[string.Format("A1:C{0}", Ciclos.Count+2)])
            {
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            }

            //AGREGAMOS LA GRAFICA
            var chart = (hoja.Drawings.AddChart("PORCENTAJE DE RESPUESTAS POR CICLO", OfficeOpenXml.Drawing.Chart.eChartType.Radar) as ExcelRadarChart);
            chart.Title.Text = "PORCENTAJE DE RESPUESTAS POR CICLO";
            chart.SetPosition(1, 0, 2, 0);
            chart.SetSize(600, 400); // Tamaño de la gráfica
            chart.Legend.Remove(); // Si desea eliminar la leyenda
            chart.YAxis.MaxValue = 100;            
            chart.DataLabel.ShowPercent = true;
            

            // Define donde está la información de la gráfica.
            var serie = chart.Series.Add(hoja.Cells[string.Format("B3:B{0}", Ciclos.Count + 2)], hoja.Cells[string.Format("A3:A{0}", Ciclos.Count + 2)]);

            //FileInfo fileInfo = new FileInfo(@"D:\Archivos\CantidadPreguntas.xlsx");
            //Ac.SaveAs(fileInfo);
            return excel.GetAsByteArray();
        }

        /// <summary>
        /// Obtiene los datos para la grafica del informe parcial de porcentaje calificacion obtenido
        /// por ciclo
        /// </summary>
        /// <param name="Nit"></param>
        /// <returns></returns>
        public List<EDCiclo> ObtenerPorcentajeObtenido(string Nit)
        {
            List<EDCiclo> Ciclos = esm.ObtenerCiclos();
            foreach (EDCiclo ciclo in Ciclos)
            {
                decimal puntoObtenidos = resm.ObtenerPorcentajeObtenidoAlmomento(ciclo.Id_Ciclo, Nit);
                if (puntoObtenidos > 0)
                    ciclo.PorcenObtenido = (puntoObtenidos * 100) / ciclo.Porcentaje_Max;
                else
                    ciclo.PorcenObtenido = 0;
            }
            return Ciclos;
        }


        /// <summary>
        /// Obtiene el excel para la grafica del informe parcial de porcentaje calificacion obtenido
        /// por ciclo
        /// </summary>
        /// <param name="Nit"></param>
        /// <returns></returns>
        public byte [] ObtenerExcelPorcentajeObtenido(string Nit)
        {
            List<EDCiclo> Ciclos = esm.ObtenerCiclos();
            foreach (EDCiclo ciclo in Ciclos)
            {
                decimal puntoObtenidos = resm.ObtenerPorcentajeObtenidoAlmomento(ciclo.Id_Ciclo, Nit);
                if (puntoObtenidos > 0)
                    ciclo.PorcenObtenido = (puntoObtenidos * 100) / ciclo.Porcentaje_Max;
                else
                    ciclo.PorcenObtenido = 0;
            }

            ExcelPackage excel = new ExcelPackage();

            excel.Workbook.Worksheets.Add("Cantidad de respuestas");

            ExcelWorksheet hoja = excel.Workbook.Worksheets[1];

            hoja.Cells["A1:B1"].Merge = true;
            hoja.Cells["A1"].Value = "PORCENTAJE DE CALIFICACION POR CICLO";
            hoja.Cells["A1"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["A1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["A1"].Style.Font.Bold = true;
            hoja.Cells["A1"].Style.WrapText = true;

            hoja.Cells["A2"].Value = "CICLO";
            hoja.Cells["A2"].Style.Font.Bold = true;
            hoja.Cells["B2"].Value = "% DE CALIFICACION";
            hoja.Cells["B2"].Style.Font.Bold = true;
            int nunInicial = 3;

            foreach (var ciclo in Ciclos)
            {
                hoja.Cells[string.Format("A{0}", nunInicial)].Value = ciclo.Nombre;
                hoja.Cells[string.Format("A{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].Value = ciclo.PorcenObtenido;
                hoja.Cells[string.Format("B{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].OfType<decimal>();

                nunInicial++;
            }

            hoja.Row(1).Height = 40;
            hoja.Column(1).Width = 50;
            hoja.Column(2).Width = 25;


            foreach (var cel in hoja.Cells[string.Format("A1:C{0}", Ciclos.Count + 2)])
            {
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            }

            //AGREGAMOS LA GRAFICA
            var chart = (hoja.Drawings.AddChart("PORCENTAJE DE CALIFICACION POR CICLO", OfficeOpenXml.Drawing.Chart.eChartType.Radar) as ExcelRadarChart);
            chart.Title.Text = "PORCENTAJE DE CALIFICACION POR CICLO";
            chart.SetPosition(1, 0, 2, 0);
            chart.SetSize(600, 400); // Tamaño de la gráfica
            chart.Legend.Remove(); // Si desea eliminar la leyenda
            chart.YAxis.MaxValue = 100;            
            chart.DataLabel.ShowPercent = true;
           
                        
            // Define donde está la información de la gráfica.
            var serie = chart.Series.Add(hoja.Cells[string.Format("B3:B{0}", Ciclos.Count + 2)], hoja.Cells[string.Format("A3:A{0}", Ciclos.Count + 2)]);

            //FileInfo fileInfo = new FileInfo(@"D:\Archivos\puntajeObtenido.xlsx");
            //Ac.SaveAs(fileInfo);
            return excel.GetAsByteArray();
        }

        /// <summary>
        /// Genra los datos para la grafica de informe final de calificacion de 
        /// estandares por ciclo
        /// </summary>
        /// <param name="Nit"></param>
        /// <param name="IdCiclo"></param>
        /// <returns></returns>
        public EDCiclo ObtenerCalificacionEstandresPorCliclo(string Nit, int IdCiclo)
        {
            EDCiclo ciclo = new EDCiclo();
            ciclo = esm.ObtenerStandares(IdCiclo);
            foreach (var stand in ciclo.Estandares)
            {
                var calificacion = resm.ObtenerPorcentajeObtenidoEstandar(IdCiclo, stand.Id_Estandar, Nit);
                if (calificacion > 0)
                    stand.Calificacion = (calificacion * 100) / stand.Porcentaje_Max;
                else
                    stand.Calificacion = 0;
            }

            return ciclo;
        }

        /// <summary>
        /// Genra el archivo excel para la grafica de informe final de calificacion de 
        /// estandares por ciclo
        /// </summary>
        /// <param name="Nit"></param>
        /// <param name="IdCiclo"></param>
        /// <returns></returns>
        public byte [] ObtenerExcelCalificacionEstandresPorCliclo(string Nit, int IdCiclo)
        {
            EDCiclo ciclo = new EDCiclo();
            ciclo = esm.ObtenerStandares(IdCiclo);
            foreach (var stand in ciclo.Estandares)
            {
                var calificacion = resm.ObtenerPorcentajeObtenidoEstandar(IdCiclo, stand.Id_Estandar, Nit);
                if (calificacion > 0)
                    stand.Calificacion = (calificacion * 100) / stand.Porcentaje_Max;
                else
                    stand.Calificacion = 0;
            }

            ExcelPackage excel = new ExcelPackage();

            excel.Workbook.Worksheets.Add(ciclo.Nombre);

            ExcelWorksheet hoja = excel.Workbook.Worksheets[1];

            hoja.Cells["A1:B1"].Merge = true;
            hoja.Cells["A1"].Value = "CALIFICACION DEL CICLO " + ciclo.Nombre;
            hoja.Cells["A1"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["A1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["A1"].Style.Font.Bold = true;
            hoja.Cells["A1"].Style.WrapText = true;

            hoja.Cells["A2"].Value = "ESTANDAR";
            hoja.Cells["A2"].Style.Font.Bold = true;
            hoja.Cells["B2"].Value = "CALIFICACION";
            hoja.Cells["B2"].Style.Font.Bold = true;            
            int nunInicial = 3;

            foreach (var estandar in ciclo.Estandares)
            {
                hoja.Cells[string.Format("A{0}", nunInicial)].Value = estandar.Descripcion;
                hoja.Cells[string.Format("A{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].Value = estandar.Calificacion;
                hoja.Cells[string.Format("B{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].OfType<decimal>();
                
                nunInicial++;
            }

            hoja.Row(1).Height = 40;
            hoja.Row(3).Height = 40;
            hoja.Column(1).Width = 50;
            hoja.Column(2).Width = 25;
            hoja.Column(3).Width = 20;

            foreach (var cel in hoja.Cells[string.Format("A1:C{0}", ciclo.Estandares.Count + 2)])
            {
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            }
            
            //AGREGAMOS LA GRAFICA
            var chart = hoja.Drawings.AddChart("CALIFICACION DEL CICLO " + ciclo.Nombre , OfficeOpenXml.Drawing.Chart.eChartType.ColumnClustered3D) ;
            chart.Title.Text = "CALIFICACION DEL CICLO " + ciclo.Nombre;
            chart.SetPosition(1, 0, 2, 0);
            chart.SetSize(600, 400); // Tamaño de la gráfica
            chart.Legend.Remove(); // Si desea eliminar la leyenda
            chart.YAxis.MaxValue = 100;            

            // Define donde está la información de la gráfica.
            var serie = chart.Series.Add(hoja.Cells[string.Format("B3:B{0}", ciclo.Estandares.Count + 2)], hoja.Cells[string.Format("A3:A{0}", ciclo.Estandares.Count + 2)]);
            
            //FileInfo fileInfo = new FileInfo(@"D:\Archivos\Actividades.xlsx");
            //Ac.SaveAs(fileInfo);
            return excel.GetAsByteArray();
        }
    }
}
