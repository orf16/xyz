using SG_SST.Models;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.Interfaces.MedicionEvaluacion;
using SG_SST.Models.MedicionEvaluacion;
using SG_SST.EntidadesDominio.MedicionEvaluacion;
using SG_SST.Models.Organizacion;
using SG_SST.Models.Empresas;
using SG_SST.Models.Empleado;
using System.IO;
using System.Data.Entity;
using System.Drawing;
using SG_SST.Interfaces.Empresas;
using SG_SST.Repositorio.Empresas;
using SG_SST.EntidadesDominio.Empresas;

namespace SG_SST.Repositorio.MedicionEvaluacion
{
    public class AuditoriaManager : IAuditoria
    {
        private static EmpresaManager em = new EmpresaManager();
        public bool GuardarPrograma(EDAuditoriaPrograma ProgramaAuditoria)
        {
            bool ProbarGuardar = false;
            AuditoriaPrograma AuditoriaPrograma = new AuditoriaPrograma();
            AuditoriaPrograma.Pk_Id_Programa = ProgramaAuditoria.Pk_Id_Programa;
            AuditoriaPrograma.Titulo = ProgramaAuditoria.Titulo;
            AuditoriaPrograma.Objetivo = ProgramaAuditoria.Objetivo;
            AuditoriaPrograma.Alcance = ProgramaAuditoria.Alcance;
            AuditoriaPrograma.Metodologia = ProgramaAuditoria.Metodologia;
            AuditoriaPrograma.Competencia = ProgramaAuditoria.Competencia;
            AuditoriaPrograma.Recursos = ProgramaAuditoria.Recursos;
            AuditoriaPrograma.Fecha_Programacion = ProgramaAuditoria.Fecha_Programacion;
            AuditoriaPrograma.Año = ProgramaAuditoria.Año;
            AuditoriaPrograma.Periodicidad = ProgramaAuditoria.Periodicidad;
            AuditoriaPrograma.NombreArchivoRes = ProgramaAuditoria.NombreArchivoRes;
            AuditoriaPrograma.RutaArchivoRes = ProgramaAuditoria.RutaArchivoRes;
            AuditoriaPrograma.Nombre_Responsable = ProgramaAuditoria.Nombre_Responsable;
            AuditoriaPrograma.Numero_Id_Responsable = ProgramaAuditoria.Numero_Id_Responsable;
            AuditoriaPrograma.FirmaScrImageRes = ProgramaAuditoria.FirmaScrImageRes;
            AuditoriaPrograma.NombreArchivoCopasst = ProgramaAuditoria.NombreArchivoCopasst;
            AuditoriaPrograma.RutaArchivoPres = ProgramaAuditoria.RutaArchivoPres;
            AuditoriaPrograma.Nombre_Copasst = ProgramaAuditoria.Nombre_Copasst;
            AuditoriaPrograma.Numero_Id_Copasst = ProgramaAuditoria.Numero_Id_Copasst;
            AuditoriaPrograma.FirmaScrImagePres = ProgramaAuditoria.FirmaScrImagePres;
            AuditoriaPrograma.Fk_Id_Empresa = ProgramaAuditoria.Fk_Id_Empresa;
            AuditoriaPrograma.SedeAuditoria = ProgramaAuditoria.SedeAuditoria;
            AuditoriaPrograma.Fk_Id_Sede = ProgramaAuditoria.Fk_Id_Sede;
            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    db.Tbl_AuditoriaPrograma.Add(AuditoriaPrograma);
                    db.SaveChanges();
                    ProbarGuardar = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.ToString();
                return ProbarGuardar;
            }
            return ProbarGuardar;
        }
        public bool CrearAuditoria(EDAuditoria EDAuditorias)
        {
            bool ProbarGuardar = false;
            Auditorias NuevaAuditoria = new Auditorias();
            NuevaAuditoria.Pk_Id_Auditoria = EDAuditorias.Pk_Id_Auditoria;
            NuevaAuditoria.Periodo = EDAuditorias.Periodo;
            NuevaAuditoria.Objetivo = EDAuditorias.Objetivo;
            NuevaAuditoria.Alcance = EDAuditorias.Alcance;
            NuevaAuditoria.Criterios = EDAuditorias.Criterios;
            NuevaAuditoria.FechaRealizacion = EDAuditorias.FechaRealizacion;
            NuevaAuditoria.Auditado = EDAuditorias.Auditado;
            NuevaAuditoria.Auditador = EDAuditorias.Auditador;
            NuevaAuditoria.Requisito = EDAuditorias.Requisito;
            NuevaAuditoria.Duracion = EDAuditorias.Duracion;
            NuevaAuditoria.Fk_Id_Programa = EDAuditorias.Fk_Id_Programa;
            NuevaAuditoria.Fk_Id_Proceso = EDAuditorias.Fk_Id_Proceso;
            
            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    db.Tbl_Auditorias.Add(NuevaAuditoria);
                    db.SaveChanges();
                    ProbarGuardar = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.ToString();
                return ProbarGuardar;
            }
            return ProbarGuardar;
        }
        public bool GuardarActividad(EDAuditoriaActividad EDAuditoriaActividad)
        {
            bool ProbarGuardar = false;
            AuditoriaCronograma NuevaActividad = new AuditoriaCronograma();
            NuevaActividad.Tema = EDAuditoriaActividad.Tema;
            NuevaActividad.Responsable = EDAuditoriaActividad.Responsable;
            NuevaActividad.Fecha_Hora = EDAuditoriaActividad.Fecha_Hora;
            NuevaActividad.TiempoEstimado = EDAuditoriaActividad.TiempoEstimado;
            NuevaActividad.Lugar = EDAuditoriaActividad.Lugar;
            NuevaActividad.Fk_Id_Auditoria = EDAuditoriaActividad.Fk_Id_Auditoria;
            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    db.Tbl_AuditoriaCronograma.Add(NuevaActividad);
                    db.SaveChanges();
                    ProbarGuardar = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.ToString();
                return ProbarGuardar;
            }
            return ProbarGuardar;
        }
        public bool GuardarActividadPlan(EDActividadAuditoria EDAuditoriaActividad)
        {
            bool ProbarGuardar = false;
            ActividadAuditoria NuevaActividad = new ActividadAuditoria();
            NuevaActividad.Responsable = EDAuditoriaActividad.Responsable;
            NuevaActividad.Actividad = EDAuditoriaActividad.Actividad;
            NuevaActividad.FechaFinalizacion = EDAuditoriaActividad.FechaFinalizacion;
            NuevaActividad.Fk_Id_Auditoria = EDAuditoriaActividad.Fk_Id_Auditoria;
            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    db.Tbl_ActividadAuditoria.Add(NuevaActividad);
                    db.SaveChanges();
                    ProbarGuardar = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.ToString();
                return ProbarGuardar;
            }
            return ProbarGuardar;
        }
        public bool GuardarListaVer(EDAuditoriaVerificacion EDAuditoriaVerificacion)
        {
            bool ProbarGuardar = false;
            AuditoriaListaVer NuevaAuditoriaListaVer = new AuditoriaListaVer();
            NuevaAuditoriaListaVer.Pregunta = EDAuditoriaVerificacion.Pregunta;
            NuevaAuditoriaListaVer.Requisito = EDAuditoriaVerificacion.Requisito;
            NuevaAuditoriaListaVer.Hallazgo = EDAuditoriaVerificacion.Hallazgo;
            NuevaAuditoriaListaVer.Tipo_Hallazgo = EDAuditoriaVerificacion.Tipo_Hallazgo;
            NuevaAuditoriaListaVer.Fk_Id_Auditoria = EDAuditoriaVerificacion.Fk_Id_Auditoria;
            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    db.Tbl_AuditoriaListaVer.Add(NuevaAuditoriaListaVer);
                    db.SaveChanges();
                    ProbarGuardar = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.ToString();
                return ProbarGuardar;
            }
            return ProbarGuardar;
        }
        public bool ActualizarPrograma(EDAuditoriaPrograma ProgramaAuditoria)
        {
            bool ProbarGuardar = false;
            AuditoriaPrograma AuditoriaPrograma = new AuditoriaPrograma();
            AuditoriaPrograma.Pk_Id_Programa = ProgramaAuditoria.Pk_Id_Programa;
            AuditoriaPrograma.Titulo = ProgramaAuditoria.Titulo;
            AuditoriaPrograma.Objetivo = ProgramaAuditoria.Objetivo;
            AuditoriaPrograma.Alcance = ProgramaAuditoria.Alcance;
            AuditoriaPrograma.Metodologia = ProgramaAuditoria.Metodologia;
            AuditoriaPrograma.Competencia = ProgramaAuditoria.Competencia;
            AuditoriaPrograma.Recursos = ProgramaAuditoria.Recursos;
            AuditoriaPrograma.Fecha_Programacion = ProgramaAuditoria.Fecha_Programacion;
            AuditoriaPrograma.Año = ProgramaAuditoria.Año;
            AuditoriaPrograma.Periodicidad = ProgramaAuditoria.Periodicidad;
            AuditoriaPrograma.NombreArchivoRes = ProgramaAuditoria.NombreArchivoRes;
            AuditoriaPrograma.RutaArchivoRes = ProgramaAuditoria.RutaArchivoRes;
            AuditoriaPrograma.Nombre_Responsable = ProgramaAuditoria.Nombre_Responsable;
            AuditoriaPrograma.Numero_Id_Responsable = ProgramaAuditoria.Numero_Id_Responsable;
            AuditoriaPrograma.FirmaScrImageRes = ProgramaAuditoria.FirmaScrImageRes;
            AuditoriaPrograma.NombreArchivoCopasst = ProgramaAuditoria.NombreArchivoCopasst;
            AuditoriaPrograma.RutaArchivoPres = ProgramaAuditoria.RutaArchivoPres;
            AuditoriaPrograma.Nombre_Copasst = ProgramaAuditoria.Nombre_Copasst;
            AuditoriaPrograma.Numero_Id_Copasst = ProgramaAuditoria.Numero_Id_Copasst;
            AuditoriaPrograma.FirmaScrImagePres = ProgramaAuditoria.FirmaScrImagePres;
            AuditoriaPrograma.Fk_Id_Empresa = ProgramaAuditoria.Fk_Id_Empresa;
            AuditoriaPrograma.SedeAuditoria = ProgramaAuditoria.SedeAuditoria;
            AuditoriaPrograma.Fk_Id_Sede = ProgramaAuditoria.Fk_Id_Sede;
            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    db.Entry(AuditoriaPrograma).State = EntityState.Modified;
                    db.SaveChanges();
                    ProbarGuardar = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.ToString();
                return ProbarGuardar;
            }
            return ProbarGuardar;
        }
        public bool ActualizarAuditoria(EDAuditoria EDAuditorias)
        {
            Auditorias Auditorias = new Auditorias();

            Auditorias.Pk_Id_Auditoria = EDAuditorias.Pk_Id_Auditoria;
            Auditorias.Periodo = EDAuditorias.Periodo;
            Auditorias.Objetivo = EDAuditorias.Objetivo;
            Auditorias.Alcance = EDAuditorias.Alcance;
            Auditorias.Criterios = EDAuditorias.Criterios;
            Auditorias.FechaRealizacion = EDAuditorias.FechaRealizacion;
            Auditorias.Auditado = EDAuditorias.Auditado;
            Auditorias.Auditador = EDAuditorias.Auditador;
            Auditorias.Requisito = EDAuditorias.Requisito;
            Auditorias.Duracion = EDAuditorias.Duracion;
            Auditorias.Fk_Id_Programa = EDAuditorias.Fk_Id_Programa;
            Auditorias.Fk_Id_Proceso = EDAuditorias.Fk_Id_Proceso;


            bool ProbarGuardar = false;
            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    db.Entry(Auditorias).State = EntityState.Modified;
                    db.SaveChanges();
                    ProbarGuardar = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.ToString();
                return ProbarGuardar;
            }



            return ProbarGuardar;




        }
        public bool ActualizarActividad(EDAuditoriaActividad EDAuditoriaActividad)
        {
            AuditoriaCronograma AuditoriaCronograma = new AuditoriaCronograma();

            AuditoriaCronograma.Pk_Id_Cronograma_Auditoria = EDAuditoriaActividad.Pk_Id_Cronograma_Auditoria;
            AuditoriaCronograma.Tema = EDAuditoriaActividad.Tema;
            AuditoriaCronograma.Responsable = EDAuditoriaActividad.Responsable;
            AuditoriaCronograma.Fecha_Hora = EDAuditoriaActividad.Fecha_Hora;
            AuditoriaCronograma.TiempoEstimado = EDAuditoriaActividad.TiempoEstimado;
            AuditoriaCronograma.Lugar = EDAuditoriaActividad.Lugar;
            AuditoriaCronograma.Fk_Id_Auditoria = EDAuditoriaActividad.Fk_Id_Auditoria;



            bool ProbarGuardar = false;
            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    db.Entry(AuditoriaCronograma).State = EntityState.Modified;
                    db.SaveChanges();
                    ProbarGuardar = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.ToString();
                return ProbarGuardar;
            }



            return ProbarGuardar;



        }
        public bool ActualizarActividadPlan(EDActividadAuditoria EDAuditoriaActividad)
        {
            ActividadAuditoria AuditoriaActividad = new ActividadAuditoria();
            AuditoriaActividad.Pk_Id_Actividad = EDAuditoriaActividad.Pk_Id_Actividad;
            AuditoriaActividad.Actividad = EDAuditoriaActividad.Actividad;
            AuditoriaActividad.Responsable = EDAuditoriaActividad.Responsable;
            AuditoriaActividad.FechaFinalizacion = EDAuditoriaActividad.FechaFinalizacion;
            AuditoriaActividad.Fk_Id_Auditoria = EDAuditoriaActividad.Fk_Id_Auditoria;
            bool ProbarGuardar = false;
            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    db.Entry(AuditoriaActividad).State = EntityState.Modified;
                    db.SaveChanges();
                    ProbarGuardar = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.ToString();
                return ProbarGuardar;
            }
            return ProbarGuardar;
        }
        public bool ActualizarListaVer(EDAuditoriaVerificacion EDAuditoriaVerificacion)
        {
            bool ProbarGuardar = false;
            AuditoriaListaVer AuditoriaListaVer = new AuditoriaListaVer();
            AuditoriaListaVer.Pk_Id_Lista_Verificacion = EDAuditoriaVerificacion.Pk_Id_Lista_Verificacion;
            AuditoriaListaVer.Pregunta = EDAuditoriaVerificacion.Pregunta;
            AuditoriaListaVer.Requisito = EDAuditoriaVerificacion.Requisito;
            AuditoriaListaVer.Hallazgo = EDAuditoriaVerificacion.Hallazgo;
            AuditoriaListaVer.Tipo_Hallazgo = EDAuditoriaVerificacion.Tipo_Hallazgo;
            AuditoriaListaVer.Fk_Id_Auditoria = EDAuditoriaVerificacion.Fk_Id_Auditoria;

            AuditoriaListaVer.Compromiso = EDAuditoriaVerificacion.Compromiso;
            AuditoriaListaVer.FechaCierre = EDAuditoriaVerificacion.FechaCierre;
            AuditoriaListaVer.Responsable = EDAuditoriaVerificacion.Responsable;
            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    db.Entry(AuditoriaListaVer).State = EntityState.Modified;
                    db.SaveChanges();
                    ProbarGuardar = true;
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.ToString();
                return ProbarGuardar;
            }
            return ProbarGuardar;
        }
        public List<EDAuditoriaPrograma> ConsultaTodosProgramas(int Pkempresa)
        {
            List<EDAuditoriaPrograma> ListaConsulta = new List<EDAuditoriaPrograma>();
            List<AuditoriaPrograma> ListaConsultaBD = new List<AuditoriaPrograma>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_AuditoriaPrograma
                                where s.Fk_Id_Empresa == Pkempresa
                                select s).ToList<AuditoriaPrograma>();
                if (Listavar != null)
                {
                    ListaConsultaBD = Listavar;
                    foreach (var item in ListaConsultaBD)
                    {
                        EDAuditoriaPrograma EDAuditoriaPrograma = new EDAuditoriaPrograma();
                        EDAuditoriaPrograma.Pk_Id_Programa = item.Pk_Id_Programa;
                        EDAuditoriaPrograma.Titulo = item.Titulo;
                        EDAuditoriaPrograma.Objetivo = item.Objetivo;
                        EDAuditoriaPrograma.Alcance = item.Alcance;
                        EDAuditoriaPrograma.Metodologia = item.Metodologia;
                        EDAuditoriaPrograma.Competencia = item.Competencia;
                        EDAuditoriaPrograma.Recursos = item.Recursos;
                        EDAuditoriaPrograma.Fecha_Programacion = item.Fecha_Programacion;
                        EDAuditoriaPrograma.Año = item.Año;
                        EDAuditoriaPrograma.Periodicidad = item.Periodicidad;
                        EDAuditoriaPrograma.NombreArchivoRes = item.NombreArchivoRes;
                        EDAuditoriaPrograma.RutaArchivoRes = item.RutaArchivoRes;
                        EDAuditoriaPrograma.Nombre_Responsable = item.Nombre_Responsable;
                        EDAuditoriaPrograma.Numero_Id_Responsable = item.Numero_Id_Responsable;
                        EDAuditoriaPrograma.FirmaScrImageRes = item.FirmaScrImageRes;
                        EDAuditoriaPrograma.NombreArchivoCopasst = item.NombreArchivoCopasst;
                        EDAuditoriaPrograma.RutaArchivoPres = item.RutaArchivoPres;
                        EDAuditoriaPrograma.Nombre_Copasst = item.Nombre_Copasst;
                        EDAuditoriaPrograma.Numero_Id_Copasst = item.Numero_Id_Copasst;
                        EDAuditoriaPrograma.FirmaScrImagePres = item.FirmaScrImagePres;
                        EDAuditoriaPrograma.Fk_Id_Empresa = item.Fk_Id_Empresa;
                        EDAuditoriaPrograma.SedeAuditoria = item.SedeAuditoria;
                        EDAuditoriaPrograma.Fk_Id_Sede = item.Fk_Id_Sede;
                        ListaConsulta.Add(EDAuditoriaPrograma);
                    }
                }

            }
            return ListaConsulta;
        }
        public List<EDAuditoria> ConsultaAuditorias(int IdPrograma, string NitEmpresa)
        {

            List<EDAuditoria> NuevaLista = new List<EDAuditoria>();
            List<Auditorias> ListAuditoria = new List<Auditorias>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_Auditorias
                                where s.Fk_Id_Programa == IdPrograma
                                select s).ToList<Auditorias>();

                if (Listavar != null)
                {
                    ListAuditoria = Listavar;
                }

            }
            foreach (var item in ListAuditoria)
            {
                EDAuditoria ElementoAud = new EDAuditoria();
                int IdProceso = item.Fk_Id_Proceso;

                List<EDProceso> ProcesoLista = em.ObtenerProcesosPorEmpres(NitEmpresa);
                EDProceso EDProceso = new EDProceso();
                EDProceso = ProcesoLista.Find(s => s.Id_Proceso == IdProceso);

                ElementoAud.Pk_Id_Auditoria = item.Pk_Id_Auditoria;
                ElementoAud.Periodo = item.Periodo;
                ElementoAud.Objetivo = item.Objetivo;
                ElementoAud.Alcance = item.Alcance;
                ElementoAud.Criterios = item.Criterios;
                ElementoAud.FechaRealizacion = item.FechaRealizacion;
                ElementoAud.Auditado = item.Auditado;
                ElementoAud.Auditador = item.Auditador;
                ElementoAud.Requisito = item.Requisito;
                ElementoAud.Duracion = item.Duracion;
                ElementoAud.Fk_Id_Programa = item.Fk_Id_Programa;
                ElementoAud.NombreProceso1 = EDProceso.Descripcion;
                ElementoAud.Fk_Id_Proceso = item.Fk_Id_Proceso;
                NuevaLista.Add(ElementoAud);
            }


            return NuevaLista;
        }
        public List<EDActividadAuditoria> ConsultaActividadesPlan(int IdAuditoria)
        {

            List<EDActividadAuditoria> NuevaLista = new List<EDActividadAuditoria>();
            List<ActividadAuditoria> ListaConsulta = new List<ActividadAuditoria>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Listavar = (from s in db.Tbl_ActividadAuditoria
                                where s.Fk_Id_Auditoria == IdAuditoria
                                select s).ToList<ActividadAuditoria>();

                if (Listavar != null)
                {
                    ListaConsulta = Listavar;
                }

            }
            foreach (var item in ListaConsulta)
            {
                EDActividadAuditoria ElementoAud = new EDActividadAuditoria();
                ElementoAud.Pk_Id_Actividad = item.Pk_Id_Actividad;
                ElementoAud.Actividad = item.Actividad;
                ElementoAud.Responsable = item.Responsable;
                ElementoAud.FechaFinalizacion = item.FechaFinalizacion;
                ElementoAud.Fk_Id_Auditoria = item.Fk_Id_Auditoria;
                NuevaLista.Add(ElementoAud);
            }


            return NuevaLista;
        }
        public EDAuditoriaPrograma Consultaprograma(int IdPrograma)
        {
            AuditoriaPrograma AuditoriaPrograma = new AuditoriaPrograma();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Prog = (from s in db.Tbl_AuditoriaPrograma
                            where s.Pk_Id_Programa == IdPrograma
                            select s).FirstOrDefault<AuditoriaPrograma>();
                if (Prog != null)
                {
                    AuditoriaPrograma = Prog;
                }
            }

            EDAuditoriaPrograma EDProgramaConsulta = new EDAuditoriaPrograma();
            EDProgramaConsulta.Pk_Id_Programa = AuditoriaPrograma.Pk_Id_Programa;
            EDProgramaConsulta.Titulo = AuditoriaPrograma.Titulo;
            EDProgramaConsulta.Objetivo = AuditoriaPrograma.Objetivo;
            EDProgramaConsulta.Alcance = AuditoriaPrograma.Alcance;
            EDProgramaConsulta.Metodologia = AuditoriaPrograma.Metodologia;
            EDProgramaConsulta.Competencia = AuditoriaPrograma.Competencia;
            EDProgramaConsulta.Recursos = AuditoriaPrograma.Recursos;
            EDProgramaConsulta.Fecha_Programacion = AuditoriaPrograma.Fecha_Programacion;
            EDProgramaConsulta.Año = AuditoriaPrograma.Año;
            EDProgramaConsulta.Periodicidad = AuditoriaPrograma.Periodicidad;
            EDProgramaConsulta.NombreArchivoRes = AuditoriaPrograma.NombreArchivoRes;
            EDProgramaConsulta.RutaArchivoRes = AuditoriaPrograma.RutaArchivoRes;
            EDProgramaConsulta.Nombre_Responsable = AuditoriaPrograma.Nombre_Responsable;
            EDProgramaConsulta.Numero_Id_Responsable = AuditoriaPrograma.Numero_Id_Responsable;
            EDProgramaConsulta.FirmaScrImageRes = AuditoriaPrograma.FirmaScrImageRes;
            EDProgramaConsulta.NombreArchivoCopasst = AuditoriaPrograma.NombreArchivoCopasst;
            EDProgramaConsulta.RutaArchivoPres = AuditoriaPrograma.RutaArchivoPres;
            EDProgramaConsulta.Nombre_Copasst = AuditoriaPrograma.Nombre_Copasst;
            EDProgramaConsulta.Numero_Id_Copasst = AuditoriaPrograma.Numero_Id_Copasst;
            EDProgramaConsulta.FirmaScrImagePres = AuditoriaPrograma.FirmaScrImagePres;
            EDProgramaConsulta.Fk_Id_Empresa = AuditoriaPrograma.Fk_Id_Empresa;
            EDProgramaConsulta.SedeAuditoria = AuditoriaPrograma.SedeAuditoria;
            EDProgramaConsulta.Fk_Id_Sede = AuditoriaPrograma.Fk_Id_Sede;




            return EDProgramaConsulta;


        }
        public EDAuditoriaPrograma ConsultaprogramaEmpresa(int IdPrograma, int Idempresa)
        {
            AuditoriaPrograma AuditoriaPrograma = new AuditoriaPrograma();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Prog = (from s in db.Tbl_AuditoriaPrograma
                            where s.Pk_Id_Programa == IdPrograma && s.Fk_Id_Empresa== Idempresa
                            select s).FirstOrDefault<AuditoriaPrograma>();
                if (Prog != null)
                {
                    AuditoriaPrograma = Prog;
                }
            }

            EDAuditoriaPrograma EDProgramaConsulta = new EDAuditoriaPrograma();
            EDProgramaConsulta.Pk_Id_Programa = AuditoriaPrograma.Pk_Id_Programa;
            EDProgramaConsulta.Titulo = AuditoriaPrograma.Titulo;
            EDProgramaConsulta.Objetivo = AuditoriaPrograma.Objetivo;
            EDProgramaConsulta.Alcance = AuditoriaPrograma.Alcance;
            EDProgramaConsulta.Metodologia = AuditoriaPrograma.Metodologia;
            EDProgramaConsulta.Competencia = AuditoriaPrograma.Competencia;
            EDProgramaConsulta.Recursos = AuditoriaPrograma.Recursos;
            EDProgramaConsulta.Fecha_Programacion = AuditoriaPrograma.Fecha_Programacion;
            EDProgramaConsulta.Año = AuditoriaPrograma.Año;
            EDProgramaConsulta.Periodicidad = AuditoriaPrograma.Periodicidad;
            EDProgramaConsulta.NombreArchivoRes = AuditoriaPrograma.NombreArchivoRes;
            EDProgramaConsulta.RutaArchivoRes = AuditoriaPrograma.RutaArchivoRes;
            EDProgramaConsulta.Nombre_Responsable = AuditoriaPrograma.Nombre_Responsable;
            EDProgramaConsulta.Numero_Id_Responsable = AuditoriaPrograma.Numero_Id_Responsable;
            EDProgramaConsulta.FirmaScrImageRes = AuditoriaPrograma.FirmaScrImageRes;
            EDProgramaConsulta.NombreArchivoCopasst = AuditoriaPrograma.NombreArchivoCopasst;
            EDProgramaConsulta.RutaArchivoPres = AuditoriaPrograma.RutaArchivoPres;
            EDProgramaConsulta.Nombre_Copasst = AuditoriaPrograma.Nombre_Copasst;
            EDProgramaConsulta.Numero_Id_Copasst = AuditoriaPrograma.Numero_Id_Copasst;
            EDProgramaConsulta.FirmaScrImagePres = AuditoriaPrograma.FirmaScrImagePres;
            EDProgramaConsulta.Fk_Id_Empresa = AuditoriaPrograma.Fk_Id_Empresa;
            EDProgramaConsulta.SedeAuditoria = AuditoriaPrograma.SedeAuditoria;
            EDProgramaConsulta.Fk_Id_Sede = AuditoriaPrograma.Fk_Id_Sede;




            return EDProgramaConsulta;


        }
        public EDAuditoriaInforme ConsultaConclusionesInforme(int IdAuditoria)
        {
            AuditoriaInforme AuditoriaInforme = new AuditoriaInforme();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var conclu = (from s in db.Tbl_AuditoriaInforme
                            where s.Pk_Id_Auditoria == IdAuditoria
                            select s).FirstOrDefault<AuditoriaInforme>();
                if (conclu != null)
                {
                    AuditoriaInforme = conclu;
                }
            }

            EDAuditoriaInforme EDAuditoriaInforme = new EDAuditoriaInforme();
            EDAuditoriaInforme.Pk_Id_Auditoria = AuditoriaInforme.Pk_Id_Auditoria;
            EDAuditoriaInforme.Conclusiones = AuditoriaInforme.Conclusiones;
            EDAuditoriaInforme.NombreArchivoAuditor = AuditoriaInforme.NombreArchivoAuditor;
            EDAuditoriaInforme.NombreArchivoRes = AuditoriaInforme.NombreArchivoRes;
            EDAuditoriaInforme.RutaArchivoAuditor = AuditoriaInforme.RutaArchivoAuditor;
            EDAuditoriaInforme.RutaArchivoRes = AuditoriaInforme.RutaArchivoRes;

            return EDAuditoriaInforme;


        }
        public EDAuditoria ConsultaAuditoria(int IdAuditoria, string NitEmpresa)
        {
            Auditorias Auditorias = new Auditorias();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Aud = (from s in db.Tbl_Auditorias
                           where s.Pk_Id_Auditoria == IdAuditoria
                           select s).FirstOrDefault<Auditorias>();
                if (Aud != null)
                {
                    Auditorias = Aud;
                }
            }

            EDAuditoria EDAuditoriaConsulta = new EDAuditoria();
            int IdProceso = Auditorias.Fk_Id_Proceso;
            List<EDProceso> ProcesoLista = em.ObtenerProcesosPorEmpres(NitEmpresa);
            EDProceso EDProceso = new EDProceso();
            EDProceso = ProcesoLista.Find(s => s.Id_Proceso == IdProceso);
            EDAuditoriaConsulta.Pk_Id_Auditoria = Auditorias.Pk_Id_Auditoria;
            EDAuditoriaConsulta.Periodo = Auditorias.Periodo;
            EDAuditoriaConsulta.Objetivo = Auditorias.Objetivo;
            EDAuditoriaConsulta.Alcance = Auditorias.Alcance;
            EDAuditoriaConsulta.Criterios = Auditorias.Criterios;
            EDAuditoriaConsulta.FechaRealizacion = Auditorias.FechaRealizacion;
            EDAuditoriaConsulta.Auditado = Auditorias.Auditado;
            EDAuditoriaConsulta.Auditador = Auditorias.Auditador;
            EDAuditoriaConsulta.Requisito = Auditorias.Requisito;
            EDAuditoriaConsulta.Duracion = Auditorias.Duracion;
            EDAuditoriaConsulta.Fk_Id_Programa = Auditorias.Fk_Id_Programa;
            EDAuditoriaConsulta.Fk_Id_Proceso = Auditorias.Fk_Id_Proceso;
            EDAuditoriaConsulta.NombreProceso1 = EDProceso.Descripcion;

            return EDAuditoriaConsulta;

        }
        public EDAuditoriaActividad ConsultaAuditoriaActividades(int IdAuditoria)
        {
            List<EDAuditoriaActividad> ListaEDActividad = new List<EDAuditoriaActividad>();
            List<AuditoriaCronograma> ListaActividad = new List<AuditoriaCronograma>();
            EDAuditoriaActividad EDAuditoriaConsulta = new EDAuditoriaActividad();

            Auditorias Auditorias = new Auditorias();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Aud = (from s in db.Tbl_AuditoriaCronograma
                           where s.Fk_Id_Auditoria == IdAuditoria
                           select s).ToList<AuditoriaCronograma>();

                if (Aud != null)
                {
                    ListaActividad = ListaActividad.OrderBy(x => x.Fecha_Hora).ToList();
                    ListaActividad = Aud;
                }
            }
            foreach (var item in ListaActividad)
            {
                EDAuditoriaActividad EDAuditoriaNuevo = new EDAuditoriaActividad();
                EDAuditoriaNuevo.Pk_Id_Cronograma_Auditoria = item.Pk_Id_Cronograma_Auditoria;
                EDAuditoriaNuevo.Tema = item.Tema;
                EDAuditoriaNuevo.Responsable = item.Responsable;
                EDAuditoriaNuevo.Fecha_Hora = item.Fecha_Hora;
                EDAuditoriaNuevo.TiempoEstimado = item.TiempoEstimado;
                EDAuditoriaNuevo.Lugar = item.Lugar;
                EDAuditoriaNuevo.Fk_Id_Auditoria = item.Fk_Id_Auditoria;

                ListaEDActividad.Add(EDAuditoriaNuevo);
            }

            EDAuditoriaConsulta.ListaActividad = ListaEDActividad;

            return EDAuditoriaConsulta;


        }
        public EDAuditoriaActividad ConsultaInforme(int IdAuditoria)
        {
            List<EDAuditoriaActividad> ListaEDActividad = new List<EDAuditoriaActividad>();
            List<AuditoriaCronograma> ListaActividad = new List<AuditoriaCronograma>();
            EDAuditoriaActividad EDAuditoriaConsulta = new EDAuditoriaActividad();

            Auditorias Auditorias = new Auditorias();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Aud = (from s in db.Tbl_AuditoriaCronograma
                           where s.Fk_Id_Auditoria == IdAuditoria
                           select s).ToList<AuditoriaCronograma>();

                if (Aud != null)
                {
                    ListaActividad = ListaActividad.OrderBy(x => x.Fecha_Hora).ToList();
                    ListaActividad = Aud;
                }
            }
            foreach (var item in ListaActividad)
            {
                EDAuditoriaActividad EDAuditoriaNuevo = new EDAuditoriaActividad();
                EDAuditoriaNuevo.Pk_Id_Cronograma_Auditoria = item.Pk_Id_Cronograma_Auditoria;
                EDAuditoriaNuevo.Tema = item.Tema;
                EDAuditoriaNuevo.Responsable = item.Responsable;
                EDAuditoriaNuevo.Fecha_Hora = item.Fecha_Hora;
                EDAuditoriaNuevo.TiempoEstimado = item.TiempoEstimado;
                EDAuditoriaNuevo.Lugar = item.Lugar;
                EDAuditoriaNuevo.Fk_Id_Auditoria = item.Fk_Id_Auditoria;

                ListaEDActividad.Add(EDAuditoriaNuevo);
            }

            EDAuditoriaConsulta.ListaActividad = ListaEDActividad;

            return EDAuditoriaConsulta;


        }
        public EDAuditoriaActividad ConsultaAuditoriaActividad(int IdActividad)
        {
            EDAuditoriaActividad EDAuditoriaConsulta = new EDAuditoriaActividad();
            AuditoriaCronograma AuditoriaCronograma = new AuditoriaCronograma();

            Auditorias Auditorias = new Auditorias();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Aud = (from s in db.Tbl_AuditoriaCronograma
                           where s.Pk_Id_Cronograma_Auditoria == IdActividad
                           select s).FirstOrDefault<AuditoriaCronograma>();

                if (Aud != null)
                {
                    AuditoriaCronograma = Aud;
                    EDAuditoriaConsulta.Pk_Id_Cronograma_Auditoria = AuditoriaCronograma.Pk_Id_Cronograma_Auditoria;
                    EDAuditoriaConsulta.Tema = AuditoriaCronograma.Tema;
                    EDAuditoriaConsulta.Responsable = AuditoriaCronograma.Responsable;
                    EDAuditoriaConsulta.Fecha_Hora = AuditoriaCronograma.Fecha_Hora;
                    EDAuditoriaConsulta.TiempoEstimado = AuditoriaCronograma.TiempoEstimado;
                    EDAuditoriaConsulta.Lugar = AuditoriaCronograma.Lugar;
                    EDAuditoriaConsulta.Fk_Id_Auditoria = AuditoriaCronograma.Fk_Id_Auditoria;
                }
                else
                {
                    EDAuditoriaConsulta = null;
                }
            }
            return EDAuditoriaConsulta;
        }
        public EDActividadAuditoria ConsultaAuditoriaActividadPlan(int IdActividad)
        {
            EDActividadAuditoria EDAuditoriaConsulta = new EDActividadAuditoria();
            ActividadAuditoria AuditoriaCronograma = new ActividadAuditoria();

            Auditorias Auditorias = new Auditorias();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Aud = (from s in db.Tbl_ActividadAuditoria
                           where s.Pk_Id_Actividad == IdActividad
                           select s).FirstOrDefault<ActividadAuditoria>();
                if (Aud != null)
                {
                    AuditoriaCronograma = Aud;
                    EDAuditoriaConsulta.Pk_Id_Actividad = AuditoriaCronograma.Pk_Id_Actividad;
                    EDAuditoriaConsulta.Actividad = AuditoriaCronograma.Actividad;
                    EDAuditoriaConsulta.Responsable = AuditoriaCronograma.Responsable;
                    EDAuditoriaConsulta.FechaFinalizacion = AuditoriaCronograma.FechaFinalizacion;
                    EDAuditoriaConsulta.Fk_Id_Auditoria = AuditoriaCronograma.Fk_Id_Auditoria;
                }
                else
                {
                    EDAuditoriaConsulta = null;
                }
            }
            return EDAuditoriaConsulta;
        }
        public EDAuditoriaVerificacion ConsultaListavers(int IdAuditoria)
        {
            List<EDAuditoriaVerificacion> ListaEDAuditoriaVerificacion = new List<EDAuditoriaVerificacion>();
            List<AuditoriaListaVer> ListaListaVer = new List<AuditoriaListaVer>();
            EDAuditoriaVerificacion EDAuditoriaVerificacionConsulta = new EDAuditoriaVerificacion();

            Auditorias Auditorias = new Auditorias();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Aud = (from s in db.Tbl_AuditoriaListaVer
                           where s.Fk_Id_Auditoria == IdAuditoria
                           select s).ToList<AuditoriaListaVer>();

                if (Aud != null)
                {
                    ListaListaVer = Aud;
                }
            }
            foreach (var item in ListaListaVer)
            {
                EDAuditoriaVerificacion EDAuditoriaVerificacionNuevo = new EDAuditoriaVerificacion();
                EDAuditoriaVerificacionNuevo.Pk_Id_Lista_Verificacion = item.Pk_Id_Lista_Verificacion;
                EDAuditoriaVerificacionNuevo.Pregunta = item.Pregunta;
                EDAuditoriaVerificacionNuevo.Requisito = item.Requisito;
                EDAuditoriaVerificacionNuevo.Hallazgo = item.Hallazgo;
                EDAuditoriaVerificacionNuevo.Tipo_Hallazgo = item.Tipo_Hallazgo;
                EDAuditoriaVerificacionNuevo.Fk_Id_Auditoria = item.Fk_Id_Auditoria;


                ListaEDAuditoriaVerificacion.Add(EDAuditoriaVerificacionNuevo);
            }

            EDAuditoriaVerificacionConsulta.ListaVerficiacionLista = ListaEDAuditoriaVerificacion;

            return EDAuditoriaVerificacionConsulta;
        }
        public EDAuditoriaVerificacion ConsultaCuadroCompromiso(int IdAuditoria)
        {
            List<EDAuditoriaVerificacion> ListaEDAuditoriaVerificacion = new List<EDAuditoriaVerificacion>();
            List<AuditoriaListaVer> ListaListaVer = new List<AuditoriaListaVer>();
            EDAuditoriaVerificacion EDAuditoriaVerificacionConsulta = new EDAuditoriaVerificacion();

            Auditorias Auditorias = new Auditorias();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Aud = (from s in db.Tbl_AuditoriaListaVer
                           where s.Fk_Id_Auditoria == IdAuditoria && s.Tipo_Hallazgo== "No Conformidad"
                           select s).ToList<AuditoriaListaVer>();

                if (Aud != null)
                {
                    ListaListaVer = Aud;
                }
            }
            foreach (var item in ListaListaVer)
            {
                EDAuditoriaVerificacion EDAuditoriaVerificacionNuevo = new EDAuditoriaVerificacion();
                EDAuditoriaVerificacionNuevo.Pk_Id_Lista_Verificacion = item.Pk_Id_Lista_Verificacion;
                EDAuditoriaVerificacionNuevo.Pregunta = item.Pregunta;
                EDAuditoriaVerificacionNuevo.Requisito = item.Requisito;
                EDAuditoriaVerificacionNuevo.Hallazgo = item.Hallazgo;
                EDAuditoriaVerificacionNuevo.Tipo_Hallazgo = item.Tipo_Hallazgo;
                EDAuditoriaVerificacionNuevo.Fk_Id_Auditoria = item.Fk_Id_Auditoria;

                EDAuditoriaVerificacionNuevo.Compromiso = item.Compromiso;
                EDAuditoriaVerificacionNuevo.FechaCierre = item.FechaCierre ?? DateTime.Now; 
                EDAuditoriaVerificacionNuevo.Responsable = item.Responsable;


                ListaEDAuditoriaVerificacion.Add(EDAuditoriaVerificacionNuevo);
            }

            EDAuditoriaVerificacionConsulta.ListaVerficiacionLista = ListaEDAuditoriaVerificacion;

            return EDAuditoriaVerificacionConsulta;
        }
        public EDAuditoriaVerificacion ConsultaListaver(int IdListaVer)
        {
            EDAuditoriaVerificacion EDAuditoriaVerificacionConsulta = new EDAuditoriaVerificacion();
            AuditoriaListaVer AuditoriaListaVer = new AuditoriaListaVer();

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Aud = (from s in db.Tbl_AuditoriaListaVer
                           where s.Pk_Id_Lista_Verificacion == IdListaVer
                           select s).FirstOrDefault<AuditoriaListaVer>();

                if (Aud != null)
                {
                    AuditoriaListaVer = Aud;
                    EDAuditoriaVerificacionConsulta.Pk_Id_Lista_Verificacion = AuditoriaListaVer.Pk_Id_Lista_Verificacion;
                    EDAuditoriaVerificacionConsulta.Pregunta = AuditoriaListaVer.Pregunta;
                    EDAuditoriaVerificacionConsulta.Requisito = AuditoriaListaVer.Requisito;
                    EDAuditoriaVerificacionConsulta.Hallazgo = AuditoriaListaVer.Hallazgo;
                    EDAuditoriaVerificacionConsulta.Tipo_Hallazgo = AuditoriaListaVer.Tipo_Hallazgo;
                    EDAuditoriaVerificacionConsulta.Fk_Id_Auditoria = AuditoriaListaVer.Fk_Id_Auditoria;

                    EDAuditoriaVerificacionConsulta.Compromiso = AuditoriaListaVer.Compromiso;
                    EDAuditoriaVerificacionConsulta.FechaCierre = AuditoriaListaVer.FechaCierre ?? DateTime.Today;
                    EDAuditoriaVerificacionConsulta.Responsable = AuditoriaListaVer.Responsable;
                }
                else
                {
                    EDAuditoriaVerificacionConsulta = null;
                }
            }
            return EDAuditoriaVerificacionConsulta;

        }
        public bool EliminarActividad(EDAuditoriaActividad EDAuditoriaActividad)
        {
            bool ProbarEliminar = false;
            int IdActividad = EDAuditoriaActividad.Pk_Id_Cronograma_Auditoria;
            AuditoriaCronograma AuditoriaCronograma = new AuditoriaCronograma();

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Aud = (from s in db.Tbl_AuditoriaCronograma
                           where s.Pk_Id_Cronograma_Auditoria == IdActividad
                           select s).FirstOrDefault<AuditoriaCronograma>();

                if (Aud != null)
                {
                    AuditoriaCronograma = Aud;
                }
                else
                {
                    return ProbarEliminar;
                }
            }

            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    db.Entry(AuditoriaCronograma).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    ProbarEliminar = true;
                }
            }
            catch (Exception)
            {
                return ProbarEliminar;
            }





            return ProbarEliminar;
        }
        public bool EliminarListaVer(EDAuditoriaVerificacion EDAuditoriaVerificacion)
        {
            bool ProbarEliminar = false;
            int IdLista = EDAuditoriaVerificacion.Pk_Id_Lista_Verificacion;
            AuditoriaListaVer AuditoriaListaVer = new AuditoriaListaVer();

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Aud = (from s in db.Tbl_AuditoriaListaVer
                           where s.Pk_Id_Lista_Verificacion == IdLista
                           select s).FirstOrDefault<AuditoriaListaVer>();

                if (Aud != null)
                {
                    AuditoriaListaVer = Aud;
                }
                else
                {
                    return ProbarEliminar;
                }
            }
            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    db.Entry(AuditoriaListaVer).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    ProbarEliminar = true;
                }
            }
            catch (Exception)
            {
                return ProbarEliminar;
            }
            return ProbarEliminar;
        }
        public EDAuditoriaInforme ConsultaInforme(int IdAuditoria, string NitEmpresa)
        {
            EDAuditoriaInforme EDAuditoriaInformeConsulta = new EDAuditoriaInforme();
            EDAuditoriaInforme Conclusiones = new EDAuditoriaInforme();
            EDAuditoria ConsultaAuditoria_informe = ConsultaAuditoria(IdAuditoria, NitEmpresa);
            int IdPrograma = ConsultaAuditoria_informe.Fk_Id_Programa;
            EDAuditoriaPrograma ConsultaPrograma_informe = Consultaprograma(IdPrograma);
            EDAuditoriaVerificacion ConsultaListVer_Informe = ConsultaListavers(IdAuditoria);
            EDAuditoriaVerificacion ListaCuadroCompromiso = ConsultaCuadroCompromiso(IdAuditoria);
            List<EDActividadAuditoria> ListaActividades = ConsultaActividadesPlan(IdAuditoria);
            Conclusiones = ConsultaConclusionesInforme(IdAuditoria);
            EDAuditoriaInformeConsulta.Informe_EDAuditoria = ConsultaAuditoria_informe;
            EDAuditoriaInformeConsulta.Informe_EDAuditoriaPrograma = ConsultaPrograma_informe;
            EDAuditoriaInformeConsulta.ListaVerficiacionInforme = ConsultaListVer_Informe.ListaVerficiacionLista;
            EDAuditoriaInformeConsulta.ListaCuadroCompromiso = ListaCuadroCompromiso.ListaVerficiacionLista;
            EDAuditoriaInformeConsulta.ListaActividadesInforme = ListaActividades;
            EDAuditoriaInformeConsulta.Conclusiones = Conclusiones.Conclusiones;
            EDAuditoriaInformeConsulta.NombreArchivoAuditor = Conclusiones.NombreArchivoAuditor;
            EDAuditoriaInformeConsulta.NombreArchivoRes = Conclusiones.NombreArchivoRes;
            EDAuditoriaInformeConsulta.RutaArchivoAuditor = Conclusiones.RutaArchivoAuditor;
            EDAuditoriaInformeConsulta.RutaArchivoRes = Conclusiones.RutaArchivoRes;
            return EDAuditoriaInformeConsulta;
        }
        public bool EliminarPrograma(int IdPrograma, int IdEmpresa)
        {
            bool ProbarEliminar = false;
            int IdProgramaProg = 0;
            AuditoriaPrograma AuditoriaPrograma = new AuditoriaPrograma();
            List<Auditorias> ListaAuditorias = new List<Auditorias>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var prog = (from s in db.Tbl_AuditoriaPrograma
                           where s.Pk_Id_Programa == IdPrograma && s.Fk_Id_Empresa== IdEmpresa
                            select s).FirstOrDefault<AuditoriaPrograma>();

                if (prog != null)
                {
                    AuditoriaPrograma = prog;
                    IdProgramaProg = AuditoriaPrograma.Pk_Id_Programa;
                }
                else
                {
                    return ProbarEliminar;
                }
            }
            if (IdProgramaProg!=0)
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    var ListaVer = (from s in db.Tbl_Auditorias
                                    where s.Fk_Id_Programa == IdProgramaProg
                                    select s).ToList<Auditorias>();

                    if (ListaVer != null)
                    {
                        ListaAuditorias = ListaVer;
                    }
                }
                if (ListaAuditorias.Count==0)
                {
                    try
                    {
                        using (SG_SSTContext db = new SG_SSTContext())
                        {
                            db.Entry(AuditoriaPrograma).State = System.Data.Entity.EntityState.Deleted;
                            db.SaveChanges();
                            ProbarEliminar = true;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    return ProbarEliminar;
                }
            }
            return ProbarEliminar;
        }
        public bool EliminarPlanAuditoria(int IdAuditoria)
        {
            bool ProbarEliminar = false;
            int IdAuditoriaAud = 0;
            Auditorias Auditoria = new Auditorias();
            List<AuditoriaListaVer> ListaVerificacion = new List<AuditoriaListaVer>();
            List<AuditoriaInforme> InformeAuditoria = new List<AuditoriaInforme>();
            List<ActividadAuditoria> ListaActividades = new List<ActividadAuditoria>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Aud = (from s in db.Tbl_Auditorias
                           where s.Pk_Id_Auditoria == IdAuditoria
                           select s).FirstOrDefault<Auditorias>();

                if (Aud != null)
                {
                    Auditoria = Aud;
                    IdAuditoriaAud = Auditoria.Pk_Id_Auditoria;
                }
                else
                {
                    return ProbarEliminar;
                }
            }

            if (IdAuditoriaAud!=0)
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    var ListaVer = (from s in db.Tbl_AuditoriaListaVer
                                    where s.Fk_Id_Auditoria == IdAuditoriaAud
                                    select s).ToList<AuditoriaListaVer>();

                    if (ListaVer != null)
                    {
                        ListaVerificacion = ListaVer;
                    }
                }
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    var ListaInf = (from s in db.Tbl_AuditoriaInforme
                                    where s.Pk_Id_Auditoria == IdAuditoriaAud
                                    select s).ToList<AuditoriaInforme>();

                    if (ListaInf != null)
                    {
                        InformeAuditoria = ListaInf;
                    }
                }
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    var ListaAct = (from s in db.Tbl_ActividadAuditoria
                                    where s.Fk_Id_Auditoria == IdAuditoriaAud
                                    select s).ToList<ActividadAuditoria>();

                    if (ListaAct != null)
                    {
                        ListaActividades = ListaAct;
                    }
                }
            }

            if (InformeAuditoria.Count>0)
            {
                foreach (var item in InformeAuditoria)
                {
                    try
                    {
                        using (SG_SSTContext db = new SG_SSTContext())
                        {
                            db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                            db.SaveChanges();
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        string mensaje = ex.ToString();
                    }

                }
            }
            
            foreach (var item in ListaVerificacion)
            {
                try
                {
                    using (SG_SSTContext db = new SG_SSTContext())
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                }
                catch (Exception)
                {
                }
                
            }
           
            foreach (var item in ListaActividades)
            {
                try
                {
                    using (SG_SSTContext db = new SG_SSTContext())
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                }
                catch (Exception)
                {
                }
                
            }

            try
            {
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    db.Entry(Auditoria).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                    ProbarEliminar = true;
                }
            }
            catch (Exception ex)
            {
                //restaurar Informe
                string mensaje = ex.ToString();
                foreach (var item in InformeAuditoria)
                {
                    EDAuditoriaInforme EDAuditoriaInforme = new EDAuditoriaInforme();
                    EDAuditoriaInforme.Pk_Id_Auditoria = item.Pk_Id_Auditoria;
                    EDAuditoriaInforme.Conclusiones = item.Conclusiones;
                    EDAuditoriaInforme.NombreArchivoAuditor = item.NombreArchivoAuditor;
                    EDAuditoriaInforme.NombreArchivoRes = item.NombreArchivoRes;
                    EDAuditoriaInforme.RutaArchivoAuditor = item.RutaArchivoAuditor;
                    EDAuditoriaInforme.RutaArchivoRes = item.RutaArchivoRes;
                    try
                    {
                        ActualizarConclusiones(EDAuditoriaInforme);
                    }
                    catch (Exception)
                    {
                    }
                    
                }               
            }


            
            return ProbarEliminar;
        }
        public List<EDAuditoriaPrograma> ConsultaProgramaFiltros(string Año, string Nombre, string Sede, int IdEmpresa)
        {
            List<EDAuditoriaPrograma> ListaEDPrograma = new List<EDAuditoriaPrograma>();
            List<AuditoriaPrograma> ListaPrograma = new List<AuditoriaPrograma>();

            using (SG_SSTContext db = new SG_SSTContext())
            {
                var ListaProg = (from s in db.Tbl_AuditoriaPrograma
                                 where s.Fk_Id_Empresa == IdEmpresa
                                 select s).ToList<AuditoriaPrograma>();
                if (ListaProg != null)
                {
                    ListaPrograma = ListaProg;
                }
            }

            int AñoInt = 0;
            if (int.TryParse(Año, out AñoInt))
            {
                ListaPrograma = ListaPrograma.Where(s => s.Año == AñoInt).ToList();
            }
            int SedeInt = 0;
            if (int.TryParse(Sede, out SedeInt))
            {
                ListaPrograma = ListaPrograma.Where(s => s.Fk_Id_Sede == SedeInt).ToList();
            }
            if (Nombre!=null)
            {
                ListaPrograma = ListaPrograma.Where(s => s.Titulo.ToLower().Contains(Nombre.ToLower())).ToList();
            }
            foreach (var item in ListaPrograma)
            {
                EDAuditoriaPrograma EDAuditoriaPrograma = new EDAuditoriaPrograma();
                EDAuditoriaPrograma.Pk_Id_Programa = item.Pk_Id_Programa;
                EDAuditoriaPrograma.Titulo = item.Titulo;
                EDAuditoriaPrograma.Objetivo = item.Objetivo;
                EDAuditoriaPrograma.Alcance = item.Alcance;
                EDAuditoriaPrograma.Metodologia = item.Metodologia;
                EDAuditoriaPrograma.Competencia = item.Competencia;
                EDAuditoriaPrograma.Recursos = item.Recursos;
                EDAuditoriaPrograma.Fecha_Programacion = item.Fecha_Programacion;
                EDAuditoriaPrograma.Año = item.Año;
                EDAuditoriaPrograma.Periodicidad = item.Periodicidad;
                EDAuditoriaPrograma.NombreArchivoRes = item.NombreArchivoRes;
                EDAuditoriaPrograma.RutaArchivoRes = item.RutaArchivoRes;
                EDAuditoriaPrograma.Nombre_Responsable = item.Nombre_Responsable;
                EDAuditoriaPrograma.Numero_Id_Responsable = item.Numero_Id_Responsable;
                EDAuditoriaPrograma.FirmaScrImageRes = item.FirmaScrImageRes;
                EDAuditoriaPrograma.NombreArchivoCopasst = item.NombreArchivoCopasst;
                EDAuditoriaPrograma.RutaArchivoPres = item.RutaArchivoPres;
                EDAuditoriaPrograma.Nombre_Copasst = item.Nombre_Copasst;
                EDAuditoriaPrograma.Numero_Id_Copasst = item.Numero_Id_Copasst;
                EDAuditoriaPrograma.FirmaScrImagePres = item.FirmaScrImagePres;
                EDAuditoriaPrograma.Fk_Id_Empresa = item.Fk_Id_Empresa;
                EDAuditoriaPrograma.SedeAuditoria = item.SedeAuditoria;
                EDAuditoriaPrograma.Fk_Id_Sede = item.Fk_Id_Sede;
                ListaEDPrograma.Add(EDAuditoriaPrograma);
            }

            return ListaEDPrograma;
        }
        public EDAuditoriaInforme ConsultaConclusiones(int IdAuditoria)
        {
            EDAuditoriaInforme EDAuditoriaInforme = new EDAuditoriaInforme();
            AuditoriaInforme AuditoriaInforme = new AuditoriaInforme();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Inf = (from s in db.Tbl_AuditoriaInforme
                           where s.Pk_Id_Auditoria == IdAuditoria
                           select s).FirstOrDefault<AuditoriaInforme>();

                if (Inf != null)
                {
                    AuditoriaInforme = Inf;
                    EDAuditoriaInforme.NombreArchivoAuditor = AuditoriaInforme.NombreArchivoAuditor;
                    EDAuditoriaInforme.NombreArchivoRes = AuditoriaInforme.NombreArchivoRes;
                    EDAuditoriaInforme.RutaArchivoAuditor = AuditoriaInforme.RutaArchivoAuditor;
                    EDAuditoriaInforme.RutaArchivoRes = AuditoriaInforme.RutaArchivoRes;
                }
            }

            return EDAuditoriaInforme;
        }
        public bool ActualizarConclusiones(EDAuditoriaInforme EDAuditoriaInforme)
        {
            bool ProbarGuardar = false;
            int Existe = 0;
            AuditoriaInforme AuditoriaInforme = new AuditoriaInforme();
            AuditoriaInforme AuditoriaInformeOperacion = new AuditoriaInforme();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Inf = (from s in db.Tbl_AuditoriaInforme
                           where s.Pk_Id_Auditoria == EDAuditoriaInforme.Pk_Id_Auditoria
                           select s).FirstOrDefault<AuditoriaInforme>();

                if (Inf != null)
                {
                    AuditoriaInforme = Inf;
                    Existe = 1;
                }
                else
                {
                    Existe = 2;
                }
            }
            //Actualizar
            if (Existe==1)
            {
                AuditoriaInformeOperacion.Pk_Id_Auditoria = EDAuditoriaInforme.Pk_Id_Auditoria;
                AuditoriaInformeOperacion.FechaRealizacion = DateTime.Today.ToShortDateString();
                AuditoriaInformeOperacion.Conclusiones = EDAuditoriaInforme.Conclusiones;
                AuditoriaInformeOperacion.NombreArchivoRes = EDAuditoriaInforme.NombreArchivoRes;
                AuditoriaInformeOperacion.RutaArchivoRes = EDAuditoriaInforme.RutaArchivoRes;
                AuditoriaInformeOperacion.NombreArchivoAuditor = EDAuditoriaInforme.NombreArchivoAuditor;
                AuditoriaInformeOperacion.RutaArchivoAuditor = EDAuditoriaInforme.RutaArchivoAuditor;
                try
                {
                    using (SG_SSTContext db = new SG_SSTContext())
                    {
                        db.Entry(AuditoriaInformeOperacion).State = EntityState.Modified;
                        db.SaveChanges();
                        ProbarGuardar = true;
                        return ProbarGuardar;
                    }
                }
                catch (Exception ex)
                {
                    string mensaje = ex.ToString();
                    return ProbarGuardar;
                }
            }
            //Crear
            if (Existe == 2)
            {
                AuditoriaInformeOperacion.Pk_Id_Auditoria = EDAuditoriaInforme.Pk_Id_Auditoria;
                AuditoriaInformeOperacion.FechaRealizacion = DateTime.Today.ToShortDateString();
                AuditoriaInformeOperacion.Conclusiones = EDAuditoriaInforme.Conclusiones;
                AuditoriaInformeOperacion.NombreArchivoRes = EDAuditoriaInforme.NombreArchivoRes;
                AuditoriaInformeOperacion.RutaArchivoRes = EDAuditoriaInforme.RutaArchivoRes;
                AuditoriaInformeOperacion.NombreArchivoAuditor = EDAuditoriaInforme.NombreArchivoAuditor;
                AuditoriaInformeOperacion.RutaArchivoAuditor = EDAuditoriaInforme.RutaArchivoAuditor;
                using (SG_SSTContext db = new SG_SSTContext())
                {
                    try
                    {
                        db.Tbl_AuditoriaInforme.Add(AuditoriaInformeOperacion);
                        db.SaveChanges();
                        ProbarGuardar = true;
                        return ProbarGuardar;
                    }
                    catch (Exception ex)
                    {
                        string mensaje = ex.ToString();
                        return ProbarGuardar;
                    }
                    
                }
            }
            return ProbarGuardar;
        }
    }
}
