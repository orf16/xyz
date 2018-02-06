
namespace SG_SST.Repositories.LiderazgoGerencial.Repositories
{
    using SG_SST.Models;
    using SG_SST.Models.LiderazgoGerencial;
    using SG_SST.Repositories.LiderazgoGerencial.IRepositories;
    //using SG_SST.Utilidades.Traza;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Data;
    using System.Data.Entity;

    public class PresupuestoRepositorio : IPresupuestoRepositorio
    {
        SG_SSTContext db;
        public PresupuestoRepositorio()
        {
            db = new SG_SSTContext();
        }

        public PresupuestoRepositorio(SG_SSTContext db)
        {
            this.db = db;
        }

        public bool GuardarPresupuesto(List<ActividadPresupuesto> actividadesPresupuesto)
        {

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {                   
                    foreach (ActividadPresupuesto ac in actividadesPresupuesto)
                    {
                        if (ac.PK_Actividad_Presupuesto > 0)
                        {
                            if (ac.actividadesPresupuesto != null)
                            {                               
                                List<ActividadPresupuesto> lisAux = ac.actividadesPresupuesto.ToList();
                                foreach (ActividadPresupuesto actividadesSecun in lisAux)
                                {
                                    if (actividadesSecun.PK_Actividad_Presupuesto > 0)
                                    {
                                        foreach (PresupuestoPorMes pxm in actividadesSecun.presupuestosPorMes)
                                        {
                                            db.Entry(pxm).State = EntityState.Modified;
                                        }
                                        db.Entry(actividadesSecun).State = EntityState.Modified;
                                    }
                                }
                                ac.actividadesPresupuesto = lisAux;
                            }
                            else
                            {
                                foreach (PresupuestoPorMes pxm in ac.presupuestosPorMes)
                                {
                                    db.Entry(pxm).State = EntityState.Modified;
                                }                                
                            }
                            db.Entry(ac).State = EntityState.Modified;
                        }
                        else
                        {
                            if (ac.actividadesPresupuesto != null)
                            {
                                List<ActividadPresupuesto> lisAux = ac.actividadesPresupuesto.ToList();
                                ac.actividadesPresupuesto = null;
                                db.Tbl_Actividad_Presupuesto.Add(ac);
                                foreach (ActividadPresupuesto actividadesSecun in lisAux)
                                {
                                    if (actividadesSecun.PK_Actividad_Presupuesto > 0)
                                    {
                                        foreach (PresupuestoPorMes pxm in actividadesSecun.presupuestosPorMes)
                                        {
                                            db.Entry(pxm).State = EntityState.Modified;
                                        }
                                        db.Entry(actividadesSecun).State = EntityState.Modified;
                                    }
                                    else
                                    {
                                        db.Tbl_Actividad_Presupuesto.Add(actividadesSecun);
                                    }
                                }
                                ac.actividadesPresupuesto = lisAux;
                            }
                            else
                            {
                                db.Tbl_Actividad_Presupuesto.Add(ac);
                            }

                        }                        
                    }
                    ActividadPresupuesto acti = actividadesPresupuesto.FirstOrDefault();
                    if (acti.PK_Actividad_Presupuesto > 0)
                    {
                        if (acti.actividadesPresupuesto != null)
                        {
                            Presupuesto p = acti.actividadesPresupuesto.FirstOrDefault().presupuestosPorMes.FirstOrDefault().Presupuesto;
                            
                            db.Entry(p.presupuestosPorAnio.FirstOrDefault()).State = EntityState.Modified;
                            db.Entry(p).State = EntityState.Modified;
                        }
                        else {
                            Presupuesto p = acti.presupuestosPorMes.FirstOrDefault().Presupuesto;
                            db.Entry(p.presupuestosPorAnio.FirstOrDefault()).State = EntityState.Modified;
                            db.Entry(p).State = EntityState.Modified;
                        }
                    }
                    db.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    //RegistroInformacion.EnviarError<ActividadPresupuesto>(ex.Message);
                    transaction.Rollback();
                    return false;
                }
            }
        }



