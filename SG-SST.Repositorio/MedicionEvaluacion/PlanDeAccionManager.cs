using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.Interfaces.MedicionEvaluacion;
using SG_SST.EntidadesDominio.MedicionEvaluacion;
using SG_SST.Models;
using SG_SST.Models.MedicionEvaluacion;
using SG_SST.Models.Planificacion;
using SG_SST.Models.Aplicacion;
using SG_SST.Models.Participacion;
using System.Data.Entity;
using SG_SST.Audotoria;
using SG_SST.Models.Revision;

namespace SG_SST.Repositorio.MedicionEvaluacion
{
    public class PlanDeAccionManager : IPlanDeAccion
    {

        bool numeroActividadInsp = false;
        /// <summary>
        /// Consulta el estado del plan de acción 1 - Abierto  0- Cerrado 2-CerradoNoCumple
        /// </summary>
        /// <param name="Actividades"></param>
        /// <returns>int</returns>
        public int consultarEstado(List<EDActividadPlanDeAccion> Actividades)
        {
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {

                    int i = 1;
                    int estado = 0;
                    //1 - Abierto  0- Cerrado 2-CerradoNoCumple
                    foreach (EDActividadPlanDeAccion act in Actividades)
                    {

                        if (numeroActividadInsp == false)
                        {

                            act.Num_Actividad = i++;
                        }

                        ActividadPlanDeAccion actividadPlanDeAccion = (from actividad in db1.Tbl_Actividad_Plan_Accion
                                                                       where act.Fk_Id_ModuloPlanAccion == actividad.Fk_Id_ModuloPlanAccion
                                                                           & act.Fk_Plan_Inspección == actividad.Fk_Plan_Inspección
                                                                           & act.Fk_Id_Actividad == actividad.Fk_Id_Actividad
                                                                       select actividad).FirstOrDefault();
                        if (actividadPlanDeAccion != null)
                        {
                            act.Pk_Id_ActividadPlanAccion = actividadPlanDeAccion.Pk_Id_ActividadPlanAccion;
                            act.FechaCierre = actividadPlanDeAccion.FechaCierre;
                            act.Observaciones = actividadPlanDeAccion.Observaciones;
                            if (act.FechaCierre.Date > act.FechaFinalizacion.Date && (estado != 1))
                            {
                                estado = 2;
                            }
                        }
                        else
                        {
                            estado = 1;
                        }
                    }
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Consultando el estado del plan de acción : {0}, {1}", DateTime.Now, Actividades.FirstOrDefault().Actividad), new Exception());
                    return estado;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error consultando el estado del plan de acción: {0}, {1}. Error: {2}", DateTime.Now, Actividades.FirstOrDefault().Actividad, ex.StackTrace), ex);
                return -1;
            }

        }
        /// <summary>
        /// Consulta todos los planes de acción existentes para la empresa
        /// </summary>
        /// <param name="nit"></param>
        /// <returns>List<EDPlanDeAccion></returns>
        public List<EDPlanDeAccion> ObtenerListaPlanDeAccion(int nit)
        {
            try
            {
                numeroActividadInsp = false;
                List<EDPlanDeAccion> ListaPlanDeAccion = new List<EDPlanDeAccion>();
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    var ListaMod = (from s in db1.Tbl_Modulos_Plan_Accion
                                    select s).ToList<ModulosPlanAccion>();
                    int empresa = (from s in db1.Tbl_Empresa
                                   where s.Nit_Empresa == nit.ToString()
                                   select s.Pk_Id_Empresa).FirstOrDefault();
                    if (ListaMod != null)
                    {
                        foreach (var item in ListaMod)
                        {
                            List<EDPlanDeAccion> ListaPlan = new List<EDPlanDeAccion>();
                            EDPlanDeAccion PlanDeAccion = new EDPlanDeAccion();
                            PlanDeAccion.EDActividadPlanDeAccion = new List<EDActividadPlanDeAccion>();
                            if (item.Pk_Id_ModuloPlanAccion == 1)
                            {
                                PlanDeAccion = ObtenerListaActividadEvaluacion(empresa, item.Pk_Id_ModuloPlanAccion, nit);
                                if (PlanDeAccion != null)
                                {
                                    PlanDeAccion.Origen = item.Modulo;
                                    ListaPlanDeAccion.Add(PlanDeAccion);
                                }
                            }
                            else if (item.Pk_Id_ModuloPlanAccion == 2)
                            {
                                ListaPlan = ObtenerListaActividadAccion(empresa, item.Pk_Id_ModuloPlanAccion, item.Modulo);
                                if (ListaPlan != null)
                                    ListaPlanDeAccion.AddRange(ListaPlan);
                            }
                            else if (item.Pk_Id_ModuloPlanAccion == 3)
                            {
                                ListaPlan = ObtenerListaActividadAuditoria(empresa, item.Pk_Id_ModuloPlanAccion, item.Modulo);
                                if (ListaPlan != null)
                                    ListaPlanDeAccion.AddRange(ListaPlan);
                            }
                            else if (item.Pk_Id_ModuloPlanAccion == 4)
                            {
                                ListaPlan = ObtenerListaInspecciones(empresa, item.Pk_Id_ModuloPlanAccion, item.Modulo);
                                if (ListaPlan != null)
                                    ListaPlanDeAccion.AddRange(ListaPlan);
                            }
                            else if (item.Pk_Id_ModuloPlanAccion == 5)
                            {
                                ListaPlan = ObtenerListaReportes(empresa, item.Pk_Id_ModuloPlanAccion, item.Modulo);
                                if (ListaPlan != null)
                                    ListaPlanDeAccion.AddRange(ListaPlan);
                            }
                            else if (item.Pk_Id_ModuloPlanAccion == 6)
                            {
                                ListaPlan = ObtenerListaCopasst(empresa, item.Pk_Id_ModuloPlanAccion, item.Modulo);
                                if (ListaPlan != null)
                                    ListaPlanDeAccion.AddRange(ListaPlan);
                            }
                            else if (item.Pk_Id_ModuloPlanAccion == 7)
                            {
                                ListaPlan = ObtenerListaConvivenciaLaboral(empresa, item.Pk_Id_ModuloPlanAccion, item.Modulo);
                                if (ListaPlan != null)
                                    ListaPlanDeAccion.AddRange(ListaPlan);
                            }
                            else if (item.Pk_Id_ModuloPlanAccion == 8)
                            {
                                ListaPlan = ObtenerListaRevisionSGSST(empresa, item.Pk_Id_ModuloPlanAccion, item.Modulo);
                                if (ListaPlan != null)
                                    ListaPlanDeAccion.AddRange(ListaPlan);
                            }




                        }

                    }
                }
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se obtuvo los planes de acción : {0}, {1}", DateTime.Now, nit), new Exception());
                return ListaPlanDeAccion;
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error obteniendo los planes de acción: {0}, {1}. Error: {2}", DateTime.Now, nit, ex.StackTrace), ex);
                return null;
            }
        }
        /// <summary>
        /// Consulta todos los planes de acción existentes para la empresa en el módulo evaluación
        /// </summary>
        /// <param name="empresa",name="Pk_Id_ModuloPlanAccion",name="nit"></param>
        /// <returns>List<EDPlanDeAccion></returns>
        public EDPlanDeAccion ObtenerListaActividadEvaluacion(int empresa, int Pk_Id_ModuloPlanAccion, int nit)
        {
            try
            {
                numeroActividadInsp = false;
                EDPlanDeAccion PlanDeAccion = new EDPlanDeAccion();
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    Empresa_Evaluar empresaEvaluar = (from e in db1.Tbl_Empresas_Evaluar
                                                      where e.Fk_Id_Empresa == empresa
                                                      select e).FirstOrDefault();

                    int idEmpresaEvaluar=empresaEvaluar.Pk_Id_Empresa_Evaluar;
                    List<EDActividadPlanDeAccion> Actividades = new List<EDActividadPlanDeAccion>();
                    Actividades = (from eval in db1.Tbl_Evaluacion_Estandares_Minimos
                                   join ac in db1.Tbl_Actividades_Criterio on eval.Pk_Id_Eval_Estandar_Minimo equals ac.Fk_Id_Eval_Estandar_Minimo
                                   join ae in db1.Tbl_Actividades_Evaluacion on ac.Fk_Id_Actividad equals ae.Pk_Id_Actividad
                                   where eval.Fk_Id_Empresa_Evaluar == idEmpresaEvaluar
                                   select new EDActividadPlanDeAccion
                                   {
                                       Fk_Id_ModuloPlanAccion = Pk_Id_ModuloPlanAccion,
                                       Fk_Plan_Inspección = eval.Pk_Id_Eval_Estandar_Minimo,
                                       Fk_Id_Actividad = ae.Pk_Id_Actividad,

                                       //actividadReporte = eval.Pk_Id_Eval_Estandar_Minimo.ToString(),
                                       Actividad = ae.Descripcion,
                                       Responsable = ae.Responsable,
                                       FechaFinalizacion = ae.FechaFin,
                                      // fechaEvaluacion = empresaEvaluar.Fecha_Diligencia_Eval_EstMin.Value,
                                       //Observaciones = null
                                       actividadReporte="NA"

                                   }).ToList();
                   
                        PlanDeAccion.Pk_Id_PlanDeAccion = Pk_Id_ModuloPlanAccion;
                        PlanDeAccion.Estado = consultarEstado(Actividades);
                        PlanDeAccion.EDActividadPlanDeAccion = Actividades;
                        PlanDeAccion.cantidadActividades = Actividades.Count();
                    

                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se obtuvo los planes de acción del módulo de evaluación : {0}, {1}", DateTime.Now, nit), new Exception());
                    return PlanDeAccion;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error obteniendo los planes de acción del módulo evaluación: {0}, {1}. Error: {2}", DateTime.Now, nit, ex.StackTrace), ex);
                return null;
            }
        }
        /// <summary>
        /// Consulta todos los planes de acción existentes para la empresa en el módulo acciones correctivas y preventivas
        /// </summary>
        /// <param name="empresa",name="Pk_Id_ModuloPlanAccion",name="Modulo"></param>
        /// <returns>List<EDPlanDeAccion></returns>
        public List<EDPlanDeAccion> ObtenerListaActividadAccion(int empresa, int Pk_Id_ModuloPlanAccion, string Modulo)
        {
            try
            {
                numeroActividadInsp = false;
                List<EDPlanDeAccion> ListaPlanDeAccion = new List<EDPlanDeAccion>();
                EDPlanDeAccion PlanDeAccion = new EDPlanDeAccion();
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    List<Accion> accion = (from e in db1.Tbl_Acciones
                                           where e.Fk_Id_Empresa == empresa
                                           select e).ToList();
                    List<EDActividadPlanDeAccion> Actividades = new List<EDActividadPlanDeAccion>();
                    foreach (var acc in accion)
                    {
                        Actividades = (from act in db1.Tbl_ActividadAccion
                                       where act.Fk_Id_Accion == acc.Pk_Id_Accion
                                       select new EDActividadPlanDeAccion
                                       {
                                           actividadReporte = acc.Id_Accion.ToString(),
                                           Fk_Id_ModuloPlanAccion = Pk_Id_ModuloPlanAccion,
                                           Fk_Plan_Inspección = acc.Pk_Id_Accion,
                                           Fk_Id_Actividad = act.Pk_Id_Actividad,
                                           Actividad = act.Actividad,
                                           Responsable = act.Responsable,
                                           FechaFinalizacion = act.FechaFinalizacion,
                                           consecutivo = acc.Id_Accion,
                                           //FechaCierre = ae.Descripcion,
                                           Observaciones = null

                                       }).OrderBy(x => x.Fk_Id_Actividad).ToList();
                        if (Actividades.Count() > 0)
                        {
                            PlanDeAccion.Origen = Modulo;
                            PlanDeAccion.Pk_Id_PlanDeAccion = Pk_Id_ModuloPlanAccion;
                            PlanDeAccion.Estado = consultarEstado(Actividades);
                            PlanDeAccion.EDActividadPlanDeAccion = Actividades;
                            PlanDeAccion.cantidadActividades = Actividades.Count();
                            ListaPlanDeAccion.Add(PlanDeAccion);
                            PlanDeAccion = new EDPlanDeAccion();
                        }
                    }
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se obtuvo los planes de acción del módulo de evaluación : {0}, {1}", DateTime.Now, empresa), new Exception());
                    if (ListaPlanDeAccion.Count() > 0)
                        return ListaPlanDeAccion;
                    else

                        return null;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error obteniendo los planes de acción del módulo acción: {0}, {1}. Error: {2}", DateTime.Now, empresa, ex.StackTrace), ex);
                return null;
            }
        }
        /// <summary>
        /// Consulta todos los planes de acción existentes para la empresa en el módulo auditoría
        /// </summary>
        /// <param name="empresa",name="Pk_Id_ModuloPlanAccion",name="Modulo"></param>
        /// <returns>List<EDPlanDeAccion></returns>
        public List<EDPlanDeAccion> ObtenerListaActividadAuditoria(int empresa, int Pk_Id_ModuloPlanAccion, string Modulo)
        {
            try
            {
                numeroActividadInsp = false;
                List<EDPlanDeAccion> ListaPlanDeAccion = new List<EDPlanDeAccion>();
                EDPlanDeAccion PlanDeAccion = new EDPlanDeAccion();
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    List<int> Pk_Id_Auditoria = (from aud in db1.Tbl_Auditorias
                                                 join pro in db1.Tbl_AuditoriaPrograma on aud.Fk_Id_Programa equals pro.Pk_Id_Programa
                                                 join act in db1.Tbl_ActividadAuditoria on aud.Pk_Id_Auditoria equals act.Fk_Id_Auditoria
                                                 where pro.Fk_Id_Empresa == empresa
                                                 select aud.Pk_Id_Auditoria).Distinct().ToList();
                    List<EDActividadPlanDeAccion> Actividades = new List<EDActividadPlanDeAccion>();
                    var idCon = 1;
                    foreach (var id in Pk_Id_Auditoria)
                    {


                        Actividades = (from ac in db1.Tbl_ActividadAuditoria
                                       where ac.Fk_Id_Auditoria == id
                                       select new EDActividadPlanDeAccion
                                       {


                                           actividadReporte = idCon.ToString(),
                                           Fk_Id_ModuloPlanAccion = Pk_Id_ModuloPlanAccion,
                                           Fk_Plan_Inspección = id,
                                           Fk_Id_Actividad = ac.Pk_Id_Actividad,
                                           Actividad = ac.Actividad,
                                           Responsable = ac.Responsable,
                                           FechaFinalizacion = ac.FechaFinalizacion,
                                           //FechaCierre = ae.Descripcion,
                                           Observaciones = null

                                       }).ToList(); 

                        idCon++;
                        if (Actividades.Count() > 0)
                        {
                            PlanDeAccion.Origen = Modulo;
                            PlanDeAccion.Pk_Id_PlanDeAccion = Pk_Id_ModuloPlanAccion;
                            PlanDeAccion.Estado = consultarEstado(Actividades);
                            PlanDeAccion.EDActividadPlanDeAccion = Actividades;
                            PlanDeAccion.cantidadActividades = Actividades.Count();

                            ListaPlanDeAccion.Add(PlanDeAccion);

                            PlanDeAccion = new EDPlanDeAccion();
                        }

                    }
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se obtuvo los planes de acción del módulo auditoría: {0}, {1}", DateTime.Now, empresa), new Exception());
                    if (ListaPlanDeAccion.Count() > 0)
                        return ListaPlanDeAccion;
                    else

                        return null;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error obteniendo los planes de acción del módulo auditoría: {0}, {1}. Error: {2}", DateTime.Now, empresa, ex.StackTrace), ex);
                return null;
            }
        }
        /// <summary>
        /// Consulta todos los planes de acción existentes para la empresa en el módulo inspecciones
        /// </summary>
        /// <param name="empresa",name="Pk_Id_ModuloPlanAccion",name="Modulo"></param>
        /// <returns>List<EDPlanDeAccion></returns>
        public List<EDPlanDeAccion> ObtenerListaInspecciones(int empresa, int Pk_Id_ModuloPlanAccion, string Modulo)
        {
            try
            {
                
                List<EDPlanDeAccion> ListaPlanDeAccion = new List<EDPlanDeAccion>();
                EDPlanDeAccion PlanDeAccion = new EDPlanDeAccion();
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                   

                        List<EDActividadPlanDeAccion> Actividades = new List<EDActividadPlanDeAccion>();
                       
                        List<int> inspecciones = (from inp in db1.Tbl_Inspecciones
                                                  join planIn in db1.Tbl_Planeacion_Inspeccion on inp.Fk_Id_PlaneacionInspeccion equals planIn.Pk_Id_PlaneacionInspeccion
                                                  where inp.Fk_IdEmpresa == empresa
                                                  select planIn.Pk_Id_PlaneacionInspeccion).ToList();



              


                        foreach (var ins in inspecciones)
                        {
                            Actividades = (from oip in db1.Tbl_PlanAccionInspeccion

                                           join pi in db1.Tbl_PlanAccionporCondicion on oip.Pk_Id_PlanAcccionInspeccion equals pi.Fk_Id_PlanAcccionInspeccion


                                           join ci in db1.CondicionInsegura on pi.Fk_Id_CondicionInsegura equals ci.Pk_Id_CondicionInsegura

                                           join cipo in db1.Tbl_CondicionesInseguraporasInspeccion on ci.Pk_Id_CondicionInsegura equals cipo.Fk_Id_CondicionInsegura

                                           join inp in db1.Tbl_Inspecciones on cipo.Fk_Id_Inspecciones equals inp.Pk_Id_Inspecciones

                                           join plan in db1.Tbl_Planeacion_Inspeccion on inp.Fk_Id_PlaneacionInspeccion equals plan.Pk_Id_PlaneacionInspeccion



                                          // where inp.Fk_IdEmpresa == empresa


                                           where inp.Fk_Id_PlaneacionInspeccion == ins
                                           select new EDActividadPlanDeAccion
                                           {

                                               actividadReporte = plan.ConsecutivoPlan.ToString(),
                                               Fk_Id_ModuloPlanAccion = Pk_Id_ModuloPlanAccion,
                                               Fk_Plan_Inspección = plan.Pk_Id_PlaneacionInspeccion,
                                               Fk_Id_Actividad = oip.Pk_Id_PlanAcccionInspeccion,
                                               Actividad = oip.Actividad_Plan_Accion,
                                               Responsable = oip.Responsable_Plan_Accion,
                                               FechaFinalizacion = oip.Fecha_Fin_Plan_Accion,
                                               //FechaCierre = ae.Descripcion,
                                               Observaciones = null,

                                           }).Distinct().ToList();

                           
                            foreach (var act in Actividades)
                            {
                                act.FechaFinalizacion = act.FechaFinalizacion;
                            }


                            if (Actividades.Count() > 0)
                            {
                                PlanDeAccion.Origen = Modulo;
                                PlanDeAccion.Pk_Id_PlanDeAccion = Pk_Id_ModuloPlanAccion;
                                PlanDeAccion.Estado = consultarEstado(Actividades);
                                PlanDeAccion.EDActividadPlanDeAccion = Actividades;
                                PlanDeAccion.cantidadActividades = Actividades.Count();
                                ListaPlanDeAccion.Add(PlanDeAccion);
                                PlanDeAccion = new EDPlanDeAccion();
                            }
                        }
                   
                        var log = new RegistraLog();
                        log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se obtuvo los planes de acción del módulo reportes de actos inseguros: {0}, {1}", DateTime.Now, empresa), new Exception());
                        if (ListaPlanDeAccion.Count() > 0)
                            return ListaPlanDeAccion;
                        else

                            return null;
                    }

                }
            
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error obteniendo los planes de acción del módulo inspecciones: {0}, {1}. Error: {2}", DateTime.Now, empresa, ex.StackTrace), ex);
                return null;
            }
        }
        /// <summary>
        /// Consulta todos los planes de acción existentes para la empresa en el módulo reportes de actos inseguros
        /// </summary>
        /// <param name="empresa",name="Pk_Id_ModuloPlanAccion",name="Modulo"></param>
        /// <returns>List<EDPlanDeAccion></returns>
        public List<EDPlanDeAccion> ObtenerListaReportes(int empresa, int Pk_Id_ModuloPlanAccion, string Modulo)
        {
            try
            {
                numeroActividadInsp = false;
                List<EDPlanDeAccion> ListaPlanDeAccion = new List<EDPlanDeAccion>();
                EDPlanDeAccion PlanDeAccion = new EDPlanDeAccion();
                List<EDActividadPlanDeAccion> Actividades = new List<EDActividadPlanDeAccion>();
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    string nitEmpresa = (from emp in db1.Tbl_Empresa
                                          where emp.Pk_Id_Empresa == empresa
                                   select emp.Nit_Empresa).FirstOrDefault();
                    //foreach (var sede in Fk_Id_Sede)
                    
                    //{
                        List<int> reportes = (from reporte in db1.Tbl_Reportes
                                              where reporte.FK_NitEmpresa.Equals(nitEmpresa)
                                              select reporte.PK_Id_Reportes).ToList();
                        foreach (var rep in reportes)
                        {
                            //Actividades = (from activi in db1.Tbl_ActividadesActosInseguros
                            //               where activi.FK_Id_Reportes == rep

                            Actividades = (from activi in db1.Tbl_ActividadesActosInseguros
                                           join
                                               repo in db1.Tbl_Reportes on activi.FK_Id_Reportes equals
                                               repo.PK_Id_Reportes
                                           where activi.FK_Id_Reportes == rep
                                           select new EDActividadPlanDeAccion
                                           {
                                               Fk_Id_ModuloPlanAccion = Pk_Id_ModuloPlanAccion,

                                               Fk_Plan_Inspección = rep,

                                               actividadReporte = repo.ConsecutivoReporte.ToString(),
                                               //Fk_Plan_Inspección = repo.ConsecutivoReporte,
                                               Fk_Id_Actividad = activi.PK_ID_ActividadActosInseguros,
                                               Actividad = activi.NombreActividad,
                                               Responsable = activi.ResponsableActividad,
                                               FechaFinalizacion = activi.FechaEjecucion,
                                               //FechaCierre = ae.Descripcion,
                                               Observaciones = null

                                           }).OrderBy(x => x.Fk_Id_Actividad).ToList();
                            if (Actividades.Count() > 0)
                            {
                                PlanDeAccion.Origen = Modulo;
                                PlanDeAccion.Pk_Id_PlanDeAccion = Pk_Id_ModuloPlanAccion;
                                PlanDeAccion.Estado = consultarEstado(Actividades);
                                PlanDeAccion.EDActividadPlanDeAccion = Actividades;
                                PlanDeAccion.cantidadActividades = Actividades.Count();
                                ListaPlanDeAccion.Add(PlanDeAccion);
                                PlanDeAccion = new EDPlanDeAccion();
                            }
                        }
                    //}
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se obtuvo los planes de acción del módulo reportes de actos inseguros: {0}, {1}", DateTime.Now, empresa), new Exception());
                    if (ListaPlanDeAccion.Count() > 0)
                        return ListaPlanDeAccion;
                    else

                        return null;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error obteniendo los planes de acción del módulo reportes de actos inseguros: {0}, {1}. Error: {2}", DateTime.Now, empresa, ex.StackTrace), ex);
                return null;
            }
        }


        /// <summary>
        /// Consulta todos los planes de acción existentes para la empresa en el módulo actas de Copasst
        /// </summary>
        /// <param name="empresa",name="Pk_Id_ModuloPlanAccion",name="Modulo"></param>
        /// <returns>List<EDPlanDeAccion></returns>
        public List<EDPlanDeAccion> ObtenerListaCopasst(int empresa, int Pk_Id_ModuloPlanAccion, string Modulo)
        {
            try
            {
                numeroActividadInsp = false;
                List<EDPlanDeAccion> ListaPlanDeAccion = new List<EDPlanDeAccion>();
                EDPlanDeAccion PlanDeAccion = new EDPlanDeAccion();
                List<EDActividadPlanDeAccion> Actividades = new List<EDActividadPlanDeAccion>();
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    //List<int> Fk_Id_Sede = (from sed in db1.Tbl_Sede
                    //                        where sed.Fk_Id_Empresa == empresa
                    //                        select sed.Pk_Id_Sede).ToList();
                    //foreach (var sede in Fk_Id_Sede)
                    //{
                        List<int> copasst = (from cop in db1.Tbl_ActasCopasst
                                              where cop.Fk_Id_Empresa == empresa
                                              select cop.PK_Id_Acta).ToList();
                        foreach (var cop in copasst)
                        {
                            //Actividades = (from activi in db1.Tbl_ActividadesActosInseguros
                            //               where activi.FK_Id_Reportes == rep

                            Actividades = (from activi in db1.Tbl_AccionesActaCopasst
                                           join
                                               copa in db1.Tbl_ActasCopasst on activi.Fk_Id_Acta equals
                                               copa.PK_Id_Acta
                                           where activi.Fk_Id_Acta == cop
                                           select new EDActividadPlanDeAccion
                                           {
                                               Fk_Id_ModuloPlanAccion = Pk_Id_ModuloPlanAccion,

                                               Fk_Plan_Inspección = cop,

                                               actividadReporte = copa.Consecutivo_Acta.ToString(),
                                               //Fk_Plan_Inspección = repo.ConsecutivoReporte,
                                               Fk_Id_Actividad = activi.Pk_Id_AccionActaCopasst,
                                               Actividad = activi.AccionARealizar,
                                               Responsable = activi.Responsable,
                                               FechaFinalizacion = activi.FechaProbable,
                                               //FechaCierre = ae.Descripcion,
                                               Observaciones = null

                                           }).Distinct().ToList();

                            if (Actividades.Count() > 0)
                            {
                                PlanDeAccion.Origen = Modulo;
                                PlanDeAccion.Pk_Id_PlanDeAccion = Pk_Id_ModuloPlanAccion;
                                PlanDeAccion.Estado = consultarEstado(Actividades);
                                PlanDeAccion.EDActividadPlanDeAccion = Actividades;
                                PlanDeAccion.cantidadActividades = Actividades.Count();
                                ListaPlanDeAccion.Add(PlanDeAccion);
                                PlanDeAccion = new EDPlanDeAccion();
                            }
                        }
                    //}
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se obtuvo los planes de acción del módulo Coppast: {0}, {1}", DateTime.Now, empresa), new Exception());
                    if (ListaPlanDeAccion.Count() > 0)
                        return ListaPlanDeAccion;
                    else

                        return null;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error obteniendo los planes de acción del módulo Copasst: {0}, {1}. Error: {2}", DateTime.Now, empresa, ex.StackTrace), ex);
                return null;
            }
        }

        /// <summary>
        /// Consulta todos los planes de acción existentes para la empresa en el módulo comité de convivencia laboral
        /// </summary>
        /// <param name="empresa",name="Pk_Id_ModuloPlanAccion",name="Modulo"></param>
        /// <returns>List<EDPlanDeAccion></returns>



        public List<EDPlanDeAccion> ObtenerListaConvivenciaLaboral(int empresa, int Pk_Id_ModuloPlanAccion, string Modulo)
        {
            try
            {
                numeroActividadInsp = false;
                List<EDPlanDeAccion> ListaPlanDeAccion = new List<EDPlanDeAccion>();
                EDPlanDeAccion PlanDeAccion = new EDPlanDeAccion();
                List<EDActividadPlanDeAccion> Actividades = new List<EDActividadPlanDeAccion>();
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    //List<int> Fk_Id_Sede = (from sed in db1.Tbl_Sede
                    //                        where sed.Fk_Id_Empresa == empresa
                    //                        select sed.Pk_Id_Sede).ToList();
                    //foreach (var sede in Fk_Id_Sede)
                    //{
                        List<int> convivencia = (from convi in db1.Tbl_ActasConvivencia
                                             where convi.Fk_Id_Empresa == empresa
                                             select convi.PK_Id_Acta).ToList();
                        foreach (var convi in convivencia)
                        {
                            //Actividades = (from activi in db1.Tbl_ActividadesActosInseguros
                            //               where activi.FK_Id_Reportes == rep

                            Actividades = (from activi in db1.Tbl_AccionesActaConvivencia
                                           join
                                               Convive in db1.Tbl_ActasConvivencia on activi.Fk_Id_Acta equals
                                               Convive.PK_Id_Acta
                                           where activi.Fk_Id_Acta == convi
                                           select new EDActividadPlanDeAccion
                                           {
                                               Fk_Id_ModuloPlanAccion = Pk_Id_ModuloPlanAccion,

                                               Fk_Plan_Inspección = convi,

                                               actividadReporte = Convive.Consecutivo_Acta.ToString(),
                                               //Fk_Plan_Inspección = repo.ConsecutivoReporte,
                                               Fk_Id_Actividad = activi.Pk_Id_AccionActaConvivencia,
                                               Actividad = activi.AccionARealizar,
                                               Responsable = activi.Responsable,
                                               FechaFinalizacion = activi.FechaProbable,
                                               //FechaCierre = ae.Descripcion,
                                               Observaciones = null

                                           }).OrderBy(x => x.Fk_Id_Actividad).ToList();
                            if (Actividades.Count() > 0)
                            {
                                PlanDeAccion.Origen = Modulo;
                                PlanDeAccion.Pk_Id_PlanDeAccion = Pk_Id_ModuloPlanAccion;
                                PlanDeAccion.Estado = consultarEstado(Actividades);
                                PlanDeAccion.EDActividadPlanDeAccion = Actividades;
                                PlanDeAccion.cantidadActividades = Actividades.Count();

                                ListaPlanDeAccion.Add(PlanDeAccion);
                                PlanDeAccion = new EDPlanDeAccion();
                            }
                        }
                    //}
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se obtuvo los planes de acción del módulo convivencia laboral: {0}, {1}", DateTime.Now, empresa), new Exception());
                    if (ListaPlanDeAccion.Count() > 0)
                        return ListaPlanDeAccion;
                    else

                        return null;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error obteniendo los planes de acción del módulo convivencia laboral: {0}, {1}. Error: {2}", DateTime.Now, empresa, ex.StackTrace), ex);
                return null;
            }
        }



        /// <summary>
        /// Consulta todos los planes de acción existentes para la empresa en el módulo Revision SGSST
        /// </summary>
        /// <param name="empresa",name="Pk_Id_ModuloPlanAccion",name="Modulo"></param>
        /// <returns>List<EDPlanDeAccion></returns>



        public List<EDPlanDeAccion> ObtenerListaRevisionSGSST(int empresa, int Pk_Id_ModuloPlanAccion, string Modulo)
        {
            try
            {
                numeroActividadInsp = false;
                List<EDPlanDeAccion> ListaPlanDeAccion = new List<EDPlanDeAccion>();
                EDPlanDeAccion PlanDeAccion = new EDPlanDeAccion();
                List<EDActividadPlanDeAccion> Actividades = new List<EDActividadPlanDeAccion>();
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                 
                    List<int> revision = (from revi in db1.Tbl_ActaRevision
                                             where revi.Fk_Id_Empresa == empresa
                                             select revi.PK_Id_ActaRevision).ToList();
                    foreach (var revi in revision)
                    {
                       
                        Actividades = (from activi in db1.Tbl_PlanAccionRevision
                                       join
                                           revis in db1.Tbl_ActaRevision on activi.FK_Acta equals
                                           revis.PK_Id_ActaRevision
                                       where activi.FK_Acta == revi
                                       select new EDActividadPlanDeAccion
                                       {
                                           Fk_Id_ModuloPlanAccion = Pk_Id_ModuloPlanAccion,

                                           Fk_Plan_Inspección = revi,

                                           actividadReporte = revis.Num_Acta.ToString(),
                                           //Fk_Plan_Inspección = repo.ConsecutivoReporte,
                                           Fk_Id_Actividad = activi.PK_Id_PlanAccion,
                                           Actividad = activi.Actividad,
                                           Responsable = activi.Responsable,
                                           FechaFinalizacion = activi.Fecha,
                                           //FechaCierre = ae.Descripcion,
                                           Observaciones = null

                                       }).OrderBy(x => x.Fk_Id_Actividad).ToList();

                        if (Actividades.Count()>0)
                        {
                            PlanDeAccion.Origen = Modulo;
                            PlanDeAccion.Pk_Id_PlanDeAccion = Pk_Id_ModuloPlanAccion;
                            PlanDeAccion.Estado = consultarEstado(Actividades);
                            PlanDeAccion.EDActividadPlanDeAccion = Actividades;
                            PlanDeAccion.cantidadActividades = Actividades.Count();

                            ListaPlanDeAccion.Add(PlanDeAccion);
                            PlanDeAccion = new EDPlanDeAccion();
                        }
                   
                    }
                    //}
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se obtuvo los planes de acción del módulo revisión del SGSST: {0}, {1}", DateTime.Now, empresa), new Exception());
                    if (ListaPlanDeAccion.Count() > 0)
                        return ListaPlanDeAccion;
                    else

                        return null;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error obteniendo los planes de acción del módulo revisión del SGSST: {0}, {1}. Error: {2}", DateTime.Now, empresa, ex.StackTrace), ex);
                return null;
            }
        }

        
       
        /// <summary>
        /// Guarda la fecha de cierre y las observaciones de la actividad
        /// </summary>
        /// <param name="actividadPlanDeAccion"></param>
        /// <returns>EDActividadPlanDeAccion</returns>
        public EDActividadPlanDeAccion GuardarPlanesDeAccion(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            try
            {
                ActividadPlanDeAccion planDeAccion = new ActividadPlanDeAccion();
                planDeAccion.Fk_Id_ModuloPlanAccion = actividadPlanDeAccion.Fk_Id_ModuloPlanAccion;
                planDeAccion.Fk_Plan_Inspección = actividadPlanDeAccion.Fk_Plan_Inspección;
                planDeAccion.Fk_Id_Actividad = actividadPlanDeAccion.Fk_Id_Actividad;
                planDeAccion.FechaCierre = actividadPlanDeAccion.FechaCierre;
                planDeAccion.Observaciones = actividadPlanDeAccion.Observaciones;
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    db1.Tbl_Actividad_Plan_Accion.Add(planDeAccion);
                    if (actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == 2)
                    {
                        ActividadAccion actividadAccion = new ActividadAccion();
                        actividadAccion = db1.Tbl_ActividadAccion.Where(x => x.Pk_Id_Actividad == actividadPlanDeAccion.Fk_Id_Actividad).FirstOrDefault();
                        // Se cierra la actividad en el módulo 1 Abierto - 0 Cerrado
                        actividadAccion.Estado = 0;
                        db1.Entry(actividadAccion).State = EntityState.Modified;
                    }
                    else if (actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == 4)
                    {
                        PlanAccionInspeccion planAccionInspeccion = new PlanAccionInspeccion();
                        planAccionInspeccion = db1.Tbl_PlanAccionInspeccion.Where(x => x.Pk_Id_PlanAcccionInspeccion == actividadPlanDeAccion.Fk_Id_Actividad).FirstOrDefault();
                        // Se cierra la actividad en el módulo 1 Abierto - 0 Cerrado
                        planAccionInspeccion.Estado = 0;
                        planAccionInspeccion.Fecha_Cierre_Plan = actividadPlanDeAccion.FechaCierre;
                        db1.Entry(planAccionInspeccion).State = EntityState.Modified;
                    }
                    db1.SaveChanges();
                    if (planDeAccion.Pk_Id_ActividadPlanAccion > 0)
                    {
                        actividadPlanDeAccion.Pk_Id_ActividadPlanAccion = planDeAccion.Pk_Id_ActividadPlanAccion;

                    }
                    else
                        return null;

                }
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha guardado la fecha de cierre de la actividad: {0}, {1}", DateTime.Now, actividadPlanDeAccion.Actividad), new Exception());
                return actividadPlanDeAccion;
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error guardando la fecha de cierre de la actividad: {0}, {1}. Error: {2}", DateTime.Now, actividadPlanDeAccion.Actividad, ex.StackTrace), ex);
                return null;
            }

        }
        /// <summary>
        /// Elimina la actividad del plan de acción
        /// </summary>
        /// <param name="actividadPlanDeAccion"></param>
        /// <returns>bool</returns>

        public bool EliminarActividad(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            bool eliminado = false;
            try
            {
                if (actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == 1)
                {
                    eliminado = EliminarActividadEvaluacion(actividadPlanDeAccion);
                }
                else if (actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == 2)
                {
                    eliminado = EliminarActividadAccion(actividadPlanDeAccion);
                }
                else if (actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == 3)
                {
                    eliminado = EliminarActividadAuditoria(actividadPlanDeAccion);
                }
                else if (actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == 4)
                {
                    eliminado = EliminarActividadInspecciones(actividadPlanDeAccion);
                }
                else if (actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == 5)
                {
                    eliminado = EliminarActividadReportes(actividadPlanDeAccion);
                }
                else if (actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == 6)
                {
                    eliminado = EliminarActividadCopasst(actividadPlanDeAccion);
                }
                else if (actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == 7)
                {
                    eliminado = EliminarActividadConvivenciaLaboral(actividadPlanDeAccion);
                }
                else if (actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == 8)
                {
                    eliminado = EliminarActividadRevisionSGSST(actividadPlanDeAccion);
                }
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha eliminado la actividad: {0}, {1}", DateTime.Now, actividadPlanDeAccion.Actividad), new Exception());
                return eliminado;
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error eliminando la actividad: {0}, {1}. Error: {2}", DateTime.Now, actividadPlanDeAccion.Actividad, ex.StackTrace), ex);
                return false;
            }

        }
        /// <summary>
        /// Elimina la actividad del módulo evaluación
        /// </summary>
        /// <param name="actividadPlanDeAccion"></param>
        /// <returns>bool</returns>
        public bool EliminarActividadEvaluacion(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    ActividadEvaluacion actividadEvaluacion = new ActividadEvaluacion();
                    actividadEvaluacion = db1.Tbl_Actividades_Evaluacion.Where(x => x.Pk_Id_Actividad == actividadPlanDeAccion.Fk_Id_Actividad).FirstOrDefault();
                    db1.Tbl_Actividades_Evaluacion.Remove(actividadEvaluacion);
                    ActividadPlanDeAccion planDeAccion = (from actividad in db1.Tbl_Actividad_Plan_Accion
                                                          where actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == actividad.Fk_Id_ModuloPlanAccion
                                                                       & actividadPlanDeAccion.Fk_Plan_Inspección == actividad.Fk_Plan_Inspección
                                                                       & actividadPlanDeAccion.Fk_Id_Actividad == actividad.Fk_Id_Actividad
                                                          select actividad).FirstOrDefault();
                    if (planDeAccion != null)
                    {
                        db1.Tbl_Actividad_Plan_Accion.Remove(planDeAccion);
                    }
                    db1.SaveChanges();
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha eliminado la actividad del módulo evaluación: {0}, {1}", DateTime.Now, actividadPlanDeAccion.Actividad), new Exception());
                    return true;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error eliminando la actividad del módulo evaluación: {0}, {1}. Error: {2}", DateTime.Now, actividadPlanDeAccion.Actividad, ex.StackTrace), ex);
                return false;
            }
        }
        /// <summary>
        /// Elimina la actividad del módulo acciones correctivas y preventivas
        /// </summary>
        /// <param name="actividadPlanDeAccion"></param>
        /// <returns>bool</returns>
        public bool EliminarActividadAccion(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    ActividadAccion actividadAccion = new ActividadAccion();
                    actividadAccion = db1.Tbl_ActividadAccion.Where(x => x.Pk_Id_Actividad == actividadPlanDeAccion.Fk_Id_Actividad).FirstOrDefault();
                    db1.Tbl_ActividadAccion.Remove(actividadAccion);
                    ActividadPlanDeAccion planDeAccion = (from actividad in db1.Tbl_Actividad_Plan_Accion
                                                          where actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == actividad.Fk_Id_ModuloPlanAccion
                                                                       & actividadPlanDeAccion.Fk_Plan_Inspección == actividad.Fk_Plan_Inspección
                                                                       & actividadPlanDeAccion.Fk_Id_Actividad == actividad.Fk_Id_Actividad
                                                          select actividad).FirstOrDefault();
                    if (planDeAccion != null)
                    {
                        db1.Tbl_Actividad_Plan_Accion.Remove(planDeAccion);
                    }
                    db1.SaveChanges();
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha eliminado la actividad del módulo acción: {0}, {1}", DateTime.Now, actividadPlanDeAccion.Actividad), new Exception());
                    return true;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error eliminando la actividad del módulo acción: {0}, {1}. Error: {2}", DateTime.Now, actividadPlanDeAccion.Actividad, ex.StackTrace), ex);
                return false;
            }
        }
        /// <summary>
        /// Elimina la actividad del módulo auditoría
        /// </summary>
        /// <param name="actividadPlanDeAccion"></param>
        /// <returns>bool</returns>
        /// 

        public bool EliminarActividadAuditoria(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    ActividadAuditoria actividadAuditoria = new ActividadAuditoria();
                    actividadAuditoria = db1.Tbl_ActividadAuditoria.Where(x => x.Pk_Id_Actividad == actividadPlanDeAccion.Fk_Id_Actividad).FirstOrDefault();
                    db1.Tbl_ActividadAuditoria.Remove(actividadAuditoria);
                    ActividadPlanDeAccion planDeAccion = (from actividad in db1.Tbl_Actividad_Plan_Accion
                                                          where actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == actividad.Fk_Id_ModuloPlanAccion
                                                                       & actividadPlanDeAccion.Fk_Plan_Inspección == actividad.Fk_Plan_Inspección
                                                                       & actividadPlanDeAccion.Fk_Id_Actividad == actividad.Fk_Id_Actividad
                                                          select actividad).FirstOrDefault();
                    if (planDeAccion != null)
                    {
                        db1.Tbl_Actividad_Plan_Accion.Remove(planDeAccion);
                    }
                    db1.SaveChanges();
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha eliminado la actividad del módulo auditoría: {0}, {1}", DateTime.Now, actividadPlanDeAccion.Actividad), new Exception());
                    return true;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error eliminando la actividad del módulo auditoría: {0}, {1}. Error: {2}", DateTime.Now, actividadPlanDeAccion.Actividad, ex.StackTrace), ex);
                return false;
            }
        }

        /// <summary>
        /// Elimina la actividad de inspecciones.
        /// </summary>
        /// <param name="actividadPlanDeAccion"></param>
        /// <returns>bool</returns>
        public bool EliminarActividadInspecciones(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    PlanAccionInspeccion planAccionInspeccion = new PlanAccionInspeccion();
                    planAccionInspeccion = db1.Tbl_PlanAccionInspeccion.Where(x => x.Pk_Id_PlanAcccionInspeccion == actividadPlanDeAccion.Fk_Id_Actividad).FirstOrDefault();
                    db1.Tbl_PlanAccionInspeccion.Remove(planAccionInspeccion);
                    ActividadPlanDeAccion planDeAccion = (from actividad in db1.Tbl_Actividad_Plan_Accion
                                                          where actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == actividad.Fk_Id_ModuloPlanAccion
                                                                       & actividadPlanDeAccion.Fk_Plan_Inspección == actividad.Fk_Plan_Inspección
                                                                       & actividadPlanDeAccion.Fk_Id_Actividad == actividad.Fk_Id_Actividad
                                                          select actividad).FirstOrDefault();
                    if (planDeAccion != null)
                    {
                        db1.Tbl_Actividad_Plan_Accion.Remove(planDeAccion);
                    }
                    db1.SaveChanges();
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha eliminado la actividad del módulo inspecciones: {0}, {1}", DateTime.Now, actividadPlanDeAccion.Actividad), new Exception());
                    return true;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error eliminando la actividad del módulo inspecciones: {0}, {1}. Error: {2}", DateTime.Now, actividadPlanDeAccion.Actividad, ex.StackTrace), ex);
                return false;
            }
        }

        /// <summary>
        /// Elimina la actividad de reportes.
        /// </summary>
        /// <param name="actividadPlanDeAccion"></param>
        /// <returns>bool</returns>
        public bool EliminarActividadReportes(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    ActividadesActosInseguros actividadesActosInseguros = new ActividadesActosInseguros();
                    actividadesActosInseguros = db1.Tbl_ActividadesActosInseguros.Where(x => x.PK_ID_ActividadActosInseguros == actividadPlanDeAccion.Fk_Id_Actividad).FirstOrDefault();
                    db1.Tbl_ActividadesActosInseguros.Remove(actividadesActosInseguros);
                    ActividadPlanDeAccion planDeAccion = (from actividad in db1.Tbl_Actividad_Plan_Accion
                                                          where actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == actividad.Fk_Id_ModuloPlanAccion
                                                                       & actividadPlanDeAccion.Fk_Plan_Inspección == actividad.Fk_Plan_Inspección
                                                                       & actividadPlanDeAccion.Fk_Id_Actividad == actividad.Fk_Id_Actividad
                                                          select actividad).FirstOrDefault();
                    if (planDeAccion != null)
                    {
                        db1.Tbl_Actividad_Plan_Accion.Remove(planDeAccion);
                    }
                    db1.SaveChanges();
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha eliminado la actividad del módulo reportes de condiciones inseguras: {0}, {1}", DateTime.Now, actividadPlanDeAccion.Actividad), new Exception());
                    return true;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error eliminando la actividad del módulo reportes de condiciones inseguras: {0}, {1}. Error: {2}", DateTime.Now, actividadPlanDeAccion.Actividad, ex.StackTrace), ex);
                return false;
            }
        }

        /// <summary>
        /// Elimina la actividad copasst
        /// </summary>
        /// <param name="actividadPlanDeAccion"></param>
        /// <returns>bool</returns>
        public bool EliminarActividadCopasst(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    AccionesActaCopasst actividadesActasCopasst = new AccionesActaCopasst();
                    actividadesActasCopasst = db1.Tbl_AccionesActaCopasst.Where(x => x.Pk_Id_AccionActaCopasst == actividadPlanDeAccion.Fk_Id_Actividad).FirstOrDefault();
                    db1.Tbl_AccionesActaCopasst.Remove(actividadesActasCopasst);
                    ActividadPlanDeAccion planDeAccion = (from actividad in db1.Tbl_Actividad_Plan_Accion
                                                          where actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == actividad.Fk_Id_ModuloPlanAccion
                                                                       & actividadPlanDeAccion.Fk_Plan_Inspección == actividad.Fk_Plan_Inspección
                                                                       & actividadPlanDeAccion.Fk_Id_Actividad == actividad.Fk_Id_Actividad
                                                          select actividad).FirstOrDefault();
                    if (planDeAccion != null)
                    {
                        db1.Tbl_Actividad_Plan_Accion.Remove(planDeAccion);
                    }
                    db1.SaveChanges();
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha eliminado la actividad del módulo Coppast: {0}, {1}", DateTime.Now, actividadPlanDeAccion.Actividad), new Exception());
                    return true;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error eliminando la actividad del módulo Coppast: {0}, {1}. Error: {2}", DateTime.Now, actividadPlanDeAccion.Actividad, ex.StackTrace), ex);
                return false;
            }
        }

        


        /// <summary>
        ///  Elimina la actividad de convivencia laboral.
        /// </summary>
        /// <param name="actividadPlanDeAccion"></param>
        /// <returns>bool</returns>


        public bool EliminarActividadConvivenciaLaboral(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    AccionesActaConvivencia actividadesActasConvivencia = new AccionesActaConvivencia();
                    actividadesActasConvivencia = db1.Tbl_AccionesActaConvivencia.Where(x => x.Pk_Id_AccionActaConvivencia == actividadPlanDeAccion.Fk_Id_Actividad).FirstOrDefault();
                    db1.Tbl_AccionesActaConvivencia.Remove(actividadesActasConvivencia);
                    ActividadPlanDeAccion planDeAccion = (from actividad in db1.Tbl_Actividad_Plan_Accion
                                                          where actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == actividad.Fk_Id_ModuloPlanAccion
                                                                       & actividadPlanDeAccion.Fk_Plan_Inspección == actividad.Fk_Plan_Inspección
                                                                       & actividadPlanDeAccion.Fk_Id_Actividad == actividad.Fk_Id_Actividad
                                                          select actividad).FirstOrDefault();
                    if (planDeAccion != null)
                    {
                        db1.Tbl_Actividad_Plan_Accion.Remove(planDeAccion);
                    }
                    db1.SaveChanges();
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha eliminado la actividad del módulo Convivencia Laboral: {0}, {1}", DateTime.Now, actividadPlanDeAccion.Actividad), new Exception());
                    return true;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error eliminando la actividad del módulo Convivencia Laboral: {0}, {1}. Error: {2}", DateTime.Now, actividadPlanDeAccion.Actividad, ex.StackTrace), ex);
                return false;
            }
        }


        /// <summary>
        /// Elimina la actividad de Revisión del SGSST.
        /// </summary>
        /// <param name="actividadPlanDeAccion"></param>
        /// <returns>bool</returns>

        public bool EliminarActividadRevisionSGSST(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    PlanAccionRevision actividadesRevisionSGSST = new PlanAccionRevision();
                    actividadesRevisionSGSST = db1.Tbl_PlanAccionRevision.Where(x => x.PK_Id_PlanAccion == actividadPlanDeAccion.Fk_Id_Actividad).FirstOrDefault();
                    db1.Tbl_PlanAccionRevision.Remove(actividadesRevisionSGSST);
                    ActividadPlanDeAccion planDeAccion = (from actividad in db1.Tbl_Actividad_Plan_Accion
                                                          where actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == actividad.Fk_Id_ModuloPlanAccion
                                                                       & actividadPlanDeAccion.Fk_Plan_Inspección == actividad.Fk_Plan_Inspección
                                                                       & actividadPlanDeAccion.Fk_Id_Actividad == actividad.Fk_Id_Actividad
                                                          select actividad).FirstOrDefault();
                    if (planDeAccion != null)
                    {
                        db1.Tbl_Actividad_Plan_Accion.Remove(planDeAccion);
                    }
                    db1.SaveChanges();
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha eliminado la actividad del módulo Revisión SGSST: {0}, {1}", DateTime.Now, actividadPlanDeAccion.Actividad), new Exception());
                    return true;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error eliminando la actividad del módulo Revisión SGSST: {0}, {1}. Error: {2}", DateTime.Now, actividadPlanDeAccion.Actividad, ex.StackTrace), ex);
                return false;
            }
        }
        /// <summary>
        /// Edita la actividad del plan de acción
        /// </summary>
        /// <param name="actividadPlanDeAccion"></param>
        /// <returns>bool</returns>
        public bool EditarActividad(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            bool editado = false;
            try
            {
                if (actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == 1)
                {
                    editado = EditarActividadEvaluacion(actividadPlanDeAccion);
                }
                else if (actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == 2)
                {
                    editado = EditarActividadAccion(actividadPlanDeAccion);
                }
                else if (actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == 3)
                {
                    editado = EditarActividadAuditoria(actividadPlanDeAccion);
                }
                else if (actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == 4)
                {
                    editado = EditarActividadInspeccion(actividadPlanDeAccion);
                }
                else if (actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == 5)
                {
                    editado = EditarActividadReportes(actividadPlanDeAccion);
                }
                else if (actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == 6)
                {
                    editado = EditarActividadCopasst(actividadPlanDeAccion);
                }
                else if (actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == 7)
                {
                    editado = EditarActividadConvivencia(actividadPlanDeAccion);
                }

                else if (actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == 8)
                {
                    editado = EditarActividadRevisionSGSST(actividadPlanDeAccion);
                }
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha editado la actividad: {0}, {1}", DateTime.Now, actividadPlanDeAccion.Actividad), new Exception());
                return editado;
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error editando la actividad: {0}, {1}. Error: {2}", DateTime.Now, actividadPlanDeAccion.Actividad, ex.StackTrace), ex);
                return false;
            }

        }
        /// <summary>
        /// Edita la actividad del módulo evaluación
        /// </summary>
        /// <param name="actividadPlanDeAccion"></param>
        /// <returns>bool</returns>
        public bool EditarActividadEvaluacion(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    ActividadEvaluacion actividadEvaluacion = new ActividadEvaluacion();
                    actividadEvaluacion = db1.Tbl_Actividades_Evaluacion.Where(x => x.Pk_Id_Actividad == actividadPlanDeAccion.Fk_Id_Actividad).FirstOrDefault();
                    actividadEvaluacion.Descripcion = actividadPlanDeAccion.Actividad;
                    actividadEvaluacion.Responsable = actividadPlanDeAccion.Responsable;
                    db1.Entry(actividadEvaluacion).State = EntityState.Modified;
                    if (actividadPlanDeAccion.FechaCierre.Date > DateTime.MinValue.Date)
                    {
                        ActividadPlanDeAccion planDeAccion = (from actividad in db1.Tbl_Actividad_Plan_Accion
                                                              where actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == actividad.Fk_Id_ModuloPlanAccion
                                                                           & actividadPlanDeAccion.Fk_Plan_Inspección == actividad.Fk_Plan_Inspección
                                                                           & actividadPlanDeAccion.Fk_Id_Actividad == actividad.Fk_Id_Actividad
                                                              select actividad).FirstOrDefault();
                        if (planDeAccion != null)
                        {
                            planDeAccion.FechaCierre = actividadPlanDeAccion.FechaCierre;
                            planDeAccion.Observaciones = actividadPlanDeAccion.Observaciones;
                            db1.Entry(planDeAccion).State = EntityState.Modified;
                        }
                        else
                        {
                            planDeAccion = new ActividadPlanDeAccion();
                            planDeAccion.Fk_Id_ModuloPlanAccion = actividadPlanDeAccion.Fk_Id_ModuloPlanAccion;
                            planDeAccion.Fk_Plan_Inspección = actividadPlanDeAccion.Fk_Plan_Inspección;
                            planDeAccion.Fk_Id_Actividad = actividadPlanDeAccion.Fk_Id_Actividad;
                            planDeAccion.FechaCierre = actividadPlanDeAccion.FechaCierre;
                            planDeAccion.Observaciones = actividadPlanDeAccion.Observaciones;
                            db1.Tbl_Actividad_Plan_Accion.Add(planDeAccion);
                        }
                    }
                    db1.SaveChanges();
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha editado la actividad del módulo evaluación: {0}, {1}", DateTime.Now, actividadPlanDeAccion.Actividad), new Exception());
                    return true;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error editando la actividad del módulo evaluación: {0}, {1}. Error: {2}", DateTime.Now, actividadPlanDeAccion.Actividad, ex.StackTrace), ex);
                return false;
            }
        }
        /// <summary>
        /// Edita la actividad del módulo acciones preventivas y correctivas
        /// </summary>
        /// <param name="actividadPlanDeAccion"></param>
        /// <returns>bool</returns>
        public bool EditarActividadAccion(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    ActividadAccion actividadAccion = new ActividadAccion();
                    actividadAccion = db1.Tbl_ActividadAccion.Where(x => x.Pk_Id_Actividad == actividadPlanDeAccion.Fk_Id_Actividad).FirstOrDefault();
                    actividadAccion.Actividad = actividadPlanDeAccion.Actividad;
                    actividadAccion.Responsable = actividadPlanDeAccion.Responsable;
                    // Se cierra la actividad en el módulo 1 Abierto - 0 Cerrado
                    if (actividadPlanDeAccion.FechaCierre.Date > DateTime.MinValue.Date)
                        actividadAccion.Estado = 0;
                    else
                        actividadAccion.Estado = 1;
                    db1.Entry(actividadAccion).State = EntityState.Modified;
                    if (actividadPlanDeAccion.FechaCierre.Date > DateTime.MinValue.Date)
                    {
                        ActividadPlanDeAccion planDeAccion = (from actividad in db1.Tbl_Actividad_Plan_Accion
                                                              where actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == actividad.Fk_Id_ModuloPlanAccion
                                                                           & actividadPlanDeAccion.Fk_Plan_Inspección == actividad.Fk_Plan_Inspección
                                                                           & actividadPlanDeAccion.Fk_Id_Actividad == actividad.Fk_Id_Actividad
                                                              select actividad).FirstOrDefault();
                        if (planDeAccion != null)
                        {
                            planDeAccion.FechaCierre = actividadPlanDeAccion.FechaCierre;
                            planDeAccion.Observaciones = actividadPlanDeAccion.Observaciones;
                            db1.Entry(planDeAccion).State = EntityState.Modified;
                        }
                        else
                        {
                            planDeAccion = new ActividadPlanDeAccion();
                            planDeAccion.Fk_Id_ModuloPlanAccion = actividadPlanDeAccion.Fk_Id_ModuloPlanAccion;
                            planDeAccion.Fk_Plan_Inspección = actividadPlanDeAccion.Fk_Plan_Inspección;
                            planDeAccion.Fk_Id_Actividad = actividadPlanDeAccion.Fk_Id_Actividad;
                            planDeAccion.FechaCierre = actividadPlanDeAccion.FechaCierre;
                            planDeAccion.Observaciones = actividadPlanDeAccion.Observaciones;
                            db1.Tbl_Actividad_Plan_Accion.Add(planDeAccion);
                        }
                    }
                    db1.SaveChanges();
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha editado la actividad del módulo acción: {0}, {1}", DateTime.Now, actividadPlanDeAccion.Actividad), new Exception());
                    return true;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error editando la actividad del módulo acción: {0}, {1}. Error: {2}", DateTime.Now, actividadPlanDeAccion.Actividad, ex.StackTrace), ex);
                return false;
            }
        }
        /// <summary>
        /// Edita la actividad del módulo auditoría
        /// </summary>
        /// <param name="actividadPlanDeAccion"></param>
        /// <returns>bool</returns>
        public bool EditarActividadAuditoria(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    ActividadAuditoria actividadAuditoria = new ActividadAuditoria();
                    actividadAuditoria = db1.Tbl_ActividadAuditoria.Where(x => x.Pk_Id_Actividad == actividadPlanDeAccion.Fk_Id_Actividad).FirstOrDefault();
                    actividadAuditoria.Actividad = actividadPlanDeAccion.Actividad;
                    actividadAuditoria.Responsable = actividadPlanDeAccion.Responsable;
                    db1.Entry(actividadAuditoria).State = EntityState.Modified;
                    if (actividadPlanDeAccion.FechaCierre.Date > DateTime.MinValue.Date)
                    {
                        ActividadPlanDeAccion planDeAccion = (from actividad in db1.Tbl_Actividad_Plan_Accion
                                                              where actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == actividad.Fk_Id_ModuloPlanAccion
                                                                           & actividadPlanDeAccion.Fk_Plan_Inspección == actividad.Fk_Plan_Inspección
                                                                           & actividadPlanDeAccion.Fk_Id_Actividad == actividad.Fk_Id_Actividad
                                                              select actividad).FirstOrDefault();
                        if (planDeAccion != null)
                        {
                            planDeAccion.FechaCierre = actividadPlanDeAccion.FechaCierre;
                            planDeAccion.Observaciones = actividadPlanDeAccion.Observaciones;
                            db1.Entry(planDeAccion).State = EntityState.Modified;
                        }
                        else
                        {
                            planDeAccion = new ActividadPlanDeAccion();
                            planDeAccion.Fk_Id_ModuloPlanAccion = actividadPlanDeAccion.Fk_Id_ModuloPlanAccion;
                            planDeAccion.Fk_Plan_Inspección = actividadPlanDeAccion.Fk_Plan_Inspección;
                            planDeAccion.Fk_Id_Actividad = actividadPlanDeAccion.Fk_Id_Actividad;
                            planDeAccion.FechaCierre = actividadPlanDeAccion.FechaCierre;
                            planDeAccion.Observaciones = actividadPlanDeAccion.Observaciones;
                            db1.Tbl_Actividad_Plan_Accion.Add(planDeAccion);
                        }
                    }
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha editado la actividad del módulo auditoría: {0}, {1}", DateTime.Now, actividadPlanDeAccion.Actividad), new Exception());
                    db1.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error editando la actividad del módulo auditoría: {0}, {1}. Error: {2}", DateTime.Now, actividadPlanDeAccion.Actividad, ex.StackTrace), ex);
                return false;
            }
        }
        public bool EditarActividadInspeccion(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    PlanAccionInspeccion planAccionInspeccion = new PlanAccionInspeccion();
                    planAccionInspeccion = db1.Tbl_PlanAccionInspeccion.Where(x => x.Pk_Id_PlanAcccionInspeccion == actividadPlanDeAccion.Fk_Id_Actividad).FirstOrDefault();
                    planAccionInspeccion.Actividad_Plan_Accion = actividadPlanDeAccion.Actividad;
                    planAccionInspeccion.Responsable_Plan_Accion = actividadPlanDeAccion.Responsable;
                    planAccionInspeccion.Fecha_Cierre_Plan = actividadPlanDeAccion.FechaCierre;
                    // Se cierra la actividad en el módulo 1 Abierto - 0 Cerrado
                    if (actividadPlanDeAccion.FechaCierre.Date > DateTime.MinValue.Date)
                        planAccionInspeccion.Estado = 0;
                       
                
                    else
                        planAccionInspeccion.Estado = 1;
                    db1.Entry(planAccionInspeccion).State = EntityState.Modified;
                    if (actividadPlanDeAccion.FechaCierre.Date > DateTime.MinValue.Date)
                    {
                        ActividadPlanDeAccion planDeAccion = (from actividad in db1.Tbl_Actividad_Plan_Accion
                                                              where actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == actividad.Fk_Id_ModuloPlanAccion
                                                                           & actividadPlanDeAccion.Fk_Plan_Inspección == actividad.Fk_Plan_Inspección
                                                                           & actividadPlanDeAccion.Fk_Id_Actividad == actividad.Fk_Id_Actividad
                                                              select actividad).FirstOrDefault();
                        if (planDeAccion != null)
                        {
                            planDeAccion.FechaCierre = actividadPlanDeAccion.FechaCierre;
                            planDeAccion.Observaciones = actividadPlanDeAccion.Observaciones;
                            db1.Entry(planDeAccion).State = EntityState.Modified;
                        }
                        else
                        {
                            planDeAccion = new ActividadPlanDeAccion();
                            planDeAccion.Fk_Id_ModuloPlanAccion = actividadPlanDeAccion.Fk_Id_ModuloPlanAccion;
                            planDeAccion.Fk_Plan_Inspección = actividadPlanDeAccion.Fk_Plan_Inspección;
                            planDeAccion.Fk_Id_Actividad = actividadPlanDeAccion.Fk_Id_Actividad;
                            planDeAccion.FechaCierre = actividadPlanDeAccion.FechaCierre;
                            planDeAccion.Observaciones = actividadPlanDeAccion.Observaciones;
                            db1.Tbl_Actividad_Plan_Accion.Add(planDeAccion);
                        }
                    }
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha editado la actividad del módulo inspecciones: {0}, {1}", DateTime.Now, actividadPlanDeAccion.Actividad), new Exception());
                    db1.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error editando la actividad del módulo inspecciones: {0}, {1}. Error: {2}", DateTime.Now, actividadPlanDeAccion.Actividad, ex.StackTrace), ex);
                return false;
            }
        }
        public bool EditarActividadReportes(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    ActividadesActosInseguros actividadesActosInseguros = new ActividadesActosInseguros();
                    actividadesActosInseguros = db1.Tbl_ActividadesActosInseguros.Where(x => x.PK_ID_ActividadActosInseguros == actividadPlanDeAccion.Fk_Id_Actividad).FirstOrDefault();
                    actividadesActosInseguros.NombreActividad = actividadPlanDeAccion.Actividad;
                    actividadesActosInseguros.ResponsableActividad = actividadPlanDeAccion.Responsable;
                    db1.Entry(actividadesActosInseguros).State = EntityState.Modified;
                    if (actividadPlanDeAccion.FechaCierre.Date > DateTime.MinValue.Date)
                    {
                        ActividadPlanDeAccion planDeAccion = (from actividad in db1.Tbl_Actividad_Plan_Accion
                                                              where actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == actividad.Fk_Id_ModuloPlanAccion
                                                                           & actividadPlanDeAccion.Fk_Plan_Inspección == actividad.Fk_Plan_Inspección
                                                                           & actividadPlanDeAccion.Fk_Id_Actividad == actividad.Fk_Id_Actividad
                                                              select actividad).FirstOrDefault();
                        if (planDeAccion != null)
                        {
                            planDeAccion.FechaCierre = actividadPlanDeAccion.FechaCierre;
                            planDeAccion.Observaciones = actividadPlanDeAccion.Observaciones;
                            db1.Entry(planDeAccion).State = EntityState.Modified;
                        }
                        else
                        {
                            planDeAccion = new ActividadPlanDeAccion();
                            planDeAccion.Fk_Id_ModuloPlanAccion = actividadPlanDeAccion.Fk_Id_ModuloPlanAccion;
                            planDeAccion.Fk_Plan_Inspección = actividadPlanDeAccion.Fk_Plan_Inspección;
                            planDeAccion.Fk_Id_Actividad = actividadPlanDeAccion.Fk_Id_Actividad;
                            planDeAccion.FechaCierre = actividadPlanDeAccion.FechaCierre;
                            planDeAccion.Observaciones = actividadPlanDeAccion.Observaciones;
                            db1.Tbl_Actividad_Plan_Accion.Add(planDeAccion);
                        }
                    }
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha editado la actividad del módulo reportes de actos inseguros: {0}, {1}", DateTime.Now, actividadPlanDeAccion.Actividad), new Exception());
                    db1.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error editando la actividad del módulo reportes de actos inseguros: {0}, {1}. Error: {2}", DateTime.Now, actividadPlanDeAccion.Actividad, ex.StackTrace), ex);
                return false;
            }
        }

        public bool EditarActividadCopasst(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    AccionesActaCopasst actividadesActasCopasst = new AccionesActaCopasst();
                    actividadesActasCopasst = db1.Tbl_AccionesActaCopasst.Where(x => x.Pk_Id_AccionActaCopasst == actividadPlanDeAccion.Fk_Id_Actividad).FirstOrDefault();
                    actividadesActasCopasst.AccionARealizar = actividadPlanDeAccion.Actividad;
                    actividadesActasCopasst.Responsable = actividadPlanDeAccion.Responsable;
                    db1.Entry(actividadesActasCopasst).State = EntityState.Modified;
                    if (actividadPlanDeAccion.FechaCierre.Date > DateTime.MinValue.Date)
                    {
                        ActividadPlanDeAccion planDeAccion = (from actividad in db1.Tbl_Actividad_Plan_Accion
                                                              where actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == actividad.Fk_Id_ModuloPlanAccion
                                                                           & actividadPlanDeAccion.Fk_Plan_Inspección == actividad.Fk_Plan_Inspección
                                                                           & actividadPlanDeAccion.Fk_Id_Actividad == actividad.Fk_Id_Actividad
                                                              select actividad).FirstOrDefault();
                        if (planDeAccion != null)
                        {
                            planDeAccion.FechaCierre = actividadPlanDeAccion.FechaCierre;
                            planDeAccion.Observaciones = actividadPlanDeAccion.Observaciones;
                            db1.Entry(planDeAccion).State = EntityState.Modified;
                        }
                        else
                        {
                            planDeAccion = new ActividadPlanDeAccion();
                            planDeAccion.Fk_Id_ModuloPlanAccion = actividadPlanDeAccion.Fk_Id_ModuloPlanAccion;
                            planDeAccion.Fk_Plan_Inspección = actividadPlanDeAccion.Fk_Plan_Inspección;
                            planDeAccion.Fk_Id_Actividad = actividadPlanDeAccion.Fk_Id_Actividad;
                            planDeAccion.FechaCierre = actividadPlanDeAccion.FechaCierre;
                            planDeAccion.Observaciones = actividadPlanDeAccion.Observaciones;
                            db1.Tbl_Actividad_Plan_Accion.Add(planDeAccion);
                        }
                    }
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha editado la actividad del módulo Coppast: {0}, {1}", DateTime.Now, actividadPlanDeAccion.Actividad), new Exception());
                    db1.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error editando la actividad del módulo Coppast: {0}, {1}. Error: {2}", DateTime.Now, actividadPlanDeAccion.Actividad, ex.StackTrace), ex);
                return false;
            }
        }

        public bool EditarActividadConvivencia(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    AccionesActaConvivencia actividadesActasConvivencia = new AccionesActaConvivencia();
                    actividadesActasConvivencia = db1.Tbl_AccionesActaConvivencia.Where(x => x.Pk_Id_AccionActaConvivencia == actividadPlanDeAccion.Fk_Id_Actividad).FirstOrDefault();
                    actividadesActasConvivencia.AccionARealizar = actividadPlanDeAccion.Actividad;
                    actividadesActasConvivencia.Responsable = actividadPlanDeAccion.Responsable;
                    db1.Entry(actividadesActasConvivencia).State = EntityState.Modified;
                    if (actividadPlanDeAccion.FechaCierre.Date > DateTime.MinValue.Date)
                    {
                        ActividadPlanDeAccion planDeAccion = (from actividad in db1.Tbl_Actividad_Plan_Accion
                                                              where actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == actividad.Fk_Id_ModuloPlanAccion
                                                                           & actividadPlanDeAccion.Fk_Plan_Inspección == actividad.Fk_Plan_Inspección
                                                                           & actividadPlanDeAccion.Fk_Id_Actividad == actividad.Fk_Id_Actividad
                                                              select actividad).FirstOrDefault();
                        if (planDeAccion != null)
                        {
                            planDeAccion.FechaCierre = actividadPlanDeAccion.FechaCierre;
                            planDeAccion.Observaciones = actividadPlanDeAccion.Observaciones;
                            db1.Entry(planDeAccion).State = EntityState.Modified;
                        }
                        else
                        {
                            planDeAccion = new ActividadPlanDeAccion();
                            planDeAccion.Fk_Id_ModuloPlanAccion = actividadPlanDeAccion.Fk_Id_ModuloPlanAccion;
                            planDeAccion.Fk_Plan_Inspección = actividadPlanDeAccion.Fk_Plan_Inspección;
                            planDeAccion.Fk_Id_Actividad = actividadPlanDeAccion.Fk_Id_Actividad;
                            planDeAccion.FechaCierre = actividadPlanDeAccion.FechaCierre;
                            planDeAccion.Observaciones = actividadPlanDeAccion.Observaciones;
                            db1.Tbl_Actividad_Plan_Accion.Add(planDeAccion);
                        }
                    }
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha editado la actividad del módulo Convivencia laboral: {0}, {1}", DateTime.Now, actividadPlanDeAccion.Actividad), new Exception());
                    db1.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error editando la actividad del módulo Convivencia laboral: {0}, {1}. Error: {2}", DateTime.Now, actividadPlanDeAccion.Actividad, ex.StackTrace), ex);
                return false;
            }
        }


        public bool EditarActividadRevisionSGSST(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    PlanAccionRevision actividadesRevisionSGSST = new PlanAccionRevision();
                    actividadesRevisionSGSST = db1.Tbl_PlanAccionRevision.Where(x => x.PK_Id_PlanAccion == actividadPlanDeAccion.Fk_Id_Actividad).FirstOrDefault();
                    actividadesRevisionSGSST.Actividad = actividadPlanDeAccion.Actividad;
                    actividadesRevisionSGSST.Responsable = actividadPlanDeAccion.Responsable;
                    db1.Entry(actividadesRevisionSGSST).State = EntityState.Modified;
                    if (actividadPlanDeAccion.FechaCierre.Date > DateTime.MinValue.Date)
                    {
                        ActividadPlanDeAccion planDeAccion = (from actividad in db1.Tbl_Actividad_Plan_Accion
                                                              where actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == actividad.Fk_Id_ModuloPlanAccion
                                                                           & actividadPlanDeAccion.Fk_Plan_Inspección == actividad.Fk_Plan_Inspección
                                                                           & actividadPlanDeAccion.Fk_Id_Actividad == actividad.Fk_Id_Actividad
                                                              select actividad).FirstOrDefault();
                        if (planDeAccion != null)
                        {
                            planDeAccion.FechaCierre = actividadPlanDeAccion.FechaCierre;
                            planDeAccion.Observaciones = actividadPlanDeAccion.Observaciones;
                            db1.Entry(planDeAccion).State = EntityState.Modified;
                        }
                        else
                        {
                            planDeAccion = new ActividadPlanDeAccion();
                            planDeAccion.Fk_Id_ModuloPlanAccion = actividadPlanDeAccion.Fk_Id_ModuloPlanAccion;
                            planDeAccion.Fk_Plan_Inspección = actividadPlanDeAccion.Fk_Plan_Inspección;
                            planDeAccion.Fk_Id_Actividad = actividadPlanDeAccion.Fk_Id_Actividad;
                            planDeAccion.FechaCierre = actividadPlanDeAccion.FechaCierre;
                            planDeAccion.Observaciones = actividadPlanDeAccion.Observaciones;
                            db1.Tbl_Actividad_Plan_Accion.Add(planDeAccion);
                        }
                    }
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha editado la actividad del módulo RevisionSGSST: {0}, {1}", DateTime.Now, actividadPlanDeAccion.Actividad), new Exception());
                    db1.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error editando la actividad del módulo RevisionSGSST: {0}, {1}. Error: {2}", DateTime.Now, actividadPlanDeAccion.Actividad, ex.StackTrace), ex);
                return false;
            }
        }

        /// <summary>
        /// Adicciona la actividad al plan de acción
        /// </summary>
        /// <param name="actividadPlanDeAccion"></param>
        /// <returns>bool</returns>
        public bool AdicionarActividad(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            bool adicionado = false;
            try
            {
                if (actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == 1)
                {
                    adicionado = AdicionarActividadEvaluacion(actividadPlanDeAccion);
                }
                else if (actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == 2)
                {
                    adicionado = AdicionarActividadAccion(actividadPlanDeAccion);
                }
                else if (actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == 3)
                {
                    adicionado = AdicionarActividadAuditoria(actividadPlanDeAccion);
                }
                else if (actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == 4)
                {
                    adicionado = AdicionarActividadInspecciones(actividadPlanDeAccion);
                }
                else if (actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == 5)
                {
                    adicionado = AdicionarActividadReportes(actividadPlanDeAccion);
                }
                else if (actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == 6)
                {
                    adicionado = AdicionarActividadCopasst(actividadPlanDeAccion);
                }
                else if (actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == 7)
                {
                    adicionado = AdicionarActividadConvivencia(actividadPlanDeAccion);
                }
                else if (actividadPlanDeAccion.Fk_Id_ModuloPlanAccion == 8)
                {
                    adicionado = AdicionarActividadRevisionSGSST(actividadPlanDeAccion);
                }

                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha adicionado la actividad: {0}, {1}", DateTime.Now, actividadPlanDeAccion.Actividad), new Exception());
                return adicionado;
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error adicionando la actividad: {0}, {1}. Error: {2}", DateTime.Now, actividadPlanDeAccion.Actividad, ex.StackTrace), ex);
                return false;
            }

        }
        /// <summary>
        /// Adicciona la actividad al módulo evaluación
        /// </summary>
        /// <param name="actividadPlanDeAccion"></param>
        /// <returns>bool</returns>
        public bool AdicionarActividadEvaluacion(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    ActividadEvaluacion actividadEvaluacion = new ActividadEvaluacion();
                    actividadEvaluacion.Descripcion = actividadPlanDeAccion.Actividad;
                    actividadEvaluacion.Responsable = actividadPlanDeAccion.Responsable;
                    actividadEvaluacion.FechaFin = actividadPlanDeAccion.FechaFinalizacion;
                    db1.Tbl_Actividades_Evaluacion.Add(actividadEvaluacion);
                    db1.SaveChanges();
                    Actividad_Criterio actividad_Criterio = new Actividad_Criterio();
                    actividad_Criterio.Fk_Id_Actividad = actividadEvaluacion.Pk_Id_Actividad;
                    actividad_Criterio.Fk_Id_Eval_Estandar_Minimo = actividadPlanDeAccion.Fk_Plan_Inspección;
                    db1.Tbl_Actividades_Criterio.Add(actividad_Criterio);
                    actividadPlanDeAccion.Fk_Id_Actividad = actividadEvaluacion.Pk_Id_Actividad;
                    if (actividadPlanDeAccion.FechaCierre.Date > DateTime.MinValue.Date)
                    {
                        ActividadPlanDeAccion planDeAccion = new ActividadPlanDeAccion();
                        planDeAccion.Fk_Id_ModuloPlanAccion = actividadPlanDeAccion.Fk_Id_ModuloPlanAccion;
                        planDeAccion.Fk_Plan_Inspección = actividadPlanDeAccion.Fk_Plan_Inspección;
                        planDeAccion.Fk_Id_Actividad = actividadPlanDeAccion.Fk_Id_Actividad;
                        planDeAccion.FechaCierre = actividadPlanDeAccion.FechaCierre;
                        planDeAccion.Observaciones = actividadPlanDeAccion.Observaciones;
                        db1.Tbl_Actividad_Plan_Accion.Add(planDeAccion);
                    }
                    db1.SaveChanges();
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha adicionado la actividad al módulo evaluación: {0}, {1}", DateTime.Now, actividadPlanDeAccion.Actividad), new Exception());
                    return true;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error adicionando la actividad al módulo evaluación: {0}, {1}. Error: {2}", DateTime.Now, actividadPlanDeAccion.Actividad, ex.StackTrace), ex);
                return false;
            }
        }
        /// <summary>
        /// Adicciona la actividad al módulo acciones preventivas y correctivas
        /// </summary>
        /// <param name="actividadPlanDeAccion"></param>
        /// <returns>bool</returns>
        public bool AdicionarActividadAccion(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    ActividadAccion actividadAccion = new ActividadAccion();
                    actividadAccion.Actividad = actividadPlanDeAccion.Actividad;
                    actividadAccion.Responsable = actividadPlanDeAccion.Responsable;
                    actividadAccion.FechaFinalizacion = actividadPlanDeAccion.FechaFinalizacion;
                    actividadAccion.Fk_Id_Accion = actividadPlanDeAccion.Fk_Plan_Inspección;
                    // Se cierra la actividad en el módulo 1 Abierto - 0 Cerrado
                    if (actividadPlanDeAccion.FechaCierre.Date > DateTime.MinValue.Date)
                        actividadAccion.Estado = 0;
                    else
                        actividadAccion.Estado = 1;
                    db1.Tbl_ActividadAccion.Add(actividadAccion);
                    db1.SaveChanges();
                    actividadPlanDeAccion.Fk_Id_Actividad = actividadAccion.Pk_Id_Actividad;
                    if (actividadPlanDeAccion.FechaCierre.Date > DateTime.MinValue.Date)
                    {
                        ActividadPlanDeAccion planDeAccion = new ActividadPlanDeAccion();
                        planDeAccion.Fk_Id_ModuloPlanAccion = actividadPlanDeAccion.Fk_Id_ModuloPlanAccion;
                        planDeAccion.Fk_Plan_Inspección = actividadPlanDeAccion.Fk_Plan_Inspección;
                        planDeAccion.Fk_Id_Actividad = actividadPlanDeAccion.Fk_Id_Actividad;
                        planDeAccion.FechaCierre = actividadPlanDeAccion.FechaCierre;
                        planDeAccion.Observaciones = actividadPlanDeAccion.Observaciones;
                        db1.Tbl_Actividad_Plan_Accion.Add(planDeAccion);
                    }
                    db1.SaveChanges();
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha adicionado la actividad al módulo acción: {0}, {1}", DateTime.Now, actividadPlanDeAccion.Actividad), new Exception());
                    return true;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error adicionando la actividad al módulo acción: {0}, {1}. Error: {2}", DateTime.Now, actividadPlanDeAccion.Actividad, ex.StackTrace), ex);
                return false;
            }
        }
        /// <summary>
        /// Adicciona la actividad al módulo auditorías
        /// </summary>
        /// <param name="actividadPlanDeAccion"></param>
        /// <returns>bool</returns>
        public bool AdicionarActividadAuditoria(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    ActividadAuditoria actividadAuditoria = new ActividadAuditoria();
                    actividadAuditoria.Actividad = actividadPlanDeAccion.Actividad;
                    actividadAuditoria.Responsable = actividadPlanDeAccion.Responsable;
                    actividadAuditoria.FechaFinalizacion = actividadPlanDeAccion.FechaFinalizacion;
                    actividadAuditoria.Fk_Id_Auditoria = actividadPlanDeAccion.Fk_Plan_Inspección;
                    db1.Tbl_ActividadAuditoria.Add(actividadAuditoria);
                    db1.SaveChanges();
                    actividadPlanDeAccion.Fk_Id_Actividad = actividadAuditoria.Pk_Id_Actividad;
                    if (actividadPlanDeAccion.FechaCierre.Date > DateTime.MinValue.Date)
                    {
                        ActividadPlanDeAccion planDeAccion = new ActividadPlanDeAccion();
                        planDeAccion.Fk_Id_ModuloPlanAccion = actividadPlanDeAccion.Fk_Id_ModuloPlanAccion;
                        planDeAccion.Fk_Plan_Inspección = actividadPlanDeAccion.Fk_Plan_Inspección;
                        planDeAccion.Fk_Id_Actividad = actividadPlanDeAccion.Fk_Id_Actividad;
                        planDeAccion.FechaCierre = actividadPlanDeAccion.FechaCierre;
                        planDeAccion.Observaciones = actividadPlanDeAccion.Observaciones;
                        db1.Tbl_Actividad_Plan_Accion.Add(planDeAccion);
                    }
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha adicionado la actividad al módulo auditoría: {0}, {1}", DateTime.Now, actividadPlanDeAccion.Actividad), new Exception());
                    db1.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error adicionando la actividad al módulo auditoría: {0}, {1}. Error: {2}", DateTime.Now, actividadPlanDeAccion.Actividad, ex.StackTrace), ex);
                return false;
            }
        }
        /// <summary>
        /// Adicciona la actividad al módulo inspeccciones
        /// </summary>
        /// <param name="actividadPlanDeAccion"></param>
        /// <returns>bool</returns>
        public bool AdicionarActividadInspecciones(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    PlanAccionInspeccion planAccionInspeccion = new PlanAccionInspeccion();
                    planAccionInspeccion.Actividad_Plan_Accion = actividadPlanDeAccion.Actividad;
                    planAccionInspeccion.Responsable_Plan_Accion = actividadPlanDeAccion.Responsable;
                    planAccionInspeccion.Fecha_Fin_Plan_Accion = actividadPlanDeAccion.FechaCierre;
                    // planAccionInspeccion.Fk_Id_Auditoria = actividadPlanDeAccion.Fk_Plan_Inspección;
                    // Se cierra la actividad en el módulo 1 Abierto - 0 Cerrado
                    if (actividadPlanDeAccion.FechaCierre.Date > DateTime.MinValue.Date)
                        planAccionInspeccion.Estado = 0;
                    else
                        planAccionInspeccion.Estado = 1;
                    db1.Tbl_PlanAccionInspeccion.Add(planAccionInspeccion);
                    db1.SaveChanges();
                    actividadPlanDeAccion.Fk_Id_Actividad = planAccionInspeccion.Pk_Id_PlanAcccionInspeccion;
                    if (actividadPlanDeAccion.FechaCierre.Date > DateTime.MinValue.Date)
                    {
                        ActividadPlanDeAccion planDeAccion = new ActividadPlanDeAccion();
                        planDeAccion.Fk_Id_ModuloPlanAccion = actividadPlanDeAccion.Fk_Id_ModuloPlanAccion;
                        planDeAccion.Fk_Plan_Inspección = actividadPlanDeAccion.Fk_Plan_Inspección;
                        planDeAccion.Fk_Id_Actividad = actividadPlanDeAccion.Fk_Id_Actividad;
                        planDeAccion.FechaCierre = actividadPlanDeAccion.FechaCierre;
                        planDeAccion.Observaciones = actividadPlanDeAccion.Observaciones;
                        db1.Tbl_Actividad_Plan_Accion.Add(planDeAccion);
                    }
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha adicionado la actividad al módulo inspecciones: {0}, {1}", DateTime.Now, actividadPlanDeAccion.Actividad), new Exception());
                    db1.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error adicionando la actividad al módulo inspecciones: {0}, {1}. Error: {2}", DateTime.Now, actividadPlanDeAccion.Actividad, ex.StackTrace), ex);
                return false;
            }
        }

        public bool AdicionarActividadReportes(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    ActividadesActosInseguros actividadesActosInseguros = new ActividadesActosInseguros();

                    //int fk_Plan_Inspección = (from act in db1.Tbl_ActividadesActosInseguros
                    //                          join repo in db1.Tbl_Reportes on act.FK_Id_Reportes equals repo.PK_Id_Reportes
                    //                          join emp in db1.Tbl_Empresa on repo.FK_NitEmpresa equals emp.Nit_Empresa

                    //                          where repo.ConsecutivoReporte == actividadPlanDeAccion.Fk_Plan_Inspección

                    //                          select act.FK_Id_Reportes).FirstOrDefault();

                    //actividadPlanDeAccion.Fk_Plan_Inspección = fk_Plan_Inspección;

                    actividadesActosInseguros.NombreActividad = actividadPlanDeAccion.Actividad;
                    actividadesActosInseguros.ResponsableActividad = actividadPlanDeAccion.Responsable;
                    actividadesActosInseguros.FechaEjecucion = actividadPlanDeAccion.FechaFinalizacion;
                    actividadesActosInseguros.FK_Id_Reportes = actividadPlanDeAccion.Fk_Plan_Inspección;


                    db1.Tbl_ActividadesActosInseguros.Add(actividadesActosInseguros);
                    db1.SaveChanges();
                    actividadPlanDeAccion.Fk_Id_Actividad = actividadesActosInseguros.PK_ID_ActividadActosInseguros;
                    if (actividadPlanDeAccion.FechaCierre.Date > DateTime.MinValue.Date)
                    {
                        ActividadPlanDeAccion planDeAccion = new ActividadPlanDeAccion();
                        planDeAccion.Fk_Id_ModuloPlanAccion = actividadPlanDeAccion.Fk_Id_ModuloPlanAccion;
                        planDeAccion.Fk_Plan_Inspección = actividadPlanDeAccion.Fk_Plan_Inspección;
                        planDeAccion.Fk_Id_Actividad = actividadPlanDeAccion.Fk_Id_Actividad;
                        planDeAccion.FechaCierre = actividadPlanDeAccion.FechaCierre;
                        planDeAccion.Observaciones = actividadPlanDeAccion.Observaciones;
                        db1.Tbl_Actividad_Plan_Accion.Add(planDeAccion);
                    }
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha adicionado la actividad al módulo reportes de actos inseguros: {0}, {1}", DateTime.Now, actividadPlanDeAccion.Actividad), new Exception());
                    db1.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error adicionando la actividad al módulo reportes de actos inseguros: {0}, {1}. Error: {2}", DateTime.Now, actividadPlanDeAccion.Actividad, ex.StackTrace), ex);
                return false;
            }
        }


        public bool AdicionarActividadCopasst(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    AccionesActaCopasst actividadesActasCopasst = new AccionesActaCopasst();


                    actividadesActasCopasst.AccionARealizar = actividadPlanDeAccion.Actividad;
                    actividadesActasCopasst.Responsable = actividadPlanDeAccion.Responsable;
                    actividadesActasCopasst.FechaProbable = actividadPlanDeAccion.FechaFinalizacion;
                    actividadesActasCopasst.Fk_Id_Acta = actividadPlanDeAccion.Fk_Plan_Inspección;


                    db1.Tbl_AccionesActaCopasst.Add(actividadesActasCopasst);
                    db1.SaveChanges();
                    actividadPlanDeAccion.Fk_Id_Actividad = actividadesActasCopasst.Pk_Id_AccionActaCopasst;
                    if (actividadPlanDeAccion.FechaCierre.Date > DateTime.MinValue.Date)
                    {
                        ActividadPlanDeAccion planDeAccion = new ActividadPlanDeAccion();
                        planDeAccion.Fk_Id_ModuloPlanAccion = actividadPlanDeAccion.Fk_Id_ModuloPlanAccion;
                        planDeAccion.Fk_Plan_Inspección = actividadPlanDeAccion.Fk_Plan_Inspección;
                        planDeAccion.Fk_Id_Actividad = actividadPlanDeAccion.Fk_Id_Actividad;
                        planDeAccion.FechaCierre = actividadPlanDeAccion.FechaCierre;
                        planDeAccion.Observaciones = actividadPlanDeAccion.Observaciones;
                        db1.Tbl_Actividad_Plan_Accion.Add(planDeAccion);
                    }
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha adicionado la actividad al módulo Coppast: {0}, {1}", DateTime.Now, actividadPlanDeAccion.Actividad), new Exception());
                    db1.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error adicionando la actividad al módulo Coppast: {0}, {1}. Error: {2}", DateTime.Now, actividadPlanDeAccion.Actividad, ex.StackTrace), ex);
                return false;
            }
        }

        public bool AdicionarActividadConvivencia(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    AccionesActaConvivencia actividadesActosConvivencia = new AccionesActaConvivencia();


                    actividadesActosConvivencia.AccionARealizar = actividadPlanDeAccion.Actividad;
                    actividadesActosConvivencia.Responsable = actividadPlanDeAccion.Responsable;
                    actividadesActosConvivencia.FechaProbable = actividadPlanDeAccion.FechaFinalizacion;
                    actividadesActosConvivencia.Fk_Id_Acta = actividadPlanDeAccion.Fk_Plan_Inspección;


                    db1.Tbl_AccionesActaConvivencia.Add(actividadesActosConvivencia);
                    db1.SaveChanges();
                    actividadPlanDeAccion.Fk_Id_Actividad = actividadesActosConvivencia.Pk_Id_AccionActaConvivencia;
                    if (actividadPlanDeAccion.FechaCierre.Date > DateTime.MinValue.Date)
                    {
                        ActividadPlanDeAccion planDeAccion = new ActividadPlanDeAccion();
                        planDeAccion.Fk_Id_ModuloPlanAccion = actividadPlanDeAccion.Fk_Id_ModuloPlanAccion;
                        planDeAccion.Fk_Plan_Inspección = actividadPlanDeAccion.Fk_Plan_Inspección;
                        planDeAccion.Fk_Id_Actividad = actividadPlanDeAccion.Fk_Id_Actividad;
                        planDeAccion.FechaCierre = actividadPlanDeAccion.FechaCierre;
                        planDeAccion.Observaciones = actividadPlanDeAccion.Observaciones;
                        db1.Tbl_Actividad_Plan_Accion.Add(planDeAccion);
                    }
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha adicionado la actividad al módulo Convivencia Laboral: {0}, {1}", DateTime.Now, actividadPlanDeAccion.Actividad), new Exception());
                    db1.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error adicionando la actividad al módulo Convivencia laboral: {0}, {1}. Error: {2}", DateTime.Now, actividadPlanDeAccion.Actividad, ex.StackTrace), ex);
                return false;
            }
        }


        public bool AdicionarActividadRevisionSGSST(EDActividadPlanDeAccion actividadPlanDeAccion)
        {
            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    PlanAccionRevision actividadesRevisionSGSST = new PlanAccionRevision();

                    actividadesRevisionSGSST.Actividad = actividadPlanDeAccion.Actividad;
                    actividadesRevisionSGSST.Responsable = actividadPlanDeAccion.Responsable;
                    actividadesRevisionSGSST.Fecha = actividadPlanDeAccion.FechaFinalizacion;
                    actividadesRevisionSGSST.FK_Acta = actividadPlanDeAccion.Fk_Plan_Inspección;


                    db1.Tbl_PlanAccionRevision.Add(actividadesRevisionSGSST);
                    db1.SaveChanges();
                    actividadPlanDeAccion.Fk_Id_Actividad = actividadesRevisionSGSST.PK_Id_PlanAccion;
                    if (actividadPlanDeAccion.FechaCierre.Date > DateTime.MinValue.Date)
                    {
                        ActividadPlanDeAccion planDeAccion = new ActividadPlanDeAccion();
                        planDeAccion.Fk_Id_ModuloPlanAccion = actividadPlanDeAccion.Fk_Id_ModuloPlanAccion;
                        planDeAccion.Fk_Plan_Inspección = actividadPlanDeAccion.Fk_Plan_Inspección;
                        planDeAccion.Fk_Id_Actividad = actividadPlanDeAccion.Fk_Id_Actividad;
                        planDeAccion.FechaCierre = actividadPlanDeAccion.FechaCierre;
                        planDeAccion.Observaciones = actividadPlanDeAccion.Observaciones;
                        db1.Tbl_Actividad_Plan_Accion.Add(planDeAccion);
                    }
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha adicionado la actividad al módulo RevisionSGSST: {0}, {1}", DateTime.Now, actividadPlanDeAccion.Actividad), new Exception());
                    db1.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error adicionando la actividad al módulo RevisionSGSST: {0}, {1}. Error: {2}", DateTime.Now, actividadPlanDeAccion.Actividad, ex.StackTrace), ex);
                return false;
            }
        }
        /// <summary>
        /// Consulta los planes de acción de acuerdo al módulo y rango de fechas de finalización
        /// </summary>
        /// <param name="nit",name="Pk_Id_ModuloPlanAccion",name="fechaInicial",name="fechaFinal"></param>
        /// <returns>List<EDPlanDeAccion></returns>
        public List<EDPlanDeAccion> ConsultarListaPlanDeAccion(int nit, int Pk_Id_ModuloPlanAccion, string fechaInicial, string fechaFinal)
        {
            try
            {

                List<EDPlanDeAccion> ListaPlanDeAccion = new List<EDPlanDeAccion>();
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    var modulo = (from s in db1.Tbl_Modulos_Plan_Accion
                                  where s.Pk_Id_ModuloPlanAccion == Pk_Id_ModuloPlanAccion
                                  select s).FirstOrDefault();
                    int empresa = (from s in db1.Tbl_Empresa
                                   where s.Nit_Empresa == nit.ToString()
                                   select s.Pk_Id_Empresa).FirstOrDefault();
                    List<EDPlanDeAccion> ListaPlan = new List<EDPlanDeAccion>();
                    EDPlanDeAccion PlanDeAccion = new EDPlanDeAccion();
                    PlanDeAccion.EDActividadPlanDeAccion = new List<EDActividadPlanDeAccion>();
                    DateTime inicial = Convert.ToDateTime(fechaInicial);
                    DateTime final = Convert.ToDateTime(fechaFinal);
                    if (Pk_Id_ModuloPlanAccion.Equals(1))
                    {
                        PlanDeAccion = ConsultarListaActividadEvaluacion(empresa, Pk_Id_ModuloPlanAccion, nit, inicial, final);
                        if (PlanDeAccion != null)
                        {
                            PlanDeAccion.Origen = modulo.Modulo;
                            ListaPlanDeAccion.Add(PlanDeAccion);
                        }
                    }
                    else if (Pk_Id_ModuloPlanAccion.Equals(2))
                    {
                        ListaPlan = ConsultarListaActividadAccion(empresa, Pk_Id_ModuloPlanAccion, modulo.Modulo, inicial, final);
                        if (ListaPlan != null)
                            ListaPlanDeAccion.AddRange(ListaPlan);
                    }
                    else if (Pk_Id_ModuloPlanAccion.Equals(3))
                    {
                        ListaPlan = ConsultarListaActividadAuditoria(empresa, Pk_Id_ModuloPlanAccion, modulo.Modulo, inicial, final);
                        if (ListaPlan != null)
                            ListaPlanDeAccion.AddRange(ListaPlan);
                    }
                    else if (Pk_Id_ModuloPlanAccion.Equals(4))
                    {
                        ListaPlan = ConsultarListaActividadInspecciones(empresa, Pk_Id_ModuloPlanAccion, modulo.Modulo, inicial, final);
                        if (ListaPlan != null)
                            ListaPlanDeAccion.AddRange(ListaPlan);
                    }
                    else if (Pk_Id_ModuloPlanAccion.Equals(5))
                    {
                        ListaPlan = ConsultarListaActividadReportes(empresa, Pk_Id_ModuloPlanAccion, modulo.Modulo, inicial, final);
                        if (ListaPlan != null)
                            ListaPlanDeAccion.AddRange(ListaPlan);
                    }

                    else if (Pk_Id_ModuloPlanAccion.Equals(5))
                    {
                        ListaPlan = ConsultarListaActividadReportes(empresa, Pk_Id_ModuloPlanAccion, modulo.Modulo, inicial, final);
                        if (ListaPlan != null)
                            ListaPlanDeAccion.AddRange(ListaPlan);
                    }
                    else if (Pk_Id_ModuloPlanAccion.Equals(6))
                    {
                        ListaPlan = ConsultarListaActividadCopasst(empresa, Pk_Id_ModuloPlanAccion, modulo.Modulo, inicial, final);
                        if (ListaPlan != null)
                            ListaPlanDeAccion.AddRange(ListaPlan);
                    }
                    else if (Pk_Id_ModuloPlanAccion.Equals(7))
                    {
                        ListaPlan = ConsultarListaActividaConvivencia(empresa, Pk_Id_ModuloPlanAccion, modulo.Modulo, inicial, final);
                        if (ListaPlan != null)
                            ListaPlanDeAccion.AddRange(ListaPlan);
                    }
                    else if (Pk_Id_ModuloPlanAccion.Equals(8))
                    {
                        ListaPlan = ConsultarListaActividaRevisionSGSST(empresa, Pk_Id_ModuloPlanAccion, modulo.Modulo, inicial, final);
                        if (ListaPlan != null)
                            ListaPlanDeAccion.AddRange(ListaPlan);
                    }

                }
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha consultado las actividades: {0}, {1},{2},{3}", DateTime.Now, Pk_Id_ModuloPlanAccion, fechaInicial, fechaFinal), new Exception());
                return ListaPlanDeAccion;
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error consultando las actividades: {0}, {1},{2},{3}. Error: {2}", DateTime.Now, Pk_Id_ModuloPlanAccion, fechaInicial, fechaFinal, ex.StackTrace), ex);
                return null;
            }
        }
        /// <summary>
        /// Consulta los planes de acción en el módulo evaluación de acuerdo rango de fechas de finalización
        /// </summary>
        /// <param name="nit",name="Pk_Id_ModuloPlanAccion",name="fechaInicial",name="fechaFinal"></param>
        /// <returns>EDPlanDeAccion</returns>
        public EDPlanDeAccion ConsultarListaActividadEvaluacion(int empresa, int Pk_Id_ModuloPlanAccion, int nit, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                EDPlanDeAccion PlanDeAccion = new EDPlanDeAccion();
                using (SG_SSTContext db1 = new SG_SSTContext())
                {

                    numeroActividadInsp = false;
                    if (fechaFinal.Year == 1 && Pk_Id_ModuloPlanAccion != null)
                    {


                        Empresa_Evaluar empresaEvaluar = (from e in db1.Tbl_Empresas_Evaluar
                                                          where e.Fk_Id_Empresa == empresa
                                                          select e).FirstOrDefault();
                        List<EDActividadPlanDeAccion> Actividades = new List<EDActividadPlanDeAccion>();
                        Actividades = (from eval in db1.Tbl_Evaluacion_Estandares_Minimos
                                       join ac in db1.Tbl_Actividades_Criterio on eval.Pk_Id_Eval_Estandar_Minimo equals ac.Fk_Id_Eval_Estandar_Minimo
                                       join ae in db1.Tbl_Actividades_Evaluacion on ac.Fk_Id_Actividad equals ae.Pk_Id_Actividad
                                       where eval.Fk_Id_Empresa_Evaluar == empresaEvaluar.Pk_Id_Empresa_Evaluar
                                       //  && ae.FechaFin >= fechaInicial && ae.FechaFin <= fechaFinal
                                       select new EDActividadPlanDeAccion
                                       {
                                           Fk_Id_ModuloPlanAccion = Pk_Id_ModuloPlanAccion,
                                           Fk_Plan_Inspección = eval.Pk_Id_Eval_Estandar_Minimo,
                                           Fk_Id_Actividad = ae.Pk_Id_Actividad,
                                           Actividad = ae.Descripcion,
                                           Responsable = ae.Responsable,
                                           actividadReporte="NA",
                                           FechaFinalizacion = ae.FechaFin,
                                          // fechaEvaluacion = empresaEvaluar.Fecha_Diligencia_Eval_EstMin.Value,
                                           Observaciones = null

                                       }).ToList();

                        if (Actividades.Count() > 0)
                        {
                            PlanDeAccion.Pk_Id_PlanDeAccion = Pk_Id_ModuloPlanAccion;
                            PlanDeAccion.Estado = consultarEstado(Actividades);
                            PlanDeAccion.EDActividadPlanDeAccion = Actividades;
                            PlanDeAccion.cantidadActividades = Actividades.Count();
                        }
                        else
                            return null;
                        var log = new RegistraLog();
                        log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha consultado las actividades del módulo evaluación: {0}, {1},{2},{3}", DateTime.Now, Pk_Id_ModuloPlanAccion, fechaInicial, fechaFinal), new Exception());
                        return PlanDeAccion;
                    }

                    if (fechaFinal.Year != 1 && Pk_Id_ModuloPlanAccion != null)
                    {
                        Empresa_Evaluar empresaEvaluar = (from e in db1.Tbl_Empresas_Evaluar
                                                          where e.Fk_Id_Empresa == empresa
                                                          select e).FirstOrDefault();
                        List<EDActividadPlanDeAccion> Actividades = new List<EDActividadPlanDeAccion>();
                        Actividades = (from eval in db1.Tbl_Evaluacion_Estandares_Minimos
                                       join ac in db1.Tbl_Actividades_Criterio on eval.Pk_Id_Eval_Estandar_Minimo equals ac.Fk_Id_Eval_Estandar_Minimo
                                       join ae in db1.Tbl_Actividades_Evaluacion on ac.Fk_Id_Actividad equals ae.Pk_Id_Actividad
                                       where eval.Fk_Id_Empresa_Evaluar == empresaEvaluar.Pk_Id_Empresa_Evaluar

                                             && DbFunctions.TruncateTime(ae.FechaFin) >= DbFunctions.TruncateTime(fechaInicial)
                                             && DbFunctions.TruncateTime(ae.FechaFin) <= DbFunctions.TruncateTime(fechaFinal)

                                       select new EDActividadPlanDeAccion
                                       {
                                           Fk_Id_ModuloPlanAccion = Pk_Id_ModuloPlanAccion,
                                           Fk_Plan_Inspección = eval.Pk_Id_Eval_Estandar_Minimo,
                                           Fk_Id_Actividad = ae.Pk_Id_Actividad,
                                           Actividad = ae.Descripcion,
                                           Responsable = ae.Responsable,
                                           FechaFinalizacion = ae.FechaFin,
                                       //    fechaEvaluacion = empresaEvaluar.Fecha_Diligencia_Eval_EstMin.Value,
                                           Observaciones = null,
                                           actividadReporte="NA"

                                       }).ToList();

                        if (Actividades.Count() > 0)
                        {
                            PlanDeAccion.Pk_Id_PlanDeAccion = Pk_Id_ModuloPlanAccion;
                            PlanDeAccion.Estado = consultarEstado(Actividades);
                            PlanDeAccion.EDActividadPlanDeAccion = Actividades;
                            PlanDeAccion.cantidadActividades = Actividades.Count();
                        }
                        else
                            return null;
                        var log = new RegistraLog();
                        log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha consultado las actividades del módulo evaluación: {0}, {1},{2},{3}", DateTime.Now, Pk_Id_ModuloPlanAccion, fechaInicial, fechaFinal), new Exception());
                        return PlanDeAccion;
                    }

                    return PlanDeAccion;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error consultando las actividades del módulo evaluación: {0}, {1},{2},{3}. Error: {2}", DateTime.Now, Pk_Id_ModuloPlanAccion, fechaInicial, fechaFinal, ex.StackTrace), ex);
                return null;
            }
        }
        /// <summary>
        /// Consulta los planes de acción en el módulo acciones correctivas y preventivas de acuerdo rango de fechas de finalización
        /// </summary>
        /// <param name="nit",name="Pk_Id_ModuloPlanAccion",name="fechaInicial",name="fechaFinal"></param>
        /// <returns>EDPlanDeAccion</returns>
        public List<EDPlanDeAccion> ConsultarListaActividadAccion(int empresa, int Pk_Id_ModuloPlanAccion, string Modulo, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                numeroActividadInsp = false;
                List<EDPlanDeAccion> ListaPlanDeAccion = new List<EDPlanDeAccion>();
                EDPlanDeAccion PlanDeAccion = new EDPlanDeAccion();
                using (SG_SSTContext db1 = new SG_SSTContext())
                {

                    if (fechaFinal.Year == 1 && Pk_Id_ModuloPlanAccion != null)
                    {

                        List<Accion> accion = (from e in db1.Tbl_Acciones
                                               where e.Fk_Id_Empresa == empresa
                                               select e).ToList();
                        List<EDActividadPlanDeAccion> Actividades = new List<EDActividadPlanDeAccion>();
                        foreach (var acc in accion)
                        {
                            Actividades = (from act in db1.Tbl_ActividadAccion
                                           where act.Fk_Id_Accion == acc.Pk_Id_Accion

                                           select new EDActividadPlanDeAccion
                                           {
                                               actividadReporte = acc.Id_Accion.ToString(),
                                               Fk_Id_ModuloPlanAccion = Pk_Id_ModuloPlanAccion,
                                               Fk_Plan_Inspección = acc.Pk_Id_Accion,
                                               Fk_Id_Actividad = act.Pk_Id_Actividad,
                                               Actividad = act.Actividad,
                                               Responsable = act.Responsable,
                                               FechaFinalizacion = act.FechaFinalizacion,
                                               consecutivo = acc.Id_Accion,
                                               //FechaCierre = ae.Descripcion,
                                               Observaciones = null

                                           }).ToList();
                            if (Actividades.Count() > 0)
                            {
                                PlanDeAccion.Origen = Modulo;
                                PlanDeAccion.Pk_Id_PlanDeAccion = Pk_Id_ModuloPlanAccion;
                                PlanDeAccion.Estado = consultarEstado(Actividades);
                                PlanDeAccion.EDActividadPlanDeAccion = Actividades;
                                PlanDeAccion.cantidadActividades = Actividades.Count();
                                ListaPlanDeAccion.Add(PlanDeAccion);
                                PlanDeAccion = new EDPlanDeAccion();
                            }
                        }
                        var log = new RegistraLog();
                        log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha consultado las actividades del módulo acción: {0}, {1},{2},{3}", DateTime.Now, Pk_Id_ModuloPlanAccion, fechaInicial, fechaFinal), new Exception());
                        if (ListaPlanDeAccion.Count() > 0)
                            return ListaPlanDeAccion;
                        else
                            return null;
                    }
                    else if (fechaFinal.Year != 1 && Pk_Id_ModuloPlanAccion != null)
                    {
                        List<Accion> accion = (from e in db1.Tbl_Acciones
                                               where e.Fk_Id_Empresa == empresa
                                               select e).ToList();
                        List<EDActividadPlanDeAccion> Actividades = new List<EDActividadPlanDeAccion>();
                        foreach (var acc in accion)
                        {
                            Actividades = (from act in db1.Tbl_ActividadAccion
                                           where act.Fk_Id_Accion == acc.Pk_Id_Accion
                                                && act.FechaFinalizacion >= fechaInicial && act.FechaFinalizacion <= fechaFinal

                                                && DbFunctions.TruncateTime(act.FechaFinalizacion) >= DbFunctions.TruncateTime(fechaInicial)
                                                && DbFunctions.TruncateTime(act.FechaFinalizacion) <= DbFunctions.TruncateTime(fechaFinal)

                                           select new EDActividadPlanDeAccion
                                           {

                                               actividadReporte = acc.Id_Accion.ToString(),
                                               Fk_Id_ModuloPlanAccion = Pk_Id_ModuloPlanAccion,
                                               Fk_Plan_Inspección = acc.Pk_Id_Accion,
                                               Fk_Id_Actividad = act.Pk_Id_Actividad,
                                               Actividad = act.Actividad,
                                               Responsable = act.Responsable,
                                               FechaFinalizacion = act.FechaFinalizacion,
                                               consecutivo = acc.Id_Accion,
                                               //FechaCierre = ae.Descripcion,
                                               Observaciones = null

                                           }).ToList();
                            if (Actividades.Count() > 0)
                            {
                                PlanDeAccion.Origen = Modulo;
                                PlanDeAccion.Pk_Id_PlanDeAccion = Pk_Id_ModuloPlanAccion;
                                PlanDeAccion.Estado = consultarEstado(Actividades);
                                PlanDeAccion.EDActividadPlanDeAccion = Actividades;
                                PlanDeAccion.cantidadActividades = Actividades.Count();
                                ListaPlanDeAccion.Add(PlanDeAccion);
                                PlanDeAccion = new EDPlanDeAccion();
                            }
                        }
                        var log = new RegistraLog();
                        log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha consultado las actividades del módulo acción: {0}, {1},{2},{3}", DateTime.Now, Pk_Id_ModuloPlanAccion, fechaInicial, fechaFinal), new Exception());
                        if (ListaPlanDeAccion.Count() > 0)
                            return ListaPlanDeAccion;
                        else
                            return null;


                    }
                    return ListaPlanDeAccion;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error consultando las actividades del módulo acción: {0}, {1},{2},{3}. Error: {2}", DateTime.Now, Pk_Id_ModuloPlanAccion, fechaInicial, fechaFinal, ex.StackTrace), ex);
                return null;
            }
        }
        /// <summary>
        /// Consulta los planes de acción en el módulo auditoría acuerdo rango de fechas de finalización
        /// </summary>
        /// <param name="nit",name="Pk_Id_ModuloPlanAccion",name="fechaInicial",name="fechaFinal"></param>
        /// <returns>EDPlanDeAccion</returns>
        public List<EDPlanDeAccion> ConsultarListaActividadAuditoria(int empresa, int Pk_Id_ModuloPlanAccion, string Modulo, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                numeroActividadInsp = false;
                List<EDPlanDeAccion> ListaPlanDeAccion = new List<EDPlanDeAccion>();
                EDPlanDeAccion PlanDeAccion = new EDPlanDeAccion();
                using (SG_SSTContext db1 = new SG_SSTContext())
                {

                    if (fechaFinal.Year == 1 && Pk_Id_ModuloPlanAccion != null)
                    {
                        List<int> Pk_Id_Auditoria = (from aud in db1.Tbl_Auditorias
                                                     join pro in db1.Tbl_AuditoriaPrograma on aud.Fk_Id_Programa equals pro.Pk_Id_Programa
                                                     where pro.Fk_Id_Empresa == empresa
                                                     select aud.Pk_Id_Auditoria).ToList();
                        List<EDActividadPlanDeAccion> Actividades = new List<EDActividadPlanDeAccion>();
                        var idCon = 1;
                        foreach (var id in Pk_Id_Auditoria)
                        {
                            Actividades = (from ac in db1.Tbl_ActividadAuditoria
                                           where ac.Fk_Id_Auditoria == id



                                           select new EDActividadPlanDeAccion
                                           {
                                               actividadReporte = idCon.ToString(),
                                               Fk_Id_ModuloPlanAccion = Pk_Id_ModuloPlanAccion,
                                               Fk_Plan_Inspección = id,
                                               Fk_Id_Actividad = ac.Pk_Id_Actividad,
                                               Actividad = ac.Actividad,
                                               Responsable = ac.Responsable,
                                               FechaFinalizacion = ac.FechaFinalizacion,
                                               //FechaCierre = ae.Descripcion,
                                               Observaciones = null

                                           }).ToList();

                            idCon++;
                            if (Actividades.Count() > 0)
                            {
                                PlanDeAccion.Origen = Modulo;
                                PlanDeAccion.Pk_Id_PlanDeAccion = Pk_Id_ModuloPlanAccion;
                                PlanDeAccion.Estado = consultarEstado(Actividades);
                                PlanDeAccion.EDActividadPlanDeAccion = Actividades;
                                PlanDeAccion.cantidadActividades = Actividades.Count();
                                ListaPlanDeAccion.Add(PlanDeAccion);
                                PlanDeAccion = new EDPlanDeAccion();
                            }
                        }
                        var log = new RegistraLog();
                        log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha consultado las actividades del módulo auditoría: {0}, {1},{2},{3}", DateTime.Now, Pk_Id_ModuloPlanAccion, fechaInicial, fechaFinal), new Exception());
                        if (ListaPlanDeAccion.Count() > 0)
                            return ListaPlanDeAccion;
                        else
                            return null;
                    }
                    else if (fechaFinal.Year != 1 && Pk_Id_ModuloPlanAccion != null)
                    {
                        List<int> Pk_Id_Auditoria = (from aud in db1.Tbl_Auditorias
                                                     join pro in db1.Tbl_AuditoriaPrograma on aud.Fk_Id_Programa equals pro.Pk_Id_Programa
                                                     where pro.Fk_Id_Empresa == empresa
                                                     select aud.Pk_Id_Auditoria).ToList();
                        List<EDActividadPlanDeAccion> Actividades = new List<EDActividadPlanDeAccion>();

                        var idCon = 1;
                        foreach (var id in Pk_Id_Auditoria)
                        {
                            Actividades = (from ac in db1.Tbl_ActividadAuditoria
                                           where ac.Fk_Id_Auditoria == id
                                            && DbFunctions.TruncateTime(ac.FechaFinalizacion) >= DbFunctions.TruncateTime(fechaInicial)
                                            && DbFunctions.TruncateTime(ac.FechaFinalizacion) <= DbFunctions.TruncateTime(fechaFinal)

                                           select new EDActividadPlanDeAccion
                                           {
                                               actividadReporte = idCon.ToString(),
                                               Fk_Id_ModuloPlanAccion = Pk_Id_ModuloPlanAccion,
                                               Fk_Plan_Inspección = id,
                                               Fk_Id_Actividad = ac.Pk_Id_Actividad,
                                               Actividad = ac.Actividad,
                                               Responsable = ac.Responsable,
                                               FechaFinalizacion = ac.FechaFinalizacion,
                                               //FechaCierre = ae.Descripcion,
                                               Observaciones = null

                                           }).ToList();
                            idCon++;
                            if (Actividades.Count() > 0)
                            {
                                PlanDeAccion.Origen = Modulo;
                                PlanDeAccion.Pk_Id_PlanDeAccion = Pk_Id_ModuloPlanAccion;
                                PlanDeAccion.Estado = consultarEstado(Actividades);
                                PlanDeAccion.EDActividadPlanDeAccion = Actividades;
                                PlanDeAccion.cantidadActividades = Actividades.Count();
                                ListaPlanDeAccion.Add(PlanDeAccion);
                                PlanDeAccion = new EDPlanDeAccion();
                            }
                        }
                        var log = new RegistraLog();
                        log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha consultado las actividades del módulo auditoría: {0}, {1},{2},{3}", DateTime.Now, Pk_Id_ModuloPlanAccion, fechaInicial, fechaFinal), new Exception());
                        if (ListaPlanDeAccion.Count() > 0)
                            return ListaPlanDeAccion;
                        else
                            return null;
                    }
                    return ListaPlanDeAccion;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error consultando las actividades del módulo auditoría: {0}, {1},{2},{3}. Error: {2}", DateTime.Now, Pk_Id_ModuloPlanAccion, fechaInicial, fechaFinal, ex.StackTrace), ex);
                return null;
            }
        }
        /// <summary>
        /// Consulta los planes de acción en el módulo inspecciones acuerdo rango de fechas de finalización
        /// </summary>
        /// <param name="nit",name="Pk_Id_ModuloPlanAccion",name="fechaInicial",name="fechaFinal"></param>
        /// <returns>EDPlanDeAccion</returns>
        public List<EDPlanDeAccion> ConsultarListaActividadInspecciones(int empresa, int Pk_Id_ModuloPlanAccion, string Modulo, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
              
                List<EDPlanDeAccion> ListaPlanDeAccion = new List<EDPlanDeAccion>();
                EDPlanDeAccion PlanDeAccion = new EDPlanDeAccion();
                using (SG_SSTContext db1 = new SG_SSTContext())
                {

                    if (fechaFinal.Year == 1 && Pk_Id_ModuloPlanAccion != null)
                    {
                        List<EDActividadPlanDeAccion> Actividades = new List<EDActividadPlanDeAccion>();
                        List<int> inspecciones = (from inp in db1.Tbl_Inspecciones
                                                  join planIn in db1.Tbl_Planeacion_Inspeccion on inp.Fk_Id_PlaneacionInspeccion equals planIn.Pk_Id_PlaneacionInspeccion
                                                  where inp.Fk_IdEmpresa == empresa
                                                  select planIn.Pk_Id_PlaneacionInspeccion).ToList();




                        foreach (var ins in inspecciones)
                        {

                            Actividades = (from oip in db1.Tbl_PlanAccionInspeccion

                                           join pi in db1.Tbl_PlanAccionporCondicion on oip.Pk_Id_PlanAcccionInspeccion equals pi.Fk_Id_PlanAcccionInspeccion


                                           join ci in db1.CondicionInsegura on pi.Fk_Id_CondicionInsegura equals ci.Pk_Id_CondicionInsegura

                                           join cipo in db1.Tbl_CondicionesInseguraporasInspeccion on ci.Pk_Id_CondicionInsegura equals cipo.Fk_Id_CondicionInsegura

                                           join inp in db1.Tbl_Inspecciones on cipo.Fk_Id_Inspecciones equals inp.Pk_Id_Inspecciones

                                           join plan in db1.Tbl_Planeacion_Inspeccion on inp.Fk_Id_PlaneacionInspeccion equals plan.Pk_Id_PlaneacionInspeccion


                                           where inp.Fk_Id_PlaneacionInspeccion == ins

                                           select new EDActividadPlanDeAccion
                                   {

                                       actividadReporte = plan.ConsecutivoPlan.ToString(),
                                       Fk_Id_ModuloPlanAccion = Pk_Id_ModuloPlanAccion,
                                       Fk_Plan_Inspección = plan.Pk_Id_PlaneacionInspeccion,
                                       Fk_Id_Actividad = oip.Pk_Id_PlanAcccionInspeccion,
                                       Actividad = oip.Actividad_Plan_Accion,
                                       Responsable = oip.Responsable_Plan_Accion,
                                       FechaFinalizacion = oip.Fecha_Fin_Plan_Accion,
                                       //FechaCierre = ae.Descripcion,
                                       Observaciones = null,

                                   }).Distinct().ToList();


                            foreach (var act in Actividades)
                            {
                                act.FechaFinalizacion = act.FechaFinalizacion;
                                //act.FechaFinalizacion = DateTime.Parse(act.FechaFinalizacionString);
                            }


                            if (Actividades.Count() > 0)
                            {
                                PlanDeAccion.Origen = Modulo;
                                PlanDeAccion.Pk_Id_PlanDeAccion = Pk_Id_ModuloPlanAccion;
                                PlanDeAccion.Estado = consultarEstado(Actividades);
                                PlanDeAccion.EDActividadPlanDeAccion = Actividades;
                                PlanDeAccion.cantidadActividades = Actividades.Count();
                                ListaPlanDeAccion.Add(PlanDeAccion);
                                PlanDeAccion = new EDPlanDeAccion();
                            }

                        }
                        var log = new RegistraLog();
                        log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se obtuvo los planes de acción del módulo reportes de actos inseguros: {0}, {1}", DateTime.Now, empresa), new Exception());
                        if (ListaPlanDeAccion.Count() > 0)
                            return ListaPlanDeAccion;
                        else

                            return null;
                    }
                    else if (fechaFinal.Year != 1 && Pk_Id_ModuloPlanAccion != null)
                    {
                        List<int> inspecciones = (from inp in db1.Tbl_Inspecciones
                                                  join planIn in db1.Tbl_Planeacion_Inspeccion on inp.Fk_Id_PlaneacionInspeccion equals planIn.Pk_Id_PlaneacionInspeccion
                                                  where inp.Fk_IdEmpresa == empresa
                                                  select planIn.Pk_Id_PlaneacionInspeccion).ToList();
                        List<EDActividadPlanDeAccion> Actividades = new List<EDActividadPlanDeAccion>();

                        foreach (var ins in inspecciones)
                        {
                            Actividades = (from oip in db1.Tbl_PlanAccionInspeccion

                                           join pi in db1.Tbl_PlanAccionporCondicion on oip.Pk_Id_PlanAcccionInspeccion equals pi.Fk_Id_PlanAcccionInspeccion


                                           join ci in db1.CondicionInsegura on pi.Fk_Id_CondicionInsegura equals ci.Pk_Id_CondicionInsegura

                                           join cipo in db1.Tbl_CondicionesInseguraporasInspeccion on ci.Pk_Id_CondicionInsegura equals cipo.Fk_Id_CondicionInsegura

                                           join inp in db1.Tbl_Inspecciones on cipo.Fk_Id_Inspecciones equals inp.Pk_Id_Inspecciones

                                           join plan in db1.Tbl_Planeacion_Inspeccion on inp.Fk_Id_PlaneacionInspeccion equals plan.Pk_Id_PlaneacionInspeccion


                                           where inp.Fk_Id_PlaneacionInspeccion == ins

                                            && DbFunctions.TruncateTime(oip.Fecha_Fin_Plan_Accion) >= DbFunctions.TruncateTime(fechaInicial)
                                            && DbFunctions.TruncateTime(oip.Fecha_Fin_Plan_Accion) <= DbFunctions.TruncateTime(fechaFinal)

                                           select new EDActividadPlanDeAccion
                                                      {

                                                          actividadReporte = plan.ConsecutivoPlan.ToString(),
                                                          Fk_Id_ModuloPlanAccion = Pk_Id_ModuloPlanAccion,
                                                          Fk_Plan_Inspección = plan.Pk_Id_PlaneacionInspeccion,
                                                          Fk_Id_Actividad = oip.Pk_Id_PlanAcccionInspeccion,
                                                          Actividad = oip.Actividad_Plan_Accion,
                                                          Responsable = oip.Responsable_Plan_Accion,
                                                          FechaFinalizacion = oip.Fecha_Fin_Plan_Accion,
                                                          //FechaCierre = ae.Descripcion,
                                                          Observaciones = null,

                                                      }).Distinct().ToList();

                            //foreach (var act in Actividades)
                            //{
                            //    act.FechaFinalizacion = act.FechaFinalizacion;
                            //}
                            //Actividades = Actividades.Where(x => x.FechaFinalizacion >= fechaInicial && x.FechaFinalizacion <= fechaFinal).ToList();


                            if (Actividades.Count() > 0)
                            {
                                PlanDeAccion.Origen = Modulo;
                                PlanDeAccion.Pk_Id_PlanDeAccion = Pk_Id_ModuloPlanAccion;
                                PlanDeAccion.Estado = consultarEstado(Actividades);
                                PlanDeAccion.EDActividadPlanDeAccion = Actividades;
                                PlanDeAccion.cantidadActividades = Actividades.Count();
                                ListaPlanDeAccion.Add(PlanDeAccion);
                                PlanDeAccion = new EDPlanDeAccion();

                            }


                        }

                        var log = new RegistraLog();
                        log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se obtuvo los planes de acción del módulo reportes de actos inseguros: {0}, {1}", DateTime.Now, empresa), new Exception());
                        if (ListaPlanDeAccion.Count() > 0)
                            return ListaPlanDeAccion;
                        else

                            return null;
                    }
                    return ListaPlanDeAccion;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error obteniendo los planes de acción del módulo inspecciones: {0}, {1}. Error: {2}", DateTime.Now, empresa, ex.StackTrace), ex);
                return null;
            }
        }
    
        /// <summary>
        /// Consulta los planes de acción en el módulo reportes de actos inseguros acuerdo rango de fechas de finalización
        /// </summary>
        /// <param name="nit",name="Pk_Id_ModuloPlanAccion",name="fechaInicial",name="fechaFinal"></param>
        /// <returns>EDPlanDeAccion</returns>
        public List<EDPlanDeAccion> ConsultarListaActividadReportes(int empresa, int Pk_Id_ModuloPlanAccion, string Modulo, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                numeroActividadInsp = false;
                List<EDPlanDeAccion> ListaPlanDeAccion = new List<EDPlanDeAccion>();
                EDPlanDeAccion PlanDeAccion = new EDPlanDeAccion();
                List<EDActividadPlanDeAccion> Actividades = new List<EDActividadPlanDeAccion>();
                using (SG_SSTContext db1 = new SG_SSTContext())
                {

                    if (fechaFinal.Year == 1 && Pk_Id_ModuloPlanAccion != null)
                    {

                        string nitEmpresa = (from emp in db1.Tbl_Empresa
                                             where emp.Pk_Id_Empresa == empresa
                                             select emp.Nit_Empresa).FirstOrDefault();

                        List<int> reportes = (from reporte in db1.Tbl_Reportes
                                              where reporte.FK_NitEmpresa.Equals(nitEmpresa)
                                              select reporte.PK_Id_Reportes).ToList();

                        foreach (var rep in reportes)
                        {


                            Actividades = (from activi in db1.Tbl_ActividadesActosInseguros
                                           join
                                               repo in db1.Tbl_Reportes on activi.FK_Id_Reportes equals
                                               repo.PK_Id_Reportes
                                           where activi.FK_Id_Reportes == rep

                                           select new EDActividadPlanDeAccion
                                           {
                                               Fk_Id_ModuloPlanAccion = Pk_Id_ModuloPlanAccion,
                                               Fk_Plan_Inspección = rep,
                                               actividadReporte = repo.ConsecutivoReporte.ToString(),
                                               Fk_Id_Actividad = activi.PK_ID_ActividadActosInseguros,
                                               Actividad = activi.NombreActividad,
                                               Responsable = activi.ResponsableActividad,
                                               FechaFinalizacion = activi.FechaEjecucion,
                                               //FechaCierre = ae.Descripcion,
                                               Observaciones = null

                                           }).ToList();

                            if (Actividades.Count() > 0)
                            {
                                PlanDeAccion.Origen = Modulo;
                                PlanDeAccion.Pk_Id_PlanDeAccion = Pk_Id_ModuloPlanAccion;
                                PlanDeAccion.Estado = consultarEstado(Actividades);
                                PlanDeAccion.EDActividadPlanDeAccion = Actividades;
                                PlanDeAccion.cantidadActividades = Actividades.Count();
                                ListaPlanDeAccion.Add(PlanDeAccion);
                                PlanDeAccion = new EDPlanDeAccion();
                            }
                        }

                        var log = new RegistraLog();
                        log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se obtuvo los planes de acción del módulo reportes de actos inseguros: {0}, {1}", DateTime.Now, empresa), new Exception());
                        if (ListaPlanDeAccion.Count() > 0)
                            return ListaPlanDeAccion;
                        else
                            return null;
                    }
                    else if (fechaFinal.Year != 1 && Pk_Id_ModuloPlanAccion != null)
                    {
                        string nitEmpresa = (from emp in db1.Tbl_Empresa
                                             where emp.Pk_Id_Empresa == empresa
                                             select emp.Nit_Empresa).FirstOrDefault();

                        List<int> reportes = (from reporte in db1.Tbl_Reportes
                                              where reporte.FK_NitEmpresa.Equals(nitEmpresa)
                                              select reporte.PK_Id_Reportes).ToList();

                        foreach (var rep in reportes)
                        {
                            Actividades = (from activi in db1.Tbl_ActividadesActosInseguros
                                           join
                                               repo in db1.Tbl_Reportes on activi.FK_Id_Reportes equals
                                               repo.PK_Id_Reportes
                                           where activi.FK_Id_Reportes == rep

                                           && DbFunctions.TruncateTime(activi.FechaEjecucion) >= DbFunctions.TruncateTime(fechaInicial)
                                          && DbFunctions.TruncateTime(activi.FechaEjecucion) <= DbFunctions.TruncateTime(fechaFinal)


                                           select new EDActividadPlanDeAccion
                                           {
                                               Fk_Id_ModuloPlanAccion = Pk_Id_ModuloPlanAccion,
                                               Fk_Plan_Inspección = rep,
                                               Fk_Id_Actividad = activi.PK_ID_ActividadActosInseguros,
                                               Actividad = activi.NombreActividad,
                                               actividadReporte = repo.ConsecutivoReporte.ToString(),
                                               Responsable = activi.ResponsableActividad,
                                               FechaFinalizacion = activi.FechaEjecucion,
                                               //FechaCierre = ae.Descripcion,
                                               Observaciones = null

                                           }).ToList();
                            if (Actividades.Count() > 0)
                            {
                                PlanDeAccion.Origen = Modulo;
                                PlanDeAccion.Pk_Id_PlanDeAccion = Pk_Id_ModuloPlanAccion;
                                PlanDeAccion.Estado = consultarEstado(Actividades);
                                PlanDeAccion.EDActividadPlanDeAccion = Actividades;
                                PlanDeAccion.cantidadActividades = Actividades.Count();
                                ListaPlanDeAccion.Add(PlanDeAccion);
                                PlanDeAccion = new EDPlanDeAccion();
                            }
                        }

                        var log = new RegistraLog();
                        log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se obtuvo los planes de acción del módulo reportes de actos inseguros: {0}, {1}", DateTime.Now, empresa), new Exception());
                        if (ListaPlanDeAccion.Count() > 0)
                            return ListaPlanDeAccion;
                        else
                            return null;
                    }



                    return ListaPlanDeAccion;

                }
            }

            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error obteniendo los planes de acción del módulo reportes de actos inseguros: {0}, {1}. Error: {2}", DateTime.Now, empresa, ex.StackTrace), ex);
                return null;
            }
        }



        /// <summary>
        /// Consulta los planes de acción en el módulo copasst acuerdo rango de fechas de finalización
        /// </summary>
        /// <param name="nit",name="Pk_Id_ModuloPlanAccion",name="fechaInicial",name="fechaFinal"></param>
        /// <returns>EDPlanDeAccion</returns>
        public List<EDPlanDeAccion> ConsultarListaActividadCopasst(int empresa, int Pk_Id_ModuloPlanAccion, string Modulo, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                numeroActividadInsp = false;
                List<EDPlanDeAccion> ListaPlanDeAccion = new List<EDPlanDeAccion>();
                EDPlanDeAccion PlanDeAccion = new EDPlanDeAccion();
                List<EDActividadPlanDeAccion> Actividades = new List<EDActividadPlanDeAccion>();
                using (SG_SSTContext db1 = new SG_SSTContext())
                {

                    if (fechaFinal.Year == 1 && Pk_Id_ModuloPlanAccion != null)
                    {

                        //List<int> Fk_Id_Sede = (from sed in db1.Tbl_Sede
                        //                        where sed.Fk_Id_Empresa == empresa
                        ////                        select sed.Pk_Id_Sede).ToList();
                        //foreach (var sede in Fk_Id_Sede)
                        //{
                            List<int> copasst = (from copa in db1.Tbl_ActasCopasst
                                                 where copa.Fk_Id_Empresa == empresa
                                                  select copa.PK_Id_Acta).ToList();
                            foreach (var cop in copasst)
                            {
                                Actividades = (from activi in db1.Tbl_AccionesActaCopasst
                                               join
                                                   copas in db1.Tbl_ActasCopasst on activi.Fk_Id_Acta equals
                                                   copas.PK_Id_Acta
                                               where activi.Fk_Id_Acta == cop

                                               select new EDActividadPlanDeAccion
                                               {
                                                   Fk_Id_ModuloPlanAccion = Pk_Id_ModuloPlanAccion,
                                                   Fk_Plan_Inspección = cop,
                                                   actividadReporte = copas.Consecutivo_Acta.ToString(),
                                                   Fk_Id_Actividad = activi.Pk_Id_AccionActaCopasst,
                                                   Actividad = activi.AccionARealizar,
                                                   Responsable = activi.Responsable,
                                                   FechaFinalizacion = activi.FechaProbable,
                                                   //FechaCierre = ae.Descripcion,
                                                   Observaciones = null

                                               }).ToList();
                                if (Actividades.Count() > 0)
                                {
                                    PlanDeAccion.Origen = Modulo;
                                    PlanDeAccion.Pk_Id_PlanDeAccion = Pk_Id_ModuloPlanAccion;
                                    PlanDeAccion.Estado = consultarEstado(Actividades);
                                    PlanDeAccion.EDActividadPlanDeAccion = Actividades;
                                    PlanDeAccion.cantidadActividades = Actividades.Count();
                                    ListaPlanDeAccion.Add(PlanDeAccion);
                                    PlanDeAccion = new EDPlanDeAccion();
                                }
                            }
                        
                        var log = new RegistraLog();
                        log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se obtuvo los planes de acción del módulo  Coppast: {0}, {1}", DateTime.Now, empresa), new Exception());
                        if (ListaPlanDeAccion.Count() > 0)
                            return ListaPlanDeAccion;
                        else
                            return null;
                    }
                    else if (fechaFinal.Year != 1 && Pk_Id_ModuloPlanAccion != null)
                    {
                      
                   
                            List<int> copasst = (from copa in db1.Tbl_ActasCopasst
                                                 where copa.Fk_Id_Empresa == empresa

                                                 select copa.PK_Id_Acta).ToList();
                            foreach (var cop in copasst)
                            {
                                Actividades = (from activi in db1.Tbl_AccionesActaCopasst
                                               join
                                                   copas in db1.Tbl_ActasCopasst on activi.Fk_Id_Acta equals
                                                   copas.PK_Id_Acta
                                               where activi.Fk_Id_Acta == cop

                                               && DbFunctions.TruncateTime(activi.FechaProbable) >= DbFunctions.TruncateTime(fechaInicial)
                                              && DbFunctions.TruncateTime(activi.FechaProbable) <= DbFunctions.TruncateTime(fechaFinal)



                                               select new EDActividadPlanDeAccion
                                               {
                                                   Fk_Id_ModuloPlanAccion = Pk_Id_ModuloPlanAccion,
                                                   Fk_Plan_Inspección = cop,
                                                   Fk_Id_Actividad = activi.Pk_Id_AccionActaCopasst,
                                                   Actividad = activi.AccionARealizar,
                                                   Responsable = activi.Responsable,
                                                   FechaFinalizacion = activi.FechaProbable,
                                                   //FechaCierre = ae.Descripcion,
                                                   actividadReporte = copas.Consecutivo_Acta.ToString(),
                                                   Observaciones = null

                                               }).ToList();

                                if (Actividades.Count() > 0)
                                {
                                    PlanDeAccion.Origen = Modulo;
                                    PlanDeAccion.Pk_Id_PlanDeAccion = Pk_Id_ModuloPlanAccion;
                                    PlanDeAccion.Estado = consultarEstado(Actividades);
                                    PlanDeAccion.EDActividadPlanDeAccion = Actividades;
                                    PlanDeAccion.cantidadActividades = Actividades.Count();
                                    ListaPlanDeAccion.Add(PlanDeAccion);
                                    PlanDeAccion = new EDPlanDeAccion();
                                }
                            }
                        
                        var log = new RegistraLog();
                        log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se obtuvo los planes de acción del módulo Coppast: {0}, {1}", DateTime.Now, empresa), new Exception());
                     if (ListaPlanDeAccion.Count() > 0)
                            return ListaPlanDeAccion;
                        else

                            return null;
                    }


                    return ListaPlanDeAccion;

                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error obteniendo los planes de acción del módulo Coppast: {0}, {1}. Error: {2}", DateTime.Now, empresa, ex.StackTrace), ex);
                return null;
            }
        }

        /// <summary>
        /// Consulta los planes de acción en el módulo ConsultarListaActividaConvivencia rango de fechas de finalización
        /// </summary>
        /// <param name="nit",name="Pk_Id_ModuloPlanAccion",name="fechaInicial",name="fechaFinal"></param>
        /// <returns>EDPlanDeAccion</returns>
        public List<EDPlanDeAccion> ConsultarListaActividaConvivencia(int empresa, int Pk_Id_ModuloPlanAccion, string Modulo, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                numeroActividadInsp = false;
                List<EDPlanDeAccion> ListaPlanDeAccion = new List<EDPlanDeAccion>();
                EDPlanDeAccion PlanDeAccion = new EDPlanDeAccion();
                List<EDActividadPlanDeAccion> Actividades = new List<EDActividadPlanDeAccion>();
                using (SG_SSTContext db1 = new SG_SSTContext())
                {

                    if (fechaFinal.Year == 1 && Pk_Id_ModuloPlanAccion != null)
                    {

                            List<int> convivencias = (from convivencia in db1.Tbl_ActasConvivencia
                                                      where convivencia.Fk_Id_Empresa == empresa
                                                      select convivencia.PK_Id_Acta).ToList();
                            foreach (var convi in convivencias)
                            {
                                Actividades = (from activi in db1.Tbl_AccionesActaConvivencia
                                               join
                                                   convive in db1.Tbl_ActasConvivencia on activi.Fk_Id_Acta equals
                                                   convive.PK_Id_Acta
                                               where activi.Fk_Id_Acta == convi

                                               select new EDActividadPlanDeAccion
                                               {
                                                   Fk_Id_ModuloPlanAccion = Pk_Id_ModuloPlanAccion,
                                                   Fk_Plan_Inspección = convi,
                                                   actividadReporte = convive.Consecutivo_Acta.ToString(),
                                                   Fk_Id_Actividad = activi.Pk_Id_AccionActaConvivencia,
                                                   Actividad = activi.AccionARealizar,
                                                   Responsable = activi.Responsable,
                                                   FechaFinalizacion = activi.FechaProbable,
                                                   //FechaCierre = ae.Descripcion,
                                                   Observaciones = null

                                               }).ToList();
                                if (Actividades.Count() > 0)
                                {
                                    PlanDeAccion.Origen = Modulo;
                                    PlanDeAccion.Pk_Id_PlanDeAccion = Pk_Id_ModuloPlanAccion;
                                    PlanDeAccion.Estado = consultarEstado(Actividades);
                                    PlanDeAccion.EDActividadPlanDeAccion = Actividades;
                                    PlanDeAccion.cantidadActividades = Actividades.Count();
                                    ListaPlanDeAccion.Add(PlanDeAccion);
                                    PlanDeAccion = new EDPlanDeAccion();
                                }
                        
                            }
                        
                        var log = new RegistraLog();
                        log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se obtuvo los planes de acción del módulo Convivencia laboral: {0}, {1}", DateTime.Now, empresa), new Exception());
                        if (ListaPlanDeAccion.Count() > 0)
                            return ListaPlanDeAccion;
                        else
                            return null;
                    }
                    else if (fechaFinal.Year != 1 && Pk_Id_ModuloPlanAccion != null)
                    {
                      
                      
                            List<int> convivencias = (from convivencia in db1.Tbl_ActasConvivencia
                                                      where convivencia.Fk_Id_Empresa == empresa
                                                      select convivencia.PK_Id_Acta).ToList();
                            foreach (var convi in convivencias)
                            {
                                Actividades = (from activi in db1.Tbl_AccionesActaConvivencia
                                               join
                                                   convive in db1.Tbl_ActasConvivencia on activi.Fk_Id_Acta equals
                                                   convive.PK_Id_Acta
                                               where activi.Fk_Id_Acta == convi

                                               && DbFunctions.TruncateTime(activi.FechaProbable) >= DbFunctions.TruncateTime(fechaInicial)
                                              && DbFunctions.TruncateTime(activi.FechaProbable) <= DbFunctions.TruncateTime(fechaFinal)



                                               select new EDActividadPlanDeAccion
                                               {
                                                   Fk_Id_ModuloPlanAccion = Pk_Id_ModuloPlanAccion,
                                                   Fk_Plan_Inspección = convi,
                                                   Fk_Id_Actividad = activi.Pk_Id_AccionActaConvivencia,
                                                   Actividad = activi.AccionARealizar,
                                                   actividadReporte = convive.Consecutivo_Acta.ToString(),
                                             
                                                   Responsable = activi.Responsable,
                                                   FechaFinalizacion = activi.FechaProbable,
                                                   //FechaCierre = ae.Descripcion,
                                                   Observaciones = null

                                               }).ToList();
                                if (Actividades.Count() > 0)
                                {
                                    PlanDeAccion.Origen = Modulo;
                                    PlanDeAccion.Pk_Id_PlanDeAccion = Pk_Id_ModuloPlanAccion;
                                    PlanDeAccion.Estado = consultarEstado(Actividades);
                                    PlanDeAccion.EDActividadPlanDeAccion = Actividades;
                                    PlanDeAccion.cantidadActividades = Actividades.Count();
                                    ListaPlanDeAccion.Add(PlanDeAccion);
                                    PlanDeAccion = new EDPlanDeAccion();
                                }
                            }
                        
                        var log = new RegistraLog();
                        log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se obtuvo los planes de acción del módulo convivencia laboral: {0}, {1}", DateTime.Now, empresa), new Exception());
                        if (ListaPlanDeAccion.Count() > 0)
                            return ListaPlanDeAccion;
                        else

                            return null;

                    }
                    return ListaPlanDeAccion;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error obteniendo los planes de acción del módulo convivencia laboral: {0}, {1}. Error: {2}", DateTime.Now, empresa, ex.StackTrace), ex);
                return null;
            }
        }




        /// <summary>
        /// Consulta los planes de acción en el módulo ConsultarListaActividaRevisionSGSST rango de fechas de finalización
        /// </summary>
        /// <param name="nit",name="Pk_Id_ModuloPlanAccion",name="fechaInicial",name="fechaFinal"></param>
        /// <returns>EDPlanDeAccion</returns>
        public List<EDPlanDeAccion> ConsultarListaActividaRevisionSGSST(int empresa, int Pk_Id_ModuloPlanAccion, string Modulo, DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                numeroActividadInsp = false;
                List<EDPlanDeAccion> ListaPlanDeAccion = new List<EDPlanDeAccion>();
                EDPlanDeAccion PlanDeAccion = new EDPlanDeAccion();
                List<EDActividadPlanDeAccion> Actividades = new List<EDActividadPlanDeAccion>();
                using (SG_SSTContext db1 = new SG_SSTContext())
                {

                    if (fechaFinal.Year == 1 && Pk_Id_ModuloPlanAccion != null)
                    {

                        List<int> revisiones = (from revision in db1.Tbl_ActaRevision
                                                where revision.Fk_Id_Empresa == empresa
                                                select revision.PK_Id_ActaRevision).ToList();
                        foreach (var revi in revisiones)
                        {
                            Actividades = (from activi in db1.Tbl_PlanAccionRevision
                                           join
                                               revisi in db1.Tbl_ActaRevision on activi.FK_Acta equals
                                               revisi.PK_Id_ActaRevision
                                           where activi.FK_Acta == revi

                                           select new EDActividadPlanDeAccion
                                           {
                                               Fk_Id_ModuloPlanAccion = Pk_Id_ModuloPlanAccion,
                                               Fk_Plan_Inspección = revi,
                                               actividadReporte = revisi.Num_Acta.ToString(),
                                               Fk_Id_Actividad = activi.PK_Id_PlanAccion,
                                               Actividad = activi.Actividad,
                                               Responsable = activi.Responsable,
                                               FechaFinalizacion = activi.Fecha,
                                               //FechaCierre = ae.Descripcion,
                                               Observaciones = null

                                           }).ToList();
                            if (Actividades.Count() > 0)
                            {
                                PlanDeAccion.Origen = Modulo;
                                PlanDeAccion.Pk_Id_PlanDeAccion = Pk_Id_ModuloPlanAccion;
                                PlanDeAccion.Estado = consultarEstado(Actividades);
                                PlanDeAccion.EDActividadPlanDeAccion = Actividades;
                                PlanDeAccion.cantidadActividades = Actividades.Count();
                                ListaPlanDeAccion.Add(PlanDeAccion);
                                PlanDeAccion = new EDPlanDeAccion();
                            }

                        }

                        var log = new RegistraLog();
                        log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se obtuvo los planes de acción del módulo RevisionSGSST: {0}, {1}", DateTime.Now, empresa), new Exception());
                        if (ListaPlanDeAccion.Count() > 0)
                            return ListaPlanDeAccion;
                        else
                            return null;
                    }
                    else if (fechaFinal.Year != 1 && Pk_Id_ModuloPlanAccion != null)
                    {

                        List<int> revisiones = (from revision in db1.Tbl_ActaRevision
                                                where revision.Fk_Id_Empresa == empresa
                                                select revision.PK_Id_ActaRevision).ToList();
                        foreach (var revi in revisiones)
                        {
                            Actividades = (from activi in db1.Tbl_PlanAccionRevision
                                           join
                                               revisi in db1.Tbl_ActaRevision on activi.FK_Acta equals
                                               revisi.PK_Id_ActaRevision
                                             
                                           where activi.FK_Acta == revi

                                           && DbFunctions.TruncateTime(activi.Fecha) >= DbFunctions.TruncateTime(fechaInicial)
                                          && DbFunctions.TruncateTime(activi.Fecha) <= DbFunctions.TruncateTime(fechaFinal)



                                           select new EDActividadPlanDeAccion
                                           {
                                               Fk_Id_ModuloPlanAccion = Pk_Id_ModuloPlanAccion,
                                               Fk_Plan_Inspección = revi,
                                               actividadReporte = revisi.Num_Acta.ToString(),
                                               Fk_Id_Actividad = activi.PK_Id_PlanAccion,
                                               Actividad = activi.Actividad,
                                               Responsable = activi.Responsable,
                                               FechaFinalizacion = activi.Fecha,
                                               //FechaCierre = ae.Descripcion,
                                               Observaciones = null

                                           }).ToList();
                            if (Actividades.Count() > 0)
                            {
                                PlanDeAccion.Origen = Modulo;
                                PlanDeAccion.Pk_Id_PlanDeAccion = Pk_Id_ModuloPlanAccion;
                                PlanDeAccion.Estado = consultarEstado(Actividades);
                                PlanDeAccion.EDActividadPlanDeAccion = Actividades;
                                PlanDeAccion.cantidadActividades = Actividades.Count();
                                ListaPlanDeAccion.Add(PlanDeAccion);
                                PlanDeAccion = new EDPlanDeAccion();
                            }
                        }

                        var log = new RegistraLog();
                        log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se obtuvo los planes de acción del módulo RevisionSGSST: {0}, {1}", DateTime.Now, empresa), new Exception());
                        if (ListaPlanDeAccion.Count() > 0)
                            return ListaPlanDeAccion;
                        else

                            return null;

                    }
                    return ListaPlanDeAccion;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error obteniendo los planes de acción del módulo RevisionSGSST: {0}, {1}. Error: {2}", DateTime.Now, empresa, ex.StackTrace), ex);
                return null;
            }
        }

        /// <summary>
        /// Consulta módulos que tienen planes de acción
        /// </summary>
        /// <param name="nit",name="Pk_Id_ModuloPlanAccion",name="fechaInicial",name="fechaFinal"></param>
        /// <returns>EDPlanDeAccion</returns>
        public List<ModulosPlanAccion> ObtenerModulos(int nit)
        {
            try
            {
                List<EDPlanDeAccion> ListaPlanDeAccion = new List<EDPlanDeAccion>();
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    var ListaMod = (from s in db1.Tbl_Modulos_Plan_Accion
                                    select s).ToList<ModulosPlanAccion>();
                    var log = new RegistraLog();
                    log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Se ha consultado los módulos: {0}, {1}", DateTime.Now, nit), new Exception());
                    return ListaMod;
                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                log.RegistrarError(typeof(PlanDeAccionManager), string.Format("Error consultando los módulos: {0}, {1}. Error: {2}", DateTime.Now, nit, ex.StackTrace), ex);
                return null;
            }
        }


    }

}
