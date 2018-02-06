using SG_SST.Audotoria;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.EntidadesDominio.Revision;
using SG_SST.Interfaces.Revision;
using SG_SST.Models;
using SG_SST.Models.Empresas;
using SG_SST.Models.MedicionEvaluacion;
using SG_SST.Models.Revision;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Repositorio.Revision
{
    public class RevisionManager : IRevision
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
                                where sede.Pk_Id_Sede == idsede
                                select new EDSede
                                {
                                    NombreEmpresa = Emp.Razon_Social,
                                    IDEmpresa = Emp.Nit_Empresa,
                                    NombreSede = sede.Nombre_Sede
                                }).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    registra.RegistrarError(typeof(RevisionManager), string.Format("Error al obtener la Información  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                    return null;
                }
            }

            return infosede;
        }

        //GUARDAR UN ACTA DE REVISIÓN
        public EDActaRevision GuardarActaRevision(EDActaRevision acta)
        {
            ActaRevision actaMod = new ActaRevision();
            actaMod.PK_Id_ActaRevision = acta.PK_Id_ActaRevision;
            actaMod.Nombre = acta.Nombre;
            actaMod.Num_Acta = acta.Num_Acta;
            actaMod.Fecha_Creacion_Acta = acta.Fecha_Creacion_Acta;
            actaMod.FK_Sede = acta.FK_Sede;
            actaMod.Fecha_Inicial_Revision = acta.Fecha_Inicial_Revision;
            actaMod.Fecha_Final_Revision = acta.Fecha_Final_Revision;

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {

                        var actaGuardada = (from m in context.Tbl_ActaRevision
                                            where m.Num_Acta == acta.Num_Acta
                                            && m.FK_Sede == acta.FK_Sede
                                            select m).FirstOrDefault();
                        //Guardado del acta
                        if (actaGuardada == null)
                        {
                            context.Tbl_ActaRevision.Add(actaMod);
                        }
                        else
                        {
                            actaGuardada.Nombre = acta.Nombre;
                            actaGuardada.Fecha_Creacion_Acta = acta.Fecha_Creacion_Acta;
                            actaGuardada.FK_Sede = acta.FK_Sede;
                            actaGuardada.Fecha_Inicial_Revision = acta.Fecha_Inicial_Revision;
                            actaGuardada.Fecha_Final_Revision = acta.Fecha_Final_Revision;
                            context.Entry(actaGuardada).State = EntityState.Modified;
                        }

                        //Guardado de los participantes
                        if (acta.Participantes != null && acta.Participantes.Count() > 0)
                        {
                            foreach (EDParticipanteRevision p in acta.Participantes)
                            {
                                var yaExiste = (from m in context.Tbl_ParticipanteRevision
                                                where m.Documento == p.Documento
                                                select m).FirstOrDefault();

                                if (yaExiste == null)
                                {
                                    ParticipanteRevision part = new ParticipanteRevision()
                                    {
                                        Documento = p.Documento,
                                        Nombre = p.Nombre,
                                        Cargo = p.Cargo
                                    };
                                    context.Tbl_ParticipanteRevision.Add(part);
                                }
                                else
                                {
                                    yaExiste.Nombre = p.Nombre;
                                    yaExiste.Cargo = p.Cargo;
                                    context.Entry(yaExiste).State = EntityState.Modified;
                                }
                            }
                        }

                        context.SaveChanges();
                        Transaction.Commit();

                        var actaRevision = (from ac in context.Tbl_ActaRevision
                                            where ac.Num_Acta == acta.Num_Acta
                                            && ac.FK_Sede == acta.FK_Sede
                                            select new EDActaRevision
                                            {
                                                PK_Id_ActaRevision = ac.PK_Id_ActaRevision,
                                                Nombre = ac.Nombre,
                                                Num_Acta = ac.Num_Acta,
                                                Fecha_Creacion_Acta = ac.Fecha_Creacion_Acta,
                                                FK_Sede = ac.FK_Sede,
                                                Fecha_Inicial_Revision = ac.Fecha_Inicial_Revision,
                                                Fecha_Final_Revision = ac.Fecha_Final_Revision,
                                                Elaborada = ac.Elaborada,
                                                Firma_Gerente_General = ac.Firma_Gerente_General,
                                                Firma_Representante_SGSST = ac.Firma_Representante_SGSST,
                                                Firma_Responsable_SGSST = ac.Firma_Responsable_SGSST
                                            }
                            ).FirstOrDefault();

                        return actaRevision;
                    }
                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(RevisionManager), string.Format("Error al registrar participante Acta {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

        }

        public EDActaRevision GuardarParticipanteRevision(EDActaRevision acta)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                ActaRevision actaNueva = null;
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        ActaRevision actaGuardada = new ActaRevision();
                        actaGuardada = (from m in context.Tbl_ActaRevision
                                        where m.Num_Acta == acta.Num_Acta
                                        && m.FK_Sede == acta.FK_Sede
                                        select m).FirstOrDefault();

                        if (actaGuardada == null || actaGuardada == default(ActaRevision))
                        {
                            actaNueva = new ActaRevision()
                            {
                                PK_Id_ActaRevision = acta.PK_Id_ActaRevision,
                                Nombre = acta.Nombre,
                                Num_Acta = acta.Num_Acta,
                                Fecha_Creacion_Acta = acta.Fecha_Creacion_Acta,
                                FK_Sede = acta.FK_Sede,
                                Fk_Id_Empresa = acta.FK_Empresa,
                                Fecha_Inicial_Revision = acta.Fecha_Inicial_Revision,
                                Fecha_Final_Revision = acta.Fecha_Final_Revision,
                                Elaborada = acta.Elaborada,
                                Firma_Gerente_General = acta.Firma_Gerente_General,
                                Firma_Representante_SGSST = acta.Firma_Representante_SGSST == null ? false : acta.Firma_Representante_SGSST,
                                Firma_Responsable_SGSST = acta.Firma_Responsable_SGSST == null ? false : acta.Firma_Responsable_SGSST
                            };
                            context.Tbl_ActaRevision.Add(actaNueva);

                            ParticipanteRevision part = new ParticipanteRevision()
                            {
                                Documento = acta.DocumentoParticipante,
                                Nombre = acta.NombreParticipante,
                                Cargo = acta.CargoParticipante
                            };
                            context.Tbl_ParticipanteRevision.Add(part);

                        }
                        else
                        {
                            var yaExiste = (from m in context.Tbl_ParticipanteRevision
                                            where m.FK_ActaRevision == actaGuardada.PK_Id_ActaRevision
                                            && m.Documento == acta.DocumentoParticipante
                                            select m);

                            if (yaExiste.Count() == 0)
                            {
                                ParticipanteRevision part = new ParticipanteRevision()
                                {
                                    Documento = acta.DocumentoParticipante,
                                    Nombre = acta.NombreParticipante,
                                    Cargo = acta.CargoParticipante,
                                    FK_ActaRevision = actaGuardada.PK_Id_ActaRevision
                                };
                                context.Tbl_ParticipanteRevision.Add(part);
                            }
                        }

                        context.SaveChanges();
                        Transaction.Commit();

                        if (actaGuardada == null)
                        {
                            acta.PK_Id_ActaRevision = actaNueva.PK_Id_ActaRevision;
                            acta.FK_ActaRevision = actaNueva.PK_Id_ActaRevision;
                        }
                        else
                        {
                            acta.PK_Id_ActaRevision = actaGuardada.PK_Id_ActaRevision;
                            acta.FK_ActaRevision = actaGuardada.PK_Id_ActaRevision;
                        }

                        return acta;
                    }
                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(RevisionManager), string.Format("Error al registrar Participante Acta Revisión  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

        }

        //ELIMINAR PARTICIPANTE DE UN ACTA DE REVISIÓN
        public List<EDParticipanteRevision> EliminarParticipanteRevision(string Documento, int PK_Id_Acta)
        {

            List<EDParticipanteRevision> ParticipantesActasRevision = new List<EDParticipanteRevision>();
            ParticipanteRevision participanteBorrar = new ParticipanteRevision();

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        participanteBorrar = (from part in context.Tbl_ParticipanteRevision
                                              where part.FK_ActaRevision == PK_Id_Acta
                                              && part.Documento == Documento
                                              select part).First();

                        if (participanteBorrar != null)
                            context.Tbl_ParticipanteRevision.Remove(participanteBorrar);

                        context.SaveChanges();
                        Transaction.Commit();


                        ParticipantesActasRevision = (from pa in context.Tbl_ParticipanteRevision
                                                      where pa.FK_ActaRevision == PK_Id_Acta
                                                      select new EDParticipanteRevision
                                                      {
                                                          Documento = pa.Documento,
                                                          Nombre = pa.Nombre,
                                                          Cargo = pa.Cargo,
                                                          FK_ActaRevision = PK_Id_Acta
                                                      }
                            ).ToList();

                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(RevisionManager), string.Format("Error al eliminar participante acta revision  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }
            return ParticipantesActasRevision;
        }

        //OBTENER LISTA DE PARTICIPANTES DE UN ACTA DE REVISIÓN
        public List<EDParticipanteRevision> ObtenerParticipantesRevisionPorActa(int id_Acta)
        {
            List<EDParticipanteRevision> participanteActasCopasst = new List<EDParticipanteRevision>();

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        participanteActasCopasst = (from p in context.Tbl_ParticipanteRevision
                                                    where p.FK_ActaRevision == id_Acta
                                                    select new EDParticipanteRevision
                                                    {
                                                        Documento = p.Documento,
                                                        Nombre = p.Nombre,
                                                        Cargo = p.Cargo,
                                                        FK_ActaRevision = id_Acta,
                                                    }
                            ).ToList();

                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(RevisionManager), string.Format("Error al Obtener los participantes actas revisión  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }
            return participanteActasCopasst;
        }

        public List<EDActaRevision> ObtenerActasRevisionPorEmpresa(int id)
        {
            List<EDActaRevision> ActasRevision = null;

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        ActasRevision = (from act in context.Tbl_ActaRevision
                                         where act.Fk_Id_Empresa == id
                                         select new EDActaRevision
                                         {
                                             Num_Acta = act.Num_Acta,
                                             Nombre = act.Nombre,
                                             Fecha_Creacion_Acta = act.Fecha_Creacion_Acta,
                                             FK_Sede = act.FK_Sede,
                                             FK_Empresa = act.Fk_Id_Empresa,
                                             PK_Id_ActaRevision = act.PK_Id_ActaRevision
                                         }).ToList();
                        ActasRevision = ActasRevision.OrderByDescending(x => x.Fecha_Creacion_Acta).ThenByDescending(x => x.Num_Acta).ToList();
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(RevisionManager), string.Format("Error al Obtener las actas revision  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }
            return ActasRevision;
        }

        public EDActaRevision ObtenerActaRevisionPorId(int Id_Acta)
        {
            EDActaRevision ActaR = null;

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        ActaR = (from ac in context.Tbl_ActaRevision
                                 where ac.PK_Id_ActaRevision == Id_Acta
                                 select new EDActaRevision
                                 {
                                     PK_Id_ActaRevision = ac.PK_Id_ActaRevision,
                                     Nombre = ac.Nombre,
                                     Num_Acta = ac.Num_Acta,
                                     Fecha_Creacion_Acta = ac.Fecha_Creacion_Acta,
                                     FK_Empresa = ac.Fk_Id_Empresa,
                                     FK_Sede = ac.FK_Sede,
                                     Fecha_Inicial_Revision = ac.Fecha_Inicial_Revision,
                                     Fecha_Final_Revision = ac.Fecha_Final_Revision,
                                     Elaborada = ac.Elaborada,
                                     Firma_Gerente_General = ac.Firma_Gerente_General,
                                     Firma_Representante_SGSST = ac.Firma_Representante_SGSST,
                                     Firma_Responsable_SGSST = ac.Firma_Responsable_SGSST
                                 }).First();
                        Empresa empresa = new Empresa();
                        empresa = (from ac in context.Tbl_Empresa
                                   where ac.Pk_Id_Empresa == ActaR.FK_Empresa
                                   select ac).FirstOrDefault();
                        ActaR.NombreEmpresa = empresa.Razon_Social;
                        ActaR.NitEmpresa = empresa.Nit_Empresa;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(RevisionManager), string.Format("Error al Obtener el acta revisión  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return ActaR;
        }

        public List<EDItemRevision> ObtenerItemsRevision()
        {
            List<EDItemRevision> ItemsRevision = null;

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        ItemsRevision = (from act in context.Tbl_ItemRevision
                                         select new EDItemRevision
                                         {
                                             PK_Id_ItemRevision = act.PK_Id_ItemRevision,
                                             Tema = act.Tema
                                         }).ToList();
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(RevisionManager), string.Format("Error al obtener los items de la revisión  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return ItemsRevision;
        }


        public List<EDAgendaRevision> ObtenerTemas(int IdActa)
        {
            List<EDAgendaRevision> temas = null;

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        temas = (from act in context.Tbl_AgendaRevision
                                 where act.FK_ActaRevision==IdActa
                                         select new EDAgendaRevision
                                         {
                                             PK_Id_Agenda = act.PK_Id_Agenda,
                                             Titulo = act.Titulo,
                                             Desarrollo=act.Desarrollo,
                                         }).ToList();
                        foreach(var t in temas)
                        {
                            var adjuntosTemaAgendaPorRevision = (from adjpa in context.Tbl_AdjuntoAgendaRevision
                                                                 where adjpa.FK_AgendaRevision == t.PK_Id_Agenda
                                                                 select new EDAdjuntoAgendaRevision
                                                                 {
                                                                     FK_AgendaRevision = t.PK_Id_Agenda,
                                                                     Nombre_Archivo = adjpa.Nombre_Archivo,
                                                                     PK_Id_AdjuntoAgendaRevision = adjpa.PK_Id_AdjuntoAgendaRevision
                                                                 }
                                                                ).ToList();
                            t.AdjuntosAgendaRevision = adjuntosTemaAgendaPorRevision;
                        }
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(RevisionManager), string.Format("Error al obtener los temas de la revisión  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return temas;
        }



        public EDActaRevision GuardarTemaAgendaRevision(EDActaRevision acta)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        ActaRevision actaGuardada = new ActaRevision();
                        actaGuardada = (from m in context.Tbl_ActaRevision
                                        where m.PK_Id_ActaRevision == acta.PK_Id_ActaRevision
                                        select m).FirstOrDefault();

                        if (actaGuardada != null && actaGuardada != default(ActaRevision))
                        {
                            var yaExiste = (from m in context.Tbl_AgendaRevision
                                            where m.FK_ActaRevision == actaGuardada.PK_Id_ActaRevision
                                            && m.Titulo == acta.Tema
                                            select m);

                            if (yaExiste.Count() == 0)
                            {
                                AgendaRevision part = new AgendaRevision()
                                {
                                    Titulo = acta.Tema,
                                    Desarrollo = acta.Desarrollo,
                                    FK_ActaRevision = actaGuardada.PK_Id_ActaRevision
                                };
                                context.Tbl_AgendaRevision.Add(part);
                            }
                        }

                        context.SaveChanges();
                        Transaction.Commit();

                        var AgendaPorRevision = (from pa in context.Tbl_AgendaRevision
                                                 where pa.FK_ActaRevision == acta.PK_Id_ActaRevision
                                                 select new EDAgendaRevision
                                                 {
                                                     PK_Id_Agenda = pa.PK_Id_Agenda,
                                                     Titulo = pa.Titulo,
                                                     Desarrollo = pa.Desarrollo,
                                                     FK_ActaRevision = acta.PK_Id_ActaRevision
                                                 }
                                            ).ToList();

                        acta.Agenda = new List<EDAgendaRevision>();
                        acta.Agenda = AgendaPorRevision;
                        return acta;
                    }
                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(RevisionManager), string.Format("Error al registrar Participante Acta Revisión  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }
        }

        public EDAgendaRevision GuardarDesarrolloTemaAgendaRevision(EDAgendaRevision tema)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        var yaExiste = (from m in context.Tbl_AgendaRevision
                                        where m.FK_ActaRevision == tema.FK_ActaRevision
                                        && m.Titulo == tema.Titulo
                                        select m).First();

                        if (yaExiste != null)
                        {
                            yaExiste.Desarrollo = tema.Desarrollo;
                            context.Entry(yaExiste).State = EntityState.Modified;
                        }

                        context.SaveChanges();
                        Transaction.Commit();

                        var temaAgendaPorRevision = (from pa in context.Tbl_AgendaRevision
                                                     where pa.PK_Id_Agenda == tema.PK_Id_Agenda
                                                     select new EDAgendaRevision
                                                     {
                                                         PK_Id_Agenda = tema.PK_Id_Agenda,
                                                         Titulo = pa.Titulo,
                                                         Desarrollo = pa.Desarrollo,
                                                         FK_ActaRevision = pa.FK_ActaRevision
                                                     }
                                                    ).First();
                        var adjuntosTemaAgendaPorRevision = (from adjpa in context.Tbl_AdjuntoAgendaRevision
                                                             where adjpa.FK_AgendaRevision == tema.PK_Id_Agenda
                                                             select new EDAdjuntoAgendaRevision
                                                             {
                                                                 FK_AgendaRevision = tema.PK_Id_Agenda,
                                                                 Nombre_Archivo = adjpa.Nombre_Archivo,
                                                                 PK_Id_AdjuntoAgendaRevision = adjpa.PK_Id_AdjuntoAgendaRevision
                                                             }
                                                            ).ToList();

                        temaAgendaPorRevision.AdjuntosAgendaRevision = adjuntosTemaAgendaPorRevision;
                        return temaAgendaPorRevision;
                    }
                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(RevisionManager), string.Format("Error al registrar Participante Acta Revisión  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }
        }

        public List<EDAgendaRevision> ObtenerAgendaPorActaRevision(int id)
        {
            List<EDAgendaRevision> AgendasActaRevision = null;

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        AgendasActaRevision = (from act in context.Tbl_AgendaRevision
                                               where act.FK_ActaRevision == id
                                               select new EDAgendaRevision
                                               {
                                                   PK_Id_Agenda = act.PK_Id_Agenda,
                                                   Titulo = act.Titulo,
                                                   Desarrollo = act.Desarrollo,
                                                   FK_ActaRevision = act.FK_ActaRevision
                                               }).ToList();
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(RevisionManager), string.Format("Error al Obtener las agendas de un acta revision  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }
            return AgendasActaRevision;
        }

        public List<EDAgendaRevision> EliminarTemaAgendaRevision(int IdTema, int PK_Id_Acta)
        {
            List<EDAgendaRevision> TemasActasRevision = new List<EDAgendaRevision>();
            AgendaRevision temaBorrar = new AgendaRevision();
            List<AdjuntoAgendaRevision> adjuntosBorrar = new List<AdjuntoAgendaRevision>();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        temaBorrar = (from part in context.Tbl_AgendaRevision
                                      where part.FK_ActaRevision == PK_Id_Acta
                                      && part.PK_Id_Agenda == IdTema
                                      select part).First();

                        if (temaBorrar != null)
                        {
                            adjuntosBorrar = (from part in context.Tbl_AdjuntoAgendaRevision
                                              where part.FK_AgendaRevision == temaBorrar.PK_Id_Agenda
                                              select part).ToList();

                            foreach (AdjuntoAgendaRevision q in adjuntosBorrar)
                            {
                                context.Tbl_AdjuntoAgendaRevision.Remove(q);
                            }

                            context.Tbl_AgendaRevision.Remove(temaBorrar);
                        }

                        context.SaveChanges();
                        Transaction.Commit();


                        TemasActasRevision = (from tem in context.Tbl_AgendaRevision
                                              where tem.FK_ActaRevision == PK_Id_Acta
                                              select new EDAgendaRevision
                                              {
                                                  PK_Id_Agenda = tem.PK_Id_Agenda,
                                                  Titulo = tem.Titulo,
                                                  Desarrollo = tem.Desarrollo,
                                                  FK_ActaRevision = tem.FK_ActaRevision
                                              }
                            ).ToList();

                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(RevisionManager), string.Format("Error al eliminar participante acta revision  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }
            return TemasActasRevision;
        }

        public List<EDActaRevision> EliminarActaRevision(int PK_Id_Acta)
        {
            List<EDActaRevision> actas = new List<EDActaRevision>();
            ActaRevision actaBorrar = new ActaRevision();
            List<AdjuntoAgendaRevision> adjuntosBorrar = new List<AdjuntoAgendaRevision>();
            List<AgendaRevision> agendaBorrar = new List<AgendaRevision>();
            List<PlanAccionRevision> planesAccionBorrar = new List<PlanAccionRevision>();
            List<ParticipanteRevision> participantesBorrar = new List<ParticipanteRevision>();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        agendaBorrar = (from part in context.Tbl_AgendaRevision
                                          where part.FK_ActaRevision == PK_Id_Acta
                                          select part).ToList();

                        foreach (AgendaRevision p in agendaBorrar)
                        {
                             adjuntosBorrar = (from part in context.Tbl_AdjuntoAgendaRevision
                                                   where part.FK_AgendaRevision == p.PK_Id_Agenda
                                                   select part).ToList();

                            foreach (AdjuntoAgendaRevision q in adjuntosBorrar)
                            {
                                context.Tbl_AdjuntoAgendaRevision.Remove(q);
                            }

                           context.Tbl_AgendaRevision.Remove(p);
                        }

                        planesAccionBorrar = (from part in context.Tbl_PlanAccionRevision
                                               where part.FK_Acta == PK_Id_Acta
                                               select part).ToList();

                        foreach (PlanAccionRevision p in planesAccionBorrar)
                        {
                            context.Tbl_PlanAccionRevision.Remove(p);
                        }

                        participantesBorrar = (from part in context.Tbl_ParticipanteRevision
                                               where part.FK_ActaRevision == PK_Id_Acta
                                               select part).ToList();

                        foreach (ParticipanteRevision p in participantesBorrar)
                        {
                            context.Tbl_ParticipanteRevision.Remove(p);
                        }

                        actaBorrar = (from act in context.Tbl_ActaRevision
                                      where act.PK_Id_ActaRevision == PK_Id_Acta
                                      select act).First();

                        if (actaBorrar != null)
                        {
                            context.Tbl_ActaRevision.Remove(actaBorrar);
                        }
                        context.SaveChanges();
                        Transaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(RevisionManager), string.Format("Error al eliminar participante acta revision  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }
            return actas;
        }

        public string ValidarExisteFirma(int idEmpresa, string descripcion)
        {
            Usuario edUsuario = null;
            string img = null;
            RegistraLog registraLog = new RegistraLog();
            try
            {
                using (var context = new SG_SSTContext())
                {
                    edUsuario = (from u in context.Tbl_Usuario
                                join r in context.Tbl_UsuarioRol on u.Pk_Id_Usuario equals r.Fk_Id_Usuario
                                join b in context.Tbl_Rol on r.Fk_Id_Rol equals b.Pk_Id_Rol
                                where b.Descripcion == descripcion && u.Fk_Id_Empresa == idEmpresa
                                select u

                               ).FirstOrDefault<Usuario>();

                }
                if (edUsuario != null && edUsuario != default(Usuario))
                {
                    img = edUsuario.Imagen_Firma;
                }
            }
            catch (Exception ex)
            {
                registraLog.RegistrarError(typeof(RevisionManager), string.Format("Error al validar existencia de firma  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
            }
            return img;

        }

        public EDAgendaRevision ObtenerTemaAgendaPorActaRevision(int id)
        {
            EDAgendaRevision temaAgendaActaRevision = null;

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        AgendaRevision temaAg = (from act in context.Tbl_AgendaRevision
                                                 where act.PK_Id_Agenda == id
                                                 select act).First();
                        if(temaAg != null && temaAg != default(AgendaRevision))
                        {
                            temaAgendaActaRevision = new EDAgendaRevision();
                            temaAgendaActaRevision.PK_Id_Agenda = temaAg.PK_Id_Agenda;
                            temaAgendaActaRevision.Titulo = temaAg.Titulo;
                            temaAgendaActaRevision.Desarrollo = temaAg.Desarrollo;
                            temaAgendaActaRevision.FK_ActaRevision = temaAg.FK_ActaRevision;
                            var adjuntosTemaAgendaPorRevision = (from adjpa in context.Tbl_AdjuntoAgendaRevision
                                                                 where adjpa.FK_AgendaRevision == id
                                                                 select new EDAdjuntoAgendaRevision
                                                                 {
                                                                     FK_AgendaRevision = id,
                                                                     Nombre_Archivo = adjpa.Nombre_Archivo,
                                                                     PK_Id_AdjuntoAgendaRevision = adjpa.PK_Id_AdjuntoAgendaRevision
                                                                 }
                                                                ).ToList();
                            temaAgendaActaRevision.AdjuntosAgendaRevision = adjuntosTemaAgendaPorRevision;
                        }
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(RevisionManager), string.Format("Error al Obtener el tema de la agenda de un acta revision  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }
            return temaAgendaActaRevision;
        }

        public EDAdjuntoAgendaRevision GuardarAdjuntoTemaAgendaRevision(EDAdjuntoAgendaRevision adjunto)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        AdjuntoAgendaRevision nuevo = new AdjuntoAgendaRevision();                        
                        nuevo.FK_AgendaRevision = adjunto.FK_AgendaRevision;
                        nuevo.Nombre_Archivo = adjunto.Nombre_Archivo;
                        context.Tbl_AdjuntoAgendaRevision.Add(nuevo);
                        context.SaveChanges();
                        Transaction.Commit();
                        return adjunto;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(RevisionManager), string.Format("Error al registrar la Modificacion de la Condicion Insegura  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }
        }

        public List<EDAdjuntoAgendaRevision> EliminarAdjuntoTemaAgendaRevision(int idAdjunto, int idAgenda)
        {
            List<EDAdjuntoAgendaRevision> AdjuntosTemaActaRevision = new List<EDAdjuntoAgendaRevision>();
            AdjuntoAgendaRevision adjuntoBorrar = new AdjuntoAgendaRevision();
            List<AdjuntoAgendaRevision> adjuntosBorrar = new List<AdjuntoAgendaRevision>();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        adjuntoBorrar = (from part in context.Tbl_AdjuntoAgendaRevision
                                      where part.PK_Id_AdjuntoAgendaRevision == idAdjunto
                                      select part).First();

                        if (adjuntoBorrar != null)
                        {
                            context.Tbl_AdjuntoAgendaRevision.Remove(adjuntoBorrar);
                        }

                        context.SaveChanges();
                        Transaction.Commit();


                        AdjuntosTemaActaRevision = (from tem in context.Tbl_AdjuntoAgendaRevision
                                              where tem.FK_AgendaRevision == idAgenda
                                              select new EDAdjuntoAgendaRevision
                                              {
                                                  PK_Id_AdjuntoAgendaRevision = tem.PK_Id_AdjuntoAgendaRevision,
                                                  FK_AgendaRevision = tem.FK_AgendaRevision,
                                                  Nombre_Archivo = tem.Nombre_Archivo
                                              }
                            ).ToList();

                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(RevisionManager), string.Format("Error al eliminar participante acta revision  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }
            return AdjuntosTemaActaRevision;
        }

        public bool ValidarExistePlanAccionCerradoRevision(int idActa)
        {
            ActividadPlanDeAccion plan = null;
            bool cerrado = false;
            RegistraLog registraLog = new RegistraLog();
            try
            {
                using (var context = new SG_SSTContext())
                {
                    plan = (from u in context.Tbl_Actividad_Plan_Accion                                 
                                 where u.Fk_Id_ModuloPlanAccion == 8 && u.Fk_Plan_Inspección == idActa
                                 select u
                               ).FirstOrDefault();

                }
                if (plan != null && plan != default(ActividadPlanDeAccion))
                {
                    cerrado = true;
                }
            }
            catch (Exception ex)
            {
                registraLog.RegistrarError(typeof(RevisionManager), string.Format("Error al validar existencia de plan de acción cerrado de Acta Revisión  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
            }
            return cerrado;

        }


        /// <summary>
        /// Metodos de persistencia Robinson 
        /// </summary>
        /// <param name="IdActa"></param>
        /// <returns></returns>
        public List<EDAgendaRevision> ObtenerTemasPlan(int IdActa)
        {
            List<EDAgendaRevision> temas = null;

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        temas = (from act in context.Tbl_AgendaRevision
                                 join ar in context.Tbl_ActaRevision
                                 on act.FK_ActaRevision equals ar.PK_Id_ActaRevision
                                 where act.FK_ActaRevision == IdActa
                                 select new EDAgendaRevision
                                 {
                                     PK_Id_Agenda = act.PK_Id_Agenda,
                                     Titulo = act.Titulo,
                                     Desarrollo = act.Desarrollo,
                                     FK_ActaRevision = act.FK_ActaRevision,
                                     ConsecutivoActaEmpresaED = ar.Num_Acta,
                                 }).ToList();
                        foreach (var t in temas)
                        {
                            var adjuntosTemaAgendaPorRevision = (from adjpa in context.Tbl_AdjuntoAgendaRevision
                                                                 where adjpa.FK_AgendaRevision == t.PK_Id_Agenda
                                                                 select new EDAdjuntoAgendaRevision
                                                                 {
                                                                     FK_AgendaRevision = t.PK_Id_Agenda,
                                                                     Nombre_Archivo = adjpa.Nombre_Archivo,
                                                                     PK_Id_AdjuntoAgendaRevision = adjpa.PK_Id_AdjuntoAgendaRevision
                                                                 }
                                                                ).ToList();
                            t.AdjuntosAgendaRevision = adjuntosTemaAgendaPorRevision;
                        }

                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(RevisionManager), string.Format("Error al obtener los temas de la revisión  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }

            return temas;
        }

        public EDPlanAccionRevision GrabarPlanAccionRevision(EDPlanAccionRevision planaccionrev)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        PlanAccionRevision par = new PlanAccionRevision()
                        {
                            PK_Id_PlanAccion = planaccionrev.PK_Id_PlanAccion,
                            Actividad = planaccionrev.Actividad,
                            Responsable = planaccionrev.Responsable,
                            Fecha = planaccionrev.Fecha,
                            FK_Acta = planaccionrev.FK_Acta,
                            Num_Acta = planaccionrev.Num_Acta.ToString(),

                        };
                        context.Tbl_PlanAccionRevision.Add(par);
                        context.SaveChanges();
                        Transaction.Commit();
                        return planaccionrev;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(RevisionManager), string.Format("Error al grabar el plan de accion de la revisión  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }

                }

            }
        }

        public List<EDPlanAccionRevision> ObtenerplanesporActa(int fkacta)
        {
            List<EDPlanAccionRevision> planrevision = new List<EDPlanAccionRevision>();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        planrevision = (from pr in context.Tbl_PlanAccionRevision
                                        join ar in context.Tbl_ActaRevision on pr.FK_Acta equals ar.PK_Id_ActaRevision
                                        where pr.FK_Acta == fkacta
                                        select new EDPlanAccionRevision()
                                        {
                                            PK_Id_PlanAccion = pr.PK_Id_PlanAccion,
                                            Actividad = pr.Actividad,
                                            Responsable = pr.Responsable,
                                            Fecha = pr.Fecha,
                                            FK_Acta = ar.PK_Id_ActaRevision,

                                        }).ToList();
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(RevisionManager), string.Format("Error al Obtener la Información  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }
            return planrevision;

        }

        public EDActaRevision GuardaFirmaGerente(EDActaRevision actarevision)
        {
            var actarevisiond = actarevision.FK_ActaRevision;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        ActaRevision edit = new ActaRevision();
                        edit = (from ar in context.Tbl_ActaRevision
                                where ar.PK_Id_ActaRevision == actarevisiond
                                select ar).FirstOrDefault();
                        if (string.IsNullOrEmpty(edit.Firma_Gerente_General) || !string.IsNullOrEmpty(actarevision.Firma_Gerente_General))
                        {
                            edit.Firma_Gerente_General = actarevision.Firma_Gerente_General;
                        }
                        edit.Elaborada = actarevision.Elaborada;


                        context.SaveChanges();

                        //context.Entry(edit).State = EntityState.Modified;
                        Transaction.Commit();

                        return actarevision;

                    }

                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(RevisionManager), string.Format("Error al registrar la Modificacion de la Condicion Insegura  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }

                }

            }


        }

        public EDActaRevision ValidarFirmasRSRL(int idacta)
        {

            var Firmas = new EDActaRevision();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {

                    RegistraLog registraLog = new RegistraLog();

                    try
                    {
                        Firmas = (from ar in context.Tbl_ActaRevision
                                  where ar.PK_Id_ActaRevision == idacta
                                  select new EDActaRevision()
                                  {
                                      Firma_Representante_SGSST = ar.Firma_Representante_SGSST,
                                      Firma_Responsable_SGSST = ar.Firma_Responsable_SGSST,
                                      Elaborada = ar.Elaborada,
                                      Firma_Gerente_General = ar.Firma_Gerente_General
                                  }).FirstOrDefault();

                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(RevisionManager), string.Format("Error al obtener la información  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;

                    }
                }
                return Firmas;
            }

        }

        public EDUsuario validarfirmareplegal(int Idempresa)
        {
            //EDUsuario edperfil = null;
            var edperfil = new EDUsuario();
            //var edperfil = new EDUsuario();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {

                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        edperfil = (from u in context.Tbl_Usuario
                                    join r in context.Tbl_UsuarioRol on u.Pk_Id_Usuario equals r.Fk_Id_Usuario
                                    join b in context.Tbl_Rol on r.Fk_Id_Rol equals b.Pk_Id_Rol
                                    where b.Descripcion == "REPRESENTANTE LEGAL" && u.Fk_Id_Empresa == Idempresa
                                    select new EDUsuario()
                                    {
                                        PkUsuarioED = u.Pk_Id_Usuario,
                                        NumeroDocumentoED = u.Numero_Documento,
                                        TipoDocumentoED = u.Fk_Tipo_Documento,
                                        NombreUsuarioED = u.Nombre_Usuario,
                                        FkEmpresaED = u.Fk_Id_Empresa,
                                        NitEmpresaED = u.nit_Empresa,
                                        ImagenFirmausuarioED = u.Imagen_Firma,
                                    }).FirstOrDefault();
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(RevisionManager), string.Format("Error al obtener la información  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
                return edperfil;
            }




        }


        public EDUsuario validarfirmaresponsable(int Idempresa)
        {
            //EDUsuario edperfil = null;
            var edperfil = new EDUsuario();
            //var edperfil = new EDUsuario();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {

                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        edperfil = (from u in context.Tbl_Usuario
                                    join r in context.Tbl_UsuarioRol on u.Pk_Id_Usuario equals r.Fk_Id_Usuario
                                    join b in context.Tbl_Rol on r.Fk_Id_Rol equals b.Pk_Id_Rol
                                    where b.Descripcion == "RESPONSABLE DE SGSST" && u.Fk_Id_Empresa == Idempresa
                                    select new EDUsuario()
                                    {
                                        PkUsuarioED = u.Pk_Id_Usuario,
                                        NumeroDocumentoED = u.Numero_Documento,
                                        TipoDocumentoED = u.Fk_Tipo_Documento,
                                        NombreUsuarioED = u.Nombre_Usuario,
                                        FkEmpresaED = u.Fk_Id_Empresa,
                                        NitEmpresaED = u.nit_Empresa,
                                        ImagenFirmausuarioED = u.Imagen_Firma,
                                    }).FirstOrDefault();
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(RevisionManager), string.Format("Error al obtener la información  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
                return edperfil;
            }




        }

        public bool CambiarEstadoRep(int idacta)
        {

            //var EditFres = null;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        var EditFrep = (from ar in context.Tbl_ActaRevision
                                        where ar.PK_Id_ActaRevision == idacta
                                        select ar).FirstOrDefault();
                        EditFrep.Firma_Representante_SGSST = !EditFrep.Firma_Responsable_SGSST;
                        var entry = context.Entry(EditFrep);
                        entry.State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        Transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(RevisionManager), string.Format("Error al obtener la información  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return false;
                    }
                }
                return true;
            }

        }

        public bool CambiarEstadoRes(int idacta)
        {

            //var EditFres = null;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {

                    RegistraLog registraLog = new RegistraLog();

                    try
                    {
                        var EditFres = (from ar in context.Tbl_ActaRevision
                                        where ar.PK_Id_ActaRevision == idacta
                                        select ar).FirstOrDefault();

                        EditFres.Firma_Responsable_SGSST = !EditFres.Firma_Responsable_SGSST;
                        var entry = context.Entry(EditFres);
                        entry.State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        Transaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(RevisionManager), string.Format("Error al obtener la información  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return false;

                    }
                }
                return true;

            }

        }

        public bool EliminarPlanRevision(int pkplan)
        {
            var result = false;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        var Planeliminar = (from cie in context.Tbl_PlanAccionRevision
                                            where cie.PK_Id_PlanAccion == pkplan
                                            select cie).SingleOrDefault();
                        if (Planeliminar != null)
                        {

                            context.Tbl_PlanAccionRevision.Remove(Planeliminar);
                        }
                        context.SaveChanges();
                        Transaction.Commit();
                        result = true;
                    }
                    catch (Exception ex)
                    {

                        registraLog.RegistrarError(typeof(RevisionManager), string.Format("Error al Eliminar el plan  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return result;
                    }
                }
            }

            return result;

        }
        /// <summary>
        ///  fin Metodos de persistencia Robinson 
        /// </summary>
        /// <param name="IdActa"></param>
        /// <returns></returns>


    }
}
