using SG_SST.EntidadesDominio.Ausentismo;
using SG_SST.Interfaces.Ausentismo;
using SG_SST.Models;
using SG_SST.Models.Ausentismo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Repositorio.Ausentismo
{
    public class ConfiguracionManager : IConfiguracion
    {
        public List<EDAusencia> ObtieneAusenciasDelMismoPerido(DateTime fechaInicial, DateTime FechaFinal, int IdEmpresa)
        {
            List<EDAusencia> AusensiasPerido = new List<EDAusencia>();
            try
            {
                using (var context = new SG_SSTContext())
                {
                    AusensiasPerido = (from au in context.Tbl_Ausencias
                                       where au.NitEmpresa.Equals(IdEmpresa.ToString())
                                       && (au.FechaInicio >= fechaInicial
                                       && au.Fecha_Fin <= FechaFinal)
                                       select new EDAusencia()
                                       {
                                           FechaInicio = au.FechaInicio,
                                           FechaFin = au.Fecha_Fin,
                                           IdContingencia = au.FK_Id_Contingencia,
                                           DiasAusencia = au.DiasAusencia,
                                           Documento = au.Documento_Persona                                           
                                       }).ToList();
                };
            }
            catch (Exception ex)
            {

            }

            return AusensiasPerido;
        }

        public List<EDAusencia> ObtieneAusenciasDesdePeridoAterior(DateTime fechaInicial, DateTime FechaFinal, int IdEmpresa)
        {
            List<EDAusencia> AusensiasPerido = new List<EDAusencia>();
            try
            {
                using (var context = new SG_SSTContext())
                {
                    AusensiasPerido = (from au in context.Tbl_Ausencias
                                       where au.NitEmpresa.Equals(IdEmpresa.ToString())
                                       && au.FechaInicio < fechaInicial
                                       && (au.Fecha_Fin <= FechaFinal && au.Fecha_Fin >= fechaInicial)
                                       select new EDAusencia()
                                       {
                                           FechaInicio = au.FechaInicio,
                                           FechaFin = au.Fecha_Fin,
                                           IdContingencia = au.FK_Id_Contingencia,
                                           DiasAusencia = au.DiasAusencia,
                                           Documento = au.Documento_Persona
                                       }).ToList();
                };
            }
            catch (Exception ex)
            {

            }

            return AusensiasPerido;
        }

        public List<EDAusencia> ObtieneAusenciasPeridoConFinMesSiguiente(DateTime fechaInicial, DateTime FechaFinal, int IdEmpresa)
        {
            List<EDAusencia> AusensiasPerido = new List<EDAusencia>();
            try
            {
                using (var context = new SG_SSTContext())
                {
                    AusensiasPerido = (from au in context.Tbl_Ausencias
                                       where au.NitEmpresa.Equals(IdEmpresa.ToString())
                                       && (au.FechaInicio >= fechaInicial && au.FechaInicio <= FechaFinal)
                                       && au.Fecha_Fin > FechaFinal
                                       select new EDAusencia()
                                       {
                                           FechaInicio = au.FechaInicio,
                                           FechaFin = au.Fecha_Fin,
                                           IdContingencia = au.FK_Id_Contingencia,
                                           DiasAusencia = au.DiasAusencia,
                                           Documento = au.Documento_Persona
                                       }).ToList();
                };
            }
            catch (Exception ex)
            {

            }

            return AusensiasPerido;
        }

        public List<EDAusencia> ObtieneAusenciasPeriodosAtrasYAdelante(DateTime fechaInicial, DateTime FechaFinal, int IdEmpresa)
        {
            List<EDAusencia> AusensiasPerido = new List<EDAusencia>();
            try
            {
                using (var context = new SG_SSTContext())
                {
                    AusensiasPerido = (from au in context.Tbl_Ausencias
                                       where au.NitEmpresa.Equals(IdEmpresa.ToString())
                                       && (au.FechaInicio < fechaInicial
                                       && au.Fecha_Fin > FechaFinal)
                                       select new EDAusencia()
                                       {
                                           FechaInicio = au.FechaInicio,
                                           FechaFin = au.Fecha_Fin,
                                           IdContingencia = au.FK_Id_Contingencia,
                                           DiasAusencia = au.DiasAusencia,
                                           Documento = au.Documento_Persona
                                       }).ToList();
                };
            }
            catch (Exception ex)
            {

            }

            return AusensiasPerido;
        }


        /// <summary>
        /// Guarda en base de datos una nueva configuracion asociada
        /// a una empresa
        /// </summary>
        /// <param name="configuracion"></param>
        /// <returns></returns>
        public bool GuardarConfiguracion(EDConfiguracion configuracion)
        {
            try
            {
                var result = false;
                using (var context = new SG_SSTContext())
                {
                    using (var Transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var nuevaConfiguracion = new ConfiguracionHHT();
                            nuevaConfiguracion.Documento_empresa = configuracion.IdEmpresa;
                            nuevaConfiguracion.Ano = configuracion.Anio;
                            nuevaConfiguracion.Mes = configuracion.Mes;
                            nuevaConfiguracion.Dias_Laborales = configuracion.DiasLaborales;
                            nuevaConfiguracion.Horas_Laborales = configuracion.HorasLaborales;
                            nuevaConfiguracion.Num_Trabajadores_XT = configuracion.NumeroTrabajadoresXT;
                            nuevaConfiguracion.Dias_Trabajados_DTM = configuracion.DiasTrabajadosDTM;
                            nuevaConfiguracion.Horas_Hombre_HTD = configuracion.HorasHombreHTD;
                            nuevaConfiguracion.Horas_Extras_NHE = configuracion.HorasExtrasNHE;
                            nuevaConfiguracion.Horas_Ausentismo_NHA = configuracion.HorasAusentismoNHA;
                            nuevaConfiguracion.Fecha_Modificacion = configuracion.FechaModificacion;
                            nuevaConfiguracion.Total_HHT = configuracion.Total;

                            context.Tbl_ConfiguracionesHHT.Add(nuevaConfiguracion);

                            

                            context.SaveChanges();
                            Transaction.Commit();
                            result = true;
                        }
                        catch (Exception ex)
                        {
                            Transaction.Rollback();
                            result = false;
                        }
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<EDConfiguracion> ObtenerConfiguracionesEmpresa(string NitEmpresa, int ano)
        {
            List<EDConfiguracion> configuraciones = new List<EDConfiguracion>();
            try
            {
                using (var context = new SG_SSTContext())
                {
                    configuraciones = (from c in context.Tbl_ConfiguracionesHHT
                                       where c.Documento_empresa.Equals(NitEmpresa) && c.Ano == ano
                                       select new EDConfiguracion()
                                       {
                                           IdConfiguracion = c.id_Configuracion,
                                           Anio = c.Ano,
                                           Mes = c.Mes,
                                           DiasLaborales = c.Dias_Laborales,
                                           NumeroTrabajadoresXT = c.Num_Trabajadores_XT,
                                           DiasTrabajadosDTM = c.Dias_Trabajados_DTM,
                                           HorasHombreHTD = c.Horas_Hombre_HTD,
                                           HorasExtrasNHE = c.Horas_Extras_NHE,
                                           HorasAusentismoNHA = c.Horas_Ausentismo_NHA,
                                           FechaModificacion = c.Fecha_Modificacion,
                                           Total = c.Total_HHT
                                       }).ToList();
                };
            }
            catch (Exception ex)
            {
                return configuraciones;
            }

            return configuraciones.OrderBy (c => c.Mes).ToList ();
        }

        public bool EliminarConfiguracion(string NitEmpresa, int idconfiguracion)
        {
            bool result = false;
            try
            {
                using (var context = new SG_SSTContext())
                {
                    var configuracion = (from c in context.Tbl_ConfiguracionesHHT
                                         where c.Documento_empresa.Equals(NitEmpresa) && c.id_Configuracion == idconfiguracion
                                         select c).FirstOrDefault();

                    context.Tbl_ConfiguracionesHHT.Remove(configuracion);
                    context.SaveChanges();
                    result = true;
                };
            }
            catch (Exception ex)
            {
                return result;
            }

            return result;
        }
    }
}
