using SG_SST.Enumeraciones;
using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Interfaces.Planificacion;
using SG_SST.InterfazManager.Planificacion;
using System.Collections.Generic;
using OfficeOpenXml;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace SG_SST.Logica.Planificacion
{
    public class LNEvaluacionStandMinimos
    {
        private static IEvaluacionEstandMinimos esm = IMEvaluaccion.EstandaresMinimos();
        public List<EDCiclo> ObtenerCiclos(string Nit)
        {
            List<EDCiclo> Ciclos = new List<EDCiclo>();
            var ciclos = esm.ObtenerCiclos();
            foreach (var ciclo in ciclos)
            {
                ciclo.StandPorEvaluar = esm.ObtenerCantidaEstdPorEvaluas(ciclo.Id_Ciclo, Nit);
                ciclo.CantidadCriterios = esm.ObtenerCantidadCriteriosPorEstandar(ciclo.Id_Ciclo);
                Ciclos.Add(ciclo);
            }

            return Ciclos;
        }

        public EDCiclo ObtenerEstandaresPorCiclo(int idCiclo, string Nit)
        {
            EDCiclo ciclo = null;
            ciclo = esm.ObtenerEstandarPorCiclo(idCiclo, Nit);
            if (ciclo != null)
            {
                ciclo.CantidadCriterios = esm.ObtenerCantidadCriteriosPorEstandar(ciclo.Id_Ciclo);
                ciclo.StandPorEvaluar = esm.ObtenerCantidaEstdPorEvaluas(ciclo.Id_Ciclo, Nit);
            }
            return ciclo;
        }

        public EDCiclo GuardarEvaluacionEstandar(EDEvaluacionEstandarMinimo EvaluacionEstandar)
        {
            bool result = false;
            EDCiclo ciclo = null;
            EDEvaluacionEstandarMinimo eval = esm.GuardarEvaluacionEstandar(EvaluacionEstandar);
            if (eval.IdEvalEstandarMinimo > 0)
            {
                ciclo = esm.ObtenerEstandarPorCiclo(EvaluacionEstandar.IdCiclo, EvaluacionEstandar.IdCriterio, EvaluacionEstandar.IdEmpresaEvaluar);
                if (ciclo != null)
                {
                    ciclo.CantidadCriterios = esm.ObtenerCantidadCriteriosPorEstandar(ciclo.Id_Ciclo);
                    ciclo.StandPorEvaluar = esm.ObtenerCantidaEstdPorEvaluas(ciclo.Id_Ciclo, eval.IdEmpresaEvaluar);
                }
            }

            return ciclo;
        }

        public EDValoracionFinal ObtenerCalificacionFinal(string Nit)
        {
            return esm.ObtenerCalificacion(Nit);
        }

        public byte[] GenerarArchivoExcel(string Nit)
        {
            List<EDCiclo> Ciclos = ObternerDatosArchivoExcel(Nit);
            if (Ciclos == null)
                return new byte[] { };
            // Creamos el archivo
            ExcelPackage em = new ExcelPackage();

            //Le añadimos los 'worksheets' que necesitemos.
            //En este caso añadiremos solo uno
            em.Workbook.Worksheets.Add("Estandares Minimos");

            //Creamos un objecto tipo ExcelWorksheet para
            //manejarlo facilmente.
            ExcelWorksheet hoja = em.Workbook.Worksheets[1];

            int nunInicialCL = 6;
            int nunInicialEst = 6;
            int nunInicialSub = 6;
            int nunInicialCr = 6;

            //Se arma el encabezado del Archivo
            CrearCabeceraArchivo(hoja);

            //Se Arma el cuerpo del archivo con los datos recuperados de la base de datos
            foreach (var ciclo in Ciclos)
            {
                //Se Inserta las columnas de los ciclos
                hoja.Cells[string.Format("A{0}:A{1}", nunInicialCL, ciclo.CantidadCriterios + nunInicialCL - 1)].Merge = true;
                hoja.Cells[string.Format("A{0}", nunInicialCL)].Value = ciclo.Nombre;
                hoja.Cells[string.Format("A{0}", nunInicialCL)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                hoja.Cells[string.Format("A{0}", nunInicialCL)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                hoja.Cells[string.Format("A{0}", nunInicialCL)].Style.TextRotation = 90;
                hoja.Cells[string.Format("A{0}", nunInicialCL)].Style.Font.Bold = true;

                foreach (var estandar in ciclo.Estandares)
                {
                    var cantCell = ((Ciclos.Where(c => c.Id_Ciclo == ciclo.Id_Ciclo).Select(c => c).FirstOrDefault())
                                    .Estandares.Where(e => e.Id_Estandar == estandar.Id_Estandar).Select(e => e).FirstOrDefault())
                                    .SubEstandares.SelectMany(p => p.Criterios).ToList().Count();
                    //Se Inserta las columnas de los Estandares
                    hoja.Cells[string.Format("B{0}:B{1}", nunInicialEst, cantCell + nunInicialEst - 1)].Merge = true;
                    hoja.Cells[string.Format("B{0}:B{1}", nunInicialEst, cantCell + nunInicialEst - 1)].Style.WrapText = true;
                    hoja.Cells[string.Format("B{0}", nunInicialEst)].Value = estandar.Descripcion;
                    hoja.Cells[string.Format("B{0}", nunInicialEst)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    hoja.Cells[string.Format("B{0}", nunInicialEst)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    hoja.Cells[string.Format("B{0}", nunInicialEst)].Style.TextRotation = 90;
                    hoja.Cells[string.Format("B{0}", nunInicialEst)].Style.Font.Bold = true;

                    foreach (var sub in estandar.SubEstandares)
                    {
                        var cantCellSub = (((Ciclos.Where(c => c.Id_Ciclo == ciclo.Id_Ciclo).Select(c => c).FirstOrDefault())
                                        .Estandares.Where(e => e.Id_Estandar == estandar.Id_Estandar).Select(e => e).FirstOrDefault())
                                        .SubEstandares.Where(sb => sb.Id_SubEstandar == sub.Id_SubEstandar).Select(sb => sb).FirstOrDefault())
                                        .Criterios.Select(c => c).ToList().Count();
                        //Se Inserta las columnas de los subestandades
                        hoja.Cells[string.Format("C{0}:C{1}", nunInicialSub, cantCellSub + nunInicialSub - 1)].Merge = true;
                        hoja.Cells[string.Format("C{0}:C{1}", nunInicialEst, cantCell + nunInicialEst - 1)].Style.WrapText = true;
                        hoja.Cells[string.Format("C{0}", nunInicialSub)].Value = sub.Descripcion_Corta;
                        hoja.Cells[string.Format("C{0}", nunInicialSub)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        hoja.Cells[string.Format("C{0}", nunInicialSub)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        hoja.Cells[string.Format("C{0}", nunInicialSub)].Style.Font.Size = 10;

                        //Se Inserta las columnas de los porcentajes maximos por cada subestandar
                        hoja.Cells[string.Format("F{0}:F{1}", nunInicialSub, cantCellSub + nunInicialSub - 1)].Merge = true;
                        hoja.Cells[string.Format("F{0}:F{1}", nunInicialSub, cantCellSub + nunInicialSub - 1)].Style.WrapText = true;
                        hoja.Cells[string.Format("F{0}", nunInicialSub)].Value = sub.Procentaje_Max;
                        hoja.Cells[string.Format("F{0}", nunInicialSub)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        hoja.Cells[string.Format("F{0}", nunInicialSub)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        hoja.Cells[string.Format("F{0}", nunInicialSub)].Style.Font.Size = 10;

                        //Se Inserta las columnas de las calificaciones totales
                        hoja.Cells[string.Format("K{0}:K{1}", nunInicialSub, cantCellSub + nunInicialSub - 1)].Merge = true;
                        hoja.Cells[string.Format("K{0}:K{1}", nunInicialSub, cantCellSub + nunInicialSub - 1)].Style.WrapText = true;
                        hoja.Cells[string.Format("K{0}:K{1}", nunInicialSub, cantCellSub + nunInicialSub - 1)].OfType<int>();
                        hoja.Cells[string.Format("K{0}", nunInicialSub)].Value = sub.CalTotal;
                        hoja.Cells[string.Format("K{0}", nunInicialSub)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        hoja.Cells[string.Format("K{0}", nunInicialSub)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        hoja.Cells[string.Format("K{0}", nunInicialSub)].Style.Font.Size = 10;

                        foreach (var criterio in sub.Criterios)
                        {
                            //Se Inserta las columnas de cada creiterio
                            hoja.Cells[string.Format("D{0}", nunInicialCr)].Style.WrapText = true;
                            hoja.Cells[string.Format("D{0}", nunInicialCr)].Value = criterio.Descripcion_Corta;
                            hoja.Cells[string.Format("D{0}", nunInicialCr)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                            hoja.Cells[string.Format("D{0}", nunInicialCr)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            hoja.Cells[string.Format("D{0}", nunInicialCr)].Style.Font.Size = 8;

                            //Se Inserta las columnas de la calificacion que puede tomar cada pregunta
                            hoja.Cells[string.Format("E{0}", nunInicialCr)].Style.WrapText = true;
                            hoja.Cells[string.Format("E{0}", nunInicialCr)].Value = ObtenerValorConformato(criterio.ValPorPregunta);
                            hoja.Cells[string.Format("E{0}", nunInicialCr)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                            hoja.Cells[string.Format("E{0}", nunInicialCr)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            hoja.Cells[string.Format("E{0}", nunInicialCr)].Style.Font.Size = 9;

                            if (criterio.Evaluacion != null)
                            {
                                if (criterio.Evaluacion.IdValoracionCriterio == (int)EnumPlanificacion.ValoracionStandares.CumpleTotalMente)
                                {
                                    //Se Inserta las columnas con valor 1 de los criterios calificados como cumple totalemte
                                    hoja.Cells[string.Format("G{0}", nunInicialCr)].Style.WrapText = true;
                                    hoja.Cells[string.Format("G{0}", nunInicialCr)].Value = "X";
                                    hoja.Cells[string.Format("G{0}", nunInicialCr)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                    hoja.Cells[string.Format("G{0}", nunInicialCr)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                    hoja.Cells[string.Format("G{0}", nunInicialCr)].Style.Font.Size = 9;

                                    hoja.Cells[string.Format("H{0}", nunInicialCr)].Value = "";
                                    hoja.Cells[string.Format("I{0}", nunInicialCr)].Value = "";
                                    hoja.Cells[string.Format("J{0}", nunInicialCr)].Value = "";
                                }
                                else if (criterio.Evaluacion.IdValoracionCriterio == (int)EnumPlanificacion.ValoracionStandares.NoCumple)
                                {
                                    //Se Inserta las columnas con valor 1 de los criterios calificados como no cumple
                                    hoja.Cells[string.Format("H{0}", nunInicialCr)].Style.WrapText = true;
                                    hoja.Cells[string.Format("H{0}", nunInicialCr)].Value = "X";
                                    hoja.Cells[string.Format("H{0}", nunInicialCr)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                    hoja.Cells[string.Format("H{0}", nunInicialCr)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                    hoja.Cells[string.Format("H{0}", nunInicialCr)].Style.Font.Size = 9;

                                    hoja.Cells[string.Format("G{0}", nunInicialCr)].Value = "";
                                    hoja.Cells[string.Format("I{0}", nunInicialCr)].Value = "";
                                    hoja.Cells[string.Format("J{0}", nunInicialCr)].Value = "";
                                }
                                else if (criterio.Evaluacion.IdValoracionCriterio == (int)EnumPlanificacion.ValoracionStandares.NoAplica)
                                {
                                    if (criterio.Evaluacion.Justificacion.Equals("1"))
                                    {
                                        //Se Inserta las columnas con valor 1 de los criterios calificados como no aplica y con justificacion
                                        hoja.Cells[string.Format("I{0}", nunInicialCr)].Style.WrapText = true;
                                        hoja.Cells[string.Format("I{0}", nunInicialCr)].Value = "X";
                                        hoja.Cells[string.Format("I{0}", nunInicialCr)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                        hoja.Cells[string.Format("I{0}", nunInicialCr)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                        hoja.Cells[string.Format("I{0}", nunInicialCr)].Style.Font.Size = 9;

                                        hoja.Cells[string.Format("G{0}", nunInicialCr)].Value = "";
                                        hoja.Cells[string.Format("H{0}", nunInicialCr)].Value = "";
                                        hoja.Cells[string.Format("J{0}", nunInicialCr)].Value = "";
                                    }
                                    else
                                    {
                                        //Se Inserta las columnas con valor 1 de los criterios calificados como no aplica sin justificacion
                                        hoja.Cells[string.Format("J{0}", nunInicialCr)].Style.WrapText = true;
                                        hoja.Cells[string.Format("J{0}", nunInicialCr)].Value = "X";
                                        hoja.Cells[string.Format("J{0}", nunInicialCr)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                        hoja.Cells[string.Format("J{0}", nunInicialCr)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                        hoja.Cells[string.Format("J{0}", nunInicialCr)].Style.Font.Size = 9;

                                        hoja.Cells[string.Format("G{0}", nunInicialCr)].Value = "";
                                        hoja.Cells[string.Format("H{0}", nunInicialCr)].Value = "";
                                        hoja.Cells[string.Format("I{0}", nunInicialCr)].Value = "";
                                    }
                                }
                            }
                            else
                            {
                                hoja.Cells[string.Format("G{0}", nunInicialCr)].Value = "";
                                hoja.Cells[string.Format("H{0}", nunInicialCr)].Value = "";
                                hoja.Cells[string.Format("I{0}", nunInicialCr)].Value = "";
                                hoja.Cells[string.Format("J{0}", nunInicialCr)].Value = "";
                            }

                            nunInicialCr++;
                        }
                        nunInicialSub = nunInicialSub + cantCellSub;
                    }
                    nunInicialEst = nunInicialEst + cantCell;
                }
                nunInicialCL = nunInicialCL + ciclo.CantidadCriterios;
            }


            //Se genera el pie del informe 
            CrearFinDeArchivo(hoja);

            foreach (var cel in hoja.Cells["A6:K66"])
            {
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            }

            int i;
            for (i = 6; i < nunInicialCL + 1; i++)
            {
                hoja.Row(i).Height = 60;
            }
            hoja.Row(i).Height = 40;
            hoja.Row(i + 1).Height = 30;
            hoja.Column(3).Width = 25;
            hoja.Column(4).Width = 25;
            //ep.Save();
            return em.GetAsByteArray();
        }

        public string ObtenerValorConformato(string valor)
        {
            try
            {
                return Regex.Match(valor, @"(([0-9]?(\.|\,)[1-9])*0*[1-9]*)*").Value;
            }
            catch
            {
                return valor;
            }
        }

        private static void CrearFinDeArchivo(ExcelWorksheet hoja)
        {
            hoja.Cells["A66:E66"].Merge = true;
            hoja.Cells["A66"].Value = "TOTALES";
            hoja.Cells["A66"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["A66"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["A66"].Style.Font.Size = 14;
            hoja.Cells["A66"].Style.Font.Bold = true;
            hoja.Cells["A66:E66"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

            hoja.Cells["F66"].Value = 100;
            hoja.Cells["F66"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["F66"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["F66"].Style.Font.Size = 14;
            hoja.Cells["F66"].Style.Font.Bold = true;
            hoja.Cells["F66"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

            hoja.Cells["G66"].Value = "";
            hoja.Cells["H66"].Value = "";
            hoja.Cells["I66"].Value = "";
            hoja.Cells["J66"].Value = "";

            hoja.Cells["K66"].Formula = "SUM(K6:K65)";
            hoja.Cells["K66"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["K66"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["K66"].Style.Font.Size = 14;
            hoja.Cells["K66"].Style.Font.Bold = true;
            hoja.Cells["K66"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

            hoja.Cells["A67:K67"].Merge = true;
            hoja.Cells["A67"].Value = "*Cuando se cumple con el ítem del estándar la calificación será la máxima del respectivo ítem, de lo contrario su calificación será igual a cero (0)                                                                                                                                Si el estándar No Aplica, se deberá justificar tal situación y se calificará con el porcentaje máximo del ítem indicado para cada estándar. En caso de no justificarse, la calificación del estándar será igual a cero (0)";
            hoja.Cells["A67"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["A67"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Justify;
            hoja.Cells["A67"].Style.Font.Size = 8;
            hoja.Cells["A67:K67"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);


            hoja.Cells["A68:K68"].Merge = true;
            hoja.Cells["A68"].Value = "SSTEl presente formulario es documento público, no se debe consignar hechos o manifestaciones falsas y está sujeta a las sanciones establecidas en los artículos 288 y 294 de la Ley 599 de 2000(Código Penal Colombiano).";
            hoja.Cells["A68"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["A68"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Justify;
            hoja.Cells["A68"].Style.Font.Size = 8;
            hoja.Cells["A68:K68"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

            hoja.Cells["A69:C69"].Merge = true;
            hoja.Cells["A69"].Value = "FIRMA DEL EMPLEADOR O CONTRATANTE";
            hoja.Cells["A69"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["A69"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["A69"].Style.Font.Size = 10;
            hoja.Cells["A69:C69"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

            hoja.Cells["D69:F69"].Merge = true;
            hoja.Cells["D69"].Value = "";
            hoja.Cells["D69:F69"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

            hoja.Cells["G69:K69"].Merge = true;
            hoja.Cells["G69"].Value = "FIRMA DEL RESPONSABLE  DE LA EJECUCIÓN SG- SST";
            hoja.Cells["G69"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["G69"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["G69"].Style.Font.Size = 10;
            hoja.Cells["G69:K69"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
        }

        private static void CrearCabeceraArchivo(ExcelWorksheet hoja)
        {
            hoja.Cells["A1:K1"].Merge = true;
            hoja.Cells["A1"].Value = "ESTÁNDARES MÍNIMOS SG-SST";
            hoja.Cells["A1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;            
            hoja.Cells["A1"].Style.Font.Size = 30;
            hoja.Cells["A1"].Style.Font.Bold = true;
            hoja.Cells["A1:K1"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);

            hoja.Cells["A2:K2"].Merge = true;
            hoja.Cells["A2:K2"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["A2"].Value = "TABLA DE VALORES Y CALIFICACIÓN";
            hoja.Cells["A2"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["A2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["A2"].Style.Font.Size = 18;
            hoja.Cells["A2"].Style.Font.Bold = true;

            hoja.Cells["A3:A5"].Merge = true;
            hoja.Cells["A3:A5"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["A3"].Value = "CICLO";
            hoja.Cells["A3"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["A3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["A3"].Style.WrapText = true;
            hoja.Cells["A3"].Style.Font.Size = 9;
            hoja.Cells["A3"].Style.Font.Bold = true;

            hoja.Cells["B3:C5"].Merge = true;
            hoja.Cells["B3:C5"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["B3"].Value = "ESTÁNDAR";
            hoja.Cells["B3"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["B3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["B3"].Style.WrapText = true;
            hoja.Cells["B3"].Style.Font.Size = 9;
            hoja.Cells["B3"].Style.Font.Bold = true;

            hoja.Cells["D3:D5"].Merge = true;
            hoja.Cells["D3:D5"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["D3"].Value = "INTEM DEL ESTÁNDAR";
            hoja.Cells["D3"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["D3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["D3"].Style.WrapText = true;
            hoja.Cells["D3"].Style.Font.Size = 9;
            hoja.Cells["D3"].Style.Font.Bold = true;

            hoja.Cells["E3:E5"].Merge = true;
            hoja.Cells["E3:E5"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["E3"].Value = "VALOR DEL ÍTEM";
            hoja.Cells["E3"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["E3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["E3"].Style.WrapText = true;
            hoja.Cells["E3"].Style.Font.Size = 9;
            hoja.Cells["E3"].Style.Font.Bold = true;

            hoja.Cells["F3:F5"].Merge = true;
            hoja.Cells["F3:F5"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["F3"].Value = "PESO %";
            hoja.Cells["F3"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["F3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["F3"].Style.WrapText = true;
            hoja.Cells["F3"].Style.Font.Size = 9;
            hoja.Cells["F3"].Style.Font.Bold = true;

            hoja.Cells["G3:J3"].Merge = true;
            hoja.Cells["G3:J5"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["G3"].Value = "PUNTAJE POSIBLE";
            hoja.Cells["G3"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["G3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["G3"].Style.WrapText = true;
            hoja.Cells["G3"].Style.Font.Size = 9;
            hoja.Cells["G3"].Style.Font.Bold = true;

            hoja.Cells["G4:G5"].Merge = true;
            hoja.Cells["G4:G5"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["G4"].Value = "CUMPLE TOTALMENTE";
            hoja.Cells["G4"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["G4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["G4"].Style.WrapText = true;
            hoja.Cells["G4"].Style.Font.Size = 8;
            hoja.Cells["G4"].Style.Font.Bold = true;

            hoja.Cells["H4:H5"].Merge = true;
            hoja.Cells["H4:H5"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["H4"].Value = "NO CUMPLE";
            hoja.Cells["H4"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["H4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["H4"].Style.WrapText = true;
            hoja.Cells["H4"].Style.Font.Size = 9;
            hoja.Cells["H4"].Style.Font.Bold = true;

            hoja.Cells["I4:J4"].Merge = true;
            hoja.Cells["I4:J4"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["I4"].Value = "NO APLICA";
            hoja.Cells["I4"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["I4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["I4"].Style.WrapText = true;
            hoja.Cells["I4"].Style.Font.Size = 9;
            hoja.Cells["I4"].Style.Font.Bold = true;

            hoja.Cells["I5"].Value = "JUSTIFICA";
            hoja.Cells["I5"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["I5"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["I5"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["I5"].Style.WrapText = true;
            hoja.Cells["I5"].Style.Font.Size = 9;
            hoja.Cells["I5"].Style.Font.Bold = true;

            hoja.Cells["J5"].Value = "NO JUSTIFICA";
            hoja.Cells["J5"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["J5"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["J5"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["J5"].Style.WrapText = true;
            hoja.Cells["J5"].Style.Font.Size = 9;
            hoja.Cells["J5"].Style.Font.Bold = true;

            hoja.Cells["K3:K5"].Merge = true;
            hoja.Cells["K3:K5"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["K3"].Value = "CALIFICACIÓN ";
            hoja.Cells["K3"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["K3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["K3"].Style.WrapText = true;
            hoja.Cells["K3"].Style.Font.Size = 9;
            hoja.Cells["K3"].Style.Font.Bold = true;
        }

        private List<EDCiclo> ObternerDatosArchivoExcel(string Nit)
        {
            List<EDCiclo> Ciclos = null;
            List<EDCiclo> CiclosCalificados = esm.ObtenerDatosInformeExcel(Nit);
            if (CiclosCalificados[0].Estandares[0].SubEstandares[0].Criterios.Count > 0)
            {
                Ciclos = esm.ObtenerDatosInicialesInformeExcel();
                foreach (var _ciclo in Ciclos)
                {
                    _ciclo.CantidadCriterios = esm.ObtenerCantidadCriteriosPorEstandar(_ciclo.Id_Ciclo);
                    foreach (var _estandar in _ciclo.Estandares)
                    {
                        foreach (var _sub in _estandar.SubEstandares)
                        {
                            _sub.CalTotal = ((CiclosCalificados.Where(c => c.Id_Ciclo == _ciclo.Id_Ciclo).Select(c => c).FirstOrDefault())
                                .Estandares.Where(e => e.Id_Estandar == _estandar.Id_Estandar).Select(e => e).FirstOrDefault())
                                .SubEstandares.Where(sb => sb.Id_SubEstandar == _sub.Id_SubEstandar).Select(sb => sb.CalTotal).FirstOrDefault();
                            foreach (var _criterio in _sub.Criterios)
                            {
                                _criterio.Evaluacion = (((CiclosCalificados.Where(c => c.Id_Ciclo == _ciclo.Id_Ciclo).Select(c => c).FirstOrDefault())
                                .Estandares.Where(e => e.Id_Estandar == _estandar.Id_Estandar).Select(e => e).FirstOrDefault())
                                .SubEstandares.Where(sb => sb.Id_SubEstandar == _sub.Id_SubEstandar).Select(sb => sb).FirstOrDefault())
                                .Criterios.Where(cr => cr.Id_Criterio == _criterio.Id_Criterio).Select(cr => cr.Evaluacion).FirstOrDefault();
                            }
                        }
                    }
                }
            }

            return Ciclos;
        }

        public List<EDActividad> ObtenerActividades(string Nit)
        {
            return esm.ObtenerActividades(Nit);
        }

        public byte[] ObtenerActividadesExcel(string Nit)
        {
            List<EDActividad> Actividades = esm.ObtenerActividades(Nit);
            if (Actividades.Count > 0)
            {
                ExcelPackage Ac = new ExcelPackage();
                
                Ac.Workbook.Worksheets.Add("Plan de acción");
                
                ExcelWorksheet ew1 = Ac.Workbook.Worksheets[1];

                ew1.Cells["A1:C1"].Merge = true;
                ew1.Cells["A1"].Value = "Plan de Acción para los Estándares Mínimos SGSST";
                ew1.Cells["A1"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                ew1.Cells["A1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ew1.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ew1.Cells["A1"].Style.Font.Bold = true;
                ew1.Cells["A1"].Style.WrapText = true;

                ew1.Cells["A2"].Value ="ACTIVIDAD";
                ew1.Cells["A2"].Style.Font.Bold = true;
                ew1.Cells["B2"].Value = "RESPONSABLE";
                ew1.Cells["B2"].Style.Font.Bold = true;
                ew1.Cells["C2"].Value = "FECHA FIN";
                ew1.Cells["C2"].Style.Font.Bold = true;

                int nunInicial = 3;                

                foreach (var actividad in Actividades)
                {
                    ew1.Cells[string.Format("A{0}", nunInicial)].Value = actividad.Descripcion;
                    ew1.Cells[string.Format("A{0}", nunInicial)].Style.WrapText = true;
                    ew1.Cells[string.Format("B{0}", nunInicial)].Value = actividad.Responsable;
                    ew1.Cells[string.Format("B{0}", nunInicial)].Style.WrapText = true;
                    ew1.Cells[string.Format("C{0}", nunInicial)].Value = string.Format ("{0}/{1}/{2}", actividad.FechaFin.Year, actividad.FechaFin.Month, actividad.FechaFin.Day );
                    ew1.Cells[string.Format("C{0}", nunInicial)].Style.WrapText = true;
                    nunInicial++;
                }

                ew1.Row(1).Height = 40;
                ew1.Row(3).Height = 40;
                ew1.Column(1).Width = 50;
                ew1.Column(2).Width = 25;
                ew1.Column(3).Width = 20;
                
                foreach (var cel in ew1.Cells[string.Format("A1:C{0}",Actividades.Count)])
                {
                    cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                }
                //FileInfo fileInfo = new FileInfo(@"D:\Archivos\Actividades.xlsx");
                //Ac.SaveAs(fileInfo);
                return Ac.GetAsByteArray(); 
            }
            else
                return new byte []{};
        }
    }
}
