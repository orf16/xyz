using OfficeOpenXml;
using SG_SST.EntidadesDominio.Ausentismo;
using SG_SST.Interfaces.Ausentismo;
using SG_SST.InterfazManager.Ausentismo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Logica.Ausentismo
{
    public class LNAusencia
    {
        private static List<DateTime> fechasFestivasTotales;
        private static IAusencia ausenciaMg = IMAusentismo.Ausencia();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ausencia"></param>
        /// <returns></returns>
        public EDAusencia GuardarAusencia(EDAusencia ausencia)
        {
            bool puedeCrear = ValidarCruceDeAusencias(ausencia);

            if (!puedeCrear)
                ausencia.Result = "CRUCE";
            else
            {
                var resultado = ausenciaMg.GuardarAusencia(ausencia);
                if(resultado)
                    ausencia.Result = "OK";
                else
                    ausencia.Result = "FAIL";
            }

            return ausencia;
        }

        public bool  ValidarCruceDeAusencias(EDAusencia ausencia)
        {
            List<EDAusencia> AusenciasList = ausenciaMg.ValidarCruceAusentismo(ausencia);
            bool puedeCrear = true;
            foreach (var item in AusenciasList)
            {
                if (DateTime.Compare(ausencia.FechaFin, item.FechaInicio) >= 0 && DateTime.Compare(ausencia.FechaFin, item.FechaFin) <= 0)
                {
                    puedeCrear = false;
                    break;
                }
                else if (DateTime.Compare(ausencia.FechaInicio, item.FechaFin) <= 0 && DateTime.Compare(ausencia.FechaInicio, item.FechaInicio) >= 0)
                {
                    puedeCrear = false;
                    break;
                }
                else if (DateTime.Compare(ausencia.FechaInicio, item.FechaInicio) <= 0 && DateTime.Compare(ausencia.FechaFin, item.FechaFin) >= 0)
                {
                    puedeCrear = false;
                    break;
                }
            }
            return puedeCrear;
        }

        public List<string> AutoCompletarBuscarDocumentos(string prefijo)
        {
            return ausenciaMg.BuscarDocumentos(prefijo);
        }

        public int ObtenerDiasLaborablesEmpresa(string nitEmpresa)
        {
            int diasHablides = ausenciaMg.ObtenerDiasLaborablesEmpresa(nitEmpresa);
            if (diasHablides == 1)
                diasHablides = 5;
            else if (diasHablides == 2)
                diasHablides = 6;
            else
                diasHablides = 5;

            return diasHablides;
        }

        public IEnumerable<Resultados> ConsultarDatosAusencia(EDAusencia edAusencia)
        {
            var resultado = ausenciaMg.ConsultarAusencia(edAusencia);
            return resultado;
        }


        public byte [] GenararExcelConsultaAusencias(EDAusencia edAusencia)
        {
            var resultado = ausenciaMg.ConsultarAusencia(edAusencia);

            ExcelPackage excel = new ExcelPackage();

            excel.Workbook.Worksheets.Add("Reporte de Ausentismo");

            ExcelWorksheet hoja = excel.Workbook.Worksheets[1];
            
            hoja.Cells["A1:M1"].Merge = true;
            hoja.Cells["A1"].Value = "REPORTE DE AUSENTISMO ";
            hoja.Cells["A1"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["A1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["A1"].Style.Font.Bold = true;
            hoja.Cells["A1"].Style.WrapText = true;

            hoja.Cells["A2"].Value = "Tipo";
            hoja.Cells["A2"].Style.Font.Bold = true;
            hoja.Cells["B2"].Value = "Fecha Registro";
            hoja.Cells["B2"].Style.Font.Bold = true;
            hoja.Cells["C2"].Value = "Nombre Persona";
            hoja.Cells["C2"].Style.Font.Bold = true;
            hoja.Cells["D2"].Value = "Nro. Documento";
            hoja.Cells["D2"].Style.Font.Bold = true;
            hoja.Cells["E2"].Value = "Departamento";
            hoja.Cells["E2"].Style.Font.Bold = true;
            hoja.Cells["F2"].Value = "Municipio";
            hoja.Cells["F2"].Style.Font.Bold = true;
            hoja.Cells["G2"].Value = "Sede";
            hoja.Cells["G2"].Style.Font.Bold = true;
            hoja.Cells["H2"].Value = "Contingencia";
            hoja.Cells["H2"].Style.Font.Bold = true;
            hoja.Cells["I2"].Value = "Fecha Inicio";
            hoja.Cells["I2"].Style.Font.Bold = true;
            hoja.Cells["J2"].Value = "Fecha Fin";
            hoja.Cells["J2"].Style.Font.Bold = true;
            hoja.Cells["K2"].Value = "Dias Ausencia";
            hoja.Cells["K2"].Style.Font.Bold = true;
            hoja.Cells["L2"].Value = "Diagnóstico";
            hoja.Cells["L2"].Style.Font.Bold = true;
            hoja.Cells["M2"].Value = "Costo";
            hoja.Cells["M2"].Style.Font.Bold = true;
            hoja.Cells["N2"].Value = "Observacion";
            hoja.Cells["N2"].Style.Font.Bold = true;

            int nunInicial = 3;

            foreach (var dato in resultado)
            {
                hoja.Cells[string.Format("A{0}", nunInicial)].Value = dato.TipoRegistro;
                hoja.Cells[string.Format("A{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].Value = string.Format ("{0}/{1}/{2}", dato.FechaModificacion.Value.Day, dato.FechaModificacion.Value.Month, dato.FechaModificacion.Value.Year);
                hoja.Cells[string.Format("B{0}", nunInicial)].Style.WrapText = true;                
                hoja.Cells[string.Format("C{0}", nunInicial)].Value = dato.NombrePersona;
                hoja.Cells[string.Format("C{0}", nunInicial)].Style.WrapText = true;                
                hoja.Cells[string.Format("D{0}", nunInicial)].Value = dato.Documento;
                hoja.Cells[string.Format("D{0}", nunInicial)].Style.WrapText = true;                
                hoja.Cells[string.Format("E{0}", nunInicial)].Value = dato.Departamento;
                hoja.Cells[string.Format("E{0}", nunInicial)].Style.WrapText = true;                
                hoja.Cells[string.Format("F{0}", nunInicial)].Value = dato.Municipio;
                hoja.Cells[string.Format("F{0}", nunInicial)].Style.WrapText = true;                
                hoja.Cells[string.Format("G{0}", nunInicial)].Value = dato.nombreRegional;
                hoja.Cells[string.Format("G{0}", nunInicial)].Style.WrapText = true;                
                hoja.Cells[string.Format("H{0}", nunInicial)].Value = dato.Detalle;
                hoja.Cells[string.Format("H{0}", nunInicial)].Style.WrapText = true;                
                hoja.Cells[string.Format("I{0}", nunInicial)].Value = string.Format("{0}/{1}/{2}", dato.fechainicio.Day, dato.fechainicio.Month, dato.fechainicio.Year); ;
                hoja.Cells[string.Format("I{0}", nunInicial)].Style.WrapText = true;                
                hoja.Cells[string.Format("J{0}", nunInicial)].Value = string.Format("{0}/{1}/{2}", dato.fechafin.Day, dato.fechafin.Month, dato.fechafin.Year); ;
                hoja.Cells[string.Format("J{0}", nunInicial)].Style.WrapText = true;                
                hoja.Cells[string.Format("K{0}", nunInicial)].Value = Utilidades.Utilitarios.ObtenerValorConformato(dato.diasausencia.ToString ());
                hoja.Cells[string.Format("K{0}", nunInicial)].Style.WrapText = true;                
                hoja.Cells[string.Format("L{0}", nunInicial)].Value = dato.Descripcion;
                hoja.Cells[string.Format("L{0}", nunInicial)].Style.WrapText = true;                
                hoja.Cells[string.Format("M{0}", nunInicial)].Value = dato.costo;
                hoja.Cells[string.Format("M{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("M{0}", nunInicial)].OfType<decimal>();
                hoja.Cells[string.Format("N{0}", nunInicial)].Value = dato.Observaciones;
                hoja.Cells[string.Format("N{0}", nunInicial)].Style.WrapText = true;

                nunInicial++;
            }

            for(int i = 1; i < 14; i++)
                hoja.Column(i).Width = 30;

            foreach (var cel in hoja.Cells[string.Format("A1:N{0}", resultado.Count() + 2)])
            {
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            }
                      
            //FileInfo fileInfo = new FileInfo(@"D:\Archivos\Actividades.xlsx");
            //excel.SaveAs(fileInfo);
            
            return excel.GetAsByteArray();

        }

        
        public EDAusencia ProrrogarAusencia(EDAusencia prorrogar)
        {
            List<EDAusencia> AusenciasList = ausenciaMg.ValidarCruceAusentismo(prorrogar);
            bool puedeCrear = true;
            foreach (var item in AusenciasList)
            {
                if (DateTime.Compare(prorrogar.FechaFin, item.FechaInicio) >= 0 && DateTime.Compare(prorrogar.FechaFin, item.FechaFin) <= 0)
                {
                    puedeCrear = false;
                    break;
                }
                else if (DateTime.Compare(prorrogar.FechaInicio, item.FechaFin) <= 0 && DateTime.Compare(prorrogar.FechaInicio, item.FechaInicio) >= 0)
                {
                    puedeCrear = false;
                    break;
                }
                else if (DateTime.Compare(prorrogar.FechaInicio, item.FechaInicio) <= 0 && DateTime.Compare(prorrogar.FechaFin, item.FechaFin) >= 0)
                {
                    puedeCrear = false;
                    break;
                }
            }

            if (!puedeCrear)
                prorrogar.Result = "CRUCE";
            else
            {
                var resultado = ausenciaMg.ProrrogarAusencia(prorrogar);
                if (resultado)
                    prorrogar.Result = "OK";
                else
                    prorrogar.Result = "FAIL";
            }

            return prorrogar;          
            
        }

        public int CalcularDiasLaborales(DateTime fechaInicio, DateTime fechaFin, int diasHabilesEmpresa, int tipoContingencia)
        {
            var cantidadDias = 0;
            if (tipoContingencia == (int)SG_SST.Enumeraciones.EnumAusentismo.Contingencias.LicenciaLuto || tipoContingencia == (int)SG_SST.Enumeraciones.EnumAusentismo.Contingencias.LicenciaPaternidad)
            {
                var anioFechaI = fechaInicio.Year;
                var anioFechaF = fechaFin.Year;
                if (anioFechaI == anioFechaF)
                    fechasFestivasTotales = DiasFestivos(anioFechaI);
                else
                {
                    var fechasFestivasAnioI = DiasFestivos(anioFechaI);
                    var fechasFestivasAnioF = DiasFestivos(anioFechaF);
                    fechasFestivasTotales = fechasFestivasAnioI.Select(fi => fi).ToList().Union(fechasFestivasAnioF.Select(ff => ff).ToList()).ToList();
                }
                var cantidadDiasFestivos = fechasFestivasTotales.Where(f => f >= fechaInicio && f <= fechaFin).Count();
                cantidadDias = CalcularDiasHabiles(fechaInicio, fechaFin, diasHabilesEmpresa) - cantidadDiasFestivos;
            }
            else
                cantidadDias = ((int)(fechaFin - fechaInicio).TotalDays) + 1;
            return cantidadDias;
        }

        private static int CalcularDiasHabiles(DateTime fechaInicio, DateTime fechaFin, int diasHabilesEmpresa)
        {
            var dias_habiles = 0;
            while (fechaInicio <= fechaFin)
            {
                int numero_dia = Convert.ToInt16(fechaInicio.DayOfWeek.ToString("d"));
                if (diasHabilesEmpresa == 5)
                {
                    if (numero_dia == 1 || numero_dia == 2 || numero_dia == 3 || numero_dia == 4 || numero_dia == 5)
                        dias_habiles++;
                }
                else
                {
                    if (numero_dia == 1 || numero_dia == 2 || numero_dia == 3 || numero_dia == 4 || numero_dia == 5 || numero_dia == 6)
                        dias_habiles++;
                }
                fechaInicio = fechaInicio.AddDays(1);
            }
            return dias_habiles;
        }

        private static List<DateTime> DiasFestivos(int Anio)
        {
            DateTime Pascua = calcularPascua(Anio);
            List<DateTime> diasFestivos = new List<DateTime>();
            IncluirFecha(ref diasFestivos, new DateTime(Anio, 1, 1)); //Primero de Enero
            IncluirFecha(ref diasFestivos, SiguienteDiaSemana(DayOfWeek.Monday, new DateTime(Anio, 1, 6))); //Reyes magos
            IncluirFecha(ref diasFestivos, SiguienteDiaSemana(DayOfWeek.Monday, new DateTime(Anio, 3, 19))); //San Jose
            //IncluirFecha(ref diasFestivos, SiguienteDiaSemana(DayOfWeek.Sunday, Pascua, true, false)); //Domingo de Ramos
            IncluirFecha(ref diasFestivos, SiguienteDiaSemana(DayOfWeek.Thursday, Pascua, true)); //Jueves Santo
            IncluirFecha(ref diasFestivos, SiguienteDiaSemana(DayOfWeek.Friday, Pascua, true)); //Viernes Santo
            if (!Pascua.DayOfWeek.Equals(DayOfWeek.Sunday))
                IncluirFecha(ref diasFestivos, Pascua); //Pascua
            IncluirFecha(ref diasFestivos, new DateTime(Anio, 5, 1)); //Dia del trabajo


            IncluirFecha(ref diasFestivos, SiguienteDiaSemana(DayOfWeek.Monday, Pascua).AddDays(42)); //Ascensión de Jesús
            IncluirFecha(ref diasFestivos, SiguienteDiaSemana(DayOfWeek.Monday, Pascua).AddDays(63)); //Corpus Christi
            IncluirFecha(ref diasFestivos, SiguienteDiaSemana(DayOfWeek.Monday, Pascua).AddDays(70)); //Sagrado Corazón


            IncluirFecha(ref diasFestivos, SiguienteDiaSemana(DayOfWeek.Monday, new DateTime(Anio, 6, 29))); //san Pedro y san Pablo
            IncluirFecha(ref diasFestivos, new DateTime(Anio, 7, 20)); //Grito de Independencia
            IncluirFecha(ref diasFestivos, new DateTime(Anio, 8, 7)); // Batalla de Boyacá
            IncluirFecha(ref diasFestivos, SiguienteDiaSemana(DayOfWeek.Monday, new DateTime(Anio, 8, 15))); //Asuncion de la virgen
            IncluirFecha(ref diasFestivos, SiguienteDiaSemana(DayOfWeek.Monday, new DateTime(Anio, 10, 12))); //Día de la Raza
            IncluirFecha(ref diasFestivos, SiguienteDiaSemana(DayOfWeek.Monday, new DateTime(Anio, 10, 12))); //Todos los Santos
            IncluirFecha(ref diasFestivos, SiguienteDiaSemana(DayOfWeek.Monday, new DateTime(Anio, 11, 1))); //Independencia de Cartagena
            IncluirFecha(ref diasFestivos, SiguienteDiaSemana(DayOfWeek.Monday, new DateTime(Anio, 11, 11))); //Independencia de Cartagena
            IncluirFecha(ref diasFestivos, new DateTime(Anio, 12, 8)); // Inmaculada Concepción
            IncluirFecha(ref diasFestivos, new DateTime(Anio, 12, 25)); // Navidad

            return diasFestivos;
        }

        private static void IncluirFecha(ref List<DateTime> ListaDias, DateTime fecha)
        {
            if (ListaDias.Contains(fecha) == false)
                ListaDias.Add(fecha);
        }

        private static DateTime SiguienteDiaSemana(DayOfWeek DiaSemana, DateTime fecha, bool haciaAtras = false, bool inclusive = true)
        {
            if (inclusive)
            {
                if (fecha.DayOfWeek == DiaSemana)
                {
                    return fecha;
                }
            }
            else
            {
                if (haciaAtras)
                    fecha = fecha.AddDays(-1);
                else
                    fecha = fecha.AddDays(1);
            }

            while (fecha.DayOfWeek != DiaSemana)
                if (haciaAtras)
                    fecha = fecha.AddDays(-1);
                else
                    fecha = fecha.AddDays(1);

            return fecha;
        }

        private static DateTime calcularPascua(int Anio)
        {

            int a, b, c, d, e;
            int m = 24, n = 5;


            if (Anio >= 1583 && Anio <= 1699)
            {
                m = 22;
                n = 2;
            }
            else if (Anio >= 1700 && Anio <= 1799)
            {
                m = 23;
                n = 3;
            }
            else if (Anio >= 1800 && Anio <= 1899)
            {
                m = 23;
                n = 4;
            }
            else if (Anio >= 1900 && Anio <= 2099)
            {
                m = 24;
                n = 5;
            }
            else if (Anio >= 2100 && Anio <= 2199)
            {
                m = 24;
                n = 6;
            }
            else if (Anio >= 2200 && Anio <= 2299)
            {
                m = 25;
                n = 0;
            }

            a = Anio % 19;
            b = Anio % 4;
            c = Anio % 7;
            d = ((a * 19) + m) % 30;
            e = ((2 * b) + (4 * c) + (6 * d) + n) % 7;


            int dia = d + e;


            if (dia < 10) //Marzo
                return new DateTime(Anio, 3, dia + 22);
            else //Abril
            {

                if (dia == 26)
                    dia = 19;
                else if (dia == 25 && d == 28 && e == 6 && a > 10)
                    dia = 18;
                else
                    dia -= 9;

                return new DateTime(Anio, 4, dia);
            }
        }

        public List<string> ObtenerSedes()
        {
            return ausenciaMg.ObtenerSedes();
        }

        /// <summary>
        /// Consulta de alertas por ausentismo.
        /// </summary>
        /// <param name="parametros"></param>
        public List<EDAlertaAusentismo> ConsultarAlertaAusencia(EDAlertaAusentismoParametros parametros)
        {
            var Consulta = ausenciaMg.ConsultarAlertaAusencia(parametros);

            // Calcular los días laborables para los resultados.
            foreach (var item in Consulta)
            {
                item.DiasAusencia = CalcularDiasLaborales(
                    item.FechaInicio, item.FechaFin,
                    5, item.IdContingencia);
            }

            foreach (var item in Consulta)
            {
                item.DiagnosticoDescripcion = ausenciaMg.ConsultarDiagnostico(item.Id_Diagnostico);
            }

            // Calcular la suma de los días de ausencia
            // para el año de gestión.
            var Resultado = (from x in Consulta
                             group x by x.DocumentoPersona into g
                             select new EDAlertaAusentismo
                             {
                                 DocumentoPersona = g.Key,
                                 DiasAusencia = g.Sum(y => y.DiasAusencia),
                                 EmpleadoNombre=g.FirstOrDefault().EmpleadoNombre,
                                 DiagnosticoDescripcion = g.FirstOrDefault().DiagnosticoDescripcion,
                                 ListaAlertas =g.ToList()
                             })
                            .Where(x => x.DiasAusencia >= 60)
                            .OrderBy(x => x.DiasAusencia)
                            .ToList();

            return Resultado;
        }

        public bool GuardarDiasLaborables(string NitEmpresa, int IdDiasSeleccionado)
        {
            return ausenciaMg.GuardarDiasLaborables(NitEmpresa, IdDiasSeleccionado);
        }

        public List<EDDiasLaborables> ConsultarDiasLaborables(string idEmpresa)
        {
            return ausenciaMg.ConsultarDiasLaborables(idEmpresa);
        }

        public EDAusencia ConsultarAusenciaEliminar(string NitEmpresa, int idAusencia)
        {
            return ausenciaMg.ConsultarAusenciaEliminar(NitEmpresa, idAusencia);
        }
        public bool EliminarAusencia(string NitEmpresa, int idAusencia)
        {
            return ausenciaMg.EliminarAusencia(NitEmpresa, idAusencia);
        }
        public List<EDAusencia> ConsultarAusenciaProrrogas(string NitEmpresa, int idAusencia)
        {
            return ausenciaMg.ConsultarAusenciaProrrogas(NitEmpresa, idAusencia);
        }
        public string ConsultarDiagnostico(int idDiagnostico)
        {
            return ausenciaMg.ConsultarDiagnostico(idDiagnostico);
        }
    }
}
