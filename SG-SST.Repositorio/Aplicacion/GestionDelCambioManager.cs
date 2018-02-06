using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.Models.Aplicacion;
using SG_SST.EntidadesDominio.Aplicacion;
using SG_SST.Interfaces.Aplicacion;
using System.Data;
using System.Data.Entity;
using SG_SST.Models;
using SG_SST.Audotoria;

namespace SG_SST.Repositorio.Aplicacion
{
    public class GestionDelCambioManager : IGestionDelCambio
    {


        public EDGestionDelCambio GuardarGestionDelCambio(EDGestionDelCambio GestionCambio)
        {
            GestionDelCambio acccam = new GestionDelCambio();

            acccam.Fecha = GestionCambio.Fecha;
            acccam.DescripcionDeCambio = GestionCambio.DescripcionDeCambio;
            acccam.FK_Clasificacion_De_Peligro = GestionCambio.FK_Clasificacion_De_Peligro;
            acccam.FK_Tipo_De_Peligro = GestionCambio.FK_Tipo_De_Peligro;
            acccam.RequisitoLegal = GestionCambio.RequisitoLegal;
            acccam.Recomendaciones = GestionCambio.Recomendaciones;
            acccam.FechaEjecucion = GestionCambio.FechaEjecucion;
            acccam.FechaSeguimiento = GestionCambio.FechaSeguimiento;
            acccam.FK_Id_Rol = GestionCambio.FK_Id_Rol;
            acccam.FK_Empresa = GestionCambio.fkempresa;
            acccam.Otro = GestionCambio.Otro;

            try
            {
                using (SG_SSTContext db1 = new SG_SSTContext())
                {
                    if (GestionCambio.PK_GestionDelCambio > 0)
                    {



                        if (GestionCambio.FK_Tipo_De_Peligro != 8 && GestionCambio.FK_Clasificacion_De_Peligro != 46)
                        {
                            GestionDelCambio ObregLeg = db1.Tbl_GestionDelCambio.Where(g => g.PK_GestionDelCambio == GestionCambio.PK_GestionDelCambio).FirstOrDefault();
                            ObregLeg.Fecha = ObregLeg.Fecha;
                            ObregLeg.DescripcionDeCambio = GestionCambio.DescripcionDeCambio;
                            ObregLeg.FK_Clasificacion_De_Peligro = GestionCambio.FK_Clasificacion_De_Peligro;
                            ObregLeg.FK_Tipo_De_Peligro = GestionCambio.FK_Tipo_De_Peligro;
                            ObregLeg.RequisitoLegal = GestionCambio.RequisitoLegal;
                            ObregLeg.Recomendaciones = GestionCambio.Recomendaciones;
                            ObregLeg.FechaEjecucion = GestionCambio.FechaEjecucion;
                            ObregLeg.FechaSeguimiento = GestionCambio.FechaSeguimiento;
                            ObregLeg.FK_Id_Rol = GestionCambio.FK_Id_Rol;
                            ObregLeg.FK_Empresa = GestionCambio.fkempresa;
                            ObregLeg.Otro = "";
                            db1.Entry(ObregLeg).State = EntityState.Modified;
                            db1.SaveChanges();
                        
                        
                        }
                        else
                            if (GestionCambio.FK_Tipo_De_Peligro == 8 && GestionCambio.FK_Clasificacion_De_Peligro == 46)                            
                            {
                                GestionDelCambio ObregLeg = db1.Tbl_GestionDelCambio.Where(g => g.PK_GestionDelCambio == GestionCambio.PK_GestionDelCambio).FirstOrDefault();
                                ObregLeg.Fecha = ObregLeg.Fecha;
                                ObregLeg.DescripcionDeCambio = GestionCambio.DescripcionDeCambio;
                                ObregLeg.FK_Clasificacion_De_Peligro = GestionCambio.FK_Clasificacion_De_Peligro;
                                ObregLeg.FK_Tipo_De_Peligro = GestionCambio.FK_Tipo_De_Peligro;
                                ObregLeg.RequisitoLegal = GestionCambio.RequisitoLegal;
                                ObregLeg.Recomendaciones = GestionCambio.Recomendaciones;
                                ObregLeg.FechaEjecucion = GestionCambio.FechaEjecucion;
                                ObregLeg.FechaSeguimiento = GestionCambio.FechaSeguimiento;
                                ObregLeg.FK_Id_Rol = GestionCambio.FK_Id_Rol;
                                ObregLeg.FK_Empresa = GestionCambio.fkempresa;
                                ObregLeg.Otro = GestionCambio.Otro;
                                db1.Entry(ObregLeg).State = EntityState.Modified;
                                db1.SaveChanges();                           
                            
                            }







                    }
                    else
                    {
                        db1.Tbl_GestionDelCambio.Add(acccam);
                        db1.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.ToString();
            }
            return GestionCambio;
        }
                

        public bool EliminarGestionDelCambio(int idgestiondelcambio)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        GestionDelCambio Gest = context.Tbl_GestionDelCambio.Find(idgestiondelcambio);
                        context.Tbl_GestionDelCambio.Remove(Gest);                      
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {               
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
        


    }
}