        public bool EliminarPresupuesto(List<ActividadPresupuesto> actividadesPresupuesto, int PK_Presupuesto)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    List<ActividadPresupuesto> listAux = new List<ActividadPresupuesto>();
                    foreach (ActividadPresupuesto ap in actividadesPresupuesto)
                    {
                        if (ap.actividadesPresupuesto != null)
                        {
                            foreach (ActividadPresupuesto aphijas in ap.actividadesPresupuesto)
                            {
                                //db.Tbl_Actividad_Presupuesto.Remove(aphijas);
                                listAux.Add(aphijas);
                            }
                        }

                    }
                    foreach (ActividadPresupuesto ap in listAux)
                    {
                        db.Tbl_Actividad_Presupuesto.Remove(ap);
                    }
                    db.Tbl_Actividad_Presupuesto.RemoveRange(actividadesPresupuesto);
                    Presupuesto presupuesto = db.Tbl_Presupuesto.Find(PK_Presupuesto);
                    db.Tbl_Presupuesto.Remove(presupuesto);
                    db.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    //RegistroInformacion.EnviarError<ActividadPresupuesto>(ex.Message);
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public List<PresupuestoPorAnio> ObtenerPresupuestosSedePorAnio(int pk_Sede, int periodo)
        {
            return db.Tbl_Presupuesto_Por_Anio.Where(ppa => ppa.FK_Sede == pk_Sede && ppa.Periodo == periodo).ToList();
        }


        public List<ActividadPresupuesto> ObtenerActividadesPorPresupuesto(int PK_PresupuestoPorAnio)
        {

            PresupuestoPorAnio presupuestoAnio = ObtenerPresupuestoPorAnio(PK_PresupuestoPorAnio);



            List<ActividadPresupuesto> lista1 = db.Tbl_Actividad_Presupuesto.Include(ap => ap.presupuestosPorMes)
                                    .Include(ap => ap.actividadesPresupuesto.Select(aps => aps.presupuestosPorMes))
                                    .Where(ap => ap.actividadPres == null && ap.actividadesPresupuesto.FirstOrDefault().presupuestosPorMes.FirstOrDefault().FK_Presupuesto == presupuestoAnio.FK_Presupuesto).ToList();

            List<ActividadPresupuesto> lista2 = db.Tbl_Actividad_Presupuesto
                                    .Include(ap => ap.presupuestosPorMes)
                                   .Include(ap => ap.actividadPres)
                                   .Where(ap => ap.actividadPres == null && ap.presupuestosPorMes.FirstOrDefault().FK_Presupuesto == presupuestoAnio.FK_Presupuesto).ToList();


            List<ActividadPresupuesto> listaNueva = new List<ActividadPresupuesto>();
            lista1.AddRange(lista2);



            return lista1;
        }

        public PresupuestoPorAnio ObtenerPresupuestoPorAnio(int PK_PresupuestoPorAnio)
        {
            return db.Tbl_Presupuesto_Por_Anio.Find(PK_PresupuestoPorAnio);
        }

        public List<PresupuestoPorMes> ObtenerPresupuestoPorMesActividad(int pkActividad)
        {
            return db.Tbl_Presupuesto_Por_Mes.Where(ppm => ppm.FK_Actividad_Presupuesto == pkActividad).ToList();        
        }

        public ActividadPresupuesto ObtenerActividadPresupuesto(int pkActividad)
        {
            return db.Tbl_Actividad_Presupuesto.Find(pkActividad);
        }

        public bool EliminarActividad(int pkActividad)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Tbl_Actividad_Presupuesto.Remove(ObtenerActividadPresupuesto(pkActividad));
                    db.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    //RegistroInformacion.EnviarError<ActividadPresupuesto>(ex.Message);
                    transaction.Rollback();
                    return false;
                }
            }
            
        }
    }
}