using SG_SST.Audotoria;
using SG_SST.EntidadesDominio.Aplicacion;
using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Interfaces.Aplicacion;
using SG_SST.Models;
using SG_SST.Models.Aplicacion;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SG_SST.Repositorio.Aplicacion
{
    public class PlanTrabajoManager : IPlanTrabajo
    {
        public bool CrearPlanTrabajo(EDAplicacionPlanTrabajo planTrabajo)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        AplicacionPlanTrabajo plan = new AplicacionPlanTrabajo
                        {
                            Fk_Id_Sede = planTrabajo.Fk_Id_Sede,
                            FechaInicio = planTrabajo.FechaInicio,
                            FechaFinal = planTrabajo.FechaFinal,
                            Vigencia = planTrabajo.Vigencia,
                            Tipo= planTrabajo.Tipo,
                            FechaAplicacion= planTrabajo.FechaAplicacion
                        };
                        context.Tbl_AplicacionPlanTrabajo.Add(plan);
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(AplicacionPlanTrabajo), string.Format("Error al guardar el plan  de trabajo en la base de datos  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        transaction.Rollback();
                        return false;
                    }
                }
            }

        }
        public List<EDAplicacionPlanTrabajo> ObtenerPlanesDeTrabajo(int idEmpresa)
        {
            List<EDAplicacionPlanTrabajo> plan = null;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                plan = (from pl in context.Tbl_AplicacionPlanTrabajo
                          join sd in context.Tbl_Sede on pl.Fk_Id_Sede equals sd.Pk_Id_Sede
                          where sd.Fk_Id_Empresa == idEmpresa
                        select new EDAplicacionPlanTrabajo
                          {
                              Pk_Id_PlanTrabajo  = pl.Pk_Id_PlanTrabajo,
                              NombreSede = sd.Nombre_Sede,
                              FechaInicio = pl.FechaInicio,
                              FechaFinal = pl.FechaFinal,
                              Vigencia = pl.Vigencia,
                              Fk_Id_Sede = pl.Fk_Id_Sede,
                              Tipo=pl.Tipo,
                              FechaAplicacion=pl.FechaAplicacion
                        }).ToList();
            }
            return plan;
        }
        public EDAplicacionPlanTrabajo EditarPlanTrabajo(int Pk_Id_PlanTrabajo)
        {
            EDAplicacionPlanTrabajo plan = null;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                plan = (from pl in context.Tbl_AplicacionPlanTrabajo
                        join sd in context.Tbl_Sede on pl.Fk_Id_Sede equals sd.Pk_Id_Sede
                        where pl.Pk_Id_PlanTrabajo == Pk_Id_PlanTrabajo
                        select new EDAplicacionPlanTrabajo
                        {
                            NombreSede = sd.Nombre_Sede,
                            FechaInicio = pl.FechaInicio,
                            FechaFinal = pl.FechaFinal,
                            Vigencia = pl.Vigencia,
                            Pk_Id_PlanTrabajo=pl.Pk_Id_PlanTrabajo
                        }).FirstOrDefault();
            }
            return plan;
        }
        public List<EDAplicacionPlanTrabajo> ObtenerPlanesDeTrabajoSede(int intsede)
        {
            List<EDAplicacionPlanTrabajo> Planes = new List<EDAplicacionPlanTrabajo>();
            //using (SG_SSTContext context = new SG_SSTContext())
            //{
            //    var Listavar = (from s in context.Tbl_BateriaCuestionario
            //                    join d in context.Tbl_BateriaDimension on s.Fk_Id_BateriaDimension equals d.Pk_Id_BateriaDimension
            //                    join e in context.Tbl_Bateria on d.Fk_Id_Bateria equals e.Pk_Id_Bateria
            //                    where e.Pk_Id_Bateria == IdBateria
            //                    select s).ToList<BateriaCuestionario>().Distinct();
            //}

            return Planes;
        }
        public bool crearobjetivo(EDAplicacionPlanTrabajoDetalle EDAplicacionPlanTrabajoDetalle)
        {
            bool guardar = false;
            AplicacionPlanTrabajoDetalle AplicacionPlanTrabajoDetalle = new AplicacionPlanTrabajoDetalle();

            AplicacionPlanTrabajoDetalle.Fk_Id_PlanTrabajo = EDAplicacionPlanTrabajoDetalle.Fk_Id_PlanTrabajo;
            AplicacionPlanTrabajoDetalle.Metas = EDAplicacionPlanTrabajoDetalle.Metas;
            AplicacionPlanTrabajoDetalle.Objetivo = EDAplicacionPlanTrabajoDetalle.Objetivo;
            AplicacionPlanTrabajoDetalle.RecursoHumano = EDAplicacionPlanTrabajoDetalle.RecursoHumano;
            AplicacionPlanTrabajoDetalle.RecursoTec = EDAplicacionPlanTrabajoDetalle.RecursoTec;
            AplicacionPlanTrabajoDetalle.RecursoFinanciero = EDAplicacionPlanTrabajoDetalle.RecursoFinanciero;

            using (SG_SSTContext context = new SG_SSTContext())
            {
                context.Tbl_AplicacionPlanTrabajoDetalle.Add(AplicacionPlanTrabajoDetalle);
                try
                {
                    context.SaveChanges();
                    guardar = true;
                }
                catch (Exception ex)
                {
                }
            }
            return guardar;
        }
        public bool actualizarobjetivo(EDAplicacionPlanTrabajoDetalle EDAplicacionPlanTrabajoDetalle)
        {
            bool guardar = false;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                var Listavar = (from s in context.Tbl_AplicacionPlanTrabajoDetalle
                                where s.Pk_Id_PlanTrabajoDetalle == EDAplicacionPlanTrabajoDetalle.Pk_Id_PlanTrabajoDetalle && s.Fk_Id_PlanTrabajo== EDAplicacionPlanTrabajoDetalle.Fk_Id_PlanTrabajo
                                select s).FirstOrDefault<AplicacionPlanTrabajoDetalle>();
                if (Listavar != null)
                {
                    Listavar.Objetivo = EDAplicacionPlanTrabajoDetalle.Objetivo;
                    Listavar.Metas = EDAplicacionPlanTrabajoDetalle.Metas;
                    Listavar.RecursoHumano = EDAplicacionPlanTrabajoDetalle.RecursoHumano;
                    Listavar.RecursoTec = EDAplicacionPlanTrabajoDetalle.RecursoTec;
                    Listavar.RecursoFinanciero = EDAplicacionPlanTrabajoDetalle.RecursoFinanciero;
                    context.Entry(Listavar).State = EntityState.Modified;
                    try
                    {
                        context.SaveChanges();
                        guardar = true;
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            return guardar;
        }
        public EDAplicacionPlanTrabajo ConsultarPlanTrabajo(int Pk_Id_PlanTrabajo, int IdEmpresa)
        {
            EDAplicacionPlanTrabajo plan = new EDAplicacionPlanTrabajo();
            plan.ListaDetalles = new List<EDAplicacionPlanTrabajoDetalle>();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                var Listavar = (from s in context.Tbl_AplicacionPlanTrabajo
                                join d in context.Tbl_Sede on s.Fk_Id_Sede equals d.Pk_Id_Sede
                                join e in context.Tbl_Empresa on d.Fk_Id_Empresa equals e.Pk_Id_Empresa
                                where s.Pk_Id_PlanTrabajo == Pk_Id_PlanTrabajo && e.Pk_Id_Empresa== IdEmpresa
                                select s).FirstOrDefault<AplicacionPlanTrabajo>();
                if (Listavar != null)
                {
                    plan.Pk_Id_PlanTrabajo = Listavar.Pk_Id_PlanTrabajo;
                    plan.FechaInicio = Listavar.FechaInicio;
                    plan.FechaFinal = Listavar.FechaFinal;
                    plan.Vigencia = Listavar.Vigencia;
                    plan.RepLegalImagen = Listavar.RepLegalImagen;
                    plan.RepSGSSTImagen = Listavar.RepSGSSTImagen;
                    plan.RepLegalRuta = Listavar.RepLegalRuta;
                    plan.RepSGSSTRuta = Listavar.RepSGSSTRuta;
                    plan.RepLegalNombre = Listavar.RepLegalNombre;
                    plan.RepSGSSTNombre = Listavar.RepSGSSTNombre;
                    plan.RepLegalTipoDocumento = Listavar.RepLegalTipoDocumento;
                    plan.RepSGSSTTipoDocumento = Listavar.RepSGSSTTipoDocumento;
                    plan.RepLegalDocumento = Listavar.RepLegalDocumento;
                    plan.RepSGSSTDocumento = Listavar.RepSGSSTDocumento;
                    plan.Fk_Id_Sede = Listavar.Fk_Id_Sede;
                    plan.Tipo = Listavar.Tipo;
                    plan.FechaAplicacion = Listavar.FechaAplicacion;


                    var Listavar1 = (from s in context.Tbl_AplicacionPlanTrabajoDetalle
                                     where s.Fk_Id_PlanTrabajo == plan.Pk_Id_PlanTrabajo
                                     select s).ToList<AplicacionPlanTrabajoDetalle>();
                    foreach (var item in Listavar1)
                    {
                        EDAplicacionPlanTrabajoDetalle EDAplicacionPlanTrabajoDetalle = new EDAplicacionPlanTrabajoDetalle();
                        EDAplicacionPlanTrabajoDetalle.Pk_Id_PlanTrabajoDetalle = item.Pk_Id_PlanTrabajoDetalle;
                        EDAplicacionPlanTrabajoDetalle.Objetivo = item.Objetivo;
                        EDAplicacionPlanTrabajoDetalle.Metas = item.Metas;
                        EDAplicacionPlanTrabajoDetalle.RecursoHumano = item.RecursoHumano;
                        EDAplicacionPlanTrabajoDetalle.RecursoTec = item.RecursoTec;
                        EDAplicacionPlanTrabajoDetalle.RecursoFinanciero = item.RecursoFinanciero;
                        EDAplicacionPlanTrabajoDetalle.Fk_Id_PlanTrabajo = item.Fk_Id_PlanTrabajo;
                        
                        plan.ListaDetalles.Add(EDAplicacionPlanTrabajoDetalle);
                    }
                    
                }
            }


            using (SG_SSTContext context = new SG_SSTContext())
            {
                foreach (var item in plan.ListaDetalles)
                {
                    var Listavar = (from s in context.Tbl_AplicacionPlanTrabajoActividad
                                    where s.Fk_Id_PlanTrabajoDetalle == item.Pk_Id_PlanTrabajoDetalle
                                    select s).ToList<AplicacionPlanTrabajoActividad>();
                    if (Listavar != null)
                    {
                        foreach (var item1 in Listavar)
                        {
                            item.ListaActividades = new List<EDAplicacionPlanTrabajoActividad>();
                        }
                        foreach (var item1 in Listavar)
                        {
                            EDAplicacionPlanTrabajoActividad EDAplicacionPlanTrabajoActividad = new EDAplicacionPlanTrabajoActividad();
                            EDAplicacionPlanTrabajoActividad.Pk_Id_PlanTrabajoActividad = item1.Pk_Id_PlanTrabajoActividad;
                            EDAplicacionPlanTrabajoActividad.FechaProgramacionIncial = item1.FechaProgramacionIncial;
                            EDAplicacionPlanTrabajoActividad.FechaEstado = item1.FechaEstado;
                            EDAplicacionPlanTrabajoActividad.Estado = item1.Estado;
                            EDAplicacionPlanTrabajoActividad.Observaciones = item1.Observaciones;
                            EDAplicacionPlanTrabajoActividad.ResponsableNombre = item1.ResponsableNombre;
                            EDAplicacionPlanTrabajoActividad.ResponsableDocumento = item1.ResponsableDocumento;
                            EDAplicacionPlanTrabajoActividad.ResponsableTipoDocumento = item1.ResponsableTipoDocumento;
                            EDAplicacionPlanTrabajoActividad.Fk_Id_PlanTrabajoDetalle = item1.Fk_Id_PlanTrabajoDetalle;
                            EDAplicacionPlanTrabajoActividad.Descripcion = item1.Descripcion;
                            EDAplicacionPlanTrabajoActividad.ListaProgramacion = new List<EDAplicacionPlanTrabajoProgramacion>();
                            if (item1.Programaciones!=null)
                            {
                                foreach (var item2 in item1.Programaciones)
                                {
                                    if (item2 != null)
                                    {
                                        EDAplicacionPlanTrabajoProgramacion EDAplicacionPlanTrabajoProgramacion = new EDAplicacionPlanTrabajoProgramacion();
                                        EDAplicacionPlanTrabajoProgramacion.Pk_Id_AplicacionPlanTrabajoProgramacion = item2.Pk_Id_AplicacionPlanTrabajoProgramacion;
                                        EDAplicacionPlanTrabajoProgramacion.FechaProgramacionIncial = item2.FechaProgramacionIncial;
                                        EDAplicacionPlanTrabajoProgramacion.FechaEstado = item2.FechaEstado;
                                        EDAplicacionPlanTrabajoProgramacion.Estado = item2.Estado;
                                        EDAplicacionPlanTrabajoProgramacion.Observaciones = item2.Observaciones;
                                        EDAplicacionPlanTrabajoProgramacion.Fk_Id_PlanTrabajoActividad = item2.Fk_Id_PlanTrabajoActividad;
                                        EDAplicacionPlanTrabajoActividad.ListaProgramacion.Add(EDAplicacionPlanTrabajoProgramacion);
                                    }
                                }
                            }
                            
                            item.ListaActividades.Add(EDAplicacionPlanTrabajoActividad);
                        }
                    }
                }
            }

            foreach (var item in plan.ListaDetalles)
            {
                if (item.ListaActividades != null)
                {
                    foreach (var item1 in item.ListaActividades)
                    {
                        int pkid = item1.Pk_Id_PlanTrabajoActividad;
                        var listaprogramacion = ListaProgramacion(pkid);
                        if (listaprogramacion != null)
                        {
                            item1.ListaProgramacion = listaprogramacion;
                        }
                    }
                }
                
            }
            return plan;
        }
        public bool crearactividad(EDAplicacionPlanTrabajoActividad EDAplicacionPlanTrabajoActividad)
        {
            bool guardar = false;
            AplicacionPlanTrabajoActividad AplicacionPlanTrabajoActividad = new AplicacionPlanTrabajoActividad();

            AplicacionPlanTrabajoActividad.Fk_Id_PlanTrabajoDetalle = EDAplicacionPlanTrabajoActividad.Fk_Id_PlanTrabajoDetalle;
            AplicacionPlanTrabajoActividad.FechaProgramacionIncial = EDAplicacionPlanTrabajoActividad.FechaProgramacionIncial;
            AplicacionPlanTrabajoActividad.FechaEstado = EDAplicacionPlanTrabajoActividad.FechaProgramacionIncial;
            AplicacionPlanTrabajoActividad.Estado = 1;
            AplicacionPlanTrabajoActividad.Descripcion = EDAplicacionPlanTrabajoActividad.Descripcion;
            AplicacionPlanTrabajoActividad.Observaciones = EDAplicacionPlanTrabajoActividad.Observaciones;
            AplicacionPlanTrabajoActividad.ResponsableNombre = EDAplicacionPlanTrabajoActividad.ResponsableNombre;
            AplicacionPlanTrabajoActividad.ResponsableDocumento = EDAplicacionPlanTrabajoActividad.ResponsableDocumento;
            AplicacionPlanTrabajoActividad.ResponsableTipoDocumento = "N/A";


            using (SG_SSTContext context = new SG_SSTContext())
            {
                context.Tbl_AplicacionPlanTrabajoActividad.Add(AplicacionPlanTrabajoActividad);
                try
                {
                    context.SaveChanges();
                    guardar = true;
                }
                catch (Exception ex)
                {
                }
            }
            return guardar;
        }
        public bool actualizaractividad(EDAplicacionPlanTrabajoActividad EDAplicacionPlanTrabajoActividad)
        {
            bool guardar = false;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                var Listavar = (from s in context.Tbl_AplicacionPlanTrabajoActividad
                                where s.Pk_Id_PlanTrabajoActividad == EDAplicacionPlanTrabajoActividad.Pk_Id_PlanTrabajoActividad && s.Fk_Id_PlanTrabajoDetalle == EDAplicacionPlanTrabajoActividad.Fk_Id_PlanTrabajoDetalle
                                select s).FirstOrDefault<AplicacionPlanTrabajoActividad>();
                if (Listavar != null)
                {
                    Listavar.Descripcion = EDAplicacionPlanTrabajoActividad.Descripcion;
                    Listavar.Observaciones = EDAplicacionPlanTrabajoActividad.Observaciones;
                    Listavar.ResponsableNombre = EDAplicacionPlanTrabajoActividad.ResponsableNombre;
                    Listavar.ResponsableDocumento = EDAplicacionPlanTrabajoActividad.ResponsableDocumento;

                    if (EDAplicacionPlanTrabajoActividad.FechaEstado!=DateTime.MinValue)
                    {
                        if (EDAplicacionPlanTrabajoActividad.FechaEstado!= Listavar.FechaEstado)
                        {
                            //Listavar.FechaProgramacionIncial = Listavar.FechaEstado;
                            Listavar.FechaEstado = EDAplicacionPlanTrabajoActividad.FechaEstado;
                            Listavar.Estado = 2;
                        }
                    }

                    if (EDAplicacionPlanTrabajoActividad.Estado==3)
                    {
                        Listavar.Estado = 3;
                    }
                    if (EDAplicacionPlanTrabajoActividad.Estado == 5)
                    {
                        if (Listavar.FechaEstado!= Listavar.FechaProgramacionIncial)
                        {
                            Listavar.Estado = 2;
                        }
                        else
                        {
                            Listavar.Estado = 1;
                        }
                    }

                    context.Entry(Listavar).State = EntityState.Modified;
                    try
                    {
                        context.SaveChanges();
                        guardar = true;
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            return guardar;
        }
        public List<EDAplicacionPlanTrabajo> ObtenerPlanesDeTrabajoFiltro(int idEmpresa, string Fantes, string Fdespues, int vigencia, int sede, string Tipo)
        {
            DateTime FechaA_conv = DateTime.MinValue;
            DateTime FechaD_conv = DateTime.MinValue;

            if (Fantes != null && Fdespues != null)
            {
                if (Fantes != string.Empty && Fdespues != string.Empty)
                {
                    try
                    {
                        FechaA_conv = DateTime.Parse(Fantes);
                        FechaD_conv = DateTime.Parse(Fdespues);
                    }
                    catch (Exception)
                    {
                    }
                }
            }



            List<EDAplicacionPlanTrabajo> plan = null;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                plan = (from pl in context.Tbl_AplicacionPlanTrabajo
                        join sd in context.Tbl_Sede on pl.Fk_Id_Sede equals sd.Pk_Id_Sede
                        where sd.Fk_Id_Empresa == idEmpresa
                        select new EDAplicacionPlanTrabajo
                        {
                            Pk_Id_PlanTrabajo = pl.Pk_Id_PlanTrabajo,
                            NombreSede = sd.Nombre_Sede,
                            FechaInicio = pl.FechaInicio,
                            FechaFinal = pl.FechaFinal,
                            Vigencia = pl.Vigencia,
                            Fk_Id_Sede = pl.Fk_Id_Sede,
                            Tipo = pl.Tipo,
                            FechaAplicacion = pl.FechaAplicacion
                        }).ToList();
            }

            List<EDAplicacionPlanTrabajo> planFiltro = new List<EDAplicacionPlanTrabajo>();
            if (plan!=null)
            {
                planFiltro = plan;
                if (vigencia!=0)
                {
                    planFiltro = planFiltro.Where(s => s.Vigencia == vigencia).ToList();
                }
                if (sede != 0)
                {
                    planFiltro = planFiltro.Where(s => s.Fk_Id_Sede == sede).ToList();
                }

                if (FechaA_conv != DateTime.MinValue && FechaD_conv != DateTime.MinValue)
                {
                    FechaD_conv = FechaD_conv.AddHours(23).AddMinutes(59).AddSeconds(59);
                    planFiltro = planFiltro.Where(s => s.FechaFinal >= FechaA_conv && s.FechaInicio <= FechaA_conv).ToList();
                    planFiltro = planFiltro.Where(s => s.FechaFinal >= FechaD_conv && s.FechaInicio <= FechaD_conv).ToList();
                }

                if (Tipo!=null)
                {
                    if (Tipo!="")
                    {
                        planFiltro = planFiltro.Where(s => s.Tipo == Tipo).ToList();
                    }
                }
            }

            return planFiltro;
        }
        public bool EditarPlan(EDAplicacionPlanTrabajo EDAplicacionPlanTrabajo)
        {
            bool guardar = false;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                var Listavar = (from s in context.Tbl_AplicacionPlanTrabajo
                                join d in context.Tbl_Sede on s.Fk_Id_Sede equals d.Pk_Id_Sede
                                join e in context.Tbl_Empresa on d.Fk_Id_Empresa equals e.Pk_Id_Empresa
                                where s.Pk_Id_PlanTrabajo == EDAplicacionPlanTrabajo.Pk_Id_PlanTrabajo 
                                select s).FirstOrDefault<AplicacionPlanTrabajo>();
                if (Listavar != null)
                {
                    Listavar.RepLegalDocumento = EDAplicacionPlanTrabajo.RepLegalDocumento;
                    Listavar.RepLegalImagen = EDAplicacionPlanTrabajo.RepLegalImagen;
                    Listavar.RepLegalRuta = EDAplicacionPlanTrabajo.RepLegalRuta;
                    Listavar.RepLegalNombre = EDAplicacionPlanTrabajo.RepLegalNombre;

                    Listavar.RepSGSSTDocumento = EDAplicacionPlanTrabajo.RepSGSSTDocumento;
                    Listavar.RepSGSSTImagen = EDAplicacionPlanTrabajo.RepSGSSTImagen;
                    Listavar.RepSGSSTRuta = EDAplicacionPlanTrabajo.RepSGSSTRuta;
                    Listavar.RepSGSSTNombre = EDAplicacionPlanTrabajo.RepSGSSTNombre;

                    context.Entry(Listavar).State = EntityState.Modified;
                    try
                    {
                        context.SaveChanges();
                        guardar = true;
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            return guardar;
        }
        public bool EliminarPlanDeTrabajo(int Pk_Id_PlanTrabajo)
        {
            List<AplicacionPlanTrabajoDetalle> objetivos = new List<AplicacionPlanTrabajoDetalle>();
            bool respuetaEliminar = false;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                objetivos = (from s in context.Tbl_AplicacionPlanTrabajoDetalle
                             where s.Fk_Id_PlanTrabajo == Pk_Id_PlanTrabajo
                             select s).ToList();

                if (objetivos.Count() < 1)
                {
                    AplicacionPlanTrabajo plan = context.Tbl_AplicacionPlanTrabajo.Find(Pk_Id_PlanTrabajo);
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        RegistraLog registraLog = new RegistraLog();
                        try
                        {

                            context.Tbl_AplicacionPlanTrabajo.Remove(plan);
                            context.SaveChanges();
                            transaction.Commit();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            registraLog.RegistrarError(typeof(AplicacionPlanTrabajo), string.Format("Error al eliminar el plan  de trabajo en la base de datos  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                            transaction.Rollback();
                            return false;
                        }
                    }

                }

            }
            return respuetaEliminar;
        }
        public bool EliminarObjetivoPlanDeTrabajo(int Pk_Id_ObjetivoPlanTrabajo)
        {
            List<AplicacionPlanTrabajoActividad> actividades = new List<AplicacionPlanTrabajoActividad>();
            bool respuetaEliminar = false;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                actividades = (from s in context.Tbl_AplicacionPlanTrabajoActividad
                               where s.Fk_Id_PlanTrabajoDetalle == Pk_Id_ObjetivoPlanTrabajo
                               select s).ToList();

                if (actividades.Count() < 1)
                {
                    AplicacionPlanTrabajoDetalle objetivo = context.Tbl_AplicacionPlanTrabajoDetalle.Find(Pk_Id_ObjetivoPlanTrabajo);
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        RegistraLog registraLog = new RegistraLog();
                        try
                        {

                            context.Tbl_AplicacionPlanTrabajoDetalle.Remove(objetivo);
                            context.SaveChanges();
                            transaction.Commit();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            registraLog.RegistrarError(typeof(AplicacionPlanTrabajo), string.Format("Error al eliminar el objetivo del  plan  de trabajo en la base de datos  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                            transaction.Rollback();
                            return false;
                        }
                    }

                }

            }
            return respuetaEliminar;
        }
        public bool EliminarActividadPlanDeTrabajo(int Pk_Id_ActividadPlanTrabajo)
        {
            bool respuetaEliminar = false;
            List<EDAplicacionPlanTrabajoProgramacion> ListaProgramacion1 = ListaProgramacion(Pk_Id_ActividadPlanTrabajo);
            if (ListaProgramacion1!=null)
            {
                if (ListaProgramacion1.Count>0)
                {
                    return respuetaEliminar;
                }
            }

            using (SG_SSTContext context = new SG_SSTContext())
            {

                using (var transaction = context.Database.BeginTransaction())
                {
                    AplicacionPlanTrabajoActividad objetivo = context.Tbl_AplicacionPlanTrabajoActividad.Find(Pk_Id_ActividadPlanTrabajo);
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {

                        context.Tbl_AplicacionPlanTrabajoActividad.Remove(objetivo);
                        context.SaveChanges();
                        transaction.Commit();
                        respuetaEliminar = true;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(AplicacionPlanTrabajo), string.Format("Error al eliminar el objetivo del  plan  de trabajo en la base de datos  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        transaction.Rollback();
                        respuetaEliminar = false;
                    }
                }



            }
            return respuetaEliminar;
        }
        public bool EliminarProgramaPlanDeTrabajo(int Pk_Id_ProgramaPlanTrabajo)
        {
            bool respuetaEliminar = false;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    AplicacionPlanTrabajoProgramacion objetivo = context.Tbl_AplicacionPlanTrabajoProgramacion.Find(Pk_Id_ProgramaPlanTrabajo);
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        context.Tbl_AplicacionPlanTrabajoProgramacion.Remove(objetivo);
                        context.SaveChanges();
                        transaction.Commit();
                        respuetaEliminar = true;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(AplicacionPlanTrabajo), string.Format("Error al eliminar el objetivo del  plan  de trabajo en la base de datos  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        transaction.Rollback();
                        respuetaEliminar = false;
                    }
                }
            }
            return respuetaEliminar;
        }
        public bool crearprograma(EDAplicacionPlanTrabajoProgramacion EDAplicacionPlanTrabajoProgramacion)
        {
            bool guardar = false;
            AplicacionPlanTrabajoProgramacion AplicacionPlanTrabajoProgramacion = new AplicacionPlanTrabajoProgramacion();

            AplicacionPlanTrabajoProgramacion.Fk_Id_PlanTrabajoActividad = EDAplicacionPlanTrabajoProgramacion.Fk_Id_PlanTrabajoActividad;
            AplicacionPlanTrabajoProgramacion.FechaProgramacionIncial = EDAplicacionPlanTrabajoProgramacion.FechaProgramacionIncial;
            AplicacionPlanTrabajoProgramacion.FechaEstado = EDAplicacionPlanTrabajoProgramacion.FechaProgramacionIncial;
            AplicacionPlanTrabajoProgramacion.Estado = 1;
            AplicacionPlanTrabajoProgramacion.Observaciones = EDAplicacionPlanTrabajoProgramacion.Observaciones;

            using (SG_SSTContext context = new SG_SSTContext())
            {
                context.Tbl_AplicacionPlanTrabajoProgramacion.Add(AplicacionPlanTrabajoProgramacion);
                try
                {
                    context.SaveChanges();
                    guardar = true;
                }
                catch (Exception ex)
                {
                }
            }
            return guardar;
        }
        public bool actualizarprograma(EDAplicacionPlanTrabajoProgramacion EDAplicacionPlanTrabajoProgramacion)
        {
            bool guardar = false;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                var Listavar = (from s in context.Tbl_AplicacionPlanTrabajoProgramacion
                                where s.Pk_Id_AplicacionPlanTrabajoProgramacion == EDAplicacionPlanTrabajoProgramacion.Pk_Id_AplicacionPlanTrabajoProgramacion && s.Fk_Id_PlanTrabajoActividad == EDAplicacionPlanTrabajoProgramacion.Fk_Id_PlanTrabajoActividad
                                select s).FirstOrDefault<AplicacionPlanTrabajoProgramacion>();
                if (Listavar != null)
                {

                    Listavar.Observaciones = EDAplicacionPlanTrabajoProgramacion.Observaciones;

                    if (EDAplicacionPlanTrabajoProgramacion.FechaEstado != DateTime.MinValue)
                    {
                        if (EDAplicacionPlanTrabajoProgramacion.FechaEstado != Listavar.FechaEstado)
                        {
                            //Listavar.FechaProgramacionIncial = Listavar.FechaEstado;
                            Listavar.FechaEstado = EDAplicacionPlanTrabajoProgramacion.FechaEstado;
                            Listavar.Estado = 2;
                        }
                    }

                    if (EDAplicacionPlanTrabajoProgramacion.Estado == 3)
                    {
                        Listavar.Estado = 3;
                    }
                    if (EDAplicacionPlanTrabajoProgramacion.Estado == 5)
                    {
                        if (Listavar.FechaEstado != Listavar.FechaProgramacionIncial)
                        {
                            Listavar.Estado = 2;
                        }
                        else
                        {
                            Listavar.Estado = 1;
                        }
                    }

                    context.Entry(Listavar).State = EntityState.Modified;
                    try
                    {
                        context.SaveChanges();
                        guardar = true;
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            return guardar;
        }
        public List<EDAplicacionPlanTrabajoProgramacion> ListaProgramacion(int fkIdActividad)
        {
            List<EDAplicacionPlanTrabajoProgramacion> ListaProgramacion = new List<EDAplicacionPlanTrabajoProgramacion>();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                var Listavar = (from s in context.Tbl_AplicacionPlanTrabajoProgramacion
                                where s.Fk_Id_PlanTrabajoActividad == fkIdActividad
                                select s).ToList<AplicacionPlanTrabajoProgramacion>();
                if (Listavar!=null)
                {
                    foreach (var item in Listavar)
                    {
                        EDAplicacionPlanTrabajoProgramacion Programa = new EDAplicacionPlanTrabajoProgramacion();
                        Programa.Pk_Id_AplicacionPlanTrabajoProgramacion = item.Pk_Id_AplicacionPlanTrabajoProgramacion;
                        Programa.FechaProgramacionIncial = item.FechaProgramacionIncial;
                        Programa.FechaEstado = item.FechaEstado;
                        Programa.Estado = item.Estado;
                        Programa.Observaciones = item.Observaciones;
                        Programa.Fk_Id_PlanTrabajoActividad = item.Fk_Id_PlanTrabajoActividad;
                        ListaProgramacion.Add(Programa);
                    }
                }
            }
            return ListaProgramacion;
        }
    }
}
