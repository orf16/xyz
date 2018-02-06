
namespace SG_SST.Repositorio.Aplicacion
{
    using SG_SST.Audotoria;
    using SG_SST.EntidadesDominio.Aplicacion;
    using SG_SST.Interfaces.Aplicacion;
    using SG_SST.Models;
    using SG_SST.Models.Aplicacion;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Configuration;
    public class AdquisicionBienesManager : IAdquicisionBienes
    {
        public List<EDManualAdquisicion> ObtenerManualAdquisiones(int idEmpresa)
        {
            List<EDManualAdquisicion> Man_Adq = null;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                Man_Adq = (from ma in context.Tbl_ManualGuiaAdBienes
                          where ma.FK_Empresa == idEmpresa
                          select new EDManualAdquisicion
                          {
                              Id_Manuales_Bienes = ma.PK_ManualGuiaAdBienes,
                              Nombre_Manual = ma.Nombre_Manual,
                              Fk_Empresa = ma.FK_Empresa,
                          }).ToList();
            }
            return Man_Adq;
        }

        public bool GuardarManualAdquisiones(EDManualAdquisicion documento)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        ManualGuiaAdBienes doc = new ManualGuiaAdBienes
                        {
                            Nombre_Manual = documento.Nombre_Manual,
                            FK_Empresa = documento.Fk_Empresa,
                        };
                        context.Tbl_ManualGuiaAdBienes.Add(doc);
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(AdquisicionBienesManager), string.Format("Error al guardar el manual de adquisión de bienes en la base de datos  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
        public string ObtenerManualAdquisionBienes(int idManualAdq)
        {
            string doc = null;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                doc = context.Tbl_ManualGuiaAdBienes.Find(idManualAdq).Nombre_Manual;                
            }
            return doc;
        }

        public bool EliminarManualAdqBienes(int idManualAdq)
        {

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {

                        ManualGuiaAdBienes manual  = context.Tbl_ManualGuiaAdBienes.Where(p => p.PK_ManualGuiaAdBienes == idManualAdq).FirstOrDefault();
                        context.Tbl_ManualGuiaAdBienes.Remove(manual);
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(AdquisicionBienesManager), string.Format("Error al eliminar el manual en la base de datos  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}
