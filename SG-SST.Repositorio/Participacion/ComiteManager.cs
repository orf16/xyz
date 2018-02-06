using SG_SST.Audotoria;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.Models;
using SG_SST.Models.Empresas;
using SG_SST.Models.Participacion;
using SG_SST.Interfaces.Participacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using SG_SST.EntidadesDominio.Participacion;

namespace SG_SST.Repositorio.Participacion
{
    public class ComiteManager:IComite
    {
        //OBTENER LA INFORMACION DE LA SEDE DE UNA EMPRESA
        public EDSede ObtenerInformacionSede(int idsede)
        {
            EDSede infosede = new EDSede();
            using (SG_SSTContext contexto = new SG_SSTContext())
            {
                RegistraLog registra = new RegistraLog();

                try
                {
                    infosede = (from Emp in contexto.Tbl_Empresa
                                join sede in contexto.Tbl_Sede
                                on Emp.Pk_Id_Empresa equals sede.Fk_Id_Empresa
                                where sede.Pk_Id_Sede==idsede
                            select new EDSede
                            {
                                NombreEmpresa = Emp.Razon_Social,
                                IDEmpresa = Emp.Nit_Empresa,
                                NombreSede=sede.Nombre_Sede,
                                DireccionSede=sede.Direccion_Sede,
                            }).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    registra.RegistrarError(typeof(ComiteManager), string.Format("Error al obtener la Información  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                    return null;
                }
            }

            return infosede;
        }

        //OBTENER LA INFORMACION DE UN ACTA COPASST
        public List<EDActasCopasst> ObtenerInformacionActaCopasst(int idsede)
        {
            List<EDActasCopasst> infoActaCopasst = null;

            using (SG_SSTContext contexto = new SG_SSTContext())
            {
                RegistraLog registra = new RegistraLog();

                try
                {
                    infoActaCopasst = (from Emp in contexto.Tbl_Empresa
                                join sede in contexto.Tbl_Sede
                                on Emp.Pk_Id_Empresa equals sede.Fk_Id_Empresa
                                join ac in contexto.Tbl_ActasCopasst
                                on sede.Pk_Id_Sede equals ac.Fk_Id_Sede
                                where sede.Pk_Id_Sede == idsede
                                       select new EDActasCopasst
                                {
                                    NombreEmpresa = Emp.Razon_Social,
                                    NombreSede = sede.Nombre_Sede,
                                    Consecutivo_Acta = ac.Consecutivo_Acta,
                                    Fecha = ac.Fecha,
                                    TemaReunion = ac.TemaReunion,
                                    NombreUsuario = ac.NombreUsuario,
                                    IdEmpresa = Emp.Pk_Id_Empresa,
                                    PK_Id_Acta = ac.PK_Id_Acta,
                                    NombreArchivo = ac.NombreArchivo,

                                }).OrderBy(x => x.Consecutivo_Acta).ToList();
                }
                catch (Exception ex)
                {
                    registra.RegistrarError(typeof(ComiteManager), string.Format("Error al obtener la Información  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                    return null;
                }
            }

            return infoActaCopasst;
        }

        //OBTENER LISTA DE TIPOS DE PRINCIPALES DE UN ACTA COPASST
        public List<EDTipoPrincipalActa> ObtenerTipoPrincipal()
        {
            List<EDTipoPrincipalActa> infoTipoPrincipal = null;

            using (SG_SSTContext contexto = new SG_SSTContext())
            {
                RegistraLog registra = new RegistraLog();

                try
                {
                    infoTipoPrincipal = (from doc in contexto.Tbl_TipoPrincipalActa
                                         select new EDTipoPrincipalActa
                                         {
                                             Id_TipoPrincipal = doc.PK_Id_TipoPrincipal,
                                             DescripcionTipoPrincipal = doc.DescripcionTipoPrincipal

                                         }).ToList();
                }
                catch (Exception ex)
                {
                    registra.RegistrarError(typeof(ComiteManager), string.Format("Error al obtener la Información  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                    return null;
                }
            }

            return infoTipoPrincipal;
        }

        //OBTENER ACTA COPASST POR EMPRESA
        public List<EDActasCopasst> ObtenerActasCopasstPorEmpresa(int id)
        {
            List<EDActasCopasst> ActasCopasst = null;

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        ActasCopasst = (from act in context.Tbl_ActasCopasst
                                        where act.Fk_Id_Empresa == id
                                        select new EDActasCopasst
                                        {
                                            Consecutivo_Acta = act.Consecutivo_Acta,
                                            PK_Id_Acta = act.PK_Id_Acta
                                        }).OrderBy(x => x.Consecutivo_Acta).ToList();
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al Obtener las actas copasst  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return ActasCopasst;
        }

        //OBTENER LA INFORMACION DE UN ACTA COPASST
        public EDActasCopasst ObtenerActasCopasstPorId(int Id_Acta)
        {
            EDActasCopasst ActasCopasst = null;

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        ActasCopasst = (from act in context.Tbl_ActasCopasst
                                        where act.PK_Id_Acta == Id_Acta
                                        select new EDActasCopasst
                                        {
                                            Consecutivo_Acta = act.Consecutivo_Acta,
                                            PK_Id_Acta = act.PK_Id_Acta,
                                            Fecha = act.Fecha,
                                            NombreEmpresa = act.NombreEmpresa,
                                            NombreArchivo = act.NombreArchivo,
                                            Fk_Id_Sede = act.Fk_Id_Sede,
                                            TemaReunion = act.TemaReunion,
                                            Conclusiones = act.Conclusiones,
                                        }).First();
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al Obtener el acta copasst  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return ActasCopasst;
        }

        //OBTENER LISTA DE TIPOS DE PRIORIDAD DE UN ACTA COPASST
        public List<EDTipoPrioridadActa> ObtenerTipoPrioridad()
        {
            List<EDTipoPrioridadActa> infoTipoPrioridad = null;

            using (SG_SSTContext contexto = new SG_SSTContext())
            {
                RegistraLog registra = new RegistraLog();

                try
                {
                    infoTipoPrioridad = (from doc in contexto.Tbl_TipoPrioridadMiembroComite
                                         select new EDTipoPrioridadActa
                                         {
                                             Id_TipoPrioridadMiembro = doc.PK_Id_TipoPrioridadMiembro,
                                             DescripcionTipoMiembro = doc.DescripcionTipoMiembro

                                         }).ToList();
                }
                catch (Exception ex)
                {
                    registra.RegistrarError(typeof(ComiteManager), string.Format("Error al obtener la Información  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                    return null;
                }
            }

            return infoTipoPrioridad;
        }

        //GUARDAR LOS MIEMBROS DE UN ACTA COPASST
        public EDMiembrosCopasst GuardarMiembrosActaCopasst(EDMiembrosCopasst MiembrosCop)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
               ActasCopasst acta = null;
               using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        var actaGuardada = (from m in context.Tbl_ActasCopasst
                                        where m.Consecutivo_Acta == MiembrosCop.Consecutivo_Acta
                                        && m.Fk_Id_Sede == MiembrosCop.IdSede
                                        select m).FirstOrDefault();

                        if (actaGuardada == null)
                        {
                            acta = new ActasCopasst()
                            {
                                Consecutivo_Acta = MiembrosCop.Consecutivo_Acta,
                                NombreUsuario = MiembrosCop.NombreUsuario,
                                Fk_Id_Empresa = MiembrosCop.IdEmpresa,
                                NombreEmpresa = MiembrosCop.NombreEmpresa,
                                Fk_Id_Sede = MiembrosCop.IdSede,
                                Fecha = DateTime.Today,
                            };
                            context.Tbl_ActasCopasst.Add(acta);

                            if (MiembrosCop.Fk_Id_TipoPrincipal == 0)
                            {
                                MiembrosActaCopasst miembro = new MiembrosActaCopasst()
                                {
                                    Numero_Documento = MiembrosCop.Numero_Documento,
                                    Nombre = MiembrosCop.Nombre,
                                    Fk_Id_TipoPrioridadMiembro = MiembrosCop.Fk_Id_TipoPrioridadMiembro,
                                    TipoRepresentante = MiembrosCop.TipoRepresentante,
                                };
                                context.Tbl_MiembrosActaCopasst.Add(miembro);
                            }
                            else
                            {
                                MiembrosActaCopasst miembro = new MiembrosActaCopasst()
                                {
                                    Numero_Documento = MiembrosCop.Numero_Documento,
                                    Nombre = MiembrosCop.Nombre,
                                    Fk_Id_TipoPrincipal = MiembrosCop.Fk_Id_TipoPrincipal,
                                    Fk_Id_TipoPrioridadMiembro = MiembrosCop.Fk_Id_TipoPrioridadMiembro,
                                    TipoRepresentante = MiembrosCop.TipoRepresentante,
                                };

                                context.Tbl_MiembrosActaCopasst.Add(miembro);
                            }

                            Participantes participante = new Participantes()
                            {
                                Numero_Documento = MiembrosCop.Numero_Documento,
                                Nombre = MiembrosCop.Nombre,
                            };

                            context.Tbl_Participantes.Add(participante);
                            
                            AuditoriaActaCopasst auditoria = new AuditoriaActaCopasst()
                            {

                                Fk_Id_UsuarioSistema = MiembrosCop.UsuarioSistema,
                                NombreUsuario = MiembrosCop.NombreUsuario,
                                Fecha = DateTime.Today,
                                AccionRealizada = "CREO EL ACTA COPASST",
                            };
                            context.Tbl_AuditoriaActaCopasst.Add(auditoria);


                        }
                        else
                        {
                            actaGuardada.NombreUsuario = MiembrosCop.NombreUsuario;
                            
                            var yaExiste = (from m in context.Tbl_MiembrosActaCopasst
                                            where m.Fk_Id_Acta == actaGuardada.PK_Id_Acta
                                            && m.Numero_Documento == MiembrosCop.Numero_Documento
                                            select m);

                            if (yaExiste.Count() == 0)
                            {
                                if (MiembrosCop.Fk_Id_TipoPrincipal == 0)
                                {
                                    MiembrosActaCopasst miembro = new MiembrosActaCopasst()
                                    {
                                        Numero_Documento = MiembrosCop.Numero_Documento,
                                        Nombre = MiembrosCop.Nombre,
                                        Fk_Id_TipoPrioridadMiembro = MiembrosCop.Fk_Id_TipoPrioridadMiembro,
                                        TipoRepresentante = MiembrosCop.TipoRepresentante,
                                        Fk_Id_Acta = actaGuardada.PK_Id_Acta,
                                    };
                                    context.Tbl_MiembrosActaCopasst.Add(miembro);

                                    Participantes participante = new Participantes()
                                    {
                                        Numero_Documento = MiembrosCop.Numero_Documento,
                                        Nombre = MiembrosCop.Nombre,
                                        Fk_Id_Acta = actaGuardada.PK_Id_Acta,
                                    };

                                    context.Tbl_Participantes.Add(participante);
                                }
                                else
                                {
                                    MiembrosActaCopasst miembro = new MiembrosActaCopasst()
                                    {
                                        Numero_Documento = MiembrosCop.Numero_Documento,
                                        Nombre = MiembrosCop.Nombre,
                                        Fk_Id_TipoPrincipal = MiembrosCop.Fk_Id_TipoPrincipal,
                                        Fk_Id_TipoPrioridadMiembro = MiembrosCop.Fk_Id_TipoPrioridadMiembro,
                                        TipoRepresentante = MiembrosCop.TipoRepresentante,
                                        Fk_Id_Acta = actaGuardada.PK_Id_Acta,
                                    };

                                    context.Tbl_MiembrosActaCopasst.Add(miembro);

                                    Participantes participante = new Participantes()
                                    {
                                        Numero_Documento = MiembrosCop.Numero_Documento,
                                        Nombre = MiembrosCop.Nombre,
                                        Fk_Id_Acta = actaGuardada.PK_Id_Acta,
                                    };

                                    context.Tbl_Participantes.Add(participante);

                                    AuditoriaActaCopasst auditoria = new AuditoriaActaCopasst()
                                    {

                                        Fk_Id_UsuarioSistema = MiembrosCop.UsuarioSistema,
                                        NombreUsuario = MiembrosCop.NombreUsuario,
                                        Fecha = DateTime.Today,
                                        Fk_Id_Acta = actaGuardada.PK_Id_Acta,
                                        AccionRealizada = "ADICIONO MIEMBRO AL ACTA COPASST",
                                    };
                                    context.Tbl_AuditoriaActaCopasst.Add(auditoria);
                                }
                            }
                        }
                        
                        context.SaveChanges();
                        Transaction.Commit();

                        if (actaGuardada == null)
                        {
                            MiembrosCop.PK_Id_Acta = acta.PK_Id_Acta;
                        }
                        else 
                        { 
                            MiembrosCop.PK_Id_Acta = actaGuardada.PK_Id_Acta;
                        }

                        return MiembrosCop;
                    }
                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al registrar Miembro Acta Copasst  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

        }

        //GUARDAR LOS PARTICIPANTES DE UN ACTA COPASST
        public List<EDParticipantes> GuardarParticipantesActaCopasst(EDParticipantes participante)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        
                        
                        var actaGuardada = (from m in context.Tbl_ActasCopasst
                                            where m.Consecutivo_Acta == participante.Consecutivo_Acta
                                            && m.Fk_Id_Sede == participante.IdSede
                                            select m).FirstOrDefault();

                        if (actaGuardada != null)
                        {
                            actaGuardada.NombreUsuario = participante.NombreUsuario;
                            
                            var yaExiste = (from m in context.Tbl_Participantes
                                            where m.Fk_Id_Acta == actaGuardada.PK_Id_Acta
                                            && m.Numero_Documento == participante.Numero_Documento
                                            select m);

                            if (yaExiste.Count() == 0)
                            {
 
                                    Participantes part = new Participantes()
                                    {
                                        Numero_Documento = participante.Numero_Documento,
                                        Nombre = participante.Nombre,
                                        Fk_Id_Acta = actaGuardada.PK_Id_Acta,
                                    };

                                    context.Tbl_Participantes.Add(part);

                                    AuditoriaActaCopasst auditoria = new AuditoriaActaCopasst()
                                    {

                                        Fk_Id_UsuarioSistema = participante.UsuarioSistema,
                                        NombreUsuario = participante.NombreUsuario,
                                        Fecha = DateTime.Today,
                                        Fk_Id_Acta = actaGuardada.PK_Id_Acta,
                                        AccionRealizada = "ADICIONO PARTICIPANTE AL ACTA COPASST",
                                    };
                                    context.Tbl_AuditoriaActaCopasst.Add(auditoria);
                            }
                        }

                        context.SaveChanges();
                        Transaction.Commit();

                        var participanteActasCopasst = (from miem in context.Tbl_Participantes
                                                    where miem.Fk_Id_Acta == actaGuardada.PK_Id_Acta
                                                    select new EDParticipantes
                                                    {
                                                        Numero_Documento = miem.Numero_Documento,
                                                        Nombre = miem.Nombre,
                                                        PK_Id_Acta = actaGuardada.PK_Id_Acta,

                                                    }
                            ).ToList();

                        return participanteActasCopasst;
                    }
                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al registrar Miembro Acta Copasst  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

        }

        //GUARDAR LOS TEMAS DE UN ACTA COPASST
        public List<EDTemasActasCopasst> GuardarTemasActaCopasst(EDTemasActasCopasst temas)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        var actaGuardada = (from m in context.Tbl_ActasCopasst
                                            where m.Consecutivo_Acta == temas.Consecutivo_Acta
                                            && m.Fk_Id_Sede == temas.IdSede
                                            select m).FirstOrDefault();

                        if (actaGuardada != null)
                        {
                            actaGuardada.NombreUsuario = temas.NombreUsuario;

                            TemasActaCopasst part = new TemasActaCopasst()
                            {
                                Tema = temas.Tema,
                                Observaciones = temas.Observaciones,
                                Fk_Id_Acta = actaGuardada.PK_Id_Acta,
                            };

                            context.Tbl_TemasActaCopasst.Add(part);

                                AuditoriaActaCopasst auditoria = new AuditoriaActaCopasst()
                                {

                                    Fk_Id_UsuarioSistema = temas.UsuarioSistema,
                                    NombreUsuario = temas.NombreUsuario,
                                    Fecha = DateTime.Today,
                                    Fk_Id_Acta = actaGuardada.PK_Id_Acta,
                                    AccionRealizada = "ADICIONO TEMA AL ACTA COPASST",
                                };
                                context.Tbl_AuditoriaActaCopasst.Add(auditoria);
                          }

                        context.SaveChanges();
                        Transaction.Commit();

                        var temasActasCopasst = (from miem in context.Tbl_TemasActaCopasst
                                                        where miem.Fk_Id_Acta == actaGuardada.PK_Id_Acta
                                                        select new EDTemasActasCopasst
                                                        {
                                                            Tema = miem.Tema,
                                                            Observaciones = miem.Observaciones,
                                                            PK_Id_Acta = actaGuardada.PK_Id_Acta,
                                                            PK_Id_TemaActa = miem.PK_Id_TemaActa,

                                                        }
                            ).ToList();

                        return temasActasCopasst;
                    }
                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al registrar Tema Acta Copasst  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

        }

        //ACTUALIZAR LAS OBSERVACIONES DE UN TEMA ACTA COPASST
        public List<EDTemasActasCopasst> ActualizarTemasActaCopasst(EDTemasActasCopasst temasActa)
        {
            List<EDTemasActasCopasst> temasActasCopasst = new List<EDTemasActasCopasst>();
            
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        var actaGuardadaCon = (from m in context.Tbl_ActasCopasst
                                               where m.PK_Id_Acta == temasActa.PK_Id_Acta
                                               select m).FirstOrDefault();

                        actaGuardadaCon.NombreUsuario = temasActa.NombreUsuario;

                        var TemaGuardada = (from m in context.Tbl_TemasActaCopasst
                                            where m.Fk_Id_Acta == temasActa.PK_Id_Acta
                                            && m.PK_Id_TemaActa == temasActa.PK_Id_TemaActa
                                            select m).FirstOrDefault();

                        if (TemaGuardada != null)
                        {

                            TemaGuardada.Observaciones = temasActa.Observaciones;
 
                            AuditoriaActaCopasst auditoria = new AuditoriaActaCopasst()
                            {

                                Fk_Id_UsuarioSistema = temasActa.UsuarioSistema,
                                NombreUsuario = temasActa.NombreUsuario,
                                Fecha = DateTime.Today,
                                Fk_Id_Acta = Convert.ToInt32(temasActa.PK_Id_Acta),
                                AccionRealizada = "ADICIONO OBSERVACION AL TEMA ACTA COPASST",
                            };
                            context.Tbl_AuditoriaActaCopasst.Add(auditoria);
                        }

                        context.SaveChanges();
                        Transaction.Commit();

                        temasActasCopasst = (from miem in context.Tbl_TemasActaCopasst
                                             where miem.Fk_Id_Acta == temasActa.PK_Id_Acta
                                             select new EDTemasActasCopasst
                                             {
                                                 Tema = miem.Tema,
                                                 Observaciones = miem.Observaciones,
                                                 PK_Id_TemaActa = miem.PK_Id_TemaActa,
                                                 PK_Id_Acta = temasActa.PK_Id_Acta,

                                             }
                            ).ToList();


                        return temasActasCopasst;
                    }
                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al registrar Miembro Acta Copasst  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

        }

        //GUARDAR LOS PLANES DE ACCION DE UN ACTA COPASST
        public List<EDAccionesActaCopasst> GuardarAccionesCopasst(EDAccionesActaCopasst acciones)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                ActasCopasst acta = null;
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        var actaGuardada = (from m in context.Tbl_ActasCopasst
                                            where m.Consecutivo_Acta == acciones.Consecutivo_Acta
                                            && m.Fk_Id_Sede == acciones.IdSede
                                            select m).FirstOrDefault();

                        if (actaGuardada != null)
                        {

                            actaGuardada.NombreUsuario = acciones.NombreUsuario;
                            
                            AccionesActaCopasst part = new AccionesActaCopasst()
                            {
                                AccionARealizar = acciones.AccionARealizar,
                                Responsable = acciones.Responsable,
                                FechaProbable = acciones.FechaProbable,
                                Fk_Id_Acta = actaGuardada.PK_Id_Acta,
                            };

                            context.Tbl_AccionesActaCopasst.Add(part);

                            AuditoriaActaCopasst auditoria = new AuditoriaActaCopasst()
                            {

                                Fk_Id_UsuarioSistema = acciones.UsuarioSistema,
                                NombreUsuario = acciones.NombreUsuario,
                                Fecha = DateTime.Today,
                                Fk_Id_Acta = actaGuardada.PK_Id_Acta,
                                AccionRealizada = "ADICIONO ACCION A REALIZAR AL ACTA COPASST",
                            };
                            context.Tbl_AuditoriaActaCopasst.Add(auditoria);
                        }

                        context.SaveChanges();
                        Transaction.Commit();

                        var accionesActasCopasst = (from miem in context.Tbl_AccionesActaCopasst
                                                    where miem.Fk_Id_Acta == acciones.PK_Id_Acta
                                                 select new EDAccionesActaCopasst
                                                 {
                                                     AccionARealizar = miem.AccionARealizar,
                                                     Responsable = miem.Responsable,
                                                     FechaProbable = miem.FechaProbable,
                                                     PK_Id_Acta = acciones.PK_Id_Acta,
                                                     Pk_Id_AccionActaCopasst = miem.Pk_Id_AccionActaCopasst,
                                                 }
                           ).ToList();

                        return accionesActasCopasst;
                    }
                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al registrar Acción Acta Copasst  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

        }
        //OBTENER LOS MIEMBROS DE UN ACTA COPASST
        public List<EDMiembrosCopasst> ObtenerMiembrosCopasstPorActa(int id_Acta)
        {
            //ActasCopasst ActasCopasst = new ActasCopasst();
            //EDActasCopasst edActasCopasst = new EDActasCopasst();

            List<EDMiembrosCopasst> MiembrosActasCopasst = new List<EDMiembrosCopasst>();

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        //ActasCopasst = (from act in context.Tbl_ActasCopasst
                        //                where act.PK_Id_Acta == id_Acta
                        //                select act).FirstOrDefault();

                        MiembrosActasCopasst = ( from miem in context.Tbl_MiembrosActaCopasst
                                                 join tPrio in context.Tbl_TipoPrioridadMiembroComite on miem.Fk_Id_TipoPrioridadMiembro equals tPrio.PK_Id_TipoPrioridadMiembro
                                                 join tPrin in context.Tbl_TipoPrincipalActa on miem.Fk_Id_TipoPrincipal equals tPrin.PK_Id_TipoPrincipal into sr
                                                 from x in sr.DefaultIfEmpty()
                                                 where miem.Fk_Id_Acta == id_Acta
                                                 select new EDMiembrosCopasst
                                                 {
                                                     Numero_Documento = miem.Numero_Documento,
                                                     Nombre = miem.Nombre,
                                                     Fk_Id_TipoPrincipal = miem.Fk_Id_TipoPrincipal,
                                                     Des_TipoPrincipal = x.DescripcionTipoPrincipal,
                                                     Fk_Id_TipoPrioridadMiembro = miem.Fk_Id_TipoPrioridadMiembro,
                                                     Des_TipoPrioridadMiembro = tPrio.DescripcionTipoMiembro,
                                                     TipoRepresentante = miem.TipoRepresentante,
                                                     PK_Id_Acta = id_Acta,
                                                    
                                                 }
                            ).ToList();

                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al Obtener los Miembros actas copasst  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return MiembrosActasCopasst;
        }

