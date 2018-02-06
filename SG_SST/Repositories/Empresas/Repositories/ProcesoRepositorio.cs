
namespace SG_SST.Repositories.Empresas.Repositories
{
    using SG_SST.Models;
    using SG_SST.Models.Empresas;
    using SG_SST.Repositories.Empresas.IRepositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    public class ProcesoRepositorio : IProcesoRepositorio
    {
        SG_SSTContext db;

        public ProcesoRepositorio()
        {
            db = new SG_SSTContext();
        }

        public List<Proceso> ObtenerProcesosPrincipales(int Pk_Empresa)
        {
            List<Proceso> procesos = (from p in db.Tbl_Procesos
                                      join pe in db.Tbl_ProcesoEmpresa on p.Pk_Id_Proceso equals pe.Fk_Id_Proceso
                                      where pe.Fk_Id_Empresa == Pk_Empresa
                                      select p).ToList();
             //{
             //    Pk_Id_Proceso = p.Pk_Id_Proceso,
             //    Descripcion_Proceso = p.Descripcion_Proceso,
                 
             //}).ToList(); 
            //List<Proceso> procesos = db.Tbl_Procesos.Where(p =>p.Fk_Id_Proceso == null).ToList();
            return procesos;
        }

        public List<Proceso> ObtenerSubProcesos(int Pk_ProcesoPrincipal)
        {
            List<Proceso> procesos = db.Tbl_Procesos.Where(p => p.Fk_Id_Proceso == Pk_ProcesoPrincipal).ToList();
            return procesos;
        }


        public Proceso ObtenerProceso(int Pk_Proceso) 
        {
            return db.Tbl_Procesos.Find(Pk_Proceso);
        
        }
    }
}