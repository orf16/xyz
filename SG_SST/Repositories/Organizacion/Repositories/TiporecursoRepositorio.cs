using SG_SST.Models;
using SG_SST.Models.Organizacion;
using SG_SST.Repositories.Organizacion.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SG_SST.Repositories.Organizacion.Repositories
{
    public class TiporecursoRepositorio:ITiporecursoRepositorio
    {
        SG_SSTContext db;
        public TiporecursoRepositorio()
        {
            db = new SG_SSTContext();
        }
        public TiporecursoRepositorio(SG_SSTContext db)///mandar el contexto que se va utilizar en las otras Capas.
        {
            this.db = db;
        }


        public bool GuardarTipoRecurso(TipoRecurso tiporecurso)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (tiporecurso.Pk_Id_TipoRecurso>0)
                    {
                        ModificarTipoRecurso(tiporecurso);
                    }
                    else
                    {
                        db.Tbl_Tipo_Recurso.Add(tiporecurso);
                    }
                    db.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }

        }

        //Metodo para Modificar el Tipo de Recurso.
        public void ModificarTipoRecurso(TipoRecurso tiporecurso)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {

                    if (tiporecurso != null)
                    {
                        db.Entry(tiporecurso).State = EntityState.Modified;

                    }
                    else
                    {

                        db.Tbl_Tipo_Recurso.Add(tiporecurso);
                        db.SaveChanges();
                        transaction.Commit();
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();

                }
            }
        }

        public List<TipoRecurso> ObtenerTipoRecurso()
        {

            List<TipoRecurso> tipoRecursos = db.Tbl_Tipo_Recurso.Where(tp => tp.Pk_Id_TipoRecurso != null).ToList();
            return tipoRecursos;
        }


    }
}