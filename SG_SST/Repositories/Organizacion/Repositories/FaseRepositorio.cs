using SG_SST.Models;
using SG_SST.Models.Organizacion;
using SG_SST.Repositories.Organizacion.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SG_SST.Repositories.Organizacion.Repositories
{
    public class FaseRepositorio : IFaseRepositorio
    {
        SG_SSTContext db;
        public FaseRepositorio()
        {
            db = new SG_SSTContext();
        }
        public FaseRepositorio(SG_SSTContext db)///mandar el contexto que se va utilizar en las otras Capas.
        {
            this.db = db;
        }


        public bool GuardarFase(Fase fase)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (fase.Pk_Id_Fase > 0)
                    {
                        ModificarFase(fase);
                    }
                    else
                    {
                        db.Tbl_Fase.Add(fase);
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


        ///Metodo para modificar las  fases...
        ///
        public void ModificarFase (Fase fase)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {

                    if (fase != null)
                    {
                        db.Entry(fase).State = EntityState.Modified;
                        
                    }
                    else
                    {

                        db.Tbl_Fase.Add(fase);
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


      public List<Fase> ObtenerFase()
    {

        List<Fase> fases = db.Tbl_Fase.Where(f => f.Pk_Id_Fase != null).ToList();
            return fases;
    }


    }

}