        //OBTENER LISTA DE PARTICIPANTES DE UN ACTA COPASST
        public List<EDParticipantes> ObtenerParticipantesCopasstPorActa(int id_Acta)
       {
           List<EDParticipantes> participanteActasCopasst = new List<EDParticipantes>();

           using (SG_SSTContext context = new SG_SSTContext())
           {
               using (var Transaction = context.Database.BeginTransaction())
               {
                   RegistraLog registraLog = new RegistraLog();
                   try
                   {
                       //ActasCopasst = (from act in context.Tbl_ActasCopasst
                       //                where act.PK_Id_Acta == id_Acta
                       //                select act).FirstOrDefault();

                       participanteActasCopasst = (from miem in context.Tbl_Participantes
                                                    where miem.Fk_Id_Acta == id_Acta
                                                   select new EDParticipantes
                                               {
                                                   Numero_Documento = miem.Numero_Documento,
                                                   Nombre = miem.Nombre,
                                                   PK_Id_Acta = id_Acta,

                                               }
                           ).ToList();

                   }
                   catch (Exception ex)
                   {
                       registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al Obtener los participantes actas copasst  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                       Transaction.Rollback();
                       return null;
                   }
               }
           }

           return participanteActasCopasst;
       }

        //OBTENER LISTA DE TEMAS DE UN ACTA COPASST
        public List<EDTemasActasCopasst> ObtenerTemasCopasstPorActa(int id_Acta)
       {
           List<EDTemasActasCopasst> temasActasCopasst = new List<EDTemasActasCopasst>();

           using (SG_SSTContext context = new SG_SSTContext())
           {
               using (var Transaction = context.Database.BeginTransaction())
               {
                   RegistraLog registraLog = new RegistraLog();
                   try
                   {
                       temasActasCopasst = (from miem in context.Tbl_TemasActaCopasst
                                                   where miem.Fk_Id_Acta == id_Acta
                                                   select new EDTemasActasCopasst
                                                   {
                                                       PK_Id_TemaActa = miem.PK_Id_TemaActa,
                                                       Tema = miem.Tema,
                                                       Observaciones = miem.Observaciones,
                                                       PK_Id_Acta = id_Acta,
                                                   }
                           ).ToList();

                   }
                   catch (Exception ex)
                   {
                       registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al Obtener los temas actas copasst  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                       Transaction.Rollback();
                       return null;
                   }
               }
           }

           return temasActasCopasst;
       }

