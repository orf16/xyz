using SG_SST.EntidadesDominio.Ausentismo;
using SG_SST.Interfaces.Ausentismo;
using SG_SST.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Repositorio.Ausentismo
{
    public class ReportesManager : IReportes
    {
        # region DbFuntions
        [DbFunction("AusentismoModel.Store", "Ausencias_Contingencia")]
        public static decimal? Ausencias_Contingencia(int anio)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }
        [DbFunction("AusentismoModel.Store", "Ausencias_Departamento")]
        public static decimal? Ausencias_Departamento(int anio)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }
        [DbFunction("AusentismoModel.Store", "Ausencias_Enfermedades")]
        public static decimal? Ausencias_Enfermedades(int anio)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }
        [DbFunction("AusentismoModel.Store", "Ausencias_Eps")]
        public static decimal? Ausencias_Eps(int anio)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }
        [DbFunction("AusentismoModel.Store", "Ausencias_Ocupacion")]
        public static decimal? Ausencias_Ocupacion(int anio)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }
        [DbFunction("AusentismoModel.Store", "Ausencias_Sede")]
        public static decimal? Ausencias_Sede(int anio)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }
        [DbFunction("AusentismoModel.Store", "Ausencias_Sexo")]
        public static decimal? Ausencias_Sexo(int anio)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }
        [DbFunction("AusentismoModel.Store", "Costo_Contingencia")]
        public static decimal? Costo_Contingencia(int anio)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }
        [DbFunction("AusentismoModel.Store", "Numero_Eventos")]
        public static decimal? Numero_Eventos(int anio)
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }

        #endregion
        
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="anio"></param>
        /// <returns></returns>
        public List<EDDatosReportes> ReporteContingencia(EDReportes edReporte)
        {           
            List<EDDatosReportes> edDatosReporte = new List<EDDatosReportes>();
            try
            {
                using (var context = new SG_SSTContext())
                {
                    edDatosReporte = context.Database.SqlQuery<EDDatosReportes>("SP_AUSENCIAS_CONTINGENCIA @anio, @idOrigen, @idEmpresaUsuaria, @idSede, @idDepartamento, @nitEmpresa",
                        new SqlParameter("@anio", edReporte.anio),
                        new SqlParameter("@idOrigen", edReporte.idOrigen),
                        new SqlParameter("@idEmpresaUsuaria", edReporte.IdEmpresaUsuaria),
                        new SqlParameter("@idSede", edReporte.idSede),
                        new SqlParameter("@idDepartamento", edReporte.IdDepartamento),
                        new SqlParameter("@nitEmpresa", edReporte.nitEmpresa)).ToList();                     
                }
                
                return edDatosReporte;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="edReporte"></param>
        /// <returns></returns>
        public List<EDReportesGenerados> ReporteEvento(EDReportes edReporte)
        {
            List<EDReportesGenerados> DatosReporte = new List<EDReportesGenerados>();
            try
            {
                using (var context = new SG_SSTContext())
                {
                    DatosReporte = context.Database.SqlQuery<EDReportesGenerados>("SP_AUSENCIAS_NUMERO_EVENTOS @anio, @idOrigen, @idEmpresaUsuaria, @idSede, @idDepartamento, @nitEmpresa",
                      new SqlParameter("@anio", edReporte.anio),
                      new SqlParameter("@idOrigen", edReporte.idOrigen),
                      new SqlParameter("@idEmpresaUsuaria", edReporte.IdEmpresaUsuaria),
                      new SqlParameter("@idSede", edReporte.idSede),
                      new SqlParameter("@idDepartamento", edReporte.IdDepartamento),
                      new SqlParameter("@nitEmpresa", edReporte.nitEmpresa)).ToList();                    
                }
                return DatosReporte;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="edReporte"></param>
        /// <returns></returns>
        public List<EDDatosReportes> ReporteDepartamento(EDReportes edReporte)
        {
            List<EDDatosReportes> DatosReporte = new List<EDDatosReportes>();
            try
            {
                using (var context = new SG_SSTContext())
                {
                    DatosReporte = context.Database.SqlQuery<EDDatosReportes>("SP_AUSENCIAS_DEPARTAMENTO @anio, @idOrigen, @idEmpresaUsuaria, @idSede, @idDepartamento, @nitEmpresa",
                       new SqlParameter("@anio", edReporte.anio),
                       new SqlParameter("@idOrigen", edReporte.idOrigen),
                       new SqlParameter("@idEmpresaUsuaria", edReporte.IdEmpresaUsuaria),
                       new SqlParameter("@idSede", edReporte.idSede),
                       new SqlParameter("@idDepartamento", edReporte.IdDepartamento),
                       new SqlParameter("@nitEmpresa", edReporte.nitEmpresa)).ToList();
                }
                return DatosReporte;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="edReporte"></param>
        /// <returns></returns>
        public List<EDDatosReportes> ReporteDiasAusentismoEnfermedades(EDReportes edReporte)
        {
            List<EDDatosReportes> DatosReporte = new List<EDDatosReportes>();
            try
            {
                using (var context = new SG_SSTContext())
                {
                    DatosReporte = context.Database.SqlQuery<EDDatosReportes>("SP_AUSENCIAS_ENFERMEDADES @anio, @idOrigen, @idEmpresaUsuaria, @idSede, @idDepartamento, @nitEmpresa",
                      new SqlParameter("@anio", edReporte.anio),
                      new SqlParameter("@idOrigen", edReporte.idOrigen),
                      new SqlParameter("@idEmpresaUsuaria", edReporte.IdEmpresaUsuaria),
                      new SqlParameter("@idSede", edReporte.idSede),
                      new SqlParameter("@idDepartamento", edReporte.IdDepartamento),
                      new SqlParameter("@nitEmpresa", edReporte.nitEmpresa)).ToList(); 
                    
                }
                return DatosReporte;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="edReporte"></param>
        /// <returns></returns>
        public List<EDReportesGenerados> ReporteCantiEventPorEnfermedades(EDReportes edReporte)
        {
            List<EDReportesGenerados> DatosReporte = new List<EDReportesGenerados>();
            try
            {
                using (var context = new SG_SSTContext())
                {
                    DatosReporte = context.Database.SqlQuery<EDReportesGenerados>("SP_AUSENCIAS_CANTIDAD_ENFERMEDADES @anio, @idOrigen, @idEmpresaUsuaria, @idSede, @idDepartamento, @nitEmpresa",
                      new SqlParameter("@anio", edReporte.anio),
                      new SqlParameter("@idOrigen", edReporte.idOrigen),
                      new SqlParameter("@idEmpresaUsuaria", edReporte.IdEmpresaUsuaria),
                      new SqlParameter("@idSede", edReporte.idSede),
                      new SqlParameter("@idDepartamento", edReporte.IdDepartamento),
                      new SqlParameter("@nitEmpresa", edReporte.nitEmpresa)).ToList();
                }
                return DatosReporte;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="edReporte"></param>
        /// <returns></returns>
        public List<EDDatosReportes> ReporteDiasAusentismoPorProceso(EDReportes edReporte)
        {
            List<EDDatosReportes> DatosReporte = new List<EDDatosReportes>();
            try
            {
                using (var context = new SG_SSTContext())
                {
                    DatosReporte = context.Database.SqlQuery<EDDatosReportes>("SP_AUSENCIAS_PROCESO @anio, @idOrigen, @idEmpresaUsuaria, @idSede, @idDepartamento, @nitEmpresa",
                      new SqlParameter("@anio", edReporte.anio),
                      new SqlParameter("@idOrigen", edReporte.idOrigen),
                      new SqlParameter("@idEmpresaUsuaria", edReporte.IdEmpresaUsuaria),
                      new SqlParameter("@idSede", edReporte.idSede),
                      new SqlParameter("@idDepartamento", edReporte.IdDepartamento),
                      new SqlParameter("@nitEmpresa", edReporte.nitEmpresa)).ToList();
                }
                return DatosReporte;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="edReporte"></param>
        /// <returns></returns>
        public List<EDDatosReportes> ReporteSede(EDReportes edReporte)
        {
            List<EDDatosReportes> DatosReporte = new List<EDDatosReportes>();
            try
            {
                using (var context = new SG_SSTContext())
                {
                    DatosReporte = context.Database.SqlQuery<EDDatosReportes>("SP_AUSENCIAS_SEDE @anio, @idOrigen, @idEmpresaUsuaria, @idSede, @idDepartamento, @nitEmpresa",
                      new SqlParameter("@anio", edReporte.anio),
                      new SqlParameter("@idOrigen", edReporte.idOrigen),
                      new SqlParameter("@idEmpresaUsuaria", edReporte.IdEmpresaUsuaria),
                      new SqlParameter("@idSede", edReporte.idSede),
                      new SqlParameter("@idDepartamento", edReporte.IdDepartamento),
                      new SqlParameter("@nitEmpresa", edReporte.nitEmpresa)).ToList();
                }
                return DatosReporte;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="edReporte"></param>
        /// <returns></returns>
        public List<EDReportesGenerados> ReporteCostoContingencia(EDReportes edReporte)
        {
            List<EDReportesGenerados> DatosReporte = new List<EDReportesGenerados>();
            try
            {
                using (var context = new SG_SSTContext())
                {
                    DatosReporte = context.Database.SqlQuery<EDReportesGenerados>("SP_AUSENCIAS_COSTO @anio, @idOrigen, @idEmpresaUsuaria, @idSede, @idDepartamento, @nitEmpresa",
                      new SqlParameter("@anio", edReporte.anio),
                      new SqlParameter("@idOrigen", edReporte.idOrigen),
                      new SqlParameter("@idEmpresaUsuaria", edReporte.IdEmpresaUsuaria),
                      new SqlParameter("@idSede", edReporte.idSede),
                      new SqlParameter("@idDepartamento", edReporte.IdDepartamento),
                      new SqlParameter("@nitEmpresa", edReporte.nitEmpresa)).ToList();
                }
                return DatosReporte;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="edReporte"></param>
        /// <returns></returns>
        public List<EDReportesGenerados> ReporteDiasEps(EDReportes edReporte)
        {
            List<EDReportesGenerados> DatosReporte = new List<EDReportesGenerados>();
            try
            {
                using (var context = new SG_SSTContext())
                {
                    DatosReporte = context.Database.SqlQuery<EDReportesGenerados>("SP_AUSENCIAS_EPS @anio, @idOrigen, @idEmpresaUsuaria, @idSede, @idDepartamento, @nitEmpresa",
                      new SqlParameter("@anio", edReporte.anio),
                      new SqlParameter("@idOrigen", edReporte.idOrigen),
                      new SqlParameter("@idEmpresaUsuaria", edReporte.IdEmpresaUsuaria),
                      new SqlParameter("@idSede", edReporte.idSede),
                      new SqlParameter("@idDepartamento", edReporte.IdDepartamento),
                      new SqlParameter("@nitEmpresa", edReporte.nitEmpresa)).ToList();                    
                }
                return DatosReporte;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="edReporte"></param>
        /// <returns></returns>
        public List<EDReportesGenerados> ReporteSexo(EDReportes edReporte)
        {
            List<EDReportesGenerados> DatosReporte = new List<EDReportesGenerados>();
            try
            {
                using (var context = new SG_SSTContext())
                {
                    DatosReporte = context.Database.SqlQuery<EDReportesGenerados>("SP_AUSENCIAS_SEXO @anio, @idOrigen, @idEmpresaUsuaria, @idSede, @idDepartamento, @nitEmpresa",
                      new SqlParameter("@anio", edReporte.anio),
                      new SqlParameter("@idOrigen", edReporte.idOrigen),
                      new SqlParameter("@idEmpresaUsuaria", edReporte.IdEmpresaUsuaria),
                      new SqlParameter("@idSede", edReporte.idSede),
                      new SqlParameter("@idDepartamento", edReporte.IdDepartamento),
                      new SqlParameter("@nitEmpresa", edReporte.nitEmpresa)).ToList();
                }
                return DatosReporte;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Obtiene la información de las ausencias por tipo de vinculación.
        /// </summary>
        /// <param name="edReporte"></param>
        /// <returns></returns>
        public List<EDReportesGenerados> ReporteVincualcion(EDReportes edReporte)
        {
            List<EDReportesGenerados> DatosReporte = new List<EDReportesGenerados>();
            try
            {
                using (var context = new SG_SSTContext())
                {
                    DatosReporte = context.Database.SqlQuery<EDReportesGenerados>("SP_AUSENCIAS_VINCULACION @anio, @idOrigen, @idEmpresaUsuaria, @idSede, @idDepartamento, @nitEmpresa",
                      new SqlParameter("@anio", edReporte.anio),
                      new SqlParameter("@idOrigen", edReporte.idOrigen),
                      new SqlParameter("@idEmpresaUsuaria", edReporte.IdEmpresaUsuaria),
                      new SqlParameter("@idSede", edReporte.idSede),
                      new SqlParameter("@idDepartamento", edReporte.IdDepartamento),
                      new SqlParameter("@nitEmpresa", edReporte.nitEmpresa)).ToList();                      
                }
                return DatosReporte;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
               
        /// <summary>
        /// 
        /// </summary>
        /// <param name="edReporte"></param>
        /// <returns></returns>
        public List<EDReportesGenerados> ReporteCantidadAusenciasPorOcupacion(EDReportes edReporte)
        {
            List<EDReportesGenerados> DatosReporte = new List<EDReportesGenerados>();
            try
            {
                using (var context = new SG_SSTContext())
                {
                    DatosReporte = context.Database.SqlQuery<EDReportesGenerados>("SP_AUSENCIAS_OCUPACION @anio, @idOrigen, @idEmpresaUsuaria, @idSede, @idDepartamento, @nitEmpresa",
                      new SqlParameter("@anio", edReporte.anio),
                      new SqlParameter("@idOrigen", edReporte.idOrigen),
                      new SqlParameter("@idEmpresaUsuaria", edReporte.IdEmpresaUsuaria),
                      new SqlParameter("@idSede", edReporte.idSede),
                      new SqlParameter("@idDepartamento", edReporte.IdDepartamento),
                      new SqlParameter("@nitEmpresa", edReporte.nitEmpresa)).ToList();
                }
                return DatosReporte;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="edReporte"></param>
        /// <returns></returns>
        public List<EDReportesGenerados> ReporteCantidadAusenGrupEtarios(EDReportes edReporte)
        {
            List<EDReportesGenerados> DatosReporte = new List<EDReportesGenerados>();
            try
            {
                using (var context = new SG_SSTContext())
                {
                    DatosReporte = context.Database.SqlQuery<EDReportesGenerados>("SP_AUSENCIAS_GRUPOS_ETARIOS @anio, @idOrigen, @idEmpresaUsuaria, @idSede, @idDepartamento, @nitEmpresa",
                      new SqlParameter("@anio", edReporte.anio),
                      new SqlParameter("@idOrigen", edReporte.idOrigen),
                      new SqlParameter("@idEmpresaUsuaria", edReporte.IdEmpresaUsuaria),
                      new SqlParameter("@idSede", edReporte.idSede),
                      new SqlParameter("@idDepartamento", edReporte.IdDepartamento),
                      new SqlParameter("@nitEmpresa", edReporte.nitEmpresa)).ToList();
                }
                return DatosReporte;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int Obteneranoinicioempresa(string Nit)
        {
            int ano = 2011;
            try
            {
                using (SG_SSTContext context = new SG_SSTContext())
                {
                    var fecha = context.Tbl_Ausencias.Where(au => au.NitEmpresa.Trim().Equals(Nit.Trim())).Select(au => au.FechaInicio).Min();
                    if (fecha != null)
                        ano = fecha.Year;
                }
            }
            catch
            {
                return ano;
            }
            return ano;            
        }

    }
}
