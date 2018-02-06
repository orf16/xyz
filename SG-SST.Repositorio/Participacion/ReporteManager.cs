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
    public class ReporteManager : IReporte
    {
        public EDReporte GuardarReporte(EDReporte reporte)
        {
            var validarActividad = false;

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        Reporte rep = new Reporte
                        {


                            RazonSocialEmpresa = reporte.RazonSocialEmpresa,
                            FK_NitEmpresa = reporte.nitEmpresa,
                            PK_Id_Reportes = reporte.IdReportes,

                            FK_Sede = reporte.FKSede,
                            FK_Tipo_Reporte = reporte.FKTipoReporte,
                            FK_Proceso = reporte.Procesos,
                            Area_Lugar = reporte.AreaLugar,
                            fechaSistema = reporte.fechaSistena,
                            Fecha_Ocurrencia = reporte.FechaOcurrencia,
                            Cedula_Quien_Reporta = reporte.CedulaQuienReporta,

                            descripcion_Reporte = reporte.DescripcionReporte,
                            Causa_Reporte = reporte.CausaReporte,
                            Sugerencias_Reporte = reporte.SugerenciasReporte,
                            medioAcceso = reporte.medioAcceso,
                            ConsecutivoReporte=reporte.ConsecutivoReporte,

                        };

                        context.Tbl_Reportes.Add(rep);
                        context.SaveChanges();
                        List<ActividadesActosInseguros> actividades = new List<ActividadesActosInseguros>();

                        if (reporte.actividades != null)
                        {
                            foreach (var act in reporte.actividades)
                            {

                                ActividadesActosInseguros actividad = new ActividadesActosInseguros();

                                if (act.RespActividad != null && act.nombreActividad!=null && act.FecEjecucion.Year!=1)
                                {
                                    actividad.NombreActividad = act.nombreActividad;
                                    actividad.ResponsableActividad = act.RespActividad;
                                    actividad.FechaEjecucion = act.FecEjecucion;
                                    actividad.FK_Id_Reportes = rep.PK_Id_Reportes;
                                    actividades.Add(actividad);
                                    validarActividad = true;
                                }
                            }

                        }
                        if (validarActividad == true)
                        {
                            context.Tbl_ActividadesActosInseguros.AddRange(actividades);

                        }
                        List<ImagenesReportes> imagenes = new List<ImagenesReportes>();

                        foreach (var imag in reporte.imagenes)
                        {

                            ImagenesReportes imagen = new ImagenesReportes();
                            imagen.ruta = imag;
                            imagen.FK_Id_Reportes = rep.PK_Id_Reportes;
                            imagenes.Add(imagen);

                        }



                        context.Tbl_ImagenesReportes.AddRange(imagenes);


                        context.SaveChanges();
                        transaction.Commit();

                        return reporte;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ReporteManager), string.Format("Error al guardar el reporte en la base de datos  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        transaction.Rollback();
                        return reporte;
                    }
                }
            }
        }




        public EDReporte GuardarReporteEditado(EDReporte reporte)
        {
            var validarActividad = false;
            var validarImagenes = false;
            if(reporte.acceso==1)
            {
                reporte.medioAcceso = true;
            }
            else
            {
                reporte.medioAcceso = false;
            }
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        Reporte rep = new Reporte
                        {




                            RazonSocialEmpresa = reporte.RazonSocialEmpresa,
                            FK_NitEmpresa = reporte.nitEmpresa,
                            PK_Id_Reportes = reporte.IdReportes,

                            FK_Sede = reporte.FKSede,
                            FK_Tipo_Reporte = reporte.FKTipoReporte,
                            FK_Proceso = reporte.Procesos,
                            Area_Lugar = reporte.AreaLugar,
                            fechaSistema = reporte.fechaSistena,
                            Fecha_Ocurrencia = reporte.FechaOcurrencia,
                            Cedula_Quien_Reporta = reporte.CedulaQuienReporta,

                            descripcion_Reporte = reporte.DescripcionReporte,
                            Causa_Reporte = reporte.CausaReporte,
                            Sugerencias_Reporte = reporte.SugerenciasReporte,
                            medioAcceso = reporte.medioAcceso,
                            ConsecutivoReporte = reporte.ConsecutivoReporte,
                        };

                        List<ActividadesActosInseguros> actividadesNuevas = new List<ActividadesActosInseguros>();

                        if (reporte.actividades != null)
                        {
                            foreach (var act in reporte.actividades)
                            {

                                ActividadesActosInseguros actividad = new ActividadesActosInseguros();


                            
                                if (act.FKReportes == 0 && act.nombreActividad!=null &&  act.RespActividad != null  && act.FecEjecucion.Year!=1 )
                                {
                                    actividad.NombreActividad = act.nombreActividad;
                                    actividad.ResponsableActividad = act.RespActividad;
                                    actividad.FechaEjecucion = act.FecEjecucion;
                                    actividad.FK_Id_Reportes = rep.PK_Id_Reportes;
                                    actividadesNuevas.Add(actividad);
                                    validarActividad = true;
                                }
                            }

                        }

                        List<ImagenesReportes> imagenes = new List<ImagenesReportes>();

                        foreach (var imag in reporte.imagenes)
                        {

                            if (imag != null)
                            {
                                ImagenesReportes imagen = new ImagenesReportes();
                                imagen.ruta = imag;
                                imagen.FK_Id_Reportes = rep.PK_Id_Reportes;
                                imagenes.Add(imagen);
                                validarImagenes = true;

                            }


                        }

                        if (validarImagenes == true)
                        {
                            context.Tbl_ImagenesReportes.AddRange(imagenes);


                        }

                        if (validarActividad == true)
                        {
                            context.Tbl_ActividadesActosInseguros.AddRange(actividadesNuevas);

                        }


                        context.Entry(rep).State = EntityState.Modified;



                        context.SaveChanges();
                        transaction.Commit();

                        return reporte;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ReporteManager), string.Format("Error al guardar el reporte en la base de datos  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        transaction.Rollback();
                        return reporte;
                    }
                }
            }
        }




        public List<EDReporte> ObtenerReportesPorEmpresa(string nit)
        {

            List<EDReporte> reportes = new List<EDReporte>();
            using (SG_SSTContext contex = new SG_SSTContext())
            {

                reportes = (from reporte in contex.Tbl_Reportes
                            join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                            join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa
                            where e.Nit_Empresa == nit



                            select new EDReporte
                            {
                                IdReportes = reporte.PK_Id_Reportes,
                                nitEmpresa = reporte.FK_NitEmpresa,
                                FKTipoReporte = reporte.FK_Tipo_Reporte,
                                FechaOcurrencia = reporte.Fecha_Ocurrencia,
                                AreaLugar = reporte.Area_Lugar,
                                CausaReporte = reporte.Causa_Reporte,
                                SugerenciasReporte = reporte.Sugerencias_Reporte,
                                CedulaQuienReporta = reporte.Cedula_Quien_Reporta,

                                RazonSocialEmpresa = reporte.RazonSocialEmpresa,
                                fechaSistena = reporte.fechaSistema,
                                FKSede = reporte.FK_Sede,
                                FK_Proceso = reporte.FK_Proceso,
                                sede = reporte.Sede.Nombre_Sede,
                                tipo = reporte.TipoReporte.Descripcion_Tipo_Reporte,
                                DescripcionReporte = reporte.descripcion_Reporte,
                                nombreProceso = reporte.Procesos.Descripcion_Proceso,
                                medioAcceso = reporte.medioAcceso,
                                ConsecutivoReporte=reporte.ConsecutivoReporte,
                            }).OrderBy(x=>x.ConsecutivoReporte).ToList();

                return reportes;

            }
        }


        public List<EDReporte> ObtenerReporteCondicionesInsegurasPorID(int id)
        {

            List<EDReporte> reportes = new List<EDReporte>();
            using (SG_SSTContext contex = new SG_SSTContext())
            {

                reportes = (from reporte in contex.Tbl_Reportes

                            where reporte.PK_Id_Reportes == id



                            select new EDReporte
                            {
                                IdReportes = reporte.PK_Id_Reportes,
                                nitEmpresa = reporte.FK_NitEmpresa,
                                FKTipoReporte = reporte.FK_Tipo_Reporte,
                                FechaOcurrencia = reporte.Fecha_Ocurrencia,
                                AreaLugar = reporte.Area_Lugar,
                                CausaReporte = reporte.Causa_Reporte,
                                SugerenciasReporte = reporte.Sugerencias_Reporte,
                                CedulaQuienReporta = reporte.Cedula_Quien_Reporta,

                                RazonSocialEmpresa = reporte.RazonSocialEmpresa,
                                fechaSistena = reporte.fechaSistema,
                                FKSede = reporte.FK_Sede,
                                FK_Proceso = reporte.FK_Proceso,
                                sede = reporte.Sede.Nombre_Sede,
                                tipo = reporte.TipoReporte.Descripcion_Tipo_Reporte,
                                DescripcionReporte = reporte.descripcion_Reporte,
                                medioAcceso = reporte.medioAcceso,
                                nombreProceso = reporte.Procesos.Descripcion_Proceso,
                                ConsecutivoReporte = reporte.ConsecutivoReporte,
                            }).ToList();

                return reportes;

            }
        }

        public List<EDActividadesActosInseguros> ObtenerActividadesCondicionesInsegurasPorID(int id)
        {

            List<EDActividadesActosInseguros> actividades = new List<EDActividadesActosInseguros>();
            using (SG_SSTContext contex = new SG_SSTContext())
            {

                actividades = (from activi in contex.Tbl_ActividadesActosInseguros

                               where activi.FK_Id_Reportes == id



                               select new EDActividadesActosInseguros
                               {
                                   ID_ActividadActosInseguros = activi.PK_ID_ActividadActosInseguros,
                                   nombreActividad = activi.NombreActividad,
                                   RespActividad = activi.ResponsableActividad,
                                   FecEjecucion = activi.FechaEjecucion,
                                   FKReportes = activi.FK_Id_Reportes,

                               }).ToList();

                return actividades;

            }
        }

        public List<EDImagenesReportes> ObtenerImagenesCondicionesInsegurasPorID(int id)
        {

            List<EDImagenesReportes> imagenes = new List<EDImagenesReportes>();
            using (SG_SSTContext contex = new SG_SSTContext())
            {

                imagenes = (from ima in contex.Tbl_ImagenesReportes

                            where ima.FK_Id_Reportes == id



                            select new EDImagenesReportes
                            {
                                IDImagenesReportes = ima.PK_ImagenesReportes,
                                rutaArchivo = ima.ruta,
                                FKReportes = ima.FK_Id_Reportes,

                            }).ToList();

                return imagenes;

            }
        }



        public List<EDReporte> ObteneReportesPorBusqueda(EDReporte rb)
        {
            List<int> sedes = new List<int>();
            sedes = rb.sedes;

            List<EDReporte> reporteBuscado = new List<EDReporte>();
            List<EDReporte> reportes = new List<EDReporte>();

            //documento
            if (!rb.CedulaQuienReporta.Equals("") && rb.FKTipoReporte == 0 && rb.fechaFin.Year == 1 && sedes == null)
            {

                using (SG_SSTContext contex = new SG_SSTContext())
                {

                    reportes = (from reporte in contex.Tbl_Reportes
                                join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa

                                where e.Nit_Empresa == rb.nitEmpresa
                                && reporte.Cedula_Quien_Reporta == rb.CedulaQuienReporta

                                select new EDReporte

                                {

                                    IdReportes = reporte.PK_Id_Reportes,
                                    nitEmpresa = reporte.FK_NitEmpresa,
                                    FKTipoReporte = reporte.FK_Tipo_Reporte,
                                    FechaOcurrencia = reporte.Fecha_Ocurrencia,
                                    AreaLugar = reporte.Area_Lugar,
                                    CausaReporte = reporte.Causa_Reporte,
                                    SugerenciasReporte = reporte.Sugerencias_Reporte,
                                    CedulaQuienReporta = reporte.Cedula_Quien_Reporta,

                                    RazonSocialEmpresa = reporte.RazonSocialEmpresa,
                                    fechaSistena = reporte.fechaSistema,
                                    FKSede = reporte.FK_Sede,
                                    FK_Proceso = reporte.FK_Proceso,
                                    sede = reporte.Sede.Nombre_Sede,
                                    tipo = reporte.TipoReporte.Descripcion_Tipo_Reporte,
                                    DescripcionReporte = reporte.descripcion_Reporte,
                                    medioAcceso = reporte.medioAcceso,
                                    nombreProceso = reporte.Procesos.Descripcion_Proceso,
                                    ConsecutivoReporte = reporte.ConsecutivoReporte,
                                }).ToList();

                }
                reporteBuscado = reportes;
            }
            //tipo reporte
            else if (!rb.FKTipoReporte.Equals("") && rb.CedulaQuienReporta == 0 && rb.fechaFin.Year == 1 && sedes == null)
            {
                using (SG_SSTContext contex = new SG_SSTContext())
                {

                    reportes = (from reporte in contex.Tbl_Reportes
                                join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa

                                where e.Nit_Empresa == rb.nitEmpresa
                                && reporte.FK_Tipo_Reporte == rb.FKTipoReporte

                                select new EDReporte

                                {

                                    IdReportes = reporte.PK_Id_Reportes,
                                    nitEmpresa = reporte.FK_NitEmpresa,
                                    FKTipoReporte = reporte.FK_Tipo_Reporte,
                                    FechaOcurrencia = reporte.Fecha_Ocurrencia,
                                    AreaLugar = reporte.Area_Lugar,
                                    CausaReporte = reporte.Causa_Reporte,
                                    SugerenciasReporte = reporte.Sugerencias_Reporte,
                                    CedulaQuienReporta = reporte.Cedula_Quien_Reporta,

                                    RazonSocialEmpresa = reporte.RazonSocialEmpresa,
                                    fechaSistena = reporte.fechaSistema,
                                    FKSede = reporte.FK_Sede,
                                    FK_Proceso = reporte.FK_Proceso,
                                    sede = reporte.Sede.Nombre_Sede,
                                    tipo = reporte.TipoReporte.Descripcion_Tipo_Reporte,
                                    DescripcionReporte = reporte.descripcion_Reporte,
                                    medioAcceso = reporte.medioAcceso,
                                    nombreProceso = reporte.Procesos.Descripcion_Proceso,
                                    ConsecutivoReporte = reporte.ConsecutivoReporte,
                                }).ToList();

                }
                reporteBuscado = reportes;
            }
            //fecha
            else if (!(rb.fechaFin.Year == 1) && rb.FKTipoReporte == 0 && rb.CedulaQuienReporta == 0 && sedes == null)
            {


                using (SG_SSTContext contex = new SG_SSTContext())
                {


                    reportes = (from reporte in contex.Tbl_Reportes
                                join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa

                                where e.Nit_Empresa == rb.nitEmpresa

                                && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) >= DbFunctions.TruncateTime(rb.fechaInicio) && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) <= DbFunctions.TruncateTime(rb.fechaFin)

                                select new EDReporte

                                {

                                    IdReportes = reporte.PK_Id_Reportes,
                                    nitEmpresa = reporte.FK_NitEmpresa,
                                    FKTipoReporte = reporte.FK_Tipo_Reporte,
                                    FechaOcurrencia = reporte.Fecha_Ocurrencia,
                                    AreaLugar = reporte.Area_Lugar,
                                    CausaReporte = reporte.Causa_Reporte,
                                    SugerenciasReporte = reporte.Sugerencias_Reporte,
                                    CedulaQuienReporta = reporte.Cedula_Quien_Reporta,

                                    RazonSocialEmpresa = reporte.RazonSocialEmpresa,
                                    fechaSistena = reporte.fechaSistema,
                                    FKSede = reporte.FK_Sede,
                                    FK_Proceso = reporte.FK_Proceso,
                                    sede = reporte.Sede.Nombre_Sede,
                                    tipo = reporte.TipoReporte.Descripcion_Tipo_Reporte,
                                    DescripcionReporte = reporte.descripcion_Reporte,
                                    medioAcceso = reporte.medioAcceso,
                                    nombreProceso = reporte.Procesos.Descripcion_Proceso,
                                    ConsecutivoReporte = reporte.ConsecutivoReporte,
                                }).ToList();
                }
                reporteBuscado = reportes;

            }
            //sedes
            else if (sedes != null && rb.fechaFin.Year == 1 && rb.FKTipoReporte == 0 && rb.CedulaQuienReporta == 0)
            {
                using (SG_SSTContext contex = new SG_SSTContext())
                {

                    reportes = (from reporte in contex.Tbl_Reportes
                                join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa

                                where e.Nit_Empresa == rb.nitEmpresa

                                && sedes.Contains(reporte.FK_Sede)

                                select new EDReporte

                                {

                                    IdReportes = reporte.PK_Id_Reportes,
                                    nitEmpresa = reporte.FK_NitEmpresa,
                                    FKTipoReporte = reporte.FK_Tipo_Reporte,
                                    FechaOcurrencia = reporte.Fecha_Ocurrencia,
                                    AreaLugar = reporte.Area_Lugar,
                                    CausaReporte = reporte.Causa_Reporte,
                                    SugerenciasReporte = reporte.Sugerencias_Reporte,
                                    CedulaQuienReporta = reporte.Cedula_Quien_Reporta,

                                    RazonSocialEmpresa = reporte.RazonSocialEmpresa,
                                    fechaSistena = reporte.fechaSistema,
                                    FKSede = reporte.FK_Sede,
                                    FK_Proceso = reporte.FK_Proceso,
                                    sede = reporte.Sede.Nombre_Sede,
                                    tipo = reporte.TipoReporte.Descripcion_Tipo_Reporte,
                                    DescripcionReporte = reporte.descripcion_Reporte,
                                    medioAcceso = reporte.medioAcceso,
                                    nombreProceso = reporte.Procesos.Descripcion_Proceso,
                                    ConsecutivoReporte = reporte.ConsecutivoReporte,
                                }).ToList();
                }
                reporteBuscado = reportes;
            }
            //documento y tipo
            else if (!rb.CedulaQuienReporta.Equals("") && !rb.FKTipoReporte.Equals("") && rb.fechaFin.Year == 1 && sedes == null)
            {
                using (SG_SSTContext contex = new SG_SSTContext())
                {

                    reportes = (from reporte in contex.Tbl_Reportes
                                join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa

                                where e.Nit_Empresa == rb.nitEmpresa

                                && reporte.Cedula_Quien_Reporta == rb.CedulaQuienReporta
                                && reporte.FK_Tipo_Reporte == rb.FKTipoReporte
                                select new EDReporte

                                {

                                    IdReportes = reporte.PK_Id_Reportes,
                                    nitEmpresa = reporte.FK_NitEmpresa,
                                    FKTipoReporte = reporte.FK_Tipo_Reporte,
                                    FechaOcurrencia = reporte.Fecha_Ocurrencia,
                                    AreaLugar = reporte.Area_Lugar,
                                    CausaReporte = reporte.Causa_Reporte,
                                    SugerenciasReporte = reporte.Sugerencias_Reporte,
                                    CedulaQuienReporta = reporte.Cedula_Quien_Reporta,

                                    RazonSocialEmpresa = reporte.RazonSocialEmpresa,
                                    fechaSistena = reporte.fechaSistema,
                                    FKSede = reporte.FK_Sede,
                                    FK_Proceso = reporte.FK_Proceso,
                                    sede = reporte.Sede.Nombre_Sede,
                                    tipo = reporte.TipoReporte.Descripcion_Tipo_Reporte,
                                    DescripcionReporte = reporte.descripcion_Reporte,
                                    medioAcceso = reporte.medioAcceso,
                                    nombreProceso = reporte.Procesos.Descripcion_Proceso,
                                    ConsecutivoReporte = reporte.ConsecutivoReporte,
                                }).ToList();
                }

                reporteBuscado = reportes;

            }
            //documento-sede
            else if (!rb.CedulaQuienReporta.Equals("") && sedes != null && rb.FKTipoReporte == 0 && rb.fechaFin.Year == 1)
            {
                using (SG_SSTContext contex = new SG_SSTContext())
                {

                    reportes = (from reporte in contex.Tbl_Reportes
                                join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa

                                where e.Nit_Empresa == rb.nitEmpresa

                                && reporte.Cedula_Quien_Reporta == rb.CedulaQuienReporta
                                && sedes.Contains(reporte.FK_Sede)

                                select new EDReporte

                                {

                                    IdReportes = reporte.PK_Id_Reportes,
                                    nitEmpresa = reporte.FK_NitEmpresa,
                                    FKTipoReporte = reporte.FK_Tipo_Reporte,
                                    FechaOcurrencia = reporte.Fecha_Ocurrencia,
                                    AreaLugar = reporte.Area_Lugar,
                                    CausaReporte = reporte.Causa_Reporte,
                                    SugerenciasReporte = reporte.Sugerencias_Reporte,
                                    CedulaQuienReporta = reporte.Cedula_Quien_Reporta,

                                    RazonSocialEmpresa = reporte.RazonSocialEmpresa,
                                    fechaSistena = reporte.fechaSistema,
                                    FKSede = reporte.FK_Sede,
                                    FK_Proceso = reporte.FK_Proceso,
                                    sede = reporte.Sede.Nombre_Sede,
                                    tipo = reporte.TipoReporte.Descripcion_Tipo_Reporte,
                                    DescripcionReporte = reporte.descripcion_Reporte,
                                    medioAcceso = reporte.medioAcceso,
                                    nombreProceso = reporte.Procesos.Descripcion_Proceso,
                                    ConsecutivoReporte = reporte.ConsecutivoReporte,
                                }).ToList();
                }

                reporteBuscado = reportes;

            }
            //documento-fecha
            else if (!rb.CedulaQuienReporta.Equals("") && !(rb.fechaFin.Year == 1) && sedes == null && rb.FKTipoReporte == 0)
            {
                using (SG_SSTContext contex = new SG_SSTContext())
                {

                    reportes = (from reporte in contex.Tbl_Reportes
                                join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa

                                where e.Nit_Empresa == rb.nitEmpresa

                                && reporte.Cedula_Quien_Reporta == rb.CedulaQuienReporta
                                && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) >= DbFunctions.TruncateTime(rb.fechaInicio)
                                && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) <= DbFunctions.TruncateTime(rb.fechaFin)


                                select new EDReporte

                                {

                                    IdReportes = reporte.PK_Id_Reportes,
                                    nitEmpresa = reporte.FK_NitEmpresa,
                                    FKTipoReporte = reporte.FK_Tipo_Reporte,
                                    FechaOcurrencia = reporte.Fecha_Ocurrencia,
                                    AreaLugar = reporte.Area_Lugar,
                                    CausaReporte = reporte.Causa_Reporte,
                                    SugerenciasReporte = reporte.Sugerencias_Reporte,
                                    CedulaQuienReporta = reporte.Cedula_Quien_Reporta,

                                    RazonSocialEmpresa = reporte.RazonSocialEmpresa,
                                    fechaSistena = reporte.fechaSistema,
                                    FKSede = reporte.FK_Sede,
                                    FK_Proceso = reporte.FK_Proceso,
                                    sede = reporte.Sede.Nombre_Sede,
                                    tipo = reporte.TipoReporte.Descripcion_Tipo_Reporte,
                                    DescripcionReporte = reporte.descripcion_Reporte,
                                    medioAcceso = reporte.medioAcceso,
                                    nombreProceso = reporte.Procesos.Descripcion_Proceso,
                                    ConsecutivoReporte = reporte.ConsecutivoReporte,
                                }).ToList();
                }

                reporteBuscado = reportes;

            }
            //tipo-sede
            else if (!rb.FKTipoReporte.Equals("") && sedes != null && rb.CedulaQuienReporta == 0 && rb.fechaFin.Year == 1)
            {
                using (SG_SSTContext contex = new SG_SSTContext())
                {

                    reportes = (from reporte in contex.Tbl_Reportes
                                join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa

                                where e.Nit_Empresa == rb.nitEmpresa

                                 && reporte.FK_Tipo_Reporte == rb.FKTipoReporte
                                 && sedes.Contains(reporte.FK_Sede)


                                select new EDReporte

                                {

                                    IdReportes = reporte.PK_Id_Reportes,
                                    nitEmpresa = reporte.FK_NitEmpresa,
                                    FKTipoReporte = reporte.FK_Tipo_Reporte,
                                    FechaOcurrencia = reporte.Fecha_Ocurrencia,
                                    AreaLugar = reporte.Area_Lugar,
                                    CausaReporte = reporte.Causa_Reporte,
                                    SugerenciasReporte = reporte.Sugerencias_Reporte,
                                    CedulaQuienReporta = reporte.Cedula_Quien_Reporta,

                                    RazonSocialEmpresa = reporte.RazonSocialEmpresa,
                                    fechaSistena = reporte.fechaSistema,
                                    FKSede = reporte.FK_Sede,
                                    FK_Proceso = reporte.FK_Proceso,
                                    sede = reporte.Sede.Nombre_Sede,
                                    tipo = reporte.TipoReporte.Descripcion_Tipo_Reporte,
                                    DescripcionReporte = reporte.descripcion_Reporte,
                                    medioAcceso = reporte.medioAcceso,
                                    nombreProceso = reporte.Procesos.Descripcion_Proceso,
                                    ConsecutivoReporte = reporte.ConsecutivoReporte,
                                }).ToList();
                }

                reporteBuscado = reportes;

            }
            //tipo-fecha
            else if (!rb.FKTipoReporte.Equals("") && !(rb.fechaFin.Year == 1) && rb.CedulaQuienReporta == 0 && sedes == null)
            {
                using (SG_SSTContext contex = new SG_SSTContext())
                {

                    reportes = (from reporte in contex.Tbl_Reportes
                                join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa

                                where e.Nit_Empresa == rb.nitEmpresa

                                 && reporte.FK_Tipo_Reporte == rb.FKTipoReporte
                                 && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) >= DbFunctions.TruncateTime(rb.fechaInicio)
                                 && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) <= DbFunctions.TruncateTime(rb.fechaFin)

                                select new EDReporte

                                {

                                    IdReportes = reporte.PK_Id_Reportes,
                                    nitEmpresa = reporte.FK_NitEmpresa,
                                    FKTipoReporte = reporte.FK_Tipo_Reporte,
                                    FechaOcurrencia = reporte.Fecha_Ocurrencia,
                                    AreaLugar = reporte.Area_Lugar,
                                    CausaReporte = reporte.Causa_Reporte,
                                    SugerenciasReporte = reporte.Sugerencias_Reporte,
                                    CedulaQuienReporta = reporte.Cedula_Quien_Reporta,

                                    RazonSocialEmpresa = reporte.RazonSocialEmpresa,
                                    fechaSistena = reporte.fechaSistema,
                                    FKSede = reporte.FK_Sede,
                                    FK_Proceso = reporte.FK_Proceso,
                                    sede = reporte.Sede.Nombre_Sede,
                                    tipo = reporte.TipoReporte.Descripcion_Tipo_Reporte,
                                    DescripcionReporte = reporte.descripcion_Reporte,
                                    medioAcceso = reporte.medioAcceso,
                                    nombreProceso = reporte.Procesos.Descripcion_Proceso,
                                    ConsecutivoReporte = reporte.ConsecutivoReporte,
                                }).ToList();
                }

                reporteBuscado = reportes;

            }
            //sede-fecha
            else if (sedes != null && !(rb.fechaFin.Year == 1) && rb.CedulaQuienReporta == 0 && rb.FKTipoReporte == 0)
            {
                using (SG_SSTContext contex = new SG_SSTContext())
                {

                    reportes = (from reporte in contex.Tbl_Reportes
                                join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa

                                where e.Nit_Empresa == rb.nitEmpresa

                                 && sedes.Contains(reporte.FK_Sede)
                                 && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) >= DbFunctions.TruncateTime(rb.fechaInicio)
                                 && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) <= DbFunctions.TruncateTime(rb.fechaFin)

                                select new EDReporte

                                {

                                    IdReportes = reporte.PK_Id_Reportes,
                                    nitEmpresa = reporte.FK_NitEmpresa,
                                    FKTipoReporte = reporte.FK_Tipo_Reporte,
                                    FechaOcurrencia = reporte.Fecha_Ocurrencia,
                                    AreaLugar = reporte.Area_Lugar,
                                    CausaReporte = reporte.Causa_Reporte,
                                    SugerenciasReporte = reporte.Sugerencias_Reporte,
                                    CedulaQuienReporta = reporte.Cedula_Quien_Reporta,

                                    RazonSocialEmpresa = reporte.RazonSocialEmpresa,
                                    fechaSistena = reporte.fechaSistema,
                                    FKSede = reporte.FK_Sede,
                                    FK_Proceso = reporte.FK_Proceso,
                                    sede = reporte.Sede.Nombre_Sede,
                                    tipo = reporte.TipoReporte.Descripcion_Tipo_Reporte,
                                    DescripcionReporte = reporte.descripcion_Reporte,
                                    medioAcceso = reporte.medioAcceso,
                                    nombreProceso = reporte.Procesos.Descripcion_Proceso,
                                    ConsecutivoReporte = reporte.ConsecutivoReporte,
                                }).ToList();
                }

                reporteBuscado = reportes;

            }
            //documento-tipo-sede
            else if (!rb.CedulaQuienReporta.Equals("") && !rb.FKTipoReporte.Equals("") && sedes != null && rb.fechaFin.Year == 1)
            {
                using (SG_SSTContext contex = new SG_SSTContext())
                {

                    reportes = (from reporte in contex.Tbl_Reportes
                                join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa

                                where e.Nit_Empresa == rb.nitEmpresa

                                 && reporte.Cedula_Quien_Reporta == rb.CedulaQuienReporta
                                 && reporte.FK_Tipo_Reporte == rb.FKTipoReporte
                                 && sedes.Contains(reporte.FK_Sede)
                                select new EDReporte

                                {

                                    IdReportes = reporte.PK_Id_Reportes,
                                    nitEmpresa = reporte.FK_NitEmpresa,
                                    FKTipoReporte = reporte.FK_Tipo_Reporte,
                                    FechaOcurrencia = reporte.Fecha_Ocurrencia,
                                    AreaLugar = reporte.Area_Lugar,
                                    CausaReporte = reporte.Causa_Reporte,
                                    SugerenciasReporte = reporte.Sugerencias_Reporte,
                                    CedulaQuienReporta = reporte.Cedula_Quien_Reporta,

                                    RazonSocialEmpresa = reporte.RazonSocialEmpresa,
                                    fechaSistena = reporte.fechaSistema,
                                    FKSede = reporte.FK_Sede,
                                    FK_Proceso = reporte.FK_Proceso,
                                    sede = reporte.Sede.Nombre_Sede,
                                    tipo = reporte.TipoReporte.Descripcion_Tipo_Reporte,
                                    DescripcionReporte = reporte.descripcion_Reporte,
                                    medioAcceso = reporte.medioAcceso,
                                    nombreProceso = reporte.Procesos.Descripcion_Proceso,
                                    ConsecutivoReporte = reporte.ConsecutivoReporte,
                                }).ToList();
                }

                reporteBuscado = reportes;

            }
            //documento-tipo-fecha
            else if (!rb.CedulaQuienReporta.Equals("") && !rb.FKTipoReporte.Equals("") && !(rb.fechaFin.Year == 1) && sedes == null)
            {
                using (SG_SSTContext contex = new SG_SSTContext())
                {

                    reportes = (from reporte in contex.Tbl_Reportes
                                join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa

                                where e.Nit_Empresa == rb.nitEmpresa

                                 && reporte.Cedula_Quien_Reporta == rb.CedulaQuienReporta
                                 && reporte.FK_Tipo_Reporte == rb.FKTipoReporte
                                 && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) >= DbFunctions.TruncateTime(rb.fechaInicio)
                                 && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) <= DbFunctions.TruncateTime(rb.fechaFin)

                                select new EDReporte

                                {

                                    IdReportes = reporte.PK_Id_Reportes,
                                    nitEmpresa = reporte.FK_NitEmpresa,
                                    FKTipoReporte = reporte.FK_Tipo_Reporte,
                                    FechaOcurrencia = reporte.Fecha_Ocurrencia,
                                    AreaLugar = reporte.Area_Lugar,
                                    CausaReporte = reporte.Causa_Reporte,
                                    SugerenciasReporte = reporte.Sugerencias_Reporte,
                                    CedulaQuienReporta = reporte.Cedula_Quien_Reporta,

                                    RazonSocialEmpresa = reporte.RazonSocialEmpresa,
                                    fechaSistena = reporte.fechaSistema,
                                    FKSede = reporte.FK_Sede,
                                    FK_Proceso = reporte.FK_Proceso,
                                    sede = reporte.Sede.Nombre_Sede,
                                    tipo = reporte.TipoReporte.Descripcion_Tipo_Reporte,
                                    DescripcionReporte = reporte.descripcion_Reporte,
                                    medioAcceso = reporte.medioAcceso,
                                    nombreProceso = reporte.Procesos.Descripcion_Proceso,
                                    ConsecutivoReporte = reporte.ConsecutivoReporte,
                                }).ToList();
                }

                reporteBuscado = reportes;

            }
            //documento-fecha-sede
            else if (!rb.CedulaQuienReporta.Equals("") && !(rb.fechaFin.Year == 1) && sedes != null && rb.FKTipoReporte == 0)
            {
                using (SG_SSTContext contex = new SG_SSTContext())
                {

                    reportes = (from reporte in contex.Tbl_Reportes
                                join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa

                                where e.Nit_Empresa == rb.nitEmpresa

                                 && reporte.Cedula_Quien_Reporta == rb.CedulaQuienReporta
                                 && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) >= DbFunctions.TruncateTime(rb.fechaInicio)
                                 && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) <= DbFunctions.TruncateTime(rb.fechaFin)
                                 && sedes.Contains(reporte.FK_Sede)
                                select new EDReporte

                                {

                                    IdReportes = reporte.PK_Id_Reportes,
                                    nitEmpresa = reporte.FK_NitEmpresa,
                                    FKTipoReporte = reporte.FK_Tipo_Reporte,
                                    FechaOcurrencia = reporte.Fecha_Ocurrencia,
                                    AreaLugar = reporte.Area_Lugar,
                                    CausaReporte = reporte.Causa_Reporte,
                                    SugerenciasReporte = reporte.Sugerencias_Reporte,
                                    CedulaQuienReporta = reporte.Cedula_Quien_Reporta,

                                    RazonSocialEmpresa = reporte.RazonSocialEmpresa,
                                    fechaSistena = reporte.fechaSistema,
                                    FKSede = reporte.FK_Sede,
                                    FK_Proceso = reporte.FK_Proceso,
                                    sede = reporte.Sede.Nombre_Sede,
                                    tipo = reporte.TipoReporte.Descripcion_Tipo_Reporte,
                                    DescripcionReporte = reporte.descripcion_Reporte,
                                    medioAcceso = reporte.medioAcceso,
                                    nombreProceso = reporte.Procesos.Descripcion_Proceso,
                                    ConsecutivoReporte = reporte.ConsecutivoReporte,
                                }).ToList();
                }

                reporteBuscado = reportes;

            }
            //tipo-sede-fecha
            else if (!rb.FKTipoReporte.Equals("") && sedes != null && !(rb.fechaFin.Year == 1) && rb.CedulaQuienReporta == 0)
            {
                using (SG_SSTContext contex = new SG_SSTContext())
                {

                    reportes = (from reporte in contex.Tbl_Reportes
                                join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa

                                where e.Nit_Empresa == rb.nitEmpresa
                                 && reporte.FK_Tipo_Reporte == rb.FKTipoReporte
                                 && sedes.Contains(reporte.FK_Sede)
                                 && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) >= DbFunctions.TruncateTime(rb.fechaInicio)
                                 && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) <= DbFunctions.TruncateTime(rb.fechaFin)
                                select new EDReporte

                                {

                                    IdReportes = reporte.PK_Id_Reportes,
                                    nitEmpresa = reporte.FK_NitEmpresa,
                                    FKTipoReporte = reporte.FK_Tipo_Reporte,
                                    FechaOcurrencia = reporte.Fecha_Ocurrencia,
                                    AreaLugar = reporte.Area_Lugar,
                                    CausaReporte = reporte.Causa_Reporte,
                                    SugerenciasReporte = reporte.Sugerencias_Reporte,
                                    CedulaQuienReporta = reporte.Cedula_Quien_Reporta,

                                    RazonSocialEmpresa = reporte.RazonSocialEmpresa,
                                    fechaSistena = reporte.fechaSistema,
                                    FKSede = reporte.FK_Sede,
                                    FK_Proceso = reporte.FK_Proceso,
                                    sede = reporte.Sede.Nombre_Sede,
                                    tipo = reporte.TipoReporte.Descripcion_Tipo_Reporte,
                                    DescripcionReporte = reporte.descripcion_Reporte,
                                    medioAcceso = reporte.medioAcceso,
                                    nombreProceso = reporte.Procesos.Descripcion_Proceso,
                                    ConsecutivoReporte = reporte.ConsecutivoReporte,
                                }).ToList();
                }

                reporteBuscado = reportes;

            }
            //sede-fecha-documento
            else if (sedes != null && !(rb.fechaFin.Year == 1) && !rb.CedulaQuienReporta.Equals("") && rb.FKTipoReporte == 0)
            {
                using (SG_SSTContext contex = new SG_SSTContext())
                {

                    reportes = (from reporte in contex.Tbl_Reportes
                                join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa

                                where e.Nit_Empresa == rb.nitEmpresa
                                 && sedes.Contains(reporte.FK_Sede)
                                 && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) >= DbFunctions.TruncateTime(rb.fechaInicio)
                                 && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) <= DbFunctions.TruncateTime(rb.fechaFin)
                                 && reporte.Cedula_Quien_Reporta == rb.CedulaQuienReporta
                                select new EDReporte

                                {

                                    IdReportes = reporte.PK_Id_Reportes,
                                    nitEmpresa = reporte.FK_NitEmpresa,
                                    FKTipoReporte = reporte.FK_Tipo_Reporte,
                                    FechaOcurrencia = reporte.Fecha_Ocurrencia,
                                    AreaLugar = reporte.Area_Lugar,
                                    CausaReporte = reporte.Causa_Reporte,
                                    SugerenciasReporte = reporte.Sugerencias_Reporte,
                                    CedulaQuienReporta = reporte.Cedula_Quien_Reporta,

                                    RazonSocialEmpresa = reporte.RazonSocialEmpresa,
                                    fechaSistena = reporte.fechaSistema,
                                    FKSede = reporte.FK_Sede,
                                    FK_Proceso = reporte.FK_Proceso,
                                    sede = reporte.Sede.Nombre_Sede,
                                    tipo = reporte.TipoReporte.Descripcion_Tipo_Reporte,
                                    DescripcionReporte = reporte.descripcion_Reporte,
                                    medioAcceso = reporte.medioAcceso,
                                    nombreProceso = reporte.Procesos.Descripcion_Proceso,
                                    ConsecutivoReporte = reporte.ConsecutivoReporte,
                                }).ToList();
                }

                reporteBuscado = reportes;

            }
            //fecha-documento-tipo
            else if (!(rb.fechaFin.Year == 1) && !rb.CedulaQuienReporta.Equals("") && !rb.FKTipoReporte.Equals("") && sedes == null)
            {
                using (SG_SSTContext contex = new SG_SSTContext())
                {

                    reportes = (from reporte in contex.Tbl_Reportes
                                join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa

                                where e.Nit_Empresa == rb.nitEmpresa
                                 && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) >= DbFunctions.TruncateTime(rb.fechaInicio)
                                 && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) <= DbFunctions.TruncateTime(rb.fechaFin)
                                 && reporte.Cedula_Quien_Reporta == rb.CedulaQuienReporta
                                 && reporte.FK_Tipo_Reporte == rb.FKTipoReporte
                                select new EDReporte

                                {

                                    IdReportes = reporte.PK_Id_Reportes,
                                    nitEmpresa = reporte.FK_NitEmpresa,
                                    FKTipoReporte = reporte.FK_Tipo_Reporte,
                                    FechaOcurrencia = reporte.Fecha_Ocurrencia,
                                    AreaLugar = reporte.Area_Lugar,
                                    CausaReporte = reporte.Causa_Reporte,
                                    SugerenciasReporte = reporte.Sugerencias_Reporte,
                                    CedulaQuienReporta = reporte.Cedula_Quien_Reporta,

                                    RazonSocialEmpresa = reporte.RazonSocialEmpresa,
                                    fechaSistena = reporte.fechaSistema,
                                    FKSede = reporte.FK_Sede,
                                    FK_Proceso = reporte.FK_Proceso,
                                    sede = reporte.Sede.Nombre_Sede,
                                    tipo = reporte.TipoReporte.Descripcion_Tipo_Reporte,
                                    DescripcionReporte = reporte.descripcion_Reporte,
                                    medioAcceso = reporte.medioAcceso,
                                    nombreProceso = reporte.Procesos.Descripcion_Proceso,
                                    ConsecutivoReporte = reporte.ConsecutivoReporte,
                                }).ToList();
                }

                reporteBuscado = reportes;

            }
            //documento-tipo-sede-fecha
            else if (!rb.CedulaQuienReporta.Equals("") && !rb.FKTipoReporte.Equals("") && sedes != null && !(rb.fechaFin.Year == 1))
            {
                using (SG_SSTContext contex = new SG_SSTContext())
                {

                    reportes = (from reporte in contex.Tbl_Reportes
                                join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa

                                where e.Nit_Empresa == rb.nitEmpresa
                                 && reporte.Cedula_Quien_Reporta == rb.CedulaQuienReporta
                                 && reporte.FK_Tipo_Reporte == rb.FKTipoReporte
                                 && sedes.Contains(reporte.FK_Sede)
                                 && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) >= DbFunctions.TruncateTime(rb.fechaInicio)
                                 && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) <= DbFunctions.TruncateTime(rb.fechaFin)


                                select new EDReporte

                                {

                                    IdReportes = reporte.PK_Id_Reportes,
                                    nitEmpresa = reporte.FK_NitEmpresa,
                                    FKTipoReporte = reporte.FK_Tipo_Reporte,
                                    FechaOcurrencia = reporte.Fecha_Ocurrencia,
                                    AreaLugar = reporte.Area_Lugar,
                                    CausaReporte = reporte.Causa_Reporte,
                                    SugerenciasReporte = reporte.Sugerencias_Reporte,
                                    CedulaQuienReporta = reporte.Cedula_Quien_Reporta,

                                    RazonSocialEmpresa = reporte.RazonSocialEmpresa,
                                    fechaSistena = reporte.fechaSistema,
                                    FKSede = reporte.FK_Sede,
                                    FK_Proceso = reporte.FK_Proceso,
                                    sede = reporte.Sede.Nombre_Sede,
                                    tipo = reporte.TipoReporte.Descripcion_Tipo_Reporte,
                                    DescripcionReporte = reporte.descripcion_Reporte,
                                    medioAcceso = reporte.medioAcceso,
                                    nombreProceso = reporte.Procesos.Descripcion_Proceso,
                                    ConsecutivoReporte = reporte.ConsecutivoReporte,
                                }).ToList();
                }

                reporteBuscado = reportes;

            }
            return reporteBuscado;
        }


        public List<EDActividadesActosInseguros> ObtenerActividadesPorBusqueda(EDReporte rb)
        {

            List<int> sedes = new List<int>();
            sedes = rb.sedes;
            List<EDActividadesActosInseguros> actividades = new List<EDActividadesActosInseguros>();
            List<EDActividadesActosInseguros> actividadesBuscado = new List<EDActividadesActosInseguros>();
            //documento
            if (!rb.CedulaQuienReporta.Equals("") && rb.FKTipoReporte == 0 && rb.fechaFin.Year == 1 && sedes == null)
            {

                using (SG_SSTContext contex = new SG_SSTContext())
                {


                    actividades = (from act in contex.Tbl_ActividadesActosInseguros
                                   join reporte in contex.Tbl_Reportes on act.FK_Id_Reportes equals reporte.PK_Id_Reportes
                                   join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                   join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa

                                   where e.Nit_Empresa == rb.nitEmpresa
                                   && reporte.Cedula_Quien_Reporta == rb.CedulaQuienReporta

                                   select new EDActividadesActosInseguros

                                   {
                                       FKReportes = reporte.PK_Id_Reportes,
                                       ID_ActividadActosInseguros = act.PK_ID_ActividadActosInseguros,
                                       nombreActividad = act.NombreActividad,
                                       RespActividad = act.ResponsableActividad,
                                       FecEjecucion = act.FechaEjecucion,
                                   }).ToList();




                }
                actividadesBuscado = actividades;
            }
            //tipo reporte
            else if (!rb.FKTipoReporte.Equals("") && rb.CedulaQuienReporta == 0 && rb.fechaFin.Year == 1 && sedes == null)
            {
                using (SG_SSTContext contex = new SG_SSTContext())
                {

                    actividades = (from act in contex.Tbl_ActividadesActosInseguros
                                   join reporte in contex.Tbl_Reportes on act.FK_Id_Reportes equals reporte.PK_Id_Reportes
                                   join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                   join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa

                                   where e.Nit_Empresa == rb.nitEmpresa
                                   && reporte.FK_Tipo_Reporte == rb.FKTipoReporte

                                   select new EDActividadesActosInseguros

                                   {

                                       FKReportes = reporte.PK_Id_Reportes,
                                       ID_ActividadActosInseguros = act.PK_ID_ActividadActosInseguros,
                                       nombreActividad = act.NombreActividad,
                                       RespActividad = act.ResponsableActividad,
                                       FecEjecucion = act.FechaEjecucion,
                                   }).ToList();

                }
                actividadesBuscado = actividades;
            }
            //fecha
            else if (!(rb.fechaFin.Year == 1) && rb.FKTipoReporte == 0 && rb.CedulaQuienReporta == 0 && sedes == null)
            {


                using (SG_SSTContext contex = new SG_SSTContext())
                {


                    actividades = (from act in contex.Tbl_ActividadesActosInseguros
                                   join reporte in contex.Tbl_Reportes on act.FK_Id_Reportes equals reporte.PK_Id_Reportes
                                   join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                   join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa
                                   where e.Nit_Empresa == rb.nitEmpresa

                                   && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) >= DbFunctions.TruncateTime(rb.fechaInicio)
                                   && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) <= DbFunctions.TruncateTime(rb.fechaFin)

                                   select new EDActividadesActosInseguros

                                   {
                                       FKReportes = reporte.PK_Id_Reportes,
                                       ID_ActividadActosInseguros = act.PK_ID_ActividadActosInseguros,
                                       nombreActividad = act.NombreActividad,
                                       RespActividad = act.ResponsableActividad,
                                       FecEjecucion = act.FechaEjecucion,
                                   }).ToList();
                }
                actividadesBuscado = actividades;

            }
            //sedes
            else if (sedes != null && rb.fechaFin.Year == 1 && rb.FKTipoReporte == 0 && rb.CedulaQuienReporta == 0)
            {
                using (SG_SSTContext contex = new SG_SSTContext())
                {

                    actividades = (from act in contex.Tbl_ActividadesActosInseguros
                                   join reporte in contex.Tbl_Reportes on act.FK_Id_Reportes equals reporte.PK_Id_Reportes
                                   join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                   join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa
                                   where e.Nit_Empresa == rb.nitEmpresa

                                   && sedes.Contains(reporte.FK_Sede)

                                   select new EDActividadesActosInseguros

                                   {
                                       FKReportes = reporte.PK_Id_Reportes,
                                       ID_ActividadActosInseguros = act.PK_ID_ActividadActosInseguros,
                                       nombreActividad = act.NombreActividad,
                                       RespActividad = act.ResponsableActividad,
                                       FecEjecucion = act.FechaEjecucion,
                                   }).ToList();
                }
                actividadesBuscado = actividades;
            }
            //documento y tipo
            else if (!rb.CedulaQuienReporta.Equals("") && !rb.FKTipoReporte.Equals("") && rb.fechaFin.Year == 1 && sedes == null)
            {
                using (SG_SSTContext contex = new SG_SSTContext())
                {

                    actividades = (from act in contex.Tbl_ActividadesActosInseguros
                                   join reporte in contex.Tbl_Reportes on act.FK_Id_Reportes equals reporte.PK_Id_Reportes
                                   join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                   join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa
                                   where e.Nit_Empresa == rb.nitEmpresa

                                   && reporte.Cedula_Quien_Reporta == rb.CedulaQuienReporta
                                   && reporte.FK_Tipo_Reporte == rb.FKTipoReporte
                                   select new EDActividadesActosInseguros

                                   {
                                       FKReportes = reporte.PK_Id_Reportes,
                                       ID_ActividadActosInseguros = act.PK_ID_ActividadActosInseguros,
                                       nombreActividad = act.NombreActividad,
                                       RespActividad = act.ResponsableActividad,
                                       FecEjecucion = act.FechaEjecucion,
                                   }).ToList();
                }

                actividadesBuscado = actividades;

            }
            //documento-sede
            else if (!rb.CedulaQuienReporta.Equals("") && sedes != null && rb.FKTipoReporte == 0 && rb.fechaFin.Year == 1)
            {
                using (SG_SSTContext contex = new SG_SSTContext())
                {

                    actividades = (from act in contex.Tbl_ActividadesActosInseguros
                                   join reporte in contex.Tbl_Reportes on act.FK_Id_Reportes equals reporte.PK_Id_Reportes
                                   join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                   join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa
                                   where e.Nit_Empresa == rb.nitEmpresa

                                   && reporte.Cedula_Quien_Reporta == rb.CedulaQuienReporta
                                   && sedes.Contains(reporte.FK_Sede)

                                   select new EDActividadesActosInseguros

                                   {
                                       FKReportes = reporte.PK_Id_Reportes,
                                       ID_ActividadActosInseguros = act.PK_ID_ActividadActosInseguros,
                                       nombreActividad = act.NombreActividad,
                                       RespActividad = act.ResponsableActividad,
                                       FecEjecucion = act.FechaEjecucion,
                                   }).ToList();
                }

                actividadesBuscado = actividades;

            }
            //documento-fecha
            else if (!rb.CedulaQuienReporta.Equals("") && !(rb.fechaFin.Year == 1) && sedes == null && rb.FKTipoReporte == 0)
            {
                using (SG_SSTContext contex = new SG_SSTContext())
                {
                    actividades = (from act in contex.Tbl_ActividadesActosInseguros
                                   join reporte in contex.Tbl_Reportes on act.FK_Id_Reportes equals reporte.PK_Id_Reportes
                                   join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                   join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa
                                   where e.Nit_Empresa == rb.nitEmpresa

                                   && reporte.Cedula_Quien_Reporta == rb.CedulaQuienReporta
                                   && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) >= DbFunctions.TruncateTime(rb.fechaInicio)
                                   && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) <= DbFunctions.TruncateTime(rb.fechaFin)

                                   select new EDActividadesActosInseguros

                                   {

                                       FKReportes = reporte.PK_Id_Reportes,
                                       ID_ActividadActosInseguros = act.PK_ID_ActividadActosInseguros,
                                       nombreActividad = act.NombreActividad,
                                       RespActividad = act.ResponsableActividad,
                                       FecEjecucion = act.FechaEjecucion,
                                   }).ToList();
                }

                actividadesBuscado = actividades;

            }
            //tipo-sede
            else if (!rb.FKTipoReporte.Equals("") && sedes != null && rb.CedulaQuienReporta == 0 && rb.fechaFin.Year == 1)
            {
                using (SG_SSTContext contex = new SG_SSTContext())
                {
                    actividades = (from act in contex.Tbl_ActividadesActosInseguros
                                   join reporte in contex.Tbl_Reportes on act.FK_Id_Reportes equals reporte.PK_Id_Reportes
                                   join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                   join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa
                                   where e.Nit_Empresa == rb.nitEmpresa

                                    && reporte.FK_Tipo_Reporte == rb.FKTipoReporte
                                    && sedes.Contains(reporte.FK_Sede)

                                   select new EDActividadesActosInseguros

                                   {
                                       FKReportes = reporte.PK_Id_Reportes,
                                       ID_ActividadActosInseguros = act.PK_ID_ActividadActosInseguros,
                                       nombreActividad = act.NombreActividad,
                                       RespActividad = act.ResponsableActividad,
                                       FecEjecucion = act.FechaEjecucion,
                                   }).ToList();

                }

                actividadesBuscado = actividades;

            }
            //tipo-fecha
            else if (!rb.FKTipoReporte.Equals("") && !(rb.fechaFin.Year == 1) && rb.CedulaQuienReporta == 0 && sedes == null)
            {
                using (SG_SSTContext contex = new SG_SSTContext())
                {

                    actividades = (from act in contex.Tbl_ActividadesActosInseguros
                                   join reporte in contex.Tbl_Reportes on act.FK_Id_Reportes equals reporte.PK_Id_Reportes
                                   join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                   join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa
                                   where e.Nit_Empresa == rb.nitEmpresa

                                    && reporte.FK_Tipo_Reporte == rb.FKTipoReporte
                                    && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) >= DbFunctions.TruncateTime(rb.fechaInicio)
                                    && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) <= DbFunctions.TruncateTime(rb.fechaFin)

                                   select new EDActividadesActosInseguros

                                   {
                                       FKReportes = reporte.PK_Id_Reportes,
                                       ID_ActividadActosInseguros = act.PK_ID_ActividadActosInseguros,
                                       nombreActividad = act.NombreActividad,
                                       RespActividad = act.ResponsableActividad,
                                       FecEjecucion = act.FechaEjecucion,
                                   }).ToList();
                }

                actividadesBuscado = actividades;

            }
            //sede-fecha
            else if (sedes != null && !(rb.fechaFin.Year == 1) && rb.CedulaQuienReporta == 0 && rb.FKTipoReporte == 0)
            {
                using (SG_SSTContext contex = new SG_SSTContext())
                {

                    actividades = (from act in contex.Tbl_ActividadesActosInseguros
                                   join reporte in contex.Tbl_Reportes on act.FK_Id_Reportes equals reporte.PK_Id_Reportes
                                   join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                   join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa
                                   where e.Nit_Empresa == rb.nitEmpresa

                                    && sedes.Contains(reporte.FK_Sede)
                                    && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) >= DbFunctions.TruncateTime(rb.fechaInicio)
                                    && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) <= DbFunctions.TruncateTime(rb.fechaFin)

                                   select new EDActividadesActosInseguros

                                   {
                                       FKReportes = reporte.PK_Id_Reportes,
                                       ID_ActividadActosInseguros = act.PK_ID_ActividadActosInseguros,
                                       nombreActividad = act.NombreActividad,
                                       RespActividad = act.ResponsableActividad,
                                       FecEjecucion = act.FechaEjecucion,
                                   }).ToList();
                }

                actividadesBuscado = actividades;

            }
            //documento-tipo-sede
            else if (!rb.CedulaQuienReporta.Equals("") && !rb.FKTipoReporte.Equals("") && sedes != null && rb.fechaFin.Year == 1)
            {
                using (SG_SSTContext contex = new SG_SSTContext())
                {

                    actividades = (from act in contex.Tbl_ActividadesActosInseguros
                                   join reporte in contex.Tbl_Reportes on act.FK_Id_Reportes equals reporte.PK_Id_Reportes
                                   join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                   join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa
                                   where e.Nit_Empresa == rb.nitEmpresa

                                    && reporte.Cedula_Quien_Reporta == rb.CedulaQuienReporta
                                    && reporte.FK_Tipo_Reporte == rb.FKTipoReporte
                                    && sedes.Contains(reporte.FK_Sede)
                                   select new EDActividadesActosInseguros

                                   {
                                       FKReportes = reporte.PK_Id_Reportes,
                                       ID_ActividadActosInseguros = act.PK_ID_ActividadActosInseguros,
                                       nombreActividad = act.NombreActividad,
                                       RespActividad = act.ResponsableActividad,
                                       FecEjecucion = act.FechaEjecucion,
                                   }).ToList();
                }

                actividadesBuscado = actividades;

            }
            //documento-tipo-fecha
            else if (!rb.CedulaQuienReporta.Equals("") && !rb.FKTipoReporte.Equals("") && !(rb.fechaFin.Year == 1) && sedes == null)
            {
                using (SG_SSTContext contex = new SG_SSTContext())
                {

                    actividades = (from act in contex.Tbl_ActividadesActosInseguros
                                   join reporte in contex.Tbl_Reportes on act.FK_Id_Reportes equals reporte.PK_Id_Reportes
                                   join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                   join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa
                                   where e.Nit_Empresa == rb.nitEmpresa

                                    && reporte.Cedula_Quien_Reporta == rb.CedulaQuienReporta
                                    && reporte.FK_Tipo_Reporte == rb.FKTipoReporte
                                    && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) >= DbFunctions.TruncateTime(rb.fechaInicio)
                                    && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) <= DbFunctions.TruncateTime(rb.fechaFin)

                                   select new EDActividadesActosInseguros

                                   {
                                       FKReportes = reporte.PK_Id_Reportes,
                                       ID_ActividadActosInseguros = act.PK_ID_ActividadActosInseguros,
                                       nombreActividad = act.NombreActividad,
                                       RespActividad = act.ResponsableActividad,
                                       FecEjecucion = act.FechaEjecucion,
                                   }).ToList();
                }

                actividadesBuscado = actividades;

            }
            //documento-fecha-sede
            else if (!rb.CedulaQuienReporta.Equals("") && !(rb.fechaFin.Year == 1) && sedes != null && rb.FKTipoReporte == 0)
            {
                using (SG_SSTContext contex = new SG_SSTContext())
                {


                    actividades = (from act in contex.Tbl_ActividadesActosInseguros
                                   join reporte in contex.Tbl_Reportes on act.FK_Id_Reportes equals reporte.PK_Id_Reportes
                                   join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                   join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa
                                   where e.Nit_Empresa == rb.nitEmpresa

                                    && reporte.Cedula_Quien_Reporta == rb.CedulaQuienReporta
                                    && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) >= DbFunctions.TruncateTime(rb.fechaInicio)
                                    && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) <= DbFunctions.TruncateTime(rb.fechaFin)
                                    && sedes.Contains(reporte.FK_Sede)
                                   select new EDActividadesActosInseguros

                                   {
                                       FKReportes = reporte.PK_Id_Reportes,
                                       ID_ActividadActosInseguros = act.PK_ID_ActividadActosInseguros,
                                       nombreActividad = act.NombreActividad,
                                       RespActividad = act.ResponsableActividad,
                                       FecEjecucion = act.FechaEjecucion,
                                   }).ToList();
                }

                actividadesBuscado = actividades;

            }
            //tipo-sede-fecha
            else if (!rb.FKTipoReporte.Equals("") && sedes != null && !(rb.fechaFin.Year == 1) && rb.CedulaQuienReporta == 0)
            {
                using (SG_SSTContext contex = new SG_SSTContext())
                {

                    actividades = (from act in contex.Tbl_ActividadesActosInseguros
                                   join reporte in contex.Tbl_Reportes on act.FK_Id_Reportes equals reporte.PK_Id_Reportes
                                   join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                   join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa
                                   where e.Nit_Empresa == rb.nitEmpresa

                                    && reporte.FK_Tipo_Reporte == rb.FKTipoReporte
                                    && sedes.Contains(reporte.FK_Sede)
                                    && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) >= DbFunctions.TruncateTime(rb.fechaInicio)
                                    && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) <= DbFunctions.TruncateTime(rb.fechaFin)
                                   select new EDActividadesActosInseguros

                                   {
                                       FKReportes = reporte.PK_Id_Reportes,
                                       ID_ActividadActosInseguros = act.PK_ID_ActividadActosInseguros,
                                       nombreActividad = act.NombreActividad,
                                       RespActividad = act.ResponsableActividad,
                                       FecEjecucion = act.FechaEjecucion,
                                   }).ToList();
                }

                actividadesBuscado = actividades;

            }
            //sede-fecha-documento
            else if (sedes != null && !(rb.fechaFin.Year == 1) && !rb.CedulaQuienReporta.Equals("") && rb.FKTipoReporte == 0)
            {
                using (SG_SSTContext contex = new SG_SSTContext())
                {


                    actividades = (from act in contex.Tbl_ActividadesActosInseguros
                                   join reporte in contex.Tbl_Reportes on act.FK_Id_Reportes equals reporte.PK_Id_Reportes
                                   join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                   join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa
                                   where e.Nit_Empresa == rb.nitEmpresa

                                    && sedes.Contains(reporte.FK_Sede)
                                    && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) >= DbFunctions.TruncateTime(rb.fechaInicio)
                                    && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) <= DbFunctions.TruncateTime(rb.fechaFin)
                                    && reporte.Cedula_Quien_Reporta == rb.CedulaQuienReporta
                                   select new EDActividadesActosInseguros

                                   {
                                       FKReportes = reporte.PK_Id_Reportes,
                                       ID_ActividadActosInseguros = act.PK_ID_ActividadActosInseguros,
                                       nombreActividad = act.NombreActividad,
                                       RespActividad = act.ResponsableActividad,
                                       FecEjecucion = act.FechaEjecucion,
                                   }).ToList();
                }
                actividadesBuscado = actividades;

            }
            //fecha-documento-tipo
            else if (!(rb.fechaFin.Year == 1) && !rb.CedulaQuienReporta.Equals("") && !rb.FKTipoReporte.Equals("") && sedes == null)
            {
                using (SG_SSTContext contex = new SG_SSTContext())
                {

                    actividades = (from act in contex.Tbl_ActividadesActosInseguros
                                   join reporte in contex.Tbl_Reportes on act.FK_Id_Reportes equals reporte.PK_Id_Reportes
                                   join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                   join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa
                                   where e.Nit_Empresa == rb.nitEmpresa

                                    && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) >= DbFunctions.TruncateTime(rb.fechaInicio)
                                    && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) <= DbFunctions.TruncateTime(rb.fechaFin)
                                    && reporte.Cedula_Quien_Reporta == rb.CedulaQuienReporta
                                    && reporte.FK_Tipo_Reporte == rb.FKTipoReporte
                                   select new EDActividadesActosInseguros

                                   {
                                       FKReportes = reporte.PK_Id_Reportes,
                                       ID_ActividadActosInseguros = act.PK_ID_ActividadActosInseguros,
                                       nombreActividad = act.NombreActividad,
                                       RespActividad = act.ResponsableActividad,
                                       FecEjecucion = act.FechaEjecucion,
                                   }).ToList();
                }

                actividadesBuscado = actividades;

            }
            //documento-tipo-sede-fecha
            else if (!rb.CedulaQuienReporta.Equals("") && !rb.FKTipoReporte.Equals("") && sedes != null && !(rb.fechaFin.Year == 1))
            {
                using (SG_SSTContext contex = new SG_SSTContext())
                {

                    actividades = (from act in contex.Tbl_ActividadesActosInseguros
                                   join reporte in contex.Tbl_Reportes on act.FK_Id_Reportes equals reporte.PK_Id_Reportes
                                   join s in contex.Tbl_Sede on reporte.FK_Sede equals s.Pk_Id_Sede
                                   join e in contex.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa
                                   where e.Nit_Empresa == rb.nitEmpresa

                                    && reporte.Cedula_Quien_Reporta == rb.CedulaQuienReporta
                                    && reporte.FK_Tipo_Reporte == rb.FKTipoReporte
                                    && sedes.Contains(reporte.FK_Sede)
                                    && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) >= DbFunctions.TruncateTime(rb.fechaInicio)
                                    && DbFunctions.TruncateTime(reporte.Fecha_Ocurrencia) <= DbFunctions.TruncateTime(rb.fechaFin)


                                   select new EDActividadesActosInseguros

                                   {
                                       FKReportes = reporte.PK_Id_Reportes,
                                       ID_ActividadActosInseguros = act.PK_ID_ActividadActosInseguros,
                                       nombreActividad = act.NombreActividad,
                                       RespActividad = act.ResponsableActividad,
                                       FecEjecucion = act.FechaEjecucion,
                                   }).ToList();
                }

                actividadesBuscado = actividades;

            }
            return actividadesBuscado;
        }

        //Eliminar Imagen

        public bool ELiminarImagenReporte(int idImagen)
        {

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        ImagenesReportes img = context.Tbl_ImagenesReportes.Find(idImagen);
                        context.Tbl_ImagenesReportes.Remove(img);
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ReporteManager), string.Format("Error al eliminar la imagen del reporte  de la base de datos  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }


        public EDImagenesReportes ObtenerImagen(int idImagen)
        {
            EDImagenesReportes imagen = null;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                ImagenesReportes img = context.Tbl_ImagenesReportes.Find(idImagen);
                imagen = new EDImagenesReportes
                {

                    IDImagenesReportes = img.PK_ImagenesReportes,
                    rutaArchivo = img.ruta,
                    FKReportes = img.FK_Id_Reportes,


                };
            }
            return imagen;
        }
        // APP

        public List<EDTipoReporte> ConsultarTipoReporte()
        {
            List<EDTipoReporte> reportes = new List<EDTipoReporte>();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                reportes = (from tr in context.Tbl_Tipo_Reporte

                            select new EDTipoReporte
                            {
                                IdTipoReporte = tr.Pk_Id_Tipo_Reporte,
                                DescripcionTipoReporte = tr.Descripcion_Tipo_Reporte,
                            }).ToList();
            }
            return reportes;
        }


        public EDReporteApp GuardarReporteAPP(EDReporteApp reporte)
        {
            
        
            int consecutivo = 0;
     
            var registros = ObtenerReportesPorEmpresa(reporte.nitEmpresa);
            if (registros.Count() == 0)
            {
                consecutivo = 1;
            }
            else
            {
                consecutivo = registros.Count() + 1;
            }

            if (reporte.proceso.Equals("0"))
            {
                reporte.proceso = null;

            }

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    string razonSocial = "";
                    RegistraLog registraLog = new RegistraLog();
                    
                    razonSocial = context.Tbl_Empresa.Where(e => e.Nit_Empresa.Equals(reporte.nitEmpresa)).Select(e => e.Razon_Social).FirstOrDefault();
                    reporte.nombreEmpresa = razonSocial;
               
                    try
                    {
                        Reporte rep = new Reporte
                        {
                            FK_NitEmpresa=reporte.nitEmpresa,
                            RazonSocialEmpresa=reporte.nombreEmpresa,
                            
                            FK_Sede = reporte.sede,
                            FK_Tipo_Reporte = reporte.tipo,
                            FK_Proceso = reporte.proceso,
                            Area_Lugar = reporte.area,
                            Fecha_Ocurrencia = reporte.fechaEvento,
                            fechaSistema=DateTime.Now,
                            descripcion_Reporte = reporte.descripcion,
                            Causa_Reporte = reporte.causa,
                            Sugerencias_Reporte = reporte.sugerencia,
                            medioAcceso = true,
                            Cedula_Quien_Reporta = reporte.cedulaQuienReporta,
                            ConsecutivoReporte=consecutivo
                        };

                        context.Tbl_Reportes.Add(rep);
                        context.SaveChanges();

                        if (reporte.imagen != null)
                        {
                            List<ImagenesReportes> imagenes = new List<ImagenesReportes>();

                            ImagenesReportes imagen = new ImagenesReportes();
                            imagen.ruta = reporte.nombreImagen;
                            imagen.FK_Id_Reportes = rep.PK_Id_Reportes;
                            imagenes.Add(imagen);

                            context.Tbl_ImagenesReportes.AddRange(imagenes);
                            context.SaveChanges();


                        }


                        transaction.Commit();

                        return reporte;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(ReporteManager), string.Format("Error al guardar el reporte en la base de datos  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        transaction.Rollback();
                        return reporte;
                    }
                }
            }
        }
}
}