        //OBTENER LISTA DE ACCIONES DE UN ACTA COPASST
        public List<EDAccionesActaCopasst> ObtenerAccionesCopasstPorActa(int id_Acta)
       {
           List<EDAccionesActaCopasst> accionesActasCopasst = new List<EDAccionesActaCopasst>();

           using (SG_SSTContext context = new SG_SSTContext())
           {
               using (var Transaction = context.Database.BeginTransaction())
               {
                   RegistraLog registraLog = new RegistraLog();
                   try
                   {
                       accionesActasCopasst = (from miem in context.Tbl_AccionesActaCopasst
                                            where miem.Fk_Id_Acta == id_Acta
                                            select new EDAccionesActaCopasst
                                            {
                                                Pk_Id_AccionActaCopasst = miem.Pk_Id_AccionActaCopasst,
                                                AccionARealizar = miem.AccionARealizar,
                                                FechaProbable = miem.FechaProbable,
                                                Responsable = miem.Responsable,
                                                PK_Id_Acta = id_Acta,
                                            }
                           ).ToList();

                   }
                   catch (Exception ex)
                   {
                       registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al Obtener los temas actas copasst  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                       Transaction.Rollback();
                       return null;
                   }
               }
           }

           return accionesActasCopasst;
       }

        //ELIMINAR MIEMBRO DE UN ACTA COPASST
        public List<EDMiembrosCopasst> EliminarMiembroActaCopasst(int Documento, int Usuario, string NombreUsuario, int PK_Id_Acta)
        {

            List<EDMiembrosCopasst> MiembrosActasCopasst = new List<EDMiembrosCopasst>();
            MiembrosActaCopasst miembrosBorrar = new MiembrosActaCopasst();
            Participantes participanteBorrar = new Participantes();

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {


                        var actaGuardadaCon = (from m in context.Tbl_ActasCopasst
                                               where m.PK_Id_Acta == PK_Id_Acta
                                               select m).FirstOrDefault();

                        actaGuardadaCon.NombreUsuario = NombreUsuario;

                        miembrosBorrar = (from miem in context.Tbl_MiembrosActaCopasst
                                          where miem.Fk_Id_Acta == PK_Id_Acta
                                          && miem.Numero_Documento == Documento
                                          select miem).First();

                        if (miembrosBorrar != null)
                        {
                            context.Tbl_MiembrosActaCopasst.Remove(miembrosBorrar);
                        }

                        AuditoriaActaCopasst auditoria = new AuditoriaActaCopasst()
                        {

                            Fk_Id_UsuarioSistema = Usuario,
                            NombreUsuario = NombreUsuario,
                            Fecha = DateTime.Today,
                            Fk_Id_Acta = PK_Id_Acta,
                            AccionRealizada = "ELIMINO MIEMBRO DE ACTA COPASST",
                        };
                        context.Tbl_AuditoriaActaCopasst.Add(auditoria);

                        context.SaveChanges();
                        Transaction.Commit();

                       
                        MiembrosActasCopasst = (from miem in context.Tbl_MiembrosActaCopasst
                                                join tPrio in context.Tbl_TipoPrioridadMiembroComite on miem.Fk_Id_TipoPrioridadMiembro equals tPrio.PK_Id_TipoPrioridadMiembro
                                                join tPrin in context.Tbl_TipoPrincipalActa on miem.Fk_Id_TipoPrincipal equals tPrin.PK_Id_TipoPrincipal into sr
                                                from x in sr.DefaultIfEmpty()
                                                where miem.Fk_Id_Acta == PK_Id_Acta
                                                select new EDMiembrosCopasst
                                                {
                                                    Numero_Documento = miem.Numero_Documento,
                                                    Nombre = miem.Nombre,
                                                    Fk_Id_TipoPrincipal = miem.Fk_Id_TipoPrincipal,
                                                    Des_TipoPrincipal = x.DescripcionTipoPrincipal,
                                                    Fk_Id_TipoPrioridadMiembro = miem.Fk_Id_TipoPrioridadMiembro,
                                                    Des_TipoPrioridadMiembro = tPrio.DescripcionTipoMiembro,
                                                    TipoRepresentante = miem.TipoRepresentante,
                                                    PK_Id_Acta = PK_Id_Acta,

                                                }
                            ).ToList();

                     }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al Obtener las actas copasst  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return MiembrosActasCopasst;
        }

        //ELIMINAR PARTICIPANTE DE UN ACTA COPASST
        public List<EDParticipantes> EliminarParticipanteCopasst(int Documento, int Usuario, string NombreUsuario, int PK_Id_Acta)
        {

            List<EDParticipantes> ParticipantesActasCopasst = new List<EDParticipantes>();
            MiembrosActaCopasst miembrosBorrar = new MiembrosActaCopasst();
            Participantes participanteBorrar = new Participantes();

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {


                        var actaGuardadaCon = (from m in context.Tbl_ActasCopasst
                                               where m.PK_Id_Acta == PK_Id_Acta
                                               select m).FirstOrDefault();

                        actaGuardadaCon.NombreUsuario = NombreUsuario;

                        participanteBorrar = (from miem in context.Tbl_Participantes
                                          where miem.Fk_Id_Acta == PK_Id_Acta
                                          && miem.Numero_Documento == Documento
                                          select miem).First();

                        if (participanteBorrar != null)
                            context.Tbl_Participantes.Remove(participanteBorrar);

                        AuditoriaActaCopasst auditoria = new AuditoriaActaCopasst()
                        {

                            Fk_Id_UsuarioSistema = Usuario,
                            NombreUsuario = NombreUsuario,
                            Fecha = DateTime.Today,
                            Fk_Id_Acta = PK_Id_Acta,
                            AccionRealizada = "ELIMINO PARTICIPANTE DE ACTA COPASST",
                        };
                        context.Tbl_AuditoriaActaCopasst.Add(auditoria);

                        context.SaveChanges();
                        Transaction.Commit();

                       
                        ParticipantesActasCopasst = (from miem in context.Tbl_Participantes
                                                where miem.Fk_Id_Acta == PK_Id_Acta
                                                select new EDParticipantes
                                                {
                                                    Numero_Documento = miem.Numero_Documento,
                                                    Nombre = miem.Nombre,
                                                    PK_Id_Acta = PK_Id_Acta,

                                                }
                            ).ToList();

                     }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al eliminar participante actas copasst  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return ParticipantesActasCopasst;
        }

        //ELIMINAR TEMA DE UN ACTA COPASST
        public List<EDTemasActasCopasst> EliminarTemaActaCopasst(int PK_Id_TemaActa, int Usuario, string NombreUsuario, int PK_Id_Acta)
        {

            List<EDTemasActasCopasst> temasActasCopasst = new List<EDTemasActasCopasst>();
            TemasActaCopasst temaBorrar = new TemasActaCopasst();

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {

                        var actaGuardadaCon = (from m in context.Tbl_ActasCopasst
                                               where m.PK_Id_Acta == PK_Id_Acta
                                               select m).FirstOrDefault();

                        actaGuardadaCon.NombreUsuario = NombreUsuario;

                        temaBorrar = (from miem in context.Tbl_TemasActaCopasst
                                          where miem.Fk_Id_Acta == PK_Id_Acta
                                          && miem.PK_Id_TemaActa == PK_Id_TemaActa
                                          select miem).First();

 
                        AuditoriaActaCopasst auditoria = new AuditoriaActaCopasst()
                        {

                            Fk_Id_UsuarioSistema = Usuario,
                            NombreUsuario = NombreUsuario,
                            Fecha = DateTime.Today,
                            Fk_Id_Acta = PK_Id_Acta,
                            AccionRealizada = "ELIMINO TEMA DE ACTA COPASST",
                        };
                        if (temaBorrar != null)
                        {
                            context.Tbl_TemasActaCopasst.Remove(temaBorrar);
                        }
                        
                        context.Tbl_AuditoriaActaCopasst.Add(auditoria);

                        context.SaveChanges();
                        Transaction.Commit();


                        temasActasCopasst = (from miem in context.Tbl_TemasActaCopasst
                                                where miem.Fk_Id_Acta == PK_Id_Acta
                                             select new EDTemasActasCopasst
                                                {
                                                    Tema = miem.Tema,
                                                    Observaciones = miem.Observaciones,
                                                    PK_Id_TemaActa = miem.PK_Id_TemaActa,
                                                    PK_Id_Acta = PK_Id_Acta,

                                                }
                            ).ToList();

                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al eliminar tema actas copasst  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return temasActasCopasst;
        }

        //ELIMINAR ACCION DE UN ACTA COPASST
        public List<EDAccionesActaCopasst> EliminarAccionActaCopasst(int Pk_Id_AccionActaCopasst, int Usuario, string NombreUsuario, int PK_Id_Acta)
        {

            List<EDAccionesActaCopasst> accionesActasCopasst = new List<EDAccionesActaCopasst>();
            AccionesActaCopasst accionBorrar = new AccionesActaCopasst();

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {

                        var actaGuardadaCon = (from m in context.Tbl_ActasCopasst
                                               where m.PK_Id_Acta == PK_Id_Acta
                                               select m).FirstOrDefault();

                        actaGuardadaCon.NombreUsuario = NombreUsuario;

                        accionBorrar = (from miem in context.Tbl_AccionesActaCopasst
                                      where miem.Fk_Id_Acta == PK_Id_Acta
                                      && miem.Pk_Id_AccionActaCopasst == Pk_Id_AccionActaCopasst
                                      select miem).First();


                        AuditoriaActaCopasst auditoria = new AuditoriaActaCopasst()
                        {

                            Fk_Id_UsuarioSistema = Usuario,
                            NombreUsuario = NombreUsuario,
                            Fecha = DateTime.Today,
                            Fk_Id_Acta = PK_Id_Acta,
                            AccionRealizada = "ELIMINO ACCION A REALIZAR DE ACTA COPASST",
                        };
                        if (accionBorrar != null)
                        {
                            context.Tbl_AccionesActaCopasst.Remove(accionBorrar);
                        }

                        context.Tbl_AuditoriaActaCopasst.Add(auditoria);

                        context.SaveChanges();
                        Transaction.Commit();


                        accionesActasCopasst = (from miem in context.Tbl_AccionesActaCopasst
                                             where miem.Fk_Id_Acta == PK_Id_Acta
                                             select new EDAccionesActaCopasst
                                             {
                                                 AccionARealizar = miem.AccionARealizar,
                                                 Responsable = miem.Responsable,
                                                 FechaProbable = miem.FechaProbable,
                                                 PK_Id_Acta = PK_Id_Acta,
                                                 Pk_Id_AccionActaCopasst = miem.Pk_Id_AccionActaCopasst,

                                             }
                            ).ToList();

                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al eliminar accion actas copasst  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return accionesActasCopasst;
        }

        //IMPORTAR UN ACTA COPASST
        public EDActasCopasst ImportarActaCopasst(EDActasCopasst InformacionActaCopasst)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                 using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        var actaGuardada = (from m in context.Tbl_ActasCopasst
                                            where m.PK_Id_Acta == InformacionActaCopasst.PK_Id_Acta
                                             select m).FirstOrDefault();

                        actaGuardada.NombreArchivo = InformacionActaCopasst.NombreArchivo;

                        AuditoriaActaCopasst auditoria = new AuditoriaActaCopasst()
                        {

                            Fk_Id_UsuarioSistema = InformacionActaCopasst.Fk_Id_UsuarioSistema,
                            NombreUsuario = InformacionActaCopasst.NombreUsuario,
                            Fecha = DateTime.Today,
                            Fk_Id_Acta = actaGuardada.PK_Id_Acta,
                            AccionRealizada = "IMPORTO EL DOCUMENTO",
                        };

                        context.Tbl_AuditoriaActaCopasst.Add(auditoria);

                        context.SaveChanges();
                        Transaction.Commit();

 
                        return InformacionActaCopasst;
                    }
                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al registrar Miembro Acta Copasst  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

        }

        //ACTUALIZAR UN ACTA COPASST
        public EDActasCopasst ActualizarActaCopasst(EDActasCopasst InformacionActaCopasst)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        var actaGuardada = (from m in context.Tbl_ActasCopasst
                                            where m.PK_Id_Acta == InformacionActaCopasst.PK_Id_Acta
                                             select m).FirstOrDefault();

                        actaGuardada.Fecha = InformacionActaCopasst.Fecha;
                        actaGuardada.TemaReunion = InformacionActaCopasst.TemaReunion;
                        actaGuardada.Conclusiones = InformacionActaCopasst.Conclusiones;
                        actaGuardada.NombreUsuario = InformacionActaCopasst.NombreUsuario;

                        AuditoriaActaCopasst auditoria = new AuditoriaActaCopasst()
                        {

                            Fk_Id_UsuarioSistema = InformacionActaCopasst.Fk_Id_UsuarioSistema,
                            NombreUsuario = InformacionActaCopasst.NombreUsuario,
                            Fecha = DateTime.Today,
                            Fk_Id_Acta = actaGuardada.PK_Id_Acta,
                            AccionRealizada = "ACTUALIZO EL ACTA ",
                        };

