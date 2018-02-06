
namespace SG_SST.Repositories.Organizacion.Repositories

{
    using SG_SST.Models;
    using SG_SST.Models.Planificacion;
    using SG_SST.Repositories.Planificacion.IRepositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Data;
    using System.Data.Entity;
    using SG_SST.Dtos.Planificacion;
    using SG_SST.Models.Empresas;
    using SG_SST.Repositories.Organizacion.IRepositories;
    using SG_SST.Models.Organizacion;

    public class RecursosSedesRepositorio:IRecursosSedesRepositorio
    {
        SG_SSTContext db;

        public RecursosSedesRepositorio()
        {

            db = new SG_SSTContext();
        }



        public List<RecursoporSede> ObtenerRecursoSede(int Pk_Sede, int Periodo)
        {
            List<RecursoporSede> recursoporSede = db.Tbl_RecursoporSede
                .Include(z=> z.Sede)
                .Include(t => t.Recurso.RecursosFase)
                .Include(w=> w.Recurso.RecursosTipoRecursos)
                .Include(r=> r.Recurso)
                .Where(rs => rs.Fk_Id_Sede == Pk_Sede && rs.Recurso.Periodo==Periodo).ToList();
            return (recursoporSede);
          }

        public List<RecursoporSede> validarRecursoSede(int Pk_Sede, int Periodo, int Pk_Id_fase, int Pk_Id_tiporecurso)
        {

            List<RecursoporSede> validarecursos = db.Tbl_RecursoporSede
                .Include(z=> z.Sede)
                .Include(t => t.Recurso.RecursosFase)
                .Include(w=> w.Recurso.RecursosTipoRecursos)
                .Include(r=> r.Recurso)
                .Where(rs => rs.Fk_Id_Sede == Pk_Sede && 
                    rs.Recurso.Periodo==Periodo && 
                    rs.Recurso.RecursosFase.First().Fase.Pk_Id_Fase==Pk_Id_fase && 
                    rs.Recurso.RecursosTipoRecursos.First().TipoRecurso.Pk_Id_TipoRecurso==Pk_Id_tiporecurso).ToList();

            return (validarecursos);
        }

    }
}