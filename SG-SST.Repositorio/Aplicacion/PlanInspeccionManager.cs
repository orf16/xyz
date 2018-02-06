using SG_SST.EntidadesDominio.Aplicacion;
using SG_SST.Models;
using SG_SST.Models.Aplicacion;
using SG_SST.Interfaces.Aplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.Audotoria;
using SG_SST.Models.PlanCapacitacion;
using System.Data.Entity;
using SG_SST.EntidadesDominio.Planificacion;
using System.Configuration;


namespace SG_SST.Repositorio.Aplicacion
{
    public class PlanInspeccionManager : IplanInspeccion
    {
       
        /// <summary>
        /// Guarda en base de datos la  planeacion de una inspeccion.
        /// 
        /// </summary>
        /// <param name="plaInspeccion"></param>
        /// <returns></returns>
        public EDPlanInspeccion GuardarPlaneacion(EDPlanInspeccion plainspeccione)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        PlaneacionInspeccion pli = new PlaneacionInspeccion()
                        {
                            Responsable_Tipo_Inspeccion = plainspeccione.responsable,
                            Fk_Id_Maestro_Tipo_Inspeccion = plainspeccione.DescripcionTipoInspeccion,
                            Fecha = plainspeccione.Fecha,
                            IdEmpresa = plainspeccione.idEmpresaED,
                            EstadoPlaneacion = "Sin Ejecutar",
                            ConsecutivoPlan = plainspeccione.ConsecutivoPlanED,
                        };
                        context.Tbl_Planeacion_Inspeccion.Add(pli);


                        context.SaveChanges();
                        Transaction.Commit();
                        plainspeccione.Idplaninspeccion = pli.Pk_Id_PlaneacionInspeccion;
                        return plainspeccione;
                    }

                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(PlanInspeccionManager), string.Format("Error al registrar la Planeación  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

        }


        public EDInspeccion GuardarInspeccion(EDInspeccion inspeccion)
        {

            List<EDInspeccion> inspeccions = new List<EDInspeccion>();
            List<Inspecciones> INSP = new List<Inspecciones>();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        Inspecciones insp = new Inspecciones()
                        {
                            Fecha_Realizacion = inspeccion.EDFechaRealizacion,
                            Fk_IdEmpresa = Convert.ToInt32(inspeccion.EDfkEmpresa),
                            Fk_Id_PlaneacionInspeccion = Convert.ToInt32(inspeccion.EDfkIdPlaneacionInspeccion),
                            Descripcion_Tipo_Inspeccion = inspeccion.EDDescribeinspeccion,
                            Fk_Id_Sede = Convert.ToInt32(inspeccion.EDsede),
                            Fk_Id_Proceso = Convert.ToInt32(inspeccion.EDproceso),
                            Area_Lugar = inspeccion.EDarealugar,
                            Hora = inspeccion.EDhora,
                            Responsable_Lugar = inspeccion.EDresponsableLugar,
                            Id_Inspeccion = Convert.ToInt32(inspeccion.EDConsecutivo),
                            Estado_Inspeccion = 1,

                        };

                        //var consulta = (from s in context.Tbl_Plan_Empresa
                        //                where s.IdSede == insp.Fk_Id_Sede
                        //                select s).FirstOrDefault();
                                
                        //context.Tbl_Plan_Empresa_Actividad.Add(new PlanEmpresaActividad
                        //{
                        //    pk_plan_empresa=consulta.pk_id_plan_empresa,
                        //    Actividad = insp.Descripcion_Tipo_Inspeccion,
                        //    Responsable = insp.Responsable_Lugar,
                        //    FechaProg = insp.Fecha_Realizacion.ToString().Substring(0, 10),
                        //    HoraProgIni=insp.Hora,
                        //    Estado="P"

                        //});
                        context.SaveChanges();

                        //Guarda los Asistentes por Inspeccion
                        List<Asistentes> assis = new List<Asistentes>();
                        foreach (var ap in inspeccion.Asistentes)
                        {
                            Asistentes aasit = new Asistentes();
                            aasit.Nombre_Asistente = ap.NombreAsistente;
                            assis.Add(aasit);
                        }
                        context.Tbl_AsistentesInspeccion.AddRange(assis);
                        context.Tbl_Inspecciones.Add(insp);
                        context.SaveChanges();
                        inspeccion.EDpkinspeccion = insp.Pk_Id_Inspecciones;


                        List<AsistentesporInspeccion> lapi = new List<AsistentesporInspeccion>();
                        foreach (var ai in assis)
                        {

                            AsistentesporInspeccion api = new AsistentesporInspeccion();
                            api.Fk_Id_Asistente = ai.Pk_Id_Asistente;
                            api.Fk_Id_Inspeccion = insp.Pk_Id_Inspecciones;
                            lapi.Add(api);

                        }
                        context.Tbl_AsistentesporInspeccion.AddRange(lapi);
                        context.SaveChanges();

                        if (inspeccion.EDElementos != null)
                        {
                            List<EHMInspecciones> Elementos = new List<EHMInspecciones>();
                            foreach (var ai in inspeccion.EDElementos)
                            {

                                EHMInspecciones api = new EHMInspecciones();
                                api.Fk_Id_AdmoEMH = ai.Pk_Id_AdmoEMH;
                                api.Fk_Id_Inspecciones = insp.Pk_Id_Inspecciones;
                                Elementos.Add(api);

                            }
                            context.Tbl_AdministracionEMHInspecciones.AddRange(Elementos);
                            context.SaveChanges();
                        }





                        //Guarda las configuraciones por Inspeccion 
                        List<ConfiguracionInspeccion> mcp = new List<ConfiguracionInspeccion>();
                        foreach (var cp in inspeccion.Configuraciones)
                        {
                            ConfiguracionInspeccion mscp = new ConfiguracionInspeccion();
                            mscp.DescripcionPrioridadConf = cp.Descripcion;
                            mscp.DiasDesdeConfig = cp.diasdesde;
                            mscp.DiasHastaConfig = cp.diashasta;
                            mcp.Add(mscp);
                        }
                        context.Tbl_Configuracion_Inspeccion.AddRange(mcp);
                        context.SaveChanges();

                        List<ConfiguracionporInspeccion> cfpin = new List<ConfiguracionporInspeccion>();
                        foreach (var cpins in mcp)
                        {
                            ConfiguracionporInspeccion cpic = new ConfiguracionporInspeccion();
                            cpic.Fk_Id_ConfiguracionInspeccion = cpins.Pk_Id_ConfiguracionInspeccion;
                            cpic.Fk_Id_Inspecciones = insp.Pk_Id_Inspecciones;
                            cfpin.Add(cpic);

                        }
                        context.Tbl_ConfiguracionPrioridadInspeccion.AddRange(cfpin);
                        context.SaveChanges();
                        Transaction.Commit();
                        var dinspeccion = (from ins in context.Tbl_Inspecciones orderby ins.Pk_Id_Inspecciones descending select ins);
                        inspeccion.EDpkinspeccion = dinspeccion.FirstOrDefault().Pk_Id_Inspecciones;

                        var list = dinspeccion.Select(s => new { s.Pk_Id_Inspecciones }).FirstOrDefault();///transformar un iEquryable a lista
                        var configuraciones = (from cf in context.Tbl_Configuracion_Inspeccion
                                               join cif in context.Tbl_ConfiguracionPrioridadInspeccion
                                                   on cf.Pk_Id_ConfiguracionInspeccion equals cif.Fk_Id_ConfiguracionInspeccion
                                               where cif.Fk_Id_Inspecciones == list.Pk_Id_Inspecciones

                                               select new EDConfiguracion()
                                    {
                                        idconfiguracion = cf.Pk_Id_ConfiguracionInspeccion,
                                        Descripcion = cf.DescripcionPrioridadConf,
                                        diasdesde = cf.DiasDesdeConfig,
                                        diashasta = cf.DiasHastaConfig
                                    }).ToList();
                        inspeccion.Configuraciones = configuraciones;
                        return inspeccion;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(PlanInspeccionManager), string.Format("Error al registrar la Inspeccion  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }
        }


        public List<EDTipoDePeligro> ObtenerTiposDePeligro()
        {
            List<EDTipoDePeligro> tdp = null;
            using (SG_SSTContext contex = new SG_SSTContext())
            {
                tdp = (from tp in contex.Tbl_Tipo_De_Peligro
                       select new EDTipoDePeligro
                       {
                           PK_Tipo_De_Peligro = tp.PK_Tipo_De_Peligro,
                           Descripcion_Del_Peligro = tp.Descripcion_Del_Peligro
                       }).ToList();           
            }
            return tdp;
        }


        public EDPlanAccionInspeccion GuardarPlanAccion(EDPlanAccionInspeccion plan)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {

                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        PlanAccionInspeccion planaccioni = new PlanAccionInspeccion()
                        {
                            Pk_Id_PlanAcccionInspeccion = plan.PkPlanAccionInspeccionED,
                            Actividad_Plan_Accion = plan.ActividadPlanAccionInspeccionED,
                            Responsable_Plan_Accion = plan.ResponsablePlanAccionED,
                            Fecha_Fin_Plan_Accion = plan.FechaFinPlanAccionED,
                            Fecha_Cierre_Plan=plan.FechaCierrePlanAccionED,
                            Estado = 1,
                        };
                        context.Tbl_PlanAccionInspeccion.Add(planaccioni);
                        context.SaveChanges();
                        var placci = (from plai in context.Tbl_PlanAccionInspeccion orderby plai.Pk_Id_PlanAcccionInspeccion descending select plai);
                        var list = placci.Select(s => new { s.Pk_Id_PlanAcccionInspeccion }).FirstOrDefault();///transformar un iEquryable a lista
                        var condicionI = 0;
                        int contador = 0;
                        var inspeccionC = 0;
                        List<PlanaccionPorCondicion> lapi = new List<PlanaccionPorCondicion>();
                        foreach (var ai in plan.Condiciones)
                        {
                            PlanaccionPorCondicion po = new PlanaccionPorCondicion();
                            po.Fk_Id_CondicionInsegura = ai.EDpkCondicion;
                            var condicion = (from cdc in context.CondicionInsegura
                                             where po.Fk_Id_CondicionInsegura == cdc.Pk_Id_CondicionInsegura
                                             select cdc);
                            condicion.First().Estado_Condicion = 0;
                            po.Fk_Id_PlanAcccionInspeccion = list.Pk_Id_PlanAcccionInspeccion;
                            lapi.Add(po);
                            condicionI = condicion.FirstOrDefault().Pk_Id_CondicionInsegura;
                            contador++;
                        }
                        context.Tbl_PlanAccionporCondicion.AddRange(lapi);

                        var InspeccionT = (from i in context.CondicionInsegura
                                           join cii in context.Tbl_CondicionesInseguraporasInspeccion on i.Pk_Id_CondicionInsegura
                                           equals cii.Fk_Id_CondicionInsegura
                                           where condicionI == i.Pk_Id_CondicionInsegura
                                           select cii.Fk_Id_Inspecciones
                                             );

                        inspeccionC = InspeccionT.FirstOrDefault();

                        var totalInspeccion = (from i in context.CondicionInsegura
                                               join cii in context.Tbl_CondicionesInseguraporasInspeccion on i.Pk_Id_CondicionInsegura
                                               equals cii.Fk_Id_CondicionInsegura
                                               where inspeccionC == cii.Fk_Id_Inspecciones
                                               select i
                                         ).Count();


                        var totalestado = (from i in context.CondicionInsegura
                                           join cii in context.Tbl_CondicionesInseguraporasInspeccion on i.Pk_Id_CondicionInsegura
                                           equals cii.Fk_Id_CondicionInsegura
                                           where inspeccionC == cii.Fk_Id_Inspecciones & i.Estado_Condicion == 0
                                           select i
                                         ).Count();

                        if (totalInspeccion == (contador + totalestado))
                        {
                            var InspeccionUpdate = (from i in context.Tbl_Inspecciones
                                                    where i.Pk_Id_Inspecciones == InspeccionT.FirstOrDefault()
                                                    select i);

                            InspeccionUpdate.FirstOrDefault().Estado_Inspeccion = 0;

                            var plant = 0;
                            plant = InspeccionUpdate.FirstOrDefault().Fk_Id_PlaneacionInspeccion;
                            var planinspeccion = (from pi in context.Tbl_Planeacion_Inspeccion
                                                  where plant == pi.Pk_Id_PlaneacionInspeccion
                                                  select pi);
                            planinspeccion.FirstOrDefault().EstadoPlaneacion = "Ejecutada";
                        }
                        context.SaveChanges();
                        Transaction.Commit();
                        return plan;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(PlanInspeccionManager), string.Format("Error al registrar el Plan de Accion  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

        }
        public List<EDPlanAccionCorrectiva> GuardarPlanAccionCorrectiva(List<EDPlanAccionCorrectiva> planescorrectivos)
        {

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        List<PlanAccionCorrectiva> pacrr = new List<PlanAccionCorrectiva>();

                        foreach (var lista in planescorrectivos)
                        {
                            PlanAccionCorrectiva pacorr = new PlanAccionCorrectiva();

                            pacorr.Pk_Plan_Accion_Correctiva = lista.FkPlaAccionED;
                            pacorr.Adjunto_Seguimiento = lista.AdjuntoSeguimientoED;
                            pacorr.Nombre_Verificador = lista.NombreVerificadorED;
                            pacorr.Respuesta = lista.RespuestaED;
                            pacorr.Fk_Id_PlanAcccionInspeccion = lista.FkPlaAccionED;
                            pacrr.Add(pacorr);

                        }
                        context.Tbl_PlanAccionCorrectiva.AddRange(pacrr);
                        context.SaveChanges();
                        Transaction.Commit();
                        return planescorrectivos;

                    }
                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(PlanInspeccionManager), string.Format("Error al registrar la Informacion {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }



                }


            }
        }

