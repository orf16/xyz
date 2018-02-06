using SG_SST.Audotoria;
using SG_SST.EntidadesDominio.Participacion;
using SG_SST.Models;
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

namespace SG_SST.Repositorio.Participacion
{
    public class ConsultaManager : IConsulta
    {


        public EDConsultaSST GrabarConsultaSST(EDConsultaSST consulta)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        ConsultaSST consultasst = new ConsultaSST()
                        {
                            Pk_Id_Consulta=consulta.PkConsultaED,
                            Fk_Empresa=consulta.FkEmpresaED,
                            Id_Usuario=consulta.IdUsuarioED,
                            Tipo_Consulta=consulta.TipoConsultaED,
                            Descripcion_Consulta=consulta.DescripcionConsultaED,
                            Fecha_Consulta = consulta.FechaConsultaED,
                            Fecha_Revision = consulta.FechaRevisionED
                        };
                        context.Tbl_ConsultaSST.Add(consultasst);
                        context.SaveChanges();
                        Transaction.Commit();
                        consulta.PkConsultaED = consultasst.Pk_Id_Consulta;
                        return consulta;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ConsultaManager), string.Format("Error al registrar la Consulta  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }            
        }
        
        public List<EDConsultaSST> ObtenerConsultasSST(int idEmpresa)
        {
            List<EDConsultaSST> ConsultaSST = null;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                ConsultaSST = (from s in context.Tbl_ConsultaSST
                            where s.Fk_Empresa == idEmpresa
                               select new EDConsultaSST
                            {
                                PkConsultaED = s.Pk_Id_Consulta,
                                FechaConsultaED = s.Fecha_Consulta,
                                TipoConsultaED = s.Tipo_Consulta,
                                DescripcionConsultaED = s.Descripcion_Consulta,
                                //FechaRevisionED = (s.Fecha_Revision == DateTime.MinValue ? string.Empty : s.Fecha_Revision),
                                FechaRevisionED = s.Fecha_Revision,
                                ObservacionesED = s.Observaciones,
                                NombreArchivo1_download = s.NombreArchivo1_download,
                                NombreArchivo2_download = s.NombreArchivo2_download,
                                NombreArchivo3_download = s.NombreArchivo3_download,
                                NombreArchivo1 = s.NombreArchivo1,
                                NombreArchivo2 = s.NombreArchivo2,
                                NombreArchivo3 = s.NombreArchivo3,
                                Ruta1 = s.Ruta1,
                                Ruta2 = s.Ruta2,
                                Ruta3 = s.Ruta3
                            }).ToList();
            }
            return ConsultaSST;
        }

        public EDConsultaSST ObtenerUnaConsultaSST(int idConsulta)
        {
             //join sede in contexto.Tbl_Sede
             //                   on Emp.Pk_Id_Empresa equals sede.Fk_Id_Empresa
            EDConsultaSST ConsultaSST = null;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                ConsultaSST = (from s in context.Tbl_ConsultaSST
                               join usuario in context.Tbl_UsuarioSistema
                               on s.Id_Usuario equals usuario.Pk_Id_UsuarioSistema
                               where s.Pk_Id_Consulta == idConsulta
                               select new EDConsultaSST
                               {
                                   PkConsultaED = s.Pk_Id_Consulta,
                                   FechaConsultaED = s.Fecha_Consulta,
                                   TipoConsultaED = s.Tipo_Consulta,
                                   DescripcionConsultaED = s.Descripcion_Consulta,
                                   FechaRevisionED = s.Fecha_Revision,
                                   ObservacionesED = s.Observaciones,
                                   NombreArchivo1_download = s.NombreArchivo1_download,
                                   NombreArchivo2_download = s.NombreArchivo2_download,
                                   NombreArchivo3_download = s.NombreArchivo3_download,
                                   NombreArchivo1 = s.NombreArchivo1,
                                   NombreArchivo2 = s.NombreArchivo2,
                                   NombreArchivo3 = s.NombreArchivo3,
                                   Ruta1 = s.Ruta1,
                                   Ruta2 = s.Ruta2,
                                   Ruta3 = s.Ruta3,
                                   Nombre = usuario.Nombres,
                                   Documento = usuario.Documento,
                                   Apellidos = usuario.Apellidos
                               }).First();
            }
            return ConsultaSST;

        }

        public bool EditarGestionConsulta(EDConsultaTrazabilidad NuevoAdmonCTZB)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        var GestionConsulta = context.Tbl_ConsultaSST.Where(y => y.Pk_Id_Consulta == NuevoAdmonCTZB.PkConsultaED).First();
                        GestionConsulta.Fecha_Revision = NuevoAdmonCTZB.Fecha_Fab;
                        GestionConsulta.Observaciones = NuevoAdmonCTZB.ObservacionesED;
                        GestionConsulta.NombreArchivo1 = NuevoAdmonCTZB.NombreArchivo1;
                        GestionConsulta.NombreArchivo1_download = NuevoAdmonCTZB.NombreArchivo1_download;
                        GestionConsulta.Ruta1 = NuevoAdmonCTZB.Ruta1;
                        GestionConsulta.NombreArchivo2 = NuevoAdmonCTZB.NombreArchivo2;
                        GestionConsulta.NombreArchivo2_download = NuevoAdmonCTZB.NombreArchivo2_download;
                        GestionConsulta.Ruta2 = NuevoAdmonCTZB.Ruta2;
                        GestionConsulta.NombreArchivo3 = NuevoAdmonCTZB.NombreArchivo3;
                        GestionConsulta.NombreArchivo3_download = NuevoAdmonCTZB.NombreArchivo3_download;
                        GestionConsulta.Ruta3 = NuevoAdmonCTZB.Ruta3;
                        context.Entry(GestionConsulta).State = EntityState.Modified;
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ConsultaManager), string.Format("Error al editar La Gestion de la consulta en la base de datos  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
        
        public bool EliminarEvidenciaConsulta(int id, string ruta, int dato)
        {

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {

                        ConsultaSST evidencia = context.Tbl_ConsultaSST.Where(p => p.Pk_Id_Consulta == id).FirstOrDefault();
                        if(dato == 1)
                        {
                            evidencia.NombreArchivo1 = null;
                            evidencia.NombreArchivo1_download = null;
                            evidencia.Ruta1 = null;
            }
                        if (dato == 2)
                        {
                            evidencia.NombreArchivo2 = null;
                            evidencia.NombreArchivo2_download = null;
                            evidencia.Ruta2 = null;
                        }
                        if (dato == 3)
                        {
                            evidencia.NombreArchivo3 = null;
                            evidencia.NombreArchivo3_download = null;
                            evidencia.Ruta3 = null;
                        }
                        context.Entry(evidencia).State = EntityState.Modified;
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ConsultaManager), string.Format("Error al eliminar la evidencia de la ConsultaSST base de datos  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public List<EDConsultaSST> ConsultarConsultasSST(EDConsultarConsultasSST consultar)
        {
            List<EDConsultaSST> ConsultaSST = null;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                if (consultar.Fecha_ini != null)
                {
                    ConsultaSST = (from s in context.Tbl_ConsultaSST
                               where s.Fk_Empresa == consultar.idEmpresa &&
                                (DbFunctions.TruncateTime(s.Fecha_Consulta) >= DbFunctions.TruncateTime(consultar.Fecha_ini) && DbFunctions.TruncateTime(s.Fecha_Consulta) <= DbFunctions.TruncateTime(consultar.Fecha_Fin)) 
                               select new EDConsultaSST
                               {
                                   PkConsultaED = s.Pk_Id_Consulta,
                                   FechaConsultaED = s.Fecha_Consulta,
                                   TipoConsultaED = s.Tipo_Consulta,
                                   DescripcionConsultaED = s.Descripcion_Consulta,
                                   FechaRevisionED = s.Fecha_Revision,
                                   ObservacionesED = s.Observaciones,
                                   NombreArchivo1_download = s.NombreArchivo1_download,
                                   NombreArchivo2_download = s.NombreArchivo2_download,
                                   NombreArchivo3_download = s.NombreArchivo3_download,
                                   NombreArchivo1 = s.NombreArchivo1,
                                   NombreArchivo2 = s.NombreArchivo2,
                                   NombreArchivo3 = s.NombreArchivo3,
                                   Ruta1 = s.Ruta1,
                                   Ruta2 = s.Ruta2,
                                   Ruta3 = s.Ruta3
                               }).ToList();
                }
                else
                {
                    ConsultaSST = (from s in context.Tbl_ConsultaSST
                                   where s.Fk_Empresa == consultar.idEmpresa 
                                   select new EDConsultaSST
                                   {
                                       PkConsultaED = s.Pk_Id_Consulta,
                                       FechaConsultaED = s.Fecha_Consulta,
                                       TipoConsultaED = s.Tipo_Consulta,
                                       DescripcionConsultaED = s.Descripcion_Consulta,
                                       FechaRevisionED = s.Fecha_Revision,
                                       ObservacionesED = s.Observaciones,
                                       NombreArchivo1_download = s.NombreArchivo1_download,
                                       NombreArchivo2_download = s.NombreArchivo2_download,
                                       NombreArchivo3_download = s.NombreArchivo3_download,
                                       NombreArchivo1 = s.NombreArchivo1,
                                       NombreArchivo2 = s.NombreArchivo2,
                                       NombreArchivo3 = s.NombreArchivo3,
                                       Ruta1 = s.Ruta1,
                                       Ruta2 = s.Ruta2,
                                       Ruta3 = s.Ruta3
                                   }).ToList();
                }
                //Filtros
                //Busqueda por el tipoConsult
                if (consultar.tipoConsult != "Seleccione")
                {
                    ConsultaSST = ConsultaSST.Where(o => o.TipoConsultaED == consultar.tipoConsult).ToList();                        
                }
                ////filtro para que solo me traiga las consultas desde la fecha inicial hasta la fecha final.
                //if (consultar.Fecha_ini != null && consultar.Fecha_Fin != null)
                //{
                //     //result = result.Where(o => (o.farmDate >= farmDate && o.farmDate <= farmDateTo));
                //    ConsultaSST = ConsultaSST.Where(o => (o.FechaConsultaED >= consultar.Fecha_ini && o.FechaConsultaED <= consultar.Fecha_Fin)).ToList();
                //        //>= DbFunctions.TruncateTime(Fecha_ini)).ToList();                        
                //}
            
        }
            return ConsultaSST;
    }
    }
}
