using SG_SST.EntidadesDominio.Ausentismo;
using SG_SST.Models;
using SG_SST.Interfaces.Ausentismo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SG_SST.Audotoria;

namespace SG_SST.Repositorio.Ausentismo
{
    public class IndicadoresManager : IIndicadores
    {
        [DbFunction("AusentismoModel.Store", "Indicadores_Eventos")]
        public static decimal? Indicadores_Eventos()
        {
            throw new NotSupportedException("Direct calls are not supported.");
        }

        /// <summary>
        /// Obtiene el número de eventos y la cantidad
        /// de días por evento para cada contingencia
        /// </summary>
        /// <param name="anio"></param>
        /// <returns></returns>
        public List<EDIndicadoresGenerados> CantidadEventos(int anio, int idEmpresaUsuaria, string Nit, int IdContingenia)
        {
            try
            {
                using (var context = new SG_SSTContext())
                {
                    return context.Database.SqlQuery<EDIndicadoresGenerados>("SELECT * FROM Indicadores_Eventos(@anio, @idEmpresaUsuaria, @Nit, @idContingencia)", 
                        new SqlParameter { ParameterName = "anio", Value = anio },
                        new SqlParameter { ParameterName = "idEmpresaUsuaria", Value = idEmpresaUsuaria },
                        new SqlParameter { ParameterName = "Nit", Value = Nit },
                        new SqlParameter { ParameterName = "idContingencia", Value = IdContingenia }).ToList();
                }
            }
            catch (Exception ex)
            {
                var manejoErrores = new RegistraLog();
                manejoErrores.RegistrarError(typeof(IndicadoresManager), string.Format("Error en el método CantidadEventos {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                return null;
            }

        }

        /// <summary>
        /// Se obtiene la configuracion HHT para el periodo (anio)
        /// pasado por parametro
        /// </summary>
        /// <param name="anio"></param>
        /// <returns></returns>
        public List<ResultadoConfiguracion> Configuracion(int anio, string Nit)
        {
            try
            {
                List<ResultadoConfiguracion> totalpormes = null;
                using (var context = new SG_SSTContext())
                {
                    totalpormes = (from Configuracion_HHT in context.Tbl_ConfiguracionesHHT
                                   where
                                     Configuracion_HHT.Ano == anio && Configuracion_HHT.Documento_empresa.Trim().Equals(Nit)
                                   select new ResultadoConfiguracion()
                                   {
                                       Mes = Configuracion_HHT.Mes,
                                       TotalMes = Configuracion_HHT.Total_HHT,
                                       NumeroTrabajadores = Configuracion_HHT.Num_Trabajadores_XT
                                   }).ToList();

                }
                return totalpormes;
            }
            catch (Exception ex)
            {
                var manejoErrores = new RegistraLog();
                manejoErrores.RegistrarError(typeof(IndicadoresManager), string.Format("Error en el método Configuracion {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                return null;
            }
        }

        public List<EDAcumuladoTotalContingencias> ObtenerAcumuladoTotalContingencia(int anio, int tipoContingenciaComparar, string Nit, int idEmpresaUsuaria, int IdContingenia)
        {
            try
            {
                List<EDAcumuladoTotalContingencias> totalesAcumulados = null;
                using (var context = new SG_SSTContext())
                {
                    totalesAcumulados = context.Database.SqlQuery<EDAcumuladoTotalContingencias>("SP_AcumuladoTotalContingencias @anio, @tipoContingenciaComparar, @Nit, @idEmpresaUsuaria, @IdContingenia",
                        new SqlParameter("@anio", anio),
                        new SqlParameter("@tipoContingenciaComparar", tipoContingenciaComparar),
                        new SqlParameter("@Nit", Nit),
                        new SqlParameter("@idEmpresaUsuaria", idEmpresaUsuaria),
                        new SqlParameter("@IdContingenia", IdContingenia)).ToList();
                }
                //return context.Database.SqlQuery<EDAcumuladoTotalContingencias>("SELECT * FROM AcumuladoTotalContingencias(@anio, @tipoContingenciaComparar)", new SqlParameter { ParameterName = "anio", Value = anio }, new SqlParameter { ParameterName = "tipoContingenciaComparar", Value = tipoContingenciaComparar }).ToList();
                return totalesAcumulados;
            }
            catch (Exception ex)
            {
                var manejoErrores = new RegistraLog();
                manejoErrores.RegistrarError(typeof(IndicadoresManager), string.Format("Error en el método ObtenerAcumuladoTotalContingencias {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene el acumulado por mes, de cada evento, dias de ausencia,
        /// horas trabajadas y número de trabajadores
        /// </summary>
        /// <param name="anio"></param>
        /// <returns></returns>
        public List<EDAcumuladoTotalContingencias> ObtenerAcumuladoTotalContingencias(int anio, int tipoContingenciaComparar, string Nit, int idEmpresaUsuaria, int IdContingenia)
        {
            try
            {
                List<EDAcumuladoTotalContingencias> totalesAcumulados = null;
                using (var context = new SG_SSTContext())
                {
                    totalesAcumulados = context.Database.SqlQuery<EDAcumuladoTotalContingencias>("SP_AcumuladoTotalContingencias @anio, @tipoContingenciaComparar, @Nit, @idEmpresaUsuaria",
                        new SqlParameter("@anio", anio),
                        new SqlParameter("@tipoContingenciaComparar", tipoContingenciaComparar),
                        new SqlParameter("@Nit", Nit),
                        new SqlParameter("@idEmpresaUsuaria", idEmpresaUsuaria)).ToList();
                }
                //return context.Database.SqlQuery<EDAcumuladoTotalContingencias>("SELECT * FROM AcumuladoTotalContingencias(@anio, @tipoContingenciaComparar)", new SqlParameter { ParameterName = "anio", Value = anio }, new SqlParameter { ParameterName = "tipoContingenciaComparar", Value = tipoContingenciaComparar }).ToList();
                return totalesAcumulados;
            }
            catch (Exception ex)
            {
                var manejoErrores = new RegistraLog();
                manejoErrores.RegistrarError(typeof(IndicadoresManager), string.Format("Error en el método ObtenerAcumuladoTotalContingencias {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                return null;
            }
        }
    }
}
