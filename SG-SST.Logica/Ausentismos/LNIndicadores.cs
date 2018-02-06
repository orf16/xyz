using SG_SST.EntidadesDominio.Ausentismo;
using SG_SST.InterfazManager.Ausentismo;
using SG_SST.Interfaces.Ausentismo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace SG_SST.Logica.Ausentismos
{
    public class LNIndicadores
    {
        private static IIndicadores indicadoresMg = IMAusentismo.Indicadores();

        /// <summary>
        /// Se calculan las variables: IF, IS e ILI
        /// de acuerdo a la información de ausentismos 
        /// y configuración HHT
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public IEnumerable<EDIndicadores> CalcularIndicadoresPorPeriodo(int anio, int valorK, out int respuesta, string Nit, int idEmpresaUsuaria, int IdContingencia)
        {
            List<EDIndicadores> reporteIndicadores = null;
            var resultado = indicadoresMg.CantidadEventos(anio, idEmpresaUsuaria, Nit, IdContingencia);
            if (resultado == null || resultado.Count <= 0)
            {
                //se configura esta respuesta para indicar que no hubo
                //datos asociados con ausencias
                respuesta = 1;
                return null;
            }
            var configuracionHHT = ObtenerConfiguracionHHTPorPeriodo(anio, Nit);
            if (configuracionHHT == null || configuracionHHT.Count() <= 0)
            {
                //se configura esta respuesta para indicar que no hubo
                //datos asociados con la configuración HHT
                respuesta = 2;
                return null;
            }
            //se realizan los cálculos de las variables IF, IS e ILI
            //para cada mes que existan ausensias y que esté configurado en HHT
            var calculos = (from ev in resultado
                            join conf in configuracionHHT on ev.Mes equals conf.Mes.Value
                            select new
                            {
                                IdContingencia = ev.IdContingencia,
                                Contingencia = ev.Contingencia,
                                Mes = ev.Mes,
                                VariableIF = Math.Round(((ev.NumeroEventos / conf.TotalMes.Value) * valorK), 2),
                                VariableIS = Math.Round(((ev.DiasPorEventos / conf.TotalMes.Value) * valorK), 2),
                                VariableILI = Math.Round(((((ev.NumeroEventos / conf.TotalMes.Value) * valorK) * ((ev.DiasPorEventos / conf.TotalMes.Value) * valorK)) / 1000), 2),
                                Tasa = Math.Round(((ev.NumeroEventos / conf.NumeroTrabajadores.Value) * 100), 2)//formula preguntada a negocio.
                            }).ToList().OrderBy(c => c.IdContingencia);
            if(calculos.Count() < 1)
            {
                //se configura esta respuesta para indicar que no hubo
                //datos asociados con la configuración HHT
                respuesta = 2;
                return null;
            }

            reporteIndicadores = ((from c in calculos
                                   group c by
                               new
                               {
                                   c.IdContingencia,
                                   c.Contingencia
                               } into grupo
                                   where grupo.ToList().Count > 1
                                   select new EDIndicadores()
                                   {
                                       Idcontingencia = grupo.Key.IdContingencia,
                                       Contingencia = grupo.Key.Contingencia,
                                       Ene = grupo.Where(m => m.Mes == 1).Select(m => new Variables() { VariableIF = m.VariableIF, VariableIS = m.VariableIS, VariableILI = m.VariableILI, Tasa = m.Tasa }).FirstOrDefault(),
                                       Feb = grupo.Where(m => m.Mes == 2).Select(m => new Variables() { VariableIF = m.VariableIF, VariableIS = m.VariableIS, VariableILI = m.VariableILI, Tasa = m.Tasa }).FirstOrDefault(),
                                       Mar = grupo.Where(m => m.Mes == 3).Select(m => new Variables() { VariableIF = m.VariableIF, VariableIS = m.VariableIS, VariableILI = m.VariableILI, Tasa = m.Tasa }).FirstOrDefault(),
                                       Abr = grupo.Where(m => m.Mes == 4).Select(m => new Variables() { VariableIF = m.VariableIF, VariableIS = m.VariableIS, VariableILI = m.VariableILI, Tasa = m.Tasa }).FirstOrDefault(),
                                       May = grupo.Where(m => m.Mes == 5).Select(m => new Variables() { VariableIF = m.VariableIF, VariableIS = m.VariableIS, VariableILI = m.VariableILI, Tasa = m.Tasa }).FirstOrDefault(),
                                       Jun = grupo.Where(m => m.Mes == 6).Select(m => new Variables() { VariableIF = m.VariableIF, VariableIS = m.VariableIS, VariableILI = m.VariableILI, Tasa = m.Tasa }).FirstOrDefault(),
                                       Jul = grupo.Where(m => m.Mes == 7).Select(m => new Variables() { VariableIF = m.VariableIF, VariableIS = m.VariableIS, VariableILI = m.VariableILI, Tasa = m.Tasa }).FirstOrDefault(),
                                       Ago = grupo.Where(m => m.Mes == 8).Select(m => new Variables() { VariableIF = m.VariableIF, VariableIS = m.VariableIS, VariableILI = m.VariableILI, Tasa = m.Tasa }).FirstOrDefault(),
                                       Sep = grupo.Where(m => m.Mes == 9).Select(m => new Variables() { VariableIF = m.VariableIF, VariableIS = m.VariableIS, VariableILI = m.VariableILI, Tasa = m.Tasa }).FirstOrDefault(),
                                       Oct = grupo.Where(m => m.Mes == 10).Select(m => new Variables() { VariableIF = m.VariableIF, VariableIS = m.VariableIS, VariableILI = m.VariableILI, Tasa = m.Tasa }).FirstOrDefault(),
                                       Nov = grupo.Where(m => m.Mes == 11).Select(m => new Variables() { VariableIF = m.VariableIF, VariableIS = m.VariableIS, VariableILI = m.VariableILI, Tasa = m.Tasa }).FirstOrDefault(),
                                       Dic = grupo.Where(m => m.Mes == 12).Select(m => new Variables() { VariableIF = m.VariableIF, VariableIS = m.VariableIS, VariableILI = m.VariableILI, Tasa = m.Tasa }).FirstOrDefault()
                                   }).Union(
                                   from c in calculos
                                   group c by
                                   new
                                   {
                                       c.IdContingencia,
                                       c.Contingencia
                                   } into grupo
                                   where grupo.ToList().Count == 1
                                   select new EDIndicadores()
                                   {
                                       Idcontingencia = grupo.Key.IdContingencia,
                                       Contingencia = grupo.Key.Contingencia,
                                       Ene = grupo.Where(m => m.Mes == 1).Select(m => new Variables() { VariableIF = m.VariableIF, VariableIS = m.VariableIS, VariableILI = m.VariableILI, Tasa = m.Tasa }).FirstOrDefault(),
                                       Feb = grupo.Where(m => m.Mes == 2).Select(m => new Variables() { VariableIF = m.VariableIF, VariableIS = m.VariableIS, VariableILI = m.VariableILI, Tasa = m.Tasa }).FirstOrDefault(),
                                       Mar = grupo.Where(m => m.Mes == 3).Select(m => new Variables() { VariableIF = m.VariableIF, VariableIS = m.VariableIS, VariableILI = m.VariableILI, Tasa = m.Tasa }).FirstOrDefault(),
                                       Abr = grupo.Where(m => m.Mes == 4).Select(m => new Variables() { VariableIF = m.VariableIF, VariableIS = m.VariableIS, VariableILI = m.VariableILI, Tasa = m.Tasa }).FirstOrDefault(),
                                       May = grupo.Where(m => m.Mes == 5).Select(m => new Variables() { VariableIF = m.VariableIF, VariableIS = m.VariableIS, VariableILI = m.VariableILI, Tasa = m.Tasa }).FirstOrDefault(),
                                       Jun = grupo.Where(m => m.Mes == 6).Select(m => new Variables() { VariableIF = m.VariableIF, VariableIS = m.VariableIS, VariableILI = m.VariableILI, Tasa = m.Tasa }).FirstOrDefault(),
                                       Jul = grupo.Where(m => m.Mes == 7).Select(m => new Variables() { VariableIF = m.VariableIF, VariableIS = m.VariableIS, VariableILI = m.VariableILI, Tasa = m.Tasa }).FirstOrDefault(),
                                       Ago = grupo.Where(m => m.Mes == 8).Select(m => new Variables() { VariableIF = m.VariableIF, VariableIS = m.VariableIS, VariableILI = m.VariableILI, Tasa = m.Tasa }).FirstOrDefault(),
                                       Sep = grupo.Where(m => m.Mes == 9).Select(m => new Variables() { VariableIF = m.VariableIF, VariableIS = m.VariableIS, VariableILI = m.VariableILI, Tasa = m.Tasa }).FirstOrDefault(),
                                       Oct = grupo.Where(m => m.Mes == 10).Select(m => new Variables() { VariableIF = m.VariableIF, VariableIS = m.VariableIS, VariableILI = m.VariableILI, Tasa = m.Tasa }).FirstOrDefault(),
                                       Nov = grupo.Where(m => m.Mes == 11).Select(m => new Variables() { VariableIF = m.VariableIF, VariableIS = m.VariableIS, VariableILI = m.VariableILI, Tasa = m.Tasa }).FirstOrDefault(),
                                       Dic = grupo.Where(m => m.Mes == 12).Select(m => new Variables() { VariableIF = m.VariableIF, VariableIS = m.VariableIS, VariableILI = m.VariableILI, Tasa = m.Tasa }).FirstOrDefault()
                                   }).OrderBy(c => c.Idcontingencia)).ToList();
            respuesta = 0;
            return reporteIndicadores;
        }

        public byte[] IndicadoresPorPeriodoExcel(int anio, int valorK, string Nit, int idEmpresaUsuaria, int IdContingencia)
        {
            int respuesta = 0;
            List<EDIndicadores> Indicadores = CalcularIndicadoresPorPeriodo(anio, valorK, out respuesta, Nit, idEmpresaUsuaria, IdContingencia).ToList();

            ExcelPackage excel = new ExcelPackage();
            excel.Workbook.Worksheets.Add("Indicadores");

            ExcelWorksheet hoja = excel.Workbook.Worksheets[1];

            hoja.Cells["A1:AW1"].Merge = true;
            hoja.Cells["A1"].Value = "INDICADRES ";
            hoja.Cells["A1"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["A1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["A1"].Style.Font.Bold = true;
            hoja.Cells["A1"].Style.WrapText = true;

            hoja.Cells["A2"].Value = "AÑO " + anio;
            hoja.Cells["A2"].Style.Font.Bold = true;
            hoja.Cells["B2:E2"].Merge = true;
            hoja.Cells["B2"].Value = "Enero";
            hoja.Cells["B2"].Style.Font.Bold = true;

            hoja.Cells["F2:I2"].Merge = true;
            hoja.Cells["F2"].Value = "Febrero";
            hoja.Cells["F2"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["F2"].Style.Font.Bold = true;

            hoja.Cells["J2:M2"].Merge = true;
            hoja.Cells["J2"].Value = "Marzo";
            hoja.Cells["J2"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["J2"].Style.Font.Bold = true;

            hoja.Cells["N2:Q2"].Merge = true;
            hoja.Cells["N2"].Value = "Abril";
            hoja.Cells["N2"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["N2"].Style.Font.Bold = true;

            hoja.Cells["R2:U2"].Merge = true;
            hoja.Cells["R2"].Value = "Mayo";
            hoja.Cells["R2"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["R2"].Style.Font.Bold = true;

            hoja.Cells["V2:Y2"].Merge = true;
            hoja.Cells["V2"].Value = "Junio";
            hoja.Cells["V2"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["V2"].Style.Font.Bold = true;

            hoja.Cells["Z2:AC2"].Merge = true;
            hoja.Cells["Z2"].Value = "Julio";
            hoja.Cells["Z2"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["Z2"].Style.Font.Bold = true;

            hoja.Cells["AD2:AG2"].Merge = true;
            hoja.Cells["AD2"].Value = "Agosto";
            hoja.Cells["AD2"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["AD2"].Style.Font.Bold = true;

            hoja.Cells["AH2:AK2"].Merge = true;
            hoja.Cells["AH2"].Value = "Septiembre";
            hoja.Cells["AH2"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["AH2"].Style.Font.Bold = true;

            hoja.Cells["AL2:AO2"].Merge = true;
            hoja.Cells["AL2"].Value = "Octubre";
            hoja.Cells["AL2"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["AL2"].Style.Font.Bold = true;

            hoja.Cells["AP2:AS2"].Merge = true;
            hoja.Cells["AP2"].Value = "Noviembre";
            hoja.Cells["AP2"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["AP2"].Style.Font.Bold = true;

            hoja.Cells["AT2:AW2"].Merge = true;
            hoja.Cells["AT2"].Value = "Diciembre";
            hoja.Cells["AT2"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["AT2"].Style.Font.Bold = true;


            hoja.Cells["A3"].Value = "Tipo Contingencia";
            hoja.Cells["A3"].Style.Font.Bold = true;
            hoja.Cells["B3"].Value = "IF";
            hoja.Cells["B3"].Style.Font.Bold = true;
            hoja.Cells["C3"].Value = "IS";
            hoja.Cells["C3"].Style.Font.Bold = true;
            hoja.Cells["D3"].Value = "ILI";
            hoja.Cells["D3"].Style.Font.Bold = true;
            hoja.Cells["E3"].Value = "Tasa/100";
            hoja.Cells["E3"].Style.Font.Bold = true;

            hoja.Cells["F3"].Value = "IF";
            hoja.Cells["F3"].Style.Font.Bold = true;
            hoja.Cells["G3"].Value = "IS";
            hoja.Cells["G3"].Style.Font.Bold = true;
            hoja.Cells["H3"].Value = "ILI";
            hoja.Cells["H3"].Style.Font.Bold = true;
            hoja.Cells["I3"].Value = "Tasa/100";
            hoja.Cells["I3"].Style.Font.Bold = true;

            hoja.Cells["J3"].Value = "IF";
            hoja.Cells["J3"].Style.Font.Bold = true;
            hoja.Cells["K3"].Value = "IS";
            hoja.Cells["K3"].Style.Font.Bold = true;
            hoja.Cells["L3"].Value = "ILI";
            hoja.Cells["L3"].Style.Font.Bold = true;
            hoja.Cells["M3"].Value = "Tasa/100";
            hoja.Cells["M3"].Style.Font.Bold = true;

            hoja.Cells["N3"].Value = "IF";
            hoja.Cells["N3"].Style.Font.Bold = true;
            hoja.Cells["O3"].Value = "IS";
            hoja.Cells["O3"].Style.Font.Bold = true;
            hoja.Cells["P3"].Value = "ILI";
            hoja.Cells["P3"].Style.Font.Bold = true;
            hoja.Cells["Q3"].Value = "Tasa/100";
            hoja.Cells["Q3"].Style.Font.Bold = true;

            hoja.Cells["R3"].Value = "IF";
            hoja.Cells["R3"].Style.Font.Bold = true;
            hoja.Cells["S3"].Value = "IS";
            hoja.Cells["S3"].Style.Font.Bold = true;
            hoja.Cells["T3"].Value = "ILI";
            hoja.Cells["T3"].Style.Font.Bold = true;
            hoja.Cells["U3"].Value = "Tasa/100";
            hoja.Cells["U3"].Style.Font.Bold = true;

            hoja.Cells["V3"].Value = "IF";
            hoja.Cells["V3"].Style.Font.Bold = true;
            hoja.Cells["W3"].Value = "IS";
            hoja.Cells["W3"].Style.Font.Bold = true;
            hoja.Cells["X3"].Value = "ILI";
            hoja.Cells["X3"].Style.Font.Bold = true;
            hoja.Cells["Y3"].Value = "Tasa/100";
            hoja.Cells["Y3"].Style.Font.Bold = true;

            hoja.Cells["Z3"].Value = "IF";
            hoja.Cells["Z3"].Style.Font.Bold = true;
            hoja.Cells["AA3"].Value = "IS";
            hoja.Cells["AA3"].Style.Font.Bold = true;
            hoja.Cells["AB3"].Value = "ILI";
            hoja.Cells["AB3"].Style.Font.Bold = true;
            hoja.Cells["AC3"].Value = "Tasa/100";
            hoja.Cells["AC3"].Style.Font.Bold = true;

            hoja.Cells["AD3"].Value = "IF";
            hoja.Cells["AD3"].Style.Font.Bold = true;
            hoja.Cells["AE3"].Value = "IS";
            hoja.Cells["AE3"].Style.Font.Bold = true;
            hoja.Cells["AF3"].Value = "ILI";
            hoja.Cells["AF3"].Style.Font.Bold = true;
            hoja.Cells["AG3"].Value = "Tasa/100";
            hoja.Cells["AG3"].Style.Font.Bold = true;

            hoja.Cells["AH3"].Value = "IF";
            hoja.Cells["AH3"].Style.Font.Bold = true;
            hoja.Cells["AI3"].Value = "IS";
            hoja.Cells["AI3"].Style.Font.Bold = true;
            hoja.Cells["AJ3"].Value = "ILI";
            hoja.Cells["AJ3"].Style.Font.Bold = true;
            hoja.Cells["AK3"].Value = "Tasa/100";
            hoja.Cells["AK3"].Style.Font.Bold = true;

            hoja.Cells["AL3"].Value = "IF";
            hoja.Cells["AL3"].Style.Font.Bold = true;
            hoja.Cells["AM3"].Value = "IS";
            hoja.Cells["AM3"].Style.Font.Bold = true;
            hoja.Cells["AN3"].Value = "ILI";
            hoja.Cells["AN3"].Style.Font.Bold = true;
            hoja.Cells["AO3"].Value = "Tasa/100";
            hoja.Cells["AO3"].Style.Font.Bold = true;

            hoja.Cells["AP3"].Value = "IF";
            hoja.Cells["AP3"].Style.Font.Bold = true;
            hoja.Cells["AQ3"].Value = "IS";
            hoja.Cells["AQ3"].Style.Font.Bold = true;
            hoja.Cells["AR3"].Value = "ILI";
            hoja.Cells["AR3"].Style.Font.Bold = true;
            hoja.Cells["AS3"].Value = "Tasa/100";
            hoja.Cells["AS3"].Style.Font.Bold = true;

            hoja.Cells["AT3"].Value = "IF";
            hoja.Cells["AT3"].Style.Font.Bold = true;
            hoja.Cells["AU3"].Value = "IS";
            hoja.Cells["AU3"].Style.Font.Bold = true;
            hoja.Cells["AV3"].Value = "ILI";
            hoja.Cells["AV3"].Style.Font.Bold = true;
            hoja.Cells["AW3"].Value = "Tasa/100";
            hoja.Cells["AW3"].Style.Font.Bold = true;

            int nunInicial = 4;

            foreach (var dato in Indicadores)
            {
                hoja.Cells[string.Format("A{0}", nunInicial)].Value = dato.Contingencia;
                hoja.Cells[string.Format("A{0}", nunInicial)].Style.WrapText = true;

                hoja.Cells[string.Format("B{0}", nunInicial)].Value = dato.Ene == null ? 0 : dato.Ene.VariableIF;
                hoja.Cells[string.Format("C{0}", nunInicial)].Value = dato.Ene == null ? 0 : dato.Ene.VariableIS;
                hoja.Cells[string.Format("D{0}", nunInicial)].Value = dato.Ene == null ? 0 : dato.Ene.VariableILI;
                hoja.Cells[string.Format("E{0}", nunInicial)].Value = dato.Ene == null ? 0 : dato.Ene.Tasa;

                hoja.Cells[string.Format("F{0}", nunInicial)].Value = dato.Feb == null ? 0 : dato.Feb.VariableIF;
                hoja.Cells[string.Format("G{0}", nunInicial)].Value = dato.Feb == null ? 0 : dato.Feb.VariableIS;
                hoja.Cells[string.Format("H{0}", nunInicial)].Value = dato.Feb == null ? 0 : dato.Feb.VariableILI;
                hoja.Cells[string.Format("I{0}", nunInicial)].Value = dato.Feb == null ? 0 : dato.Feb.Tasa;

                hoja.Cells[string.Format("J{0}", nunInicial)].Value = dato.Mar == null ? 0 : dato.Mar.VariableIF;
                hoja.Cells[string.Format("K{0}", nunInicial)].Value = dato.Mar == null ? 0 : dato.Mar.VariableIS;
                hoja.Cells[string.Format("L{0}", nunInicial)].Value = dato.Mar == null ? 0 : dato.Mar.VariableILI;
                hoja.Cells[string.Format("M{0}", nunInicial)].Value = dato.Mar == null ? 0 : dato.Mar.Tasa;

                hoja.Cells[string.Format("N{0}", nunInicial)].Value = dato.Abr == null ? 0 : dato.Abr.VariableIF;
                hoja.Cells[string.Format("O{0}", nunInicial)].Value = dato.Abr == null ? 0 : dato.Abr.VariableIS;
                hoja.Cells[string.Format("P{0}", nunInicial)].Value = dato.Abr == null ? 0 : dato.Abr.VariableILI;
                hoja.Cells[string.Format("Q{0}", nunInicial)].Value = dato.Abr == null ? 0 : dato.Abr.Tasa;

                hoja.Cells[string.Format("R{0}", nunInicial)].Value = dato.May == null ? 0 : dato.May.VariableIF;
                hoja.Cells[string.Format("S{0}", nunInicial)].Value = dato.May == null ? 0 : dato.May.VariableIS;
                hoja.Cells[string.Format("T{0}", nunInicial)].Value = dato.May == null ? 0 : dato.May.VariableILI;
                hoja.Cells[string.Format("U{0}", nunInicial)].Value = dato.May == null ? 0 : dato.May.Tasa;

                hoja.Cells[string.Format("V{0}", nunInicial)].Value = dato.Jun == null ? 0 : dato.Jun.VariableIF;
                hoja.Cells[string.Format("W{0}", nunInicial)].Value = dato.Jun == null ? 0 : dato.Jun.VariableIS;
                hoja.Cells[string.Format("X{0}", nunInicial)].Value = dato.Jun == null ? 0 : dato.Jun.VariableILI;
                hoja.Cells[string.Format("Y{0}", nunInicial)].Value = dato.Jun == null ? 0 : dato.Jun.Tasa;

                hoja.Cells[string.Format("Z{0}", nunInicial)].Value = dato.Jul == null ? 0 : dato.Jul.VariableIF;
                hoja.Cells[string.Format("AA{0}", nunInicial)].Value = dato.Jul == null ? 0 : dato.Jul.VariableIS;
                hoja.Cells[string.Format("AB{0}", nunInicial)].Value = dato.Jul == null ? 0 : dato.Jul.VariableILI;
                hoja.Cells[string.Format("AC{0}", nunInicial)].Value = dato.Jul == null ? 0 : dato.Jul.Tasa;

                hoja.Cells[string.Format("AD{0}", nunInicial)].Value = dato.Ago == null ? 0 : dato.Ago.VariableIF;
                hoja.Cells[string.Format("AE{0}", nunInicial)].Value = dato.Ago == null ? 0 : dato.Ago.VariableIS;
                hoja.Cells[string.Format("AF{0}", nunInicial)].Value = dato.Ago == null ? 0 : dato.Ago.VariableILI;
                hoja.Cells[string.Format("AG{0}", nunInicial)].Value = dato.Ago == null ? 0 : dato.Ago.Tasa;

                hoja.Cells[string.Format("AH{0}", nunInicial)].Value = dato.Sep == null ? 0 : dato.Sep.VariableIF;
                hoja.Cells[string.Format("AI{0}", nunInicial)].Value = dato.Sep == null ? 0 : dato.Sep.VariableIS;
                hoja.Cells[string.Format("AJ{0}", nunInicial)].Value = dato.Sep == null ? 0 : dato.Sep.VariableILI;
                hoja.Cells[string.Format("AK{0}", nunInicial)].Value = dato.Sep == null ? 0 : dato.Sep.Tasa;

                hoja.Cells[string.Format("AL{0}", nunInicial)].Value = dato.Oct == null ? 0 : dato.Oct.VariableIF;
                hoja.Cells[string.Format("AM{0}", nunInicial)].Value = dato.Oct == null ? 0 : dato.Oct.VariableIS;
                hoja.Cells[string.Format("AN{0}", nunInicial)].Value = dato.Oct == null ? 0 : dato.Oct.VariableILI;
                hoja.Cells[string.Format("AO{0}", nunInicial)].Value = dato.Oct == null ? 0 : dato.Oct.Tasa;

                hoja.Cells[string.Format("AP{0}", nunInicial)].Value = dato.Nov == null ? 0 : dato.Nov.VariableIF;
                hoja.Cells[string.Format("AQ{0}", nunInicial)].Value = dato.Nov == null ? 0 : dato.Nov.VariableIS;
                hoja.Cells[string.Format("AR{0}", nunInicial)].Value = dato.Nov == null ? 0 : dato.Nov.VariableILI;
                hoja.Cells[string.Format("AS{0}", nunInicial)].Value = dato.Nov == null ? 0 : dato.Nov.Tasa;

                hoja.Cells[string.Format("AT{0}", nunInicial)].Value = dato.Dic == null ? 0 : dato.Dic.VariableIF;
                hoja.Cells[string.Format("AU{0}", nunInicial)].Value = dato.Dic == null ? 0 : dato.Dic.VariableIS;
                hoja.Cells[string.Format("AV{0}", nunInicial)].Value = dato.Dic == null ? 0 : dato.Dic.VariableILI;
                hoja.Cells[string.Format("AW{0}", nunInicial)].Value = dato.Dic == null ? 0 : dato.Dic.Tasa;

                nunInicial++;
            }

            hoja.Cells[string.Format("A{0}", nunInicial)].Value = "Total";
            hoja.Cells[string.Format("A{0}", nunInicial)].Style.WrapText = true;

            hoja.Cells[string.Format("B{0}", nunInicial)].Formula = string.Format("SUM(B4:B{0})", nunInicial-1);
            hoja.Cells[string.Format("C{0}", nunInicial)].Formula = string.Format("SUM(C4:C{0})", nunInicial - 1);
            hoja.Cells[string.Format("D{0}", nunInicial)].Formula = string.Format("SUM(D4:D{0})", nunInicial - 1);
            hoja.Cells[string.Format("E{0}", nunInicial)].Formula = string.Format("SUM(E4:E{0})", nunInicial - 1);

            hoja.Cells[string.Format("F{0}", nunInicial)].Formula = string.Format("SUM(F4:F{0})", nunInicial - 1);
            hoja.Cells[string.Format("G{0}", nunInicial)].Formula = string.Format("SUM(G4:G{0})", nunInicial - 1);
            hoja.Cells[string.Format("H{0}", nunInicial)].Formula = string.Format("SUM(H4:H{0})", nunInicial - 1);
            hoja.Cells[string.Format("I{0}", nunInicial)].Formula = string.Format("SUM(I4:I{0})", nunInicial - 1);

            hoja.Cells[string.Format("J{0}", nunInicial)].Formula = string.Format("SUM(J4:J{0})", nunInicial - 1);
            hoja.Cells[string.Format("K{0}", nunInicial)].Formula = string.Format("SUM(K4:K{0})", nunInicial - 1);
            hoja.Cells[string.Format("L{0}", nunInicial)].Formula = string.Format("SUM(L4:L{0})", nunInicial - 1);
            hoja.Cells[string.Format("M{0}", nunInicial)].Formula = string.Format("SUM(M4:M{0})", nunInicial - 1);

            hoja.Cells[string.Format("N{0}", nunInicial)].Formula = string.Format("SUM(N4:N{0})", nunInicial - 1);
            hoja.Cells[string.Format("O{0}", nunInicial)].Formula = string.Format("SUM(O4:O{0})", nunInicial - 1);
            hoja.Cells[string.Format("P{0}", nunInicial)].Formula = string.Format("SUM(P4:P{0})", nunInicial - 1);
            hoja.Cells[string.Format("Q{0}", nunInicial)].Formula = string.Format("SUM(Q4:Q{0})", nunInicial - 1);

            hoja.Cells[string.Format("R{0}", nunInicial)].Formula = string.Format("SUM(R4:R{0})", nunInicial - 1);
            hoja.Cells[string.Format("S{0}", nunInicial)].Formula = string.Format("SUM(S4:S{0})", nunInicial - 1);
            hoja.Cells[string.Format("T{0}", nunInicial)].Formula = string.Format("SUM(T4:T{0})", nunInicial - 1);
            hoja.Cells[string.Format("U{0}", nunInicial)].Formula = string.Format("SUM(U4:U{0})", nunInicial - 1);

            hoja.Cells[string.Format("V{0}", nunInicial)].Formula = string.Format("SUM(V4:V{0})", nunInicial - 1);
            hoja.Cells[string.Format("W{0}", nunInicial)].Formula = string.Format("SUM(W4:W{0})", nunInicial - 1);
            hoja.Cells[string.Format("X{0}", nunInicial)].Formula = string.Format("SUM(X4:X{0})", nunInicial - 1);
            hoja.Cells[string.Format("Y{0}", nunInicial)].Formula = string.Format("SUM(Y4:Y{0})", nunInicial - 1);

            hoja.Cells[string.Format("Z{0}", nunInicial)].Formula = string.Format("SUM(Z4:Z{0})", nunInicial - 1);
            hoja.Cells[string.Format("AA{0}", nunInicial)].Formula = string.Format("SUM(AA4:AA{0})", nunInicial - 1);
            hoja.Cells[string.Format("AB{0}", nunInicial)].Formula = string.Format("SUM(AB4:AB{0})", nunInicial - 1);
            hoja.Cells[string.Format("AC{0}", nunInicial)].Formula = string.Format("SUM(AC4:AC{0})", nunInicial - 1);

            hoja.Cells[string.Format("AD{0}", nunInicial)].Formula = string.Format("SUM(AD4:AD{0})", nunInicial - 1);
            hoja.Cells[string.Format("AE{0}", nunInicial)].Formula = string.Format("SUM(AE4:AE{0})", nunInicial - 1);
            hoja.Cells[string.Format("AF{0}", nunInicial)].Formula = string.Format("SUM(AF4:AF{0})", nunInicial - 1);
            hoja.Cells[string.Format("AG{0}", nunInicial)].Formula = string.Format("SUM(AG4:AG{0})", nunInicial - 1);

            hoja.Cells[string.Format("AH{0}", nunInicial)].Formula = string.Format("SUM(AH4:AH{0})", nunInicial - 1);
            hoja.Cells[string.Format("AI{0}", nunInicial)].Formula = string.Format("SUM(AI4:AI{0})", nunInicial - 1);
            hoja.Cells[string.Format("AJ{0}", nunInicial)].Formula = string.Format("SUM(AJ4:AJ{0})", nunInicial - 1);
            hoja.Cells[string.Format("AK{0}", nunInicial)].Formula = string.Format("SUM(AK4:AK{0})", nunInicial - 1);

            hoja.Cells[string.Format("AL{0}", nunInicial)].Formula = string.Format("SUM(AL4:AL{0})", nunInicial - 1);
            hoja.Cells[string.Format("AM{0}", nunInicial)].Formula = string.Format("SUM(AM4:AM{0})", nunInicial - 1);
            hoja.Cells[string.Format("AN{0}", nunInicial)].Formula = string.Format("SUM(AN4:AN{0})", nunInicial - 1);
            hoja.Cells[string.Format("AO{0}", nunInicial)].Formula = string.Format("SUM(AO4:AO{0})", nunInicial - 1);

            hoja.Cells[string.Format("AP{0}", nunInicial)].Formula = string.Format("SUM(AP4:AP{0})", nunInicial - 1);
            hoja.Cells[string.Format("AQ{0}", nunInicial)].Formula = string.Format("SUM(AQ4:AQ{0})", nunInicial - 1);
            hoja.Cells[string.Format("AR{0}", nunInicial)].Formula = string.Format("SUM(AR4:AR{0})", nunInicial - 1);
            hoja.Cells[string.Format("AS{0}", nunInicial)].Formula = string.Format("SUM(AS4:AS{0})", nunInicial - 1);

            hoja.Cells[string.Format("AT{0}", nunInicial)].Formula = string.Format("SUM(AT4:AT{0})", nunInicial - 1);
            hoja.Cells[string.Format("AU{0}", nunInicial)].Formula = string.Format("SUM(AU4:AU{0})", nunInicial - 1);
            hoja.Cells[string.Format("AV{0}", nunInicial)].Formula = string.Format("SUM(AV4:AV{0})", nunInicial - 1);
            hoja.Cells[string.Format("AW{0}", nunInicial)].Formula = string.Format("SUM(AW4:AW{0})", nunInicial - 1);

            hoja.Column(1).Width = 40;

            foreach (var cel in hoja.Cells[string.Format("A1:AW{0}", Indicadores.Count + 4)])
            {
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            }

            return excel.GetAsByteArray();

        }


        private IEnumerable<ResultadoConfiguracion> ObtenerConfiguracionHHTPorPeriodo(int anio, string Nit)
        {
            var resultado = indicadoresMg.Configuracion(anio, Nit);
            return resultado;
        }

        /// <summary>
        /// calcula el total de variables IF, IS, ILI, Horas y trabajadores para el periodo (anio)
        /// pasado por parametro
        /// </summary>
        /// <param name="anio"></param>
        /// <returns></returns>
        public IEnumerable<EDAcumuladoTotalContingencias> ObtenerAcumuladoTotalContingencias(int anio, int valorK, string Nit, int idEmpresaUsuaria, int tipoContingenciaComparar)
        {
            var resultado = indicadoresMg.ObtenerAcumuladoTotalContingencias(anio, tipoContingenciaComparar, Nit, idEmpresaUsuaria, tipoContingenciaComparar);
            if (resultado == null || resultado.Count <= 0)
            {
                //se configura esta respuesta para indicar que no hubo
                //datos asociados con ausencias
                return null;
            }
            var configuracionHHT = ObtenerConfiguracionHHTPorPeriodo(anio, Nit);
            if (configuracionHHT == null || configuracionHHT.Count() <= 0)
            {
                //se configura esta respuesta para indicar que no hubo
                //datos asociados con la configuración HHT
                return null;
            }
            var resultadoAcumulado = (from r in resultado
                                      join confHHT in configuracionHHT on r.Mes equals confHHT.Mes into query
                                      from conf in query.DefaultIfEmpty()
                                      select new EDAcumuladoTotalContingencias
                                      {
                                          Mes = r.Mes,
                                          VariableIF = (conf == null ? 0 :  Math.Round(((r.EventosPorMes / conf.TotalMes.Value) * valorK), 2)),
                                          VariableIS = (conf == null ? 0 : Math.Round(((r.DiasAusenciaPorMes / conf.TotalMes.Value) * valorK), 2)),
                                          VariableILI = (conf == null ? 0 : Math.Round(((((r.EventosPorMes / conf.TotalMes.Value) * valorK) * ((r.DiasAusenciaPorMes / conf.TotalMes.Value) * valorK)) / 1000), 2)),
                                          Tasa = (conf == null ? 0 : Math.Round(((r.EventosPorMes / conf.NumeroTrabajadores.Value) * 100), 2)),
                                          HorasTrabajadas = r.HorasTrabajadas,
                                          NumeroTrabajadores = r.NumeroTrabajadores
                                      }).ToList().OrderBy(r => r.Mes);

            var tmp = resultadoAcumulado.Where(r => r.HorasTrabajadas > 0).Select(r => r).FirstOrDefault();
            if (tmp != null)
            {

                var total = new TotalAcumulado()
                {
                    TotalVariableIF = Math.Round((resultado.Sum(r => r.EventosPorMes) / resultado.Sum(r => r.HorasTrabajadas)) * valorK, 2),
                    TotalVariableIS = Math.Round((resultado.Sum(r => r.DiasAusenciaPorMes) / resultado.Sum(r => r.HorasTrabajadas)) * valorK, 2),
                    TotalVariableILI = Math.Round(((((resultado.Sum(r => r.DiasAusenciaPorMes) / resultado.Sum(r => r.HorasTrabajadas)) * valorK) * ((resultado.Sum(r => r.DiasAusenciaPorMes) / resultado.Sum(r => r.HorasTrabajadas)) * valorK)) / 1000), 2),
                    TotalHorasTrabajadas = Math.Round(resultado.Sum(r => r.HorasTrabajadas), 2),
                    TotalNumeroTrabajadores = Math.Round(resultado.Sum(r => r.NumeroTrabajadores), 2),
                    TotalTasa = Math.Round(resultadoAcumulado.Sum(r => r.Tasa), 2),
                };
                resultadoAcumulado.FirstOrDefault().TotalPeriodo = total;
            }
            return resultadoAcumulado;
        }

        public byte [] ObtenerExcelAcumuladoTotalContingencias(int anio, int valorK, string Nit, int idEmpresaUsuaria, int tipoContingenciaComparar)
        {
            var Acumulado = ObtenerAcumuladoTotalContingencias(anio, valorK, Nit, idEmpresaUsuaria, tipoContingenciaComparar);

            ExcelPackage excel = new ExcelPackage();
            excel.Workbook.Worksheets.Add("AcumuladoContingencias");

            ExcelWorksheet hoja = excel.Workbook.Worksheets[1];

            hoja.Cells["A1:G1"].Merge = true;
            hoja.Cells["A1"].Value = "ACUMULADO TOTAL DE CONTINGENCIAS ";
            hoja.Cells["A1"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["A1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["A1"].Style.Font.Bold = true;
            hoja.Cells["A1"].Style.WrapText = true;

            hoja.Cells["A2"].Value = "MESES " ;
            hoja.Cells["A2"].Style.Font.Bold = true;

            hoja.Cells["B2"].Value = "IF";
            hoja.Cells["B2"].Style.Font.Bold = true;

            hoja.Cells["C2"].Value = "IS";
            hoja.Cells["C2"].Style.Font.Bold = true;

            hoja.Cells["D2"].Value = "ILI";
            hoja.Cells["D2"].Style.Font.Bold = true;

            hoja.Cells["E2"].Value = "Tasa/100";
            hoja.Cells["E2"].Style.Font.Bold = true;

            hoja.Cells["F2"].Value = "Horas Trabajadas";
            hoja.Cells["F2"].Style.Font.Bold = true;

            hoja.Cells["G2"].Value = "Nro. Trabajadores";            
            hoja.Cells["G2"].Style.Font.Bold = true;
            

            int nunInicial = 3;

            decimal totalVariableIF = 0;
            decimal totalVariableIS = 0;
            decimal totalVariableILI = 0;
            decimal totalTasa = 0;
            decimal totalHorasTrabajadas = 0;
            decimal totalNumeroTrabajadores = 0;

            foreach (var dato in Acumulado)
            {
                if(nunInicial == 3)
                {
                    totalVariableIF = dato.TotalPeriodo.TotalVariableIF;
                    totalVariableIS = dato.TotalPeriodo.TotalVariableIS;
                    totalVariableILI = dato.TotalPeriodo.TotalVariableILI;
                    totalTasa = dato.TotalPeriodo.TotalTasa;
                    totalHorasTrabajadas = dato.TotalPeriodo.TotalHorasTrabajadas;
                    totalNumeroTrabajadores = dato.TotalPeriodo.TotalNumeroTrabajadores;
                }

                hoja.Cells[string.Format("A{0}", nunInicial)].Value = ObtenerMes(dato.Mes);
                hoja.Cells[string.Format("A{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].Value = dato.VariableIF;
                hoja.Cells[string.Format("B{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("C{0}", nunInicial)].Value = dato.VariableIS;
                hoja.Cells[string.Format("C{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("D{0}", nunInicial)].Value = dato.VariableILI;
                hoja.Cells[string.Format("D{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("E{0}", nunInicial)].Value = dato.Tasa;
                hoja.Cells[string.Format("E{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("F{0}", nunInicial)].Value = dato.HorasTrabajadas;
                hoja.Cells[string.Format("F{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("G{0}", nunInicial)].Value = dato.NumeroTrabajadores;
                hoja.Cells[string.Format("G{0}", nunInicial)].Style.WrapText = true;

                nunInicial++;
            }

            hoja.Cells[string.Format("A{0}", nunInicial)].Value = "Anual";
            hoja.Cells[string.Format("A{0}", nunInicial)].Style.WrapText = true;
            hoja.Cells[string.Format("B{0}", nunInicial)].Value = totalVariableIF;
            hoja.Cells[string.Format("B{0}", nunInicial)].Style.WrapText = true;
            hoja.Cells[string.Format("C{0}", nunInicial)].Value = totalVariableIS;
            hoja.Cells[string.Format("C{0}", nunInicial)].Style.WrapText = true;
            hoja.Cells[string.Format("D{0}", nunInicial)].Value = totalVariableILI;
            hoja.Cells[string.Format("D{0}", nunInicial)].Style.WrapText = true;
            hoja.Cells[string.Format("E{0}", nunInicial)].Value = totalTasa;
            hoja.Cells[string.Format("E{0}", nunInicial)].Style.WrapText = true;
            hoja.Cells[string.Format("F{0}", nunInicial)].Value = totalHorasTrabajadas;
            hoja.Cells[string.Format("F{0}", nunInicial)].Style.WrapText = true;
            hoja.Cells[string.Format("G{0}", nunInicial)].Value = totalNumeroTrabajadores;
            hoja.Cells[string.Format("G{0}", nunInicial)].Style.WrapText = true;


            hoja.Column(1).Width = 15;
            hoja.Column(2).Width = 15;
            hoja.Column(3).Width = 15;
            hoja.Column(4).Width = 15;
            hoja.Column(5).Width = 20;
            hoja.Column(6).Width = 20;
            hoja.Column(7).Width = 20;

            foreach (var cel in hoja.Cells[string.Format("A1:G{0}", Acumulado.Count() + 3)])
            {
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            }

            //AGREGAMOS LA GRAFICA
            var chartprimerano = hoja.Drawings.AddChart("ACUMULADO TOTAL DE CONTINGENCIAS " + anio, OfficeOpenXml.Drawing.Chart.eChartType.ColumnClustered3D);
            chartprimerano.Title.Text = "ACUMULADO TOTAL DE CONTINGENCIAS " + anio;
            chartprimerano.SetPosition(1, 0, 9, 0);
            chartprimerano.SetSize(800, 400); // Tamaño de la gráfica
            chartprimerano.Legend.Remove(); // Si desea eliminar la leyenda

            // Define donde está la información de la gráfica.
            var serieprimerano = chartprimerano.Series.Add(hoja.Cells["B2:B14"], hoja.Cells["A2:A14"]);
            serieprimerano = chartprimerano.Series.Add(hoja.Cells["C2:C14"], hoja.Cells["A2:A14"]);
            serieprimerano = chartprimerano.Series.Add(hoja.Cells["D2:D14"], hoja.Cells["A2:A14"]);
            
            return excel.GetAsByteArray();
        }

        public byte [] ObtenerExcelComparativo(int anio1, int anio2, int valorK, string Nit, int idEmpresaUsuaria, int tipoContingenciaComparar)
        {
            var listaPrimerAno = ObtenerAcumuladoTotalContingencias(anio1, valorK, Nit, idEmpresaUsuaria, tipoContingenciaComparar);
            var listaSegundoAno = ObtenerAcumuladoTotalContingencias(anio2, valorK, Nit, idEmpresaUsuaria, tipoContingenciaComparar);

            ExcelPackage excel = new ExcelPackage();
            excel.Workbook.Worksheets.Add("AcumuladoContingencias");

            ExcelWorksheet hoja = excel.Workbook.Worksheets[1];

            hoja.Cells["A1:D1"].Merge = true;
            hoja.Cells["A1"].Value = "ACUMULADO TOTAL DE CONTINGENCIAS " + anio1;
            hoja.Cells["A1:D1"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["A1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["A1"].Style.Font.Bold = true;
            hoja.Cells["A1"].Style.WrapText = true;

            hoja.Cells["A2"].Value = "MESES ";
            hoja.Cells["A2"].Style.Font.Bold = true;
            hoja.Cells["A2"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

            hoja.Cells["B2"].Value = "IF";
            hoja.Cells["B2"].Style.Font.Bold = true;
            hoja.Cells["B2"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

            hoja.Cells["C2"].Value = "IS";
            hoja.Cells["C2"].Style.Font.Bold = true;
            hoja.Cells["C2"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

            hoja.Cells["D2"].Value = "ILI";
            hoja.Cells["D2"].Style.Font.Bold = true;
            hoja.Cells["D2"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

            int nunInicial = 3;

            decimal totalVariableIF = 0;
            decimal totalVariableIS = 0;
            decimal totalVariableILI = 0;
            
            foreach (var dato in listaPrimerAno)
            {
                if (nunInicial == 3)
                {
                    totalVariableIF = dato.TotalPeriodo.TotalVariableIF;
                    totalVariableIS = dato.TotalPeriodo.TotalVariableIS;
                    totalVariableILI = dato.TotalPeriodo.TotalVariableILI;                   
                }

                hoja.Cells[string.Format("A{0}", nunInicial)].Value = ObtenerMes(dato.Mes);
                hoja.Cells[string.Format("A{0}", nunInicial)].Style.WrapText = true;

                hoja.Cells[string.Format("B{0}", nunInicial)].Value = dato.VariableIF;
                hoja.Cells[string.Format("B{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                hoja.Cells[string.Format("C{0}", nunInicial)].Value = dato.VariableIS;
                hoja.Cells[string.Format("C{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("C{0}", nunInicial)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                hoja.Cells[string.Format("D{0}", nunInicial)].Value = dato.VariableILI;
                hoja.Cells[string.Format("D{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("D{0}", nunInicial)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                nunInicial++;
            }

            hoja.Cells[string.Format("A{0}", nunInicial)].Value = "Anual";
            hoja.Cells[string.Format("A{0}", nunInicial)].Style.WrapText = true;
            hoja.Cells[string.Format("A{0}", nunInicial)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells[string.Format("B{0}", nunInicial)].Value = totalVariableIF;
            hoja.Cells[string.Format("B{0}", nunInicial)].Style.WrapText = true;
            hoja.Cells[string.Format("B{0}", nunInicial)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells[string.Format("C{0}", nunInicial)].Value = totalVariableIS;
            hoja.Cells[string.Format("C{0}", nunInicial)].Style.WrapText = true;
            hoja.Cells[string.Format("C{0}", nunInicial)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells[string.Format("D{0}", nunInicial)].Value = totalVariableILI;
            hoja.Cells[string.Format("D{0}", nunInicial)].Style.WrapText = true;
            hoja.Cells[string.Format("D{0}", nunInicial)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

            hoja.Column(1).Width = 15;
            hoja.Column(2).Width = 15;
            hoja.Column(3).Width = 15;
            hoja.Column(4).Width = 15;

            //AGREGAMOS LA GRAFICA
            var chartprimerano = hoja.Drawings.AddChart("ACUMULADO TOTAL DE CONTINGENCIAS " + anio1, OfficeOpenXml.Drawing.Chart.eChartType.ColumnClustered3D);
            chartprimerano.Title.Text = "ACUMULADO TOTAL DE CONTINGENCIAS " + anio1;
            chartprimerano.SetPosition(1, 0, 6, 0);
            chartprimerano.SetSize(800, 400); // Tamaño de la gráfica
            chartprimerano.Legend.Remove(); // Si desea eliminar la leyenda

            // Define donde está la información de la gráfica.
            var serieprimerano = chartprimerano.Series.Add(hoja.Cells["B2:B14"], hoja.Cells["A2:A14"]);
            serieprimerano = chartprimerano.Series.Add(hoja.Cells["C2:C14"], hoja.Cells["A2:A14"]);
            serieprimerano = chartprimerano.Series.Add(hoja.Cells["D2:D14"], hoja.Cells["A2:A14"]);

            //se genera la tabla dos
            hoja.Cells["A18:D18"].Merge = true;
            hoja.Cells["A18"].Value = "ACUMULADO TOTAL DE CONTINGENCIAS " + anio2;
            hoja.Cells["A18:D18"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["A18"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["A18"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["A18"].Style.Font.Bold = true;
            hoja.Cells["A18"].Style.WrapText = true;

            hoja.Cells["A19"].Value = "MESES ";
            hoja.Cells["A19"].Style.Font.Bold = true;
            hoja.Cells["A19"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

            hoja.Cells["B19"].Value = "IF";
            hoja.Cells["B19"].Style.Font.Bold = true;
            hoja.Cells["B19"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

            hoja.Cells["C19"].Value = "IS";
            hoja.Cells["C19"].Style.Font.Bold = true;
            hoja.Cells["C19"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

            hoja.Cells["D19"].Value = "ILI";
            hoja.Cells["D19"].Style.Font.Bold = true;
            hoja.Cells["D19"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            
            nunInicial = 20;

            totalVariableIF = 0;
            totalVariableIS = 0;
            totalVariableILI = 0;

            foreach (var dato in listaSegundoAno)
            {
                if (nunInicial == 3)
                {
                    totalVariableIF = dato.TotalPeriodo.TotalVariableIF;
                    totalVariableIS = dato.TotalPeriodo.TotalVariableIS;
                    totalVariableILI = dato.TotalPeriodo.TotalVariableILI;
                }

                hoja.Cells[string.Format("A{0}", nunInicial)].Value = ObtenerMes(dato.Mes);
                hoja.Cells[string.Format("A{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("A{0}", nunInicial)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                hoja.Cells[string.Format("B{0}", nunInicial)].Value = dato.VariableIF;
                hoja.Cells[string.Format("B{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                hoja.Cells[string.Format("C{0}", nunInicial)].Value = dato.VariableIS;
                hoja.Cells[string.Format("C{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("C{0}", nunInicial)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                hoja.Cells[string.Format("D{0}", nunInicial)].Value = dato.VariableILI;
                hoja.Cells[string.Format("D{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("D{0}", nunInicial)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                nunInicial++;
            }

            hoja.Cells[string.Format("A{0}", nunInicial)].Value = "Anual";
            hoja.Cells[string.Format("A{0}", nunInicial)].Style.WrapText = true;
            hoja.Cells[string.Format("A{0}", nunInicial)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells[string.Format("B{0}", nunInicial)].Value = totalVariableIF;
            hoja.Cells[string.Format("B{0}", nunInicial)].Style.WrapText = true;
            hoja.Cells[string.Format("B{0}", nunInicial)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells[string.Format("C{0}", nunInicial)].Value = totalVariableIS;
            hoja.Cells[string.Format("C{0}", nunInicial)].Style.WrapText = true;
            hoja.Cells[string.Format("C{0}", nunInicial)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells[string.Format("D{0}", nunInicial)].Value = totalVariableILI;
            hoja.Cells[string.Format("D{0}", nunInicial)].Style.WrapText = true;
            hoja.Cells[string.Format("D{0}", nunInicial)].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

            //AGREGAMOS LA GRAFICA
            var chartSegunAno = hoja.Drawings.AddChart("ACUMULADO TOTAL DE CONTINGENCIA " + anio2, OfficeOpenXml.Drawing.Chart.eChartType.ColumnClustered3D);
            chartSegunAno.Title.Text = "ACUMULADO TOTAL DE CONTINGENCIAS " + anio2;
            chartSegunAno.SetPosition(23, 0, 6, 0);
            chartSegunAno.SetSize(800, 400); // Tamaño de la gráfica
            chartSegunAno.Legend.Remove(); // Si desea eliminar la leyenda

            // Define donde está la información de la gráfica.
            var serieSegundoano = chartSegunAno.Series.Add(hoja.Cells["B19:B31"], hoja.Cells["A19:A31"]);
            serieSegundoano = chartSegunAno.Series.Add(hoja.Cells["C19:C31"], hoja.Cells["A19:A31"]);
            serieSegundoano = chartSegunAno.Series.Add(hoja.Cells["D19:D31"], hoja.Cells["A19:A31"]);

            return excel.GetAsByteArray();
        }

        private string  ObtenerMes(int mes)
        {
            switch(mes)
            {
                case 1: return "Enero";
                case 2: return "Febrero";
                case 3: return "Marzo";
                case 4: return "Abril";
                case 5: return "Mayo";
                case 6: return "Junio";
                case 7: return "Julio";
                case 8: return "Agosto";
                case 9: return "Septiembre";
                case 10: return "Octubre";
                case 11: return "Noviembre";
                case 12: return "Diciembre";
                default: return "";
            }
        }
    }
}
