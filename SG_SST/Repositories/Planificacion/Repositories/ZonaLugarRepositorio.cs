    using SG_SST.Models;
    using SG_SST.Models.Planificacion;
    using SG_SST.Repositories.Planificacion.IRepositories;
    using System.Collections.Generic;
    using System.Linq;
namespace SG_SST.Repositories.Planificacion.Repositories
{
    public class ZonaLugarRepositorio:IConsultarZonasRepositorio
    {
         SG_SSTContext db;
         public ZonaLugarRepositorio()
        {
            db = new SG_SSTContext();
        }

         public List<Peligro> ConsultarZonasLugares(int idEmpresa)
        {
            return db.Tbl_Peligro.Where(cp => cp.Sede.Fk_Id_Empresa == idEmpresa).ToList();
        }
    }
}