                        context.Tbl_AuditoriaActaCopasst.Add(auditoria);

                        context.SaveChanges();
                        Transaction.Commit();

                          return InformacionActaCopasst;
                    }
                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al registrar Miembro Acta Copasst  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

        }

////////////////////////////////////////////////////////////////////////////////////////////////

        //OBTENER LA INFORMACION DE UN ACTA CONVIVENCIA
        public List<EDActasConvivencia> ObtenerInformacionActaConvivencia(int idsede)
        {
            List<EDActasConvivencia> infoActaConvivencia = null;

            using (SG_SSTContext contexto = new SG_SSTContext())
            {
                RegistraLog registra = new RegistraLog();

                try
                {
                    infoActaConvivencia = (from Emp in contexto.Tbl_Empresa
                                       join sede in contexto.Tbl_Sede
                                       on Emp.Pk_Id_Empresa equals sede.Fk_Id_Empresa
                                       join ac in contexto.Tbl_ActasConvivencia
                                       on sede.Pk_Id_Sede equals ac.Fk_Id_Sede
                                       where sede.Pk_Id_Sede == idsede
                                       select new EDActasConvivencia
                                       {
                                           NombreEmpresa = Emp.Razon_Social,
                                           NombreSede = sede.Nombre_Sede,
                                           Consecutivo_Acta = ac.Consecutivo_Acta,
                                           Fecha = ac.Fecha,
                                           TemaReunion = ac.TemaReunion,
                                           NombreUsuario = ac.NombreUsuario,
                                           IdEmpresa = Emp.Pk_Id_Empresa,
                                           PK_Id_Acta = ac.PK_Id_Acta,
                                           NombreArchivo = ac.NombreArchivo,

                                       }).OrderBy(x=>x.Consecutivo_Acta).ToList();
                }
                catch (Exception ex)
                {
                    registra.RegistrarError(typeof(ComiteManager), string.Format("Error al obtener la Información  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                    return null;
                }
            }

            return infoActaConvivencia;
        }

         //OBTENER ACTA CONVIVENCIA POR EMPRESA
        public List<EDActasConvivencia> ObtenerActasConvivenciaPorEmpresa(int id)
        {
            List<EDActasConvivencia> ActasConvivencia = null;

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        ActasConvivencia = (from act in context.Tbl_ActasConvivencia
                                        where act.Fk_Id_Empresa == id
                                        select new EDActasConvivencia
                                        {
                                            Consecutivo_Acta = act.Consecutivo_Acta,
                                            PK_Id_Acta = act.PK_Id_Acta
                                        }).OrderBy(x => x.Consecutivo_Acta).ToList();
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al Obtener las actas copasst  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return ActasConvivencia;
        }

        //OBTENER LA INFORMACION DE UN ACTA CONVIVENCIA
        public EDActasConvivencia ObtenerActasConvivenciaPorId(int Id_Acta)
        {
            EDActasConvivencia ActasConvivencia = null;

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        ActasConvivencia = (from act in context.Tbl_ActasConvivencia
                                        where act.PK_Id_Acta == Id_Acta
                                        select new EDActasConvivencia
                                        {
                                            Consecutivo_Acta = act.Consecutivo_Acta,
                                            PK_Id_Acta = act.PK_Id_Acta,
                                            Fecha = act.Fecha,
                                            NombreEmpresa = act.NombreEmpresa,
                                            NombreArchivo = act.NombreArchivo,
                                            Fk_Id_Sede = act.Fk_Id_Sede,
                                            TemaReunion = act.TemaReunion,
                                            Conclusiones = act.Conclusiones,
                                        }).First();
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al Obtener el acta copasst  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return ActasConvivencia;
        }

         //GUARDAR LOS MIEMBROS DE UN ACTA CONVIVENCIA
        public EDMiembrosConvivencia GuardarMiembrosActaConvivencia(EDMiembrosConvivencia MiembrosCop)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                ActasConvivencia acta = null;
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        var actaGuardada = (from m in context.Tbl_ActasConvivencia
                                            where m.Consecutivo_Acta == MiembrosCop.Consecutivo_Acta
                                            && m.Fk_Id_Sede == MiembrosCop.IdSede
                                            select m).FirstOrDefault();

                        if (actaGuardada == null)
                        {
                            acta = new ActasConvivencia()
                            {
                                Consecutivo_Acta = MiembrosCop.Consecutivo_Acta,
                                NombreUsuario = MiembrosCop.NombreUsuario,
                                Fk_Id_Empresa = MiembrosCop.IdEmpresa,
                                NombreEmpresa = MiembrosCop.NombreEmpresa,
                                Fk_Id_Sede = MiembrosCop.IdSede,
                                Fecha = DateTime.Today,
                            };
                            context.Tbl_ActasConvivencia.Add(acta);

                            if (MiembrosCop.Fk_Id_TipoPrincipal == 0)
                            {
                                MiembrosActaConvivencia miembro = new MiembrosActaConvivencia()
                                {
                                    Numero_Documento = MiembrosCop.Numero_Documento,
                                    Nombre = MiembrosCop.Nombre,
                                    Fk_Id_TipoPrioridadMiembro = MiembrosCop.Fk_Id_TipoPrioridadMiembro,
                                    TipoRepresentante = MiembrosCop.TipoRepresentante,
                                };
                                context.Tbl_MiembrosActaConvivencia.Add(miembro);
                            }
                            else
                            {
                                MiembrosActaConvivencia miembro = new MiembrosActaConvivencia()
                                {
                                    Numero_Documento = MiembrosCop.Numero_Documento,
                                    Nombre = MiembrosCop.Nombre,
                                    Fk_Id_TipoPrincipal = MiembrosCop.Fk_Id_TipoPrincipal,
                                    Fk_Id_TipoPrioridadMiembro = MiembrosCop.Fk_Id_TipoPrioridadMiembro,
                                    TipoRepresentante = MiembrosCop.TipoRepresentante,
                                };

                                context.Tbl_MiembrosActaConvivencia.Add(miembro);
                            }

                            ParticipantesActasConvivencia participante = new ParticipantesActasConvivencia()
                            {
                                Numero_Documento = MiembrosCop.Numero_Documento,
                                Nombre = MiembrosCop.Nombre,
                            };

                            context.Tbl_ParticipantesActasConvivencia.Add(participante);

                            AuditoriaActaConvivencia auditoria = new AuditoriaActaConvivencia()
                            {

                                Fk_Id_UsuarioSistema = MiembrosCop.UsuarioSistema,
                                NombreUsuario = MiembrosCop.NombreUsuario,
                                Fecha = DateTime.Today,
                                AccionRealizada = "CREO EL ACTA CONVIVENCIA",
                            };
                            context.Tbl_AuditoriaActaConvivencia.Add(auditoria);


                        }
                        else
                        {
                            actaGuardada.NombreUsuario = MiembrosCop.NombreUsuario;
                            
                            var yaExiste = (from m in context.Tbl_MiembrosActaConvivencia
                                            where m.Fk_Id_Acta == actaGuardada.PK_Id_Acta
                                            && m.Numero_Documento == MiembrosCop.Numero_Documento
                                            select m);

                            if (yaExiste.Count() == 0)
                            {
                                if (MiembrosCop.Fk_Id_TipoPrincipal == 0)
                                {
                                    MiembrosActaConvivencia miembro = new MiembrosActaConvivencia()
                                    {
                                        Numero_Documento = MiembrosCop.Numero_Documento,
                                        Nombre = MiembrosCop.Nombre,
                                        Fk_Id_TipoPrioridadMiembro = MiembrosCop.Fk_Id_TipoPrioridadMiembro,
                                        TipoRepresentante = MiembrosCop.TipoRepresentante,
                                        Fk_Id_Acta = actaGuardada.PK_Id_Acta,
                                    };
                                    context.Tbl_MiembrosActaConvivencia.Add(miembro);

                                    ParticipantesActasConvivencia participante = new ParticipantesActasConvivencia()
                                    {
                                        Numero_Documento = MiembrosCop.Numero_Documento,
                                        Nombre = MiembrosCop.Nombre,
                                        Fk_Id_Acta = actaGuardada.PK_Id_Acta,
                                    };

                                    context.Tbl_ParticipantesActasConvivencia.Add(participante);
                                }
                                else
                                {
                                    MiembrosActaConvivencia miembro = new MiembrosActaConvivencia()
                                    {
                                        Numero_Documento = MiembrosCop.Numero_Documento,
                                        Nombre = MiembrosCop.Nombre,
                                        Fk_Id_TipoPrincipal = MiembrosCop.Fk_Id_TipoPrincipal,
                                        Fk_Id_TipoPrioridadMiembro = MiembrosCop.Fk_Id_TipoPrioridadMiembro,
                                        TipoRepresentante = MiembrosCop.TipoRepresentante,
                                        Fk_Id_Acta = actaGuardada.PK_Id_Acta,
                                    };

                                    context.Tbl_MiembrosActaConvivencia.Add(miembro);

                                    ParticipantesActasConvivencia participante = new ParticipantesActasConvivencia()
                                    {
                                        Numero_Documento = MiembrosCop.Numero_Documento,
                                        Nombre = MiembrosCop.Nombre,
                                        Fk_Id_Acta = actaGuardada.PK_Id_Acta,
                                    };

                                    context.Tbl_ParticipantesActasConvivencia.Add(participante);

                                    AuditoriaActaConvivencia auditoria = new AuditoriaActaConvivencia()
                                    {

                                        Fk_Id_UsuarioSistema = MiembrosCop.UsuarioSistema,
                                        NombreUsuario = MiembrosCop.NombreUsuario,
                                        Fecha = DateTime.Today,
                                        Fk_Id_Acta = actaGuardada.PK_Id_Acta,
                                        AccionRealizada = "ADICIONO MIEMBRO AL ACTA CONVIVENCIA",
                                    };
                                    context.Tbl_AuditoriaActaConvivencia.Add(auditoria);
                                }
                            }
                        }

                        context.SaveChanges();
                        Transaction.Commit();

                        if (actaGuardada == null)
                        {
                            MiembrosCop.PK_Id_Acta = acta.PK_Id_Acta;
                        }
                        else
                        {
                            MiembrosCop.PK_Id_Acta = actaGuardada.PK_Id_Acta;
                        }

                        return MiembrosCop;
                    }
                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al registrar Miembro Acta Convivencia  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

        }

        //GUARDAR LOS PARTICIPANTES DE UN ACTA CONVIVENCIA
        public List<EDParticipantesActaConvivencia> GuardarParticipantesActaConvivencia(EDParticipantesActaConvivencia participante)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        var actaGuardada = (from m in context.Tbl_ActasConvivencia
                                            where m.Consecutivo_Acta == participante.Consecutivo_Acta
                                            && m.Fk_Id_Sede == participante.IdSede
                                            select m).FirstOrDefault();

                        if (actaGuardada != null)
                        {
                            actaGuardada.NombreUsuario = participante.NombreUsuario;
                            
                            var yaExiste = (from m in context.Tbl_ParticipantesActasConvivencia
                                            where m.Fk_Id_Acta == actaGuardada.PK_Id_Acta
                                            && m.Numero_Documento == participante.Numero_Documento
                                            select m);

                            if (yaExiste.Count() == 0)
                            {

                                ParticipantesActasConvivencia part = new ParticipantesActasConvivencia()
                                {
                                    Numero_Documento = participante.Numero_Documento,
                                    Nombre = participante.Nombre,
                                    Fk_Id_Acta = actaGuardada.PK_Id_Acta,
                                };

                                context.Tbl_ParticipantesActasConvivencia.Add(part);

                                AuditoriaActaConvivencia auditoria = new AuditoriaActaConvivencia()
                                {

                                    Fk_Id_UsuarioSistema = participante.UsuarioSistema,
                                    NombreUsuario = participante.NombreUsuario,
                                    Fecha = DateTime.Today,
                                    Fk_Id_Acta = actaGuardada.PK_Id_Acta,
                                    AccionRealizada = "ADICIONO PARTICIPANTE AL ACTA CONVIVENCIA",
                                };
                                context.Tbl_AuditoriaActaConvivencia.Add(auditoria);
                            }
                        }

                        context.SaveChanges();
                        Transaction.Commit();

                        var participanteActasConvivencia = (from miem in context.Tbl_ParticipantesActasConvivencia
                                                        where miem.Fk_Id_Acta == actaGuardada.PK_Id_Acta
                                                        select new EDParticipantesActaConvivencia
                                                        {
                                                            Numero_Documento = miem.Numero_Documento,
                                                            Nombre = miem.Nombre,
                                                            PK_Id_Acta = actaGuardada.PK_Id_Acta,

                                                        }
                            ).ToList();

                        return participanteActasConvivencia;
                    }
                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al registrar Miembro Acta Convivencia  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

        }

        //GUARDAR LOS RESPONSABLES DE QUEJA DE UN ACTA CONVIVENCIA
        public List<EDResponsablesQuejas> GuardarResponsablesQueja(EDResponsablesQuejas Responsable)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        var yaExiste = (from m in context.Tbl_ResponsablesQuejas
                                    where m.Fk_Id_Queja == Responsable.Fk_Id_Queja
                                    && m.Numero_Documento == Responsable.Numero_Documento
                                    select m);

                        if (yaExiste.Count() == 0)
                        {

                            var actaGuardadaCon = (from m in context.Tbl_ActasConvivencia
                                                   where m.PK_Id_Acta == Responsable.PK_Id_Acta
                                                   select m).FirstOrDefault();

                            actaGuardadaCon.NombreUsuario = Responsable.NombreUsuario;

                            ResponsablesQuejas part = new ResponsablesQuejas()
                            {
                                Numero_Documento = Responsable.Numero_Documento,
                                Nombre = Responsable.Nombre,
                                Fk_Id_Queja = Responsable.Fk_Id_Queja,
                            };

                            context.Tbl_ResponsablesQuejas.Add(part);

                            AuditoriaActaConvivencia auditoria = new AuditoriaActaConvivencia()
                            {
                                Fk_Id_UsuarioSistema = Responsable.UsuarioSistema,
                                NombreUsuario = Responsable.NombreUsuario,
                                Fecha = DateTime.Today,
                                Fk_Id_Acta = Convert.ToInt32(Responsable.PK_Id_Acta),
                                AccionRealizada = "ADICIONO RESPONSABLE A QUEJA AL ACTA CONVIVENCIA",
                            };
                            context.Tbl_AuditoriaActaConvivencia.Add(auditoria);
                        }
                        context.SaveChanges();
                        Transaction.Commit();

                        var participanteActasConvivencia = (from miem in context.Tbl_ResponsablesQuejas
                                                        where miem.Fk_Id_Queja == Responsable.Fk_Id_Queja
                                                        select new EDResponsablesQuejas
                                                        {
                                                            Numero_Documento = miem.Numero_Documento,
                                                            Nombre = miem.Nombre,
                                                            Fk_Id_Queja = Responsable.Fk_Id_Queja,

                                                        }
                            ).ToList();

                        return participanteActasConvivencia;
                    }
                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al registrar Responsable Queja Acta Convivencia  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

        }

        //GUARDAR QUEJAS DE UN ACTA CONVIVENCIA
        public EDActaConvivenciaQuejas GuardarActasQueja(EDActaConvivenciaQuejas acta)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        var actaGuardadaCon = (from m in context.Tbl_ActasConvivencia
                                               where m.PK_Id_Acta == acta.Fk_Id_Acta
                                               select m).FirstOrDefault();

                        actaGuardadaCon.NombreUsuario = acta.NombreUsuario;
                        
                        ActaConvivenciaQuejas queja = new ActaConvivenciaQuejas()
                                {
                                    Fk_Id_Sede = acta.Fk_Id_Sede,
                                    Fk_Id_Acta = acta.Fk_Id_Acta,
                                    Consecutivo_Caso = acta.Consecutivo_Caso,
                                    Consecutivo_Queja = acta.Consecutivo_Queja,
                                    Fecha = DateTime.Now,
                                };

                                context.Tbl_ActaConvivenciaQuejas.Add(queja);

                                AuditoriaActaConvivencia auditoria = new AuditoriaActaConvivencia()
                                {

                                    Fk_Id_UsuarioSistema = acta.UsuarioSistema,
                                    NombreUsuario = acta.NombreUsuario,
                                    Fecha = DateTime.Today,
                                    Fk_Id_Acta = acta.Fk_Id_Acta,
                                    AccionRealizada = "ADICIONO QUEJA AL ACTA CONVIVENCIA",
                                };
                                
                        context.Tbl_AuditoriaActaConvivencia.Add(auditoria);
 
                        context.SaveChanges();
                        Transaction.Commit();

                        var quejaGuardada = (from aq in context.Tbl_ActaConvivenciaQuejas
                                                        where aq.Fk_Id_Acta == acta.Fk_Id_Acta
                                                        select new EDActaConvivenciaQuejas
                                                        {
                                                            PK_Id_Queja = aq.PK_Id_Queja,
                                                            Consecutivo_Queja = aq.Consecutivo_Queja,
                                                            Consecutivo_Caso = aq.Consecutivo_Caso,
                                                            Fecha = aq.Fecha,
                                                            NombreRefiereSituacion = aq.NombreRefiereSituacion,
                                                            AspectosNoResueltos = aq.AspectosNoResueltos,
                                                            Compromisos = aq.Compromisos,
                                                            Fk_Id_Acta = aq.Fk_Id_Acta,
                                                            Fk_Id_Sede = aq.Fk_Id_Sede,
                                                        }
                            ).First();

                        return quejaGuardada;
                    }
                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al registrar Queja Acta Convivencia  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

        }

        //GUARDAR SEGUIMIENTO DE UN ACTA CONVIVENCIA
        public EDSeguimientoActaConvivencia GuardarActasSeguimiento(EDSeguimientoActaConvivencia acta)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        var actaGuardadaCon = (from m in context.Tbl_ActasConvivencia
                                               where m.PK_Id_Acta == acta.Fk_Id_Acta
                                               select m).FirstOrDefault();

                        actaGuardadaCon.NombreUsuario = acta.NombreUsuario;
                        
                        SeguimientoActaConvivencia seguimiento = new SeguimientoActaConvivencia()
                                {
                                    Fk_Id_Sede = acta.Fk_Id_Sede,
                                    Fk_Id_Acta = acta.Fk_Id_Acta,
                                    Consecutivo_Evento = acta.Consecutivo_Evento,
                                    Fecha = DateTime.Now,
                                };

                                context.Tbl_SeguimientoActaConvivencia.Add(seguimiento);

                                AuditoriaActaConvivencia auditoria = new AuditoriaActaConvivencia()
                                {

                                    Fk_Id_UsuarioSistema = acta.UsuarioSistema,
                                    NombreUsuario = acta.NombreUsuario,
                                    Fecha = DateTime.Today,
                                    Fk_Id_Acta = acta.Fk_Id_Acta,
                                    AccionRealizada = "ADICIONO SEGUIMIENTO AL ACTA CONVIVENCIA",
                                };
                                
                        context.Tbl_AuditoriaActaConvivencia.Add(auditoria);
 
                        context.SaveChanges();
                        Transaction.Commit();

                        var seguimientoGuardada = (from aq in context.Tbl_SeguimientoActaConvivencia
                                                        where aq.Fk_Id_Acta == acta.Fk_Id_Acta
                                                        select new EDSeguimientoActaConvivencia
                                                        {
                                                            PK_Id_Seguimiento = aq.PK_Id_Seguimiento,
                                                            Consecutivo_Evento = aq.Consecutivo_Evento,
                                                            Fecha = aq.Fecha,
                                                            NombreParteInvolucrada = aq.NombreParteInvolucrada,
                                                            CompromisosAdquiridos = aq.CompromisosAdquiridos,
                                                            Observaciones = aq.Observaciones,
                                                            Fk_Id_Acta = aq.Fk_Id_Acta,
                                                            Fk_Id_Sede = aq.Fk_Id_Sede,
                                                        }
                            ).First();

                        return seguimientoGuardada;
                    }
                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al registrar Queja Acta Convivencia  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

        }

        //GUARDAR LOS TEMAS DE UN ACTA CONVIVENCIA
        public List<EDTemasActasConvivencia> GuardarTemasActaConvivencia(EDTemasActasConvivencia temas)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        var actaGuardada = (from m in context.Tbl_ActasConvivencia
                                            where m.Consecutivo_Acta == temas.Consecutivo_Acta
                                            && m.Fk_Id_Sede == temas.IdSede
                                            select m).FirstOrDefault();

                        if (actaGuardada != null)
                        {

                            actaGuardada.NombreUsuario = temas.NombreUsuario;

                            TemasActaConvivencia part = new TemasActaConvivencia()
                            {
                                Tema = temas.Tema,
                                Observaciones = temas.Observaciones,
                                Fk_Id_Acta = actaGuardada.PK_Id_Acta,
                            };

                            context.Tbl_TemasActaConvivencia.Add(part);

                            AuditoriaActaConvivencia auditoria = new AuditoriaActaConvivencia()
                            {

                                Fk_Id_UsuarioSistema = temas.Fk_Id_UsuarioSistema,
                                NombreUsuario = temas.NombreUsuario,
                                Fecha = DateTime.Today,
                                Fk_Id_Acta = actaGuardada.PK_Id_Acta,
                                AccionRealizada = "ADICIONO TEMA AL ACTA CONVIVENCIA",
                            };
                            context.Tbl_AuditoriaActaConvivencia.Add(auditoria);
                        }

                        context.SaveChanges();
                        Transaction.Commit();

                        var temasActasConvivencia = (from miem in context.Tbl_TemasActaConvivencia
                                                 where miem.Fk_Id_Acta == actaGuardada.PK_Id_Acta
                                                 select new EDTemasActasConvivencia
                                                 {
                                                     Tema = miem.Tema,
                                                     Observaciones = miem.Observaciones,
                                                     PK_Id_Acta = actaGuardada.PK_Id_Acta,
                                                     PK_Id_TemaActa = miem.PK_Id_TemaActa,

                                                 }
                            ).ToList();

                        return temasActasConvivencia;
                    }
                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al registrar Tema Acta Convivencia  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

        }

        //ACTUALIZAR LAS OBSERVACIONES DE UN TEMA ACTA CONVIVENCIA
        public List<EDTemasActasConvivencia> ActualizarTemasActaConvivencia(EDTemasActasConvivencia temas)
        {
            List<EDTemasActasConvivencia> temasActasConvivencia = new List<EDTemasActasConvivencia>();

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        var actaGuardadaCon = (from m in context.Tbl_ActasConvivencia
                                               where m.PK_Id_Acta == temas.PK_Id_Acta
                                               select m).FirstOrDefault();

                        actaGuardadaCon.NombreUsuario = temas.NombreUsuario;
                        
                        var TemaGuardada = (from m in context.Tbl_TemasActaConvivencia
                                            where m.Fk_Id_Acta == temas.PK_Id_Acta
                                            && m.PK_Id_TemaActa == temas.PK_Id_TemaActa
                                            select m).FirstOrDefault();

                        if (TemaGuardada != null)
                        {

                            TemaGuardada.Observaciones = temas.Observaciones;

                            AuditoriaActaConvivencia auditoria = new AuditoriaActaConvivencia()
                            {

                                Fk_Id_UsuarioSistema = temas.Fk_Id_UsuarioSistema,
                                NombreUsuario = temas.NombreUsuario,
                                Fecha = DateTime.Today,
                                Fk_Id_Acta = Convert.ToInt32(temas.PK_Id_Acta),
                                AccionRealizada = "ADICIONO OBSERVACION AL TEMA ACTA CONVIVENCIA",
                            };
                            context.Tbl_AuditoriaActaConvivencia.Add(auditoria);
                        }

                        context.SaveChanges();
                        Transaction.Commit();

                        temasActasConvivencia = (from miem in context.Tbl_TemasActaConvivencia
                                             where miem.Fk_Id_Acta == temas.PK_Id_Acta
                                             select new EDTemasActasConvivencia
                                             {
                                                 Tema = miem.Tema,
                                                 Observaciones = miem.Observaciones,
                                                 PK_Id_TemaActa = miem.PK_Id_TemaActa,
                                                 PK_Id_Acta = temas.PK_Id_Acta,

                                             }
                            ).ToList();


                        return temasActasConvivencia;
                    }
                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al registrar Miembro Acta Convivencia  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

        }

        //GUARDAR LOS PLANES DE ACCION DE UN ACTA CONVIVENCIA
        public List<EDAccionesActaConvivencia> GuardarAccionesConvivencia(EDAccionesActaConvivencia acciones)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        var actaGuardada = (from m in context.Tbl_ActasConvivencia
                                            where m.Consecutivo_Acta == acciones.Consecutivo_Acta
                                            && m.Fk_Id_Sede == acciones.IdSede
                                            select m).FirstOrDefault();

                        if (actaGuardada != null)
                        {

                            actaGuardada.NombreUsuario = acciones.NombreUsuario;

                            AccionesActaConvivencia part = new AccionesActaConvivencia()
                            {
                                AccionARealizar = acciones.AccionARealizar,
                                Responsable = acciones.Responsable,
                                FechaProbable = acciones.FechaProbable,
                                Fk_Id_Acta = actaGuardada.PK_Id_Acta,
                            };

                            context.Tbl_AccionesActaConvivencia.Add(part);

                            AuditoriaActaConvivencia auditoria = new AuditoriaActaConvivencia()
                            {

                                Fk_Id_UsuarioSistema = acciones.UsuarioSistema,
                                NombreUsuario = acciones.NombreUsuario,
                                Fecha = DateTime.Today,
                                Fk_Id_Acta = actaGuardada.PK_Id_Acta,
                                AccionRealizada = "ADICIONO ACCION A REALIZAR AL ACTA CONVIVENCIA",
                            };
                            context.Tbl_AuditoriaActaConvivencia.Add(auditoria);
                        }

                        context.SaveChanges();
                        Transaction.Commit();

                        var accionesActasConvivencia = (from miem in context.Tbl_AccionesActaConvivencia
                                                    where miem.Fk_Id_Acta == acciones.PK_Id_Acta
                                                    select new EDAccionesActaConvivencia
                                                    {
                                                        AccionARealizar = miem.AccionARealizar,
                                                        Responsable = miem.Responsable,
                                                        FechaProbable = miem.FechaProbable,
                                                        PK_Id_Acta = acciones.PK_Id_Acta,
                                                        Pk_Id_AccionActaConvivencia = miem.Pk_Id_AccionActaConvivencia,
                                                    }
                           ).ToList();

                        return accionesActasConvivencia;
                    }
                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al registrar Acción Acta Convivencia  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

        }

       //GUARDAR LAS ACCIONES DE UNA QUEJA ACTA CONVIVENCIA
        public List<EDAccionesActaQuejas> GuardarAccionesActaQueja(EDAccionesActaQuejas acciones)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        var actaGuardadaCon = (from m in context.Tbl_ActasConvivencia
                                               where m.PK_Id_Acta == acciones.PK_Id_Acta
                                               select m).FirstOrDefault();

                        actaGuardadaCon.NombreUsuario = acciones.NombreUsuario;

                        AccionesActaQuejas part = new AccionesActaQuejas()
                        {
                            AccionARealizar = acciones.AccionARealizar,
                            Fk_Id_Queja = acciones.Fk_Id_Queja,
                        };

                        context.Tbl_AccionesActaQuejas.Add(part);

                        AuditoriaActaConvivencia auditoria = new AuditoriaActaConvivencia()
                        {
                            Fk_Id_UsuarioSistema = acciones.UsuarioSistema,
                            NombreUsuario = acciones.NombreUsuario,
                            Fecha = DateTime.Today,
                            Fk_Id_Acta = acciones.PK_Id_Acta,
                            AccionRealizada = "ADICIONO ACCION QUEJA AL ACTA CONVIVENCIA",
                        };
                        context.Tbl_AuditoriaActaConvivencia.Add(auditoria);
 
                        context.SaveChanges();
                        Transaction.Commit();

                        var accionesActasqueja = (from miem in context.Tbl_AccionesActaQuejas
                                                    where miem.Fk_Id_Queja == acciones.Fk_Id_Queja
                                                    select new EDAccionesActaQuejas
                                                    {
                                                        AccionARealizar = miem.AccionARealizar,
                                                        Pk_Id_AccionQueja = miem.Pk_Id_AccionQueja,
                                                        Fk_Id_Queja = miem.Fk_Id_Queja,
                                                        PK_Id_Acta = acciones.PK_Id_Acta,
                                                     }
                           ).ToList();

                        return accionesActasqueja;
                    }
                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al registrar Acción Queja Acta Convivencia  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

        }

       //GUARDAR COMPROMISOS DE UN SEGUIMIENTO ACTA ACONVIVENCIA
        public List<EDCompromisosPendientes> GuardarCompromisosSeguimiento(EDCompromisosPendientes Compromiso)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        var actaGuardadaCon = (from m in context.Tbl_ActasConvivencia
                                               where m.PK_Id_Acta == Compromiso.PK_Id_Acta
                                               select m).FirstOrDefault();

                        actaGuardadaCon.NombreUsuario = Compromiso.NombreUsuario;

                        CompromisosPendientes part = new CompromisosPendientes()
                        {
                            CompromisoPendiente = Compromiso.CompromisoPendiente,
                            FK_Id_Seguimiento = Compromiso.FK_Id_Seguimiento,
                        };

                        context.Tbl_CompromisosPendientes.Add(part);

                        AuditoriaActaConvivencia auditoria = new AuditoriaActaConvivencia()
                        {
                            Fk_Id_UsuarioSistema = Compromiso.UsuarioSistema,
                            NombreUsuario = Compromiso.NombreUsuario,
                            Fecha = DateTime.Today,
                            Fk_Id_Acta = Compromiso.PK_Id_Acta,
                            AccionRealizada = "ADICIONO COMPROMISO A SEGUIMEINTO ACTA CONVIVENCIA",
                        };
                        context.Tbl_AuditoriaActaConvivencia.Add(auditoria);
 
                        context.SaveChanges();
                        Transaction.Commit();

                        var compro = (from miem in context.Tbl_CompromisosPendientes
                                                  where miem.FK_Id_Seguimiento == Compromiso.FK_Id_Seguimiento
                                                    select new EDCompromisosPendientes
                                                    {
                                                        CompromisoPendiente = miem.CompromisoPendiente,
                                                        FK_Id_Seguimiento = miem.FK_Id_Seguimiento,
                                                        PK_Id_Acta = Compromiso.PK_Id_Acta,
                                                        Pk_Id_Compromiso = miem.Pk_Id_Compromiso,
                                                     }
                           ).ToList();

                        return compro;
                    }
                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al registrar Acción Queja Acta Convivencia  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

        }
        
        //OBTENER LOS MIEMBROS DE UN ACTA CONVIVENCIA
        public List<EDMiembrosConvivencia> ObtenerMiembrosConvivenciaPorActa(int id_Acta)
        {
            //ActasConvivencia ActasConvivencia = new ActasConvivencia();
            //EDActasConvivencia edActasConvivencia = new EDActasConvivencia();

            List<EDMiembrosConvivencia> MiembrosActasConvivencia = new List<EDMiembrosConvivencia>();

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        //ActasConvivencia = (from act in context.Tbl_ActasConvivencia
                        //                where act.PK_Id_Acta == id_Acta
                        //                select act).FirstOrDefault();

                        MiembrosActasConvivencia = (from miem in context.Tbl_MiembrosActaConvivencia
                                                join tPrio in context.Tbl_TipoPrioridadMiembroComite on miem.Fk_Id_TipoPrioridadMiembro equals tPrio.PK_Id_TipoPrioridadMiembro
                                                join tPrin in context.Tbl_TipoPrincipalActa on miem.Fk_Id_TipoPrincipal equals tPrin.PK_Id_TipoPrincipal into sr
                                                from x in sr.DefaultIfEmpty()
                                                where miem.Fk_Id_Acta == id_Acta
                                                select new EDMiembrosConvivencia
                                                {
                                                    Numero_Documento = miem.Numero_Documento,
                                                    Nombre = miem.Nombre,
                                                    Fk_Id_TipoPrincipal = miem.Fk_Id_TipoPrincipal,
                                                    Des_TipoPrincipal = x.DescripcionTipoPrincipal,
                                                    Fk_Id_TipoPrioridadMiembro = miem.Fk_Id_TipoPrioridadMiembro,
                                                    Des_TipoPrioridadMiembro = tPrio.DescripcionTipoMiembro,
                                                    TipoRepresentante = miem.TipoRepresentante,
                                                    PK_Id_Acta = id_Acta,

                                                }
                            ).ToList();

                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al Obtener los Miembros actas copasst  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return MiembrosActasConvivencia;
        }

        //OBTENER LISTA DE PARTICIPANTES DE UN ACTA CONVIVENCIA
        public List<EDParticipantesActaConvivencia> ObtenerParticipantesConvivenciaPorActa(int id_Acta)
        {
            List<EDParticipantesActaConvivencia> participanteActasConvivencia = new List<EDParticipantesActaConvivencia>();

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        //ActasConvivencia = (from act in context.Tbl_ActasConvivencia
                        //                where act.PK_Id_Acta == id_Acta
                        //                select act).FirstOrDefault();

                        participanteActasConvivencia = (from miem in context.Tbl_ParticipantesActasConvivencia
                                                    where miem.Fk_Id_Acta == id_Acta
                                                    select new EDParticipantesActaConvivencia
                                                    {
                                                        Numero_Documento = miem.Numero_Documento,
                                                        Nombre = miem.Nombre,
                                                        PK_Id_Acta = id_Acta,

                                                    }
                            ).ToList();

                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al Obtener los participantes actas copasst  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return participanteActasConvivencia;
        }

        //OBTENER LISTA DE TEMAS DE UN ACTA CONVIVENCIA
        public List<EDTemasActasConvivencia> ObtenerTemasConvivenciaPorActa(int id_Acta)
        {
            List<EDTemasActasConvivencia> temasActasConvivencia = new List<EDTemasActasConvivencia>();

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        temasActasConvivencia = (from miem in context.Tbl_TemasActaConvivencia
                                             where miem.Fk_Id_Acta == id_Acta
                                             select new EDTemasActasConvivencia
                                             {
                                                 PK_Id_TemaActa = miem.PK_Id_TemaActa,
                                                 Tema = miem.Tema,
                                                 Observaciones = miem.Observaciones,
                                                 PK_Id_Acta = id_Acta,
                                             }
                            ).ToList();

                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al Obtener los temas actas copasst  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return temasActasConvivencia;
        }

        //OBTENER LISTA DE ACCIONES DE UN ACTA CONVIVENCIA
        public List<EDAccionesActaConvivencia> ObtenerAccionesConvivenciaPorActa(int id_Acta)
        {
            List<EDAccionesActaConvivencia> accionesActasConvivencia = new List<EDAccionesActaConvivencia>();

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        accionesActasConvivencia = (from miem in context.Tbl_AccionesActaConvivencia
                                                where miem.Fk_Id_Acta == id_Acta
                                                select new EDAccionesActaConvivencia
                                                {
                                                    Pk_Id_AccionActaConvivencia = miem.Pk_Id_AccionActaConvivencia,
                                                    AccionARealizar = miem.AccionARealizar,
                                                    FechaProbable = miem.FechaProbable,
                                                    Responsable = miem.Responsable,
                                                    PK_Id_Acta = id_Acta,
                                                }
                            ).ToList();

                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al Obtener los temas actas copasst  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return accionesActasConvivencia;
        }

       //OBTENER QUEJA ACTA CONVIVENCIA
        public List<EDActaConvivenciaQuejas> ObtenerActasConvivenciaQueja(int IdSede)
        {
            List<EDActaConvivenciaQuejas> actasConvivenciaQueja = new List<EDActaConvivenciaQuejas>();

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        actasConvivenciaQueja = (from acQueja in context.Tbl_ActaConvivenciaQuejas
                                                where acQueja.Fk_Id_Sede == IdSede
                                                select new EDActaConvivenciaQuejas
                                                {
                                                    PK_Id_Queja = acQueja.PK_Id_Queja,
                                                    Consecutivo_Queja = acQueja.Consecutivo_Queja,
                                                    Consecutivo_Caso = acQueja.Consecutivo_Caso,
                                                    Fecha = acQueja.Fecha,
                                                    NombreRefiereSituacion = acQueja.NombreRefiereSituacion,
                                                    AspectosNoResueltos = acQueja.AspectosNoResueltos,
                                                    Compromisos = acQueja.Compromisos,
                                                    Fk_Id_Acta = acQueja.Fk_Id_Acta,
                                                    Fk_Id_Sede = acQueja.Fk_Id_Sede,
                                                }
                            ).OrderBy(x => x.Consecutivo_Queja).ToList();

                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al Obtener los temas actas copasst  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return actasConvivenciaQueja;
        }

      //OBTENER QUEJA ACTA CONVIVENCIA
        public List<EDSeguimientoActaConvivencia> ObtenerActasConvivenciaSeguimiento(int IdSede)
        {
            List<EDSeguimientoActaConvivencia> actasConvivenciaSeguimiento = new List<EDSeguimientoActaConvivencia>();

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        actasConvivenciaSeguimiento = (from acQueja in context.Tbl_SeguimientoActaConvivencia
                                                       where acQueja.Fk_Id_Sede == IdSede
                                                       select new EDSeguimientoActaConvivencia
                                                {
                                                    PK_Id_Seguimiento = acQueja.PK_Id_Seguimiento,
                                                    Consecutivo_Evento = acQueja.Consecutivo_Evento,
                                                    Fecha = acQueja.Fecha,
                                                    NombreParteInvolucrada = acQueja.NombreParteInvolucrada,
                                                    CompromisosAdquiridos = acQueja.CompromisosAdquiridos,
                                                    Observaciones = acQueja.Observaciones,
                                                    Fk_Id_Acta = acQueja.Fk_Id_Acta,
                                                    Fk_Id_Sede = acQueja.Fk_Id_Sede,
                                                }
                            ).OrderBy(x => x.Consecutivo_Evento).ToList();

                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al Obtener los seguimientos actas  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return actasConvivenciaSeguimiento;
        }

       //OBTENER QUEJA ACTA CONVIVENCIA
        public List<EDAccionesActaQuejas> ObtenerAccionesActasQueja(int PK_Id_Queja)
        {
            List<EDAccionesActaQuejas> accionesActasQueja = new List<EDAccionesActaQuejas>();

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        accionesActasQueja = (from acaQueja in context.Tbl_AccionesActaQuejas
                                              where acaQueja.Fk_Id_Queja == PK_Id_Queja
                                              select new EDAccionesActaQuejas
                                                {
                                                    Pk_Id_AccionQueja = acaQueja.Pk_Id_AccionQueja,
                                                    AccionARealizar = acaQueja.AccionARealizar,
                                                    Fk_Id_Queja = acaQueja.Fk_Id_Queja,
                                                }
                            ).ToList();

                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al Obtener los temas actas copasst  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return accionesActasQueja;
        }

       //OBTENER COMPROMISOS ACTA CONVIVENCIA
        public List<EDCompromisosPendientes> ObtenerCompromisosActaConvivencia(int PK_Id_Seguimiento)
        {
            List<EDCompromisosPendientes> compromisosActasSeguimiento = new List<EDCompromisosPendientes>();

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        compromisosActasSeguimiento = (from acaQueja in context.Tbl_CompromisosPendientes
                                                       where acaQueja.FK_Id_Seguimiento == PK_Id_Seguimiento
                                              select new EDCompromisosPendientes
                                                {
                                                    Pk_Id_Compromiso = acaQueja.Pk_Id_Compromiso,
                                                    CompromisoPendiente = acaQueja.CompromisoPendiente,
                                                    FK_Id_Seguimiento = acaQueja.FK_Id_Seguimiento,
                                                }
                            ).ToList();

                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al Obtener los compromisos al seguimiento  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return compromisosActasSeguimiento;
        }

       //OBTENER QUEJA ACTA CONVIVENCIA
        public List<EDResponsablesQuejas> ObtenerResponsablesQueja(int PK_Id_Queja)
        {
            List<EDResponsablesQuejas> responsablesActasQueja = new List<EDResponsablesQuejas>();

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        responsablesActasQueja = (from acaQueja in context.Tbl_ResponsablesQuejas
                                              where acaQueja.Fk_Id_Queja == PK_Id_Queja
                                              select new EDResponsablesQuejas
                                                {
                                                    Pk_Id_Responsable = acaQueja.Pk_Id_Responsable,
                                                    Numero_Documento = acaQueja.Numero_Documento,
                                                    Nombre = acaQueja.Nombre,
                                                    Fk_Id_Queja = acaQueja.Fk_Id_Queja,
                                                }
                            ).ToList();

                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al Obtener los temas actas copasst  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return responsablesActasQueja;
        }

        //ELIMINAR MIEMBRO DE UN ACTA CONVIVENCIA
        public List<EDMiembrosConvivencia> EliminarMiembroActaConvivencia(int Documento, int Usuario, string NombreUsuario, int PK_Id_Acta)
        {

            List<EDMiembrosConvivencia> MiembrosActasConvivencia = new List<EDMiembrosConvivencia>();
            MiembrosActaConvivencia miembrosBorrar = new MiembrosActaConvivencia();
            Participantes participanteBorrar = new Participantes();

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {


                        var actaGuardadaCon = (from m in context.Tbl_ActasConvivencia
                                               where m.PK_Id_Acta == PK_Id_Acta
                                               select m).FirstOrDefault();

                        actaGuardadaCon.NombreUsuario = NombreUsuario;
                        
                        miembrosBorrar = (from miem in context.Tbl_MiembrosActaConvivencia
                                          where miem.Fk_Id_Acta == PK_Id_Acta
                                          && miem.Numero_Documento == Documento
                                          select miem).First();

                        if (miembrosBorrar != null)
                        {
                            context.Tbl_MiembrosActaConvivencia.Remove(miembrosBorrar);
                        }

                        AuditoriaActaConvivencia auditoria = new AuditoriaActaConvivencia()
                        {

                            Fk_Id_UsuarioSistema = Usuario,
                            NombreUsuario = NombreUsuario,
                            Fecha = DateTime.Today,
                            Fk_Id_Acta = PK_Id_Acta,
                            AccionRealizada = "ELIMINO MIEMBRO DE ACTA CONVIVENCIA",
                        };
                        context.Tbl_AuditoriaActaConvivencia.Add(auditoria);

                        context.SaveChanges();
                        Transaction.Commit();


                        MiembrosActasConvivencia = (from miem in context.Tbl_MiembrosActaConvivencia
                                                join tPrio in context.Tbl_TipoPrioridadMiembroComite on miem.Fk_Id_TipoPrioridadMiembro equals tPrio.PK_Id_TipoPrioridadMiembro
                                                join tPrin in context.Tbl_TipoPrincipalActa on miem.Fk_Id_TipoPrincipal equals tPrin.PK_Id_TipoPrincipal into sr
                                                from x in sr.DefaultIfEmpty()
                                                where miem.Fk_Id_Acta == PK_Id_Acta
                                                select new EDMiembrosConvivencia
                                                {
                                                    Numero_Documento = miem.Numero_Documento,
                                                    Nombre = miem.Nombre,
                                                    Fk_Id_TipoPrincipal = miem.Fk_Id_TipoPrincipal,
                                                    Des_TipoPrincipal = x.DescripcionTipoPrincipal,
                                                    Fk_Id_TipoPrioridadMiembro = miem.Fk_Id_TipoPrioridadMiembro,
                                                    Des_TipoPrioridadMiembro = tPrio.DescripcionTipoMiembro,
                                                    TipoRepresentante = miem.TipoRepresentante,
                                                    PK_Id_Acta = PK_Id_Acta,

                                                }
                            ).ToList();

                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al Obtener las actas copasst  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return MiembrosActasConvivencia;
        }

        //ELIMINAR PARTICIPANTE DE UN ACTA CONVIVENCIA
        public List<EDParticipantesActaConvivencia> EliminarParticipanteConvivencia(int Documento, int Usuario, string NombreUsuario, int PK_Id_Acta)
        {

            List<EDParticipantesActaConvivencia> ParticipantesActasConvivencia = new List<EDParticipantesActaConvivencia>();
            MiembrosActaConvivencia miembrosBorrar = new MiembrosActaConvivencia();
            ParticipantesActasConvivencia participanteBorrar = new ParticipantesActasConvivencia();

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {


                        var actaGuardadaCon = (from m in context.Tbl_ActasConvivencia
                                               where m.PK_Id_Acta == PK_Id_Acta
                                               select m).FirstOrDefault();

                        actaGuardadaCon.NombreUsuario = NombreUsuario;

                        participanteBorrar = (from miem in context.Tbl_ParticipantesActasConvivencia
                                              where miem.Fk_Id_Acta == PK_Id_Acta
                                              && miem.Numero_Documento == Documento
                                              select miem).First();

                        if (participanteBorrar != null)
                            context.Tbl_ParticipantesActasConvivencia.Remove(participanteBorrar);

                        AuditoriaActaConvivencia auditoria = new AuditoriaActaConvivencia()
                        {

                            Fk_Id_UsuarioSistema = Usuario,
                            NombreUsuario = NombreUsuario,
                            Fecha = DateTime.Today,
                            Fk_Id_Acta = PK_Id_Acta,
                            AccionRealizada = "ELIMINO PARTICIPANTE DE ACTA CONVIVENCIA",
                        };
                        context.Tbl_AuditoriaActaConvivencia.Add(auditoria);

                        context.SaveChanges();
                        Transaction.Commit();


                        ParticipantesActasConvivencia = (from miem in context.Tbl_ParticipantesActasConvivencia
                                                     where miem.Fk_Id_Acta == PK_Id_Acta
                                                     select new EDParticipantesActaConvivencia
                                                     {
                                                         Numero_Documento = miem.Numero_Documento,
                                                         Nombre = miem.Nombre,
                                                         PK_Id_Acta = PK_Id_Acta,

                                                     }
                            ).ToList();

                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al eliminar participante actas copasst  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return ParticipantesActasConvivencia;
        }

        //ELIMINAR RESPONSABLE QUEJA DE UN ACTA CONVIVENCIA
        public List<EDResponsablesQuejas> EliminarResponsableQueja(int Documento, int Usuario, string NombreUsuario, int Fk_Id_Queja, int PK_Id_Acta)
        {

            List<EDResponsablesQuejas> ResponsableQueja = new List<EDResponsablesQuejas>();
            ResponsablesQuejas responsableBorrar = new ResponsablesQuejas();

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {


                        var actaGuardadaCon = (from m in context.Tbl_ActasConvivencia
                                               where m.PK_Id_Acta == PK_Id_Acta
                                               select m).FirstOrDefault();

                        actaGuardadaCon.NombreUsuario = NombreUsuario;
                        
                        responsableBorrar = (from miem in context.Tbl_ResponsablesQuejas
                                             where miem.Fk_Id_Queja == Fk_Id_Queja
                                              && miem.Numero_Documento == Documento
                                              select miem).First();

                        if (responsableBorrar != null)
                            context.Tbl_ResponsablesQuejas.Remove(responsableBorrar);

                        AuditoriaActaConvivencia auditoria = new AuditoriaActaConvivencia()
                        {

                            Fk_Id_UsuarioSistema = Usuario,
                            NombreUsuario = NombreUsuario,
                            Fecha = DateTime.Today,
                            Fk_Id_Acta = PK_Id_Acta,
                            AccionRealizada = "ELIMINO RESPONSABLE QUEJA DE ACTA CONVIVENCIA",
                        };
                        context.Tbl_AuditoriaActaConvivencia.Add(auditoria);

                        context.SaveChanges();
                        Transaction.Commit();


                        ResponsableQueja = (from miem in context.Tbl_ResponsablesQuejas
                                            where miem.Fk_Id_Queja == Fk_Id_Queja
                                                     select new EDResponsablesQuejas
                                                     {
                                                         Pk_Id_Responsable = miem.Pk_Id_Responsable,
                                                         Numero_Documento = miem.Numero_Documento,
                                                         Nombre = miem.Nombre,
                                                         Fk_Id_Queja = miem.Fk_Id_Queja,
                                                         PK_Id_Acta = PK_Id_Acta,

                                                     }
                            ).ToList();

                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al eliminar participante actas copasst  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return ResponsableQueja;
        }

        //ELIMINAR TEMA DE UN ACTA CONVIVENCIA
        public List<EDTemasActasConvivencia> EliminarTemaActaConvivencia(int PK_Id_TemaActa, int Usuario, string NombreUsuario, int PK_Id_Acta)
        {

            List<EDTemasActasConvivencia> temasActasConvivencia = new List<EDTemasActasConvivencia>();
            TemasActaConvivencia temaBorrar = new TemasActaConvivencia();

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {

                        var actaGuardadaCon = (from m in context.Tbl_ActasConvivencia
                                               where m.PK_Id_Acta == PK_Id_Acta
                                               select m).FirstOrDefault();

                        actaGuardadaCon.NombreUsuario = NombreUsuario;

                        temaBorrar = (from miem in context.Tbl_TemasActaConvivencia
                                      where miem.Fk_Id_Acta == PK_Id_Acta
                                      && miem.PK_Id_TemaActa == PK_Id_TemaActa
                                      select miem).First();


                        AuditoriaActaConvivencia auditoria = new AuditoriaActaConvivencia()
                        {

                            Fk_Id_UsuarioSistema = Usuario,
                            NombreUsuario = NombreUsuario,
                            Fecha = DateTime.Today,
                            Fk_Id_Acta = PK_Id_Acta,
                            AccionRealizada = "ELIMINO TEMA DE ACTA CONVIVENCIA",
                        };
                        if (temaBorrar != null)
                        {
                            context.Tbl_TemasActaConvivencia.Remove(temaBorrar);
                        }

                        context.Tbl_AuditoriaActaConvivencia.Add(auditoria);

                        context.SaveChanges();
                        Transaction.Commit();


                        temasActasConvivencia = (from miem in context.Tbl_TemasActaConvivencia
                                             where miem.Fk_Id_Acta == PK_Id_Acta
                                             select new EDTemasActasConvivencia
                                             {
                                                 Tema = miem.Tema,
                                                 Observaciones = miem.Observaciones,
                                                 PK_Id_TemaActa = miem.PK_Id_TemaActa,
                                                 PK_Id_Acta = PK_Id_Acta,

                                             }
                            ).ToList();

                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al eliminar tema actas copasst  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return temasActasConvivencia;
        }

        //ELIMINAR ACCION DE UN ACTA CONVIVENCIA
        public List<EDAccionesActaConvivencia> EliminarAccionActaConvivencia(int Pk_Id_AccionActaConvivencia, int Usuario, string NombreUsuario, int PK_Id_Acta)
        {

            List<EDAccionesActaConvivencia> accionesActasConvivencia = new List<EDAccionesActaConvivencia>();
            AccionesActaConvivencia accionBorrar = new AccionesActaConvivencia();

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {

                        var actaGuardadaCon = (from m in context.Tbl_ActasConvivencia
                                               where m.PK_Id_Acta == PK_Id_Acta
                                               select m).FirstOrDefault();

                        actaGuardadaCon.NombreUsuario = NombreUsuario;

                        accionBorrar = (from miem in context.Tbl_AccionesActaConvivencia
                                        where miem.Fk_Id_Acta == PK_Id_Acta
                                        && miem.Pk_Id_AccionActaConvivencia == Pk_Id_AccionActaConvivencia
                                        select miem).First();


                        AuditoriaActaConvivencia auditoria = new AuditoriaActaConvivencia()
                        {

                            Fk_Id_UsuarioSistema = Usuario,
                            NombreUsuario = NombreUsuario,
                            Fecha = DateTime.Today,
                            Fk_Id_Acta = PK_Id_Acta,
                            AccionRealizada = "ELIMINO ACCION A REALIZAR DE ACTA CONVIVENCIA",
                        };
                        if (accionBorrar != null)
                        {
                            context.Tbl_AccionesActaConvivencia.Remove(accionBorrar);
                        }

                        context.Tbl_AuditoriaActaConvivencia.Add(auditoria);

                        context.SaveChanges();
                        Transaction.Commit();


                        accionesActasConvivencia = (from miem in context.Tbl_AccionesActaConvivencia
                                                where miem.Fk_Id_Acta == PK_Id_Acta
                                                select new EDAccionesActaConvivencia
                                                {
                                                    AccionARealizar = miem.AccionARealizar,
                                                    Responsable = miem.Responsable,
                                                    FechaProbable = miem.FechaProbable,
                                                    PK_Id_Acta = PK_Id_Acta,
                                                    Pk_Id_AccionActaConvivencia = miem.Pk_Id_AccionActaConvivencia,

                                                }
                            ).ToList();

                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al eliminar accion actas copasst  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return accionesActasConvivencia;
        }

        //ELIMINAR ACCION DE UN ACTA CONVIVENCIA
        public List<EDAccionesActaQuejas> EliminarAccionActaQueja(int Pk_Id_AccionQueja, int Usuario, string NombreUsuario, int Fk_Id_Queja, int PK_Id_Acta)
        {

            List<EDAccionesActaQuejas> accionesActasQuejas = new List<EDAccionesActaQuejas>();
            AccionesActaQuejas accionBorrar = new AccionesActaQuejas();

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {

                        var actaGuardadaCon = (from m in context.Tbl_ActasConvivencia
                                               where m.PK_Id_Acta == PK_Id_Acta
                                               select m).FirstOrDefault();

                        actaGuardadaCon.NombreUsuario = NombreUsuario;

                        accionBorrar = (from miem in context.Tbl_AccionesActaQuejas
                                        where miem.Fk_Id_Queja == Fk_Id_Queja
                                        && miem.Pk_Id_AccionQueja == Pk_Id_AccionQueja
                                        select miem).First();


                        AuditoriaActaConvivencia auditoria = new AuditoriaActaConvivencia()
                        {

                            Fk_Id_UsuarioSistema = Usuario,
                            NombreUsuario = NombreUsuario,
                            Fecha = DateTime.Today,
                            Fk_Id_Acta = PK_Id_Acta,
                            AccionRealizada = "ELIMINO ACCION QUEJA DE ACTA CONVIVENCIA",
                        };
                        if (accionBorrar != null)
                        {
                            context.Tbl_AccionesActaQuejas.Remove(accionBorrar);
                        }

                        context.Tbl_AuditoriaActaConvivencia.Add(auditoria);

                        context.SaveChanges();
                        Transaction.Commit();


                        accionesActasQuejas = (from miem in context.Tbl_AccionesActaQuejas
                                               where miem.Fk_Id_Queja == Fk_Id_Queja
                                                select new EDAccionesActaQuejas
                                                {
                                                    AccionARealizar = miem.AccionARealizar,
                                                    PK_Id_Acta = PK_Id_Acta,
                                                    Pk_Id_AccionQueja = miem.Pk_Id_AccionQueja,
                                                    Fk_Id_Queja = miem.Fk_Id_Queja,

                                                }
                            ).ToList();

                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al eliminar accion Queja actas {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return accionesActasQuejas;
        }

        //ELIMINAR COMPROMISO SEGUIMIENTO
        public List<EDCompromisosPendientes> EliminarCompromisoSeguimiento(int Pk_Id_Compromiso, int Usuario, string NombreUsuario, int FK_Id_Seguimiento, int PK_Id_Acta)
        {

            List<EDCompromisosPendientes> compromisoSeg = new List<EDCompromisosPendientes>();
            CompromisosPendientes compromisoBorrar = new CompromisosPendientes();

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {

                        var actaGuardadaCon = (from m in context.Tbl_ActasConvivencia
                                               where m.PK_Id_Acta == PK_Id_Acta
                                               select m).FirstOrDefault();

                        actaGuardadaCon.NombreUsuario = NombreUsuario;
                        
                        compromisoBorrar = (from miem in context.Tbl_CompromisosPendientes
                                        where miem.FK_Id_Seguimiento == FK_Id_Seguimiento
                                        && miem.Pk_Id_Compromiso == Pk_Id_Compromiso
                                        select miem).First();


                        AuditoriaActaConvivencia auditoria = new AuditoriaActaConvivencia()
                        {

                            Fk_Id_UsuarioSistema = Usuario,
                            NombreUsuario = NombreUsuario,
                            Fecha = DateTime.Today,
                            Fk_Id_Acta = PK_Id_Acta,
                            AccionRealizada = "ELIMINO COMPROMISO A SEGUMIENTO",
                        };
                        if (compromisoBorrar != null)
                        {
                            context.Tbl_CompromisosPendientes.Remove(compromisoBorrar);
                        }

                        context.Tbl_AuditoriaActaConvivencia.Add(auditoria);

                        context.SaveChanges();
                        Transaction.Commit();


                        compromisoSeg = (from miem in context.Tbl_CompromisosPendientes
                                               where miem.FK_Id_Seguimiento == FK_Id_Seguimiento
                                                select new EDCompromisosPendientes
                                                {
                                                    CompromisoPendiente = miem.CompromisoPendiente,
                                                    PK_Id_Acta = PK_Id_Acta,
                                                    Pk_Id_Compromiso = miem.Pk_Id_Compromiso,
                                                    FK_Id_Seguimiento = miem.FK_Id_Seguimiento,

                                                }
                            ).ToList();

                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al eliminar accion Queja actas {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return compromisoSeg;
        }

        //IMPORTAR UN ACTA CONVIVENCIA
        public EDActasConvivencia ImportarActaConvivencia(EDActasConvivencia InformacionActaConvivencia)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        var actaGuardada = (from m in context.Tbl_ActasConvivencia
                                            where m.PK_Id_Acta == InformacionActaConvivencia.PK_Id_Acta
                                            select m).FirstOrDefault();

                        actaGuardada.NombreArchivo = InformacionActaConvivencia.NombreArchivo;

                        AuditoriaActaConvivencia auditoria = new AuditoriaActaConvivencia()
                        {

                            Fk_Id_UsuarioSistema = InformacionActaConvivencia.Fk_Id_UsuarioSistema,
                            NombreUsuario = InformacionActaConvivencia.NombreUsuario,
                            Fecha = DateTime.Today,
                            Fk_Id_Acta = actaGuardada.PK_Id_Acta,
                            AccionRealizada = "IMPORTO EL DOCUMENTO",
                        };

                        context.Tbl_AuditoriaActaConvivencia.Add(auditoria);

                        context.SaveChanges();
                        Transaction.Commit();


                        return InformacionActaConvivencia;
                    }
                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al registrar Miembro Acta Convivencia  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

        }

        //ACTUALIZAR UN ACTA CONVIVENCIA
        public EDActasConvivencia ActualizarActaConvivencia(EDActasConvivencia InformacionActaConvivencia)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        var actaGuardada = (from m in context.Tbl_ActasConvivencia
                                            where m.PK_Id_Acta == InformacionActaConvivencia.PK_Id_Acta
                                            select m).FirstOrDefault();

                        actaGuardada.Fecha = InformacionActaConvivencia.Fecha;
                        actaGuardada.TemaReunion = InformacionActaConvivencia.TemaReunion;
                        actaGuardada.Conclusiones = InformacionActaConvivencia.Conclusiones;
                        actaGuardada.NombreUsuario = InformacionActaConvivencia.NombreUsuario;

                        AuditoriaActaConvivencia auditoria = new AuditoriaActaConvivencia()
                        {

                            Fk_Id_UsuarioSistema = InformacionActaConvivencia.Fk_Id_UsuarioSistema,
                            NombreUsuario = InformacionActaConvivencia.NombreUsuario,
                            Fecha = DateTime.Today,
                            Fk_Id_Acta = actaGuardada.PK_Id_Acta,
                            AccionRealizada = "ACTUALIZO EL ACTA",
                        };

                        context.Tbl_AuditoriaActaConvivencia.Add(auditoria);

                        context.SaveChanges();
                        Transaction.Commit();

                        return InformacionActaConvivencia;
                    }
                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al registrar Miembro Acta Convivencia  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

        }

        //ACTUALIZAR UN ACTA CONVIVENCIA QUEJA
        public EDActaConvivenciaQuejas ActualizarActaConvivenciaQueja(EDActaConvivenciaQuejas InformacionActaConvivenciaQueja)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        var actaGuardadaCon = (from m in context.Tbl_ActasConvivencia
                                               where m.PK_Id_Acta == InformacionActaConvivenciaQueja.Fk_Id_Acta
                                            select m).FirstOrDefault();

                        actaGuardadaCon.NombreUsuario = InformacionActaConvivenciaQueja.NombreUsuario;

                        var actaGuardada = (from m in context.Tbl_ActaConvivenciaQuejas
                                            where m.PK_Id_Queja == InformacionActaConvivenciaQueja.PK_Id_Queja
                                            && m.Fk_Id_Acta == InformacionActaConvivenciaQueja.Fk_Id_Acta
                                            select m).FirstOrDefault();

                        actaGuardada.Fecha = InformacionActaConvivenciaQueja.Fecha;
                        actaGuardada.NombreRefiereSituacion = InformacionActaConvivenciaQueja.NombreRefiereSituacion;
                        actaGuardada.AspectosNoResueltos = InformacionActaConvivenciaQueja.AspectosNoResueltos;
                        actaGuardada.Compromisos = InformacionActaConvivenciaQueja.Compromisos;

                        AuditoriaActaConvivencia auditoria = new AuditoriaActaConvivencia()
                        {

                            Fk_Id_UsuarioSistema = InformacionActaConvivenciaQueja.UsuarioSistema,
                            NombreUsuario = InformacionActaConvivenciaQueja.NombreUsuario,
                            Fecha = DateTime.Today,
                            Fk_Id_Acta = actaGuardada.Fk_Id_Acta,
                            AccionRealizada = "ACTUALIZO EL SEGUMIENTO A LA QUEJA",
                        };

                        context.Tbl_AuditoriaActaConvivencia.Add(auditoria);

                        context.SaveChanges();
                        Transaction.Commit();

                        return InformacionActaConvivenciaQueja;
                    }
                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al registrar Miembro Acta Convivencia  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

        }

        //ACTUALIZAR UN ACTA CONVIVENCIA SEGUIMIENTO
        public EDSeguimientoActaConvivencia ActualizarActaConvivenciaSeguimiento(EDSeguimientoActaConvivencia InformacionActaConvivenciaSeg)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        var actaGuardadaCon = (from m in context.Tbl_ActasConvivencia
                                               where m.PK_Id_Acta == InformacionActaConvivenciaSeg.Fk_Id_Acta
                                               select m).FirstOrDefault();

                        actaGuardadaCon.NombreUsuario = InformacionActaConvivenciaSeg.NombreUsuario;

                        var actaGuardada = (from m in context.Tbl_SeguimientoActaConvivencia
                                            where m.PK_Id_Seguimiento == InformacionActaConvivenciaSeg.PK_Id_Seguimiento
                                            && m.Fk_Id_Acta == InformacionActaConvivenciaSeg.Fk_Id_Acta
                                            select m).FirstOrDefault();

                        actaGuardada.Fecha = InformacionActaConvivenciaSeg.Fecha;
                        actaGuardada.NombreParteInvolucrada = InformacionActaConvivenciaSeg.NombreParteInvolucrada;
                        actaGuardada.CompromisosAdquiridos = InformacionActaConvivenciaSeg.CompromisosAdquiridos;
                        actaGuardada.Observaciones = InformacionActaConvivenciaSeg.Observaciones;

                        AuditoriaActaConvivencia auditoria = new AuditoriaActaConvivencia()
                        {

                            Fk_Id_UsuarioSistema = InformacionActaConvivenciaSeg.UsuarioSistema,
                            NombreUsuario = InformacionActaConvivenciaSeg.NombreUsuario,
                            Fecha = DateTime.Today,
                            Fk_Id_Acta = actaGuardada.Fk_Id_Acta,
                            AccionRealizada = "ACTUALIZO EL SEGUMIENTO",
                        };

                        context.Tbl_AuditoriaActaConvivencia.Add(auditoria);

                        context.SaveChanges();
                        Transaction.Commit();

                        return InformacionActaConvivenciaSeg;
                    }
                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(ComiteManager), string.Format("Error al registrar Seguimiento Acta Convivencia  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

        }
    }
}