        public EDCondicionInsegura GuardarCondicionesInspeccion(EDCondicionInsegura condicion)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        CondicionInsegura ci = new CondicionInsegura()
                        {

                            Descripcion_Condicion = condicion.EDDescribeCondicion,
                            UbicacionEspecificaInspeccion = condicion.EDUbicacionespecifica,
                            RiesgoPeligroIdentificado = condicion.EDRiesgopeligro,
                            DescripcionRiesgoIdentificado = condicion.EDClasificacionRiesgo,
                            Evidencia = condicion.EDEvidenciacondicion,
                            PKConfiguracionPrioridadInspeccion = condicion.EDConfiguracioncondicion,
                            OtroTipoPeligro=condicion.EDOtraClasePeligro,
                            Estado_Condicion = 1,

                        };
                        context.CondicionInsegura.Add(ci);
                        context.SaveChanges();
                        //var cond = (from cins in context.Tbl_Inspecciones orderby cins.Pk_Id_Inspecciones descending select cins).FirstOrDefault();

                        context.Tbl_CondicionesInseguraporasInspeccion.Add(new CondicionesInsegurasporInspeccion
                        {
                            Fk_Id_CondicionInsegura = ci.Pk_Id_CondicionInsegura,
                            Fk_Id_Inspecciones = condicion.EDPkInspeccion,

                        });
                        context.SaveChanges();
                        Transaction.Commit();
                        condicion.EDpkCondicion = ci.Pk_Id_CondicionInsegura;
                        return condicion;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(PlanInspeccionManager), string.Format("Error al registrar la Condicion Insegura  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }

                }

            }
        }


        public EDInspeccion ObtenerInfoInspeccion(int Idinspeccion, int Idcondicion)
        {

            using (SG_SSTContext context = new SG_SSTContext())
            {
                EDInspeccion inspeccion = new EDInspeccion();
                using (var Transaction = context.Database.BeginTransaction())
                {
                    List<EDAsistente> asistentes = new List<EDAsistente>();
                    List<EDCondicionInsegura> condiciones = new List<EDCondicionInsegura>();
                    List<EDConfiguracion> configuraciones = new List<EDConfiguracion>();
                    List<EDAdmoEMH> elementos = new List<EDAdmoEMH>();
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        condiciones = (from ci in context.CondicionInsegura
                                       join cipi in context.Tbl_CondicionesInseguraporasInspeccion
                                           on ci.Pk_Id_CondicionInsegura equals cipi.Fk_Id_CondicionInsegura
                                       join cfi in context.Tbl_Configuracion_Inspeccion on
                                        ci.PKConfiguracionPrioridadInspeccion equals cfi.Pk_Id_ConfiguracionInspeccion
                                       where cipi.Fk_Id_Inspecciones == Idinspeccion
                                       select new EDCondicionInsegura()
                                       {
                                           EDpkCondicion = ci.Pk_Id_CondicionInsegura,
                                           EDDescribeCondicion = ci.Descripcion_Condicion,
                                           EDUbicacionespecifica = ci.UbicacionEspecificaInspeccion,
                                           EDRiesgopeligro = ci.RiesgoPeligroIdentificado,
                                           EDClasificacionRiesgo = ci.DescripcionRiesgoIdentificado,
                                           EDEvidenciacondicion = ci.Evidencia,
                                           EDConfiguracioncondicion = ci.PKConfiguracionPrioridadInspeccion,
                                           EDEstadoCondicion = ci.Estado_Condicion,
                                           EDescribePrioridad = cfi.DescripcionPrioridadConf,
                                           EDDiasDesde = cfi.DiasDesdeConfig,
                                           EDDiasHasta = cfi.DiasHastaConfig,
                                       }).ToList();
                        elementos = (from ele in context.Tbl_AdministracionEMH
                                     join eleins in context.Tbl_AdministracionEMHInspecciones
                                         on ele.Pk_Id_AdmoEMH equals eleins.Fk_Id_AdmoEMH
                                     join i in context.Tbl_Inspecciones
                                         on eleins.Fk_Id_Inspecciones equals i.Pk_Id_Inspecciones
                                     where i.Pk_Id_Inspecciones == Idinspeccion
                                     select new EDAdmoEMH()
                                     {
                                         Pk_Id_AdmoEMH = ele.Pk_Id_AdmoEMH,
                                         TipoElemento = ele.TipoElemento,
                                         NombreElemento = ele.NombreElemento,
                                         Marca = ele.Marca,
                                     }).ToList();
                        asistentes = (from ai in context.Tbl_AsistentesInspeccion
                                      join api in context.Tbl_AsistentesporInspeccion
                                          on ai.Pk_Id_Asistente equals api.Fk_Id_Asistente
                                      join i in context.Tbl_Inspecciones
                                          on api.Fk_Id_Inspeccion equals i.Pk_Id_Inspecciones
                                      where i.Pk_Id_Inspecciones == Idinspeccion
                                      select new EDAsistente()
                                      {
                                          Idasistente = ai.Pk_Id_Asistente,
                                          NombreAsistente = ai.Nombre_Asistente,
                                      }).ToList();
                        inspeccion = (from ins in context.Tbl_Inspecciones
                                      join pli in context.Tbl_Planeacion_Inspeccion
                                      on ins.Fk_Id_PlaneacionInspeccion equals pli.Pk_Id_PlaneacionInspeccion
                                      join mti in context.Tbl_Maestro_Planeación_Inspeccion on
                                      pli.Fk_Id_Maestro_Tipo_Inspeccion equals mti.Pk_Id_Maestro_Tipo_Inspeccion
                                      join sede in context.Tbl_Sede on ins.Fk_Id_Sede equals sede.Pk_Id_Sede
                                      join empresa in context.Tbl_Empresa on
                                      sede.Fk_Id_Empresa equals empresa.Pk_Id_Empresa
                                      join proceso in context.Tbl_Procesos
                                      on ins.Fk_Id_Proceso equals proceso.Pk_Id_Proceso

                                      where ins.Pk_Id_Inspecciones == Idinspeccion
                                      select new EDInspeccion()
                                      {
                                          EDpkinspeccion = ins.Pk_Id_Inspecciones,
                                          EDarealugar = ins.Area_Lugar,
                                          EDhora = ins.Hora,
                                          EDDescribeinspeccion = ins.Descripcion_Tipo_Inspeccion,
                                          EDdescribesede = sede.Nombre_Sede,
                                          EDDescripcionTipoI = mti.Descripcion_Tipo_Inspeccion,
                                          EDResponsablePlaneacion = pli.Responsable_Tipo_Inspeccion,
                                          EDresponsableLugar = ins.Responsable_Lugar,
                                          EDFechaPlaneacionIns = pli.Fecha,
                                          EDFechaRealizacion = ins.Fecha_Realizacion,
                                          EDNitEmpresa = empresa.Nit_Empresa,
                                          EDDescribeEmpresa = empresa.Razon_Social,
                                          EDConsecutivo = ins.Id_Inspeccion,
                                          EDdescribeProceso = proceso.Descripcion_Proceso,
                                          EDEstadoInspeccion = ins.Estado_Inspeccion,
                                      }).FirstOrDefault();
                        inspeccion.Asistentes = asistentes;
                        inspeccion.CondicionesIns = condiciones;
                        inspeccion.EDElementos = elementos;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(PlanInspeccionManager), string.Format("Error al Obtener la Información  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;

                    }
                }
                return inspeccion;

            }


        }


        public EDCondicionInsegura ObtenerCondicionInsegura(int Idcondicion, int idInspeccion)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                List<EDTipoDePeligro> peligros = new List<EDTipoDePeligro>();
                List<EDClasificacionDePeligro> Clasificacion = new List<EDClasificacionDePeligro>();
                List<EDConfiguracion> configuraciones = new List<EDConfiguracion>();
                EDCondicionInsegura condicion = new EDCondicionInsegura();
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {

                        condicion = (from Ci in context.CondicionInsegura
                                     join paipc in context.Tbl_PlanAccionporCondicion on Ci.Pk_Id_CondicionInsegura
                                     equals paipc.Fk_Id_CondicionInsegura
                                     join pai in context.Tbl_PlanAccionInspeccion on paipc.Fk_Id_PlanAcccionInspeccion
                                     equals pai.Pk_Id_PlanAcccionInspeccion
                                     where Ci.Pk_Id_CondicionInsegura == Idcondicion
                                     select new EDCondicionInsegura()
                                     {
                                         EDpkCondicion = Ci.Pk_Id_CondicionInsegura,
                                         EDDescribeCondicion = Ci.Descripcion_Condicion,
                                         EDUbicacionespecifica = Ci.UbicacionEspecificaInspeccion,
                                         EDEstadoCondicion = Ci.Estado_Condicion,
                                         EDClasificacionRiesgo = Ci.RiesgoPeligroIdentificado,
                                         EDRiesgopeligro = Ci.DescripcionRiesgoIdentificado,
                                         EDConfiguracioncondicion = Ci.PKConfiguracionPrioridadInspeccion,
                                         EDOtraClasePeligro=Ci.OtroTipoPeligro,
                                         EDEstadoPlanAccion=pai.Estado
                                     }).FirstOrDefault();

                        peligros = (from tp in context.Tbl_Tipo_De_Peligro
                                    
                                    select new EDTipoDePeligro
                                    {
                                        PK_Tipo_De_Peligro = tp.PK_Tipo_De_Peligro,
                                        Descripcion_Del_Peligro = tp.Descripcion_Del_Peligro
                                    }).ToList();
                        condicion.Peligros = peligros;


                        Clasificacion = (from cp in context.Tbl_Clasificacion_De_Peligro
                                         select new EDClasificacionDePeligro {                                     
                                             IdClasificacionDePeligro=cp.PK_Clasificacion_De_Peligro,
                                             DescripcionClaseDePeligro=cp.Descripcion_Clase_De_Peligro,
                                         }).ToList();
                        condicion.ClasificacionPeligros = Clasificacion;


                        configuraciones = (from ci in context.Tbl_Configuracion_Inspeccion
                                           join cfii in context.Tbl_ConfiguracionPrioridadInspeccion
                                           on ci.Pk_Id_ConfiguracionInspeccion equals cfii.Fk_Id_ConfiguracionInspeccion
                                           where cfii.Fk_Id_Inspecciones == idInspeccion

                                           select new EDConfiguracion

                                           {
                                               idconfiguracion = ci.Pk_Id_ConfiguracionInspeccion,
                                               Descripcion = ci.DescripcionPrioridadConf,
                                               diasdesde = ci.DiasDesdeConfig,
                                               diashasta = ci.DiasHastaConfig

                                           }).ToList();
                        condicion.Configuraciones = configuraciones;

                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(PlanInspeccionManager), string.Format("Error al Obtener la Información  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;

                    }

                }
                return condicion;
            }


        }
        public List<EDConfiguracion> ObtenerConfiguracionesPorIns(int idi)
        {
            List<EDConfiguracion> configspi = new List<EDConfiguracion>();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {

                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        configspi = (from cf in context.Tbl_Configuracion_Inspeccion
                                     join cfi in context.Tbl_ConfiguracionPrioridadInspeccion
                                     on cf.Pk_Id_ConfiguracionInspeccion equals cfi.Fk_Id_ConfiguracionInspeccion
                                     join i in context.Tbl_Inspecciones
                                     on cfi.Fk_Id_Inspecciones equals i.Pk_Id_Inspecciones
                                     where i.Pk_Id_Inspecciones == idi
                                     select new EDConfiguracion()
                                     {
                                         idconfiguracion = cf.Pk_Id_ConfiguracionInspeccion,
                                         Descripcion = cf.DescripcionPrioridadConf,
                                         diasdesde = cf.DiasDesdeConfig,
                                         diashasta = cf.DiasHastaConfig
                                     }).ToList();
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(PlanInspeccionManager), string.Format("Error al Obtener la Información  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;

                    }

                }


            }
            return configspi;



        }

        public bool EliminarPlaneacion(int idplaneacion)
        {
            var result = false;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        var Eliminado = (from plin in context.Tbl_Planeacion_Inspeccion
                                         where plin.Pk_Id_PlaneacionInspeccion == idplaneacion
                                         select plin).FirstOrDefault();
                        if (Eliminado != null)
                        {
                            context.Tbl_Planeacion_Inspeccion.Remove(Eliminado);

                        }
                        context.SaveChanges();
                        result = true;
                        Transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(PlanInspeccionManager), string.Format("Error al Eliminar la Planeación  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return result;
                    }
                }
            }
            return result;
        }

        public bool EliminarCondicion(int Idcondicion)
        {
            var result = false;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        var condicioneliminar=(from cie in context.CondicionInsegura
                                                   where cie.Pk_Id_CondicionInsegura==Idcondicion && cie.Estado_Condicion==1 select cie).SingleOrDefault();

                        if (condicioneliminar!=null)
                        {

                            context.CondicionInsegura.Remove(condicioneliminar);
                        }

                       
                        context.SaveChanges();
                        Transaction.Commit();
                        result = true;
                    }
                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(PlanInspeccionManager), string.Format("Error al Eliminar la Inspeccion  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return result;
                    }
                }
            }

            return result;

        }
        public bool EliminarInspeccion(int idinspeccion, int idplaneacion)
        {
            var result = false;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        var Eliminado = (from i in context.Tbl_Inspecciones
                                         where i.Pk_Id_Inspecciones == idinspeccion && i.Fk_Id_PlaneacionInspeccion == idplaneacion
                                         select i).FirstOrDefault();
                        if (Eliminado != null)
                        {
                            context.Tbl_Inspecciones.Remove(Eliminado);
                        }
                        var asistentesElim = (from ai in context.Tbl_AsistentesInspeccion
                                              join api in context.Tbl_AsistentesporInspeccion
                                              on ai.Pk_Id_Asistente equals api.Fk_Id_Asistente
                                              where api.Fk_Id_Inspeccion == idinspeccion
                                              select ai).ToList();
                        if (asistentesElim != null)
                        {
                            context.Tbl_AsistentesInspeccion.RemoveRange(asistentesElim);
                        }
                        var planEliminado = (from plin in context.Tbl_Planeacion_Inspeccion
                                             where plin.Pk_Id_PlaneacionInspeccion == idplaneacion
                                             select plin).FirstOrDefault();
                        if (planEliminado != null)
                        {
                            context.Tbl_Planeacion_Inspeccion.Remove(planEliminado);
                        }
                        var configuracionesInspeccion = (from cfi in context.Tbl_Configuracion_Inspeccion
                                                         join cfpi in context.Tbl_ConfiguracionPrioridadInspeccion
                                                         on cfi.Pk_Id_ConfiguracionInspeccion equals cfpi.Fk_Id_ConfiguracionInspeccion
                                                         where cfpi.Fk_Id_Inspecciones == idinspeccion
                                                         select cfi).ToList();
                        if (configuracionesInspeccion != null)
                        {
                            context.Tbl_Configuracion_Inspeccion.RemoveRange(configuracionesInspeccion);
                        }

                        var elementosI = (from elementos in context.Tbl_AdministracionEMHInspecciones
                                          where elementos.Fk_Id_Inspecciones == idinspeccion
                                          select elementos).ToList();

                        if (elementosI != null)
                        {
                            context.Tbl_AdministracionEMHInspecciones.RemoveRange(elementosI);
                        }

                        var condicionesInsegurasInspeccion = (from cdsi in context.CondicionInsegura
                                                              join cdspi in context.Tbl_CondicionesInseguraporasInspeccion
                                                                  on cdsi.Pk_Id_CondicionInsegura equals cdspi.Fk_Id_CondicionInsegura
                                                              where cdspi.Fk_Id_Inspecciones == idinspeccion

                                                              select cdsi).ToList();
                        if (condicionesInsegurasInspeccion != null)
                        {
                            context.CondicionInsegura.RemoveRange(condicionesInsegurasInspeccion);
                        }
                        else 
                        {
                            return false;
                        }

                        context.SaveChanges();
                        Transaction.Commit();
                        result = true;
                    }
                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(PlanInspeccionManager), string.Format("Error al Eliminar la Inspeccion  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return result;
                    }
                }
            }

            return result;
        }

        public EDInspeccion EjecutarPlan(int consecutivo, string responsable, string fecha, string descripciontipoinspeccion, int id)
        {
            EDInspeccion resultado = new EDInspeccion();
            using (SG_SSTContext context = new SG_SSTContext())
            {

                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        resultado = (from ins in context.Tbl_Inspecciones
                                     join pli in context.Tbl_Planeacion_Inspeccion
                                         on ins.Fk_Id_PlaneacionInspeccion equals pli.Pk_Id_PlaneacionInspeccion
                                     where pli.ConsecutivoPlan == consecutivo & ins.Fk_IdEmpresa == id
                                     select new EDInspeccion
                                     {
                                         EDpkinspeccion = ins.Pk_Id_Inspecciones,
                                         EDarealugar = ins.Area_Lugar,
                                         EDfkIdPlaneacionInspeccion = ins.Fk_Id_PlaneacionInspeccion,
                                         EDhora = ins.Hora,
                                         EDDescripcionTipoI = ins.Descripcion_Tipo_Inspeccion,
                                         EDsede = ins.Fk_Id_Sede,
                                         EDproceso = ins.Fk_Id_Proceso,
                                         EDConsecutivo = ins.Id_Inspeccion,
                                         EDfkEmpresa = ins.Fk_IdEmpresa,
                                         EDresponsableLugar = ins.Responsable_Lugar,
                                     }).FirstOrDefault();
                    }
                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(PlanInspeccionManager), string.Format("Error al registrar la Condicion Insegura  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }

                }


            }
            return resultado;

        }

        public EDPlanInspeccion ContinuarEjecucionPlan(int consecutivo, string responsable, string fecha, string descripciontipoinspeccion, int pkplan, int id)
        {
            EDPlanInspeccion resultado = new EDPlanInspeccion();
            using (SG_SSTContext context = new SG_SSTContext())
            {

                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        resultado = (from ins in context.Tbl_Inspecciones
                                     join pli in context.Tbl_Planeacion_Inspeccion
                                         on ins.Fk_Id_PlaneacionInspeccion equals pli.Pk_Id_PlaneacionInspeccion
                                     join mti in context.Tbl_Maestro_Planeación_Inspeccion on pli.Fk_Id_Maestro_Tipo_Inspeccion equals mti.Pk_Id_Maestro_Tipo_Inspeccion
                                     where pli.ConsecutivoPlan == consecutivo
                                     & ins.Fk_IdEmpresa == id
                                     & ins.Id_Inspeccion == pli.ConsecutivoPlan
                                     & ins.Fk_Id_PlaneacionInspeccion == pkplan
                                     select new EDPlanInspeccion
                                     {
                                         ConsecutivoPlanED = pli.ConsecutivoPlan,
                                         responsable = pli.Responsable_Tipo_Inspeccion,
                                         Describetipoinspeccion = mti.Descripcion_Tipo_Inspeccion,
                                         Fecha = pli.Fecha,




                                     }).FirstOrDefault();
                    }
                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(PlanInspeccionManager), string.Format("Error al registrar la Condicion Insegura  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }

                }


            }
            return resultado;

        }










        public List<EDPlanInspeccion> ObtenerplaneacionPorEmpresa(int id)
        {
            List<EDPlanInspeccion> Planeaciones = new List<EDPlanInspeccion>();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        Planeaciones = (from pli in context.Tbl_Planeacion_Inspeccion
                                        where pli.IdEmpresa == id
                                        select new EDPlanInspeccion
                                        {
                                            EstadoPlaneacionED = pli.EstadoPlaneacion,
                                            ConsecutivoPlanED = pli.ConsecutivoPlan,
                                        }).ToList();
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(PlanInspeccionManager), string.Format("Error al registrar la Modificacion de la Condicion Insegura  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return Planeaciones;
        }

        public EDInspeccion ObtenerInspeccionNoEjecutada(int id, int idp, int idi)
        {
            EDInspeccion InspeccionNoE = new EDInspeccion();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {

                        List<EDAsistente> asistentes = new List<EDAsistente>();
                        List<EDCondicionInsegura> condiciones = new List<EDCondicionInsegura>();
                        List<EDConfiguracion> configuraciones = new List<EDConfiguracion>();
                        List<EDAdmoEMH> elementos = new List<EDAdmoEMH>();

                        asistentes = (from ai in context.Tbl_AsistentesInspeccion
                                      join api in context.Tbl_AsistentesporInspeccion
                                          on ai.Pk_Id_Asistente equals api.Fk_Id_Asistente
                                      join i in context.Tbl_Inspecciones
                                          on api.Fk_Id_Inspeccion equals i.Pk_Id_Inspecciones
                                      where i.Pk_Id_Inspecciones == idi
                                      select new EDAsistente()
                                      {
                                          Idasistente = ai.Pk_Id_Asistente,
                                          NombreAsistente = ai.Nombre_Asistente,
                                      }).ToList();

                        configuraciones = (from cf in context.Tbl_Configuracion_Inspeccion
                                           join cfi in context.Tbl_ConfiguracionPrioridadInspeccion
                                           on cf.Pk_Id_ConfiguracionInspeccion equals cfi.Fk_Id_ConfiguracionInspeccion
                                           join i in context.Tbl_Inspecciones
                                           on cfi.Fk_Id_Inspecciones equals i.Pk_Id_Inspecciones
                                           where i.Pk_Id_Inspecciones == idi
                                           select new EDConfiguracion()
                                           {
                                               idconfiguracion = cf.Pk_Id_ConfiguracionInspeccion,
                                               Descripcion = cf.DescripcionPrioridadConf,
                                               diasdesde = cf.DiasDesdeConfig,
                                               diashasta = cf.DiasHastaConfig
                                           }).ToList();

                        elementos = (from ele in context.Tbl_AdministracionEMH
                                     join eleins in context.Tbl_AdministracionEMHInspecciones
                                         on ele.Pk_Id_AdmoEMH equals eleins.Fk_Id_AdmoEMH
                                     join i in context.Tbl_Inspecciones
                                         on eleins.Fk_Id_Inspecciones equals i.Pk_Id_Inspecciones
                                     where i.Pk_Id_Inspecciones == idi
                                     select new EDAdmoEMH()
                                     {
                                         Pk_Id_AdmoEMH = ele.Pk_Id_AdmoEMH,
                                         TipoElemento = ele.TipoElemento,
                                         NombreElemento = ele.NombreElemento,
                                         Marca = ele.Marca,
                                     }).ToList();

                        condiciones = (from ci in context.CondicionInsegura
                                       join cipi in context.Tbl_CondicionesInseguraporasInspeccion
                                           on ci.Pk_Id_CondicionInsegura equals cipi.Fk_Id_CondicionInsegura
                                       join cfi in context.Tbl_Configuracion_Inspeccion on
                                        ci.PKConfiguracionPrioridadInspeccion equals cfi.Pk_Id_ConfiguracionInspeccion
                                       where cipi.Fk_Id_Inspecciones == idi && ci.Estado_Condicion == 1
                                       select new EDCondicionInsegura()
                                       {
                                           EDpkCondicion = ci.Pk_Id_CondicionInsegura,
                                           EDDescribeCondicion = ci.Descripcion_Condicion,
                                           EDUbicacionespecifica = ci.UbicacionEspecificaInspeccion,
                                           EDRiesgopeligro = ci.RiesgoPeligroIdentificado,
                                           EDClasificacionRiesgo = ci.DescripcionRiesgoIdentificado,
                                           EDEvidenciacondicion = ci.Evidencia,
                                           EDConfiguracioncondicion = ci.PKConfiguracionPrioridadInspeccion,
                                           EDEstadoCondicion = ci.Estado_Condicion,
                                           EDescribePrioridad = cfi.DescripcionPrioridadConf,
                                           EDDiasDesde = cfi.DiasDesdeConfig,
                                           EDDiasHasta = cfi.DiasHastaConfig,


                                       }).ToList();


                        InspeccionNoE = (from i in context.Tbl_Inspecciones
                                         join plin in context.Tbl_Planeacion_Inspeccion on
                                         i.Fk_Id_PlaneacionInspeccion equals plin.Pk_Id_PlaneacionInspeccion
                                         join mti in context.Tbl_Maestro_Planeación_Inspeccion
                                         on plin.Fk_Id_Maestro_Tipo_Inspeccion equals mti.Pk_Id_Maestro_Tipo_Inspeccion

                                         join sede in context.Tbl_Sede on i.Fk_Id_Sede equals sede.Pk_Id_Sede
                                         join emp in context.Tbl_Empresa on sede.Fk_Id_Empresa equals emp.Pk_Id_Empresa
                                         join pro in context.Tbl_Procesos on i.Fk_Id_Proceso equals pro.Pk_Id_Proceso
                                         where plin.IdEmpresa == id & i.Estado_Inspeccion == 1 & plin.Pk_Id_PlaneacionInspeccion == idp
                                         select new EDInspeccion
                                         {
                                             EDFechaRealizacion = i.Fecha_Realizacion,
                                             EDpkinspeccion = i.Pk_Id_Inspecciones,
                                             EDfkIdPlaneacionInspeccion = i.Fk_Id_PlaneacionInspeccion,
                                             EDhora = i.Hora,
                                             EDDescribeinspeccion = i.Descripcion_Tipo_Inspeccion,
                                             EDsede = i.Fk_Id_Sede,
                                             EDdescribesede = sede.Nombre_Sede,
                                             EDproceso = i.Fk_Id_Proceso,
                                             EDdescribeProceso = pro.Descripcion_Proceso,
                                             EDIdInspeccion = i.Id_Inspeccion,
                                             EDEstadoInspeccion = i.Estado_Inspeccion,
                                             EDfkEmpresa = i.Fk_IdEmpresa,
                                             EDDescribeEmpresa = emp.Razon_Social,
                                             EDarealugar = i.Area_Lugar,
                                             EDresponsableLugar = i.Responsable_Lugar,
                                             EDNitEmpresa = emp.Nit_Empresa,
                                             EDResponsablePlaneacion = plin.Responsable_Tipo_Inspeccion,
                                             EDFechaPlaneacion = plin.Fecha.ToString(),
                                             EDidEmpresa = plin.IdEmpresa,
                                             EDEStadoPlaneacion = plin.EstadoPlaneacion,
                                             EDConsecutivoPlaneacion = plin.ConsecutivoPlan,
                                             EDDescripcionTipoI = mti.Descripcion_Tipo_Inspeccion,
                                         }).FirstOrDefault();
                        InspeccionNoE.Asistentes = asistentes;
                        InspeccionNoE.Configuraciones = configuraciones;
                        InspeccionNoE.EDElementos = elementos;
                        InspeccionNoE.CondicionesIns = condiciones;

                    }

                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(PlanInspeccionManager), string.Format("Error al registrar la Modificacion de la Condicion Insegura  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return InspeccionNoE;
        }





        public List<EDInspeccion> ObtenerplaneacionPorEmpresase(int id)
        {
            List<EDInspeccion> Planeacionse = new List<EDInspeccion>();
            List<EDAsistente> Asistente = new List<EDAsistente>();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        Planeacionse = (from plin in context.Tbl_Planeacion_Inspeccion
                                        join mti in context.Tbl_Maestro_Planeación_Inspeccion on plin.Fk_Id_Maestro_Tipo_Inspeccion
                                         equals mti.Pk_Id_Maestro_Tipo_Inspeccion
                                        join i in context.Tbl_Inspecciones on
                                         plin.Pk_Id_PlaneacionInspeccion equals i.Fk_Id_PlaneacionInspeccion
                                         into sr
                                        from x in sr.DefaultIfEmpty()
                                        join pro in context.Tbl_Procesos on x.Fk_Id_Proceso equals pro.Pk_Id_Proceso
                                         into s
                                        from y in s.DefaultIfEmpty()
                                        where plin.IdEmpresa == id && plin.EstadoPlaneacion == "Sin Ejecutar"
                                        select new EDInspeccion
                                           {
                                               EDpkinspeccion = x.Pk_Id_Inspecciones,
                                               EDfkIdPlaneacionInspeccion = plin.Pk_Id_PlaneacionInspeccion,
                                               EDhora = x.Hora,
                                               EDDescribeinspeccion = x.Descripcion_Tipo_Inspeccion,
                                               EDsede = x.Fk_Id_Sede,
                                               //EDdescribesede = sede.Nombre_Sede,
                                               EDproceso = x.Fk_Id_Proceso,
                                               EDdescribeProceso = y.Descripcion_Proceso,
                                               EDIdInspeccion = x.Id_Inspeccion,
                                               EDEstadoInspeccion = x.Estado_Inspeccion,
                                               EDfkEmpresa = x.Fk_IdEmpresa,
                                               EDarealugar = x.Area_Lugar,
                                               EDresponsableLugar = x.Responsable_Lugar,
                                               EDResponsablePlaneacion = plin.Responsable_Tipo_Inspeccion,
                                               EDFechaPlaneacionIns = plin.Fecha,
                                               EDidEmpresa = plin.IdEmpresa,
                                               EDEStadoPlaneacion = plin.EstadoPlaneacion,
                                               EDConsecutivoPlaneacion = plin.ConsecutivoPlan,
                                               EDDescripcionTipoI = mti.Descripcion_Tipo_Inspeccion
                                           }).ToList();
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(PlanInspeccionManager), string.Format("Error al registrar la Modificacion de la Condicion Insegura  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return Planeacionse;

        }


        public List<EDInspeccion> ObtenerInspeccionPorEmpresa(int id)
        {
            List<EDInspeccion> Inspecciones = new List<EDInspeccion>();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        Inspecciones = (from ei in context.Tbl_Inspecciones
                                        join sede in context.Tbl_Sede
                                        on ei.Fk_Id_Sede equals sede.Pk_Id_Sede
                                        where sede.Fk_Id_Empresa == id
                                        select new EDInspeccion
                                        {
                                            EDIdInspeccion = ei.Id_Inspeccion,
                                        }).ToList();



                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(PlanInspeccionManager), string.Format("Error al registrar la Modificacion de la Condicion Insegura  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }


                }


            }

            return Inspecciones;

        }





        public EDCondicionInsegura EditarCondicion(EDCondicionInsegura condicion)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        CondicionInsegura edit = new CondicionInsegura();
                        edit = (from ci in context.CondicionInsegura
                                where ci.Pk_Id_CondicionInsegura == condicion.EDpkCondicion
                                select ci
                                      ).FirstOrDefault();

                        edit.Descripcion_Condicion = condicion.EDDescribeCondicion;
                        edit.PKConfiguracionPrioridadInspeccion = condicion.EDConfiguracioncondicion;
                        edit.DescripcionRiesgoIdentificado = condicion.EDClasificacionRiesgo;
                        edit.UbicacionEspecificaInspeccion = condicion.EDUbicacionespecifica;
                        edit.RiesgoPeligroIdentificado = condicion.EDRiesgopeligro;
                        edit.Evidencia = condicion.EDEvidenciacondicion;
                        edit.OtroTipoPeligro = condicion.EDOtraClasePeligro;
                       
                        context.SaveChanges();
                        
                        //context.Entry(edit).State = EntityState.Modified;
                        Transaction.Commit();

                        //var buscareditado = (from ci in context.CondicionInsegura
                        //                     where ci.Pk_Id_CondicionInsegura == condicion.EDpkCondicion
                        //                     select new EDCondicionInsegura
                        //    {
                        //        EDDescribeCondicion = ci.Descripcion_Condicion,
                        //        EDConfiguracioncondicion = ci.PKConfiguracionPrioridadInspeccion,
                        //        EDClasificacionRiesgo = ci.DescripcionRiesgoIdentificado,
                        //        EDUbicacionespecifica = ci.UbicacionEspecificaInspeccion,
                        //        EDRiesgopeligro = ci.RiesgoPeligroIdentificado,
                        //        EDEvidenciacondicion = ci.Evidencia,
                        //    }).FirstOrDefault();
                        return condicion;

                    }

                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(PlanInspeccionManager), string.Format("Error al registrar la Modificacion de la Condicion Insegura  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }

                }

            }




        }

        public List<EDPlanAccionInspeccion> ObtenerInspeccionPorfechaEstado(int idsede, string FechaIn, string FechaFin)
        {
            List<EDPlanAccionInspeccion> oipf = new List<EDPlanAccionInspeccion>();
            using (SG_SSTContext contexto = new SG_SSTContext())
            {
                RegistraLog registra = new RegistraLog();
                try
                {
                    DateTime dt = Convert.ToDateTime(FechaFin);
                    DateTime dta = Convert.ToDateTime(FechaIn);
                  
                    oipf = (from oip in contexto.Tbl_PlanAccionInspeccion

                            join pi in contexto.Tbl_PlanAccionporCondicion on oip.Pk_Id_PlanAcccionInspeccion equals pi.Fk_Id_PlanAcccionInspeccion
                            //join apla in contexto.Tbl_Actividad_Plan_Accion on oip.Pk_Id_PlanAcccionInspeccion equals apla.Fk_Id_Actividad
                            join ci in contexto.CondicionInsegura on pi.Fk_Id_CondicionInsegura equals ci.Pk_Id_CondicionInsegura
                            join cipo in contexto.Tbl_CondicionesInseguraporasInspeccion on ci.Pk_Id_CondicionInsegura equals cipo.Fk_Id_CondicionInsegura
                            join inp in contexto.Tbl_Inspecciones on cipo.Fk_Id_Inspecciones equals inp.Pk_Id_Inspecciones
                            join plan in contexto.Tbl_Planeacion_Inspeccion on inp.Fk_Id_PlaneacionInspeccion equals plan.Pk_Id_PlaneacionInspeccion
                            where inp.Fk_Id_Sede == idsede & plan.Fecha >= dta & plan.Fecha <= dt
                            select new EDPlanAccionInspeccion
                            {
                                ActividadPlanAccionInspeccionED = oip.Actividad_Plan_Accion,
                                ResponsablePlanAccionED = oip.Responsable_Plan_Accion,
                                PkPlanAccionInspeccionED = oip.Pk_Id_PlanAcccionInspeccion,
                                EstadoIDED = oip.Estado,
                                FechaFinPlanAccionED = oip.Fecha_Fin_Plan_Accion,
                                ConsecutivoPlanInspeccionED = plan.ConsecutivoPlan,
                                FechaCierrePlanAccionED = oip.Fecha_Cierre_Plan,
                            }).Distinct().ToList();
  
                    foreach (var planaccionfecha in oipf)
                    {
                        if (planaccionfecha.FechaCierrePlanAccionED == null & planaccionfecha.FechaFinPlanAccionED >= DateTime.Now)
                        {
                            planaccionfecha.EstadoPlanAccionED = "Abierto-Vigente";
                        }
                        else 
                        { 
                            if(planaccionfecha.FechaCierrePlanAccionED == null & planaccionfecha.FechaFinPlanAccionED<=DateTime.Now)
                            {
                                planaccionfecha.EstadoPlanAccionED = "Abierto-Vencido";
                            }
                            else
                            {
                                if(planaccionfecha.FechaCierrePlanAccionED!=null & planaccionfecha.FechaFinPlanAccionED >= planaccionfecha.FechaCierrePlanAccionED)
                                {
                                    planaccionfecha.EstadoPlanAccionED = "Cerrado-Vigente";
                                }
                                else
                                {
                                    planaccionfecha.EstadoPlanAccionED = "Cerrado-Vencido";
                                }
                            }
                        }
                    }
                 
                }
                catch (Exception ex)
                {
                    registra.RegistrarError(typeof(PlanInspeccionManager), string.Format("Error al obtener la Información  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                    return null;
                }
            }
            
            return oipf;
        }

       


        public List<EDInspeccion> ObtenerInspecciones(int idsede, string TipoInspeccion, DateTime? FechaIn, DateTime? FechaFin)
        {
            List<EDInspeccion> oiptipo = new List<EDInspeccion>();
            using (SG_SSTContext contexto = new SG_SSTContext())
            {
                RegistraLog registra = new RegistraLog();

                try
                {
                    //DateTime dta = Convert.ToDateTime(FechaIn);
                    //DateTime dt = Convert.ToDateTime(FechaFin);
                    oiptipo = (from plai in contexto.Tbl_PlanAccionInspeccion
                               join paporc in contexto.Tbl_PlanAccionporCondicion on plai.Pk_Id_PlanAcccionInspeccion
                               equals paporc.Fk_Id_PlanAcccionInspeccion
                               join ci in contexto.CondicionInsegura on paporc.Fk_Id_CondicionInsegura
                               equals ci.Pk_Id_CondicionInsegura
                               join cipori in contexto.Tbl_CondicionesInseguraporasInspeccion on ci.Pk_Id_CondicionInsegura
                               equals cipori.Fk_Id_CondicionInsegura
                               join i in contexto.Tbl_Inspecciones on cipori.Fk_Id_Inspecciones
                               equals i.Pk_Id_Inspecciones
                               join sede in contexto.Tbl_Sede on i.Fk_Id_Sede
                               equals sede.Pk_Id_Sede
                               join pli in contexto.Tbl_Planeacion_Inspeccion on i.Fk_Id_PlaneacionInspeccion
                               equals pli.Pk_Id_PlaneacionInspeccion
                               join mti in contexto.Tbl_Maestro_Planeación_Inspeccion on pli.Fk_Id_Maestro_Tipo_Inspeccion
                               equals mti.Pk_Id_Maestro_Tipo_Inspeccion
                               where sede.Pk_Id_Sede == idsede
                               & mti.Descripcion_Tipo_Inspeccion == (TipoInspeccion ?? mti.Descripcion_Tipo_Inspeccion)
                               & (pli.Fecha >= (FechaIn ?? pli.Fecha) & pli.Fecha <= (FechaFin ?? pli.Fecha))
                               select new EDInspeccion
                               {
                                   EDFechaRealizacion = i.Fecha_Realizacion,
                                   EDFechaPlaneacionIns = pli.Fecha,
                                   EDDescribeinspeccion = i.Descripcion_Tipo_Inspeccion,
                                   EDsede = i.Fk_Id_Sede,
                                   EDpkTipoI = mti.Pk_Id_Maestro_Tipo_Inspeccion,
                                   EDDescripcionTipoI = mti.Descripcion_Tipo_Inspeccion,
                                   EDdescribesede = sede.Nombre_Sede,
                                   EDpkCondicion = ci.Pk_Id_CondicionInsegura,
                                   EDDescribeCondicion = ci.Descripcion_Condicion,
                                   EDplanAccionInspeccion = plai.Pk_Id_PlanAcccionInspeccion,
                                   EstadoIdED = plai.Estado,
                                   EDpkinspeccion = i.Pk_Id_Inspecciones,
                                   EDResponsablePlaneacion = pli.Responsable_Tipo_Inspeccion,
                                   FechaCierrePlanED=plai.Fecha_Cierre_Plan,
                                   FechaFinPlanED=plai.Fecha_Fin_Plan_Accion,
                               }).ToList();
                       foreach (var planaccionfecha in oiptipo)
                       {
                           if (planaccionfecha.FechaCierrePlanED == null & planaccionfecha.FechaFinPlanED >= DateTime.Now)
                           {
                               planaccionfecha.EDEstadoPlanAccionInspeccion = "Abierto-Vigente";
                           }
                           else
                           {
                               if (planaccionfecha.FechaCierrePlanED == null & planaccionfecha.FechaFinPlanED <= DateTime.Now)
                               {
                                   planaccionfecha.EDEstadoPlanAccionInspeccion = "Abierto-Vencido";
                               }
                               else
                               {
                                   if (planaccionfecha.FechaCierrePlanED != null & planaccionfecha.FechaFinPlanED >= planaccionfecha.FechaCierrePlanED)
                                   {
                                       planaccionfecha.EDEstadoPlanAccionInspeccion = "Cerrado-Vigente";
                                   }
                                   else
                                   {
                                       planaccionfecha.EDEstadoPlanAccionInspeccion = "Cerrado-Vencido";
                                   }
                               }
                           }
                       }

                    //foreach (var planaccionfecha in oiptipo)
                    //{
                    //    if (planaccionfecha.EstadoIdED == 1)
                    //    { planaccionfecha.EDEstadoPlanAccionInspeccion = "Vigente"; }
                    //    else { planaccionfecha.EDEstadoPlanAccionInspeccion = "Vencida"; }

                    //}

                }
                catch (Exception ex)
                {
                    registra.RegistrarError(typeof(PlanInspeccionManager), string.Format("Error al obtener la Información  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                    return null;
                }
            }

            return oiptipo;
        }


        public EDPlanInspeccion ObtenerPlanInspeccion()
        {
            EDPlanInspeccion epi = new EDPlanInspeccion();
            using (SG_SSTContext contexto = new SG_SSTContext())
            {
                RegistraLog registra = new RegistraLog();
                try
                {
                    epi = (from i in contexto.Tbl_Planeacion_Inspeccion
                           join inp in contexto.Tbl_Maestro_Planeación_Inspeccion on i.Fk_Id_Maestro_Tipo_Inspeccion equals inp.Pk_Id_Maestro_Tipo_Inspeccion
                           orderby i.Pk_Id_PlaneacionInspeccion descending
                           select new EDPlanInspeccion
                           {
                               Idplaninspeccion = i.Pk_Id_PlaneacionInspeccion,
                               responsable = i.Responsable_Tipo_Inspeccion,
                               descripcion = inp.Descripcion_Tipo_Inspeccion,
                               Fecha = i.Fecha,
                               ConsecutivoPlanED = i.ConsecutivoPlan,
                               Idtipoinspeccion = inp.Pk_Id_Maestro_Tipo_Inspeccion,
                           }).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    registra.RegistrarError(typeof(PlanInspeccionManager), string.Format("Error al obtener la Informacion {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                    return null;
                }
            }
            return epi;
        }

        public List<EDPlanAccionCorrectiva> ObtenerCorrectivas(int IdEmpresa)
        {
            List<EDPlanAccionCorrectiva> pac = new List<EDPlanAccionCorrectiva>();
            using (SG_SSTContext contexto = new SG_SSTContext())
            {
                using (var Transaction = contexto.Database.BeginTransaction())
                {
                    RegistraLog registra = new RegistraLog();
                    try
                    {
                        pac = (from cins in contexto.Tbl_PlanAccionCorrectiva
                               join pai in contexto.Tbl_PlanAccionInspeccion on cins.Fk_Id_PlanAcccionInspeccion equals pai.Pk_Id_PlanAcccionInspeccion
                               join i in contexto.Tbl_PlanAccionporCondicion on pai.Pk_Id_PlanAcccionInspeccion equals i.Fk_Id_PlanAcccionInspeccion
                               join cis in contexto.CondicionInsegura on i.Fk_Id_CondicionInsegura equals cis.Pk_Id_CondicionInsegura
                               join cisi in contexto.Tbl_CondicionesInseguraporasInspeccion on cis.Pk_Id_CondicionInsegura equals cisi.Fk_Id_CondicionInsegura
                               join inps in contexto.Tbl_Inspecciones on cisi.Fk_Id_Inspecciones equals inps.Pk_Id_Inspecciones
                               join pro in contexto.Tbl_Procesos on inps.Fk_Id_Proceso equals pro.Pk_Id_Proceso
                               join sede in contexto.Tbl_Sede on inps.Fk_Id_Sede equals sede.Pk_Id_Sede
                               join emp in contexto.Tbl_Empresa on sede.Fk_Id_Empresa equals emp.Pk_Id_Empresa 
                               where cins.Respuesta == "SI" & emp.Pk_Id_Empresa==IdEmpresa
                               select new EDPlanAccionCorrectiva
                        {


                            PkplanAccionCorrectivaED = cins.Pk_Plan_Accion_Correctiva,
                            RespuestaED = cins.Respuesta,
                            InformacionActividadED = pai.Actividad_Plan_Accion,
                            PksedeED = inps.Fk_Id_Sede,
                            nombresedeED = sede.Nombre_Sede,
                            PkprocesoED = inps.Fk_Id_Proceso,
                            NombreProcesoED = pro.Descripcion_Proceso,

                        }).Distinct().ToList();
                    }

                    catch (Exception ex)
                    {
                        registra.RegistrarError(typeof(PlanInspeccionManager), string.Format("Error al obtener la Información  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        return null;

                    }


                }
                return pac;

            }
        }


        public List<EDPlanAccionCorrectiva> ObtenerTodasCorrectivas(int IdEmpresa)
        {
            List<EDPlanAccionCorrectiva> pact = new List<EDPlanAccionCorrectiva>();
            using (SG_SSTContext contexto = new SG_SSTContext())
            {
                using (var Transaction = contexto.Database.BeginTransaction())
                {
                    RegistraLog registra = new RegistraLog();
                    try
                    {
                        pact = (from cins in contexto.Tbl_PlanAccionCorrectiva
                                join pai in contexto.Tbl_PlanAccionInspeccion on cins.Fk_Id_PlanAcccionInspeccion equals pai.Pk_Id_PlanAcccionInspeccion
                                join i in contexto.Tbl_PlanAccionporCondicion on pai.Pk_Id_PlanAcccionInspeccion equals i.Fk_Id_PlanAcccionInspeccion
                                join cis in contexto.CondicionInsegura on i.Fk_Id_CondicionInsegura equals cis.Pk_Id_CondicionInsegura
                                join cisi in contexto.Tbl_CondicionesInseguraporasInspeccion on cis.Pk_Id_CondicionInsegura equals cisi.Fk_Id_CondicionInsegura
                                join inps in contexto.Tbl_Inspecciones on cisi.Fk_Id_Inspecciones equals inps.Pk_Id_Inspecciones
                                join pro in contexto.Tbl_Procesos on inps.Fk_Id_Proceso equals pro.Pk_Id_Proceso
                                join sede in contexto.Tbl_Sede on inps.Fk_Id_Sede equals sede.Pk_Id_Sede
                                join emp in contexto.Tbl_Empresa on sede.Fk_Id_Empresa equals emp.Pk_Id_Empresa
                                where emp.Pk_Id_Empresa==IdEmpresa
                                select new EDPlanAccionCorrectiva
                                {
                                    PkplanAccionCorrectivaED = cins.Pk_Plan_Accion_Correctiva,
                                    RespuestaED = cins.Respuesta,
                                    NombreVerificadorED = cins.Nombre_Verificador,
                                    InformacionActividadED = pai.Actividad_Plan_Accion,
                                    PksedeED = inps.Fk_Id_Sede,
                                    nombresedeED = sede.Nombre_Sede,
                                    PkprocesoED = inps.Fk_Id_Proceso,
                                    NombreProcesoED = pro.Descripcion_Proceso,
                                    DescribeCondicionED=cis.Descripcion_Condicion,
                                }).ToList();
                    }

                    catch (Exception ex)
                    {
                        registra.RegistrarError(typeof(PlanInspeccionManager), string.Format("Error al obtener la Información  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        return null;

                    }


                }
                return pact;

            }
        }


        public List<EDConfiguracion> ObtenerConfiguraciones()
        {
            List<EDConfiguracion> cpr = null;
            using (SG_SSTContext contexto = new SG_SSTContext())
            {
                try
                {
                    cpr = (from p in contexto.Tbl_Maestro_Configuracion_Prioridad
                           select new EDConfiguracion
                           {
                               idconfiguracion = p.Pk_Id_MaestroConfiguracion,
                               Descripcion = p.DescripcionPrioridad,
                               diasdesde = p.DiasDesde,
                               diashasta = p.DiasHasta
                           }).ToList();
                }
                catch
                {

                    return null;
                }
            }
            return cpr;
        }

        public List<EDConfiguracion> ObtenerConfiguracionesInspeccion()
        {
            List<EDConfiguracion> cpi = null;
            using (SG_SSTContext contexto = new SG_SSTContext())
            {
                RegistraLog registra = new RegistraLog();
                try
                {
                    var epi = (from i in contexto.Tbl_Inspecciones
                               orderby i.Pk_Id_Inspecciones descending
                               select i.Pk_Id_Inspecciones).FirstOrDefault();

                    cpi = (from p in contexto.Tbl_ConfiguracionPrioridadInspeccion
                           where p.Fk_Id_Inspecciones == epi
                           select new EDConfiguracion
                           {
                               idconfiguracion = p.Pk_Id_ConfiguracionPorInspeccion,
                               Descripcion = p.ConfiguracionInspeccion.DescripcionPrioridadConf,
                               diasdesde = p.ConfiguracionInspeccion.DiasDesdeConfig,
                               diashasta = p.ConfiguracionInspeccion.DiasHastaConfig
                           }).ToList();


                }
                catch (Exception ex)
                {

                    registra.RegistrarError(typeof(PlanInspeccionManager), string.Format("Error al obtener la Informacion {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                    return null;
                }

            }
            return cpi;
        }


        //Obtiene  las Condiciones Inseguras por Inspeccion
        public List<EDCondicionInsegura> ObtenerCondicionesPorInspeccion(int idinspeccion)
        {
            List<EDCondicionInsegura> condpi = null;
            using (SG_SSTContext contexto = new SG_SSTContext())
            {
                RegistraLog registra = new RegistraLog();
                try
                {


                 

                    List<EDCondicionInsegura> cs = new List<EDCondicionInsegura>();

                    condpi = (from ci in contexto.CondicionInsegura
                              join cipi in contexto.Tbl_CondicionesInseguraporasInspeccion
                                  on ci.Pk_Id_CondicionInsegura equals cipi.Fk_Id_CondicionInsegura
                              join cfi in contexto.Tbl_Configuracion_Inspeccion on
                               ci.PKConfiguracionPrioridadInspeccion equals cfi.Pk_Id_ConfiguracionInspeccion
                              where cipi.Fk_Id_Inspecciones == idinspeccion && ci.Estado_Condicion == 1
                                  where cipi.Fk_Id_Inspecciones == idinspeccion && ci.Estado_Condicion == 1

                              select new EDCondicionInsegura
                              {
                                  EDpkCondicion = ci.Pk_Id_CondicionInsegura,
                                  EDPkInspeccion=idinspeccion,
                                  EDConfiguracioncondicion = ci.PKConfiguracionPrioridadInspeccion,
                                  EDescribePrioridad = cfi.DescripcionPrioridadConf,
                                  EDDiasDesde = cfi.DiasDesdeConfig,
                                  EDDiasHasta = cfi.DiasHastaConfig,
                                  EDUbicacionespecifica = ci.UbicacionEspecificaInspeccion,
                                  EDDescribeCondicion = ci.Descripcion_Condicion,
                                  EDEvidenciacondicion=ci.Evidencia,

                              }).ToList();
                    cs.AddRange(condpi);
                    condpi = new List<EDCondicionInsegura>();
                    return cs;

                }
                catch (Exception ex)
                {

                    registra.RegistrarError(typeof(PlanInspeccionManager), string.Format("Error al obtener la Informacion {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                    return null;
                }

            }

        }

        //Obtiene los Planes de Accion Generados a las condiciones Inseguras de la Inspeccion
        public List<EDPlanAccionInspeccion> ObtenerPlanAccionInspeccion()
        {

            List<EDPlanAccionInspeccion> planaccion = null;
            using (SG_SSTContext contexto = new SG_SSTContext())
            {
                RegistraLog registra = new RegistraLog();
                try
                {
                 

                    var inspeccion = (from i in contexto.Tbl_Inspecciones orderby i.Pk_Id_Inspecciones descending select i).FirstOrDefault();
                    planaccion = (from pai in contexto.Tbl_PlanAccionInspeccion
                                  join pc in contexto.Tbl_PlanAccionporCondicion
                                  on pai.Pk_Id_PlanAcccionInspeccion equals pc.Fk_Id_PlanAcccionInspeccion
                                  join ci in contexto.CondicionInsegura on pc.Fk_Id_CondicionInsegura equals ci.Pk_Id_CondicionInsegura
                                  join cipi in contexto.Tbl_CondicionesInseguraporasInspeccion on ci.Pk_Id_CondicionInsegura equals cipi.Fk_Id_CondicionInsegura



                                  where ci.Estado_Condicion == 0 & cipi.Fk_Id_Inspecciones == inspeccion.Pk_Id_Inspecciones

                                  select new EDPlanAccionInspeccion
                                  {
                                      ActividadPlanAccionInspeccionED = pai.Actividad_Plan_Accion,
                                      ResponsablePlanAccionED = pai.Responsable_Plan_Accion,
                                      FechaFinPlanAccionED = pai.Fecha_Fin_Plan_Accion,
                                      PkPlanAccionInspeccionED = pai.Pk_Id_PlanAcccionInspeccion,
                                      EstadoIDED = pai.Estado,
                                      CondicionesInsegurasED = ci.Descripcion_Condicion,



                                  }).OrderByDescending(t => t.PkPlanAccionInspeccionED).ToList();
                    foreach (var planaccionfecha in planaccion)
                    {
                        if (planaccionfecha.FechaCierrePlanAccionED == null & planaccionfecha.FechaFinPlanAccionED >= DateTime.Now)
                        {
                            planaccionfecha.EstadoPlanAccionED = "Abierto-Vigente";
                        }
                        else
                        {
                            if (planaccionfecha.FechaCierrePlanAccionED == null & planaccionfecha.FechaFinPlanAccionED <= DateTime.Now)
                            {
                                planaccionfecha.EstadoPlanAccionED = "Abierto-Vencido";
                            }
                            else
                            {
                                if (planaccionfecha.FechaCierrePlanAccionED != null & planaccionfecha.FechaFinPlanAccionED >= planaccionfecha.FechaCierrePlanAccionED)
                                {
                                    planaccionfecha.EstadoPlanAccionED = "Cerrado-Vigente";
                                }
                                else
                                {
                                    planaccionfecha.EstadoPlanAccionED = "Cerrado-Vencido";
                                }
                            }
                        }
                    }
                  

                    return planaccion;
                }
                catch (Exception ex)
                {

                    registra.RegistrarError(typeof(PlanInspeccionManager), string.Format("Error al obtener la Informacion {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                    return null;
                }

            }

        }


    }




}




